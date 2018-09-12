using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//导入引用
using EMEWEQUALITY.HelpClass;
using EMEWEEntity;
using EMEWEDAL;
using EMEWE.Common;
using System.Data.Linq;
using System.Linq.Expressions;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class PresetTareForm : Form
    {
        public PresetTareForm()
        {
            InitializeComponent();
        }

        public MainFrom mf;//主窗体
        /// <summary>
        /// 返回一个空的预置皮重
        /// </summary>
        Expression<Func<View_PresetTare_Dictionary, bool>> expr = null;
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();

        #region 加载显示预置皮重
        /// <summary>
        /// 预置皮重的加载显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetTareForm_Load(object sender, EventArgs e)
        {
            InitPresetTrade();
        }

        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitPresetTrade()
        {
            this.lvwPresetTare.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            LoadData();
            btnUpUser.Visible = false;
            btnUserCancle.Visible = false;
            BindCboxState();//绑定添加或修改时显示状态
            BindCboxSearchState();//绑定搜索时显示状态
        }
        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {

                this.lvwPresetTare.DataSource = null;
                lvwPresetTare.DataSource = page.BindBoundControl<View_PresetTare_Dictionary>("", txtCurrentPage2, lblPageCount2, expr, "PresetTare_ID desc");
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("日记管理 LoadData()" + ex.Message.ToString());
            }

        }
        #endregion

        #region 绑定预置皮重状态
        /// <summary>
        /// 绑定预置皮重状态（添加或修改时显示状态）
        /// </summary>
        private void BindCboxState()
        {
            //PresetTare_Dictionary_ID 预置皮重的状态字段
            this.cbxPresetTareState.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
            this.cbxPresetTareState.DisplayMember = "Dictionary_Name";
            cbxPresetTareState.ValueMember = "Dictionary_ID";
            if (cbxPresetTareState.DataSource != null)
            {
                cbxPresetTareState.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 绑定预置皮重状态（搜索按钮的状态）
        /// </summary>
        private void BindCboxSearchState()
        {
            this.cbxSeachState.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
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
                //定义一个字段用以保存预置皮重名称
                var PresetTradeName = this.txtPresetTare_Name.Text.Trim();
                //判断名称是否为空
                if (PresetTradeName == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重名称不能为空！", txtPresetTare_Name, this);
                    txtPresetTare_Name.Text = "";
                    txtPresetTare_Name.Focus();
                    rbool = false;
                }
                Expression<Func<View_PresetTare_Dictionary, bool>> funview_PresetTredeinfo = n => n.PresetTare_NAME == PresetTradeName;
                if (PresetTareDAL.Query(funview_PresetTredeinfo).Count() > 0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "该预置皮重名称已存在", txtPresetTare_Name, this);
                    txtPresetTare_Name.Focus();
                    rbool = false; ;
                }
                return rbool;

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("用户管理 btnCheck()" + ex.Message.ToString());
                rbool = false;
            }
            return rbool;
        }
        /// <summary>
        /// “查重”按钮的单击事件（查询是否存在重复的预置皮重名称）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Check_Click(object sender, EventArgs e)
        {
            if (btnCheck())
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "该用户名可用", txtPresetTare_Name, this);
            }
        }

        /// <summary>
        /// “添加”按钮的单击事件（添加预置皮重信息）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtPresetTare_Weigh.Text == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重不能为空！", txtPresetTare_Weigh, this);
                    return;
                }
                //if (Convert.ToDouble(this.txtPresetTare_Weigh.Text) == 0)
                //{
                //    mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重不能为零！", txtPresetTare_Weigh, this);
                //    return;
                //}
                if (selectIsHave(txtPresetTare_Name.Text) == false)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重名称重复！", txtPresetTare_Weigh, this);
                    return;
                }
                //ValidateTextBox();//调用验证文本框方法
                if (!btnCheck()) return;//去重
                var presetTradeadd = new PresetTare
                {
                    PresetTare_Dictionary_ID = Convert.ToInt32(this.cbxPresetTareState.SelectedValue),
                    PresetTare_NAME = this.txtPresetTare_Name.Text.Trim(),
                    PresetTare_WEIGH = Convert.ToDecimal(this.txtPresetTare_Weigh.Text.Trim()),
                    PresetTare_REMARK = this.txtPresetTare_Remark.Text.Trim()
                };

                if (txtPresetTare_Weigh.Text.Substring(0, 1) == ".")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "第一位不能是小数点！", txtPresetTare_Weigh, this);
                }
                else
                {
                    if (PresetTareDAL.InsertOneQCRecord(presetTradeadd))
                    {
                        MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "不能同时输入两位以上的小数点(包含两位)！", txtPresetTare_Weigh, this);
                    }
                }


                string strContent = "预置皮重名称为：" + this.txtPresetTare_Name.Text.Trim();
                Common.WriteLogData("新增", "新增 " + strContent + " 的信息", Common.USERNAME);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("预置皮重管理 btnAdd_Click()" + ex.Message.ToString());
                // mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重输入格式错误！", txtPresetTare_Weigh, this);
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }

        string UpdateName = "";
        /// <summary>
        /// 查重
        /// </summary>
        private bool selectIsHave(string PresetTareNAME)
        {

            DataTable dt = LinQBaseDao.Query("select * from PresetTare where PresetTare_NAME = '" + PresetTareNAME + "' and PresetTare_NAME!='" + UpdateName + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// “修改”按钮的单击事件（修改预置皮重信息）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtPresetTare_Weigh.Text == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重不能为空！", txtPresetTare_Weigh, this);
                    return;
                }
                if (this.txtPresetTare_Name.Text == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重名称不能为空！", txtPresetTare_Weigh, this);
                    return;
                }
                //if (Convert.ToDouble(this.txtPresetTare_Weigh.Text) == 0)
                //{
                //    mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重不能为零！", txtPresetTare_Weigh, this);
                //    return;
                //}

                if (selectIsHave(txtPresetTare_Name.Text) == false)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "预置皮重名称重复！", txtPresetTare_Weigh, this);
                    return;
                }
                if (cbxPresetTareState.SelectedIndex > -1)
                {
                    Expression<Func<PresetTare, bool>> p = n => n.PresetTare_NAME == lvwPresetTare.SelectedRows[0].Cells["PresetTare_Name"].Value.ToString();
                    Action<PresetTare> ap = s =>
                    {
                        s.PresetTare_Dictionary_ID = Convert.ToInt32(this.cbxPresetTareState.SelectedValue);
                        s.PresetTare_NAME = this.txtPresetTare_Name.Text.Trim();
                        s.PresetTare_WEIGH = Convert.ToDecimal(this.txtPresetTare_Weigh.Text.Trim());
                        s.PresetTare_REMARK = this.txtPresetTare_Remark.Text.Trim();
                    };

                    if (txtPresetTare_Weigh.Text.Substring(0, 1) == ".")
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "第一位不能是小数点！", txtPresetTare_Weigh, this);
                        this.txtPresetTare_Weigh.Text = "";
                    }
                    else
                    {
                        if (PresetTareDAL.Update(p, ap))
                        {
                            MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            mf.ShowToolTip(ToolTipIcon.Info, "提示", "不能同时输入两位以上的小数点(包含两位)！", txtPresetTare_Weigh, this);
                        }
                    }
                    string strContent = "预置皮重名称为：" + this.txtPresetTare_Name.Text.Trim();
                    Common.WriteLogData("修改", "修改 " + strContent + " 的信息", Common.USERNAME);//添加日志
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("预置皮重管理 bntUpUser_Click()" + ex.Message.ToString());
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
        /// “取消”按钮的单击事件（取消修改当前预置皮重信息）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserCancle_Click(object sender, EventArgs e)
        {
            ShowAddButton();//调用函数
        }
        /// <summary>
        /// 设置文本框内的值为空
        /// </summary>
        private void ShowAddButton()
        {
            btnAdd.Visible = true;
            btnUpUser.Visible = false;
            btnUserCancle.Visible = false;
            this.cbxPresetTareState.SelectedIndex = 1;
            this.txtPresetTare_Name.Text = "";
            this.txtPresetTare_Weigh.Text = "";
            this.txtPresetTare_Remark.Text = "";
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
                    //expr = PredicateExtensionses.True<PresetTare>();
                    expr = (Expression<Func<View_PresetTare_Dictionary, bool>>)PredicateExtensionses.True<View_PresetTare_Dictionary>();
                }
                int i = 0;
                if (this.cbxSeachState.SelectedValue != null)//预置皮重状态
                {
                    int stateID = Converter.ToInt(cbxSeachState.SelectedValue.ToString());
                    if (stateID > 0)
                    {
                        expr = expr.And(n => n.PresetTare_Dictionary_ID == Converter.ToInt(cbxSeachState.SelectedValue.ToString()));

                        i++;
                    }
                }
                if (this.txtName.Text != "")//预置皮重名称
                {
                    //expr = expr.And(n => n.PresetTare_NAME.Contains(txtName.Text).ToString() == txtName.Text.Trim());
                    expr = expr.And(n => n.PresetTare_NAME.Contains(txtName.Text));

                    i++;
                }
                if (i == 0)
                {
                    expr = null;
                }

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("预置皮重管理 selectTJ()" + ex.Message.ToString());
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
                btnSearch.Enabled = true;
            }
            Common.WriteLogData("查询", "预置皮重信息查询", Common.USERNAME); //操作日志
        }

        #region 是否全选
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lvwPresetTare.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)dgvRoleList.Rows[i].Cells[0]).Value = false;
                lvwPresetTare.Rows[i].Selected = false;
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvwPresetTare.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)lvwUserList.Rows[i].Cells[0]).Value = true;
                lvwPresetTare.Rows[i].Selected = true;
            }
        }
        #endregion

        /// <summary>
        ///删除预置皮重信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbtnDelUser_delete()
        {
            try
            {
                int j = 0;
                if (lvwPresetTare.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //选中数量
                        int count = lvwPresetTare.SelectedRows.Count;
                        string id = "";
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            id = lvwPresetTare.SelectedRows[i].Cells["PresetTare_ID"].Value.ToString();
                            string sql = "delete PresetTare where PresetTare_ID='" + id + "'";
                            try
                            {
                                j = j + LinQBaseDao.ExecuteSql(sql);
                            }
                            catch
                            {
                            }
                        }
                        if (j > 0)
                        {
                            MessageBox.Show("成功删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要删除的行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                string strContent = "预置皮重编号为：" + this.lvwPresetTare.SelectedRows[0].Cells["PresetTare_ID"].Value.ToString(); ;
                Common.WriteLogData("删除", "删除 " + strContent + " 的信息 ", Common.USERNAME);//添加日志
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("预置皮重管理 tbtnDelUser_delete()+" + ex.Message.ToString());
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
            if (this.lvwPresetTare.SelectedRows.Count > 0)//选中行
            {
                if (lvwPresetTare.SelectedRows.Count > 1)
                {
                    MessageBox.Show("修改只能选中一行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (lvwPresetTare.SelectedRows[0].Cells["PresetTare_Name"].Value == null)
                    {
                        MessageBox.Show("选中数据违规，无法操作！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //修改的值
                    this.txtPresetTare_Name.Text = lvwPresetTare.SelectedRows[0].Cells["PresetTare_Name"].Value.ToString();
                    Expression<Func<View_PresetTare_Dictionary, bool>> funviewinto = n => n.PresetTare_NAME == this.txtPresetTare_Name.Text.Trim();
                    foreach (var n in PresetTareDAL.Query(funviewinto))
                    {
                        if (n.PresetTare_Dictionary_ID > 0)
                        {
                            //预置皮重状态
                            this.cbxPresetTareState.SelectedValue = n.PresetTare_Dictionary_ID;
                        }
                        if (n.PresetTare_NAME != null)
                        {
                            //预置皮重名称
                            this.txtPresetTare_Name.Text = n.PresetTare_NAME;
                            UpdateName = n.PresetTare_NAME;
                        }
                        if (n.PresetTare_WEIGH != null)
                        {
                            //预置皮重
                            this.txtPresetTare_Weigh.Text = n.PresetTare_WEIGH.ToString();
                        }
                        if (n.PresetTare_REMARK != null)
                        {
                            //预置皮重备注
                            this.txtPresetTare_Remark.Text = n.PresetTare_REMARK;
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
                MessageBox.Show("请选择要修改的行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            this.lvwPresetTare.DataSource = page.BindBoundControl<View_PresetTare_Dictionary>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr, "PresetTare_ID desc");
        }

        #region DataGridView的数据操作事件 绑定添加或修改时显示预置皮重状态
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
        /// <summary>
        /// 绑定添加或修改时显示预置皮重状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxPresetTareState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxPresetTareState.SelectedIndex != null)
            {
                //    int stateID = cbxPresetTareState.SelectedIndex;

                //    if (stateID >= 0)
                //    {
                //        Dictionary dic = cbxPresetTareState.SelectedItem as Dictionary;
                //        if (dic.Dictionary_Value == "终止质检")
                //        {
                //            txtQCInfo_REMARK.Enabled = true;
                //        }
                //        else
                //        {
                //            txtQCInfo_REMARK.Enabled = false;
                //        }
                //    }

            }
        }
        #endregion

        double weight = 0;
        /// <summary>
        /// 获取当前重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPresetTare_Weigh_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Timer 时间控件的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCurrentWeight_Tick(object sender, EventArgs e)
        {
            try
            {
                if (mf.isNewWeight)
                {
                    lblCurrentWeight.Text = mf.NewWeight.ToString();
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("PresetTareForm.timerCurrentWeight_Tick" + ex.Message);
            }
        }

        /// <summary>
        /// 在控件具有焦点并且用户按下并释放某个键后发生(只能输入数字)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPresetTare_Weigh_KeyPress(object sender, KeyPressEventArgs e)
        {
            //实例化出预置皮重文本框的当前值
            TextBox txt = (TextBox)sender;

            //只可输入数字和小数点
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46) && (e.KeyChar != 127))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "提示", "可输入浮点型数或整数！", txt, this);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPresetTare_Weigh.Text == "0" || txtPresetTare_Weigh.Text == "")
                {
                    txtPresetTare_Weigh.Text = lblCurrentWeight.Text;
                }
                else if (CheckRegex.RegexDecelmal(txtPresetTare_Weigh.Text))
                {
                }
                else
                {
                    if (MessageBox.Show("确定是否继续添加预置皮重的重量！", "称重提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        weight = Converter.ToDouble(txtPresetTare_Weigh.Text) + Convert.ToDouble(mf.NewWeight);
                        txtPresetTare_Weigh.Text = weight.ToString();
                    }
                    else
                    {
                        txtPresetTare_Weigh.Text = lblCurrentWeight.Text;
                    }
                }
            }
            catch
            {

            }
        }
    }
}
