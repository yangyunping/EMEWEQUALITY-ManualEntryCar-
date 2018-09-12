using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEQUALITY.HelpClass;
using EMEWEEntity;
using System.Linq.Expressions;
using EMEWEDAL;
using System.Data.Linq.SqlClient;

namespace EMEWEQUALITY.NewAdd
{
    public partial class ExceptionHandlingForm : Form
    {
        public ExceptionHandlingForm()
        {
            InitializeComponent();
        }
        public MainFrom mf;//主窗体

        public string CardPeoples = "";//刷卡处理人
        public bool btnSaveIsEnabled = false;//保存按钮是否禁用
        string sqlWhere = " 1=1";
        /// <summary>
        /// 返回一个空的检测记录
        /// </summary>
        Expression<Func<View_UnusualHandel, bool>> expr = null;
        /// <summary>
        /// 选中DGVUnusual中的ID列表数据
        /// </summary>
        List<int> listrecordID = new List<int>();
        List<int> QCInfoIDS = new List<int>();
        List<int> DRAW_EXAM_INTERFACE_ID_IDs = new List<int>();
        /// <summary>
        /// 选中行列表
        /// </summary>
        List<int> ids = new List<int>();
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();
        private void ExceptionHandlingForm_Load(object sender, EventArgs e)
        {
            //绑定质检状态
            DataTable Dictionary_NameDT = LinQBaseDao.Query("select Dictionary_ID,Dictionary_Name from Dictionary where Dictionary_OtherID=6").Tables[0];
            DataRow newRow = Dictionary_NameDT.NewRow();

            newRow["Dictionary_Name"] = "全部";


            Dictionary_NameDT.Rows.InsertAt(newRow, 0);
            cbxDictionary_Name.DataSource = Dictionary_NameDT;
            cbxDictionary_Name.DisplayMember = "Dictionary_Name";
            cbxDictionary_Name.ValueMember = "Dictionary_Name";


            DataTable testItemDT = LinQBaseDao.Query("select TestItems_NAME from TestItems").Tables[0];
            DataRow newRowB = testItemDT.NewRow();

            newRowB["TestItems_NAME"] = "全部";
            testItemDT.Rows.InsertAt(newRowB, 0);
            cbxTestItems_NAME.DataSource = testItemDT;
            cbxTestItems_NAME.DisplayMember = "TestItems_NAME";
            cbxTestItems_NAME.ValueMember = "TestItems_NAME";

            //PROD_ID
            DataTable PROD_IDDT = LinQBaseDao.Query("SELECT Unusualstandard_PROD FROM  Unusualstandard").Tables[0];


            DataRow newRowC = PROD_IDDT.NewRow();
            newRowC["Unusualstandard_PROD"] = "全部";
            PROD_IDDT.Rows.InsertAt(newRowC, 0);
            cbxPROD_ID.DataSource = PROD_IDDT;
            cbxPROD_ID.ValueMember = "Unusualstandard_PROD";
            cbxPROD_ID.DisplayMember = "Unusualstandard_PROD";

            cbxUnusual_State.SelectedIndex = 1;

            tscbxPageSize2.Text = "10";
            txtCurrentPage2.Text = "1";
            LoadData("");
            this.DGVUnusual.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            btnSaveIsEnabled = BtnSave.Enabled;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData("");

        }

