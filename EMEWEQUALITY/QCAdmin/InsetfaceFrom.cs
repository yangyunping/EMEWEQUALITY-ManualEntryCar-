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
using EMEWEQUALITY.HelpClass;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;
using EMEWE.Common;
using System.Collections;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class InsetfaceFrom : Form
    {
        public MainFrom mf;//主窗体
        // 设置公共的质检车牌号并初始化
        public string cntr_no = "";
        /// <summary>
        /// 返回一个空的检测记录
        /// </summary>
       Expression<Func<MATERIAL_QC_INTERFACE, bool>> mexpr = null;
        //// 获取质检表中的车牌号
        Expression<Func<MATERIAL_QC_INTERFACE, string>> k = n => n.CNTR_NO;

        Expression<Func<OCC_MOIST_INTERFACE, bool>> expr = null;
        //// 获取质检表中的车牌号
        Expression<Func<OCC_MOIST_INTERFACE, string>> p = n => n.CNTR_NO;
        private bool isnames = true;
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();
        public InsetfaceFrom()
        {
            InitializeComponent();
        }

        private void InsetfaceFrom_Load(object sender, EventArgs e)
        {
            LoadData();  // 绑定显示列表信息
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                if (isnames)
                {
                    this.dataGridView1.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
                    //判断车牌号不为空
                    if (cntr_no != "" || cntr_no != null)
                    {
                        dvgCarList.DataSource = null;//清空数据源
                        dataGridView1.DataSource = null;//清空数据源
                        page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                        dataGridView1.DataSource = page.BindBoundControl<OCC_MOIST_INTERFACE>("", txtCurrentPage2, lblPageCount2, expr, "OCC_MOIST_INTERFACE_ID desc");
                    }
                    else
                    {
                        MessageBox.Show("没有可查询的详情信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.dvgCarList.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
                    //判断车牌号不为空
                    if (cntr_no != "" || cntr_no != null)
                    {
                        dataGridView1.DataSource = null;//清空数据源
                        dvgCarList.DataSource = null;//清空数据源
                        page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                        dvgCarList.DataSource = page.BindBoundControl<MATERIAL_QC_INTERFACE>("", txtCurrentPage2, lblPageCount2, mexpr);
                    }
                    else
                    {
                        MessageBox.Show("没有可查询的详情信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("InsetfaceFrom_LoadData异常：" + ex.Message.ToString());
            }
        }

        private void btnQCOneInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isnames)
                {
                    mexpr = (Expression<Func<MATERIAL_QC_INTERFACE, bool>>)PredicateExtensionses.True<MATERIAL_QC_INTERFACE>();
                    var i = 0;
                    //按车牌号搜索
                    if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                    {
                        mexpr = mexpr.And(c => SqlMethods.Like(c.CNTR_NO, "%" + txtCarNO.Text.Trim() + "%"));
                        i++;
                    }
                    //按磅单号
                    if (!string.IsNullOrEmpty(txtWEIGHT_TICKET_NO.Text.Trim()))
                    {
                        mexpr = mexpr.And(c => c.WEIGHT_TICKET_NO.Contains(txtWEIGHT_TICKET_NO.Text.Trim()));
                        i++;
                    }

                    //按采购单
                    if (!string.IsNullOrEmpty(txtPO_NO.Text.Trim()))
                    {
                        mexpr = mexpr.And(c => c.PO_NO.Contains(txtPO_NO.Text.Trim()));
                        i++;
                    }
                    //送货单
                    if (!string.IsNullOrEmpty(txtSHIPMENT_NO.Text.Trim()))
                    {
                        mexpr = mexpr.And(c => c.SHIPMENT_NO.Contains(txtSHIPMENT_NO.Text.Trim()));
                        i++;
                    }
                    if (!string.IsNullOrEmpty(cboxState.Text.Trim()))
                    {
                        if (cboxState.Text.Trim() == "已过数")
                        {
                            mexpr = mexpr.And(c => c.TRANS_TO_DTS_FLAG.Contains("Y"));
                            i++;
                        }
                        else
                        {
                            expr = expr.And(c => c.TRANS_TO_DTS_FLAG.ToString().ToLower() != "Y");
                            i++;
                        }
                    }
                    if (i == 0)
                    {
                        expr = null;
                    }
                }
                else
                {
                    expr = (Expression<Func<OCC_MOIST_INTERFACE, bool>>)PredicateExtensionses.True<OCC_MOIST_INTERFACE>();
                    var i = 0;
                    //按车牌号搜索
                    if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                    {
                        expr = expr.And(c => SqlMethods.Like(c.CNTR_NO, "%" + txtCarNO.Text.Trim() + "%"));
                        i++;
                    }
                    //按磅单号
                    if (!string.IsNullOrEmpty(txtWEIGHT_TICKET_NO.Text.Trim()))
                    {
                        expr = expr.And(c => c.WEIGHT_TICKET_NO.Contains(txtWEIGHT_TICKET_NO.Text.Trim()));
                        i++;
                    }

                    //按采购单
                    if (!string.IsNullOrEmpty(txtPO_NO.Text.Trim()))
                    {
                        expr = expr.And(c => c.PO_NO.Contains(txtPO_NO.Text.Trim()));
                        i++;
                    }
                    //送货单
                    if (!string.IsNullOrEmpty(txtSHIPMENT_NO.Text.Trim()))
                    {
                        expr = expr.And(c => c.SHIPMENT_NO.Contains(txtSHIPMENT_NO.Text.Trim()));
                        i++;
                    }
                    if (!string.IsNullOrEmpty(cboxState.Text.Trim()))
                    {
                        if (cboxState.Text.Trim() == "已过数")
                        {
                            expr = expr.And(c => c.TRANS_TO_DTS_FLAG.ToString().ToLower() == "Y"); i++;
                        }
                        else
                        {
                            expr = expr.And(c => c.TRANS_TO_DTS_FLAG.ToString().ToLower() != "Y");
                            i++;
                        }
                    }
                    if (i == 0)
                    {
                        expr = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("InsetfaceFrom 搜索异常" + ex.Message.ToString());
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
            if (isnames)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < dvgCarList.Rows.Count; i++)
                {
                    dvgCarList.Rows[i].Selected = true;
                }
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        private void tsbNotSelectAll_Click()
        {

            if (isnames)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                }
            }
            else
            {
                for (int i = 0; i < this.dvgCarList.Rows.Count; i++)
                {
                    dvgCarList.Rows[i].Selected = false;
                }
            }
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        private void tsbDelete_Click()
        {
            try
            {
                bool isdel = false;
                int j = 0;
                if (this.dvgCarList.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("将会同时删除该车辆过数DTS水分数据表的信息，确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        isdel = true;
                        //选中数量
                        int count = dvgCarList.SelectedRows.Count;
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            Expression<Func<MATERIAL_QC_INTERFACE, bool>> material_qc = n => n.PO_NO == dvgCarList.SelectedRows[i].Cells["PO_NO"].Value.ToString() && n.SHIPMENT_NO == dvgCarList.SelectedRows[i].Cells["SHIPMENT_NO"].Value.ToString();
                            Expression<Func<OCC_MOIST_INTERFACE, bool>> occ_moist = n => n.PO_NO == dvgCarList.SelectedRows[i].Cells["PO_NO"].Value.ToString() && n.SHIPMENT_NO == dvgCarList.SelectedRows[i].Cells["SHIPMENT_NO"].Value.ToString();
                            IEnumerable<OCC_MOIST_INTERFACE> occ_erface = OCC_MOIST_INTERFACEDAL.Query(occ_moist);
                            string trans_to_dtsflag = "";
                            foreach (var occ in occ_erface)
                            {
                                if (!string.IsNullOrEmpty(occ.TRANS_TO_DTS_FLAG))
                                {
                                    trans_to_dtsflag = occ.TRANS_TO_DTS_FLAG;
                                }
                            }
                            if (!string.IsNullOrEmpty(trans_to_dtsflag))
                            {
                                if (MessageBox.Show("编号为：" + dvgCarList.SelectedRows[i].Cells["MATERIAL_QC_INTERFACE_ID"].Value.ToString() + "车牌号为：" + dvgCarList.SelectedRows[i].Cells["CNTRNO"].Value.ToString() + " 已经过数，确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    if (!MATERIAL_QC_INTERFACEDAL.DeleteToMany(material_qc))
                                    {
                                        j++;
                                    }
                                    if (!OCC_MOIST_INTERFACEDAL.DeleteToMany(occ_moist))
                                    {
                                        j++;
                                    }
                                }
                                else
                                {
                                    isdel = false;
                                }
                            }
                            else
                            {
                                if (!MATERIAL_QC_INTERFACEDAL.DeleteToMany(material_qc))
                                {
                                    j++;
                                }
                                if (!OCC_MOIST_INTERFACEDAL.DeleteToMany(occ_moist))
                                {
                                    j++;
                                }
                            }
                        }

                        if (j == 0)
                        {
                            if (isdel)
                            {
                                MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (isdel)
                            {
                                MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        string strContent = "编号为：" + dvgCarList.SelectedRows[0].Cells["MATERIAL_QC_INTERFACE_ID"].Value.ToString() + "，车牌号为：" + dvgCarList.SelectedRows[0].Cells["CNTRN"].Value.ToString() + "，删除成功！";
                        LogInfoDAL.loginfoadd("删除", "删除过数DTS水分数据与过数DTS重量数据", Common.USERNAME);//添加日志
                    }
                }
                else if (this.dataGridView1.SelectedRows.Count > 0)//选中删除
                {
                    if (MessageBox.Show("将会同时删除该车辆过数DTS重量数据表的信息，确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        isdel = true;
                        //选中数量
                        int count = dataGridView1.SelectedRows.Count;
                        //遍历
                        for (int i = 0; i < count; i++)
                        {
                            Expression<Func<MATERIAL_QC_INTERFACE, bool>> material_qc = n => n.PO_NO == dataGridView1.SelectedRows[i].Cells["PONO"].Value.ToString() && n.SHIPMENT_NO == dataGridView1.SelectedRows[i].Cells["SHIPMENTNO"].Value.ToString();
                            Expression<Func<OCC_MOIST_INTERFACE, bool>> occ_moist = n => n.PO_NO == dataGridView1.SelectedRows[i].Cells["PONO"].Value.ToString() && n.SHIPMENT_NO == dataGridView1.SelectedRows[i].Cells["SHIPMENTNO"].Value.ToString();
                            IEnumerable<OCC_MOIST_INTERFACE> occ_erface = OCC_MOIST_INTERFACEDAL.Query(occ_moist);
                            string trans_to_dtsflag = "";
                            foreach (var occ in occ_erface)
                            {
                                if (!string.IsNullOrEmpty(occ.TRANS_TO_DTS_FLAG))
                                {
                                    trans_to_dtsflag = occ.TRANS_TO_DTS_FLAG;
                                }
                            }
                            if (!string.IsNullOrEmpty(trans_to_dtsflag))
                            {
                                if (MessageBox.Show("编号为：" + dataGridView1.SelectedRows[i].Cells["OCC_MOIST_INTERFACE_ID"].Value.ToString() + "车牌号为：" + dataGridView1.SelectedRows[i].Cells["CNTRN"].Value.ToString() + " 已经过数，确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    if (!MATERIAL_QC_INTERFACEDAL.DeleteToMany(material_qc))
                                    {
                                        j++;
                                    }
                                    if (!OCC_MOIST_INTERFACEDAL.DeleteToMany(occ_moist))
                                    {
                                        j++;
                                    }
                                }
                                else
                                {
                                    isdel = false;
                                }
                            }
                            else
                            {
                                if (!MATERIAL_QC_INTERFACEDAL.DeleteToMany(material_qc))
                                {
                                    j++;
                                }
                                if (!OCC_MOIST_INTERFACEDAL.DeleteToMany(occ_moist))
                                {
                                    j++;
                                }
                            }
                        }
                        if (j == 0)
                        {
                            if (isdel)
                            {
                                MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (isdel)
                            {
                                MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        string strContent = "编号为：" + dataGridView1.SelectedRows[0].Cells["OCC_MOIST_INTERFACE_ID"].Value.ToString() + "，车牌号为：" + dataGridView1.SelectedRows[0].Cells["CNTRN"].Value.ToString() + "，删除成功！";
                        LogInfoDAL.loginfoadd("删除", "删除过数DTS水分数据与过数DTS重量数据", Common.USERNAME);//添加日志
                    }
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要删除的行！");
                }
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("InsetfaceFrom tsbDelete_Click()+" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }
        }

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
            if (!isnames)
            {
                dvgCarList.DataSource = page.BindBoundControl<MATERIAL_QC_INTERFACE>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, mexpr);
            }
            else
            {
                dataGridView1.DataSource = page.BindBoundControl<OCC_MOIST_INTERFACE>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr);
            }
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

        private void cbxocc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tables = cbxocc.Text.Trim();
            if (!string.IsNullOrEmpty(tables))
            {
                if (tables == "过数DTS水分表")
                {
                    isnames = true;
                    dataGridView1.Visible = true;
                    dvgCarList.Visible = false;
                    LoadData();
                }
                if (tables == "过数DTS重量表")
                {
                    isnames = false;
                    dataGridView1.Visible = false;
                    dvgCarList.Visible = true;
                    LoadData();
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (Convert.ToString(dataGridView1.Rows[i].Cells["TRANS_TO_DTSFLAG"].Value) == "")
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gold;
                        }
                        else
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Lime;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dvgCarList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (dvgCarList.RowCount > 0)
                {
                    for (int i = 0; i < dvgCarList.RowCount; i++)
                    {
                        if (Convert.ToString(dvgCarList.Rows[i].Cells["TRANS_TO_DTS_FLAG"].Value) == "")
                        {
                            dvgCarList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gold;
                        }
                        else
                        {
                            dvgCarList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Lime;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
