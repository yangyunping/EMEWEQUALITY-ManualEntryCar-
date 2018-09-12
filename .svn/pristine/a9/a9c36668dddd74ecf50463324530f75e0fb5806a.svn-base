using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEEntity;
using EMEWE.Common;
using EMEWEDAL;
using EMEWEQUALITY.HelpClass;
using System.Linq.Expressions;
namespace EMEWEQUALITY.SystemAdmin
{
    public partial class DictionaryAdmin : Form
    {
         public DictionaryAdmin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 记录字典编号
        ///</summary>
        int dictionaryID = 0;
        /// <summary>
        /// 返回一个空的预置皮重
        /// </summary>
        Expression<Func<Dictionary, bool>> expr = null;
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();
        public MainFrom mf;//主窗体

        #region  加载显示  ComboBox绑定状态  
        /// <summary>
        /// 加 载 显 示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DictionaryAdmin_Load(object sender, EventArgs e)
        {
            this.txtDictionary_Value.Focus();
            BindDictionaryOther();

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    this.cbGroup.DataSource = ds.Tables[0];
            //    this.cbGroup.DisplayMember = "Fan_GroupID";
            //    this.cbGroup.ValueMember = "Fan_GroupID";
            //}
            //else
            //{
            //    this.cbGroup.DataSource = null;
            //}

            this.dgvDictionary.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            LoadData();
            mf = new MainFrom();
        }
        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                GetDgvDictionarySeacher();