        DCQUALITYDataContext dc = new DCQUALITYDataContext();
        int maxpage = 0;
        int pageSize = 0;
        int currentPage = 0;
        private void LoadData(string clickName)
        {

            if (clickName == "tslNextPage1")
            {
                if (currentPage != maxpage)
                {
                    txtCurrentPage2.Text = (Convert.ToInt32(txtCurrentPage2.Text) + 1).ToString();

                }
                else
                {
                    MessageBox.Show("已经是最后一页！");
                    return;
                }
            }
            if (clickName == "tslPreviousPage2")
            {
                if (currentPage != 1)
                {
                    txtCurrentPage2.Text = (Convert.ToInt32(txtCurrentPage2.Text) - 1).ToString();
                }
                else
                {
                    MessageBox.Show("已经是第一页！");
                    return;
                }

            }
            if (clickName == "tslHomPage1")
            {
                if (currentPage != 1)
                {
                    txtCurrentPage2.Text = "1";
                }
                else
                {
                    MessageBox.Show("已经是第一页！");
                    return;
                }
            }
            if (clickName == "tslLastPage1")
            {
                if (currentPage != maxpage)
                {
                    txtCurrentPage2.Text = maxpage.ToString();

                }
                else
                {
                    MessageBox.Show("已经是最后一页！");
                    return;
                }
            }
            sqlWhere = " 1=1";

            if (expr == null)
            {
                expr = PredicateExtensionses.True<View_UnusualHandel>();
            }

            if (txtCarNO.Text != "")
            {

                expr = expr.And(n => SqlMethods.Like(n.CNTR_NO, "%" + txtCarNO.Text.Trim() + "%"));
            }

            if (cbxPROD_ID.Text.Trim() != "全部")
            {

                expr = expr.And(n => SqlMethods.Like(n.PROD_ID, "%" + cbxPROD_ID.Text.Trim() + "%"));
            }
            if (cbxDictionary_Name.Text.Trim() != "全部")
            {
                expr = expr.And(n => SqlMethods.Like(n.Dictionary_Name, "%" + cbxDictionary_Name.Text.Trim() + "%"));
            }
            if (cbxTestItems_NAME.Text.Trim() != "全部")
            {
                expr = expr.And(n => SqlMethods.Like(n.TestItems_NAME, "%" + cbxTestItems_NAME.Text.Trim() + "%"));
            }

            if (cbxUnusual_State.Text.Trim() != "全部")
            {
                expr = expr.And(n => SqlMethods.Like(n.Unusual_State, "%" + cbxUnusual_State.Text.Trim() + "%"));
            }



            if (txtREF_NO.Text != "")
            {
                sqlWhere += " and REF_NO like '%" + txtREF_NO.Text.Trim() + "%'";
            }

            pageSize = Convert.ToInt32(tscbxPageSize2.Text);
            currentPage = Convert.ToInt32(txtCurrentPage2.Text);
            int count = dc.GetTable<View_UnusualHandel>().Where<View_UnusualHandel>(expr).ToList().Count;
            maxpage = count % pageSize == 0 ? count % pageSize : Convert.ToInt32(count / pageSize) + 1;
            lblPageCount2.Text = "共" + maxpage + "页";
            System.Collections.Generic.List<View_UnusualHandel> list = dc.GetTable<View_UnusualHandel>().Where<View_UnusualHandel>(expr).ToList().Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            DGVUnusual.DataSource = list;
            DGVUnusual.VirtualMode = false;
            listrecordID.Clear();
            QCInfoIDS.Clear();
            DRAW_EXAM_INTERFACE_ID_IDs.Clear();
            DRAW_EXAM_INTERFACE_ID_IDs.Clear();
            ids.Clear();
            expr = null;
        }



