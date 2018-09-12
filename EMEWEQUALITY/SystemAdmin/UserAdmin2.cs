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
using System.Data.Linq;
using EMEWEQUALITY.HelpClass;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class UserAdmin2 : Form
    {
        private string userCreateName;//当前登录人
        public UserAdmin2(string userCreateName)
        {
            this.userCreateName = userCreateName;
            InitializeComponent();
        }
        public MainFrom mf;//主窗体
        Expression<Func<View_UserInfo2_D_R_d, bool>> expr = null;
        PageControl page = new PageControl();
        private bool rbool;//是否有添加权限
       /// <summary>
       /// 修改编号
       /// </summary>
        string strUserID = "";
        private void UserAdmin_Load(object sender, EventArgs e)
        {
            InitUser();
          
        }
      
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {
            expr = PredicateExtensionses.True<View_UserInfo2_D_R_d>();
          //  expr = expr.And(n => n.UserLoginId != "emewe");
            this.lvwUserList.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            LoadData();
            bingcob_Duty_Name();
            bingcob_QCGroup_Name(cbxState);
          //  bingcob_QCGroup_Name(cmbSearchRole);
            btnUpUser.Visible = false;
            btnUserCancle.Visible = false;
            mf = new MainFrom();
            if(btnAdd.Visible)
            {
                rbool = true;
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
                lvwUserList.DataSource = page.BindBoundControl<View_UserInfo2_D_R_d>("", txtCurrentPage2, lblPageCount2, expr);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("用户管理 LoadData()"+ex.Message.ToString());
            }

        }

        //绑定职务
        private void bingcob_Duty_Name()
        {
            cobDuty_Name.DataSource = LinQBaseDao.Query<DutyInfo>(new DCQUALITYDataContext());
            cobDuty_Name.DisplayMember = "Duty_Name";
            cobDuty_Name.ValueMember = "Duty_ID";

            cobDuty_Name2.DataSource = LinQBaseDao.Query<DutyInfo>(new DCQUALITYDataContext());
            cobDuty_Name2.DisplayMember = "Duty_Name";
            cobDuty_Name2.ValueMember = "Duty_ID";
            

            
        }
        //绑定状态
        private void bingcob_QCGroup_Name(ComboBox cobName)
        {

            cbxState.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
           cbxState.DisplayMember = "Dictionary_Name";
            cbxState.ValueMember = "Dictionary_ID";
            cobName.Text = "启动";
           // "启动";
            //cobName.DataSource = LinQBaseDao.Query<RoleInfo>(new DCQUALITYDataContext());
            //cobName.DisplayMember = "Role_Name";
            //cobName.ValueMember = "Role_Id";
            //cobName.Text = "";
        }

        //取消
        private void btnUserCancle_Click(object sender, EventArgs e)
        {
            ShowAddButton();
        }

        private void ShowAddButton()
        {
            if (rbool)
            {
                btnAdd.Visible = true;
            }
            btnUpUser.Visible = false;
            btnUserCancle.Visible = false;
            //btnAdd.Visible = true;
            //btnUpUser.Visible = false;
            //btnUserCancle.Visible = false;
           // txtLoginId.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtRemark.Text = "";
           // txtLoginId.ReadOnly = false;
            txtCardNo.Text = "";
            cbxState.Text = "启动";
            //chkGetWeight.Checked = false;
        }

        //添加
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
                int j = 0;
                int rint = 0;
                if (!btnCheck()) return;
                var useradd = new UserInfo2
                {
                    UserName = txtName.Text.Trim(),
                    UserPhone = txtPhone.Text.Trim(),
                    UserAddress = txtAddress.Text.Trim(),
                    UserCarId = txtCardNo.Text.Trim(),
                    UserCreateTime = DateTime.Now,
                    UserCreateName = userCreateName,                      //创建人
                    User_Duty_ID = Convert.ToInt32(cobDuty_Name.SelectedValue.ToString()),
               
                    //User_Dictionary_ID = 2,                                    //用户状态
                    User_Dictionary_ID= Convert.ToInt32(cbxState.SelectedValue.ToString()),//DictionaryDAL.GetDictionaryID("启动"),
                    UserRemark = txtRemark.Text.Trim()
                };
               
                if (UserInfo2DAL.InsertOneQCRecord(useradd, out rint))
                {
                    MessageBox.Show("添加成功", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加失败", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                string Log_Content = String.Format("用户名称：{0} ", txtName.Text.Trim());
                Common.WriteLogData("新增", Log_Content, Common.USERNAME);//添加日志

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("用户管理 btnAdd_Click()" + ex.Message.ToString());
                MessageBox.Show("添加失败", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }
        #region //查重方法
        /// <summary>
        /// 查重方法
        /// </summary>
        /// <returns></returns>
        private bool btnCheck()
        {

            bool rbool = true;
            try
            {

                var LoginId = txtName.Text.Trim();
                if (LoginId == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "真实姓名不能为空！", txtName, this);
                    txtName.Text = "";
                    txtName.Focus();
                    rbool = false;
                }
                Expression<Func<UserInfo2, bool>> funview_userinfo = n => n.UserName == LoginId;
                if (UserInfo2DAL.Query(funview_userinfo).Count()>0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "该真实姓名已存在", txtName, this);
                    txtName.Focus();
                    rbool = false; ;
                }

                return rbool;

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("用户管理 btnCheck()"+ex.Message.ToString());
                rbool = false;
            }
            return rbool;

        }
        #endregion
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void bntUpUser_Click(object sender, EventArgs e)
        {

            if (btnUpUser.Enabled)
           {
               btnUpUser.Enabled = false;
               bntUpUser_Click();
               btnUpUser.Enabled = true;
            }
          
           
        }

        private void bntUpUser_Click()
        {
            try
            {
                int j = 0;
                Expression<Func<UserInfo2, bool>> p = n => n.UserId == Convert.ToInt32(lvwUserList.SelectedRows[0].Cells["UserID"].Value);
                Action<UserInfo2> ap = n =>
                {
                    n.UserAddress = txtAddress.Text.Trim();
                    n.UserName = txtName.Text.Trim();
                    n.UserPhone = txtPhone.Text.Trim();
                    n.UserRemark = txtRemark.Text.Trim();
                    n.User_Dictionary_ID = Convert.ToInt32(cbxState.SelectedValue);
                    n.UserCarId = txtCardNo.Text.Trim();
                    n.User_Duty_ID = Convert.ToInt32(cobDuty_Name.SelectedValue);
                };
                if (!UserInfo2DAL.Update(p, ap))//用户是否修改失败
                {
                    MessageBox.Show(" 修改失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("修改成功", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                string Log_Content = String.Format("用户名称：{0} ", txtName.Text.Trim());
                Common.WriteLogData("修改", Log_Content, Common.USERNAME);//添加日志

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("用户管理 bntUpUser_Click()" + ex.Message.ToString());

            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
                ShowAddButton();
              

            }

        }
        /// <summary>
        /// 查重
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btn_Check_Click(object sender, EventArgs e)
        {
            if (btnCheck())
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "该用户名可用", txtName, this);
            }
        }
       
        #region 查看修改信息
        private void tsbtnUpdateSelect()
        {
            if (lvwUserList.SelectedRows.Count > 0)//选中行
            {
                if (lvwUserList.SelectedRows.Count > 1)
                {
                    MessageBox.Show("修改只能选中一行！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    //////修改的值
    
                    if (lvwUserList.SelectedRows[0].Cells["Userid"].Value == null) return;
                        strUserID = lvwUserList.SelectedRows[0].Cells["Userid"].Value.ToString();
                        int iuserid = int.Parse(lvwUserList.SelectedRows[0].Cells["Userid"].Value.ToString());

                        Expression<Func<View_UserInfo2_D_R_d, bool>> funviewinto = n => n.UserId == iuserid;
                        IEnumerable<View_UserInfo2_D_R_d> user = UserInfo2DAL.Query(funviewinto);
                   foreach (var n in user)
                    {
                        
                          if(n.UserName!=null)
                        {
                        //真实姓名
                        txtName.Text = n.UserName;
                        }
                        if(n.UserPhone!=null)
                        {
                        //用户联系电话
                        txtPhone.Text = n.UserPhone;
                        }
                       
                        //if(n.Role_Name!=null)
                        //{
                        ////角色名称
                        //cbxState.Text = n.Role_Name;
                        //}
                        if (n.Duty_Name != null)
                        {
                        //职务名称
                            cobDuty_Name.Text = n.Duty_Name;
                        }
                        if (n.Dictionary_Name != null)
                        {
                            //状态
                            cbxState.Text = n.Dictionary_Name;
                        }
                        //txtCardNo.ReadOnly = true;
                        if(n.UserCarId!=null)
                        {
                        //IC卡编号
                        txtCardNo.Text = n.UserCarId;
                        }
                        if(n.UserAddress!=null)
                        {
                        //用户联系地址
                        txtAddress.Text = n.UserAddress;
                        }
                        if(n.UserRemark!=null)
                        {
                        //备注
                        txtRemark.Text = n.UserRemark;
                        }
                        break;
                    }
        
                    ////
                    //if (dt.Rows[0]["user_permssion"].ToString().Substring(0, 1) == "1")
                    //{
                    //    chkGetWeight.Checked = true;
                    //}
                    //隐藏添加按钮
                    btnAdd.Visible = false;
                    btnUpUser.Visible = true;
                    btnUserCancle.Visible = true;
                    //txtLoginId.ReadOnly = true;

                }
            }
            else
            {
                MessageBox.Show("请选择要修改的行！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        #endregion
        #region 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        private void selectTJ()
        {


            try
            {

                //if (expr == null)
                //{
                    //expr = PredicateExtensionses.True<View_UserInfo_D_R_d>();
                expr = (Expression<Func<View_UserInfo2_D_R_d, bool>>)PredicateExtensionses.True<View_UserInfo2_D_R_d>();
                //}
                int i = 0;
                //if (txtSearchId.Text.Trim() != "")//用户名
                //{
                //    //expr = expr.And(n => n.UserLoginId == txtSearchId.Text.Trim());
                //    expr = expr.And(n => SqlMethods.Like(n.UserLoginId, "%" + txtSearchId.Text.Trim() + "%") && n.UserLoginId != "emewe");
                //    i++;

                //}
                if (txt_Name.Text.Trim() != "")//真实姓名
                {
                    //expr = expr.And(n => n.UserName == txt_Name.Text.Trim());
                    expr = expr.And(n => SqlMethods.Like(n.UserName, "%" + txt_Name.Text.Trim() + "%"));
                    i++;
                }
                if (cobDuty_Name2.SelectedValue != "") //职位
                {
                    expr = expr.And(n => SqlMethods.Like(n.Duty_Name, "%" + cobDuty_Name2.Text.Trim() + "%"));
                    i++;
                }
                //if (cmbSearchRole.Text != "")//角色
                //{
                //    expr = expr.And(n => n.Role_Name == cmbSearchRole.Text && n.UserLoginId != "emewe");

                //    i++;
                //}
                //if (i == 0)
                //{
                //    expr = expr.And(n => n.UserLoginId != "emewe");
                //}
                
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("用户管理 selectTJ()" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }

        ///条件查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Enabled)
            {
                btnSearch.Enabled = false;
                selectTJ();
                btnSearch.Enabled = true;
            }

        }
        #endregion

        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tslSelectAll_Click()
        {
            
            for (int i = 0; i < lvwUserList.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)lvwUserList.Rows[i].Cells[0]).Value = true;
                lvwUserList.Rows[i].Selected = true;
            }
        }
        /// <summary>
        ///删除用户信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tbtnDelUser_delete()
        {
            try
            {
                string Log_Content ="";
                int j = 0;
                if (lvwUserList.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //选中数量
                        int count = lvwUserList.SelectedRows.Count;
                        string id = "";
                        int iuser = 0;
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            //if (i == 0)
                            //{
                            //    id = lvwUserList.SelectedRows[i].Cells["UserLoginId"].Value.ToString();
                            //    continue;
                            //}
                            if (lvwUserList.SelectedRows[i].Cells["Userid"].Value != null)
                            {
                                Log_Content += lvwUserList.SelectedRows[i].Cells["Userid"].Value.ToString();
                                int.TryParse(lvwUserList.SelectedRows[i].Cells["Userid"].Value.ToString(), out iuser);
                                if (iuser > 0)
                                {
                                    Expression<Func<UserInfo2, bool>> funuserinfo = n => n.UserId == iuser;

                                    if (!UserInfo2DAL.DeleteToMany(funuserinfo))
                                    {
                                        j++;
                                    }
                                }
                            }
                           // if (lvwUserList.SelectedRows[i].Cells["UserId"].Value!=null)
                           //{
                           //    //删除用户的所有权限
                           //    Expression<Func<PermissionsInfo, bool>> funmenuinfo = n => n.Permissions_UserId ==Common.GetInt( lvwUserList.SelectedRows[i].Cells["UserId"].Value.ToString());
                           //    if (!PermissionsInfoDAL.DeleteToMany(funmenuinfo))//用户权限菜单是否删除失败
                           //    {
                           //        j++;
                           //    }
                           //}
                           

                        }
                        if (j == 0)
                        {
                            MessageBox.Show("成功删除", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("删除失败", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        Common.WriteLogData("删除", "删除用户" + Log_Content, Common.USERNAME);//操作日志

                    }
                    
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要删除的行！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
               
               

            }
            catch(Exception ex)
            {

                Common.WriteTextLog("用户管理 tbtnDelUser_delete()+" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();//更新
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
            LoadData();
        }
    //分页菜单响应事件
        private void bdnInfo_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsbtnUpdate")//修改
            {
                if (tsbtnUpdate.Enabled)
                {
                    tsbtnUpdate.Enabled = false;
                    tsbtnUpdateSelect();
                    tsbtnUpdate.Enabled = true;
                }
               
                return;
            }
            if (e.ClickedItem.Name == "tsbtnDel")//删除
            {
                tbtnDelUser_delete();
                return;
            }
            if (e.ClickedItem.Name == "tslSelectAll")//全选
            {
                if (tslSelectAll.Enabled)
                {
                    tslSelectAll.Enabled = false;
                    tslSelectAll_Click();
                    tslSelectAll.Enabled = true;
                }
               
                return;
            }
            if (e.ClickedItem.Name == "tslNotSelect")//取消全选
            {
                if (tslNotSelect.Enabled)
                {
                    tslNotSelect.Enabled = false;
                    tslNotSelect_Click();
                    tslNotSelect.Enabled = true;
                }
              
                return;
            }


            lvwUserList.DataSource = page.BindBoundControl<View_UserInfo2_D_R_d>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr);
        }



    }
}