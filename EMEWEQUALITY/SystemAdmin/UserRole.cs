using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEEntity;
using EMEWEDAL;
using EMEWEQUALITY.HelpClass;
using System.Collections;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class UserRole : Form
    {
        public UserRole()
        {
            InitializeComponent();
        }
        public MainFrom mf;//主窗体
        UserRoleADD uradd = new UserRoleADD();
        Expression<Func<RoleInfo, bool>> expr = null;
        PageControl page = new PageControl();
        private bool rbool;//是否有添加权限
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
            //comb_PageMaxCount.SelectedText = "10条";
            if (btnAdd.Visible)
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
                lvwUserList.DataSource = page.BindBoundControl<RoleInfo>("", txtCurrentPage2, lblPageCount2, expr);
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
            //btnAdd.Visible = true;
            //btnUpdate.Visible = false;
            //btnCancle.Visible = false;
            //数据清空
            txtRoleName.Text = "";
            txtRemark.Text = "";
            chkCheckPaper.Checked = false;
            chkSetBag.Checked = false;
            chkSetPaper.Checked = false;
            chkSystem.Checked = false;
        }
        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //得到修改的数据
                string name = txtRoleName.Text;
                string permi = SetRoles();
                string remark = txtRemark.Text;
                string id = lblId.Text;
                int j = 0;
                Action<RoleInfo> action = n =>
                {
                    n.Role_Name = name;
                    n.Role_Permission = permi;
                    n.Role_Remark = remark;
                };
                Expression<Func<RoleInfo, bool>> funroleinfo = n => n.Role_Id.ToString() == id;
                if (!RoleInfoDAL.UpdateOneRoleInfo(funroleinfo, action))//角色是否修改失败
                {
                    j++;
                }

                ////删除角色的所有权限
                //Expression<Func<PermissionsInfo, bool>> funmenuinfo = n => n.Permissions_Role_Id.ToString()== id;
                //if (!PermissionsInfoDAL.DeleteToMany(funmenuinfo))//角色权限菜单是否删除失败
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
                //        Permissions_Role_Id = Common.RoleID

                //    };
                //    if (!PermissionsInfoDAL.InsertOneQCRecord(permissionsInfo))//是否添加失败
                //    {
                //        j++;
                //    }
                //}
               　 

                if(j==0)
                {
                    MessageBox.Show("修改成功","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(" 修改失败","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                string Log_Content = String.Format("角色名称：{0}", name);
                Common.WriteLogData("修改", Log_Content, Common.NAME);//添加日志
            }
            catch(Exception ex)
            {
                Common.WriteTextLog("角色管理 btnUpdate_Click" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }


        }
         /// <summary>
        /// 查重方法
        /// </summary>
        /// <returns></returns>
        private bool btnCheck()
        {

            bool rbool = true;
            try
            {

                var RoleNmae = txtRoleName.Text.Trim();
                if (RoleNmae == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "角色名不能为空！", txtRoleName, this);
                    txtRoleName.Text = "";
                    txtRoleName.Focus();
                    rbool = false;
                }
                Expression<Func<View_UserInfo_D_R_d, bool>> funview_userinfo = n => n.Role_Name == RoleNmae;
                if (UserInfoDAL.Query(funview_userinfo).Count()>0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "该角色已存在", txtRoleName, this);
                    txtRoleName.Focus();
                    rbool = false; ;
                }

                return rbool;

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("角色管理 btnCheck()"+ex.Message.ToString());
                rbool = false;
            }
            return rbool;

        }
      
        #region 添加角色

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                int j = 0;
                int rint = 0;
                ////得到添加的数据
                string name = txtRoleName.Text;
                string permi = SetRoles();
                string remark = txtRemark.Text;
                if (!btnCheck()) return;
                var rf = new RoleInfo
                {
                    Role_Name = name,
                    Role_Permission = permi,
                    Role_Remark = remark
                };
                if (!RoleInfoDAL.InsertOneRoleInfo(rf,out rint))
                {
                    j++;
                }
               
                if (j == 0)
                {
                    MessageBox.Show("成功增加", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("成功失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                string Log_Content = String.Format("角色名称：{0}", name);
                Common.WriteLogData("新增", Log_Content, Common.NAME);//添加日志
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("角色管理 btnAdd_Click()" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();//更新数据
            }
        }
       
        /// <summary>
        /// 设置权限
        /// </summary>
        /// <returns></returns>

        private string SetRoles()
        {
            string role = "";
            //系统管理
            if (chkSystem.Checked)
            {
                role += 1;
            }
            else
            {
                role += 0;
            }
            //异常管理
            if (chkSetBag.Checked)
            {
                role += 1;
            }
            else
            {
                role += 0;
            }
            //质检管理
            if (chkCheckPaper.Checked)
            {
                role += 1;
            }
            else
            {
                role += 0;
            }
            //数据统计
            if (chkSetPaper.Checked)
            {
                role += 1;
            }
            else
            {
                role += 0;
            }

            //车辆登记
            if (chkCheckCar.Checked)
            {
                role += 1;
            }
            else
            {
                role += 0;
            }
            //水份管理
            if (chb_ShuiFen.Checked)
            {
                role += 1;
            }
            else
            {
                role += 0;
            }
            return role;
        }
        #endregion
        #region 取消修改
        /// <summary>
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            ShowAddButton();
        }
        #endregion
        #region 查看修改信息
        /// <summary>
        /// 查看修改信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnUpdate_select()
        {
            try
            {
                if (lvwUserList.SelectedRows.Count > 0)//选中行
                {
                    if (lvwUserList.SelectedRows.Count > 1 || lvwUserList.SelectedRows[0].Cells[1].Value.ToString() == "")
                    {
                        MessageBox.Show("修改只能选中一行且修改行信息不能为空！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        //清空权限
                        txtRoleName.ReadOnly = true;
                        chkCheckPaper.Checked = false;
                        chkSetBag.Checked = false;
                        chkSetPaper.Checked = false;
                        chkSystem.Checked = false;
                        chb_ShuiFen.Checked = false;
                        if (lvwUserList.SelectedRows[0].Cells["Role_Id"].Value!= null)
                        {
                            int id = Convert.ToInt32(lvwUserList.SelectedRows[0].Cells["Role_Id"].Value.ToString());
                            Common.RoleID = id;
                            lblId.Text = id.ToString();
                            Expression<Func<RoleInfo, bool>> funbif=n=>n.Role_Id==id;
                            foreach(var n in RoleInfoDAL.Query(funbif))
                            {
                                if (n.Role_Name != null)
                                {
                                    txtRoleName.Text = n.Role_Name;
                                }
                                if(n.Role_Permission!=null)
                                {
                                    txtRemark.Text = n.Role_Permission;
                                }
                                if(n.Role_Remark!=null)
                                {
                                    txtRemark.Text = n.Role_Remark;
                                }
                                if (n.Role_Permission != null)
                                {
                                    string peri = n.Role_Permission;
                                    //系统管理0:隐藏
                                    if (peri.Substring(0, 1) == "1")
                                    {
                                        chkSystem.Checked = true;
                                    }
                                    //异常管理
                                    if (peri.Substring(1, 1) == "1")
                                    {
                                        chkSetBag.Checked = true;
                                    }
                                    //质检管理
                                    if (peri.Substring(2, 1) == "1")
                                    {
                                        chkCheckPaper.Checked = true;
                                    }
                                    //数据统计
                                    if (peri.Substring(3, 1) == "1")
                                    {
                                        chkSetPaper.Checked = true;
                                    }
                                    //车辆登记
                                    if (peri.Substring(4, 1) == "1")
                                    {
                                        chkCheckCar.Checked = true;
                                    }
                                    //水分管理
                                    if (peri.Substring(5, 1) == "1")
                                    {
                                        chb_ShuiFen.Checked = true;
                                    }
                                }
                                break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("选中行的角色ID不能为空","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        //显示隐藏按钮
                        btnAdd.Visible = false;
                        btnCancle.Visible = true;
                        btnUpdate.Visible = true;

                    }
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要修改的行！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                Common.WriteTextLog("角色管理 tsbtnUpdate_Click" + ex.Message.ToString());
            }
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDel_delete()
        {
            try
            {
                int j = 0;
                string Log_Content = "";
                if (lvwUserList.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //选中数量
                        int count = lvwUserList.SelectedRows.Count;
                        //string id = "";
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            Expression<Func<View_UserInfo_D_R_d, bool>> funviewuserinfo = n => n.Role_Name == lvwUserList.SelectedRows[i].Cells["Role_Name"].Value.ToString();
                            if (UserInfoDAL.Query(funviewuserinfo).Count() > 0)
                            {
                                MessageBox.Show("删除的用户角色中有角色正在使用，不能删除！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                return;
                            }
                            Expression<Func<RoleInfo, bool>> funuserinfo = n => n.Role_Id.ToString() == lvwUserList.SelectedRows[i].Cells["Role_Id"].Value.ToString();
                            if (!RoleInfoDAL.DeleteToMany(funuserinfo))
                            {
                                j++;
                            }
                            else
                            {
                                Log_Content += lvwUserList.SelectedRows[i].Cells["Role_Id"].Value.ToString()+" ";
                            }
                            if (lvwUserList.SelectedRows[i].Cells["Role_Id"].Value != null)
                            {
                                //删除角色的所有权限
                                Expression<Func<PermissionsInfo, bool>> funmenuinfo = n => n.Permissions_Role_Id == Common.GetInt(lvwUserList.SelectedRows[i].Cells["Role_Id"].Value.ToString());
                                if (!PermissionsInfoDAL.DeleteToMany(funmenuinfo))//用户权限菜单是否删除失败
                                {
                                    j++;
                                }
                            }

                         

                        }
                        if (j == 0)
                        {
                            MessageBox.Show("成功删除", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("删除失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        Common.WriteLogData("删除", "删除角色" + Log_Content, Common.NAME);//操作日志

                    }

                }
                else//没有选中
                {
                    MessageBox.Show("请选择要删除的行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



            }
            catch(Exception ex)
            {
                Common.WriteTextLog("角色管理  tsbtnDel_delete()" + ex.Message.ToString());
               
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }
       
        #endregion
        #region 全选
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click()
        {
            for (int i = 0; i < lvwUserList.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)dgvRoleList.Rows[i].Cells[0]).Value = true;
                lvwUserList.Rows[i].Selected = true;
            }
        }
        #endregion 
        #region 取消全选
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click()
        {
            //rowlist.Clear();
            for (int i = 0; i < lvwUserList.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)dgvRoleList.Rows[i].Cells[0]).Value = false;
                lvwUserList.Rows[i].Selected = false;
            }
        }
        #endregion
        #region 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Enabled)
            {
                btnSearch.Enabled = false;
                SelectShere();
                btnSearch.Enabled = true;
            }
            //expr = null;
            //page = new PageControl();
            //page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            //LoadData("");
        }
        private void SelectShere()
        {
            try
            {
                //if (expr == null)
                //{
                    expr = PredicateExtensionses.True<RoleInfo>();
                //}
                string SearchName = txtSearchName.Text.Trim();
                int i = 0;
                if (SearchName != "")//根据角色名查询
                {
                    //expr = expr.And(n => n.Role_Name == SearchName);
                    expr = expr.And(n => SqlMethods.Like(n.Role_Name, "%" + SearchName + "%"));
                    i++;
                }
                if (i == 0)
                {
                    expr = null;

                }

               
            }
            catch(Exception ex)
            {
                Common.WriteTextLog("UserRole.SelectShere()"+ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }
        
        #endregion
        //初始化
        private void UserRole_Load(object sender, EventArgs e)
        {
            InitUser();
            
        }

        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsbtnUpdate")//修改
            {
                //mf = new MainFrom();
                tsbtnUpdate_select();
                //mf.ShowChildForm(uradd);
                //UserRoleADD uradd = new UserRoleADD();
                // uradd.rbool = false;
                // uradd.YesNoBoolRoleUser = false;
                //uradd.ShowDialog();
               
                return;
            }
            if (e.ClickedItem.Name == "tsbtnDel")//删除
            {
                tsbtnDel_delete();
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

            lvwUserList.DataSource = page.BindBoundControl<RoleInfo>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr);
        }
        /// <summary>
        /// 设置每页显示最大条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData();
        }
     
        private void lb_role_Click(object sender, EventArgs e)
        {
            UserRoleADD uradd = new UserRoleADD();
            uradd.rbool = true;
            uradd.YesNoBoolRoleUser = false;
            mf.ShowChildForm(uradd);
        }

        private void lblId_Click(object sender, EventArgs e)
        {
            MenuInfoADD menuinfo = new MenuInfoADD();
            menuinfo.ShowDialog();

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
                        if (lvwUserList.SelectedRows[0].Cells["Role_Id"].Value == null) return;

                        if (Common.GetInt(lvwUserList.SelectedRows[0].Cells["Role_Id"].Value.ToString()) > 0)
                        {
                            UserRoleADD uradd = new UserRoleADD();
                            uradd.YesNoBoolRoleUser = false;
                            uradd.rbool = false;
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
