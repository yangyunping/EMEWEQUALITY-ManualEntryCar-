using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//导入引用
using EMEWEQUALITY.HelpClass;
using EMEWEDAL;
using EMEWEEntity;
using System.Linq.Expressions;
using EMEWE.Common;
using WindowsFormsApplication26;
using System.Threading;
using Word = Microsoft.Office.Interop.Word;
using System.IO;

namespace EMEWEQUALITY.Statistics
{
    public partial class ReturnStillPackForm : Form
    {
        public ReturnStillPackForm()
        {
            InitializeComponent();
        }

        public MainFrom mf;//主窗体
        /// <summary>
        /// 车辆表  QCInfo
        /// </summary>
        Expression<Func<View_QCInfo, bool>> expr = n => n.QCInfo_RECV_RMA_METHOD=="退货";
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();
        int iQcInfoID = 0;
        /// <summary>
        /// 排序字段及方式[以空格格开]
        /// </summary>
        private string orderbywhere = "QCInfo_RETURNBAG_TIME desc";

        /// <summary>
        /// 退货还包 加载显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnStillPackForm_Load(object sender, EventArgs e)
        {
            this.dgvCarStatisticsQC.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            InitUser();
        }
        /// <summary>
        /// 绑定质检状态的comboBox值
        /// </summary>
        private void BindCheckState()
        {
            cbxQCInfoState.DataSource = DictionaryDAL.GetValueStateDictionary("质检状态");
            this.cbxQCInfoState.DisplayMember = "Dictionary_Name";
            cbxQCInfoState.ValueMember = "Dictionary_ID";
            if (cbxQCInfoState.DataSource != null)
            {

                cbxQCInfoState.SelectedIndex = cbxQCInfoState.Items.Count - 1;
            }
        }
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {

            mf = new MainFrom();
            BindCheckState();//绑定质检状态
                
           
            //DateTimeBeginISNull();
            //DateTimeEndIsNull();
            TimeEndIsNull();
           // LoadData();
            btnSearch_Click(null, null);
        }
        /// <summary>
        /// 初始化时间
        /// </summary>
        private void TimeEndIsNull()
        {
            DateTime Time = DateTime.Now.AddDays(0);
            txtbeginTime.Value = Time;
            txtendTime.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {

                this.dgvCarStatisticsQC.DataSource = null;
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                dgvCarStatisticsQC.DataSource = page.BindBoundControl<View_QCInfo>("", txtCurrentPage2, lblPageCount2, expr, orderbywhere);

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("日记管理 修改质检数据统计(CarStatisticsForm)加载显示" + ex.Message.ToString());
            }

        }
        /// <summary>
        /// 退货还包统计搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
               
                    expr = (Expression<Func<View_QCInfo, bool>>)PredicateExtensionses.True<View_QCInfo>();
                   
                int i = 0;
                if (this.cbxQCInfoState.SelectedValue != null)//检测状态
                {
                    int stateID = Converter.ToInt(cbxQCInfoState.SelectedValue.ToString());
                    if (stateID > 0)
                    {

                        expr = expr.And(n => n.Dictionary_ID == Converter.ToInt(cbxQCInfoState.SelectedValue.ToString()) );
                        i++;
                    }
                }
                if (this.txtQCInfoCarNo.Text != "")//车牌号
                {
                    expr = expr.And(n => n.CNTR_NO.Contains(txtQCInfoCarNo.Text));
                    i++;
                }
                if (this.txtWaitWEIGHTTICKETNO.Text != "")//磅单号
                {
                    expr = expr.And(n => n.WEIGHT_TICKET_NO.Contains(txtWaitWEIGHTTICKETNO.Text));
                    i++;
                }
                if (this.txtWaitPO_NO.Text != "")//采购单
                {
                    expr = expr.And(n => n.PO_NO.Contains(txtWaitPO_NO.Text));
                    i++;
                }
                if (this.txtWaitSHIPMENT_NO.Text != "")//送货单
                {
                    expr = expr.And(n => n.SHIPMENT_NO.Contains(txtWaitSHIPMENT_NO.Text));
                    i++;
                }
                //string beginTime = txtbeginTime.Text.Trim();
                //string endTime = txtendTime.Text.Trim();
                //string begin = beginTime;  // + " 00:00:00"
                //string end = endTime;  // + "23:59:59 "

