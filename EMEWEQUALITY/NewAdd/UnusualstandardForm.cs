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
    public partial class UnusualstandardForm : Form
    {
        public UnusualstandardForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 返回一个空的检测记录
        /// </summary>
        Expression<Func<Unusualstandard, bool>> expr = null;
        public MainFrom mf;//主窗体
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();

        string sqlWhere = " where 1=1 ";

        private void Unusualstandard_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        private void LoadData()
        {
            BindCBX();
            sqlWhere = " where 1=1 ";
            if (cbxSelPROD.Text.Trim() != "")
            {
                sqlWhere += " and Unusualstandard_PROD like '%" + cbxSelPROD.Text.Trim() + "%'";
            }
            int numh = int.Parse(tscbxPageSize2.Text);
            int ye = int.Parse(txtCurrentPage2.Text);
            DGVUnusual.DataSource = LinQBaseDao.Query("select top(" + numh + ")* from Unusualstandard " + sqlWhere + "  and  Unusualstandard_ID not  in(select top(" + (numh * (ye - 1)) + " )Unusualstandard_ID from Unusualstandard " + sqlWhere + ") ").Tables[0];
            counts = (numh + int.Parse(LinQBaseDao.GetSingle("select count(*) from Unusualstandard " + sqlWhere).ToString())) / numh;
        }
        int counts = 0;
        /// <summary>
        /// 绑定combox
        /// </summary>
        private void BindCBX()
        {
            DataTable dt = LinQBaseDao.Query("select Unusualstandard_PROD from Unusualstandard").Tables[0];
            DataRow dr = dt.NewRow();
            dr["Unusualstandard_PROD"] = "";
            dt.Rows.InsertAt(dr, 0);
            cbxSelPROD.DataSource = dt;
            cbxSelPROD.DisplayMember = "Unusualstandard_PROD";
            cbxSelPROD.ValueMember = "Unusualstandard_PROD";

        }

        /// <summary>
        /// 全选
        /// </summary>
        private void tsbSelectAll_Click()
        {
            for (int i = 0; i < DGVUnusual.Rows.Count; i++)
            {
                DGVUnusual.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        private void tsbNotSelectAll_Click()
        {
            for (int i = 0; i < this.DGVUnusual.Rows.Count; i++)
            {
                DGVUnusual.Rows[i].Selected = false;
            }
        }
        /// <summary>
        /// 删除 
        /// </summary>
        private void tsbDelete_Click()
        {
            try
            {
                int j = 0;
                if (this.DGVUnusual.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //选中数量
                        int count = DGVUnusual.SelectedRows.Count;
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            Expression<Func<Unusualstandard, bool>> funUnusual = n => n.Unusualstandard_ID == Convert.ToInt32(DGVUnusual.SelectedRows[i].Cells["Unusualstandard_ID"].Value.ToString());

                            if (!UnusualstandardDAL.DeleteToMany(funUnusual))
                            {
                                j++;
                                LogInfoDAL.loginfoadd("删除", "删除检测项目异常标准：" + DGVUnusual.SelectedRows[i].Cells["Unusualstandard_PROD"].Value.ToString(), Common.USERNAME);//添加日志

                            }
                        }
                        if (j == 0)
                        {
                            MessageBox.Show("成功删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要删除的行！");
                }
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("检测项目异常标准 tsbDelete_Click()+" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                //LoadData(Name);//更新
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();

            }
        }
        /// <summary>
        /// 分 页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsbDelete")//删除
            {
                tsbDelete_Click();
                return;
            }
            if (e.ClickedItem.Name == "tsbSelectAll")//全选
            {
                tsbSelectAll_Click();
                return;
            }
            if (e.ClickedItem.Name == "tsbNotSelectAll")//取消全选
            {
                tsbNotSelectAll_Click();
                return;
            }


            LoadData();
        }

        /// <summary>
        /// 设置每页显示条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData();
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Unusualstandard u = new Unusualstandard();


                if (txtPROD.Text.Trim() != "")
                {
                    u.Unusualstandard_PROD = txtPROD.Text.Trim();
                }
                else
                {
                    MessageBox.Show("货品不能为空！");
                    return;
                }
                if (txtDEGRADE_MOISTURE_PERCT.Text.Trim() != "")
                {
                    u.Unusualstandard_DEGRADE_MOISTURE_PERCT = txtDEGRADE_MOISTURE_PERCT.Text.Trim();

                }
                else
                {
                    MessageBox.Show("水分标准不能为空！");
                    return;
                }


                if (txtDEGRADE_MATERIAL_PERCT.Text.Trim() != "")
                {
                    u.Unusualstandard_DEGRADE_MATERIAL_PERCT = txtDEGRADE_MATERIAL_PERCT.Text.Trim();
                }
                else
                {
                    MessageBox.Show("杂质标准不能为空！");
                    return;
                }
                if (txtDEGRADE_OUTTHROWS_PERCT.Text.Trim() != "")
                {
                    u.DEGRADE_OUTTHROWS_PERCT = txtDEGRADE_OUTTHROWS_PERCT.Text.Trim();
                }
                else
                {
                    MessageBox.Show("杂纸标准不能为空！");
                    return;
                }

                if (txtMOISTUREMIN.Text != "")
                {
                    u.Unusualstandard_MOISTUREMIN = Convert.ToInt32(txtMOISTUREMIN.Text);
                }
                else
                {
                    MessageBox.Show("水分接收下限不能为空！");
                    return;
                }

                if (txtMOISTUREMAX.Text != "")
                {
                    u.Unusualstandard_MOISTUREMAX = Convert.ToInt32(txtMOISTUREMAX.Text);
                }
                else
                {
                    MessageBox.Show("水分接收上限不能为空！");
                    return;
                }


                DataTable dt = LinQBaseDao.Query("select * from dbo.Unusualstandard where Unusualstandard_PROD ='" + txtPROD.Text.Trim() + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("货品重名！");
                    return;
                }
                else
                {

                    int result = 0;
                    UnusualstandardDAL.InsertOneQCRecord(u, out result);
                    if (result > 0)
                    {
                        MessageBox.Show("添加成功！");
                        LoadData();
                    }
                }

            }
            catch
            {


            }

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (DGVUnusual.SelectedRows.Count != 1 && DGVUnusual.DataSource != null)
            {
                MessageBox.Show("请选择一条数据！");
                return;
            }
            Unusualstandard u = new Unusualstandard();


            if (txtPROD.Text.Trim() == "")
            {

                MessageBox.Show("货品不能为空！");
                return;
            }
            if (txtDEGRADE_MOISTURE_PERCT.Text.Trim() == "")
            {

                MessageBox.Show("水分标准不能为空！");
                return;
            }


            if (txtDEGRADE_MATERIAL_PERCT.Text.Trim() == "")
            {

                MessageBox.Show("杂质标准不能为空！");
                return;
            }
            if (txtDEGRADE_OUTTHROWS_PERCT.Text.Trim() == "")
            {
                MessageBox.Show("杂纸标准不能为空！");
                return;
            }
            if (txtMOISTUREMIN.Text != "")
            {
                u.Unusualstandard_MOISTUREMIN = Convert.ToInt32(txtMOISTUREMIN.Text);
            }
            else
            {
                MessageBox.Show("水分接收下限不能为空！");
                return;
            }

            if (txtMOISTUREMAX.Text != "")
            {
                u.Unusualstandard_MOISTUREMAX = Convert.ToInt32(txtMOISTUREMAX.Text);
            }
            else
            {
                MessageBox.Show("水分接收上限不能为空！");
                return;
            }

            int id = Convert.ToInt32(DGVUnusual.SelectedRows[0].Cells["Unusualstandard_ID"].Value);

            Expression<Func<Unusualstandard, bool>> fnUnu = n => n.Unusualstandard_ID == id;

            Action<Unusualstandard> aUnu = a =>
            {
                a.Unusualstandard_PROD = txtPROD.Text.Trim();
                a.Unusualstandard_DEGRADE_MOISTURE_PERCT = txtDEGRADE_MOISTURE_PERCT.Text.Trim();
                a.Unusualstandard_DEGRADE_MATERIAL_PERCT = txtDEGRADE_MATERIAL_PERCT.Text.Trim();
                a.DEGRADE_OUTTHROWS_PERCT = txtDEGRADE_OUTTHROWS_PERCT.Text.Trim();
                a.Unusualstandard_MOISTUREMIN = Convert.ToInt32(txtMOISTUREMIN.Text);
                a.Unusualstandard_MOISTUREMAX = Convert.ToInt32(txtMOISTUREMAX.Text);
            };
            bool b = UnusualstandardDAL.Update(fnUnu, aUnu);
            if (b)
            {
                MessageBox.Show("修改成功！");
                LoadData();
            }
        }

        private void btnSel_Click(object sender, EventArgs e)
        {

            LoadData();
        }

        /// <summary>
        /// 单击DGVUnusual单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVUnusual_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVUnusual.SelectedRows.Count > 0 && DGVUnusual.DataSource != null)
            {
                txtPROD.Text = DGVUnusual.SelectedRows[0].Cells["Unusualstandard_PROD"].Value.ToString();
                txtDEGRADE_MATERIAL_PERCT.Text = DGVUnusual.SelectedRows[0].Cells["Unusualstandard_DEGRADE_MATERIAL_PERCT"].Value.ToString();
                txtDEGRADE_MOISTURE_PERCT.Text = DGVUnusual.SelectedRows[0].Cells["Unusualstandard_DEGRADE_MOISTURE_PERCT"].Value.ToString();
                txtDEGRADE_OUTTHROWS_PERCT.Text = DGVUnusual.SelectedRows[0].Cells["DEGRADE_OUTTHROWS_PERCT"].Value.ToString();
                txtMOISTUREMIN.Text = DGVUnusual.SelectedRows[0].Cells["Unusualstandard_MOISTUREMIN"].Value.ToString();
                txtMOISTUREMAX.Text = DGVUnusual.SelectedRows[0].Cells["Unusualstandard_MOISTUREMAX"].Value.ToString();
            }

        }

        private void tslPreviousPage2_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtCurrentPage2.Text) > 1)
            {
                txtCurrentPage2.Text = (int.Parse(txtCurrentPage2.Text) - 1).ToString();
                LoadData();
            }
            else
                MessageBox.Show("已经是首页");
        }

        private void tslNextPage1_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtCurrentPage2.Text) < counts)
            {
                txtCurrentPage2.Text = (int.Parse(txtCurrentPage2.Text) + 1).ToString();
                LoadData();
            }
            else
                MessageBox.Show("已经是尾页");
        }

        private void tslHomPage1_Click(object sender, EventArgs e)
        {
            txtCurrentPage2.Text = "1";
            LoadData();
        }

        private void tslLastPage1_Click(object sender, EventArgs e)
        {
            txtCurrentPage2.Text = counts.ToString();
            LoadData();
        }


    }
}