        /// <summary>
        /// 全选
        /// </summary>
        private void tsbSelectAll_Click()
        {
            for (int i = 0; i < DGVUnusual.Rows.Count; i++)
            {
                DGVUnusual.Rows[i].Cells["xz"].Value = true;
                DGVUnusual.Rows[i].Selected = true;

            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        private void tsbNotSelectAll_Click()
        {
            for (int i = 0; i < this.DGVUnusual.Rows.Count; i++)
            {
                DGVUnusual.Rows[i].Cells["xz"].Value = false;
                DGVUnusual.Rows[i].Selected = false;
                ids.Clear();
                listrecordID.Clear();
                QCInfoIDS.Clear();
                DRAW_EXAM_INTERFACE_ID_IDs.Clear();
            }
        }
        /// <summary>
        /// 删除 
        /// </summary>
        private void tsbDelete_Click()
        {
            try
            {
                int j = 0;
                if (listrecordID.Count > 0)//选中删除
                {
                    if (MessageBox.Show("确定要删除吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //遍历
                        for (int i = 0; i < listrecordID.Count; i++)
                        {
                            Expression<Func<Unusual, bool>> funUnusual = n => n.Unusual_Id == Convert.ToInt32(listrecordID[i]);

                            if (!UnusualDAL.DeleteToMany(funUnusual))
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
                        string strContent = "质检编号为：" + Convert.ToInt32(DGVUnusual.SelectedRows[0].Cells["QCInfo_ID"].Value.ToString()) + "，质检车牌号为：" + this.txtCarNO.Text.Trim() + "，删除成功！";
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

                Common.WriteTextLog("异常信息处理 tsbDelete_Click()+" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                LoadData("");
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();

            }
        }

        /// <summary>
        /// 继续质检
        /// </summary>
        private void tsbGoOnClick()
        {
            if (MessageBox.Show("确定要修改状态为质检中吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool b = UpdateQCinfo("质检中");
                if (b)
                {


                    MessageBox.Show("操作成功！");
                }
            }
        }

        /// <summary>
        /// 暂停质检
        /// </summary>
        private void tsBsuspendClick()
        {
            if (MessageBox.Show("确定要修改状态为暂停质检吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool b = UpdateQCinfo("暂停质检");
                if (b)
                {
                    MessageBox.Show("操作成功！");
                }
            }
        }

        /// <summary>
        /// 修改质检状态
        /// </summary>
        /// <param name="dicName">字典名称</param>
        private bool UpdateQCinfo(string dicName)
        {
            int num = 0;

            DataTable statisIdDT = LinQBaseDao.Query("select Dictionary_ID from Dictionary where Dictionary_Name='" + dicName + "'").Tables[0];
            if (QCInfoIDS.Count > 0)
            {
                int statisId = Convert.ToInt32(statisIdDT.Rows[0][0]);
                foreach (int item in QCInfoIDS)//循环质检编号
                {
                    Expression<Func<QCInfo, bool>> fnQCiNFO = n => n.QCInfo_ID == item;
                    Action<QCInfo> ACQCInfo = q =>
                    {
                        q.QCInfo_Dictionary_ID = statisId;
                        q.QCInfo_RECV_RMA_METHOD = null;
                        q.QCInfo_REMARK = txtRemork.Text.Trim();
                    };
                    bool b = QCInfoDAL.Update(fnQCiNFO, ACQCInfo);
                    if (b == true)
                    {
                        Expression<Func<Unusual, bool>> fnUnusual = n => n.Unusual_QCInfo_ID == item && n.Unusual_State == "未处理";
                        var Unusuals = UnusualDAL.Query(fnUnusual);

                        foreach (var u in Unusuals)
                        {
                            Expression<Func<Unusual, bool>> fnU = n => n.Unusual_Id == u.Unusual_Id;
                            Action<Unusual> AcUnusual_Id = a =>
                            {
                                a.Unusual_State = "已处理";
                                a.Unusual_handle_UserId = Convert.ToInt32(Common.USERID);

                                if (dicName == "质检中")
                                {
                                    a.Unusual_handle_Result = "继续质检";
                                }
                                else
                                {
                                    a.Unusual_handle_Result = "暂停质检";

                                }
                                if (CardPeoples != "")
                                {
                                    a.Unusual_Remork = "异常处理授权人：" + CardPeoples;
                                }
                                else
                                {
                                    a.Unusual_Remork = "";
                                }
                            };
                            bool bl = UnusualDAL.Update(fnU, AcUnusual_Id);
                            if (bl)
                            {
                                num++;
                            }
                        }
                    }
                }
            }
            if (num > 0)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 退货
        /// </summary>
        private void tsbReturnedPurchaseClick()
        {


            if (MessageBox.Show("确定要退货吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                try
                {
                    bool b = UpdateQCinfo("质检中");//修改质检状态为继续质检
                    if (b == false)
                    {
                        MessageBox.Show("操作失败！");
                        return;
                    }
                    foreach (int item in QCInfoIDS)
                    {

                        Expression<Func<QCInfo, bool>> fnQCiNFO = n => n.QCInfo_ID == item;
                        Action<QCInfo> ACQCInfo = q =>
                        {
                            q.QCInfo_RECV_RMA_METHOD = "退货";
                            q.QCInfo_REMARK = txtRemork.Text.Trim();
                        };
                        bool bol = QCInfoDAL.Update(fnQCiNFO, ACQCInfo);
                        if (bol == true)
                        {
                            Expression<Func<Unusual, bool>> fnUnusual = n => n.Unusual_QCInfo_ID == item && n.Unusual_State == "未处理";
                            var Unusuals = UnusualDAL.Query(fnUnusual);

                            foreach (var u in Unusuals)
                            {
                                Expression<Func<Unusual, bool>> fnU = n => n.Unusual_Id == u.Unusual_Id;
                                Action<Unusual> AcUnusual_Id = a =>
                                {
                                    a.Unusual_State = "已处理";
                                    a.Unusual_handle_UserId = Convert.ToInt32(Common.USERID);
                                    a.Unusual_handle_Result = "退货";
                                    if (CardPeoples != "")
                                    {
                                        a.Unusual_Remork = "异常处理授权人：" + CardPeoples;
                                    }
                                    else
                                    {
                                        a.Unusual_Remork = "";
                                    }
                                };
                                UnusualDAL.Update(fnU, AcUnusual_Id);
                            }
                        }
                    }

                    MessageBox.Show("操作成功！");

                }
                catch
                {
                    MessageBox.Show("操作失败！");

                }
            }
        }


        /// <summary>
        /// 收货
        /// </summary>
        private void tsbShouHuoClick()
        {

            if (MessageBox.Show("确定要收货吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                try
                {



                    bool b = UpdateQCinfo("质检中");//修改质检状态为继续质检
                    if (b == false)
                    {
                        MessageBox.Show("操作失败！");
                        return;
                    }
                    foreach (int item in QCInfoIDS)
                    {

                        Expression<Func<QCInfo, bool>> fnQCiNFO = n => n.QCInfo_ID == item;
                        Action<QCInfo> ACQCInfo = q =>
                        {
                            q.QCInfo_RECV_RMA_METHOD = "收货";
                            q.QCInfo_REMARK = txtRemork.Text.Trim();
                        };
                        bool bol = QCInfoDAL.Update(fnQCiNFO, ACQCInfo);
                        if (bol == true)
                        {
                            Expression<Func<Unusual, bool>> fnUnusual = n => n.Unusual_QCInfo_ID == item && n.Unusual_State == "未处理";
                            var Unusuals = UnusualDAL.Query(fnUnusual);

                            foreach (var u in Unusuals)
                            {
                                Expression<Func<Unusual, bool>> fnU = n => n.Unusual_Id == u.Unusual_Id;
                                Action<Unusual> AcUnusual_Id = a =>
                                {
                                    a.Unusual_State = "已处理";
                                    a.Unusual_handle_UserId = Convert.ToInt32(Common.USERID);
                                    a.Unusual_handle_Result = "收货";
                                    if (CardPeoples != "")
                                    {
                                        a.Unusual_Remork = "异常处理授权人：" + CardPeoples;
                                    }
                                    else
                                    {
                                        a.Unusual_Remork = "";
                                    }
                                };
                                UnusualDAL.Update(fnU, AcUnusual_Id);
                            }

                        }

                    }


                    MessageBox.Show("操作成功！");

                }
                catch
                {
                    MessageBox.Show("操作失败！");

                }
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
            if (e.ClickedItem.Name == "tsbUpLoadPhoto")//上传照片
            {
                if (DGVUnusual.SelectedRows.Count == 1)
                {
                    int Unusual_Id = Convert.ToInt32(DGVUnusual.SelectedRows[0].Cells["Unusual_Id"].Value);
                    PrintUploadingForm puf = new PrintUploadingForm(Unusual_Id);
                    puf.Show();
                }
                if (DGVUnusual.SelectedRows.Count > 1)
                {
                    MessageBox.Show("只能选择一条异常信息");
                }
                else if (DGVUnusual.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请选择一条异常信息！");
                }
            }



            LoadData(e.ClickedItem.Name);
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
            LoadData("");
        }



        /// <summary>
        /// 绑定lable
        /// </summary>
        /// <param name="qcInfo_ID"></param>
        private void bindLable(int qcInfo_ID)
        {

            DataTable qcinfoDT = LinQBaseDao.Query("select * from View_QCInfo where QCInfo_ID=" + qcInfo_ID).Tables[0];
            if (qcinfoDT.Rows.Count > 0)
            {
                lblCNTR_NO.Text = qcinfoDT.Rows[0]["CNTR_NO"].ToString();
                lblNO_OF_BALES.Text = qcinfoDT.Rows[0]["NO_OF_BALES"].ToString();
                lblPO_NO.Text = qcinfoDT.Rows[0]["PO_NO"].ToString();
                lblPROD_ID.Text = qcinfoDT.Rows[0]["PROD_ID"].ToString();
                lblQCInfo_MATERIAL_WEIGHT.Text = qcinfoDT.Rows[0]["QCInfo_MATERIAL_WEIGHT"].ToString();
                lblQCInfo_MOIST_PER_SAMPLE.Text = qcinfoDT.Rows[0]["QCInfo_MOIST_PER_SAMPLE"].ToString();
                lblQCInfo_PAPER_SCALE.Text = qcinfoDT.Rows[0]["QCInfo_PAPER_SCALE"].ToString();
                lblQCInfo_PAPER_WEIGHT.Text = qcinfoDT.Rows[0]["QCInfo_PAPER_WEIGHT"].ToString();
                lblQCInfo_PumpingPackets.Text = qcinfoDT.Rows[0]["QCInfo_PumpingPackets"].ToString();

                lblQCInfo_UnpackBack_MOIST_COUNT.Text = qcinfoDT.Rows[0]["QCInfo_UnpackBack_MOIST_COUNT"].ToString();
                lblQCInfo_UnpackBack_MOIST_PER_SAMPLE.Text = qcinfoDT.Rows[0]["QCInfo_UnpackBack_MOIST_PER_SAMPLE"].ToString();
                lblQCInfo_UnpackBefore_MOIST_PER_COUNT.Text = qcinfoDT.Rows[0]["QCInfo_UnpackBefore_MOIST_PER_COUNT"].ToString();
                lblQCInfo_UnpackBefore_MOIST_PER_SAMPLE.Text = qcinfoDT.Rows[0]["QCInfo_UnpackBefore_MOIST_PER_SAMPLE"].ToString();

                lblREF_NO.Text = qcinfoDT.Rows[0]["REF_NO"].ToString();
                lblSHIPMENT_NO.Text = qcinfoDT.Rows[0]["SHIPMENT_NO"].ToString();
                lbltxtQCInfo_MATERIAL_SCALE.Text = qcinfoDT.Rows[0]["QCInfo_MATERIAL_SCALE"].ToString();
                lblWEIGHT_TICKET_NO.Text = qcinfoDT.Rows[0]["WEIGHT_TICKET_NO"].ToString();
                lblQCInfo_BAGWeight.Text = qcinfoDT.Rows[0]["QCInfo_BAGWeight"].ToString();

            }


        }
        private void DGVUnusual_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (DGVUnusual.SelectedRows[0].Cells["QCInfo_ID"].Value != null)
                {
                    bindLable(Convert.ToInt32(DGVUnusual.SelectedRows[0].Cells["QCInfo_ID"].Value));
                }

                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    Common.SetSelectionBackColor(DGVUnusual);

                    if ((bool)DGVUnusual.Rows[e.RowIndex].Cells["xz"].EditedFormattedValue != false)
                    {
                        DGVUnusual.Rows[e.RowIndex].Cells["xz"].Value = false;
                        DGVUnusual.Rows[e.RowIndex].Selected = false;
                        ids.Remove(e.RowIndex);
                        if (DGVUnusual.Rows[e.RowIndex].Cells["Unusual_Id"].Value != null)
                        {

                            ids.Remove(e.RowIndex);
                            int qcrid = EMEWE.Common.Converter.ToInt(DGVUnusual.Rows[e.RowIndex].Cells["Unusual_Id"].Value.ToString());
                            listrecordID.Remove(qcrid);
                            int id = EMEWE.Common.Converter.ToInt(DGVUnusual.Rows[e.RowIndex].Cells["QCInfo_ID"].Value.ToString());
                            QCInfoIDS.Remove(id);


                            int did = EMEWE.Common.Converter.ToInt(DGVUnusual.Rows[e.RowIndex].Cells["did"].Value.ToString());
                            DRAW_EXAM_INTERFACE_ID_IDs.Remove(did);


                        }
                    }
                    else
                    {
                        DGVUnusual.Rows[e.RowIndex].Cells["xz"].Value = true;
                        DGVUnusual.Rows[e.RowIndex].Selected = true;
                        ids.Add(e.RowIndex);
                        if (DGVUnusual.Rows[e.RowIndex].Cells["Unusual_Id"].Value != null)
                        {
                            ids.Add(e.RowIndex);
                            int qcrid = EMEWE.Common.Converter.ToInt(DGVUnusual.Rows[e.RowIndex].Cells["Unusual_Id"].Value.ToString());
                            listrecordID.Remove(qcrid);
                            listrecordID.Add(qcrid);

                            int id = EMEWE.Common.Converter.ToInt(DGVUnusual.Rows[e.RowIndex].Cells["QCInfo_ID"].Value.ToString());
                            QCInfoIDS.Remove(id);
                            QCInfoIDS.Add(id);

                            int did = EMEWE.Common.Converter.ToInt(DGVUnusual.Rows[e.RowIndex].Cells["did"].Value.ToString());
                            DRAW_EXAM_INTERFACE_ID_IDs.Remove(did);
                            DRAW_EXAM_INTERFACE_ID_IDs.Add(did);




                        }
                    }
                }
                //选中多行
                if (ids.Count > 0)
                {
                    for (int i = 0; i < ids.Count; i++)
                    {
                        DGVUnusual.Rows[ids[i]].Selected = true;
                        DGVUnusual.Rows[ids[i]].Cells["xz"].Value = true;
                    }
                }

            }
            catch
            {


            }


        }

        private void DGVUnusual_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DGVUnusual.SelectedRows[0].Cells["QCInfo_ID"].Value != null)
                {
                    bindLable(Convert.ToInt32(DGVUnusual.SelectedRows[0].Cells["QCInfo_ID"].Value));
                }
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 查看异常图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenImage_Click(object sender, EventArgs e)
        {
            int Unusual_Id = Convert.ToInt32(DGVUnusual.SelectedRows[0].Cells["Unusual_Id"].Value);
            UnusualPictureForm u = new UnusualPictureForm(Unusual_Id);
            u.Show();
        }



        /// <summary>
        /// 保存异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {

            if (QCInfoIDS.Count == 0)
            {
                MessageBox.Show("请选择需要处理的异常！");
                return;
            }
            if (rbtGoOn.Checked == false && tsBsuspend.Checked == false && rbtSou.Checked == false && rbtTui.Checked == false)
            {
                MessageBox.Show("处理结果未选择！");
                return;
            }



            if (rbtGoOn.Checked)
            {
                tsbGoOnClick();
            }
            if (tsBsuspend.Checked)
            {
                tsBsuspendClick();
            }




            //收货、退货


            if (rbtSou.Checked)
            {
                if (txtRemork.Text.Trim() == "")
                {
                    MessageBox.Show("异常收货必须填写备注！");
                    txtRemork.Focus();
                    return;
                }
                else
                {
                    tsbShouHuoClick();
                }

            }

            if (rbtTui.Checked)
            {
                if (txtRemork.Text.Trim() == "")
                {
                    MessageBox.Show("异常退货必须填写备注！");
                    txtRemork.Focus();
                    return;
                }
                else
                {
                    tsbReturnedPurchaseClick();

                }
            }
            rbtGoOn.Checked = false;
            rbtSou.Checked = false;
            rbtTui.Checked = false;
            tsBsuspend.Checked = false;

            LoadData("");
            txtRemork.Text = "";
            CardPeoples = "";
            cardControl1.TXTCARD = "";
            cardControl2.TXTCARD = "";
            cardControl1.userInfo = null;
            cardControl2.userInfo = null;
            BtnSave.Enabled = btnSaveIsEnabled;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cardControl1.userInfo != null && cardControl2.userInfo != null)
            {

                BtnSave.Enabled = true;
                CardPeoples = cardControl1.userInfo.UserName + "/" + cardControl2.userInfo.UserName;
            }

        }



    }

}

