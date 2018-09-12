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
using EMEWEDAL;
using System.Linq.Expressions;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class FormWeightSet : Form
    {

        public MainFrom mf;
        PageControl page = new PageControl();
        Expression<Func<View_WeightSet, bool>> expr = null;
        int currentId = 0;//当前操作ID
        DCQUALITYDataContext dc = null;
        public FormWeightSet()
        {
            InitializeComponent();
        }


        private void FormWeightSet_Load(object sender, EventArgs e)
        {

            bindCombox();
            cbxWeightNum.SelectedIndex = 0;
            cbxWeightNum2.SelectedIndex = 0;
            cbxTestItem.SelectedIndex = 0;
            cbxTestItem2.SelectedIndex = 0;
            LoadData();
        }

        private void bindCombox()
        {

           
            cbxTestItem.DataSource = LinQBaseDao.Query("select TestItems_ID,TestItems_NAME from TestItems where Tes_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='重量检测')").Tables[0]; ;
            cbxTestItem.DisplayMember = "TestItems_NAME";
            cbxTestItem.ValueMember = "TestItems_ID";

            DataTable dt = LinQBaseDao.Query("select TestItems_ID,TestItems_NAME from TestItems where Tes_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='重量检测')").Tables[0];

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "";
            dt.Rows.InsertAt(dr, 0);
            cbxTestItem2.DataSource = dt;
            cbxTestItem2.DisplayMember = "TestItems_NAME";
            cbxTestItem2.ValueMember = "TestItems_ID";

        }
        private void LoadData()
        {
            if (expr == null)
            {
                expr = (Expression<Func<View_WeightSet, bool>>)PredicateExtensionses.True<View_WeightSet>();
            }

            if (cbxTestItem2.Text != "")
            {
                expr = expr.And(n => n.TestItems_ID == Convert.ToInt32(cbxTestItem2.SelectedValue)+1);
            }

            if (cbxWeightNum2.Text != "")
            {
                expr = expr.And(n => n.WeightSet_weightNum == (cbxWeightNum2.SelectedIndex ));
            }
            dgvView_WaterSet.AutoGenerateColumns = false;
            dgvView_WaterSet.DataSource = page.BindBoundControl<View_WeightSet>("", txtCurrentPage1, lblPageCount1, expr, "WeightSet_id");
        }

        /// <summary>
        /// 验证该坚持项目是已经否配置
        /// </summary>
        /// <param name="TestItemsID"></param>
        /// <returns></returns>
        private bool isHaveWeightSet(int TestItemsID)
        {
            DataTable dt = LinQBaseDao.Query("select TestItems_NAME from View_WeightSet where TestItems_ID=" + TestItemsID).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                WeightSet ws = new WeightSet();
                dc = new DCQUALITYDataContext();
                if (btnAdd.Text == "添加")
                {
                    if (isHaveWeightSet(Convert.ToInt32(cbxTestItem.SelectedValue)))
                    {
                        ws.WeightSet_TestItems_ID = Convert.ToInt32(cbxTestItem.SelectedValue);
                        ws.WeightSet_weightNum = cbxWeightNum.SelectedIndex + 1;
                        dc = new DCQUALITYDataContext();
                        dc.WeightSet.InsertOnSubmit(ws);
                        dc.SubmitChanges();
                        MessageBox.Show("操作成功！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("该检测项目已经配置，请修改或删除后再添加！");
                    }
                }
                else if (btnAdd.Text == "修改" && currentId > 0)
                {
                    ws = dc.WeightSet.Where(n => n.WeightSet_id == currentId).First();
                    ws.WeightSet_TestItems_ID = Convert.ToInt32(cbxTestItem.SelectedValue);
                    ws.WeightSet_weightNum = cbxWeightNum.SelectedIndex + 1;
                    dc.SubmitChanges();
                    MessageBox.Show("操作成功！", "提示");

                }
                btnAdd.Text = "添加";
                cbxTestItem.Enabled = true;

                LoadData();

            }
            catch (Exception ex)
            {

                MessageBox.Show("操作失败！", "提示");
                return;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && currentId > 0)
                {
                    dc = new DCQUALITYDataContext();
                    dc.WeightSet.DeleteOnSubmit(dc.WeightSet.Where(n => n.WeightSet_id == currentId).First());
                    dc.SubmitChanges();
                    MessageBox.Show("操作成功！", "提示");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！", "提示");
                return;
            }
        }

        private void dgvView_WaterTestConfigure_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentId = Convert.ToInt32(dgvView_WaterSet.SelectedRows[0].Cells[0].Value);
        }

        private void tsbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dc = new DCQUALITYDataContext();
                WeightSet ws = dc.WeightSet.Where(n => n.WeightSet_id == currentId).First();
                cbxTestItem.SelectedValue = ws.WeightSet_TestItems_ID;
                cbxWeightNum.SelectedIndex = Convert.ToInt32(ws.WeightSet_weightNum) - 1;
                btnAdd.Text = "修改";
                cbxTestItem.Enabled = false;
                LoadData();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvView_WaterSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentId = Convert.ToInt32(dgvView_WaterSet.SelectedRows[0].Cells[0].Value);
        }
    }
}
