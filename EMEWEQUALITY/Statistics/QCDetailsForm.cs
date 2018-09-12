﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//导入引用包
using EMEWEDAL;
using EMEWEEntity;
using EMEWEQUALITY.HelpClass;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;
using EMEWE.Common;
using WindowsFormsApplication26;
using System.Threading;

namespace EMEWEQUALITY.Statistics
{
    public partial class QCDetailsForm : Form
    {
        // 设置公共的质检车牌号并初始化
        public string weight_ticket_no = "";
        /// <summary>
        /// 返回一个空的检测记录
        /// </summary>
        Expression<Func<View_QCInfo, bool>> expr = null;
        //// 获取质检表中的车牌号
        Expression<Func<View_QCInfo, string>> p = n => n.WEIGHT_TICKET_NO;

        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();

        public QCDetailsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QCDetailsForm_Load(object sender, EventArgs e)
        {
            expr = n => n.WEIGHT_TICKET_NO == weight_ticket_no;
            
            LoadData();  // 绑定显示列表信息
            BindState(); // 绑定下拉框comboBox
        }
        /// <summary>
        /// 绑定要显示的数据加载时显示
        /// </summary>
        private void LoadData()
        {
            try
            {
                this.dvgCarList.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
                //判断磅单号不为空
                if (weight_ticket_no != "" || weight_ticket_no != null)
                {

                    dvgCarList.DataSource = null;//清空数据源
                    page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                    dvgCarList.DataSource = page.BindBoundControl<View_QCInfo>("", txtCurrentPage2, lblPageCount2, expr);
                }
                else
                {
                    MessageBox.Show("没有可查询的详情信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("frmQcRecordInfo.LoadData异常：" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 质检状态绑定
        /// </summary>
        private void BindState()
        {
            List<Dictionary> list = DictionaryDAL.GetValueStateDictionary("质检状态");
            cboxState.DataSource = list;
            cboxState.DisplayMember = "Dictionary_Name";
            cboxState.ValueMember = "Dictionary_ID";
            if (cboxState.DataSource != null)
            {
                cboxState.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQCOneInfo_Click(object sender, EventArgs e)
        {
            try
            {
                expr = (Expression<Func<View_QCInfo, bool>>)PredicateExtensionses.True<View_QCInfo>();
                var i = 0;
                if (weight_ticket_no != null)
                {
                    expr = expr.And(c => (c.CNTR_NO == weight_ticket_no));//一次组合
                    i++;
                }
                //按车牌号搜索
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    expr = expr.And(c => SqlMethods.Like(c.CNTR_NO, "%" + txtCarNO.Text.Trim() + "%"));
                    //expr = expr.And(c => c.CNTR_NO.Contains(txtCarNO.Text.Trim()));
                    i++;
                }
                //按磅单号
                if (!string.IsNullOrEmpty(txtWEIGHT_TICKET_NO.Text.Trim()))
                {
                    //expr = expr.And(c => SqlMethods.Like(c.WEIGHT_TICKET_NO, "%" + txtWEIGHT_TICKET_NO.Text.Trim() + "%"));
                    expr = expr.And(c => c.WEIGHT_TICKET_NO.Contains(txtWEIGHT_TICKET_NO.Text.Trim()));
                    i++;
                }
                //按质检状态
                if (this.cboxState.SelectedValue != null)//质检状态
                {
                    int stateID = Converter.ToInt(cboxState.SelectedValue.ToString());
                    if (stateID > 0)
                    {
                        expr = expr.And(n => n.Dictionary_ID == Converter.ToInt(cboxState.SelectedValue.ToString()));
                        i++;
                    }
                }
                //按采购单
                if (!string.IsNullOrEmpty(txtPO_NO.Text.Trim()))
                {
                    //expr = expr.And(c => SqlMethods.Like(c.PO_NO, "%" + txtPO_NO.Text.Trim() + "%"));
                    expr = expr.And(c => c.PO_NO.Contains(txtPO_NO.Text.Trim()));
                    i++;
                }
                //送货单
                if (!string.IsNullOrEmpty(txtSHIPMENT_NO.Text.Trim()))
                {
                    //expr = expr.And(c => SqlMethods.Like(c.SHIPMENT_NO, "%" + txtSHIPMENT_NO.Text.Trim() + "%"));
                    expr = expr.And(c => c.SHIPMENT_NO.Contains(txtSHIPMENT_NO.Text.Trim()));
                    i++;
                }
                if (i == 0)
                {
                    expr = null;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("QCDetailsForm 搜索异常" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void tsbSelectAll_Click()
        {
            for (int i = 0; i < dvgCarList.Rows.Count; i++)
            {
                dvgCarList.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        private void tsbNotSelectAll_Click()
        {
            for (int i = 0; i < this.dvgCarList.Rows.Count; i++)
            {
                dvgCarList.Rows[i].Selected = false;
            }
        }
        /// <summary>
        /// 删除详情信息
        /// </summary>
        private void tsbDelete_Click()
        {
            try
            {
                int j = 0;
                if (this.dvgCarList.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //选中数量
                        int count = dvgCarList.SelectedRows.Count;
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            Expression<Func<QCRecord, bool>> funQCRecordinfo = n => n.QCInfo.QCInfo_ID == Convert.ToInt32(dvgCarList.SelectedRows[i].Cells["QCInfo_ID"].Value.ToString());

                            if (!QCRecordDAL.DeleteToMany(funQCRecordinfo))
                            {
                                j++;
                            }
                        }
                        if (j == 0)
                        {
                            MessageBox.Show("成功删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        string strContent = "质检编号为：" + Convert.ToInt32(dvgCarList.SelectedRows[0].Cells["QCInfo_ID"].Value.ToString()) + "，质检车牌号为：" + this.txtCarNO.Text.Trim() + "，删除成功！";
                        LogInfoDAL.loginfoadd("删除", "删除质检信息", Common.USERNAME);//添加日志
                    }
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要删除的行！");
                }
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("车辆统计详情 tsbDelete_Click()+" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                //LoadData(Name);//更新
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }

        /// <summary>
        /// 分 页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsbDelete")//删除
            {
                tsbDelete_Click();
                return;
            }
            if (e.ClickedItem.Name == "tsbSelectAll")//全选
            {
                tsbSelectAll_Click();
                return;
            }
            if (e.ClickedItem.Name == "tsbNotSelectAll")//取消全选
            {
                tsbNotSelectAll_Click();
                return;
            }

            dvgCarList.DataSource = page.BindBoundControl<View_QCInfo>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr);
        }

        /// <summary>
        /// 设置每页显示条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData();
        }
    }
}