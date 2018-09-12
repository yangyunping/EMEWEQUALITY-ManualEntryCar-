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
    public partial class UserAdmin : Form
    {
        private string userCreateName;//当前登录人
        public UserAdmin(string userCreateName)
        {
            this.userCreateName = userCreateName;
            InitializeComponent();
        }
        public MainFrom mf;//主窗体
        Expression<Func<View_UserInfo_D_R_d, bool>> expr = null;
        PageControl page = new PageControl();
        private bool rbool;//是否有添加权限
        private void UserAdmin_Load(object sender, EventArgs e)
        {
            InitUser();
          
        }
      
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {
            expr =PredicateExtensionses.True<View_UserInfo_D_R_d>();
            expr = expr.And(n => n.UserLoginId != "emewe");
            this.lvwUserList.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            LoadData();
            bingcob_Duty_Name();
            bingcob_QCGroup_Name(cmbRole);
            bingcob_QCGroup_Name(cmbSearchRole);
            bindState();
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
                lvwUserList.DataSource = page.BindBoundControl<View_UserInfo_D_R_d>("", txtCurrentPage2, lblPageCount2, expr);

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
            cobDuty_Name.Text = "";
        }
        //绑定角色
        private void bingcob_QCGroup_Name(ComboBox cobName)
        {

            cobName.DataSource = LinQBaseDao.Query<RoleInfo>(new DCQUALITYDataContext());
            cobName.DisplayMember = "Role_Name";
            cobName.ValueMember = "Role_Id";
            cobName.Text = "";

            //var m=new 
        }

        /// <summary>
        /// 绑定状态
        /// </summary>
        private void bindState()
        {
            List<Dictionary> list = DictionaryDAL.GetValueStateDictionary("状态");
            list.RemoveAt(list.Count - 1);
            list.RemoveAt(list.Count-1);
            cbxState.DataSource = list;
           cbxState.DisplayMember = "Dictionary_Name";
           cbxState.ValueMember = "Dictionary_ID";

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
            txtLoginId.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtRemark.Text = "";
            txtLoginId.ReadOnly = false;
            txtCardNo.Text = "";
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
                var useradd = new UserInfo
                {
                    UserLoginId = txtLoginId.Text.Trim(),
                    UserLoginPwd = txtPassword.Text.Trim(),
                    UserName = txtName.Text.Trim(),
                    UserPhone = txtPhone.Text.Trim(),
                    UserAddress = txtAddress.Text.Trim(),
                    //,UserPermission                    //权限信息
                    //,UserIdentification                 //加密狗
                    UserCarId = txtCardNo.Text.Trim(),
                    UserCreateTime = DateTime.Now,
                    UserCreateName = userCreateName,                      //创建人
                    User_Duty_ID = Convert.ToInt32(cobDuty_Name.SelectedValue.ToString()),
                    User_Role_Id = Convert.ToInt32(cmbRole.SelectedValue.ToString()),
                     User_Dictionary_ID = Convert.ToInt32(cbxState.SelectedValue.ToString()),        //用户状态
                    UserRemark = txtRemark.Text.Trim()
                };
               
                if (UserInfoDAL.InsertOneQCRecord(useradd, out rint))
                {
                    MessageBox.Show("添加成功", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加失败", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                string Log_Content = String.Format("用户名称：{0} ", txtLoginId.Text.Trim());
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

                var LoginId = txtLoginId.Text.Trim();
                if (LoginId == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "登录名不能为空！", txtLoginId, this);
                    txtLoginId.Text = "";
                    txtLoginId.Focus();
                    rbool = false;
                }
                Expression<Func<View_UserInfo_D_R_d, bool>> funview_userinfo = n => n.UserLoginId == LoginId;
                if (UserInfoDAL.Query(funview_userinfo).Count()>0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "该用户名已存在", txtLoginId, this);
                    txtLoginId.Focus();
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
                Expression<Func<UserInfo, bool>> p = n => n.UserLoginId == txtLoginId.Text.Trim();
                Action<UserInfo> ap = n =>
                {
                    n.UserAddress = txtAddress.Text.Trim();
                    n.UserLoginId = txtLoginId.Text.Trim();
                    n.UserName = txtName.Text.Trim();
                    n.UserLoginPwd = txtPassword.Text.Trim();
                    n.UserPhone = txtPhone.Text.Trim();
                    n.UserRemark = txtRemark.Text.Trim();
                    n.User_Role_Id = Convert.ToInt32(cmbRole.SelectedValue);
                    n.UserCarId = txtCardNo.Text.Trim();
                    n.User_Duty_ID = Convert.ToInt32(cobDuty_Name.SelectedValue);
                    n.User_Dictionary_ID = Convert.ToInt32(cbxState.SelectedValue);
                };
                if (!UserInfoDAL.Update(p, ap))//用户是否修改失败
                {
                    MessageBox.Show(" 修改失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("修改成功", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                //if (!UserInfoDAL.Update(p, ap))//用户是否修改失败
                //{
                //    j++;

                //}

                ////删除角色的所有权限
                //Expression<Func<PermissionsInfo, bool>> funmenuinfo = n => n.Permissions_UserId == Common.UserID;
                //if (!PermissionsInfoDAL.DeleteToMany(funmenuinfo))//用户权限菜单是否删除失败
                //{
                //    j++;
                //}
                //for (int i = 0; i < Common.arraylist.Count; i++)
                //{

                //    var permissionsInfo = new PermissionsInfo
                //    {
                //        Permissions_Menu_ID = Common.GetInt(Common.arraylist[i].ToString()),
                //        Permissions_Dictionary_ID = DictionaryDAL.GetDictionaryID("启动"),
                //        Permissions_Visible = true,
                //        Permissions_Enabled = true,
                //        Permissions_UserId = Common.UserID

                //    };
                //    if (!PermissionsInfoDAL.InsertOneQCRecord(permissionsInfo))//是否添加失败
                //    {
                //        j++;
                //    }
                //}


                //if (j == 0)
                //{
                //    MessageBox.Show("修改成功", "系统提示");
                //}
                //else
                //{
                //    MessageBox.Show(" 修改失败", "系统提示");
                //}
                string Log_Content = String.Format("用户名称：{0} ", txtLoginId.Text.Trim());
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
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "该用户名可用", txtLoginId, this);
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
                    //用户名(登录)
                    txtLoginId.ReadOnly = true; ;
                    if (lvwUserList.SelectedRows[0].Cells["UserLoginId"].Value ==null) return;
                    if (lvwUserList.SelectedRows[0].Cells["Userid"].Value ==null) return;
                    Common.UserID = Common.GetInt(lvwUserList.SelectedRows[0].Cells["Userid"].Value.ToString());
                    txtLoginId.Text = lvwUserList.SelectedRows[0].Cells["UserLoginId"].Value.ToString();
                    Expression<Func<View_UserInfo_D_R_d, bool>> funviewinto = n => n.UserLoginId == txtLoginId.Text.Trim();
                    foreach (var n in UserInfoDAL.Query(funviewinto))
                    {
                        
                        //密码
                        //txtPassword.ReadOnly = true;
                        if (n.UserLoginPwd!=null)
                        {
                          txtPassword.Text = n.UserLoginPwd;
                        }
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
                        if(n.Role_Name!=null)
                        {
                        //角色名称
                        cmbRole.Text = n.Role_Name;
                        }
                        if(n.Duty_Name!=null)
                        {
                        //职务名称
                        cobDuty_Name.Text =n.Duty_Name;
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
                    expr = (Expression<Func<View_UserInfo_D_R_d, bool>>)PredicateExtensionses.True<View_UserInfo_D_R_d>();
                //}
                int i = 0;
                if (txtSearchId.Text.Trim() != "")//用户名
                {
                    //expr = expr.And(n => n.UserLoginId == txtSearchId.Text.Trim());
                    expr = expr.And(n => SqlMethods.Like(n.UserLoginId, "%" + txtSearchId.Text.Trim() + "%") && n.UserLoginId != "emewe");
                    i++;

                }
                if (txt_Name.Text.Trim() != "")//真实姓名
                {
                    //expr = expr.And(n => n.UserName == txt_Name.Text.Trim());
                    expr = expr.And(n => SqlMethods.Like(n.UserName, "%" + txt_Name.Text.Trim() + "%") && n.UserLoginId != "emewe");
                    i++;

                }
                if (cmbSearchRole.Text != "")//角色
                {
                    expr = expr.And(n => n.Role_Name == cmbSearchRole.Text && n.UserLoginId != "emewe");

                    i++;
                }
                if (i == 0)
                {
                    expr = expr.And(n => n.UserLoginId != "emewe");
                }
                
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
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            //if (i == 0)
                            //{
                            //    id = lvwUserList.SelectedRows[i].Cells["UserLoginId"].Value.ToString();
                            //    continue;
                            //}
                            if (lvwUserList.SelectedRows[i].Cells["UserLoginId"].Value!=null)
                            {
                                Log_Content += lvwUserList.SelectedRows[i].Cells["UserLoginId"].Value.ToString();
                                Expression<Func<UserInfo, bool>> funuserinfo = n => n.UserLoginId == lvwUserList.SelectedRows[i].Cells["UserLoginId"].Value.ToString();

                                if (!UserInfoDAL.DeleteToMany(funuserinfo))
                                {
                                    j++;
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    
       
        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {

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
            if (e.ClickedItem.Name == "tsl_Role")//权限设置
            {
                if (tsl_Role.Enabled)
                {
                    tsl_Role.Enabled = false;
                    tsl_Role_Click();
                    tsl_Role.Enabled = true;
                }
               
                return;
            }

            lvwUserList.DataSource = page.BindBoundControl<View_UserInfo_D_R_d>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr);
        }
        /// <summary>
        /// 用户权限配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_UserRole_Click(object sender, EventArgs e)
        {
            UserRoleADD uradd = new UserRoleADD();
            uradd.rbool = true;
            uradd.YesNoBoolRoleUser = true;
            uradd.ShowDialog();
        }

      
        /// <summary>
        /// 权限设置
        /// </summary>
        /// <param name="?"></param>
        private void tsl_Role_Click()
        {
            
            try
            {
                if (lvwUserList.SelectedRows.Count > 0)//选择行
                {
                    if (lvwUserList.SelectedRows.Count > 1)
                    {
                        MessageBox.Show("只能选择一行进行权限设置", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (lvwUserList.SelectedRows[0].Cells["Userid"].Value == null) return;
                        if (lvwUserList.SelectedRows[0].Cells["Role_Id"].Value == null) return;

                        if (Common.GetInt(lvwUserList.SelectedRows[0].Cells["Userid"].Value.ToString()) > 0)
                        {
                            UserRoleADD uradd = new UserRoleADD();
                            uradd.YesNoBoolRoleUser = true;
                            uradd.rbool = false;
                            Common.UserID = Common.GetInt(lvwUserList.SelectedRows[0].Cells["Userid"].Value.ToString());
                            Common.RoleID = Common.GetInt(lvwUserList.SelectedRows[0].Cells["Role_Id"].Value.ToString());
                            mf.ShowChildForm(uradd);
                            //uradd.ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择行", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("UserRole.tsl_Role_Click()" + ex.Message.ToString());
            }
        }

    }
}