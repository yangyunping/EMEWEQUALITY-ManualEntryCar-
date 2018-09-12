using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEDAL;
using EMEWEEntity;
using EMEWEQUALITY.HelpClass;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;
namespace EMEWEQUALITY.NewAdd
{
    public partial class SMSSendSetForm : Form
    {
        public SMSSendSetForm()
        {
            InitializeComponent();
        }
        public MainFrom mf;//主窗体
        Expression<Func<SMSConfigure, bool>> expr = null;
 
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData(string strName)
        {
            try
            {

                if (txtSMSConfigure_ReceivePhone.Text != "")
                {
                   
                    expr = expr.And(n => SqlMethods.Like(n.SMSConfigure_ReceivePhone, "%" + txtSMSConfigure_ReceivePhone.Text.Trim() + "%"));
                }

                if (txtSMSConfigure_Receive.Text != "")
                {
                    expr = expr.And(n => SqlMethods.Like(n.SMSConfigure_Receive, "%" + txtSMSConfigure_Receive.Text.Trim() + "%"));

                }

                this.dgvSMSSend.DataSource = null;
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                dgvSMSSend.DataSource = page.BindBoundControl<SMSConfigure>(strName, txtCurrentPage2, lblPageCount2, expr);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("用户管理 LoadData()" + ex.Message.ToString());
            }

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
            }
            else
            {
                cboUnusualType_Name.DataSource = null;
            }

        }
        /// <summary>
        /// 分页控件
        /// </summary>
        PageControl page = new PageControl();

        /// </summary>
        /// 排序字段及方式[以空格格开]
        /// </summary>
        private string orderbywhere = "SMSConfigure_ID desc";
       
        string Cntr_No = "";
        /// <summary>
        /// 单击保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //获取选中人的姓名和电话
            string name = "";
            string Phon = "";
            foreach (TreeNode tnTemp in tv_ReceivePhone.Nodes)
            {
                if (tnTemp.Checked == true)//选中的项
                {
                    name += tnTemp.Text + ";";
                    Phon += tnTemp.Tag + ";";
                }
            }
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请选择人员号码！");
                return;
            }
            //短信内容
            if (cboUnusualType_Name.SelectedIndex < 0)
            {
                MessageBox.Show("请选择一项发送异常类型");
                return;
            }
            string field = "";
            string fieldTxt = "";
            //取选中电话号码进行租和
            //取选择的短信内容值 控件chb_SendContent
            foreach (var item in chb_SendContent.CheckedItems)
            {

                //if (item.ToString() == "拆包前平均水分")
                //{
                //    if (fieldTxt != "")
                //    {
                //        field += ",QCInfo_UnpackBefore_MOIST_PER_SAMPLE";
                //        fieldTxt += ",拆包前平均水分";
                //    }
                //    else
                //    {
                //        field += "QCInfo_UnpackBefore_MOIST_PER_SAMPLE";
                //        fieldTxt += "拆包前平均水分";
                //    }
                //}
                if (item.ToString() == "车牌号")
                {
                    if (fieldTxt != "")
                    {
                        field += ",CNTR_NO";
                        fieldTxt += ",车牌号";
                    }
                    else
                    {
                        field += "CNTR_NO";
                        fieldTxt += "车牌号";
                    }
                    
                }
                //if (item.ToString() == "拆包后平均水分")
                //{
                //    if (fieldTxt != "")
                //    {
                //        field += ",QCInfo_UnpackBack_MOIST_PER_SAMPLE";
                //        fieldTxt += ",拆包后平均水分";
                //    }
                //    else
                //    {
                //        field += "QCInfo_UnpackBack_MOIST_PER_SAMPLE";
                //        fieldTxt += "拆包后平均水分";
                //    }
                //}
                if (item.ToString() == "货品")
                {
                    if (fieldTxt != "")
                    {
                        field += ",PROD_ID";
                        fieldTxt += ",货品";
                    }
                    else
                    {
                        field += "PROD_ID";
                        fieldTxt += "货品";
                    }
                }
                if (item.ToString() == "参号")
                {
                    if (fieldTxt != "")
                    {
                        field += ",REF_NO";
                        fieldTxt += ",参号";
                    }
                    else
                    {
                        fieldTxt += "参号";
                    }
                }

                //若继续加短信内容，在此处再写

            }
            //将字段组合保存到短信内容内
            SMSConfigure SMSfIG = new SMSConfigure();
            SMSfIG.SMSConfigure_Receive = name;
            SMSfIG.SMSConfigure_ReceivePhone = Phon;
            SMSfIG.SMSConfigure_SendContent = field;
            SMSfIG.SMSConfigure_UnusualType_Id = null;
            SMSfIG.SMSConfigure_TestItems_ID = Convert.ToInt32(cboUnusualType_Name.SelectedValue);
            SMSfIG.SMSConfigure_Remark = txtRemark.Text.Trim();
            SMSfIG.SMSConfigure_SendContentText = fieldTxt;
            if (SMSConfigureDAL.InsertOneQCRecord(SMSfIG))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }


            LoadData("");
        }
        /// <summary>
        /// 单击清空按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCler_Click(object sender, EventArgs e)
        {
            Empty();
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Empty()
        {
            indexs = 0;
            chb_SendContent.Items.Clear();
            cboUnusualType_Name.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SMSSendSetForm_Load_1(object sender, EventArgs e)
        {
            LoadData("");
            UnusualType();
            SMSPhone();
            this.dgvSMSSend.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
        }

        TreeNode nodeTemps;
        /// <summary>
        /// 绑定电话号码
        /// </summary>
        private void SMSPhone()
        {
            //只绑定电话号码不为空的用户
            DataSet ds = LinQBaseDao.Query("select UserId,UserName,UserPhone from UserInfo where UserPhone!=''");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                nodeTemps = new TreeNode();
                nodeTemps.Text = ds.Tables[0].Rows[i]["UserName"].ToString();
                nodeTemps.Tag = ds.Tables[0].Rows[i]["UserPhone"].ToString();
                nodeTemps.Name = ds.Tables[0].Rows[i]["UserId"].ToString();
                tv_ReceivePhone.Nodes.Add(nodeTemps);
            }

        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click()
        {
            for (int i = 0; i < dgvSMSSend.Rows.Count; i++)
            {
                dgvSMSSend.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click()
        {
            for (int i = 0; i < this.dgvSMSSend.Rows.Count; i++)
            {
                dgvSMSSend.Rows[i].Selected = false;
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
        //分页菜单响应事件
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
            LoadData(e.ClickedItem.Name);
        }

        private void tscbxPageSize2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData("");
        }

        int indexs = 0;
        private void chb_SendContent_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //判断内容选项是否超过四项
            if (chb_SendContent.CheckedItems.Count > 3)
            {
                if (indexs == 0)
                {
                    MessageBox.Show("超过了文字，添加选项慎重，建议选项为四项，若超出选项短信为两条发送");
                    indexs = 1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData("");
        }

    }
}
