using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEDAL;
using EMEWEQUALITY.HelpClass;
using EMEWE.Common;
using EMEWEEntity;
using System.Linq.Expressions;
using EMEWEQUALITY.GetPhoto;
using System.IO;
using EMEWE.SOFT.PRINT;
using System.Threading.Tasks;
using EMEWEQUALITY.SystemAdmin;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class FormWeight : Form
    {

        /// 页面参数
        PageControl page = new PageControl();
        public MainFrom mf;
        //int currentWeightNum = 1;
        public static FormWeight fw = null;
        /// <summary>
        /// 重量
        /// </summary>
        double weight = 0;
        /// <summary>
        /// 退货还包总重量
        /// </summary>
        double dReturnsWeight = 0;
        /// <summary>
        /// 退货还包数
        /// </summary>
        int iReturnsBag = 0;

        /// <summary>
        /// 重量检测项目编号
        /// 说明：由于使用频繁，所有在加载页面时获取编号
        /// </summary>
        static int iWeighItemId = 1;
        /// <summary>
        /// 当前质检编号
        /// 说明：tabPageWaitQC选择单条信息是赋值，选择下一条数据时重新赋值
        /// </summary>
        int iQcInfoID = 0;
        /// <summary>
        /// 复测编号
        /// 说明：tabPageWaitQC选择单条信息是赋值，在保存后清空，
        /// 如果是复测，则需要复测和质检编号两个条件才能进行操作
        /// </summary>
        int iQCRetest_ID = 0;

        /// <summary>
        /// 是否有修改数据dgv_SFJC
        ///说明： 修改数据打卡确认后ISUpdatedvgOne设置为“True”，第一条修改数据可以不验证水分仪序号，第一条修改数据获取仪表信息后将值付为"false"
        /// </summary>
        bool ISUpdatedvgOne = false;

        /// <summary>
        /// 选择列表中的质检记录编号
        /// 说明：选择列表dgvWateOne时将编号写入，修改完成或保存后清除
        /// </summary>
        List<int> listrecordID = new List<int>();

        /// <summary>
        /// 选中的复选框质检记录编号
        /// 说明：选择列表dgvWateOne时将编号写入，修改完成或保存后清除
        /// </summary>
        List<int> ids = new List<int>();

        /// <summary>
        /// 引用正在加载
        /// </summary>
        public Form_Loading form_loading = null;


        /// <summary>
        /// 生明无返回值的委托
        /// </summary>
        private delegate void AsynUpdateUI();

        public FormWeight()
        {
            InitializeComponent();
        }


        private void FormWeight_Load(object sender, EventArgs e)
        {

            TimeEndIsNull();
            BindData();
            timerCurrentWeight.Start();

            cbxWaitQCInfoState.SelectedIndex = 1;
            cbxWeighState.SelectedIndex = 1;

        }
        /// <summary>
        /// 初始化时间
        /// </summary>
        private void TimeEndIsNull()
        {
            DateTime Time = DateTime.Now.AddDays(0);
            txtbeginTime.Value = Time;
            txtendTime.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));
            DateTime Time1 = DateTime.Now.AddDays(0);
            txtbeginTime1.Value = Time1;
            txtendTime1.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 绑定重量检测状态下拉列表框数据
        /// </summary>
        private void BindCboxState()
        {
            List<Dictionary> list = DictionaryDAL.GetValueStateDictionary("质检状态");

            cboxState.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
            this.cboxState.DisplayMember = "Dictionary_Name";
            cboxState.ValueMember = "Dictionary_ID";
            if (cboxState.DataSource != null)
            {
                cboxState.SelectedIndex = 0;
            }

            cbxQCInfoState.DataSource = list;
            cbxQCInfoState.DisplayMember = "Dictionary_Name";
            cbxQCInfoState.ValueMember = "Dictionary_ID";
            if (cbxQCInfoState.DataSource != null)
            {
                cbxQCInfoState.SelectedIndex = 1;
            }
            cbxWaitQCInfoState.DataSource = list;
            this.cbxWaitQCInfoState.DisplayMember = "Dictionary_Name";
            cbxWaitQCInfoState.ValueMember = "Dictionary_ID";
            if (cbxWaitQCInfoState.DataSource != null)
            {

                cbxWaitQCInfoState.SelectedIndex = 1;
            }
        }
        /// <summary>
        /// 绑定界面数据
        /// </summary>
        private void BindData()
        {
            Common.WriteTextLog("绑定数据开始时间：" + DateTime.Now.ToString());
            iWeighItemId = TestItemsDAL.GetTestItemId("重量检测");
            BindCbxQCRecord_TARE();
            BindCboxTestItems();
            BindCboxState();
            //BindcbxQCInfoStatecbxWaitQCInfoState
            page = new PageControl();
            tscbxPageSize1.SelectedItem = "10";
            // page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
             BindDgvWaitQCOne("");
           // BindDgvWaitQC("");
            tscbxPageSize2.SelectedItem = "10";

        }
        /// <summary>
        /// 绑定预置皮重
        /// </summary>
        private void BindCbxQCRecord_TARE()
        {
            try
            {
                Expression<Func<PresetTare, bool>> fun = n => n.Dictionary.Dictionary_Value == "启动";
                List<PresetTare> list = PresetTareDAL.GetListPresetTare(fun);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        cbxQCRecord_TARE.DataSource = list;
                        cbxQCRecord_TARE.DisplayMember = "PresetTare_NAME";
                        cbxQCRecord_TARE.ValueMember = "PresetTare_WEIGH";
                        cbxQCRecord_TARE.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.BindCbxQCRecord_TARE异常：" + ex.Message);
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strQCState = "";
            switch (tabControlQCOne.SelectedTab.Name)
            {
                case "tabPageCurrentWeight"://当前质检记录
                    listrecordID.Clear();
                    if (lblCurrentQcState.Text == "质检中")
                    {
                        LoadDataCurrentWeight();
                        //shuXinLBL();
                    }
                    else if (lblCNTR_NO.Text.Trim() != "" && lblCurrentQcState.Text != "质检中")
                    {
                        MessageBox.Show("质检状态不为质检中，不能进行质检！");
                        tabControlQCOne.SelectedIndex = 0;
                    }
                    break;
                case "tabPageWeight"://质检记录数据
                    page = new PageControl();
                    page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                    LoadData("");
                    break;
                case "tabPageUpdateWeight"://修改检测数据 条件：修改检测数据必须选择修改行数和质检编号不能为空，质检状态未开始状态才能打开“确认”修改按钮
                    // ISOpenTabControl();
                    if (cbxQCInfoState.Text != "")
                    {
                        int stateID = cbxQCInfoState.SelectedIndex;
                        if (stateID >= 0)
                        {
                            Dictionary dic = cbxQCInfoState.SelectedItem as Dictionary;
                            strQCState = dic.Dictionary_Value;
                        }
                    }
                    if (listrecordID.Count > 0 && iQcInfoID > 0 && strQCState == "质检中")
                    {
                        btnOFFQcRecord.Enabled = true;
                        ISOpenSave();
                    }
                    else
                    {
                        btnOFFQcRecord.Enabled = false;
                    }

                    break;
                case "tabPageWaitWeight"://获取待测数据
                default:
                    page = new PageControl();
                    page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
                    BindDgvWaitQC("");
                    break;
            }
        }
        /// 待检测信息

        /// <summary>
        /// 打开或关闭检测保存、获取重量
        /// 2012-10-29 不进行验证屏蔽
        /// </summary>
        private void ISOpenSave()
        {
            //if (iQcInfoID > 0 && (string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text) || txtQCInfo_MATERIAL_WEIGHT.Text == "0.00" || string.IsNullOrEmpty(txtQCInfo_MATERIAL_SCALE.Text) || txtQCInfo_MATERIAL_SCALE.Text == "0.00" || string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text) || txtQCInfo_PAPER_WEIGHT.Text == "0.00" || string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text) || txtQCInfo_PAPER_SCALE.Text == "0.00" || string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text) || txtQCInfo_BAGWeight.Text == "0.00") && lblCurrentQcState.Text == "质检中")
            //{
            btnSave.Enabled = true;
            btnGetWeight.Enabled = true;
            btnWeightSave.Enabled = true;
            //}
            //else
            //{
            //    ISClose();
            //}

        }
        /// <summary>
        /// 待检测数据列表条件
        /// </summary>
        Expression<Func<View_QCInfo, bool>> waitQcP = null;
        /// <summary>
        /// 绑定等待质检数据
        /// </summary>
        private async void BindDgvWaitQC(string itemName)
        {
 
            form_loading = new Form_Loading();
            form_loading.ShowLoading(this);
            form_loading.Text = "重量数据加载中...";
            await GetWeightData(itemName);
            form_loading.CloseLoading(this);

        }

        private async void BindDgvWaitQCOne(string itemName)
        {          
            await GetWeightData(itemName);

        }

        private async Task GetWeightData(string itemName)
        {
            await Task.Factory.StartNew(() =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AsynUpdateUI(delegate ()
                    {
                        try
                        {
                            GetDgvWaitQCSeacher();
                            this.dgvWaitQC.AutoGenerateColumns = false;
                            dgvWaitQC.DataSource = page.BindBoundControl<View_QCInfo>(itemName, txtCurrentPage1, lblPageCount1, waitQcP, "QCInfo_ID desc");
                        }
                        catch (Exception ex)
                        {
                            Common.WriteTextLog("FormWeight.BindDgvWaitQC异常:" + ex.Message.ToString());
                        }

                    }));
                }
            });
        }

        /// <summary>
        /// 搜索待检测记录条件
        /// </summary>
        private void GetDgvWaitQCSeacher()
        {
            try
            {
                var i = 0;
                if (waitQcP == null)
                {
                    waitQcP = (Expression<Func<View_QCInfo, bool>>)PredicateExtensionses.True<View_QCInfo>();
                }
                if (!string.IsNullOrEmpty(cbbxReturnBag.Text))
                {
                    if (cbbxReturnBag.Text.Trim() == "是")
                    {
                        waitQcP = waitQcP.And(c => c.QCInfo_RETURNBAG_WEIGH != null);
                        i++;
                    }
                    else
                    {
                        waitQcP = waitQcP.And(c => c.QCInfo_RETURNBAG_WEIGH == null);
                        i++;
                    }
                }


                if (cbxWaitQCInfoState.SelectedValue != null)
                {
                    int stateID = Converter.ToInt(cbxWaitQCInfoState.SelectedValue.ToString());
                    if (stateID > 0)
                    {
                        waitQcP = waitQcP.And(n => n.Dictionary_ID == stateID);
                        i++;
                        /// 操作不方便
                        //Dictionary dicEntity = cbxWaitQCInfoState.SelectedItem as Dictionary;
                        //if (dicEntity.Dictionary_Value == "质检中")
                        //{
                        //    waitQcP = waitQcP.And(c => c.Dictionary_ID == stateID && (c.QCInfo_MATERIAL_WEIGHT == null || c.QCInfo_PAPER_WEIGHT == null || c.QCInfo_MATERIAL_SCALE == null || c.QCInfo_PAPER_SCALE == null));
                        //    i++;
                        //}
                        //else if (dicEntity.Dictionary_Value == "完成质检")
                        //{
                        //    waitQcP = waitQcP.And(c => c.Dictionary_ID == stateID || (c.Dictionary_ID == DictionaryDAL.GetDictionaryID("质检中") && (c.QCInfo_MATERIAL_WEIGHT != null || c.QCInfo_PAPER_WEIGHT != null || c.QCInfo_MATERIAL_SCALE != null || c.QCInfo_PAPER_SCALE != null)));
                        //    i++;
                        //}
                        //else
                        //{
                        //    waitQcP = waitQcP.And(n => n.Dictionary_ID == stateID);
                        //    i++;
                        //}

                    }
                }
                else
                {
                    waitQcP = waitQcP.And(c => c.Dictionary_Name == "质检中");
                    //waitQcP = waitQcP.And(c => c.Dictionary_Name == "质检中" && (c.QCInfo_MATERIAL_WEIGHT == null || c.QCInfo_PAPER_WEIGHT == null || c.QCInfo_MATERIAL_SCALE == null || c.QCInfo_PAPER_SCALE == null));
                    i++;

                }

                if (!string.IsNullOrEmpty(cbxWeighState.Text))
                {
                    string strcbxWaitQCInfoState = cbxWeighState.Text;


                    if (strcbxWaitQCInfoState == "已完成")
                    {
                        waitQcP = waitQcP.And(c => c.QCInfo_MATERIAL_WEIGHT > 0 && c.QCInfo_PAPER_WEIGHT > 0 && c.QCInfo_MATERIAL_SCALE > 0 && c.QCInfo_PAPER_SCALE > 0);

                        //   waitQcP = waitQcP.And(c => c.QCInfo_MATERIAL_WEIGHT != null || c.QCInfo_PAPER_WEIGHT != null || c.QCInfo_MATERIAL_SCALE != null || c.QCInfo_PAPER_SCALE != null);
                        i++;
                    }
                    else if (strcbxWaitQCInfoState == "未完成")
                    {
                        waitQcP = waitQcP.And(c => c.QCInfo_MATERIAL_WEIGHT == null || c.QCInfo_MATERIAL_WEIGHT <= 0 || c.QCInfo_PAPER_WEIGHT == null || c.QCInfo_PAPER_WEIGHT <= 0 || c.QCInfo_MATERIAL_SCALE == null || c.QCInfo_MATERIAL_SCALE <= 0 || c.QCInfo_PAPER_SCALE == null || c.QCInfo_PAPER_SCALE <= 0);
                        i++;
                    }
                }

                //else
                //{
                //    waitQcP = waitQcP.And(c => c.QCInfo_MATERIAL_WEIGHT == null || c.QCInfo_PAPER_WEIGHT == null || c.QCInfo_MATERIAL_SCALE == null || c.QCInfo_PAPER_SCALE == null);
                //    i++;

                //}


                if (!string.IsNullOrEmpty(txtWaitWEIGHTTICKETNO.Text))
                {
                    waitQcP = waitQcP.And(n => n.WEIGHT_TICKET_NO.Contains(txtWaitWEIGHTTICKETNO.Text.Trim().ToUpper()) || n.WEIGHT_TICKET_NO.Contains(txtWaitWEIGHTTICKETNO.Text.Trim().ToLower()));
                    i++;
                }
                if (txtWaitCarNo.Text.Trim() != "")
                {
                    waitQcP = waitQcP.And(n => n.CNTR_NO.Contains(txtWaitCarNo.Text.Trim().ToUpper()) || n.CNTR_NO.Contains(txtWaitCarNo.Text.Trim().ToLower()));
                    i++;
                }
                if (!string.IsNullOrEmpty(txtWaitPO_NO.Text))
                {
                    waitQcP = waitQcP.And(n => n.PO_NO.Contains(txtWaitPO_NO.Text.Trim().ToUpper()) || n.PO_NO.Contains(txtWaitPO_NO.Text.Trim().ToLower()));
                    i++;
                }
                if (!string.IsNullOrEmpty(txtWaitSHIPMENT_NO.Text))
                {
                    waitQcP = waitQcP.And(n => n.SHIPMENT_NO.Contains(txtWaitSHIPMENT_NO.Text.Trim().ToUpper()) || n.SHIPMENT_NO.Contains(txtWaitSHIPMENT_NO.Text.Trim().ToLower()));
                    i++;
                }
                string beginTime = txtbeginTime.Value.ToString("yyyy-MM -dd ");
                string endTime = txtendTime.Value.ToString("yyyy-MM -dd ");
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "
                if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                {
                    waitQcP = waitQcP.And(n => n.QCInfo_TIME > DateTime.Parse(begin) && n.QCInfo_TIME < DateTime.Parse(end));
                    i++;
                }
                if (i == 0)
                {
                    waitQcP = null;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.GetDgvWaitQCSeacher异常:" + ex.Message.ToString());
            }
        }


        private void bdnInfoWaitWeight_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tbtnNotSelect")//打印
            {
                printdemoformweiht();
                return;
            }
            BindDgvWaitQC(e.ClickedItem.Name);
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void printdemoformweiht()
        {
            try
            {
                if (dgvWaitQC.SelectedRows.Count > 0)//选中行
                {
                    if (dgvWaitQC.SelectedRows.Count > 1 || dgvWaitQC.SelectedRows[0].Cells["QCInfo_ID"].Value == null)
                    {
                        MessageBox.Show("打印只能选中一行且打印行质检编号信息不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        printdemoformweiht(Common.GetInt(dgvWaitQC.SelectedRows[0].Cells["QCInfo_ID"].Value.ToString()));
                    }
                }
                else
                {
                    MessageBox.Show("请选择中要打印的行", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.printdemoformweiht()" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="qcinfo_id">质检ID</param>
        private void printdemoformweiht(int qcinfo_id)
        {
            try
            {
                if (MessageBox.Show("是否确定打印吗？", "打印提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (qcinfo_id <= 0) return;
                    //  WeightPrint.PrintBorderFormweiht(qcinfo_id);
                     WeightPrint.PrintFormweiht(qcinfo_id);
                    //EMEWE.SOFT.PRINT.PrintWeight pdfw = new EMEWE.SOFT.PRINT.PrintWeight();
                    //Expression<Func<View_QCInfo, bool>> fun = n => n.QCInfo_ID == qcinfo_id;
                    //var viemqcinfointerface = QCInfoDAL.Query(fun);
                    //if (viemqcinfointerface == null) return;
                    //foreach (var m in viemqcinfointerface)
                    //{
                    //    if (m.QCInfo_ID > 0) //检测编号
                    //    {
                    //        pdfw.QcInfo_ID = m.QCInfo_ID.ToString();

                    //    }
                    //    if (!string.IsNullOrEmpty(m.CNTR_NO))//车牌号码
                    //    {
                    //        pdfw.CNTR_NO = m.CNTR_NO;
                    //    }
                    //    if (!string.IsNullOrEmpty(m.REF_NO))//REF_NO
                    //    {
                    //        pdfw.REF_NO = m.REF_NO;
                    //    }
                    //    if (!string.IsNullOrEmpty(m.PO_NO))//采购单号
                    //    {
                    //        pdfw.PO_NO = m.PO_NO;
                    //    }
                    //    if (!string.IsNullOrEmpty(m.SHIPMENT_NO))//送货通知书编号
                    //    {
                    //        pdfw.SHIPMENT_NO = m.SHIPMENT_NO;
                    //    }
                    //    if (m.NO_OF_BALES > 0)//送货件数
                    //    {
                    //        pdfw.NO_OF_BALES = m.NO_OF_BALES.ToString();
                    //    }
                    //    if (m.QCInfo_PumpingPackets > 0)//抽检件数
                    //    {
                    //        pdfw.QCInfo_PumpingPackets = m.QCInfo_PumpingPackets.ToString();
                    //    }
                    //    if (m.QCInfo_RETURNBAG_TIME != null)//退货还包称重时间
                    //    {
                    //        pdfw.QCInfo_RETURNBAG_TIME = m.QCInfo_RETURNBAG_TIME.ToString();
                    //    }
                    //    if (m.QCInfo_BAGWeight > 0) //抽检重量
                    //    {
                    //        pdfw.QCInfo_BAGWeight = m.QCInfo_BAGWeight.ToString();
                    //    }
                    //    if (m.QCInfo_RETURNBAG_WEIGH > 0)//退货重量
                    //    {
                    //        pdfw.QCInfo_RETURNBAG_WEIGH = m.QCInfo_RETURNBAG_WEIGH.ToString();
                    //    }
                    //    if (m.QCInfo_MATERIAL_WEIGHT > 0)//杂质重量
                    //    {
                    //        pdfw.QCInfo_MATERIAL_WEIGHT = m.QCInfo_MATERIAL_WEIGHT.ToString();
                    //    }

                    //    if (m.QCInfo_PAPER_WEIGHT > 0)//杂纸重量
                    //    {
                    //        pdfw.QCInfo_PAPER_WEIGHT = m.QCInfo_PAPER_WEIGHT.ToString();
                    //    }
                    //    if (!string.IsNullOrEmpty(m.PROD_ID))//货品
                    //    {
                    //        pdfw.PROD_ID = m.PROD_ID.ToString();
                    //    }
                    //    if (!string.IsNullOrEmpty(m.WEIGHT_TICKET_NO))//货品
                    //    {
                    //        pdfw.WEIGHT_TICKET_NO = m.WEIGHT_TICKET_NO.ToString();
                    //    }
                    //    if (!string.IsNullOrEmpty(m.ExtensionField2))
                    //    {
                    //        pdfw.ExtensionField2 = m.ExtensionField2;
                    //    }
                    //    break;

                    //}
                    //Expression<Func<View_QCRecordInfo, bool>> funcvierqcrecordinfo = n => n.QCInfo_ID == qcinfo_id && n.TestItems_NAME != "拆包后水分检测" && n.TestItems_NAME != "拆包前水分检测" && n.TestItems_NAME != "拆包前水分检测" && n.TestItems_NAME != "水分检测";
                    //var view_QCRecordInfo = from n in QCInfoDAL.Query(funcvierqcrecordinfo)
                    //                        orderby QCRecord_TIME descending
                    //                        select n;
                    //if (view_QCRecordInfo != null)
                    //{
                    //    foreach (var p in view_QCRecordInfo)
                    //    {
                    //        if (!string.IsNullOrEmpty(p.UserLoginId))//司称员
                    //        {
                    //            pdfw.QCRecord_Name = p.UserLoginId;
                    //        }
                    //        if (p.QCRecord_TIME != null)//抽检称重时间
                    //        {
                    //            pdfw.QCRecord_TIME = p.QCRecord_TIME.ToString();
                    //        }
                    //        break;
                    //    }
                    //}
                    //pdfw.PrintDemoFormWeightPrint();//打印
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.printdemoformweiht()" + ex.Message.ToString());
            }
        }
        private void tscbxPageSize1_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
            BindDgvWaitQC("");
        }

        /// <summary>
        /// 搜索待种类检测项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaitQCSeacher_Click(object sender, EventArgs e)
        {
            waitQcP = null;
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
            BindDgvWaitQC("");
        }

        /// <summary>
        /// 搜索待检测信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaitSeacher_Click(object sender, EventArgs e)
        {
            if (btnWaitSeacher.Enabled)
            {
                btnWaitSeacher.Enabled = false;
                waitQcP = null;
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
                BindDgvWaitQC("");

                btnWaitSeacher.Enabled = true;
            }
        }

        /// <summary>
        /// 关闭按钮
        /// 2012-10-29 不进行验证屏蔽
        /// </summary>
        private void ISClose()
        {
            //btnSave.Enabled = false;
            //btnGetWeight.Enabled = false;
            //btnCalculateAfter.Enabled = false;
            //btnWeightSave.Enabled = false;
            //btnOFFQcRecord.Enabled = false;
        }


        /// 重量检测
        /// <summary>
        /// 绑定重量检测项目
        /// </summary>
        private void BindCboxTestItems()
        {
            List<TestItems> list = TestItemsDAL.GetListWhereTestItemName("重量检测", "启动");
            cboxTestItems.DataSource = list;
            cboxTestItems.DisplayMember = "TestItems_NAME";
            cboxTestItems.ValueMember = "TestItems_ID";

            List<TestItems> list2 = TestItemsDAL.GetListWhereTestItemName("重量检测", "启动");
            cboxTestItems2.DataSource = list2;
            cboxTestItems2.DisplayMember = "TestItems_NAME";
            cboxTestItems2.ValueMember = "TestItems_ID";
            if (cboxTestItems2.DataSource != null)
            {
                cboxTestItems2.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 绑定当前选中行的包号
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        private void BindcbxBags(int iqcinfoID)
        {
            if (iqcinfoID > 0)
            {
                List<string> listBags = new List<string>();
                DataTable dt = LinQBaseDao.Query("select QCInfo_DRAW from QCInfo where QCInfo_ID=" + iqcinfoID).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                    {
                        string[] str = dt.Rows[0][0].ToString().Split(',');
                        foreach (string item in str)
                        {
                            listBags.Add(item);
                        }
                    }
                    listBags.Insert(0, "全部");
                }


                cbxBags.DataSource = listBags;
            }
            else
            { //退货还包
                List<int> list = new List<int>();
                for (int i = 0; i <= 10; i++)
                    list.Add(i);
                cbxBags.DataSource = list;
            }
        }

        /// <summary>
        /// 点击检测项目时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxTestItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxTestItems.SelectedIndex <= 0)
            {
                mf.ShowToolTip(ToolTipIcon.Info, "选择检测项目提示", "请您输入选择检测项目！", cboxTestItems, this);
                return;
            }
            else
            {
                if (iQcInfoID <= 0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "请您输入选择重量！", txtWeight, this);
                    return;
                }
                TestItems testitem = cboxTestItems.SelectedItem as TestItems;

                ///
                //yk2016.4.15修改
                //DataTable dt = LinQBaseDao.Query("select WeightSet_weightNum from View_WeightSet where TestItems_ID=" + testitem.TestItems_ID).Tables[0];

                //if (dt.Rows.Count > 0)
                //{
                //    currentWeightNum = Convert.ToInt32(dt.Rows[0][0]);

                //    if (currentWeightNum == 1)
                //    {
                //        lblWeightName.Text = "一号磅重量：";

                //    }
                //    else if (currentWeightNum == 2)
                //    {
                //        lblWeightName.Text = "二号磅重量：";
                //    }
                //}



                if (testitem.TestItems_NAME == "退货还包")//废纸包总重量不能为空
                {
                    if (string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text) || txtQCInfo_BAGWeight.Text == "0")
                    {
                        //cboxTestItems.SelectedIndex = 0;
                        mf.ShowToolTip(ToolTipIcon.Info, "选择检测项目提示", "当前车没有废纸包总重量不能进行退后还包！", txtQCInfo_BAGWeight, this);
                        return;
                    }
                    else
                    {
                        SetReturns();
                    }
                }
                else
                {
                    CloseReturns();
                    BindcbxBags(iQcInfoID);
                    ISOpenSave();
                }
            }
            //验证当前该检测的项目
            //获取重量

        }
        /// <summary>
        /// 定时获取当前重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCurrentWeight_Tick(object sender, EventArgs e)
        {
            try
            {
                //yk1026.4.15修改
                //if (currentWeightNum == 1)
                //{
                //    //lblCurrentWeight.Text = "1";
                //    //小磅1
                //    if (mf.isNewWeight)
                //    {
                //        lblCurrentWeight.Text = mf.NewWeight;
                //        mf.isNewWeight = false;
                //    }
                //}
                //else if (currentWeightNum == 2)
                //{
                //    //lblCurrentWeight.Text = "2";
                //    //小磅2
                //    if (mf.isNewWeight2)
                //    {
                //        lblCurrentWeight.Text = mf.NewWeight2;
                //        mf.isNewWeight2 = false;
                //    }
                //}

                //小磅1
                if (mf.isNewWeight)
                {
                    lblCurrentWeight.Text = mf.NewWeight;
                    mf.isNewWeight = false;
                }
                if (mf.isNewWeight2)
                {
                    lblCurrentWeight2.Text = mf.NewWeight2;
                    mf.isNewWeight2 = false;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.timerCurrentWeight_Tick" + ex.Message);
            }
        }

        /// <summary>
        /// 获取当前重量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetWeight_Click(object sender, EventArgs e)
        {
            //if (txtWeight.Text == "0" || txtWeight.Text == "")
            //{
            //    if (lblCurrentWeight.Text != "0")
            //    {
            //        weight = Convert.ToDouble(lblCurrentWeight.Text);
            //      //  txtWeight.Text = weight.ToString();

            //        double dTARE = 0;//皮重
            //        if (cbxQCRecord_TARE.SelectedIndex > -1)
            //        {
            //            dTARE = EMEWE.Common.Converter.ToDouble(cbxQCRecord_TARE.SelectedValue.ToString(), 0);
            //        }
            //        lblCurrentWeight.Text = (dReturnsWeight + weight - dTARE).ToString();
            //    }
            //}
            //if (!string.IsNullOrEmpty(lblCurrentWeight.Text))
            //{
            //    txtWeight.Text = lblCurrentWeight.Text;
            //}
            txtWeight.Text = lblCurrentWeight.Text;
            ShowNetWeight();
            
        }

        private void buttonGetWeight2_Click(object sender, EventArgs e)
        {
            txtWeight.Text = lblCurrentWeight2.Text;
            ShowNetWeight();
        }

        #region 计算净重
        private void ShowNetWeight()
        {
            try
            {
                double weight = double.Parse(txtWeight.Text);
                double netWeight = weight - GetPresetTare();
                txtNetWeight.Text = Convert.ToString(netWeight);
            }
            catch
            {
                txtNetWeight.Text = "";
            }
        }

        private double GetPresetTare()
        {
            if (cbxQCRecord_TARE.SelectedIndex > -1)
            {
                 return EMEWE.Common.Converter.ToDouble(cbxQCRecord_TARE.SelectedValue.ToString(), 0);
            }
            throw new Exception();
        }
        #endregion

        /// <summary>
        /// 判断是否现场抽包
        /// </summary>
        /// <param name="qcid"></param>
        /// <returns></returns>
        private bool Packets(int qcid)
        {
            string strsql = " select Packets_this from Packets where Packets_QCInfo_DRAW_EXAM_INTERFACE_ID in (select QCInfo_DRAW_EXAM_INTERFACE_ID from QCInfo where QCInfo_ID=" + qcid + ")";
            DataTable dtpack = LinQBaseDao.Query(strsql).Tables[0];
            if (dtpack.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtpack.Rows[0][0].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 重量检测保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled)
            {
                btnSave.Enabled = false;
                int i = 0;
                string strItemName = "";
                TestItems testitem = null;

                if (Common.ISPackets == "是")
                {
                    if (!Packets(iQcInfoID))
                    {
                        MessageBox.Show(this, "还没有抽包，请抽包后检测重量！");
                        return;
                    }
                }

                #region ykNote:加入包号自由选择功能后，需要使用
                //if (string.IsNullOrEmpty(textBoxBagsID.Text))
                //{
                //    btnSave.Enabled = true;
                //    MessageBox.Show(this, "请选择包。");
                //    return;
                //}
                #endregion

                List<int> bags = new List<int>();               
                if (cbxBags.Text == "全部")  
                {
                    if (cbxBags.Items.Count == 1)
                    {
                        MessageBox.Show("没有废纸包序号，请抽包后添加数据！");
                        return;
                    }

                    if (cboxTestItems.Text == "杂质" || cboxTestItems.Text == "杂纸")
                    {
                        bags.Add(0);
                    }
                    else
                    {
                        for (int j = 1; j < cbxBags.Items.Count; j++)
                        {
                            if (!string.IsNullOrEmpty(cbxBags.Items[j].ToString()))
                            {
                                bags.Add(Convert.ToInt32(cbxBags.Items[j].ToString()));
                            }
                        }
                    }
                }
                else
                {

                    bags.Add(Convert.ToInt32(cbxBags.Text));
                }

                if (iQcInfoID > 0)
                {
                    DataTable dt = LinQBaseDao.Query("select QCInfo_RECV_RMA_METHOD  from QCInfo where QCInfo_RECV_RMA_METHOD!='' and QCInfo_ID=" + iQcInfoID).Tables[0];

                    if (dt.Rows.Count > 0 && cboxTestItems.Text != "退货还包")
                    {
                        MessageBox.Show("质检车辆为退货状态下只能进行退货还包！");
                        return;
                    }
                }

                /// 判断是否为空
                Expression<Func<QCRecord, bool>> exprQcrecord = (Expression<Func<QCRecord, bool>>)PredicateExtensionses.True<QCRecord>();
                int itemId = 0;
                decimal dweight;//除去皮重的重量
                decimal dTARE = 0; //EMEWE.Common.Converter.ToDecimal(txtQCRecord_TARE.Text.Trim(), 0);//皮重
                if (cbxQCRecord_TARE.SelectedIndex > -1)
                {
                    dTARE = EMEWE.Common.Converter.ToDecimal(cbxQCRecord_TARE.SelectedValue.ToString(), 0);
                }

                if (iQcInfoID <= 0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "请在检测信息中选择开始检测状态的信息继续质检！", dgvWaitQC, this);
                    btnSave.Enabled = true;
                    return;
                }
                else
                {
                    exprQcrecord = exprQcrecord.And(c => (c.QCRecord_QCInfo_ID == iQcInfoID));
                    i++;
                }
                if (iQCRetest_ID > 0)
                {
                    exprQcrecord = exprQcrecord.And(c => (c.QCRecord_QCRetest_ID == iQCRetest_ID));
                    i++;
                }
                if (cboxTestItems.SelectedIndex <= 0)
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "当前检测项目不能质检，请您输入选择检测项目！", txtWeight, this); btnSave.Enabled = true;
                    return;
                }
                else
                {
                    itemId = Converter.ToInt(cboxTestItems.SelectedValue.ToString());

                    testitem = cboxTestItems.SelectedItem as TestItems;
                    strItemName = testitem.TestItems_NAME;
                    if (testitem.TestItems_NAME == "废纸包重" || testitem.TestItems_NAME == "退货还包")//当检测项目废纸包重时需加上包号
                    {
                        //int ibag = 0;
                        //if (cbxBags.SelectedIndex > 0)
                        //{
                        //    ibag = Converter.ToInt(cbxBags.SelectedValue.ToString());
                        //    if (ibag <= 0)
                        //    {
                        //        mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "当前检测项目为废纸包重货或退货还包，请您输入选择废纸包序号！", cbxBags, this);
                        //        btnSave.Enabled = true;
                        //        return;
                        //    }
                        //}
                        //else
                        //{
                        //    mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "当前检测项目为废纸包重货或退货还包，请您输入选择废纸包序号！", cbxBags, this);
                        //    btnSave.Enabled = true;
                        //    return;
                        //}
                        //exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId && c.QCRecord_DRAW == ibag));
                    }
                    else if (testitem.TestItems_NAME == "杂质" || testitem.TestItems_NAME == "杂纸")
                    {
                        int ibag = 0;
                        if (cbxBags.SelectedIndex > 0)
                        {
                            ibag = Converter.ToInt(cbxBags.SelectedValue.ToString());
                            if (ibag > 0)
                            {
                                exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId && c.QCRecord_DRAW == ibag));
                            }
                            else
                            {
                                exprQcrecord = exprQcrecord.And(c => c.QCRecord_TestItems_ID == itemId);
                            }
                        }
                        else
                        {
                            exprQcrecord = exprQcrecord.And(c => c.QCRecord_TestItems_ID == itemId);
                        }
                    }
                    else
                    {
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId));
                        i++;
                    }
                    i++;
                }


                if (string.IsNullOrEmpty(txtWeight.Text) || txtWeight.Text.Trim() == "0")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "请您点击重量文本框获取当前磅上重量！", txtWeight, this);
                    btnSave.Enabled = true;
                    return;
                }
                else
                {
                    dweight = EMEWE.Common.Converter.ToDecimal(txtWeight.Text.Trim()) - dTARE;
                }

                int idictionaryId = DictionaryDAL.GetDictionaryID("启动");
                if (idictionaryId > 0)
                {
                    exprQcrecord = exprQcrecord.And(c => (c.QCRecord_Dictionary_ID == idictionaryId));
                    i++;
                }


                int num = 0;
                foreach (int item in bags)
                {
                    QCRecord qcRecord = new QCRecord();
                    qcRecord.QCRecord_QCInfo_ID = iQcInfoID;
                    if (iQCRetest_ID > 0)
                    {
                        qcRecord.QCRecord_QCRetest_ID = iQCRetest_ID;
                    }
                    qcRecord.QCRecord_Dictionary_ID = idictionaryId;
                    qcRecord.QCRecord_Client_ID = Common.CLIENTID;
                    qcRecord.QCRecord_ISRETEST = false;
                    qcRecord.QCRecord_UserId = Converter.ToInt(Common.USERID);
                    qcRecord.QCRecord_TIME = DateTime.Now;
                    qcRecord.QCRecord_TestItems_ID = itemId;
                    qcRecord.QCRecord_RESULT = dweight / bags.Count;
                    qcRecord.QCRecord_TARE = dTARE;//皮重
                    qcRecord.QCRecord_ISLEDSHOW = false;

                    if (item > 0)
                    {
                        qcRecord.QCRecord_DRAW = item;
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId && c.QCRecord_DRAW == item));
                    }
                    else
                    {
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId));
                    }


                    int rint = 0;
                    if (QCRecordDAL.InsertOneQCRecord(qcRecord, out rint))
                    {
                        CapturePic(rint, cboxTestItems.Text.ToString());//拍照

                        string strContent = "质检编号为：" + iQcInfoID.ToString() + "，新增";
                        strContent += "重量检测" + strItemName + "项目重量：" + qcRecord.QCRecord_RESULT.ToString() + "，数据编号：" + rint;
                        Common.WriteLogData("新增", strContent, "");
                        num++;
                    }
                }
                string strContents = "质检编号为：" + iQcInfoID.ToString() + "，新增";
                strContents += "重量检测" + strItemName + "项目总毛重为：" + txtWeight.Text;
                Common.WriteLogData("新增", strContents, "");
                if (num == bags.Count)
                {
                    if (strItemName == "退货还包")
                    {
                        ///  退后还包总重量
                        dReturnsWeight = dReturnsWeight + EMEWE.Common.Converter.ToDouble(dweight.ToString());
                        iReturnsBag++;
                        lblTotalWeight.Text = dReturnsWeight.ToString();

                    }
                    /// 清除当前重量
                    txtWeight.Text = "0";
                    txtNetWeight.Text = "";
                    mf.weight = 0;
                    lblCurrentWeight.Text = "0";


                    LoadDataCurrentWeight();
                    tabControlQCOne.SelectedIndex = 1;

                    if (cboxTestItems.Text == "杂质")
                    {
                        lblQCInfo_MATERIALWeightWay.Text = "过磅";
                    }
                    else if (cboxTestItems.Text == "杂纸")
                    {
                        lblQCInfo_PAPERWeightWay.Text = "过磅";
                    }
                    MessageBox.Show(" 保存成功！", "检测重量信息", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(" 保存失败！", "检测重量信息", MessageBoxButtons.OK,
                                                   MessageBoxIcon.Error);
                    btnSave.Enabled = true;
                    return;
                } //当前行已检测过不能重复检测，请选中未检测行

                btnSave.Enabled = true;
                CalculateAfterAVG();
            }
        }


        /// 杨敦钦目测杂质算法
        /// <summary>
        /// 目测杂质
        /// </summary>
        private void mucezazhizidongjisuans()
        {
            try
            {
                double zongzhong = double.Parse(txtQCInfo_BAGWeight.Text);
                double bili = double.Parse(txtQCInfo_MATERIAL_SCALE.Text) / 100;
                if (zongzhong > 0 && bili > 0)
                {
                    txtQCInfo_MATERIAL_WEIGHT.Text = (zongzhong * (bili)).ToString();
                    int i = 0;
                    string strItemName = "";
                    TestItems testitem = null;
                    List<int> bags = new List<int>();
                    bags.Add(0);

                    if (iQcInfoID > 0)
                    {
                        DataTable dt = LinQBaseDao.Query("select QCInfo_RECV_RMA_METHOD  from QCInfo where QCInfo_RECV_RMA_METHOD!='' and QCInfo_ID=" + iQcInfoID).Tables[0];
                        if (dt.Rows.Count > 0 && "杂质" != "退货还包")
                        {
                            MessageBox.Show("质检车辆为退货状态下只能进行退货还包！");
                            return;
                        }
                    }

                    /// 判断是否为空
                    Expression<Func<QCRecord, bool>> exprQcrecord = (Expression<Func<QCRecord, bool>>)PredicateExtensionses.True<QCRecord>();
                    int itemId = 0;
                    decimal dweight;//除去皮重的重量
                    if (iQcInfoID <= 0)
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "请在检测信息中选择开始检测状态的信息继续质检！", dgvWaitQC, this);
                        return;
                    }
                    else
                    {
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_QCInfo_ID == iQcInfoID));
                        i++;
                    }
                    if (iQCRetest_ID > 0)
                    {
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_QCRetest_ID == iQCRetest_ID));
                        i++;
                    }
                    DataSet ds = LinQBaseDao.Query("select TestItems_ID from dbo.TestItems where TestItems_NAME='杂质'");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        itemId = Converter.ToInt(ds.Tables[0].Rows[0][0].ToString());//检测项目ID
                        strItemName = "杂质";
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId));
                        i++;
                    }
                    else
                    {
                        return;
                    }


                    if (string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text) || txtQCInfo_MATERIAL_WEIGHT.Text.Trim() == "0")//重量值
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "请您点击重量文本框获取当前磅上重量！", txtWeight, this);
                        btnSave.Enabled = true;
                        return;
                    }
                    else
                    {
                        dweight = EMEWE.Common.Converter.ToDecimal(txtQCInfo_MATERIAL_WEIGHT.Text.Trim());//重量值
                    }

                    int idictionaryId = DictionaryDAL.GetDictionaryID("启动");
                    if (idictionaryId > 0)
                    {
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_Dictionary_ID == idictionaryId));
                        i++;
                    }

                    int num = 0;
                    foreach (int item in bags)
                    {
                        QCRecord qcRecord = new QCRecord();
                        qcRecord.QCRecord_QCInfo_ID = iQcInfoID;
                        if (iQCRetest_ID > 0)
                        {
                            qcRecord.QCRecord_QCRetest_ID = iQCRetest_ID;
                        }
                        qcRecord.QCRecord_Dictionary_ID = idictionaryId;
                        qcRecord.QCRecord_Client_ID = Common.CLIENTID;
                        qcRecord.QCRecord_ISRETEST = false;
                        qcRecord.QCRecord_UserId = Converter.ToInt(Common.USERID);
                        qcRecord.QCRecord_TIME = DateTime.Now;
                        qcRecord.QCRecord_TestItems_ID = itemId;
                        qcRecord.QCRecord_RESULT = dweight / bags.Count;
                        qcRecord.QCRecord_ISLEDSHOW = false;

                        if (item > 0)
                        {
                            qcRecord.QCRecord_DRAW = item;
                            exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId && c.QCRecord_DRAW == item));
                        }
                        else
                        {
                            exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId));
                        }


                        int rint = 0;
                        if (QCRecordDAL.InsertOneQCRecord(qcRecord, out rint))
                        {
                            //CapturePic(rint, cboxTestItems.Text.ToString());//拍照
                            string strContent = "质检编号为：" + iQcInfoID.ToString() + "，新增";
                            strContent += "重量检测" + strItemName + "项目重量：" + qcRecord.QCRecord_RESULT.ToString() + "，数据编号：" + rint;
                            Common.WriteLogData("新增", strContent, "");
                            num++;
                        }
                    }
                    if (num == bags.Count)
                    {
                        LoadDataCurrentWeight();
                        tabControlQCOne.SelectedIndex = 1;
                        lblQCInfo_PAPERWeightWay.Text = "过磅";
                    }
                    else
                    {
                        return;
                    } //当前行已检测过不能重复检测，请选中未检测行
                    CalculateAfterAVG();
                }
            }
            catch (Exception)
            {
            }


        }

        /// 杨敦钦目测杂质算法

        /// <summary>
        /// 杂纸目测
        /// </summary>
        private void mucezazhis()
        {
            try
            {
                double zongzhong = double.Parse(txtQCInfo_BAGWeight.Text);
                double bili = double.Parse(txtQCInfo_PAPER_SCALE.Text) / 100;
                if (zongzhong > 0 && bili > 0)
                {
                    txtQCInfo_PAPER_WEIGHT.Text = (zongzhong * (bili)).ToString();
                    int i = 0;
                    string strItemName = "";
                    TestItems testitem = null;
                    List<int> bags = new List<int>();
                    bags.Add(0);

                    if (iQcInfoID > 0)
                    {
                        DataTable dt = LinQBaseDao.Query("select QCInfo_RECV_RMA_METHOD  from QCInfo where QCInfo_RECV_RMA_METHOD!='' and QCInfo_ID=" + iQcInfoID).Tables[0];
                        if (dt.Rows.Count > 0 && "杂质" != "退货还包")
                        {
                            MessageBox.Show("质检车辆为退货状态下只能进行退货还包！");
                            return;
                        }
                    }

                    /// 判断是否为空
                    Expression<Func<QCRecord, bool>> exprQcrecord = (Expression<Func<QCRecord, bool>>)PredicateExtensionses.True<QCRecord>();
                    int itemId = 0;
                    decimal dweight;//除去皮重的重量
                    if (iQcInfoID <= 0)
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "请在检测信息中选择开始检测状态的信息继续质检！", dgvWaitQC, this);
                        return;
                    }
                    else
                    {
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_QCInfo_ID == iQcInfoID));
                        i++;
                    }
                    if (iQCRetest_ID > 0)
                    {
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_QCRetest_ID == iQCRetest_ID));
                        i++;
                    }
                    DataSet ds = LinQBaseDao.Query("select TestItems_ID from dbo.TestItems where TestItems_NAME='杂纸'");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        itemId = Converter.ToInt(ds.Tables[0].Rows[0][0].ToString());//检测项目ID
                        strItemName = "杂纸";
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId));
                        i++;
                    }
                    else
                    {
                        return;
                    }


                    if (string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text) || txtQCInfo_PAPER_WEIGHT.Text.Trim() == "0")//重量值
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "选择重量质检提示", "请您点击重量文本框获取当前磅上重量！", txtWeight, this);
                        btnSave.Enabled = true;
                        return;
                    }
                    else
                    {
                        dweight = EMEWE.Common.Converter.ToDecimal(txtQCInfo_PAPER_WEIGHT.Text.Trim());//重量值
                    }

                    int idictionaryId = DictionaryDAL.GetDictionaryID("启动");
                    if (idictionaryId > 0)
                    {
                        exprQcrecord = exprQcrecord.And(c => (c.QCRecord_Dictionary_ID == idictionaryId));
                        i++;
                    }

                    int num = 0;
                    foreach (int item in bags)
                    {
                        QCRecord qcRecord = new QCRecord();
                        qcRecord.QCRecord_QCInfo_ID = iQcInfoID;
                        if (iQCRetest_ID > 0)
                        {
                            qcRecord.QCRecord_QCRetest_ID = iQCRetest_ID;
                        }
                        qcRecord.QCRecord_Dictionary_ID = idictionaryId;
                        qcRecord.QCRecord_Client_ID = Common.CLIENTID;
                        qcRecord.QCRecord_ISRETEST = false;
                        qcRecord.QCRecord_UserId = Converter.ToInt(Common.USERID);
                        qcRecord.QCRecord_TIME = DateTime.Now;
                        qcRecord.QCRecord_TestItems_ID = itemId;
                        qcRecord.QCRecord_RESULT = dweight / bags.Count;

                        qcRecord.QCRecord_ISLEDSHOW = false;

                        if (item > 0)
                        {
                            qcRecord.QCRecord_DRAW = item;
                            exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId && c.QCRecord_DRAW == item));
                        }
                        else
                        {
                            exprQcrecord = exprQcrecord.And(c => (c.QCRecord_TestItems_ID == itemId));
                        }


                        int rint = 0;
                        if (QCRecordDAL.InsertOneQCRecord(qcRecord, out rint))
                        {
                            //CapturePic(rint, cboxTestItems.Text.ToString());//拍照
                            string strContent = "质检编号为：" + iQcInfoID.ToString() + "，新增";
                            strContent += "重量检测" + strItemName + "项目重量：" + qcRecord.QCRecord_RESULT.ToString() + "，数据编号：" + rint;
                            Common.WriteLogData("新增", strContent, "");
                            num++;
                        }
                    }

                    if (num == bags.Count)
                    {
                        LoadDataCurrentWeight();
                        tabControlQCOne.SelectedIndex = 1;
                        lblQCInfo_PAPERWeightWay.Text = "过磅";
                    }
                    else
                    {
                        return;
                    } //当前行已检测过不能重复检测，请选中未检测行
                    CalculateAfterAVG();
                }
            }
            catch (Exception)
            {
            }


        }


        /// 当前质检记录
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadDataCurrentWeight()
        {

            try
            {
                string CurrentQcState = lblCurrentQcState.Text;
                dgvCurentWeight.DataSource = null;
                //GetCondition();
                dgvCurentWeight.AutoGenerateColumns = false;
                if (iQcInfoID > 0)
                {
                    List<View_QCRecordInfo> list = QCRecordDAL.GetListQCRecordWate(iQcInfoID, iQCRetest_ID, "重量检测", "启动", "启动");
                    if (list.Count > 0)
                    {
                        dgvCurentWeight.DataSource = list;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.LoadData异常：" + ex.Message.ToString());
            }

        }

        private void dgvCurentWeight_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                Common.SetSelectionBackColor(dgvCurentWeight);
                if ((bool)dgvCurentWeight.Rows[e.RowIndex].Cells["xz2"].EditedFormattedValue != false)
                {
                    dgvCurentWeight.Rows[e.RowIndex].Cells["xz2"].Value = false;
                    dgvCurentWeight.Rows[e.RowIndex].Selected = false;
                    ids.Remove(e.RowIndex);
                    if (dgvCurentWeight.Rows[e.RowIndex].Cells["QCRecord_ID"].Value != null)
                    {
                        ids.Remove(e.RowIndex);
                        int qcrid = EMEWE.Common.Converter.ToInt(dgvCurentWeight.Rows[e.RowIndex].Cells["QCRecord_ID"].Value.ToString());
                        listrecordID.Remove(qcrid);
                    }
                }
                else
                {
                    dgvCurentWeight.Rows[e.RowIndex].Cells["xz2"].Value = true;
                    dgvCurentWeight.Rows[e.RowIndex].Selected = true;
                    ids.Add(e.RowIndex);
                    if (dgvCurentWeight.Rows[e.RowIndex].Cells["QCRecord_ID"].Value != null)
                    {
                        ids.Add(e.RowIndex);
                        int qcrid = EMEWE.Common.Converter.ToInt(dgvCurentWeight.Rows[e.RowIndex].Cells["QCRecord_ID"].Value.ToString());
                        listrecordID.Remove(qcrid);
                        listrecordID.Add(qcrid);
                    }
                }
            }
            //选中多行
            if (ids.Count > 0)
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    dgvCurentWeight.Rows[ids[i]].Selected = true;
                }
            }
        }

        /// 重量单条信息搜索
        /// <summary>
        /// 单条记录列表条件
        /// </summary>
        Expression<Func<View_QCRecordInfo, bool>> expr = null;

        /// <summary>
        /// 搜索单条质检记录条件
        /// </summary>
        private void GetCondition()
        {
            try
            {
                if (expr == null)
                {
                    expr = (Expression<Func<View_QCRecordInfo, bool>>)PredicateExtensionses.True<View_QCRecordInfo>();
                }
                var i = 0;


                if (!string.IsNullOrEmpty(txtWEIGHT_TICKET_NO.Text))
                {
                    expr = expr.And(c => (c.WEIGHT_TICKET_NO.Contains(txtWEIGHT_TICKET_NO.Text.Trim().ToUpper()) || c.WEIGHT_TICKET_NO.Contains(txtWEIGHT_TICKET_NO.Text.Trim().ToLower())));

                    i++;
                }
                if (!string.IsNullOrEmpty(txtCarNO.Text))
                {
                    expr = expr.And(c => (c.CNTR_NO.Contains(txtCarNO.Text.Trim().ToUpper()) || c.CNTR_NO.Contains(txtCarNO.Text.Trim().ToLower())));
                    i++;
                }
                if (cboxState.SelectedValue != null)
                {
                    int stateID = Converter.ToInt(cboxState.SelectedValue.ToString());

                    if (stateID > 0)
                    {
                        expr = expr.And(c => (c.Dictionary_ID == stateID));
                        i++;
                    }

                }
                if (cboxTestItems2.SelectedIndex > -1)
                {
                    int itemId = Converter.ToInt(cboxTestItems2.SelectedValue.ToString());
                    if (itemId > -1)
                    {
                        TestItems items = cboxTestItems2.SelectedItem as TestItems;
                        if (items.TestItems_NAME == "重量检测")
                        {
                            expr = expr.And(c => (c.Tes_TestItems_ID == itemId));
                        }
                        else
                            expr = expr.And(c => (c.TestItems_ID == itemId));
                        i++;

                    }
                }

                if (!string.IsNullOrEmpty(txtPO_NO.Text))
                {
                    expr = expr.And(c => (c.PO_NO.Contains(txtPO_NO.Text.Trim().ToUpper()) || c.PO_NO.Contains(txtPO_NO.Text.Trim().ToLower())));

                    i++;
                }
                if (!string.IsNullOrEmpty(txtSHIPMENT_NO.Text))
                {
                    expr = expr.And(c => (c.SHIPMENT_NO.Contains(txtSHIPMENT_NO.Text.Trim().ToUpper()) || c.SHIPMENT_NO.Contains(txtSHIPMENT_NO.Text.Trim().ToLower())));
                    i++;
                }
                string beginTime = txtbeginTime1.Value.ToString("yyyy-MM -dd ");
                string endTime = txtendTime1.Value.ToString("yyyy-MM -dd ");
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "
                if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                {
                    expr = expr.And(c => (c.QCRecord_TIME >= DateTime.Parse(begin) && c.QCRecord_TIME <= DateTime.Parse(endTime)));
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
        /// 加载数据
        /// </summary>
        private void LoadData(string Name)
        {

            try
            {
                dgv_SFJC.DataSource = null;
                GetCondition();
                dgv_SFJC.AutoGenerateColumns = false;
                dgv_SFJC.DataSource = page.BindBoundControl<View_QCRecordInfo>(Name, txtCurrentPage2, lblPageCount2, expr, "QCRecord_TIME desc");

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.LoadData异常：" + ex.Message.ToString());
            }

        }
        private void btnQCOneInfo_Click(object sender, EventArgs e)
        {
            if (btnQCInfo.Enabled)
            {
                btnQCInfo.Enabled = false;
                expr = null;
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData("");
                btnQCInfo.Enabled = true;
            }
        }



        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LoadData(e.ClickedItem.Name);

        }

        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {

            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData("");
        }






        /// 修改重量质检信息
        private void SetCarControlISNUll(string strcardControl1, string strcardControl2, string strcardControl3)
        {
            if (string.IsNullOrEmpty(strcardControl1))
            {
                cardControl1.ISPass = true;
                cardControl1.TXTCARD = "";
                cardControl1.LblName = "卡号1:";
            }
            if (string.IsNullOrEmpty(strcardControl2))
            {

                cardControl2.ISPass = true;
                cardControl2.TXTCARD = "";
                cardControl2.LblName = "卡号2:";
            }
            if (string.IsNullOrEmpty(strcardControl3))
            {
                cardControl3.ISPass = true;
                cardControl3.TXTCARD = "";
                cardControl3.LblName = "卡号3:";
            }
        }
        /// <summary>
        /// 确认修改信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOFFQcRecord_Click(object sender, EventArgs e)
        {
            if (btnOFFQcRecord.Enabled)
            {
                btnOFFQcRecord.Enabled = false;
                int userId1 = 0;
                int userId2 = 0;
                int userId3 = 0;
                string strcardControl1 = cardControl1.TXTCARD;
                string strcardControl2 = cardControl2.TXTCARD;
                string strcardControl3 = cardControl3.TXTCARD;
                int icountList = listrecordID.Count;
                try
                {
                    if (listrecordID.Count <= 0)
                    {
                        tabControlQCOne.SelectedTab.Name = "tabPageWeight";
                        mf.ShowToolTip(ToolTipIcon.Warning, "修改", "请选择修改项", dgv_SFJC, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQCRecord_UPDATE_REASON.Text.ToString()))
                    {
                        mf.ShowToolTip(ToolTipIcon.Warning, "确认修改", "修改原因不能为空", txtQCRecord_UPDATE_REASON, this);
                        return;
                    }
                    /// 这个三个顺序不能颠倒 ,界面依次放，验证时依次放
                    if (!mf.VerificationCard(cardControl1, this, out userId1))
                    {
                        btnOFFQcRecord.Enabled = true;
                        SetCarControlISNUll("", strcardControl2, strcardControl3);
                        return;
                    }
                    else if (cardControl1.LblCardNO == cardControl2.LblCardNO || cardControl1.LblCardNO == cardControl3.LblCardNO)
                    {
                        btnOFFQcRecord.Enabled = true;
                        cardControl2.ISPass = true;
                        SetCarControlISNUll("", strcardControl2, strcardControl3);
                        mf.ShowToolTip(ToolTipIcon.Warning, "确认修改", "当前卡号与其它卡号相同，请重新刷卡", cardControl2, this);
                        return;
                    }

                    if (!mf.VerificationCard(cardControl2, this, out userId2))
                    {
                        btnOFFQcRecord.Enabled = true;
                        SetCarControlISNUll(strcardControl1, "", strcardControl3);
                        return;
                    }
                    else if (cardControl2.LblCardNO == cardControl1.LblCardNO || cardControl2.LblCardNO == cardControl3.LblCardNO)
                    {
                        btnOFFQcRecord.Enabled = true;
                        cardControl2.ISPass = true;
                        SetCarControlISNUll(strcardControl1, "", strcardControl3);
                        mf.ShowToolTip(ToolTipIcon.Warning, "确认修改", "当前卡号与其它卡号相同，请重新刷卡", cardControl2, this);

                        return;
                    }





                    ///“注销”状态编号
                    int iDictionaryID = DictionaryDAL.GetDictionaryID("注销");
                    ///“已审批”状态编号
                    int isp = DictionaryDAL.GetDictionaryID("已审批");
                    bool rbool = false;
                    int iFinish = 0;
                    foreach (int iqcrecordID in listrecordID)
                    {
                        QCRecord record1 = QCRecordDAL.GetQCInfo(iqcrecordID);
                        QCRecord record = record1;
                        record.QCRecord_UserId_UpdateID = EMEWE.Common.Converter.ToInt(Common.USERID);
                        DateTime dt = LinQBaseDao.getDate();
                        record.QCRecord_UPDATE_TIME = dt;
                        record.QCRecord_UPDATE_REASON = txtQCRecord_UPDATE_REASON.Text.Trim();
                        record.QCRecord_Dictionary_ID = iDictionaryID;//注销
                        record.QCRecord_UpdateCardUserName = cardControl1.TXTCARD + "," + cardControl2.TXTCARD;
                        rbool = QCRecordDAL.UpdateOneQcRecord(record);

                        if (record.QCRecord_TestItems_ID == TestItemsDAL.GetTestItemId("杂纸"))
                        {
                            LinQBaseDao.Query("update QCInfo set QCInfo_PAPER_WEIGHT = null,QCInfo_PAPER_SCALE=null where QCInfo_ID=" + record.QCRecord_QCInfo_ID);
                        }

                        if (record.QCRecord_TestItems_ID == TestItemsDAL.GetTestItemId("杂质"))
                        {
                            LinQBaseDao.Query("update QCInfo set QCInfo_MATERIAL_WEIGHT = null,QCInfo_MATERIAL_SCALE=null where QCInfo_ID=" + record.QCRecord_QCInfo_ID);
                        }

                        if (record.QCRecord_TestItems_ID == TestItemsDAL.GetTestItemId("废纸包重"))
                        {
                            LinQBaseDao.Query("update QCInfo set QCInfo_BAGWeight = null,QCInfo_MATERIAL_WEIGHT = null,QCInfo_MATERIAL_SCALE=null,QCInfo_PAPER_WEIGHT = null,QCInfo_PAPER_SCALE=null  where QCInfo_ID=" + record.QCRecord_QCInfo_ID);
                        }

                        if (rbool)
                        {
                            OFFQCRecord offqc = new OFFQCRecord();
                            offqc.OFFQCRecord_QCRecord_ID = iqcrecordID;
                            offqc.OFFQCRecord_Dictionary_ID = isp;//审批
                            offqc.OFFQCRecord_Time = dt;
                            offqc.OFFQCRecord_UserId = userId1;//卡号对应的用户编号
                            int ioff = 0;
                            rbool = OFFQCRecordDAL.InsertOFFQCRecord(offqc, out ioff);
                            if (rbool)
                            {
                                OFFQCRecord offqc2 = new OFFQCRecord();
                                offqc2.OFFQCRecord_QCRecord_ID = iqcrecordID;
                                offqc2.OFFQCRecord_Dictionary_ID = isp;//审批
                                offqc2.OFFQCRecord_Time = dt;
                                offqc2.OFFQCRecord_UserId = userId2;//卡号对应的用户编号
                                rbool = OFFQCRecordDAL.InsertOFFQCRecord(offqc2, out ioff);

                            }
                            else
                            {
                                QCRecordDAL.UpdateOneQcRecord(record1);
                            }
                        }
                        if (rbool)
                        {
                            string strContent = "质检编号为：" + iQcInfoID.ToString() + "，注销重量检测记录编号为" + iqcrecordID + ",刷卡授权人：" + cardControl1.TXTCARD + "、" + cardControl2.TXTCARD + "、" + cardControl3.TXTCARD; ;
                            Common.WriteLogData("注销", strContent, "");
                            iFinish++;
                        }
                    }
                    /// 清除当前数据和重新加载数据

                    txtQCRecord_UPDATE_REASON.Text = "";
                    ids.Clear();
                    listrecordID.Clear();
                    btnOFFQcRecord.Enabled = false;
                    cardControl1.TXTCARD = "";
                    cardControl2.TXTCARD = "";
                    cardControl3.TXTCARD = "";
                    cardControl1.ISPass = true;
                    cardControl2.ISPass = true;
                    cardControl3.ISPass = true;


                    MessageBox.Show(" 共修改" + icountList + "条，成功" + iFinish + "条,", "修改重量信息", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Information);
                    tabControlQCOne.SelectedIndex = 1;
                    BindData();
                    shuXinLBL();
                }
                catch (Exception ex)
                {
                    Common.WriteTextLog("确认修改FormWate.btnOFFQcRecord_Click异常：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 四舍五入算法
        /// </summary>
        /// <param name="shu">小数</param>
        /// <returns></returns>
        private string floor(string shu)
        {
            try
            {
                int xiaoshu = int.Parse(shu.Substring(shu.IndexOf('.') + 1, shu.Length - (shu.IndexOf('.') + 1)));
                if (xiaoshu > 0)
                {
                    shu = (int.Parse(shu.Substring(0, shu.IndexOf('.'))) + 1).ToString();
                }
            }
            catch
            {
            }
            return shu;
        }
        /// <summary>
        /// 计算方法
        /// 2012-08-17 周意
        /// </summary>
        private void CalculateAfterAVG()
        {
            if (iQcInfoID != 0)
            {
                StringBuilder sb = new StringBuilder();
                List<QCRecordAVGEnitiy> list = QCRecordDAL.GetQCRecord_RESULT_SUM(iQcInfoID, iQCRetest_ID, "重量检测");
                foreach (QCRecordAVGEnitiy qc in list)
                {
                    string stravg = GetData.GetStringMath((qc.Avg).ToString(), 2);
                    switch (qc.TestItemName)
                    {
                        /// 重量平均数
                        case "废纸包重"://抽样总重量是废纸包总数等于计算结果中总数量
                            txtQCInfo_BAGWeight.Text = floor(stravg);

                            break;
                        case "杂质":
                            txtQCInfo_MATERIAL_WEIGHT.Text = stravg;
                            break;
                        case "杂纸":
                            txtQCInfo_PAPER_WEIGHT.Text = stravg;
                            break;
                        case "退货还包"://抽样总重量是废纸包总数等于计算结果中总数量
                            txtQCInfo_RETURNBAG_WEIGH.Text = stravg;
                            txtQCInfo_RETURNBAG_COUNT.Text = qc.Count.ToString();
                            iReturnsBag = qc.Count;
                            dReturnsWeight = qc.Avg;
                            lblTotalWeight.Text = stravg;
                            break;

                    }
                }


                if (string.IsNullOrEmpty(lblQCInfo_MATERIALWeightWay.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text.Trim()))
                    {
                        lblQCInfo_MATERIALWeightWay.Text = "过磅";
                    }
                }

                if (!string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text))
                {
                    /// 2013-06-31  杂质也按照杂质可手输
                    //杂质比例 如果杂质重量和废纸包总重量不为空时计算
                    if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text) && lblQCInfo_MATERIALWeightWay.Text == "过磅")
                    {

                        txtQCInfo_MATERIAL_SCALE.Text = GetData.GetStringMathAndPercentage((Converter.ToDouble(txtQCInfo_MATERIAL_WEIGHT.Text) / Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                        lblQCInfo_MATERIALWeightWay.Text = "过磅";
                    }


                    /// 2012-10-29 杂纸比例和重量计算的时候有杂纸过磅,就通过计算,没有的话直接存储,输入的杂纸比例,如果即有输入比例,又有过磅,那么按照过磅来计算.一般不会出现这样的问题


                    if (string.IsNullOrEmpty(lblQCInfo_PAPERWeightWay.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text.Trim()))
                        {
                            lblQCInfo_PAPERWeightWay.Text = "过磅";
                        }
                    }
                    //杂纸比例 如果杂纸重量和废纸包总重量不为空时计算
                    if (!string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text) && lblQCInfo_PAPERWeightWay.Text == "过磅")
                    {
                        txtQCInfo_PAPER_SCALE.Text = GetData.GetStringMathAndPercentage((Converter.ToDouble(txtQCInfo_PAPER_WEIGHT.Text) / Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                        lblQCInfo_PAPERWeightWay.Text = "过磅";
                    }
                    else
                    {
                        //杂纸重量 如果杂纸比例和废纸包总重量不为空时就算杂纸重量 目测
                        if (!string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text))
                        {
                            double result = 0;
                            if (Double.TryParse(txtQCInfo_PAPER_SCALE.Text, out result))
                            {
                                txtQCInfo_PAPER_WEIGHT.Text = GetData.GetStringMath((Convert.ToDouble(txtQCInfo_PAPER_SCALE.Text) / 100 * Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                                lblQCInfo_PAPERWeightWay.Text = "目测";
                            }
                            else
                            {
                                MessageBox.Show("杂纸比例格式错误！");
                                return;
                            }

                        }

                    }

                }


                if (string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text) && lblQCInfo_MATERIALWeightWay.Text == "目测")
                {
                    if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_SCALE.Text))//杂质比例不为空，手输 计算出杂质重量
                    {
                        double result = 0;
                        if (Double.TryParse(txtQCInfo_MATERIAL_SCALE.Text, out result))
                        {
                            // txtQCInfo_MATERIAL_WEIGHT.Text = GetData.GetStringMath((Convert.ToDouble(txtQCInfo_MATERIAL_SCALE.Text) / 100 * Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                            // txtQCInfo_MATERIAL_WEIGHT.Text = GetData.GetStringMath((Convert.ToDouble(txtQCInfo_MATERIAL_SCALE.Text) / 100).ToString(), 2);
                            txtQCInfo_MATERIAL_WEIGHT.Text = "0";
                            lblQCInfo_MATERIALWeightWay.Text = "目测";
                        }
                        else
                        {
                            MessageBox.Show("杂质比例格式错误！");
                            return;
                        }
                    }
                }

                if (string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text) && lblQCInfo_PAPERWeightWay.Text == "目测")
                {

                    //杂纸重量 如果杂纸比例和废纸包总重量不为空时就算杂纸重量 目测
                    if (!string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text))
                    {
                        double result = 0;
                        if (Double.TryParse(txtQCInfo_PAPER_SCALE.Text, out result))
                        {
                            // txtQCInfo_PAPER_WEIGHT.Text = GetData.GetStringMath((Convert.ToDouble(txtQCInfo_PAPER_SCALE.Text) / 100 * Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                            // txtQCInfo_PAPER_WEIGHT.Text = GetData.GetStringMath((Convert.ToDouble(txtQCInfo_MATERIAL_WEIGHT.Text) / 100).ToString(), 2);
                            txtQCInfo_PAPER_WEIGHT.Text = "0";
                            lblQCInfo_PAPERWeightWay.Text = "目测";
                        }
                        else
                        {
                            MessageBox.Show("杂纸比例格式错误！");
                            return;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    MessageBox.Show(sb.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// 保存质检计算结果
        /// 先计算在保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeightSave_Click(object sender, EventArgs e)
        {
            if (txtQCInfo_PAPER_SCALE.Enabled)
            {
                MessageBox.Show("目测杂纸数据未完成！");
                return;
            }
            if (txtQCInfo_MATERIAL_SCALE.Enabled)
            {

                MessageBox.Show("目测杂质数据未完成！");
                return;
            }

            if (btnWeightSave.Enabled)
            {
                btnWeightSave.Enabled = false;
                if (iQcInfoID != 0)
                {

                    if (txtQCInfo_MATERIAL_SCALE.Enabled == true && txtQCInfo_MATERIAL_SCALE.Text.Trim() == "")
                    {
                        MessageBox.Show("目测杂质比例不能为空！");
                        return;
                    }
                    if (txtQCInfo_PAPER_SCALE.Enabled == true && txtQCInfo_PAPER_SCALE.Text.Trim() == "")
                    {
                        MessageBox.Show("目测杂纸比例不能为空！");
                        return;
                    }

                    CalculateAfterAVG();

                    QCInfo qc = QCInfoDAL.GetQCInfo(iQcInfoID);



                    if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text))
                    {
                        qc.QCInfo_MATERIAL_WEIGHT = Converter.ToDecimal(txtQCInfo_MATERIAL_WEIGHT.Text);
                        qc.QCInfo_MATERIAL_EXAMINER = Common.NAME;

                    }
                    if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_SCALE.Text))
                    {
                        qc.QCInfo_MATERIAL_SCALE = Converter.ToDecimal(txtQCInfo_MATERIAL_SCALE.Text);
                        //杂质比例是否异常
                        bool b = ADDUnusual(iQcInfoID, "杂质", Convert.ToDouble(txtQCInfo_MATERIAL_SCALE.Text.Trim()), "Unusualstandard_DEGRADE_MATERIAL_PERCT");
                        if (b == true)
                        {
                            qc.QCInfo_Dictionary_ID = DictionaryDAL.GetDictionaryID("暂停质检");
                            MessageBox.Show("杂质平均值异常！");
                        }

                    }
                    if (!string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text))
                    {
                        qc.QCInfo_PAPER_WEIGHT = Converter.ToDecimal(txtQCInfo_PAPER_WEIGHT.Text);
                        qc.QCInfo_PAPER_EXAMINER = Common.NAME;
                    }
                    if (!string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text))
                    {
                        qc.QCInfo_PAPER_SCALE = Converter.ToDecimal(txtQCInfo_PAPER_SCALE.Text);
                        //杂纸比例是否异常
                        bool b = ADDUnusual(iQcInfoID, "杂纸", Convert.ToDouble(txtQCInfo_PAPER_SCALE.Text.Trim()), "DEGRADE_OUTTHROWS_PERCT");
                        if (b == true)
                        {
                            qc.QCInfo_Dictionary_ID = DictionaryDAL.GetDictionaryID("暂停质检");
                            MessageBox.Show("杂纸平均值异常！");
                        }
                    }
                    if (!string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text))
                    {
                        qc.QCInfo_BAGWeight = Converter.ToDecimal(txtQCInfo_BAGWeight.Text);
                    }
                    /// 临时只检测水分  2011-12-26 周意
                    bool zjwc = false;//质检是否完成
                    if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text) && !string.IsNullOrEmpty(txtQCInfo_MATERIAL_SCALE.Text) && !string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text) && !string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text) && !string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text))
                    {

                        zjwc = true;
                    }


                    /// 判断是否有检测退货环保  没有就清空退货还包相关属性
                    bool isReturnBag = false;
                    for (int i = 0; i < dgvCurentWeight.RowCount; i++)
                    {
                        if (dgvCurentWeight.Rows[i].Cells["TestItems_NAME"].Value.ToString() == "退货还包")
                        {
                            isReturnBag = true;
                        }
                    }

                    if (!isReturnBag)
                    {
                        qc.QCInfo_RETURNBAG_COUNT = null;
                        qc.QCInfo_RETURNBAG_EXAMINER = null;
                        qc.QCInfo_RETURNBAG_TIME = null;
                        qc.QCInfo_RETURNBAG_WEIGH = null;
                    }




                    qc.QCInfo_MATERIALWeightWay = lblQCInfo_MATERIALWeightWay.Text;
                    qc.QCInfo_PAPERWeightWay = lblQCInfo_PAPERWeightWay.Text;
                    bool rbool = QCInfoDAL.UpdateInfo(qc);
                    if (rbool)
                    {
                        if (zjwc)
                        {

                            QCRecordDAL.UpdateQcRecordQCInfoID(iQcInfoID, iWeighItemId);

                        }
                        string strContent = "质检编号为：" + iQcInfoID.ToString() + "，修改重量检测汇总结果";
                        Common.WriteLogData("修改", strContent, "");


                        ISClose();
                        txtQCInfo_PAPER_SCALE.Enabled = false;
                        txtQCInfo_MATERIAL_SCALE.Enabled = false;
                        MessageBox.Show("保存质检信息重量结果", "保存成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string sql = "select	U9Bool from dbo.U9Start where U9Name='重量检测'";
                        bool isbool = bool.Parse(LinQBaseDao.Query(sql).Tables[0].Rows[0]["U9Bool"].ToString());
                        if (isbool)
                        {
                            int DRAW_EXAM_INTERFACE_ID = (int)qc.QCInfo_DRAW_EXAM_INTERFACE_ID;
                            string strs = U9Class.updataIsU9(DRAW_EXAM_INTERFACE_ID, iQcInfoID.ToString());
                            if (strs == "失败")
                            {
                                MessageBox.Show("发送数据到U9失败！");
                                return;
                            }
                        }
                        printdemoformweiht(iQcInfoID);
                    }
                    else
                    {
                        btnWeightSave.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("保存质检信息重量结果", "请在检测信息中选择开始检测状态的信息继续质检！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnWeightSave.Enabled = true;
                    return;
                }
                Common.WriteLogData("新增", "保存质检计算结果", Common.USERNAME);//操作日志
                btnWeightSave.Enabled = true;
            }
            BindDgvWaitQC("");
        }



        /// <summary>
        /// 添加检测异常信息
        /// </summary>
        /// <param name="QcInfoID">质检编号</param>
        /// <param name="testItemid">测试项目编号</param>
        /// <param name="avg">平均值</param>
        /// <param name="standardName">标准字段</param>
        /// <returns>是否异常</returns>
        private bool ADDUnusual(int QcInfoID, string testItemName, double avg, string standardName)
        {
            double standard = 0;//异常标准


            //超标取系统设置的值
            DataTable standard_DT = LinQBaseDao.Query("select " + standardName + " from Unusualstandard where Unusualstandard_PROD =(select PROD_ID from DRAW_EXAM_INTERFACE where DRAW_EXAM_INTERFACE_ID = (select QCInfo_DRAW_EXAM_INTERFACE_ID from QCInfo where QCInfo_ID=" + QcInfoID + "))").Tables[0];
            if (standard_DT.Rows.Count > 0)
            {
                standard = Convert.ToDouble(standard_DT.Rows[0][0]);
            }
            else
            {
                //没有异常标准返回false
                return false;
            }
            if (avg > standard)
            {

                DataTable TestItems_NAME_DT = LinQBaseDao.Query("select TestItems_ID from TestItems where TestItems_Name='" + testItemName + "'").Tables[0];
                if (TestItems_NAME_DT.Rows.Count > 0)
                {

                    lblCurrentQcState.Text = "暂停质检";
                    Unusual u = new Unusual();
                    u.Unusual_State = "未处理";
                    u.Unusual_TestItems_ID = Convert.ToInt32(TestItems_NAME_DT.Rows[0][0]);
                    u.Unusual_UnusualType_Id = Convert.ToInt32(TestItems_NAME_DT.Rows[0][0]);
                    u.Unusual_QCInfo_ID = QcInfoID;
                    u.Unusual_content = testItemName + "异常：" + avg + "%";
                    u.Unusual_handle_UserId = 0;
                    u.Unusual_ISSMSSend = false;
                    u.Unusual_time = LinQBaseDao.getDate();
                    int u_id = 0;
                    UnusualDAL.InsertOneQCRecord(u, out u_id);
                    //短信

                    int TestItems_ID = TestItemsDAL.GetTestItemId(testItemName);
                    //发送内容及发送人
                    DataSet SMSDs = LinQBaseDao.Query("select top(1)* from SMSConfigure where SMSConfigure_TestItems_ID=" + TestItems_ID);//只会存在一条

                    /// 取内容
                    //string[] SMSContent = SMSDs.Tables[0].Rows[0]["SMSConfigure_SendContent"].ToString().Split(',');
                    //string[] SMSContentTxt = SMSDs.Tables[0].Rows[0]["SMSConfigure_SendContentText"].ToString().Split(',');
                    //string Contents = SMSSendContent(SMSContent, u.Unusual_Id, SMSContentTxt, u.Unusual_content);//存储短信内容


                    /// 取号码、姓名,并对其号码发送短信（号码和姓名同位置是一组数据）
                    //string[] SMSPhon = SMSDs.Tables[0].Rows[0]["SMSConfigure_ReceivePhone"].ToString().Split(';');//号码
                    //string[] SMSName = SMSDs.Tables[0].Rows[0]["SMSConfigure_Receive"].ToString().Split(';');//姓名

                    //for (int y = 0; y < SMSPhon.Count(); y++)//循环要发送的人数的电话号码
                    //{
                    //    if (SMSPhon[y] != "")
                    //    {
                    //        SmsSend ss = new SmsSend();
                    //        ss.SmsSend_Phone = SMSPhon[y];
                    //        ss.SmsSend_Text = Contents;
                    //        ss.SmsSend_userName = SMSName[y];
                    //        ss.SmsSend_IsSend = "0";
                    //        ss.SmsSend_Unusunal_ID = u.Unusual_Id;
                    //        bool b = SmsSendDAL.Insert(ss);

                    //    }

                    //}

                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 取短信发送内容
        /// </summary>
        /// <param name="SMSContent">字段组合</param>
        /// <param name="UnusualType_Id">异常ID</param>
        /// <returns></returns>
        private string SMSSendContent(string[] SMSContent, int Unusual_Id, string[] SMSContentTxt, string sendText)
        {

            try
            {

                string NumContent = sendText + "  ";

                for (int i = 0; i < SMSContent.Count(); i++)
                {

                    DataSet ds = LinQBaseDao.Query("select top(1)" + SMSContent[i] + " from View_Unusual_SMS where Unusual_Id=" + Unusual_Id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        NumContent += SMSContentTxt[i] + "：" + ds.Tables[0].Rows[0][0].ToString() + "  ";
                    }

                }
                return NumContent;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            return "";

        }

        /// 退货还包
        /// <summary>
        /// 设置退货的选项
        /// </summary>
        private void SetReturns()
        {
            lblTotalWeight.Visible = true;
            lblTotalName.Visible = true;
            btnReturnBag.Visible = true;
            // cbxBags.Enabled = false;
            btnGetWeight.Enabled = true;
            btnSave.Enabled = true;
            BindcbxBags(0);
            GetReturnsWeight();

        }
        /// <summary>
        /// 关闭退货还包选项
        /// </summary>
        private void CloseReturns()
        {
            lblTotalWeight.Visible = false;
            lblTotalName.Visible = false;
            btnReturnBag.Visible = false;
            //cbxBags.Enabled = true;
            btnGetWeight.Enabled = false;
            btnSave.Enabled = false;
            iReturnsBag = 0;
            dReturnsWeight = 0;
            lblTotalWeight.Text = "";
        }


        /// <summary>
        /// 退货还包保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnBag_Click(object sender, EventArgs e)
        {
            if (btnReturnBag.Enabled)
            {
                btnReturnBag.Enabled = false;
                if (iQcInfoID > 0)
                {
                    if (string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text) || txtQCInfo_BAGWeight.Text == "0")
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "系统提示", "当前质检信息不能进行退后还包，没有抽检包总重量！", txtQCInfo_BAGWeight, this);
                        btnReturnBag.Enabled = true;
                        return;
                    }
                    else
                    {
                        decimal decweight = EMEWE.Common.Converter.ToDecimal(dReturnsWeight.ToString(), 0);
                        decimal decbagweigth = EMEWE.Common.Converter.ToDecimal(txtQCInfo_BAGWeight.Text, 0);
                        decimal decResult = decweight - decbagweigth;
                        if (decResult < 0)// 判断当前退后总重量是否大于抽检总重量
                        {
                            if (MessageBox.Show("请继续退货还包称重，当前总重量小于抽检重量" + decResult, "退货还包确认！", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                UpdateQCInfo(decweight);
                                btnReturnBag.Enabled = true;
                            }
                            else
                            {
                                btnReturnBag.Enabled = true;
                                return;
                            }
                        }
                        else
                        {
                            UpdateQCInfo(decweight);
                            btnReturnBag.Enabled = true;
                        }
                    }

                }
                else
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "系统提示", "请在检测信息中选择开始检测状态的信息继续质检！", dgvWaitQC, this);
                    btnReturnBag.Enabled = true;
                    return;
                }

            }
        }
        private void GetReturnsWeight()
        {
            List<QCRecordAVGEnitiy> list = QCRecordDAL.GetQCRecord_RESULT_SUM(iQcInfoID, iQCRetest_ID, "重量检测");
            foreach (QCRecordAVGEnitiy qc in list)
            {

                switch (qc.TestItemName)
                {

                    /// 重量平均数
                    case "退货还包"://抽样总重量是废纸包总数等于计算结果中总数量
                        string stravg = GetData.GetStringMath(qc.Avg.ToString(), 2);
                        iReturnsBag = qc.Count;
                        dReturnsWeight = qc.Avg;
                        lblTotalWeight.Text = stravg;
                        break;


                }
            }
        }
        /// <summary>
        /// 更新QCInfo退货还包数据及打印
        /// </summary>
        /// <param name="decweight"></param>
        private void UpdateQCInfo(decimal decweight)
        {
            QCInfo qc = QCInfoDAL.GetQCInfo(iQcInfoID);
            qc.QCInfo_RETURNBAG_COUNT = iReturnsBag;
            qc.QCInfo_RETURNBAG_EXAMINER = Common.NAME;
            qc.QCInfo_RETURNBAG_TIME = DateTime.Now;
            qc.QCInfo_RETURNBAG_WEIGH = decweight;
            bool rbool = QCInfoDAL.UpdateInfo(qc);
            if (rbool)
            {
                string strContent = "质检编号为：" + iQcInfoID.ToString();
                if (!string.IsNullOrEmpty(txtQCInfo_RETURNBAG_WEIGH.Text) || txtQCInfo_RETURNBAG_WEIGH.Text != "0")
                {
                    strContent = "修改";
                }
                else
                {
                    strContent = "添加";
                }
                strContent += "退货还包件数" + iReturnsBag.ToString() + "总重量" + decweight;
                Common.WriteLogData("修改", strContent, "");

                txtQCInfo_RETURNBAG_COUNT.Text = qc.QCInfo_RETURNBAG_COUNT.ToString();
                txtQCInfo_RETURNBAG_WEIGH.Text = qc.QCInfo_RETURNBAG_WEIGH.ToString();
                //将单条记录“退货还包”数据“暂停”设置为“启动”
                CloseReturns();
                //调用打印当前信息
                btnReturnBag.Visible = false;
                printdemoformweiht(iQcInfoID);
            }
        }



        private void dgvWaitQC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                //(string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text) || string.IsNullOrEmpty(txtQCInfo_MATERIAL_SCALE.Text) || string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text) || string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text) || string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text)) && lblCurrentQcState.Text == "质检中")
                shuXinLBL();

            }
        }


        /// <summary>
        /// 刷新lable
        /// </summary>
        private void shuXinLBL()
        {
            try
            {
                ISClose();
                ids.Clear();
                listrecordID.Clear();
                Common.SetSelectionBackColor(dgvWaitQC);
                int a = dgvWaitQC.SelectedRows[0].Index;

                if (dgvWaitQC.Rows[a].Cells["QCInfo_MATERIALWeightWay"].Value != null)
                {
                    lblQCInfo_MATERIALWeightWay.Text = dgvWaitQC.Rows[a].Cells["QCInfo_MATERIALWeightWay"].Value.ToString();

                }
                else
                {
                    lblQCInfo_MATERIALWeightWay.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["QCInfo_PAPERWeightWay"].Value != null)
                {
                    lblQCInfo_PAPERWeightWay.Text = dgvWaitQC.Rows[a].Cells["QCInfo_PAPERWeightWay"].Value.ToString();

                }
                else
                {
                    lblQCInfo_PAPERWeightWay.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["QCInfo_ID"].Value != null)
                {
                    iQcInfoID = EMEWE.Common.Converter.ToInt(dgvWaitQC.Rows[a].Cells["QCInfo_ID"].Value.ToString());
                    SDKCommon.CEQCINFO_ID = iQcInfoID;
                    BindcbxBags(iQcInfoID);
                }
                if (dgvWaitQC.Rows[a].Cells["CNTR_NO"].Value != null)
                {
                    lblCNTR_NO.Text = dgvWaitQC.Rows[a].Cells["CNTR_NO"].Value.ToString();
                    //txtCarNO.Text = dgvWaitQC.Rows[a].Cells["CNTR_NO"].Value.ToString();
                }
                else
                {
                    lblCNTR_NO.Text = "";
                    //txtCarNO.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["SHIPMENT_NO"].Value != null)
                {
                    lblSHIPMENT_NO.Text = dgvWaitQC.Rows[a].Cells["SHIPMENT_NO"].Value.ToString();
                    //txtSHIPMENT_NO.Text = dgvWaitQC.Rows[a].Cells["SHIPMENT_NO"].Value.ToString();
                }
                else
                {
                    lblSHIPMENT_NO.Text = "";
                    //txtSHIPMENT_NO.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["PO_NO"].Value != null)
                {

                    lblPO_NO.Text = dgvWaitQC.Rows[a].Cells["PO_NO"].Value.ToString();
                    // txtPO_NO.Text = dgvWaitQC.Rows[a].Cells["PO_NO"].Value.ToString();
                }
                else
                {
                    lblPO_NO.Text = "";
                    // txtPO_NO.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["PROD_ID"].Value != null)
                {
                    lblPROD_ID.Text = dgvWaitQC.Rows[a].Cells["PROD_ID"].Value.ToString();
                }
                else
                {
                    lblPROD_ID.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["DEGRADE_MOISTURE_PERCT"].Value != null)
                    lblDEGRADE_MOISTURE_PERCT.Text = dgvWaitQC.Rows[a].Cells["DEGRADE_MOISTURE_PERCT"].Value.ToString();
                else
                    lblDEGRADE_MOISTURE_PERCT.Text = "";
                if (dgvWaitQC.Rows[a].Cells["NO_OF_BALES"].Value != null)
                    lblNO_OF_BALES.Text = dgvWaitQC.Rows[a].Cells["NO_OF_BALES"].Value.ToString();
                else lblNO_OF_BALES.Text = "";
                if (dgvWaitQC.Rows[a].Cells["WEIGHT_TICKET_NO"].Value != null)
                {
                    lblWEIGHT_TICKET_NO.Text = dgvWaitQC.Rows[a].Cells["WEIGHT_TICKET_NO"].Value.ToString();
                    // txtWEIGHT_TICKET_NO.Text = dgvWaitQC.Rows[a].Cells["WEIGHT_TICKET_NO"].Value.ToString();
                }
                else
                {
                    lblWEIGHT_TICKET_NO.Text = "";
                    //txtWEIGHT_TICKET_NO.Text = "";
                }

                if (dgvWaitQC.Rows[a].Cells["QCInfo_PumpingPackets"].Value != null)
                    lblQCInfo_PumpingPackets.Text = dgvWaitQC.Rows[a].Cells["QCInfo_PumpingPackets"].Value.ToString();
                else lblQCInfo_PumpingPackets.Text = "";

                string strState = "";
                if (dgvWaitQC.Rows[a].Cells["QCInfo_Dictionary_ID"].Value != null)
                {
                    if (cbxQCInfoState.DataSource != null)
                    {
                        cbxQCInfoState.SelectedValue = EMEWE.Common.Converter.ToInt(dgvWaitQC.Rows[a].Cells["QCInfo_Dictionary_ID"].Value.ToString());
                        Dictionary dic = cbxQCInfoState.SelectedItem as Dictionary;

                        strState = dic.Dictionary_Name;
                        lblCurrentQcState.Text = dic.Dictionary_Name;
                    }
                }


                if (dgvWaitQC.Rows[a].Cells["QCInfo_MATERIAL_WEIGHT"].Value != null)
                {
                    txtQCInfo_MATERIAL_WEIGHT.Text = dgvWaitQC.Rows[a].Cells["QCInfo_MATERIAL_WEIGHT"].Value.ToString();

                }
                else
                {
                    txtQCInfo_MATERIAL_WEIGHT.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["QCInfo_MATERIAL_SCALE"].Value != null)
                {

                    txtQCInfo_MATERIAL_SCALE.Text = dgvWaitQC.Rows[a].Cells["QCInfo_MATERIAL_SCALE"].Value.ToString();
                }
                else
                {
                    txtQCInfo_MATERIAL_SCALE.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["QCInfo_PAPER_WEIGHT"].Value != null)
                {
                    txtQCInfo_PAPER_WEIGHT.Text = dgvWaitQC.Rows[a].Cells["QCInfo_PAPER_WEIGHT"].Value.ToString();
                }
                else
                {
                    txtQCInfo_PAPER_WEIGHT.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["QCInfo_PAPER_SCALE"].Value != null)
                {
                    txtQCInfo_PAPER_SCALE.Text = dgvWaitQC.Rows[a].Cells["QCInfo_PAPER_SCALE"].Value.ToString();
                }
                else
                {
                    txtQCInfo_PAPER_SCALE.Text = "";
                }

                if (dgvWaitQC.Rows[a].Cells["QCInfo_BAGWeight"].Value != null)
                {
                    txtQCInfo_BAGWeight.Text = floor(dgvWaitQC.Rows[a].Cells["QCInfo_BAGWeight"].Value.ToString());
                }
                else
                {
                    txtQCInfo_BAGWeight.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["QCInfo_REMARK"].Value != null)
                {
                    txtQCInfo_REMARK.Text = dgvWaitQC.Rows[a].Cells["QCInfo_REMARK"].Value.ToString();
                }
                else
                {
                    txtQCInfo_REMARK.Text = "";
                }

                if (dgvWaitQC.Rows[a].Cells["QCInfo_RETURNBAG_WEIGH"].Value != null)
                {
                    txtQCInfo_RETURNBAG_WEIGH.Text = dgvWaitQC.Rows[a].Cells["QCInfo_RETURNBAG_WEIGH"].Value.ToString();
                }
                else
                {
                    txtQCInfo_RETURNBAG_WEIGH.Text = "";
                }
                if (dgvWaitQC.Rows[a].Cells["QCInfo_RETURNBAG_COUNT"].Value != null)
                {
                    txtQCInfo_RETURNBAG_COUNT.Text = dgvWaitQC.Rows[a].Cells["QCInfo_RETURNBAG_COUNT"].Value.ToString();
                }
                else
                {
                    txtQCInfo_RETURNBAG_COUNT.Text = "";
                }
                CloseReturns(); /// 清空退后还包数据或按钮关闭
                ISOpenSave();
                txtQCInfo_MATERIAL_SCALE.Enabled = false;
                txtQCInfo_PAPER_SCALE.Enabled = false;
                //if (cboxTestItems.DataSource != null)
                //{
                //    cboxTestItems.SelectedIndex = 0;
                //}
                //tabControlQCOne.SelectedIndex = 1;//加载质检单条信息
                CalculateAfterAVG();
            }
            catch
            {

            }
        }

        private void BtnNoCard_Click(object sender, EventArgs e)
        {


            if (txtQCRecord_UPDATE_REASON.Text.Trim() == "")
            {
                mf.ShowToolTip(ToolTipIcon.Warning, "确认修改", "修改原因不能为空", txtQCRecord_UPDATE_REASON, this);
                return;
            }
            ///“注销”状态编号
            int iDictionaryID = DictionaryDAL.GetDictionaryID("注销");
            ///“已审批”状态编号
            int isp = DictionaryDAL.GetDictionaryID("已审批");
            bool rbool = false;
            int iFinish = 0;
            foreach (int iqcrecordID in listrecordID)
            {
                QCRecord record1 = QCRecordDAL.GetQCInfo(iqcrecordID);
                QCRecord record = record1;
                record.QCRecord_UserId_UpdateID = EMEWE.Common.Converter.ToInt(Common.USERID);
                DateTime dt = LinQBaseDao.getDate();
                record.QCRecord_UPDATE_TIME = dt;
                record.QCRecord_UPDATE_REASON = txtQCRecord_UPDATE_REASON.Text.Trim();
                record.QCRecord_Dictionary_ID = iDictionaryID;//注销
                if (record.QCRecord_TestItems_ID == TestItemsDAL.GetTestItemId("杂纸"))
                {
                    LinQBaseDao.Query("update QCInfo set QCInfo_PAPER_WEIGHT = null,QCInfo_PAPER_SCALE=null where QCInfo_ID=" + record.QCRecord_QCInfo_ID);
                }
                if (record.QCRecord_TestItems_ID == TestItemsDAL.GetTestItemId("杂质"))
                {
                    LinQBaseDao.Query("update QCInfo set QCInfo_MATERIAL_WEIGHT = null,QCInfo_MATERIAL_SCALE=null where QCInfo_ID=" + record.QCRecord_QCInfo_ID);
                }
                if (record.QCRecord_TestItems_ID == TestItemsDAL.GetTestItemId("废纸包重"))
                {
                    LinQBaseDao.Query("update QCInfo set QCInfo_BAGWeight = null,QCInfo_MATERIAL_WEIGHT = null,QCInfo_MATERIAL_SCALE=null,QCInfo_PAPER_WEIGHT = null,QCInfo_PAPER_SCALE=null  where QCInfo_ID=" + record.QCRecord_QCInfo_ID);
                }
                rbool = QCRecordDAL.UpdateOneQcRecord(record);





                if (rbool)
                {
                    string strContent = "质检编号为：" + iQcInfoID.ToString() + "，注销重量检测记录编号为" + iqcrecordID + ",刷卡授权人：" + cardControl1.TXTCARD + "、" + cardControl2.TXTCARD + "、" + cardControl3.TXTCARD; ;
                    Common.WriteLogData("注销", strContent, "");
                    iFinish++;
                }
            }
            /// 清除当前数据和重新加载数据




            MessageBox.Show(" 共修改" + listrecordID.Count + "条，成功" + iFinish + "条,", "修改重量信息", MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);
            tabControlQCOne.SelectedIndex = 1;



            txtQCRecord_UPDATE_REASON.Text = "";
            ids.Clear();
            listrecordID.Clear();
            btnOFFQcRecord.Enabled = false;
            cardControl1.TXTCARD = "";
            cardControl2.TXTCARD = "";
            cardControl3.TXTCARD = "";
            cardControl1.ISPass = true;
            cardControl2.ISPass = true;
            cardControl3.ISPass = true;
            BindData();
            shuXinLBL();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtQCInfo_MATERIAL_SCALE.Enabled == false)
            {

                if (txtQCInfo_MATERIAL_WEIGHT.Text == "" || txtQCInfo_MATERIAL_WEIGHT.Text == "0")
                {

                    DataTable dt = LinQBaseDao.Query("select QCRecord_ID from QCRecord where QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='杂质')  and QCRecord_Dictionary_ID=" + DictionaryDAL.GetDictionaryID("启动") + " and QCRecord_QCInfo_ID=" + iQcInfoID).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        lblQCInfo_MATERIALWeightWay.Text = "目测";
                        txtQCInfo_MATERIAL_SCALE.Enabled = true;
                        txtQCInfo_MATERIAL_SCALE.Focus();
                    }
                    else
                    {
                        MessageBox.Show("已有杂质重量，不能目测！");
                    }

                }
                else
                {
                    MessageBox.Show("已有杂质重量，不能目测！");
                }
            }
            else
            {
                txtQCInfo_MATERIAL_SCALE.Enabled = false;
                mucezazhizidongjisuans();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txtQCInfo_PAPER_SCALE.Enabled == false)
            {
                if (txtQCInfo_PAPER_WEIGHT.Text == "" || txtQCInfo_PAPER_WEIGHT.Text == "0")
                {
                    DataTable dt = LinQBaseDao.Query("select QCRecord_ID from QCRecord where QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='杂纸')  and QCRecord_Dictionary_ID=" + DictionaryDAL.GetDictionaryID("启动") + " and QCRecord_QCInfo_ID=" + iQcInfoID).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        lblQCInfo_PAPERWeightWay.Text = "目测";
                        txtQCInfo_PAPER_SCALE.Enabled = true;
                        txtQCInfo_PAPER_SCALE.Focus();
                    }
                    else
                    {
                        MessageBox.Show("已有杂纸重量，不能目测！");
                    }
                }
                else
                {
                    MessageBox.Show("已有杂纸重量，不能目测！");
                }
            }
            else
            {
                txtQCInfo_PAPER_SCALE.Enabled = false;
                mucezazhis();
            }
        }


        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="channel"></param>
        public void CapturePic(int id, string str)
        {
            if (SDKCommon.m_lUserID < 0)
            {
                return;
            }
            if (Common.Channel == 0)
            {
                return;
            }
            try
            {
                string strDirectoryName = Common.BaseFile;
                if (!Directory.Exists(strDirectoryName))
                {
                    Directory.CreateDirectory(strDirectoryName);
                }
                string strfileImage = "";
                if (str == "废纸包重")
                {
                    strfileImage = SDKCommon.CaptureJPEGPicture(strDirectoryName, id, Common.Channel);
                }
                else if (str == "杂纸")
                {
                    strfileImage = SDKCommon.CaptureJPEGPicture(strDirectoryName, id, Common.Channel2);
                }
                else if (str == "杂质")
                {
                    strfileImage = SDKCommon.CaptureJPEGPicture(strDirectoryName, id, Common.Channel3);
                }

                //上传图片
                List<string> list = new List<string>();

                //得到图片的路径
                string path = strfileImage;
                string stryear = Common.GetServersTime().Year + "\\" + Common.GetServersTime().Month + "\\" + Common.GetServersTime().ToString("yyyyMMdd") + "\\";
                string filename = path.Substring(path.LastIndexOf('\\') + 1);
                ImageFile.UpLoadFile(path, Common.SaveFiel + stryear);//上传图片到指定路径

                ImageFile.Delete(path);//上传完成后，删除图片
                //保存图片信息
                LinQBaseDao.Query("insert ImageRecord values(" + id + ",null,'" + stryear + filename + "',getdate(),null)");


            }
            catch (Exception ex)
            {

            }
        }

        private void dgv_SFJC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PicForm pf = new PicForm(Convert.ToInt32(dgv_SFJC.SelectedRows[0].Cells["QCRecord_ID2"].Value));
            pf.Show();
        }

        private void FormWeight_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormWeight.fw = null;
        }

        private void FormWeight_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormWeight.fw = null;
        }

        private void dgvWaitQC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            shuXinLBL();
        }

        private void txtWaitWEIGHTTICKETNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                if (btnWaitSeacher.Enabled)
                {
                    btnWaitSeacher.Enabled = false;
                    waitQcP = null;
                    page = new PageControl();
                    page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
                    BindDgvWaitQC("");
                    btnWaitSeacher.Enabled = true;
                }
            }
        }

        private void lblTotalName_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalWeight_Click(object sender, EventArgs e)
        {

        }

        private void lblWeightName_Click(object sender, EventArgs e)
        {

        }

        #region yk_加入包号自由选择后，需要使用
        private List<string> _bagIdList = new List<string>();
        private void cbxBags_TextChanged(object sender, EventArgs e)
        {
            //ComboBox cmb = sender as ComboBox;
            //_bagIdList.Clear();
            //GetSelectedBags(textBoxBagsID.Text, ref _bagIdList);
            //if (cmb.Text == "全部")
            //    _bagIdList.Clear();
            //else
            //{
            //    if (_bagIdList.Contains("全部"))
            //        _bagIdList.Remove("全部");
            //}
            //if (!_bagIdList.Contains(cmb.Text))
            //    _bagIdList.Add(cmb.Text);
            //for (int bagIndex = 0; bagIndex < _bagIdList.Count; bagIndex++)
            //{
            //    string bagID = _bagIdList[bagIndex];
            //    if (!cmb.Items.Contains(bagID))
            //        _bagIdList.Remove(bagID);
            //}

            //textBoxBagsID.Text = "";
            //foreach (string bag in _bagIdList)
            //    textBoxBagsID.Text += (bag + ",");
            //textBoxBagsID.Text = textBoxBagsID.Text.TrimEnd(',');
        }

        private void textBoxBagsID_Leave(object sender, EventArgs e)
        {
            //TextBox textBoxBagsID = sender as TextBox;
            //CheckSelectedBagsID();
        }

        //private bool CheckSelectedBagsID()
        //{
        //    _bagIdList.Clear();
        //    GetSelectedBags(textBoxBagsID.Text, ref _bagIdList);
        //    for (int bagIndex = 0; bagIndex < _bagIdList.Count; bagIndex++)
        //    {
        //        try
        //        {
        //            string bagID = _bagIdList[bagIndex];
        //            if (!cbxBags.Items.Contains(bagID))
        //                throw new Exception(bagID);
        //        }
        //        catch (Exception ex)
        //        {                   
        //            MessageBox.Show("抽包号" + ex.Message + "不存在，请重新选择。");
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //public void GetSelectedBags(string bags, ref List<string> bagList)
        //{
        //    if (!string.IsNullOrEmpty(bags))
        //    {
        //        string tBags = bags;
        //        int pos = tBags.IndexOf(",");
        //        if (pos > 0)
        //        {
        //            string curBag = tBags.Substring(0, pos);
        //            bagList.Add(curBag);

        //            tBags = bags.Substring(pos + 1);
        //            GetSelectedBags(tBags, ref bagList);
        //        }
        //        else if (tBags.Length > 0)
        //        {
        //            bagList.Add(tBags);
        //        }
        //    }
        //}
        #endregion


    }
}