                dgvDictionary.DataSource = page.BindBoundControl<Dictionary>("", txtCurrentPage1, lblPageCount1, expr);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.BindDgvWaitQC异常:" + ex.Message.ToString());
            }

        }
        /// <summary>
        /// 绑定所属字典
        /// </summary>
        private void BindDictionaryOther()
        {

            List<Dictionary> list = DictionaryDAL.GetStateDictionary();
            if (list.Count > 0)
            {
                this.cbxDictionary_OtherID.DataSource = list;
                this.cbxDictionary_OtherID.DisplayMember = "Dictionary_Value";
                cbxDictionary_OtherID.ValueMember = "Dictionary_ID";
            }
            else
            {
                this.cbxDictionary_OtherID.DataSource = null;
            }
        }
        #endregion

        /// <summary>
        /// 搜索待检测记录条件
        /// </summary>
        private void GetDgvDictionarySeacher()
        {
            try
            {
                var i = 0;
                if (expr == null)
                {
                    expr = (Expression<Func<Dictionary, bool>>)PredicateExtensionses.True<Dictionary>();
                }

                if (!string.IsNullOrEmpty(cbxDictionary_State.Text))
                {
                    if (cbxDictionary_State.Text == "启动")
                        expr = expr.And(n => n.Dictionary_State == true);
                    else
                        expr = expr.And(n => n.Dictionary_State == false);
                    i++;
                }
                if (!string.IsNullOrEmpty(txtDictionary_Value.Text))
                {
                    expr = expr.And(n => n.Dictionary_Value == txtDictionary_Value.Text.Trim());
                    i++;
                }
                if (!string.IsNullOrEmpty(txtDictionary_Name.Text))
                {
                    expr = expr.And(n => n.Dictionary_Name == txtDictionary_Name.Text.Trim());
                    i++;
                }
                if (cbxDictionary_OtherID.SelectedIndex >= 0)
                {
                    int itemid = Converter.ToInt(cbxDictionary_OtherID.SelectedValue.ToString());
                    if (itemid > 0)
                        expr = expr.And(n => n.Dictionary_OtherID == itemid);
                    i++;
                }
                if (!string.IsNullOrEmpty(txtDictionary_Remark.Text))
                {
                    expr = expr.And(n => n.Dictionary_Remark == txtDictionary_Remark.Text.Trim());
                    i++;
                }
                if (i == 0)
                {
                    expr = null;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("DictionaryAdmin.GetDgvDictionarySeacher异常:" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 搜索 的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeacher_Click(object sender, EventArgs e)
        {

            if (btnSeacher.Enabled)
            {
                btnSeacher.Enabled = false;
                expr = null;
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
                BindDgvDictionary("");
                btnSeacher.Enabled = true;
            }
        }

        /// <summary>
        /// 验证  字典表  Dictionary_Value 非空
        /// </summary>
        /// <returns></returns>
        private bool InputCheck()
        {
            // 保存字典值
            string strDictionaryValue = this.txtDictionary_Value.Text.Trim();
            // 开始验证
            if (string.IsNullOrEmpty(strDictionaryValue))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "保存提示", "请您输入字典值！", txtDictionary_Value, this);
                return false;
            }

            // 如果已通过以上所有验证则返回真
            return true;
        }
        /// <summary>
        /// 保存字典信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool rbool = true;
            StringBuilder sbMessage = new StringBuilder();
                
            InputCheck(); // 字典值不能为空
            Dictionary dic = new Dictionary();
            dic.Dictionary_Name = txtDictionary_Name.Text.Trim();
            dic.Dictionary_Value = txtDictionary_Value.Text.Trim();
            // dic.Dictionary_OtherID = Converter.ToInt(cbxDictionary_OtherID.Text.Trim(), 0);
            dic.Dictionary_OtherID = int.Parse(this.cbxDictionary_OtherID.SelectedValue.ToString());
            if (cbxDictionary_State.Text.Trim() == "启动")
                dic.Dictionary_State = true; // Converter.ToInt(cbxDictionary_State.Text.Trim(),0);
            if (cbxDictionary_State.Text.Trim() == "注销")
                dic.Dictionary_State = false;
            dic.Dictionary_Remark = txtDictionary_Remark.Text.Trim();
            sbMessage.Append("保存"); //新增字典
            rbool = DictionaryDAL.InsertOneDictionary(dic);
            
            #region 注释
            //if (InputCheck())
            //{
            //    Dictionary dic = new Dictionary();
            //    dic.Dictionary_Name = txtDictionary_Name.Text.Trim();
            //    dic.Dictionary_Value = txtDictionary_Value.Text.Trim();
            //    dic.Dictionary_OtherID = Converter.ToInt(cbxDictionary_OtherID.Text.Trim(), 0);
            //    if (cbxDictionary_State.Text.Trim() == "启动")
            //    { dic.Dictionary_State = true; }// Converter.ToInt(cbxDictionary_State.Text.Trim(),0);
            //    if (cbxDictionary_State.Text.Trim() == "注销")
            //    { dic.Dictionary_State = false; }
            //    dic.Dictionary_Remark = txtDictionary_Remark.Text.Trim();
            //    if (dictionaryID != 0)//修改字典
            //    {
            //        sbMessage.Append("修改");
            //        rbool = DictionaryDAL.Update(dic);
            //    }
            //    else
            //    {
            //        sbMessage.Append("新增"); //新增字典
            //        rbool = DictionaryDAL.Insert(dic);
            //    }
            //}
            #endregion
            if (rbool)
            {
                MessageBox.Show(
                sbMessage.ToString() + "字典信息成功！",
                sbMessage.ToString() + "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show(sbMessage.ToString() + "字典信息信息失败！",sbMessage.ToString() + "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 修改 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool rbool = true;
            StringBuilder sbMessage = new StringBuilder();
            if (this.dgvDictionary.SelectedRows.Count > 0)//选中行
            {
                if (dgvDictionary.SelectedRows.Count > 1)
                {
                    MessageBox.Show("修改只能选中一行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Dictionary dic = new Dictionary();
                    dic.Dictionary_ID = int.Parse(dgvDictionary.SelectedRows[0].Cells["Dictionary_ID"].Value.ToString());
                    dic.Dictionary_Name = txtDictionary_Name.Text.Trim();
                    dic.Dictionary_Value = txtDictionary_Value.Text.Trim();
                    dic.Dictionary_OtherID = int.Parse(this.cbxDictionary_OtherID.SelectedValue.ToString());
                    if (cbxDictionary_State.Text.Trim() == "启动")
                        dic.Dictionary_State = true; // Converter.ToInt(cbxDictionary_State.Text.Trim(),0);
                    if (cbxDictionary_State.Text.Trim() == "注销")
                        dic.Dictionary_State = false;
                    sbMessage.Append("修改");
                    rbool = DictionaryDAL.Update(dic);
                    if (rbool)
                    {
                        MessageBox.Show(
                        sbMessage.ToString() + "字典信息成功！",
                        sbMessage.ToString() + "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(sbMessage.ToString() + "字典信息信息失败！", sbMessage.ToString() + "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        /// <summary>
        /// 取消文本框的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCanle_Click(object sender, EventArgs e)
        {
            ClearFrom();
        }
        /// <summary>
        /// 取消文本框的值  的方法
        /// </summary>
        private void ClearFrom()
        {
            txtDictionary_Value.Text = "";
            txtDictionary_Name.Text = "";
            txtDictionary_Remark.Text = "";
        }


        #region 是否全选
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll2_Click()
        {

            for (int i = 0; i < dgvDictionary.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)lvwUserList.Rows[i].Cells[0]).Value = true;
                dgvDictionary.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click()
        {
            for (int i = 0; i < this.dgvDictionary.Rows.Count; i++)
            {
                //((DataGridViewCheckBoxCell)dgvRoleList.Rows[i].Cells[0]).Value = false;
                dgvDictionary.Rows[i].Selected = false;
            }
        }
        #endregion

        #region 行选择  删除  修改  预置皮重信息
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
                if (dgvDictionary.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //选中数量
                        int count = dgvDictionary.SelectedRows.Count;
                        string id = "";
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            Expression<Func<Dictionary, bool>> funuserinfo = n => n.Dictionary_ID == Convert.ToInt32(dgvDictionary.SelectedRows[i].Cells["Dictionary_ID"].Value.ToString());

                            if (!DictionaryDAL.DeleteToMany(funuserinfo))
                            {
                                j++;
                            }
                        }
                        if (j == 0)
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
                string strContent = "字典编号为：" + Convert.ToInt32(dgvDictionary.SelectedRows[0].Cells["Dictionary_ID"].Value.ToString()) + " 字典所属编号为" + Converter.ToInt(dgvDictionary.SelectedRows[0].Cells["Dictionary_OtherID"].Value.ToString());
                Common.WriteLogData("保存", "删除 " + strContent + " 的信息", Common.USERNAME);//添加日志
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("字典管理 tbtnDelUser_delete()+" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
                LoadData();//更新
            }
        }
        /// <summary>
        /// 查看修改信息
        /// </summary>
        private void tsbtnUpdateSelect()
        {
            if (this.dgvDictionary.SelectedRows.Count > 0)//选中行
            {
                if (dgvDictionary.SelectedRows.Count > 1)
                {
                    MessageBox.Show("修改只能选中一行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //修改的值
                    this.txtDictionary_Name.Text = dgvDictionary.SelectedRows[0].Cells["Dictionary_Name"].Value.ToString();
                    Expression<Func<Dictionary, bool>> funviewinto = n => n.Dictionary_Name == this.txtDictionary_Name.Text.Trim();
                    foreach (var n in DictionaryDAL.Query(funviewinto))
                    {
                        if (n.Dictionary_Name != null)
                        {
                            //字典描述
                            this.txtDictionary_Name.Text = n.Dictionary_Name;
                        }
                        if (n.Dictionary_OtherID != null)
                        {
                            //字典所属
                            this.cbxDictionary_OtherID.SelectedValue = n.Dictionary_OtherID;
                        }
                        if (n.Dictionary_State != null)
                        {
                            //字典状态
                            this.cbxDictionary_State.SelectedValue = n.Dictionary_Name.ToString();
                        }
                        if (n.Dictionary_Value != null)
                        {
                            //字典值
                            this.txtDictionary_Value.Text = n.Dictionary_Value;
                        }
                        if (n.Dictionary_Remark != null)
                        {
                            //字典备注
                            this.txtDictionary_Remark.Text = n.Dictionary_Remark;
                        }
                        break;
                    }
                    ////隐藏添加按钮
                    //btnAdd.Visible = false;
                    //btnUpUser.Visible = true;
                    //btnUserCancle.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("请选择要修改的行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 设置每页显示条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize1_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
            BindDgvDictionary("");
        }
        /// <summary>
        /// 分页设置分页菜单响应事件(修改、删除】全选、取消全选)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            #region  注 释      
            ////if (e.ClickedItem.Name == "tsbtnUpdate") {
            ////    return;
            ////}
            ////if (e.ClickedItem.Name == "tsbtnDel")
            ////{
            ////    return;
            ////}
            //switch (e.ClickedItem.Name) { 
            //    case "tsbtnUpdate":
            //        break;
            //    case "tsbtnDel":
            //        dgvDictionary.EndEdit();
            //string delStr = checkmulti();
            //if (delStr == "")
            //{
            //    MessageBox.Show("请先选择删除项", "操作提示", MessageBoxButtons.OK,
            //                     MessageBoxIcon.Information);
            //    return;
            //}
            //if (MessageBox.Show("是否确定要删除？", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //{ }
            //        break;
            //    case "tslSelectAll2":
            //        dgvDictionary.EndEdit();
            //        foreach (DataGridViewRow dr in dgvDictionary.Rows)
            //        {
            //            ((DataGridViewCheckBoxCell)dr.Cells[0]).Value = true;
            //        }
            //        blIsSelectAll = true;
            //        break;
            //    case "tslNotSelect":
            //        dgvDictionary.EndEdit();
            //        foreach (DataGridViewRow dr in dgvDictionary.Rows)
            //        {
            //            ((DataGridViewCheckBoxCell)dr.Cells[0]).Value = false;
            //        }
            //        blIsSelectAll = false;
            //        return;
            //        break;
            //}
            //BindDgvDictionary(e.ClickedItem.Name);
            #endregion

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
            if (e.ClickedItem.Name == "tslSelectAll2")//全选
            {
                tslSelectAll2_Click();
                return;
            }
            if (e.ClickedItem.Name == "tslNotSelect")//取消全选
            {
                tslNotSelect_Click();
                return;
            }

            dgvDictionary.DataSource = page.BindBoundControl<Dictionary>(e.ClickedItem.Name, txtCurrentPage1, lblPageCount1, expr);
        }

        #region 选择删除（待用代码）
        private string checkmulti()
        {
            string delStr = "";
            for (int i = 0; i < Convert.ToInt32(dgvDictionary.RowCount); i++)
            {
                dgvDictionary.EndEdit();
                if (dgvDictionary[0, i].Value != null)
                {
                    if (Convert.ToBoolean(dgvDictionary[0, i].Value))
                    {
                        int k = dgvDictionary.ColumnCount - 1;

                        delStr += dgvDictionary[1, i].Value.ToString() + ",";

                    }
                }
            }
            if (delStr != "")
            {
                delStr = delStr.Substring(0, delStr.Length - 1);
            }

            return delStr;
        }
        private void BindDgvDictionary(string itemName)
        {

            try
            {
                GetDgvDictionarySeacher();
                this.dgvDictionary.AutoGenerateColumns = false;
                dgvDictionary.DataSource = page.BindBoundControl<Dictionary>(itemName, txtCurrentPage1, lblPageCount1, expr);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.BindDgvWaitQC异常:" + ex.Message.ToString());
            }

        }
        #endregion

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
