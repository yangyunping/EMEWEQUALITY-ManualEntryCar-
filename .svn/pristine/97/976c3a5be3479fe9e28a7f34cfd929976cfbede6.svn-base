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
using System.Linq.Expressions;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class MenuInfoADD : Form
    {
        public MenuInfoADD()
        {
            InitializeComponent();
        }
        public MainFrom mf;
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {
            mf = new MainFrom();
            InitMenu();//绑定菜单
            Menu_Dictionary_ID();//绑定菜单状态
            Menu_ControlType();//绑定菜单类型


           

        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// 
        protected void InitMenu()
        {
            //加载TreeView菜单           
            LoadNode(TV_MenuInfo.Nodes, "0");

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

                ////dv["Menu_ID"].ToString();
                if (m.Menu_ControlText != null)
                {
                    nodeTemp.Text = m.Menu_ControlText;
                }
                //dv["Menu_ControlText"].ToString();  //节点名称 
                if (nodeTemp != null)
                {
                    node.Add(nodeTemp);  //加入节点 
                }
                this.LoadNode(nodeTemp.Nodes, nodeTemp.Tag.ToString().Split(',')[0]);  //递归

            }

        }
        /// <summary>
        /// 绑定菜单状态
        /// </summary>
        private void Menu_Dictionary_ID()
        {
            cob_Menu_Dictionary_ID.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
            cob_Menu_Dictionary_ID.DisplayMember = "Dictionary_Name";
            cob_Menu_Dictionary_ID.ValueMember = "Dictionary_ID";
            if (cob_Menu_Dictionary_ID.DataSource!=null)
            {
                cob_Menu_Dictionary_ID.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// 绑定菜单类型
        /// </summary>
        private void Menu_ControlType()
        {
            cob_Menu_ControlType.DataSource = MenuTypeDAL.Query();
            cob_Menu_ControlType.DisplayMember = "MenuType_Name";
            cob_Menu_ControlType.ValueMember = "MenuType_ID";
            if (cob_Menu_ControlType.DataSource != null)
            {
                cob_Menu_ControlType.SelectedIndex = -1;
            }
        }
        private void MenuInfoADD_Load(object sender, EventArgs e)
        {
            InitUser();
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (btn_Add.Enabled)
           {
               btn_Add.Enabled = false;
               btn_Add_Click();
               btn_Add.Enabled = true;
           }
        }
        /// <summary>
        /// 添加菜单方法
        /// </summary>
        private void btn_Add_Click()
        {
            try
            {
                if (Common.GetInt(TV_MenuInfo.SelectedNode.Tag.ToString())<0)
                {
                    MessageBox.Show("请选中节点","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(cob_Menu_Dictionary_ID.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "状态不能为空！", cob_Menu_Dictionary_ID, this);
                    return;
                }
                if (string.IsNullOrEmpty(cob_Menu_ControlType.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "控件类型不能为空！", cob_Menu_ControlType, this);
                    return;
                }
                //if (string.IsNullOrEmpty(txt_Menu_ControlText.Text))
                //{
                //    mf.ShowToolTip(ToolTipIcon.Info, "提示", "菜单名称不能为空！", txt_Menu_ControlText, this);
                //    return;
                //}
                if (string.IsNullOrEmpty(cob_Menu_Visible.Text))
                {

                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "是否可见不能为空！", cob_Menu_Visible, this);
                    return;
                }
                if (string.IsNullOrEmpty(cob_Menu_Enabled.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "是否可见不能为空！", cob_Menu_Enabled, this);
                    return;
                }
                var MenuInfoAdd = new MenuInfo
                {
                    Menu_Dictionary_ID = Common.GetInt(cob_Menu_Dictionary_ID.SelectedValue.ToString()),//状态
                    Menu_OtherID=Common.GetInt(TV_MenuInfo.SelectedNode.Tag.ToString()),//父级ID
                  

                };
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MenuInfoADD.btn_Add_Click()" + ex.Message.ToString());
            }
            finally
            {
                InitMenu();//重新绑定控件
            }


        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Update_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
          

        }
        private void btn_Delete_Click()
        {
            if (Common.GetInt(TV_MenuInfo.SelectedNode.Tag.ToString()) > 0)
            {
                Expression<Func<MenuInfo, bool>> fun = n => n.Menu_ID == Common.GetInt(TV_MenuInfo.SelectedNode.Tag.ToString());
                //if()
                //{
                
                //}
            }
        }

     
      
    }
}