                string beginTime = txtbeginTime.Value.ToString("yyyy-MM -dd ");
                string endTime = txtendTime.Value.ToString("yyyy-MM -dd ");
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "
                if (beginTime != "")//开始时间   QCInfo_TIME
                {
                    expr = expr.And(n => n.QCIInfo_EndTime >= Common.GetDateTime(begin));
                    i++;
                }
                if (endTime != "")//结束时间   QCInfo表字段:QCIInfo_EndTime
                {
                    expr = expr.And(n => n.QCIInfo_EndTime <= Common.GetDateTime(end));
                    i++;
                }
                if (beginTime != "" && endTime != "")
                {
                    if (Common.GetDateTime(begin) > Common.GetDateTime(end))
                    {
                        MessageBox.Show("查询开始时间不能大于结束时间！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtbeginTime.CustomFormat = " ";
                        this.txtendTime.CustomFormat = " ";
                        //expr = (Expression<Func<View_QCInfo, bool>>)PredicateExtensionses.True<View_QCInfo>();
                        return;
                    }
                    //else
                    //{
                    //    expr = expr.And(n => n.QCInfo_UPDATE_TIME == Common.GetDateTime(begin) && n.QCInfo_UPDATE_TIME == Common.GetDateTime(end));
                    //    i++;
                    //}
                }
                //if (i == 0)
                //{
                //    expr = n => n.QCInfo_RETURNBAG_WEIGH > 0; ;
                //} 
                expr = expr.And( n => n.QCInfo_RECV_RMA_METHOD == "退货");
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("项目检测管理 selectTJ()" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click()
        {
            for (int i = 0; i < dgvCarStatisticsQC.Rows.Count; i++)
            {
                dgvCarStatisticsQC.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click()
        {
            for (int i = 0; i < this.dgvCarStatisticsQC.Rows.Count; i++)
            {
                dgvCarStatisticsQC.Rows[i].Selected = false;
            }
        }

        /// <summary>
        /// 导出Excel 
        /// </summary>
        private void tslExport_Click(object sender, EventArgs e)
        {
            string fileName = "理文检测系统退货还包统计Excel报表-" + DateTime.Now.ToShortDateString();
            tslExport_Click(fileName, dgvCarStatisticsQC);

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

        /// <summary>
        /// 分 页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tslSelectAll")//全选
            {
                tslSelectAll_Click();
                return;
            }
            if (e.ClickedItem.Name == "tslNotSelect")//取消全选
            {
                tslNotSelect_Click();
                return;
            }
            //查看详情
            if (e.ClickedItem.Name == "tslDetails")
            {
                //int Id = this.dgvCarStatisticsQC.SelectedRows[0].Cells["QCInfo_ID"].RowIndex;
                //iQcInfoID = Id;
                //if (iQcInfoID > 0)
                //{
                //    UpdateQCDetailsForm frm = new UpdateQCDetailsForm();
                //    frm.Owner = this;
                //    frm.iqcinfoid = iQcInfoID;
                //    frm.Show();
                //}
                //else
                //{
                //    MessageBox.Show("选中行的质检编号不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}


                if (this.dgvCarStatisticsQC.SelectedRows.Count > 0) // 判断是否选中
                {
                    if (dgvCarStatisticsQC.SelectedRows.Count > 1) // 判断收选中多行
                    {
                        MessageBox.Show("查看详细只能选中一行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        int Id =Convert.ToInt32(this.dgvCarStatisticsQC.SelectedRows[0].Cells["QCInfo_ID"].Value);
                        iQcInfoID = Id;
                        if (iQcInfoID > 0)
                        {
                            UpdateQCDetailsForm frm = new UpdateQCDetailsForm();
                            frm.Owner = this;
                            frm.iqcinfoid = iQcInfoID;
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("选中行的质检编号不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择要查看详细的行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            this.dgvCarStatisticsQC.DataSource = page.BindBoundControl<View_QCInfo>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr, orderbywhere);
        }

        /// <summary>
        /// 设置每页显示信息条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData();
        }

        #region  DataTimePicker控件的自定义显示  和 KeyPress事件
        /// <summary>
        /// DataTimePicker控件的文本操作方法
        /// </summary>
        private void DateTimeBeginISNull()
        {
            this.txtbeginTime.Format = DateTimePickerFormat.Custom;
            this.txtbeginTime.CustomFormat = " ";
        }
        private void DateTimeEndIsNull()
        {
            this.txtendTime.Format = DateTimePickerFormat.Custom;
            this.txtendTime.CustomFormat = " ";
        }
        /// <summary>
        /// 将“DataTiemPicker”控件赋值为null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbeginTime_ValueChanged(object sender, EventArgs e)
        {
            this.txtbeginTime.Format = DateTimePickerFormat.Custom;
            this.txtbeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
        private void txtendTime_ValueChanged(object sender, EventArgs e)
        {
            this.txtendTime.Format = DateTimePickerFormat.Custom;
            this.txtendTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
        /// <summary>
        /// “DataTiemPicker”控件的键盘事件 KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbeginTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                DateTimeBeginISNull();
            }
        }
        private void txtendTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            if (e.KeyChar == '\b')
            {
                DateTimeEndIsNull();
            }
        }
        #endregion      

        /// <summary>
        /// 导出文档 -- Ｗｏｒｄ的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslWord_Click(object sender, EventArgs e)
        {
            tslExport_Word();
        }
        /// <summary>
        /// 导出Word 的方法
        /// </summary>
        private void tslExport_Word()
        {
            SaveFileDialog sfile = new SaveFileDialog();
            sfile.AddExtension = true;
            sfile.DefaultExt = ".doc";
            sfile.Filter = "(*.doc)|*.doc";
            sfile.FileName = "理文检测系统退货还包统计Word报表" + DateTime.Now.ToShortDateString();
            if (sfile.ShowDialog() == DialogResult.OK)
            {
                object path = sfile.FileName;
                Object none = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document document = wordApp.Documents.Add(ref none, ref none, ref none, ref none);
                //建立表格
                Microsoft.Office.Interop.Word.Table table = document.Tables.Add(document.Paragraphs.Last.Range, dgvCarStatisticsQC.Rows.Count + 1, dgvCarStatisticsQC.Columns.Count, ref none, ref none);
                try
                {
                    for (int i = 0; i < dgvCarStatisticsQC.Columns.Count; i++)//设置标题
                    {
                        table.Cell(0, i + 1).Range.Text = dgvCarStatisticsQC.Columns[i].HeaderText;
                    }
                    for (int i = 1; i < dgvCarStatisticsQC.Rows.Count; i++)//填充数据
                    {
                        for (int j = 0; j < dgvCarStatisticsQC.Columns.Count; j++)
                        {
                            //table.Cell(i + 1, j + 1).Range.Text = dgvCarStatisticsQC[j, i - 1].Value.ToString();
                            if (dgvCarStatisticsQC[j, i - 1].Value != null)
                            {
                                table.Cell(i + 1, j + 1).Range.Text = dgvCarStatisticsQC[j, i - 1].Value.ToString();
                            }
                        }
                    }
                    document.SaveAs(ref path, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none);
                    document.Close(ref none, ref none, ref none);
                    MessageBox.Show("导出数据成功！");
                }

                finally
                {
                    wordApp.Quit(ref none, ref none, ref none);
                }
            }
        }

       

    }
}
