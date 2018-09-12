using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEQUALITY.HelpClass;
using EMEWEEntity;
using System.Linq.Expressions;
using EMEWEDAL;
using System.Data.Linq.SqlClient;

namespace EMEWEQUALITY.NewAdd
{
    public partial class SMSSendRecordForm : Form
    {
        public SMSSendRecordForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// SMSRecord
        /// </summary>
        Expression<Func<SMSRecord, bool>> expr = null;
        public MainFrom mf;//主窗体
        string Cntr_No = "";
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        string Sql = "1=1";
        PageControl page = new PageControl();
        /// </summary>
        /// 排序字段及方式[以空格格开]
        /// </summary>
        private string orderbywhere = "SMSRecord_Id desc";

        /// <summary>
        /// 用户权限
        /// </summary>
        private void UserRoleInfo()
        {
            string peri = Common.PERI;
            if (!string.IsNullOrEmpty(peri))
            {
                if (peri.Substring(4, 1) != "1")
                {
                    tsbtnDel.Enabled = false;
                }
            }
        }

        private void SMSSendRecordForm_Load(object sender, EventArgs e)
        {
            LoadData("");
            Time();
            UserRoleInfo();
            UnusualType();
        }

        /// <summary>
        /// 设置默认起始时间 提前一个月
        /// </summary>
        private void Time()
        {
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
        }
        /// <summary>
        /// 绑定异常类型
        /// </summary>
        private void UnusualType()
        {
            string Sql = "select * from dbo.TestItems";
            DataSet ds = LinQBaseDao.Query(Sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.cboUnusualType_Name.DataSource = ds.Tables[0];
                this.cboUnusualType_Name.DisplayMember = "TestItems_NAME";
                this.cboUnusualType_Name.ValueMember = "TestItems_ID";
                this.cboUnusualType_Name.SelectedIndex = -1;
            }
            else
            {
                cboUnusualType_Name.DataSource = null;
            }

        }
        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private void LoadData(string strName)
        {
            try
            {
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                dgvSMSRecord.DataSource = page.BindBoundControl<SMSRecord>(strName, txtCurrentPage2, lblPageCount2, expr, orderbywhere);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("短信记录()加载显示" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click()
        {
            for (int i = 0; i < dgvSMSRecord.Rows.Count; i++)
            {
                dgvSMSRecord.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click()
        {
            for (int i = 0; i < this.dgvSMSRecord.Rows.Count; i++)
            {
                dgvSMSRecord.Rows[i].Selected = false;
            }
        }
        /// <summary>
        /// 设置每页显示最大条数事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData("");
        }
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tslSelectAll") // 全选
            {
                tslSelectAll_Click();
                return;
            }
            if (e.ClickedItem.Name == "tslNotSelect") // 取消全选
            {
                tslNotSelect_Click();
                return;
            }
            if (e.ClickedItem.Name == "tsbtnDel")//删除
            {
                Delete();
            }
            this.dgvSMSRecord.DataSource = page.BindBoundControl<SMSRecord>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr, orderbywhere);
        }

        /// <summary>
        /// 单击查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string begin = dateTimePicker1.Value.ToString();  // + " 00:00:00"
                string end = dateTimePicker2.Value.ToString();  // + "23:59:59 "
                expr = (Expression<Func<SMSRecord, bool>>)PredicateExtensionses.True<SMSRecord>();
                if (!string.IsNullOrEmpty(txtCNTR_NO.Text.Trim()))
                {
                    expr = expr.And(c => SqlMethods.Like(c.SMSRecord_DRAW_EXAM_INTERFACE_CNTR_NO, "%" + txtCNTR_NO.Text.Trim() + "%"));
                }
                if (cboUnusualType_Name.SelectedIndex > -1)
                {
                    expr = expr.And(c => SqlMethods.Like(c.SMSRecord_Unusual_Id.ToString(), "%" + cboUnusualType_Name.SelectedValue.ToString() + "%"));
                }
                if (!string.IsNullOrEmpty(txtReceivePhone.Text.Trim()))
                {
                    expr = expr.And(c => SqlMethods.Like(c.SMSRecord_ReceivePhone.ToString(), "%" + txtReceivePhone.Text.Trim() + "%"));
                }
                if (Convert.ToDateTime(begin) > Convert.ToDateTime(end))
                {
                    MessageBox.Show("查询起止时间不能大于截止时间");
                    return;
                }
                //时间
                if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString()))
                {
                    expr = expr.And(n => n.SMSRecord_SendTime >= Common.GetDateTime(begin));
                }
                if (!string.IsNullOrEmpty(dateTimePicker2.Value.ToString()))
                {
                    expr = expr.And(n => n.SMSRecord_SendTime <= Common.GetDateTime(end));
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadData("");
            }
        }

        private void tslExport_Click(object sender, EventArgs e)
        {
            string fileName = "理文短信发送记录数据统计Excel报表-" + DateTime.Now.ToShortDateString();
            tslExport_Click(fileName, dgvSMSRecord);
        }

        /// <summary>
        /// 导出Excel 的方法
        /// </summary>
        private void tslExport_Click(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            //bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消 
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1 


            //写入标题
            for (int i = 1; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 1; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            Microsoft.Office.Interop.Excel.Range rang = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[myDGV.Rows.Count + 2, 2]);
            rang.NumberFormat = "000000000000";

            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    //fileSaved = true;
                }
                catch (Exception ex)
                {
                    //fileSaved = false;
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }

            }
            //else
            //{
            //    fileSaved = false;
            //}
            xlApp.Quit();
            GC.Collect();//强行销毁 
            // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
            MessageBox.Show(fileName + ",保存成功", "提示", MessageBoxButtons.OK);

        }

        private void btnCler_Click(object sender, EventArgs e)
        {
            Emety();
        }
        /// <summary>
        /// 清空
        /// </summary>
        private void Emety()
        {
            txtCNTR_NO.Text = "";
            txtReceivePhone.Text = "";
            cboUnusualType_Name.SelectedIndex = -1;
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            int index = 0;
            if (this.dgvSMSRecord.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择至少一行进行删除");
                return;
            }
            for (int i = 0; i < dgvSMSRecord.SelectedRows.Count; i++)
            {
                string SqlDelete = "delete SMSRecord where SMSRecord_Id=" + dgvSMSRecord.SelectedRows[i].Cells["SMSRecord_Id"].Value.ToString();
                int num = LinQBaseDao.ExecuteSql(SqlDelete);
                if (num > 0)
                {
                    index++;
                }
            }
            if (index > 0)
            {
                MessageBox.Show("删除成功");
                LoadData("");
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }
    }

}
