using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EMEWEEntity;
using EMEWEDAL;
using System.Linq.Expressions;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class UserRoleADD : Form
    {
        public UserRoleADD()
        {
            InitializeComponent();
        }
        public bool rbool;//true：添加  false:修改
        public int RoleID;//角色ID
        public bool YesNoBoolRoleUser;//true：用户权限  false:角色权限
        ArrayList arraylist = new ArrayList();
        private void UserRoleADD_Load(object sender, EventArgs e)
        {
            InitUser();
        }
         /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {
            YesNoAddUpdate();
            InitMenu();
            if (!YesNoBoolRoleUser)
            {
                btnUpdateRole();//角色绑定权限
            }
            else
            {
                btnUpdateUser();//用户绑定权限
            }
        }
        /// <summary>
        /// 判断当前操作是修改还是添加用户权限
        /// </summary>
        private void YesNoAddUpdate()
        {
            if (!rbool)
            {
                btnUpdate.Visible = true;
                btnAdd.Visible = false;
                btnCancle.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
                btnAdd.Visible = true;
                btnCancle.Visible = false;
            }

        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// 
        protected void InitMenu()
        {
            //加载TreeView菜单           
            LoadNode(tv_MenuInfo.Nodes, "0");
          
        }
        /// <summary>
        /// 递归创建TreeView菜单(模块列表)
        /// </summary>
        /// <param name="node">子菜单</param>
        /// <param name="MtID">子菜单上级ID</param>
        protected void LoadNode(TreeNodeCollection node, string MtID)
        {

            Expression<Func<MenuInfo, bool>> funmenuinfo = n => n.Menu_OtherID.ToString() == MtID;//过滤父节点 
            var p = MenuInfoDAL.Query(funmenuinfo);
            TreeNode nodeTemp;
            foreach (var m in p)
            {
                nodeTemp = new TreeNode();
                nodeTemp.Tag = m.Menu_ID;


                if (Common.USERNAME == "emewe")
                {
                    //dv["Menu_ID"].ToString();
                    if (m.Menu_ControlText != null)
                    {
                        nodeTemp.Text = m.Menu_ControlText;
                    }
                    //dv["Menu_ControlText"].ToString();  //节点名称 
                    if (nodeTemp != null)
                    {
                        node.Add(nodeTemp);  //加入节点 
                    }
                }
                else
                {
                    if (m.Menu_Visible.Value)//Menu_Visible为TRUE才显示
                    {
                        //dv["Menu_ID"].ToString();
                        if (m.Menu_ControlText != null)
                        {
                            nodeTemp.Text = m.Menu_ControlText;
                        }
                        //dv["Menu_ControlText"].ToString();  //节点名称 
                        if (nodeTemp != null)
                        {
                            node.Add(nodeTemp);  //加入节点 
                        }
                    }
                }

                this.LoadNode(nodeTemp.Nodes, nodeTemp.Tag.ToString().Split(',')[0]);  //递归

            }

        }
        #region 选中父级菜单该菜单下的所有子级菜单自动选中
        private void tv_MenuInfo_AfterCheck(object sender, TreeViewEventArgs e)
        {
          
            if (e.Action != TreeViewAction.Unknown)
            {
                SetNodeCheckStatus(e.Node, e.Node.Checked);
                SetNodeStyle(e.Node);
            }
           
        }
        private void SetNodeCheckStatus(TreeNode tn, bool Checked)
        {

            if (tn == null) return;
            foreach (TreeNode tnChild in tn.Nodes)
            {

                tnChild.Checked = Checked;

                SetNodeCheckStatus(tnChild, Checked);

            }
            TreeNode tnParent = tn;
        }



        private void SetNodeStyle(TreeNode Node)
        {
            int nNodeCount = 0;
            if (Node.Nodes.Count != 0)
            {
                foreach (TreeNode tnTemp in Node.Nodes)
                {

                    if (tnTemp.Checked == true)

                        nNodeCount++;
                }

                if (nNodeCount == Node.Nodes.Count)
                {
                    Node.Checked = true;
                    Node.ExpandAll();
                    Node.ForeColor = Color.Black;
                }
                else if (nNodeCount == 0)
                {
                    Node.Checked = false;
                    Node.Collapse();
                    Node.ForeColor = Color.Black;
                }
                else
                {
                    Node.Checked = true;
                    Node.ForeColor = Color.Gray;
                }
            }
            //当前节点选择完后，判断父节点的状态，调用此方法递归。   
            if (Node.Parent != null)
                SetNodeStyle(Node.Parent);
        }
        #endregion

      
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                arraylist.Clear();//清空动态数组内的成员
                add();
                Common.arraylist = arraylist;
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("UserRoleADD.btnAdd_Click()" + ex.Message.ToString());
            }
            finally {
                this.Close();
            }
        }
        /// <summary>
        /// 查出选中的父级ID
        /// </summary>
        private void add( )
        {
            if (tv_MenuInfo != null)
            {
                foreach (TreeNode tnTemp in tv_MenuInfo.Nodes)
                {

                    if (tnTemp.Checked == true)
                    {
                        arraylist.Add(tnTemp.Tag);
                    }
                    addDiGui(tnTemp);
                }
            }
        }
       /// <summary>
       /// 递归出所有选中的子级
       /// </summary>
       /// <param name="tn"></param>
        private void addDiGui(TreeNode tn)
        {
            if (tn != null)
            {
                foreach (TreeNode tnTemp in tn.Nodes)
                {

                    if (tnTemp.Checked == true)
                    {
                        arraylist.Add(tnTemp.Tag);
                    }
                    addDiGui(tnTemp);

                }
            }
        }
        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnUpdate.Enabled)
            {
                btnUpdate.Enabled = false;
                btnUpdate_Click();
                Common.WriteLogData("修改", "用户编号为"+Common.UserID +"角色编号为:"+Common.RoleID+"权限信息", Common.NAME); //操作日志
                btnUpdate.Enabled = true;
            }
           
        }
        private void btnUpdate_Click()
        {
            try
            {

                arraylist.Clear();//清空动态数组内的成员
                add();
                int j = 0;
                bool Menu_Enabled = true;
                bool Menu_Visible = true;
                if(!chb_Menu_Visible.Checked)
                {
                    Menu_Visible = false;
                }
                if(!chb_Menu_Enabled.Checked)
                {
                 Menu_Enabled = false;
                }
                if (YesNoBoolRoleUser)
                {
                    //删除用户的所有权限
                    if (Common.UserID <= 0) return;
                    Expression<Func<PermissionsInfo, bool>> funmenuinfo = n => n.Permissions_UserId == Common.UserID;
                    if (!PermissionsInfoDAL.DeleteToMany(funmenuinfo))//用户权限菜单是否删除失败
                    {
                        j++;
                    }
                    for (int i = 0; i < arraylist.Count; i++)
                    {

                        var permissionsInfo = new PermissionsInfo
                        {
                            Permissions_Menu_ID = Common.GetInt(arraylist[i].ToString()),
                            Permissions_Dictionary_ID = DictionaryDAL.GetDictionaryID("启动"),
                            Permissions_Visible = Menu_Visible,
                            Permissions_Enabled = Menu_Enabled,
                          
                            Permissions_UserId = Common.UserID

                        };
                        if (!PermissionsInfoDAL.InsertOneQCRecord(permissionsInfo))//是否添加失败
                        {
                            j++;
                        }
                    }
                }
                else
                {
                    //删除用户的所有权限
                    Expression<Func<PermissionsInfo, bool>> funmenuinfo = n => n.Permissions_Role_Id == Common.RoleID;
                    if (!PermissionsInfoDAL.DeleteToMany(funmenuinfo))//用户权限菜单是否删除失败
                    {
                        j++;
                    }
                    for (int i = 0; i < arraylist.Count; i++)
                    {

                        var permissionsInfo = new PermissionsInfo
                        {
                            Permissions_Menu_ID = Common.GetInt(arraylist[i].ToString()),
                            Permissions_Dictionary_ID = DictionaryDAL.GetDictionaryID("启动"),
                            Permissions_Visible = Menu_Visible,
                            Permissions_Enabled = Menu_Enabled,
                            Permissions_Role_Id = Common.RoleID

                        };
                        if (!PermissionsInfoDAL.InsertOneQCRecord(permissionsInfo))//是否添加失败
                        {
                            j++;
                        }
                    }
                }
                if (j == 0)
                {
                    MessageBox.Show("添加成功", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("添加失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("添加成功", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Common.WriteTextLog("UserRoleADD.btnUpdate_Click()" + ex.Message.ToString());
            }
            finally
            {
                this.Close();
            }
            //try
            //{
            //    arraylist.Clear();//清空动态数组内的成员
            //    add();
            //    Common.arraylist = arraylist;
            //}
            //catch (Exception ex)
            //{
            //    Common.WriteTextLog("UserRoleADD.btnUpdate_Click()" + ex.Message.ToString());
            //}
            //finally
            //{
            //    this.Close();
            //}
        }
        /// <summary>
        /// 根据角色ID查数据
        /// </summary>
        private void btnUpdateRole()
        {
            if (Common.RoleID > 0)
            {
                Expression<Func<PermissionsInfo, bool>> funPermissionsInfo = n => n.Permissions_Role_Id == Common.RoleID;
                var permissionsInfo = PermissionsInfoDAL.Query(funPermissionsInfo);//根据角色ID查出菜单ID
                if (permissionsInfo == null) return;//没有数据跳出方法
                foreach (var m in permissionsInfo)
                {
                    if (m.Permissions_Menu_ID.Value > 0)
                    {
                        UpdateSelectRole(m.Permissions_Menu_ID.Value);
                    }
                }

            }
        }
        /// <summary>
        /// 根据用户ID查数据
        /// </summary>
        private void btnUpdateUser()
        {
            if (Common.UserID > 0 || Common.RoleID>0)
            {
                Expression<Func<PermissionsInfo, bool>> funPermissionsInfo = n => n.Permissions_UserId == Common.UserID || n.Permissions_Role_Id==Common.RoleID;
                var permissionsInfo = PermissionsInfoDAL.Query(funPermissionsInfo);//根据用户ID查出菜单ID
                if (permissionsInfo == null) return;//没有数据跳出方法
                foreach (var m in permissionsInfo)
                {
                    if (m.Permissions_Menu_ID.Value > 0)
                    {
                        UpdateSelectRole(m.Permissions_Menu_ID.Value);
                    }
                }
            }
        }
        /// <summary>
        /// 遍历一级菜单
        /// </summary>
        /// <param name="Menu_ID"></param>
        private void UpdateSelectRole(int Menu_ID)
        {
            foreach (TreeNode tnTemp in tv_MenuInfo.Nodes)
            {
                if (tnTemp.Tag!=null)
                {
                    if (Common.GetInt(tnTemp.Tag.ToString()) == Menu_ID)
                    {

                        tnTemp.Checked = true;
                        tnTemp.ExpandAll();//展开所有子节点
                    }
                    else
                    {
                        UpdateSelectRoleDiGui(tnTemp, Menu_ID);
                    }
                }
            }
        }
        /// <summary>
        /// 递归出有权限的菜单并选中
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="Menu_ID"></param>
        private void UpdateSelectRoleDiGui(TreeNode tn, int Menu_ID)
        {
            foreach (TreeNode tnTemp in tn.Nodes)
            {
                if (tnTemp.Tag != null)
                {
                    if (Common.GetInt(tnTemp.Tag.ToString()) == Menu_ID)
                    {
                        tnTemp.Checked = true;
                        tnTemp.ExpandAll();//展开所有子节点
                    }
                    else
                    {
                        UpdateSelectRoleDiGui(tnTemp, Menu_ID);
                    }
                }
            }
        }
        /// <summary>
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
       
       

        private void chb_SelectAll_Click(object sender, EventArgs e)
        {
            if (chb_SelectAll.Checked)
            {
                SelectAll(true);
            }
            else
            {
                SelectAll(false);
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        private void SelectAll(bool chrbool)
        {
            foreach (TreeNode tnTemp in tv_MenuInfo.Nodes)
            {
                if (tnTemp.Tag != null)
                {

                    tnTemp.Checked = chrbool;
                        tnTemp.ExpandAll();//展开所有子节点
                        SelectAllDiGui(tnTemp, chrbool);
                  
                }
            }
        }

        private void SelectAllDiGui(TreeNode tn, bool chrbool)
        {
            foreach (TreeNode tnTemp in tn.Nodes)
            {
                if (tnTemp.Tag != null)
                {

                    tnTemp.Checked = chrbool;
                    SelectAllDiGui(tnTemp, chrbool);
                        tnTemp.ExpandAll();//展开所有子节点
                   
                }
            }
        }
     

    }
}
