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
using System.Linq.Expressions;
using EMEWE.Common;
using Microsoft.Office.Interop.Word;
using System.Data.Linq.SqlClient;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class frmUpdateQC : Form
    {
        public MainFrom mf;//主窗体
        /// <summary>
        /// 返回一个空的检测记录
        /// </summary>
        Expression<Func<View_QCRecordInfo, bool>> expr = null;
        // 获取检测记录表中的记录ID
      //  Expression<Func<View_QCRecordInfo, int>> p = n => n.QCRecord_ID;
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();

        public int iQcInfoID=0;
        /// <summary>
        /// 质检记录编号
        /// </summary>
        public int iQCRecordId = 0;

        public frmUpdateQC()
        {
            InitializeComponent();
        }

        #region 加载显示(初始显示)
        /// <summary>
        /// 加载显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUpdateQC_Load(object sender, EventArgs e)
        {
           // this.btnSave.Enabled = false;//初始化加载保存按钮不显示
            btnSave.Enabled = false;
            mf = new MainFrom();//弹出手动修改质检信息
   
            //DataGridView的数据绑定显示
            BindData(); //调用重量检测的绑定显示方法
        }
        /// <summary>
        /// 加载数据
        /// </summary>

        private void LoadData(string ClickedItemName)
        {
            try
            {
                 SearchUp();//按条件搜索质检记录信息
                dgv_SFJC.DataSource = null;//清空数据源
                this.dgv_SFJC.AutoGenerateColumns = false;
                dgv_SFJC.DataSource = page.BindBoundControl<View_QCRecordInfo>(ClickedItemName, txtCurrentPage2, lblPageCount2, expr, "QCRecord_UPDATE_TIME desc");
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.LoadData异常：" + ex.Message.ToString());
            }
        }
        #endregion

        #region 绑定重量检测状态
        /// <summary>
        /// 绑定界面数据
        /// </summary>
        private void BindData()
        {
            //iWeighItemId = TestItemsDAL.GetTestItemId("重量检测");
            BindCboxState();//绑定(重量、水分)检测项目
            BindCboxTestItems();//绑定(重量、水分)检测状态下拉列表框数据

            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            tscbxPageSize2.SelectedItem = "10";
            LoadData("");

        }
        /// <summary>
        /// 绑定(重量、水分)检测项目
        /// </summary>
        private void BindCboxTestItems()
        {
            List<TestItems> list = TestItemsDAL.GetListWhereTestItemName("", "启动");
            TestItems ti=new TestItems();
            ti.TestItems_ID=0;
            ti.TestItems_NAME="全部";

            TestItems t = list[0];
            list[0] = ti;
            list.Add(t);
            
            List<TestItems> list2 = list;
            cboxTestItems2.DataSource = list;
            cboxTestItems2.DisplayMember = "TestItems_NAME";
            cboxTestItems2.ValueMember = "TestItems_ID";
            
            if (cboxTestItems2.DataSource != null)
            {
                cboxTestItems2.SelectedValue =0;
            }

            cboxTestItems.DataSource = list2;// TestItemsDAL.GetListWhereTestItemName("", "启动");
            cboxTestItems.DisplayMember = "TestItems_NAME";
            cboxTestItems.ValueMember = "TestItems_ID";
            if (cboxTestItems.DataSource != null)
            {
              //  cboxTestItems.SelectedIndex =-1;
                cboxTestItems.SelectedValue = 0;
            }
        }
        /// <summary>
        /// 绑定(重量、水分)检测状态下拉列表框数据
        /// </summary>
        private void BindCboxState()
        {
            List<EMEWEEntity.Dictionary> list = DictionaryDAL.GetValueStateDictionary("状态");
            EMEWEEntity.Dictionary d = list[0];
            list[0] = list[list.Count - 1];
            list[list.Count - 1] = d;

            List<EMEWEEntity.Dictionary> list2 = list;
            cboxState.DataSource = list;
            this.cboxState.DisplayMember = "Dictionary_Name";
            cboxState.ValueMember = "Dictionary_ID";
            if (cboxState.DataSource != null)
            {
                cboxState.SelectedValue = 0;
                //cboxState.SelectedIndex = 0;
            }
            cboxState1.DataSource = list2;
            this.cboxState1.DisplayMember = "Dictionary_Name";
            cboxState1.ValueMember = "Dictionary_ID";
            if (cboxState1.DataSource != null)
            {
                cboxState1.SelectedValue = 0;
                //cboxState1.SelectedIndex = -1;
            }



        }
        #endregion

        /// <summary>
        /// 搜索质检记录的方法
        /// </summary>
        private void SearchUp()
        {
            try
            {
               if (expr == null)
               {
                expr = (Expression<Func<View_QCRecordInfo, bool>>)PredicateExtensionses.True<View_QCRecordInfo>();
               }
                var i = 0;
                if (iQcInfoID > 0)
                {
                    expr = expr.And(c => (c.QCInfo_ID == iQcInfoID));//一次组合
                    i++;
                }
                //if (iQCRetest_ID > 0)
                //{
                //    expr = expr.And(c => (c.QCRecord_QCRetest_ID == iQCRetest_ID));
                //    i++;
                //} 
                //按车牌号搜索
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    expr = expr.And(c => SqlMethods.Like(c.CNTR_NO, "%" + txtCarNO.Text.Trim() + "%"));
                    //expr = expr.And(c => (c.CNTR_NO.Contains(txtCarNO.Text.Trim().ToUpper()) || c.CNTR_NO.Contains(txtCarNO.Text.Trim().ToLower())));
                    i++;
                }
                //按磅单号
                if (!string.IsNullOrEmpty(txtWEIGHT_TICKET_NO.Text.Trim()))
                {
                    //expr = expr.And(c => (c.WEIGHT_TICKET_NO.Contains(txtWEIGHT_TICKET_NO.Text.Trim().ToUpper()) || c.WEIGHT_TICKET_NO.Contains(txtWEIGHT_TICKET_NO.Text.Trim().ToLower())));
                    expr = expr.And(c => SqlMethods.Like(c.WEIGHT_TICKET_NO, "%" + txtWEIGHT_TICKET_NO.Text.Trim() + "%"));
                    i++;
                }
                //按检测项目
                if (cboxTestItems2.SelectedItem != null)
                {
                    int itemId = Converter.ToInt(cboxTestItems2.SelectedValue.ToString());
                    if (itemId > 0)
                    {

                        if (cboxTestItems2.Text == "水分检测" || cboxTestItems2.Text == "重量检测")
                        {
                            expr = expr.And(n => n.Tes_TestItems_ID == itemId);
                        }
                        else
                        {
                            expr = expr.And(c => (c.TestItems_ID == itemId));
                        }
                        i++;
                    }

                    
                   
                }
          
                //按数据状态
                if (cboxState.SelectedValue != null)
                {
                    int stateID = Converter.ToInt(cboxState.SelectedValue.ToString());

                    if (stateID > 0)
                    {
                        expr = expr.And(c => (c.Dictionary_ID == stateID));
                        i++;
                    }
                }
                //按采购单
                if (!string.IsNullOrEmpty(txtPO_NO.Text.Trim()))
                {
                    //expr = expr.And(c => (c.PO_NO.Contains(txtPO_NO.Text.Trim().ToUpper()) || c.PO_NO.Contains(txtPO_NO.Text.Trim().ToLower())));
                    expr = expr.And(c => SqlMethods.Like(c.PO_NO, "%" + txtPO_NO.Text.Trim() + "%"));

                    i++;
                }
                //送货单
                if (!string.IsNullOrEmpty(txtSHIPMENT_NO.Text.Trim()))
                {
                    //expr = expr.And(c => (c.SHIPMENT_NO.Contains(txtSHIPMENT_NO.Text.Trim().ToUpper()) || c.SHIPMENT_NO.Contains(txtSHIPMENT_NO.Text.Trim().ToLower())));
                    expr = expr.And(c => SqlMethods.Like(c.SHIPMENT_NO, "%" + txtSHIPMENT_NO.Text.Trim() + "%"));
                    i++;
                }
                if (i == 0)
                {
                    expr = null;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.GetCondition异常" + ex.Message.ToString());
            }
            
        }
        /// <summary>
        /// “搜索”按钮的单击事件（按条件查询）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQCOneInfo_Click(object sender, EventArgs e)
        {
            if (btnQCOneInfo.Enabled)
            {
                btnQCOneInfo.Enabled = false;
                expr = null;
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData("");
                btnQCOneInfo.Enabled = true;
            }
           // LogInfoDAL.loginfoadd("查询", "手动修改查询", Common.USERNAME); //操作日志
        }

        /// <summary>
        /// 对已绑定的单行数据进行修改并保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv_SFJC.SelectedRows.Count > 1 || iQCRecordId<=0)
                {
                    MessageBox.Show("修改只能选中一行！");
                }
                else
                {
                    #region 验证文本框
                    if (this.txtQCRecord_RESULT.Text == "")
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "质检结果不能为空！", txtQCRecord_RESULT, this);
                        return;
                    }
                    if (Convert.ToDecimal(this.txtQCRecord_RESULT.Text) == 0)
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "质检结果不能为零！", txtQCRecord_RESULT, this);
                        return;
                    }
                    #endregion

                    #region 找到要修改行信息的位置并进行修改
                    Expression<Func<QCRecord, bool>> p = n => n.QCRecord_ID == iQCRecordId;
                    Action<QCRecord> ap = s =>
                    {
                        s.QCRecord_RESULT = Convert.ToDecimal(this.txtQCRecord_RESULT.Text.Trim());//水分值
                        s.QCRecord_NUMBER = this.txtQCRecord_NUMBER.Text.Trim();//质检序号
                        s.QCRecord_DRAW = Convert.ToDecimal(this.txtQCRecord_DRAW.Text.Trim());//抽检包号
                        s.QCRecord_TARE = Convert.ToDecimal(this.txtQCRecord_TARE.Text.Trim());//预置皮重
                        s.QCRecord_QCCOUNT = Convert.ToInt32(this.txtQCRecord_QCCOUNT.Text.Trim());//结果质检次数
                        s.QCRecord_COUNT = this.txtQCRecord_COUNT.Text.Trim();//记录质检次数
                    };

                    if (txtQCRecord_RESULT.Text.Substring(0, 1) == ".")
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "第一位不能是小数点！", txtQCRecord_RESULT, this);
                        return;
                    }
                    else
                    {
                        if (QCRecordDAL.Update(p, ap))
                        {
                            MessageBox.Show("修改成功", "提示");
                        }
                        else
                        {
                            MessageBox.Show("修改失败", "提示");
                        }
                    }
                    string strContent = "质检记录编号为：" + QCRecord_ID.ToString() + "，修改";
                    LogInfoDAL.loginfoadd("修改", "修改质检记录信息", Common.USERNAME);//添加日志
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("手动修改管理 bntUpUser_Click()" + ex.Message.ToString());
            }
            finally
            {
               ClearB(); 
               LoadData("");
            }
        }

        #region 是否全选
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNotSelectAll_Click()
        {
            for (int i = 0; i < this.dgv_SFJC.Rows.Count; i++)
            {
                dgv_SFJC.Rows[i].Selected = false;
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSelectAll_Click()
        {
            for (int i = 0; i < dgv_SFJC.Rows.Count; i++)
            {
                dgv_SFJC.Rows[i].Selected = true;
            }
        }
        #endregion

        #region   行选择  查看要修改的质检信息  删除选中行
        /// <summary>
        /// 检测项目:是水份检测或重量检测
        /// </summary>
        private void FormWate(string TestItems_NAME)
        {
            if (TestItems_NAME.Contains("水分"))
            {
                txtQCRecord_DRAW.Enabled = false;
                txtQCRecord_TARE.Enabled = false;
                txtQCRecord_QCCOUNT.Enabled = true;
                txtQCRecord_NUMBER.Enabled = true;
                txtQCRecord_COUNT.Enabled = true;
            }
            else 
            {
                 txtQCRecord_RESULT.Enabled = true;
                 txtQCRecord_DRAW.Enabled = true;
                 txtQCRecord_TARE.Enabled = true;
                txtQCRecord_QCCOUNT.Enabled = false;
                txtQCRecord_NUMBER.Enabled = false;
                txtQCRecord_COUNT.Enabled = false;
            }
        }
       
        /// <summary>
        /// 查看修改信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbUpdate_Click()
        {
            if (this.dgv_SFJC.SelectedRows.Count > 0)//选中行
            {
                if (dgv_SFJC.SelectedRows.Count > 1)
                {
                    MessageBox.Show("修改只能选中一行！");
                    return;
                }
                else
                {
                    if (dgv_SFJC.SelectedRows[0].Cells["QCRecord_ID"].Value != null)
                    {
                        iQCRecordId =Converter.ToInt(dgv_SFJC.SelectedRows[0].Cells["QCRecord_ID"].Value.ToString(),0);
                    }
                    if (iQCRecordId > 0)
                    {
                        btnCanle.Enabled = true;
                        btnSave.Enabled = true;
                       // this.btnSave.Enabled = true;
                        Expression<Func<View_QCRecordInfo, bool>> funviewinto = n => n.QCRecord_ID == iQCRecordId;
                        #region 修改时赋值
                        var qcrecord = QCRecordDAL.Query(funviewinto);
                        if (qcrecord == null) return;

                        foreach (var n in QCRecordDAL.Query(funviewinto))
                        {
                            if (n.TestItems_ID > 0)//项目ID
                            {
                                cboxTestItems.SelectedValue = n.TestItems_ID;

                            }
                            if (!string.IsNullOrEmpty(n.TestItems_NAME))//项目名称
                            {
                                FormWate(n.TestItems_NAME);
                            }
                            if (n.QCRecord_RESULT > 0)//水分值/重量值:
                            {
                                txtQCRecord_RESULT.Text = n.QCRecord_RESULT.ToString();
                            }
                            if (n.Dictionary_ID > 0)//数据状态
                            {
                                cboxState1.SelectedValue = n.Dictionary_ID;
                            }
                            if (n.QCRecord_DRAW > 0)//抽检包序号:
                            {
                                txtQCRecord_DRAW.Text = n.QCRecord_DRAW.ToString();
                            }
                            if (n.QCRecord_TARE > 0)//(预置)皮重:
                            {
                                txtQCRecord_TARE.Text = n.QCRecord_TARE.ToString();
                            }
                            if (n.QCRecord_QCCOUNT > 0)//检测序号:
                            {
                                txtQCRecord_QCCOUNT.Text = n.QCRecord_QCCOUNT.ToString();
                            }
                            if (!string.IsNullOrEmpty(n.QCRecord_NUMBER))//仪表序号:
                            {
                                txtQCRecord_NUMBER.Text = n.QCRecord_NUMBER.ToString();
                            }
                            if (!string.IsNullOrEmpty(n.QCRecord_COUNT))//仪表检测次数:
                            {
                                txtQCRecord_COUNT.Text = n.QCRecord_COUNT.ToString();
                            }
                            break;
                        }
    
                        #endregion
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要修改的行！");
            }
        }        
        /// <summary>
        /// 删除选中的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDelete_Click()
        {
            try
            {
                int j = 0;
                if (this.dgv_SFJC.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //选中数量
                        int count = dgv_SFJC.SelectedRows.Count;
                        string id = "";
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            Expression<Func<QCRecord, bool>> funQCRecordinfo = n => n.QCRecord_ID == Convert.ToInt32(dgv_SFJC.SelectedRows[i].Cells["QCRecord_ID"].Value.ToString());

                            if (!QCRecordDAL.DeleteToMany(funQCRecordinfo))
                            {
                                j++;
                            }
                        }
                        if (j == 0)
                        {
                            MessageBox.Show("成功删除", "提示");

                        }
                        else
                        {
                            MessageBox.Show("删除失败", "提示");
                       
                        }
                        string strContent = "质检记录编号为：" +QCRecord_ID.ToString() + "，删除";
                        LogInfoDAL.loginfoadd("删除", "删除质检记录信息", Common.USERNAME);//添加日志
                    }
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要删除的行！");
                }
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("手动修改管理 tbtnDelUser_delete()+" + ex.Message.ToString());
            }
            finally
            {
                LoadData("");
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
            LoadData("");
        }
        //分页菜单响应事件
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsbUpdate")//修改
            {
                tsbUpdate.Enabled = false;
                tsbUpdate_Click();
                tsbUpdate.Enabled = true;
                return;
            }
            else if (e.ClickedItem.Name == "tsbDelete")//删除
            {
                tsbDelete.Enabled = false;
                tsbDelete_Click();
                tsbDelete.Enabled = true;
                return;
            }
            else if (e.ClickedItem.Name == "tsbSelectAll")//全选
            {
                tsbSelectAll.Enabled = false;
                tsbSelectAll_Click();
                tsbSelectAll.Enabled = true;
                return;
            }
            else if (e.ClickedItem.Name == "tsbNotSelectAll")//取消全选
            {
                tsbSelectAll.Enabled = false;
                tsbNotSelectAll_Click();
                tsbSelectAll.Enabled = true;
                return;
            }
            else
            {
                
                LoadData(e.ClickedItem.Name);
            }
        }

        /// <summary>
        /// 质检结果（水分值）的KeyPress键盘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQCRecord_RESULT_KeyPress(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// 手 动 修 改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                #region 验证文本框
                if (this.txtQCRecord_RESULT.Text == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "质检结果不能为空！", txtQCRecord_RESULT, this);
                    btnSave.Enabled = true;
                    return;
                }
                if (Convert.ToDecimal(this.txtQCRecord_RESULT.Text) == 0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "质检结果不能为零！", txtQCRecord_RESULT, this);
                    btnSave.Enabled = true;
                    return;
                }
                #endregion

                if (this.cboxState.SelectedIndex > -1 && this.cboxTestItems2.SelectedIndex > -1)
                {
                    Expression<Func<QCRecord, bool>> p = n => n.QCRecord_ID ==iQCRecordId;

                    Action<QCRecord> ap = s =>
                    {
                        s.QCRecord_TestItems_ID = Common.GetInt(cboxTestItems.SelectedValue.ToString());//检测项目:
                        s.QCRecord_RESULT = Common.GetDecimal(txtQCRecord_RESULT.Text.Trim());//水分值/重量值:
                        s.QCRecord_Dictionary_ID = Common.GetInt(cboxState1.SelectedValue.ToString());//数据状态:
                        s.QCRecord_DRAW = Common.GetInt(txtQCRecord_DRAW.Text.Trim());//抽检包序号:
                        s.QCRecord_TARE = Common.GetInt(txtQCRecord_TARE.Text.Trim());//(预置)皮重:
                        s.QCRecord_QCCOUNT = Common.GetInt(txtQCRecord_QCCOUNT.Text.Trim());//检测序号:
                        s.QCRecord_NUMBER = txtQCRecord_NUMBER.Text.Trim();//仪表序号:
                        s.QCRecord_COUNT = txtQCRecord_COUNT.Text.Trim();//仪表检测次数:

                        //s.QCRecord_RESULT = Convert.ToDecimal(this.txtQCRecord_RESULT.Text.Trim());//水分值
                        //s.QCRecord_NUMBER = this.txtQCRecord_NUMBER.Text.Trim();//质检序号
                        //s.QCRecord_DRAW = Convert.ToDecimal(this.txtQCRecord_DRAW.Text.Trim());//抽检包号
                        //s.QCRecord_TARE = Convert.ToDecimal(this.txtQCRecord_TARE.Text.Trim());//预置皮重
                        //s.QCRecord_QCCOUNT = Convert.ToInt32(this.txtQCRecord_QCCOUNT.Text.Trim());//结果质检次数
                        //s.QCRecord_COUNT = this.txtQCRecord_COUNT.Text.Trim();//记录质检次数

                    };

                    if (txtQCRecord_RESULT.Text.Substring(0, 1) == ".")
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "第一位不能是小数点！", txtQCRecord_RESULT, this);
                        this.txtQCRecord_RESULT.Text = "";
                    }
                    else
                    {
                        if (QCRecordDAL.Update(p, ap))
                        {
                            MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            btnSave.Enabled = true;
                            MessageBox.Show("修改失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        string strContent = "手动修改质检记录编号为：" + iQCRecordId ;
                        Common.WriteLogData("修改", strContent,"");//添加日志
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("手动修改管理 bntUpUser_Click()" + ex.Message.ToString());
            }
            finally
            {
                ClearB();
          
                LoadData("");
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        private void ClearB()
        {
            cboxTestItems.SelectedValue = 0;
            txtQCRecord_RESULT.Clear();
            cboxState1.SelectedValue = 0;
            txtQCRecord_DRAW.Clear();
            txtQCRecord_TARE.Clear();
            txtQCRecord_QCCOUNT.Clear();
            txtQCRecord_NUMBER.Clear();
            txtQCRecord_COUNT.Clear();
          btnSave.Enabled = false;
             btnCanle.Enabled = false;
            //txtQCRecord_RESULT.Enabled = false;
            txtQCRecord_DRAW.Enabled = false;
            txtQCRecord_TARE.Enabled = false;
            txtQCRecord_QCCOUNT.Enabled = false;
            txtQCRecord_NUMBER.Enabled = false;
            txtQCRecord_COUNT.Enabled = false;

        }

        private void btnCanle_Click(object sender, EventArgs e)
        {
            ClearB();
        }

       

    }
}