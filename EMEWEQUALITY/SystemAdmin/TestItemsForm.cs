using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//导入应用包
using EMEWEQUALITY.HelpClass;
using EMEWEEntity;
using EMEWEDAL;
using EMEWE.Common;
using System.Linq.Expressions;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class TestItemsForm : Form
    {
        public TestItemsForm()
        {
            InitializeComponent();
        }

        public MainFrom mf;//主窗体
        /// <summary>
        /// 返回一个空的预置皮重
        /// </summary>
        Expression<Func<View_TestItems_Dictionary, bool>> expr = null;
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();

        #region 加载显示
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestItemsForm_Load(object sender, EventArgs e)
        {
            InitTestItems();
        }
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitTestItems()
        {
            this.lvwTestItems.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            LoadData();
            btnUpUser.Visible = false;
            btnUserCancle.Visible = false;
            BindcbxTestItemsState();//绑定质检状态
            BindcbxTestItemsSeachState(); //绑定质检状态
            BindcbxTestItemsID();//绑定检测项目


            mf = new MainFrom();
        }
        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                this.lvwTestItems.DataSource = null;
                lvwTestItems.DataSource = page.BindBoundControl<View_TestItems_Dictionary>("", txtCurrentPage2, lblPageCount2, expr);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("日记管理 LoadData()" + ex.Message.ToString());
            }
        }
        #endregion

        #region 绑定下拉框的值
        /// <summary>
        /// 绑定检测项目上级（水分检测和重量检测）
        /// </summary>
        private void BindcbxTestItemsID()
        {
            cbxTes_TestItems_ID.DataSource = TestItemsDAL.GetListWhereTestItemName("", "启动");
            cbxTes_TestItems_ID.DisplayMember = "TestItems_NAME";
            cbxTes_TestItems_ID.ValueMember = "TestItems_ID";
            if (cbxTes_TestItems_ID.DataSource != null)
            {
                cbxTes_TestItems_ID.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 绑定操作界面检测项目的值
        /// </summary>
        private void BindcbxTestItemsState()
        {
            // TestItems_Dictionary_ID 检测项目的状态（操作界面绑定）
            this.cbxTestItemsState.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
            this.cbxTestItemsState.DisplayMember = "Dictionary_Name";
            cbxTestItemsState.ValueMember = "Dictionary_ID";
            if (cbxTestItemsState.DataSource != null)
            {

                cbxTestItemsState.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 绑定搜索按钮检测项目的值
        /// </summary>
        private void BindcbxTestItemsSeachState()
        {
            // TestItems_Dictionary_ID 检测项目的状态（搜索按钮）
            List<Dictionary> listDic = DictionaryDAL.GetValueStateDictionary("状态");
            this.cbxSeachState.DataSource = listDic;
            this.cbxSeachState.DisplayMember = "Dictionary_Name";
            cbxSeachState.ValueMember = "Dictionary_ID";
            if (cbxSeachState.DataSource != null)
            {

                cbxSeachState.SelectedIndex = 0;
            }
        }
        #endregion

        /// <summary>
        /// 查重方法
        /// </summary>
        /// <returns></returns>
        private bool btnCheck()
        {
            bool rbool = true;
            try
            {
                //定义一个字段用以保存项目检测名称
                var TestItemsName = this.txtTestItems_Name.Text.Trim();
                //判断名称是否为空
                if (TestItemsName == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "检测项目名称不能为空！", txtTestItems_Name, this);
                    txtTestItems_Name.Text = "";
                    txtTestItems_Name.Focus();
                    rbool = false;
                }
                Expression<Func<View_TestItems_Dictionary, bool>> funview_TestItemsinfo = n => n.TestItems_NAME == TestItemsName;
                if (TestItemsDAL.Query(funview_TestItemsinfo).Count() > 0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "该检测项目名称已存在", txtTestItems_Name, this);
                    txtTestItems_Name.Focus();
                    rbool = false; ;
                }
                return rbool;

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("项目检测管理 btnCheck()" + ex.Message.ToString());
                rbool = false;
            }
            return rbool;
        }
        /// <summary>
        /// “查重”按钮的单击事件（查询是否存在重复的项目检测名称）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Check_Click(object sender, EventArgs e)
        {
            if (btnCheck())
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "该项目检测名可用", txtTestItems_Name, this);
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
                if (this.txtTestItems_Count.Text == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "检测项目次数不能为空！", txtTestItems_Count, this);
                    return;
                }
                if (Convert.ToDouble(this.txtTestItems_Count.Text) == 0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "检测项目次数不能为零！", txtTestItems_Count, this);
                    return;
                }
                //ValidateTextBox();//调用验证文本框方法
                if (!btnCheck()) return;//去重
                var TestItemsadd = new TestItems
                {
                    TestItems_NAME = this.txtTestItems_Name.Text.Trim(),
                    //Tes_TestItems_ID = Convert.ToInt32(this.txtTes_TestItems_ID.Text.Trim()),
                    Tes_TestItems_ID = Convert.ToInt32(this.cbxTes_TestItems_ID.SelectedValue),
                    TestItems_Dictionary_ID = Convert.ToInt32(this.cbxTestItemsState.SelectedValue),
                    TestItems_COUNT = Convert.ToInt32(this.txtTestItems_Count.Text.Trim()),
                    TestItems_REMARK = this.txtTestItems_Remark.Text.Trim()
                };

                if (TestItemsDAL.InsertOneQCRecord(TestItemsadd))
                {
                    MessageBox.Show("添加成功", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加失败", "提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                string strContent = "检测项目名称为：" + this.txtTestItems_Name.Text.Trim(); ;
                LogInfoDAL.loginfoadd("新增", "新增 " + strContent + " 的信息", Common.USERNAME);//添加日志

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("项目检测管理 btnAdd_Click()" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTestItems_Count.Text == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "检测项目次数不能为空！", txtTestItems_Count, this);
                    return;
                }
                if (this.txtTestItems_Name.Text == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "检测项目名称不能为空！", txtTestItems_Count, this);
                    return;
                }
                if (Convert.ToDouble(this.txtTestItems_Count.Text) == 0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "检测项目次数不能为零！", txtTestItems_Count, this);
                    return;
                }
                if (this.cbxTestItemsState.SelectedIndex > -1)
                {
                    Expression<Func<TestItems, bool>> p = n => n.TestItems_NAME == this.lvwTestItems.SelectedRows[0].Cells["TestItems_NAME"].Value.ToString();
                    Action<TestItems> ap = s =>
                    {
                        s.TestItems_NAME = this.txtTestItems_Name.Text.Trim();
                        //s.Tes_TestItems_ID = Convert.ToInt32(this.txtTes_TestItems_ID.Text.Trim());
                        s.Tes_TestItems_ID = Convert.ToInt32(this.cbxTes_TestItems_ID.SelectedValue);
                        s.TestItems_Dictionary_ID = Convert.ToInt32(this.cbxTestItemsState.SelectedValue);
                        s.TestItems_COUNT = Convert.ToInt32(this.txtTestItems_Count.Text.Trim());
                        s.TestItems_REMARK = this.txtTestItems_Remark.Text.Trim();
                    };

                    if (TestItemsDAL.Update(p, ap))
                    {
                        MessageBox.Show("修改成功", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("修改失败", "提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    string strContent = "检测项目名称为：" + this.txtTestItems_Name.Text.Trim(); ;
                    LogInfoDAL.loginfoadd("修改", "修改 " + strContent + " 的信息", Common.USERNAME);//添加日志
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("项目检测管理 bntUpUser_Click()" + ex.Message.ToString());
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
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserCancle_Click(object sender, EventArgs e)
        {
            ShowAddButton();
        }
        /// <summary>
        /// 设置文本框内的值为空(清空文本框的方法)
        /// </summary>
        private void ShowAddButton()
        {
            btnAdd.Visible = true;
            btnUpUser.Visible = false;
            btnUserCancle.Visible = false;
            this.txtTestItems_Name.Text = "";
            this.cbxTes_TestItems_ID.SelectedIndex = 1;
            this.cbxTestItemsState.SelectedIndex = 1;
            this.txtTestItems_Count.Text = "";
            this.txtTestItems_Remark.Text = "";
        }

        /// <summary>
        /// 搜索
        /// </summary>
        private void selectTJ()
        {
            try
            {

                if (expr == null)
                {
                    expr = (Expression<Func<View_TestItems_Dictionary, bool>>)PredicateExtensionses.True<View_TestItems_Dictionary>();
                }
                int i = 0;
                if (this.cbxSeachState.SelectedValue != null)//项目检测状态
                {
                    int stateID = Converter.ToInt(cbxSeachState.SelectedValue.ToString());
                    if (stateID > 0)
                    {
                        expr = expr.And(n => n.TestItems_Dictionary_ID == Converter.ToInt(cbxSeachState.SelectedValue.ToString()));
                        i++;
                    }
                }
                if (this.txtName.Text != "")//项目检测名称
                {
                    expr = expr.And(n => n.TestItems_NAME.Contains(txtName.Text));

                    i++;
                }
                if (i == 0)
                {
                    expr = null;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("项目检测管理 selectTJ()" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                LoadData();
            }
        }
        /// <summary>
        /// “搜索”按钮的单击事件（按条件查询）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Enabled)
            {
                btnSearch.Enabled = false;
                selectTJ();
                //LogInfoDAL.loginfoadd("查询","检测项目查询",Common.USERNAME);//操作日志
                btnSearch.Enabled = true;
            }
        }

        #region 是否全选
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lvwTestItems.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)dgvRoleList.Rows[i].Cells[0]).Value = false;
                lvwTestItems.Rows[i].Selected = false;
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvwTestItems.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)lvwUserList.Rows[i].Cells[0]).Value = true;
                lvwTestItems.Rows[i].Selected = true;
            }
        }
        #endregion

        #region 行选择 删除预置皮重信息  查看修改信息
        /// <summary>
        ///删除项目检测信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbtnDelUser_delete()
        {
            try
            {
                int j = 0;
                if (lvwTestItems.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //选中数量
                        int count = lvwTestItems.SelectedRows.Count;
                        string id = "";
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            Expression<Func<TestItems, bool>> funuserinfo = n => n.TestItems_ID == Convert.ToInt32(this.lvwTestItems.SelectedRows[i].Cells["TestItems_ID"].Value.ToString());

                            if (!TestItemsDAL.DeleteToMany(funuserinfo))
                            {
                                j++;
                            }
                        }
                        if (j == 0)
                        {
                            MessageBox.Show("成功删除", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("删除失败", "提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                        string strContent = "检测项目编号为：" + this.lvwTestItems.SelectedRows[0].Cells["TestItems_ID"].Value.ToString();
                        LogInfoDAL.loginfoadd("删除", "删除 " + strContent + " 的信息", Common.USERNAME);//添加日志
                    }
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要删除的行！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("项目检测管理 tbtnDelUser_delete()+" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();//更新
            }
        }
        /// <summary>
        /// 查看修改信息
        /// </summary>
        private void tsbtnUpdateSelect()
        {
            if (this.lvwTestItems.SelectedRows.Count > 0)//选中行
            {
                if (lvwTestItems.SelectedRows.Count > 1)
                {
                    MessageBox.Show("修改只能选中一行！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    //修改的值
                    this.txtTestItems_Name.Text = lvwTestItems.SelectedRows[0].Cells["TestItems_NAME"].Value.ToString();
                    Expression<Func<View_TestItems_Dictionary, bool>> funviewinto = n => n.TestItems_NAME == this.txtTestItems_Name.Text;
                    foreach (var n in TestItemsDAL.Query(funviewinto))
                    {

                        if (n.TestItems_NAME != null)
                        {
                            //项目检测名称
                            this.txtTestItems_Name.Text = n.TestItems_NAME;
                        }
                        if (n.Tes_TestItems_ID != null)
                        {
                            //项目检测上级
                            //this.txtTes_TestItems_ID.Text = n.Tes_TestItems_ID.ToString();
                            this.cbxTes_TestItems_ID.SelectedValue = n.Tes_TestItems_ID;
                        }
                        if (n.TestItems_Dictionary_ID != null)
                        {
                            //项目检测状态
                            this.cbxTestItemsState.SelectedValue = n.TestItems_Dictionary_ID;
                        }
                        if (n.TestItems_COUNT != null)
                        {
                            //项目检测次数
                            this.txtTestItems_Count.Text = n.TestItems_COUNT.ToString();
                        }
                        if (n.TestItems_REMARK != null)
                        {
                            //项目检测备注
                            this.txtTestItems_Remark.Text = n.TestItems_REMARK;
                        }
                        break;
                    }
                    //隐藏添加按钮
                    btnAdd.Visible = false;
                    btnUpUser.Visible = true;
                    btnUserCancle.Visible = true;

                }
            }
            else
            {
                MessageBox.Show("请选择要修改的行！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        #endregion

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
        /// <summary>
        /// 分页菜单响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsbtnUpdate")//修改
            {
                tsbtnUpdateSelect();
                return;
            }
            if (e.ClickedItem.Name == "tsbtnDel")//删除
            {
                tbtnDelUser_delete();
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

            this.lvwTestItems.DataSource = page.BindBoundControl<View_TestItems_Dictionary>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr);
        }

        #region DataGridView的数据操作事件
        private void tsbtnDel_Click(object sender, EventArgs e)
        {

        }
        private void tsbtnUpdate_Click(object sender, EventArgs e)
        {

        }
        private void tslNotSelect_Click()
        {

        }
        private void tslSelectAll_Click()
        {

        }
        #endregion

        /// <summary>
        /// 在控件具有焦点并且用户按下并释放某个键后发生(只能输入数字)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTestItems_Count_KeyPress(object sender, KeyPressEventArgs e)
        {
            //实例化出预置皮重文本框的当前值
            TextBox txt = (TextBox)sender;

            //只能输入数字  || e.KeyChar == (char)8  || e.KeyChar == (char)47
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "请正确输入检测项目的次数！", txt, this);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

    }
}
