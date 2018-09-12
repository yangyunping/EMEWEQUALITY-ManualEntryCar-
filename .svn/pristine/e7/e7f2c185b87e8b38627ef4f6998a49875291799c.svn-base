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
    public partial class Beonduty_GuanLǐ : Form
    {
        public Beonduty_GuanLǐ()
        {
            InitializeComponent();
        }

        public MainFrom mf;//主窗体
        Expression<Func<View_Beonduty_D_Q, bool>> expr = null;
        PageControl page = new PageControl();
        private int beonduty_ID;//值班编号
        private bool rbool;//添加按钮权限
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {
            this.lvwUserList.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            LoadData();
            btnUpdate.Visible = false;
            btnCancle.Visible = false;
            mf = new MainFrom();
            QCGroup_NameBing();
            Beonduty_Dictionary_NameBing();
            if(btnAdd.Visible)
            {
                rbool = true;
            }
        }
        private void Beonduty_Dictionary_NameBing()
        {
            cob_Beonduty_Dictionary_Name.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
            this.cob_Beonduty_Dictionary_Name.DisplayMember = "Dictionary_Name";
            cob_Beonduty_Dictionary_Name.ValueMember = "Dictionary_ID";
            if (cob_Beonduty_Dictionary_Name.DataSource != null)
            {
                cob_Beonduty_Dictionary_Name.SelectedIndex = -1;
            }
        }


        /// <summary>
        /// 绑定小组
        /// </summary>
        private void QCGroup_NameBing()
        {
            cob_QCGroup_Name.DataSource = QCGroupDAL.Query();
            cob_QCGroup_Name.DisplayMember ="QCGroup_Name";
            cob_QCGroup_Name.ValueMember = "QCGroup_ID"; 
            if (cob_QCGroup_Name.DataSource != null)
            {
                cob_QCGroup_Name.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {

                this.lvwUserList.DataSource = null;
                lvwUserList.DataSource = page.BindBoundControl<View_Beonduty_D_Q>("", txtCurrentPage2, lblPageCount2, expr, "Beonduty_ID desc");
                ShowAddButton();

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("用户管理 LoadData()" + ex.Message.ToString());
            }

        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void ShowAddButton()
        {
            if (rbool)
            {
                btnAdd.Visible = true;
                
            }
            btnUpdate.Visible = false;
            btnCancle.Visible = false;
            //数据清空
            cob_Beonduty_Dictionary_Name.Text = "";
            cob_QCGroup_Name.Text = "";
            txtRemark.Text = "";
        }

        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Name == "tsbtnUpdate")//修改
            {
                tsbtnUpdate_Click();
                return;
            }
            if (e.ClickedItem.Name == "tsbtnDel")//删除
            {
                tsbtnDel_Click();
                return;
            }
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
            lvwUserList.DataSource = page.BindBoundControl<View_Beonduty_D_Q>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr, "Beonduty_ID desc");

        }



        private void Beonduty_Load(object sender, EventArgs e)
        {
            InitUser();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Enabled)
            {
                btnAdd.Enabled = false;
                btnAdd_Click();
               
                btnAdd.Enabled = true;

            }
        }
        private void btnAdd_Click()
        {
            try
            {
                string beginTime = dtp_beginTime.Text.Trim();
                string endTime = dtp_endTime.Text.Trim();
                string begin = beginTime + " 00:00:00";
                string end = endTime + "23:59:59 ";
                if (!GetBool()) return;
                var addBeonduty = new Beonduty
                {
                    Beonduty_BeginTime = Common.GetDateTime(begin),//开始时间
                    Beonduty_EndTime = Common.GetDateTime(end),//结束时间
                    Beonduty_Dictionary_ID = Common.GetInt(cob_Beonduty_Dictionary_Name.SelectedValue),//状态
                    Beonduty_Remake = txtRemark.Text.Trim(),//备注
                    QCGroup_ID = Common.GetInt(cob_QCGroup_Name.SelectedValue)//小组

                };
                if (BeondutyDAL.InsertOneQCRecord(addBeonduty))
                {
                    string Log_Content = String.Format("值班开始时间：{0}  值班结束时间：{1}", begin, end);
                    Common.WriteLogData("新增", "新增值班信息" + Log_Content, Common.NAME); //操作日志
                    MessageBox.Show("添加成功", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("Beonduty_GuanLǐ.btnAdd_Click()" + ex.Message.ToString());
            }
            finally
            {
                //更新数据
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();//更新数据
            }
        }
        /// <summary>
        /// 判断
        /// </summary>
        /// <returns></returns>
        private bool GetBool()
        {
            bool rbool = true;
            if (string.IsNullOrEmpty(cob_Beonduty_Dictionary_Name.SelectedItem.ToString()))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "状态不能为空", cob_Beonduty_Dictionary_Name, this);
                rbool = false;
            }
            if (string.IsNullOrEmpty(cob_QCGroup_Name.SelectedItem.ToString()))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "小组不能为空", cob_QCGroup_Name, this);
                rbool = false;
            }
            if (string.IsNullOrEmpty(dtp_beginTime.Text))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "开始时间不能为空", dtp_beginTime, this);
                rbool = false;
            }
            if (string.IsNullOrEmpty(dtp_endTime.Text))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "结束不能为空", dtp_endTime, this);
                rbool = false;
            }
            if (Common.GetDateTime(dtp_beginTime.Text) >Common.GetDateTime(dtp_endTime.Text))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "开始时间不能大于结束时间", dtp_endTime, this);
                rbool = false;
            }
            return rbool;
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void tsbtnDel_Click()
        {
            try
            {
                string Log_Content = "";
                int j = 0;
                if (lvwUserList.SelectedRows.Count > 0)
                {
                    //选中数量
                    int count = lvwUserList.SelectedRows.Count;
                    //string id = "";
                    //遍历
                    for (int i = 0; i < count; i++)
                    {
                        if (Common.GetInt(lvwUserList.SelectedRows[i].Cells["BeondutyID"].Value) > 0)
                        {
                            if (lvwUserList.SelectedRows[i].Cells["Dictionary_Name"].Value != null)
                            {
                                if (lvwUserList.SelectedRows[i].Cells["Dictionary_Name"].Value.ToString() != "启动")
                                {
                                    if (lvwUserList.SelectedRows[i].Cells["Beonduty_BeginTime"].Value != null)
                                    {
                                        Log_Content += string.Format("开始时间", lvwUserList.SelectedRows[i].Cells["Beonduty_BeginTime"].Value.ToString());
                                    }
                                    if (lvwUserList.SelectedRows[i].Cells["Beonduty_EndTime"].Value != null)
                                    {
                                        Log_Content += string.Format("结束时间", lvwUserList.SelectedRows[i].Cells["Beonduty_EndTime"].Value.ToString());
                                    }

                                    Expression<Func<Beonduty, bool>> funbeonduty = n => n.Beonduty_ID.ToString() == lvwUserList.SelectedRows[i].Cells["BeondutyID"].Value.ToString();
                                    if (!BeondutyDAL.DeleteToMany(funbeonduty))
                                    {
                                        j++;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("该值班表正在使用不能删除", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }

                    }

                    Common.WriteLogData("删除", "删除值班信息" + Log_Content, Common.NAME); //操作日志
                    if (j == 0)
                    {
                        MessageBox.Show("成功删除", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("删除失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("请选中要删除的信息", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("Beonduty_GuanLǐ.tsbtnDel_Click()" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();

                LoadData();//更新数据
            }
           
        }
        /// <summary>
        /// 查看修改信息
        /// </summary>
        private void tsbtnUpdate_Click()
        {
            try
            {
                if (lvwUserList.SelectedRows.Count > 0)//选中行
                {
                    if (lvwUserList.SelectedRows.Count > 1)
                    {
                        MessageBox.Show("修改只能选中一行！", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    else
                    {
                        btnAdd.Visible = false;
                        btnUpdate.Visible =true ;
                        btnCancle.Visible = true;
                        if (lvwUserList.SelectedRows[0].Cells["BeondutyID"].Value != null)
                        {
                            beonduty_ID = Common.GetInt(lvwUserList.SelectedRows[0].Cells["BeondutyID"].Value.ToString());
                            Expression<Func<View_Beonduty_D_Q, bool>> funviewBeonduty = n => n.Beonduty_ID == beonduty_ID;
                            foreach (var m in BeondutyDAL.Query(funviewBeonduty))
                            {
                                if (!string.IsNullOrEmpty(m.Dictionary_Name))//状态
                                {
                                    cob_Beonduty_Dictionary_Name.Text = m.Dictionary_Name;
                                }
                                if (!string.IsNullOrEmpty(m.QCGroup_Name))//小组
                                {
                                    cob_QCGroup_Name.Text = m.QCGroup_Name;
                                }
                                if (!string.IsNullOrEmpty(m.Beonduty_BeginTime.ToString()))//开始时间
                                {
                                    dtp_beginTime.Text = m.Beonduty_BeginTime.ToString();
                                }
                                if (!string.IsNullOrEmpty(m.Beonduty_EndTime.ToString()))//结束时间
                                {
                                    dtp_endTime.Text = m.Beonduty_EndTime.ToString();
                                }
                                if (!string.IsNullOrEmpty(m.Dictionary_Remark))//备注
                                {
                                    txtRemark.Text = m.Dictionary_Remark;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选中修改行！", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beonduty_GuanLǐ.tsbtnUpdate_Click()" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        private void tslNotSelect_Click()
        {
            for (int i = 0; i < lvwUserList.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)dgvRoleList.Rows[i].Cells[0]).Value = false;
                lvwUserList.Rows[i].Selected = false;
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        private void tslSelectAll_Click()
        {

            for (int i = 0; i < lvwUserList.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)lvwUserList.Rows[i].Cells[0]).Value = true;
                lvwUserList.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(btnUpdate.Enabled)
            {
                btnUpdate.Enabled = false;
                btnUpdate_Click();
               
                btnUpdate.Enabled = true;
            }
        }
        private void btnUpdate_Click()
        {
            try
            {
                string beginTime = dtp_beginTime.Text.Trim();
                string endTime = dtp_endTime.Text.Trim();
                string begin = beginTime + " 00:00:00";
                string end = endTime + "23:59:59 ";
                if (!GetBool()) return;
                Expression<Func<Beonduty, bool>> funbeondurt = m => m.Beonduty_ID == beonduty_ID;
                Action<Beonduty> action = n =>
                {
                    n.Beonduty_Remake = txtRemark.Text.Trim();//备注

                    n.Beonduty_BeginTime = Common.GetDateTime(begin);//开始时间

                    n.Beonduty_EndTime = Common.GetDateTime(end);//结束时间
                    n.Beonduty_Dictionary_ID = Common.GetInt(cob_Beonduty_Dictionary_Name.SelectedValue);//状态
                    n.QCGroup_ID = Common.GetInt(cob_QCGroup_Name.SelectedValue);//小组
                };
                if (BeondutyDAL.Update(funbeondurt, action))
                {
                    string Log_Content = String.Format("值班开始时间：{0}  值班结束时间：{1}", begin, end);
                    Common.WriteLogData("修改", "值班信息修改" + Log_Content, Common.NAME); //操作日志
                    MessageBox.Show("修改成功", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("修改失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("Beonduty_GuanLǐ.btnUpdate_Click()" + ex.Message.ToString());
            }
            finally
            {
                //page = new PageControl();//实例化分页类
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
               
                LoadData();//更新数据
            }
            
        }
        /// <summary>
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            ShowAddButton();
        }

        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData();
        }
       


    }
}
