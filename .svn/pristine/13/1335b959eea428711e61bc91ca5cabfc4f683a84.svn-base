using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EMEWEEntity;
using EMEWEDAL;
using EMEWEQUALITY.HelpClass;
using System.Linq.Expressions;

namespace EMEWEQUALITY.SystemAdmin
{
    partial class MenuInfoManager : Form
    {
        public MenuInfoManager()
        {
            InitializeComponent();
        }
        public MainFrom mf;//主窗体
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitMenu()
        {
            LoadData();//加载数据
            mf = new MainFrom();
        }
        /// <summary>
        /// 加载菜单结构列表
        /// </summary>
        private void LoadData()
        {
            try
            {
                if (btnADD.Visible==false)
                {
                       this.tvMenuList.Visible = false;
                 }
                else
                {
                    
                    MenuInfoLoad();//绑定TreeView数据
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("菜单管理 LoadData()" + ex.Message.ToString());
            }

        }
        #region 程序集属性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        #region 唐磊
        /// <summary>
        /// 加载菜单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuInfoManager_Load(object sender, EventArgs e)
        {
            mf = new MainFrom();//初始化
            InitMenu();//加载菜单信息
            DictionaryDataBind();//绑定状态
            MenuTypeDataBind();//绑定类型
        }
        /// <summary>
        /// 绑定TreeView数据
        /// </summary>
        private void MenuInfoLoad()
        {
            tvMenuList.Nodes.Add("0", "------根").Tag=0;
            MenuInfoDataBind(tvMenuList.Nodes, "0");
        }
        /// <summary>
        /// 绑定控件类型
        /// </summary>
        private void MenuTypeDataBind()
        {

            cbControlType.DataSource = MenuTypeDAL.Query();
            cbControlType.DisplayMember = "MenuType_Name";
            cbControlType.ValueMember = "MenuType_ID";
            if (cbControlType.DataSource != null)
            {
                cbControlType.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// 绑定菜单状态
        /// </summary>
        private void DictionaryDataBind()
        {
            cbDictionary.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
            cbDictionary.DisplayMember = "Dictionary_Name";
            cbDictionary.ValueMember = "Dictionary_ID";
            if (cbDictionary.DataSource != null)
            {
                cbDictionary.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// 绑定TreeView
        /// </summary>
        private void MenuInfoDataBind(TreeNodeCollection node, string otherId)
        {
            
            //得到数据 过滤父节点
            Expression<Func<MenuInfo, bool>> menuinfo = n => n.Menu_OtherID.ToString() == otherId;//过滤父节点 
            var p = MenuInfoDAL.Query(menuinfo);
            TreeNode nodes;
            foreach (var m in p)
            {
                nodes = new TreeNode();
                nodes.Tag = m.Menu_ID;
                if (m.Menu_ControlText != null)
                {
                    nodes.Text = m.Menu_ControlText;//设置显示数据
                }
                if (nodes != null)
                {
                    node.Add(nodes);  //加入节点 
                }
                this.MenuInfoDataBind(nodes.Nodes, nodes.Tag.ToString().Split(',')[0]);  //递归
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnADD_Click(object sender, EventArgs e)
        {
            if (btnADD.Text=="添加")
            {
                AddMenuInfo();//添加菜单
            }
            else
            {
                UpdateMenuInfo();//修改菜单信息
            } 
        }
        /// <summary>
        /// 修改菜单信息
        /// </summary>
        private void UpdateMenuInfo()
        {
            try
            {
                if (Common.GetInt(tvMenuList.SelectedNode.Tag.ToString()) < 0)
                {
                    MessageBox.Show("请选中节点 ", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(cbDictionary.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "请选择菜单状态！", cbDictionary, this);
                    return;
                }
                if (string.IsNullOrEmpty(cbControlType.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "请选择类型！", cbControlType, this);
                    return;
                }
                if (string.IsNullOrEmpty(cbVisble.Text))
                {

                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "请选择是否可见！", cbVisble, this);
                    return;
                }
                if (string.IsNullOrEmpty(cbEnabled.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "请选择是否可编辑！", cbEnabled, this);
                    return;
                }
                bool enabled = false;
                bool Visible = false;
                if (cbEnabled.Text == "是")
                {
                    enabled = true;
                }
                if (cbVisble.Text == "是")
                {
                    Visible = true;
                }
                int nodes = Common.GetInt(tvMenuList.SelectedNode.Tag.ToString());
                Expression<Func<MenuInfo, bool>> fun = n => n.Menu_ID == Common.GetInt(tvMenuList.SelectedNode.Tag.ToString());
                Action<MenuInfo> action = null;
                if (cbControlType.SelectedIndex >= 5)
                {
                    if (string.IsNullOrEmpty(tbControlText.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入控件中文名称", tbControlText, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(tbControlName.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入控件名称", tbControlText, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(tbFromName.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入菜单名称", tbControlText, this);
                        return;
                    }
                   action =m=>
                    {
                        m.Menu_Dictionary_ID = Common.GetInt(cbDictionary.SelectedIndex.ToString())+2;//状态
                        m.Menu_ControlType = Common.GetStr(cbControlType.SelectedIndex.ToString())+1;//类型
                        m.Menu_ControlName = tbControlName.Text;//控件名称
                        m.Menu_ControlText = tbControlText.Text;//控件中文名称
                        m.Menu_Enabled = enabled;//是否可用
                        m.Menu_Visible = Visible;//是否可见
                        m.Menu_FromName = tbFromName.Text;//菜单名称
                        m.Menu_FromText = tbFromText.Text;//菜单中文名称
                    };
                }
                else
                {
                    if (string.IsNullOrEmpty(tbFromText.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入菜单中文名称", tbControlText, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(tbFromName.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入菜单名称", tbControlText, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(tbControlText.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入控件中文名称", tbControlText, this);
                        return;
                    }
                    action = m =>
                    {
                        m.Menu_Dictionary_ID = Common.GetInt(cbDictionary.SelectedIndex.ToString()) + 2;//状态
                        m.Menu_MenuType_ID = Common.GetInt(cbControlType.SelectedIndex.ToString()) + 1;//类型s
                        m.Menu_ControlName = tbControlName.Text;//控件名称
                        m.Menu_ControlText = tbControlText.Text;//控件中文名称
                        m.Menu_Enabled = enabled;//是否可用
                        m.Menu_Visible = Visible;//是否可见
                        m.Menu_FromName = tbFromName.Text;//菜单名称
                        m.Menu_FromText = tbFromText.Text;//菜单中文名称
                    };
                }
                if (!MenuInfoDAL.Update(fun,action))//是否修改失败
                {
                    MessageBox.Show(" 修改失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("修改成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("UpdateMenuInfo()" + ex.Message.ToString());//记录异常信息
            }
            finally
            {
                tvMenuList.Nodes.Clear();
                MenuInfoLoad();//重新加载数据
            }
        }
        /// <summary>
        /// 添加菜单信息
        /// </summary>
        private void AddMenuInfo()
        {
            try
            {
                if (Common.GetInt(tvMenuList.SelectedNode.Tag.ToString()) < 0)
                {
                    MessageBox.Show("请选中节点 ", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(cbDictionary.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "请选择菜单状态！", cbDictionary, this);
                    return;
                }
                if (string.IsNullOrEmpty(cbVisble.Text))
                {

                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "请选择是否可见！", cbVisble, this);
                    return;
                }
                if (string.IsNullOrEmpty(cbEnabled.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "请选择是否可编辑！", cbEnabled, this);
                    return;
                }
                if (string.IsNullOrEmpty(cbControlType.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "请选择类型！", cbControlType, this);
                    return;
                }
                bool enabled = true;
                bool Visible = true;
                if (cbEnabled.Text == "否")
                {
                    enabled = false;
                }
                if (cbVisble.Text == "否")
                {
                    Visible = false;
                }
                int nodes = Common.GetInt(tvMenuList.SelectedNode.Tag.ToString());
                MenuInfo m = new MenuInfo();
                if (cbControlType.SelectedIndex>=5)
                {
                    if (string.IsNullOrEmpty(tbControlText.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入控件中文名称", tbControlText, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(tbControlName.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入控件名称", tbControlText, this);
                        return;
                    }
                    m.Menu_Dictionary_ID = Common.GetInt(cbDictionary.SelectedValue.ToString());//状态
                   m.Menu_OtherID = nodes;//父级ID
                    m.Menu_ControlType = Common.GetStr(cbControlType.SelectedValue.ToString());//控件类型
                    m.Menu_ControlName = tbControlName.Text;//控件名称
                   m.Menu_ControlText = tbControlText.Text;//控件中文名称
                    m.Menu_Enabled = enabled;//是否可用
                    m.Menu_Visible = Visible;//是否可见
                    m.Menu_FromName = tbFromName.Text;//菜单名称
                    m.Menu_FromText = tbFromText.Text;//菜单中文名称
                }
                else
                {
                    if (string.IsNullOrEmpty(tbFromText.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入菜单中文名称", tbControlText, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(tbFromName.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入菜单名称", tbControlText, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(tbControlText.Text))
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "请输入控件中文名称", tbControlText, this);
                        return;
                    }
                    m.Menu_Dictionary_ID = Common.GetInt(cbDictionary.SelectedValue.ToString());//状态
                    m.Menu_OtherID = nodes;//父级ID
                    m.Menu_MenuType_ID = Common.GetInt(cbControlType.SelectedValue.ToString());//菜单类型
                    m.Menu_ControlName = tbControlName.Text;//控件名称
                    m.Menu_ControlText = tbControlText.Text;//控件中文名称
                    m.Menu_Enabled = enabled;//是否可用
                    m.Menu_Visible = Visible;//是否可见
                    m.Menu_FromName = tbFromName.Text;//菜单名称
                    m.Menu_FromText = tbFromText.Text;//菜单中文名称
                }
                if (!MenuInfoDAL.InsertMenuInfo(m))//是否修改失败
                {
                    MessageBox.Show(" 添加失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("AddMenuInfo()" + ex.Message.ToString());//记录异常信息
            }
            finally
            {
                tvMenuList.Nodes.Clear();
                MenuInfoLoad();//重新加载数据
            }
        }

        private void tvMenuList_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        /// <summary>
        /// 节点的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>emr
        private void tvMenuList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }
        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMenu_Click(object sender, EventArgs e)
        {
            if (Common.GetInt(tvMenuList.SelectedNode.Tag.ToString()) < 0)
            {
                MessageBox.Show("请选中节点", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                gbManager.Text = "添加菜单信息";
                btnADD.Text = "添加";
                DictionaryDataBind();//绑定菜单状态
                MenuTypeDataBind();//绑定控件类型
            }
        }
        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateMenu_Click(object sender, EventArgs e)
        {
            if (Common.GetInt(tvMenuList.SelectedNode.Tag.ToString())<0)
            {
                MessageBox.Show("请选中节点", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                gbManager.Text = "修改菜单信息";
                btnADD.Text = "修改";
                Expression<Func<MenuInfo, bool>> fun = n => n.Menu_ID == Common.GetInt(tvMenuList.SelectedNode.Tag.ToString());
                MenuInfo menuinfo= MenuInfoDAL.Single(fun);
                DictionaryDataBind();//绑定菜单状态
                MenuTypeDataBind();//绑定控件类型
                if (menuinfo.Menu_Dictionary_ID!=null)//菜单状态
                {
                    cbDictionary.SelectedIndex = Common.GetInt(menuinfo.Menu_Dictionary_ID) - 2;
                }
                if (menuinfo.Menu_ControlType!=null)//类型
                {
                        cbControlType.SelectedIndex =Common.GetInt( menuinfo.Menu_ControlType)-1;
                }
                tbControlName.Text = menuinfo.Menu_ControlName;//菜单控件名称
                tbControlText.Text = menuinfo.Menu_ControlText;//菜单控件中文名称
                tbFromName.Text = menuinfo.Menu_FromName;//窗体名称
                tbFromText.Text = menuinfo.Menu_FromText;//窗体中文名称
                if (menuinfo.Menu_Visible==false)//菜单是否可见
                {
                    cbVisble.SelectedIndex = 1;
                }
                else
                {
                    cbVisble.SelectedIndex = 0;
                }
                if (menuinfo.Menu_Enabled==true)//菜单是否可编辑
                {
                    cbEnabled.SelectedIndex = 0;
                }
                else
                {
                    cbEnabled.SelectedIndex = 1;
                }
                selectChange();
            }
        }
        /// <summary>
        /// 删除未使用的菜单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteMenu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (Common.GetInt(tvMenuList.SelectedNode.Tag.ToString()) <= 0)
                {
                    MessageBox.Show("请选中节点", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Expression<Func<MenuInfo, bool>> sfun = n => n.Menu_OtherID == Common.GetInt(tvMenuList.SelectedNode.Tag.ToString()); 
                    if (MenuInfoDAL.Query(sfun).Count()!=0)
                    {
                        if (MessageBox.Show("节点下还有子节点，确定删除？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            Expression<Func<MenuInfo, bool>> fun = n => n.Menu_ID == Common.GetInt(tvMenuList.SelectedNode.Tag.ToString());
                            if (MenuInfoDAL.DeleteToMany(fun) && MenuInfoDAL.DeleteToMany(sfun))
                            {
                                MessageBox.Show("删除成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tvMenuList.Nodes.Clear();
                                MenuInfoLoad();//重新加载数据
                                return;
                            }
                            else
                            {
                                MessageBox.Show("删除失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tvMenuList.Nodes.Clear();
                                MenuInfoLoad();//重新加载数据
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        Expression<Func<MenuInfo, bool>> fun = n => n.Menu_ID == Common.GetInt(tvMenuList.SelectedNode.Tag.ToString());
                        if (MenuInfoDAL.DeleteToMany(fun))
                        {
                            MessageBox.Show("删除成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tvMenuList.Nodes.Clear();
                            MenuInfoLoad();//重新加载数据
                            return;
                        }
                        else
                        {
                            MessageBox.Show("删除失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tvMenuList.Nodes.Clear();
                            MenuInfoLoad();//重新加载数据
                            return;
                        }
                    }
                   
                }
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        /// <summary>
        /// 下拉列表值改变触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbControlType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectChange();
        }
        /// <summary>
        /// 下拉列表的值改变
        /// </summary>
        private void selectChange()
        {
        }
    }
}
