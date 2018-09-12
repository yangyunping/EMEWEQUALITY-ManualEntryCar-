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
using EMEWE.Common;
using System.Linq.Expressions;
using EMEWEQUALITY.HelpClass;
using EMEWEQUALITY.MyControl;
using System.Data.Linq.SqlClient;
using System.Xml;
using MoistureDetectionRules;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EMEWEQUALITY.SystemAdmin;
using System.Threading;
using System.Timers;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class FormWate : Form
    {
        public FormWate()
        {
            InitializeComponent();
           
        }

        #region 页面参数
        // FenYe fy = new FenYe();
        PageControl page = new PageControl();
        public MainFrom mf;
        public static FormWate fwt = null;
        /// <summary>
        /// 一检拆包前点数
        /// </summary>
        int OneCQCount = 0;
        /// <summary>
        /// 一检拆包后点数
        /// </summary>
        int OneCHCount = 0;
        /// <summary>
        /// 二检拆包前点数
        /// </summary>
        int TwoCQCount = 0;
        /// <summary>
        /// 二检拆包后点数
        /// </summary>
        int TwoCHCount = 0;
        /// <summary>
        ///Dgv是否为绑定状态
        /// </summary>
        bool boolBindDGV = true;
        /// <summary>
        /// 记录水分测试总行数
        /// 说明：每次加载dgvWateOne时获取行数
        /// </summary>
        public int iWateRowCount = 0;
        /// <summary>
        /// 当前检测的行数
        ///说明： 第一次检测时默认为0，修改或者中途断开检测时自动查找当前该获取数据下标
        /// </summary>
        int _iCurrentRowCount = 0;
        /// <summary>
        /// 水分检测项目编号
        /// 说明：由于使用频繁，所有在加载页面时获取编号
        /// </summary>
        static int iWateItemId = 1;
        /// <summary>
        /// 拆包前水分编号
        /// 说明：由于使用频繁，所有在加载页面时获取编号
        /// </summary>
        int iUnpackBeforeMOISTItems = 0;
        /// <summary>
        /// 拆包后水分编号
        /// 说明：由于使用频繁，所有在加载页面时获取编号
        /// </summary>
        int iUnpackBackMOIST = 0;
        /// <summary>
        /// 当前质检编号
        /// 说明：tabPageWaitQC选择单条信息是赋值，在保存后清空
        /// </summary>
        int iQcInfoID = 0;
        /// <summary>
        /// 复测编号
        /// 说明：tabPageWaitQC选择单条信息是赋值，在保存后清空，
        /// 如果是复测，则需要复测和质检编号两个条件才能进行操作
        /// </summary>
        int iQCRetest_ID = 0;
        /// <summary>
        /// 是否有修改数据dgvWateOne
        ///说明： 修改数据打卡确认后ISUpdatedvgOne设置为“True”，第一条修改数据可以不验证水分仪序号，第一条修改数据获取仪表信息后将值付为"false"
        /// </summary>
        bool ISUpdatedvgOne = false;
        /// <summary>
        /// 获取水分仪定时器timerData是否为忙碌状态
        /// </summary>
        bool ISTimesBusy = true;
        ///// <summary>
        ///// 是否锁定选项卡选择
        ///// false 不锁定 ，true锁定
        ///// 如果水分未检测完成锁定界面，如果暂停检测离开界面必须填写原因，再次进入检测时需要打卡请求进入检测
        ///// </summary>
        //bool ISOpenTabControlQCOne = false;
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
        /// 开始测水时间
        /// </summary>
        DateTime dtime;

        /// <summary>
        /// 生明无返回值的委托
        /// </summary>
        private delegate void AsynUpdateUI();

        /// <summary>
        /// 引用正在加载
        /// </summary>
        public Form_Loading form_loading = null;

        /// <summary>
        /// 定时器执行开始时间
        /// </summary>
        private DateTime Stime;

        /// <summary>
        /// 执行时间差
        /// </summary>
        private int ts;

        /// <summary>
        /// 定时器执行结束时间
        /// </summary>
        private DateTime Etime;

        /// <summary>
        /// 设置线程毫秒差
        /// </summary>
        private int Mts;

        private System.Timers.Timer timerData = new System.Timers.Timer();

        //private System.Threading.Timer timerData;
        //private delegate void SetWater();


        #endregion
        private void FormWate_Load(object sender, EventArgs e)
        {

            //获取当前线程的唯一标识符
            int id = Thread.CurrentThread.ManagedThreadId;

            Common.WriteTextLog("UI线程标识符：" + id.ToString(), "线程");

            Common.GetCollection();
            TimeEndIsNull();
            BindData();

            timerData.Start();
            timerData.Interval = 1000;
            timerData.Elapsed += new System.Timers.ElapsedEventHandler(timerData_Tick);
            //CheckForIllegalCrossThreadCalls = false;
            //timerData = new System.Threading.Timer(new System.Threading.TimerCallback(timerData_Tick), null, 0, 100);
        }
        /// <summary>
        /// 初始化时间
        /// </summary>
        private void TimeEndIsNull()
        {
            DateTime Time = DateTime.Now.AddDays(0);
            txtbeginTime.Value = Time;
            txtendTime.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));
            DateTime Time1 = DateTime.Now.AddDays(-1);
            txtbeginTime1.Value = Time;
            txtendTime1.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 绑定界面数据
        /// </summary>
        private void BindData()
        {
            iWateItemId = TestItemsDAL.GetTestItemId("水分检测");
            iUnpackBeforeMOISTItems = TestItemsDAL.GetTestItemId("拆包前水分检测");
            iUnpackBackMOIST = TestItemsDAL.GetTestItemId("拆包后水分检测");
            BindCboxTestItems();
            BindCboxState();
            BindcbxQCInfoState();//绑定检测模块
            tscbxPageSize2.SelectedItem = "10";

            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            BindDgvWaitQCOne();
            tscbxPageSize1.SelectedItem = "10";
            BindcbxUserName();
        }

        private void BindcbxUserName()
        {
            cbxUserName.DataSource = UserInfo2DAL.GetListWhereUserInfoName("水分检测员", "启动");
            this.cbxUserName.DisplayMember = "UserName";
            cbxUserName.ValueMember = "UserId";
            if (cbxUserName.Items.Count > 0)
            {
                cbxUserName.SelectedIndex = Common.waterManSelect;
            }
            else
            {
                cbxUserName.Text = "";
            }

            cbxUserNameTwo.DataSource = UserInfo2DAL.GetListWhereUserInfoName("水分检测员", "启动");
            this.cbxUserNameTwo.DisplayMember = "UserName";
            cbxUserNameTwo.ValueMember = "UserId";
            if (cbxUserNameTwo.Items.Count > 1)
            {
                cbxUserNameTwo.SelectedIndex = Common.waterManSelectTwo;
            }
            else
            {
                cbxUserNameTwo.Text = "";
            }
        }

        private void ISOpenTabControl()
        {
            string strQCState = lblCurrentQcState.Text.Trim();

            if (strQCState == "质检中" && _iCurrentRowCount + 1 < iWateRowCount)
            {
                TabControl1.Enabled = false;
            }
            else if (strQCState == "暂停质检")
            {
                TabControl1.Enabled = false;
            }
            else if (strQCState == "质检中" && _iCurrentRowCount + 1 == iWateRowCount)
            {
                TabControl1.Enabled = true;
            }
            if (listrecordID.Count > 0 && iQcInfoID != 0 && strQCState == "质检中")
            {
                btnOFFQcRecord.Enabled = true;
            }
            else
            {
                btnOFFQcRecord.Enabled = false;
            }
        }

        /// <summary>
        /// 改变选项卡读取值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlQCOne_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (TabControl1.SelectedTab.Name)
            {
                case "tabPageWater1"://水分检测

                    if (lblCurrentQcState.Text == "质检中")
                    {
                        ISTimesBusy = false;
                        dtime = Common.GetServersTime();
                        Common.GetWaterSet(lblPROD_ID.Text.Trim());
                        listrecordID.Clear();
                        BindDgvWateOne();
                        SelectedDgvWateOne();

                    }
                    else if (lblCNTR_NO.Text.Trim() != "" && lblCurrentQcState.Text != "质检中")
                    {
                        MessageBox.Show("质检状态不为质检中，不能进行质检！");
                        TabControl1.SelectedIndex = 0;
                    }

                    break;
                case "tabPageQCOneInfo"://质检记录数据
                    ISTimesBusy = true;
                    page = new PageControl();
                    page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
                    LoadData();
                    break;
                case "tabPageUpdateWate"://修改检测数据 条件：修改检测数据必须选择修改行数和质检编号不能为空，质检状态未开始状态才能打开“确认”修改按钮 
                    ClearOFFQCRecord();//先清理之前的残留数据在进行判断是否打开按钮
                    if (listrecordID.Count > 0 && iQcInfoID > 0 && lblCurrentQcState.Text.Trim() == "质检中")
                    {
                        btnOFFQcRecord.Enabled = true;
                    }
                    else
                    {
                        btnOFFQcRecord.Enabled = false;
                    }
                    break;
                case "tabPageWaitQC"://获取待测数据
                default:
                    ISTimesBusy = true;
                    page = new PageControl();
                    page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                    BindDgvWaitQC();

                    break;
            }
        }
        #region 待检测模块

        private void BindcbxQCInfoState()
        {
            List<Dictionary> listDic = DictionaryDAL.GetValueStateDictionary("质检状态");
            cbxWaitQCInfoState.DataSource = listDic;
            this.cbxWaitQCInfoState.DisplayMember = "Dictionary_Name";
            cbxWaitQCInfoState.ValueMember = "Dictionary_ID";
            if (cbxWaitQCInfoState.DataSource != null)
            {

                cbxWaitQCInfoState.SelectedIndex = 1;
            }

            //cbxQCInfoState.DataSource = listDic;
            //cbxQCInfoState.DisplayMember = "Dictionary_Name";
            //cbxQCInfoState.ValueMember = "Dictionary_ID";
            //if (cbxQCInfoState.DataSource != null)
            //{

            //    cbxQCInfoState.SelectedIndex = 1;
            //}

        }
        /// <summary>
        /// 待检测数据列表条件
        /// </summary>
        Expression<Func<View_QCInfo, bool>> waitexprp = null;
        //  private Expression<Func<View_QCInfo, bool>> waitQcP = null;
        //计算和保存按钮打开 2012-10-29 不做验证
        private void ISOpenSave()
        {
            ClearOFFQCRecord();
            if ((string.IsNullOrEmpty(txtQCInfo_MOIST_PER_SAMPLE.Text) || txtQCInfo_MOIST_PER_SAMPLE.Text == "0.00") && iQcInfoID > 0 && lblCurrentQcState.Text.Trim() == "质检中")
            {

                HelpClass.GetData.IOldSNO = 0;
            }
            btnSave.Enabled = true;

        }
        /// <summary>
        /// 绑定等待质检数据
        /// </summary>
        private async void BindDgvWaitQC()
        {
            form_loading = new Form_Loading();
            form_loading.ShowLoading(this);
            form_loading.Text = "水分数据加载中...";
            await GetWeightData();
            form_loading.CloseLoading(this);
        }
        private async void BindDgvWaitQCOne()
        {

            await GetWeightData();

        }

        private async Task GetWeightData()
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
                            dgvWaitQC.DataSource = page.BindBoundControl<View_QCInfo>("", txtCurrentPage2, lblPageCount2, waitexprp, "QCInfo_ID desc");
                        }
                        catch (Exception ex)
                        {
                            form_loading.CloseLoading(this);
                            Common.WriteTextLog("FormWate.BindDgvWaitQC异常:" + ex.Message.ToString());
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
                if (waitexprp == null)
                {
                    waitexprp = (Expression<Func<View_QCInfo, bool>>)PredicateExtensionses.True<View_QCInfo>();
                }

                if (cbxWaitQCInfoState.SelectedValue != null)
                {
                    int stateID = Converter.ToInt(cbxWaitQCInfoState.SelectedValue.ToString());
                    if (stateID > 0)
                    {
                        waitexprp = waitexprp.And(n => n.Dictionary_ID == stateID);
                        i++;

                    }
                }
                else
                {
                    waitexprp = waitexprp.And(c => c.Dictionary_Name == "质检中");
                    i++;

                }

                if (!string.IsNullOrEmpty(cbxWeighState.Text))
                {
                    string strcbxWaitQCInfoState = cbxWeighState.Text;


                    if (strcbxWaitQCInfoState == "已完成")
                    {

                        waitexprp = waitexprp.And(c => c.QCInfo_MOIST_PER_SAMPLE > 0 && c.QCInfo_UnpackBack_MOIST_PER_SAMPLE > 0 && c.QCInfo_UnpackBefore_MOIST_PER_SAMPLE > 0);
                        i++;
                    }
                    else if (strcbxWaitQCInfoState == "未完成")
                    {
                        waitexprp = waitexprp.And(c => c.QCInfo_MOIST_PER_SAMPLE == null || c.QCInfo_MOIST_PER_SAMPLE <= 0 || c.QCInfo_UnpackBack_MOIST_PER_SAMPLE == null || c.QCInfo_UnpackBack_MOIST_PER_SAMPLE <= 0 || c.QCInfo_UnpackBefore_MOIST_PER_SAMPLE == null || c.QCInfo_UnpackBefore_MOIST_PER_SAMPLE <= 0);
                        i++;
                    }
                }
                #region 注释
                //if (cbxWaitQCInfoState.SelectedValue != null)
                //{


                //    int stateID = Converter.ToInt(cbxWaitQCInfoState.SelectedValue.ToString());

                //    if (stateID > 0)
                //    {
                //        Dictionary dicEntity = cbxWaitQCInfoState.SelectedItem as Dictionary;
                //        if (dicEntity.Dictionary_Value == "质检中")//质检中
                //        {
                //            waitexprp = waitexprp.And(c => c.Dictionary_ID == stateID && (c.QCInfo_MOIST_PER_SAMPLE == null || c.QCInfo_UnpackBack_MOIST_PER_SAMPLE == null || c.QCInfo_UnpackBefore_MOIST_PER_SAMPLE == null));
                //            i++;
                //        }
                //        else if (dicEntity.Dictionary_Value == "完成质检")//完成质检
                //        {
                //            waitexprp = waitexprp.And(c => c.Dictionary_ID == stateID || (c.Dictionary_ID == DictionaryDAL.GetDictionaryID("质检中") && (c.QCInfo_MOIST_PER_SAMPLE != null || c.QCInfo_UnpackBack_MOIST_PER_SAMPLE != null || c.QCInfo_UnpackBefore_MOIST_PER_SAMPLE != null)));
                //            i++;
                //        }
                //        else if (dicEntity.Dictionary_Value == "全部")//状态为”全部“不加任何条件
                //        {
                //        }
                //        else//其它质检状态
                //        {
                //            waitexprp = waitexprp.And(n => n.Dictionary_ID == stateID);
                //            i++;
                //        }
                //    }

                //}
                //else//默认为”质检中“状态
                //{
                //    waitexprp = waitexprp.And(c => c.Dictionary_Name == "质检中" && (c.QCInfo_MOIST_PER_SAMPLE == null || c.QCInfo_UnpackBack_MOIST_PER_SAMPLE == null || c.QCInfo_UnpackBefore_MOIST_PER_SAMPLE == null));
                //    i++;

                //}
                #endregion

                if (!string.IsNullOrEmpty(txtWaitWEIGHTTICKETNO.Text))
                {
                    waitexprp = waitexprp.And(n => SqlMethods.Like(n.WEIGHT_TICKET_NO, "%" + txtWaitWEIGHTTICKETNO.Text.Trim() + "%"));

                    i++;
                }
                if (!string.IsNullOrEmpty(txtWaitCarNo.Text.Trim()))
                {
                    waitexprp = waitexprp.And(n => SqlMethods.Like(n.CNTR_NO, "%" + txtWaitCarNo.Text.Trim() + "%"));
                    i++;
                }
                if (!string.IsNullOrEmpty(txtWaitPO_NO.Text))
                {
                    waitexprp = waitexprp.And(n => SqlMethods.Like(n.PO_NO, "%" + txtWaitPO_NO.Text.Trim() + "%"));
                    i++;
                }
                if (!string.IsNullOrEmpty(txtWaitSHIPMENT_NO.Text))
                {
                    waitexprp = waitexprp.And(n => SqlMethods.Like(n.SHIPMENT_NO, "%" + txtWaitSHIPMENT_NO.Text.Trim() + "%"));
                    i++;
                }
                string beginTime = txtbeginTime.Value.ToString("yyyy-MM -dd ");
                string endTime = txtendTime.Value.ToString("yyyy-MM -dd ");
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "
                if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                {
                    waitexprp = waitexprp.And(c => (c.QCInfo_TIME > DateTime.Parse(begin) && c.QCInfo_TIME < DateTime.Parse(end)));
                    i++;
                }
                if (i == 0)
                {
                    waitexprp = null;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWate.GetDgvWaitQCSeacher异常：" + ex.Message.ToString());
            }
        }



        /// <summary>
        /// 搜索待水分检测项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaitQCSeacher_Click(object sender, EventArgs e)
        {
            waitexprp = null;
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();

            BindDgvWaitQC();
        }
        #region 待水分检测项分页事件
        /// <summary>
        ///  待水分检测项上页下页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatordgvWaitQC_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            dgvWaitQC.DataSource = page.BindBoundControl<View_QCInfo>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, waitexprp, "QCInfo_ID desc");
        }
        /// <summary>
        ///  待水分检测项改变每页条数事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            BindDgvWaitQC();
        }

        #endregion
        /// <summary>
        /// 获取当前待检信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvWaitQC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    ids.Clear();
                    listrecordID.Clear();


                    //显示抽包详情
                    int a = e.RowIndex;

                    DataTable dt = LinQBaseDao.Query("select Packets_DTS,Packets_this,Packets_this_Update from Packets where Packets_QCInfo_DRAW_EXAM_INTERFACE_ID=" + dgvWaitQC.Rows[a].Cells["QCInfo_DRAW_EXAM_INTERFACE_ID"].Value.ToString()).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        lblPackets_DTS.Text = dt.Rows[0][0].ToString();
                        if (dt.Rows[0]["Packets_this_Update"].ToString() != "")
                        {
                            lblPackets_QCInfo_DRAW_EXAM_INTERFACE_ID.Text = dt.Rows[0]["Packets_this_Update"].ToString();

                        }
                        else
                        {
                            lblPackets_QCInfo_DRAW_EXAM_INTERFACE_ID.Text = "未抽包";
                        }
                    }



                    Common.SetSelectionBackColor(dgvWaitQC);
                    if (dgvWaitQC.Rows[a].Cells["QCInfo_ID"].Value != null)
                        iQcInfoID = EMEWE.Common.Converter.ToInt(dgvWaitQC.Rows[a].Cells["QCInfo_ID"].Value.ToString());

                    #region 2012-11-15 修改不是当前客户端不能进行检测
                    //if (!mf.ISCurrentClientAndUpdateClient(iQcInfoID))
                    //{
                    //    return;
                    //}

                    #endregion

                    if (dgvWaitQC.Rows[a].Cells["CNTR_NO"].Value != null)
                        lblCNTR_NO.Text = dgvWaitQC.Rows[a].Cells["CNTR_NO"].Value.ToString();
                    else lblCNTR_NO.Text = "";
                    if (dgvWaitQC.Rows[a].Cells["SHIPMENT_NO"].Value != null)
                        lblSHIPMENT_NO.Text = dgvWaitQC.Rows[a].Cells["SHIPMENT_NO"].Value.ToString();
                    else lblSHIPMENT_NO.Text = "";
                    if (dgvWaitQC.Rows[a].Cells["PO_NO"].Value != null)
                    {
                        lblPO_NO.Text = dgvWaitQC.Rows[a].Cells["PO_NO"].Value.ToString();
                    }
                    else
                    {
                        lblPO_NO.Text = "";
                    }
                    if (dgvWaitQC.Rows[a].Cells["PROD_ID"].Value != null)
                        lblPROD_ID.Text = dgvWaitQC.Rows[a].Cells["PROD_ID"].Value.ToString();
                    else lblPROD_ID.Text = "";
                    if (dgvWaitQC.Rows[a].Cells["DEGRADE_MOISTURE_PERCT"].Value != null)
                        lblDEGRADE_MOISTURE_PERCT.Text = dgvWaitQC.Rows[a].Cells["DEGRADE_MOISTURE_PERCT"].Value.ToString();
                    else
                    {
                        lblDEGRADE_MOISTURE_PERCT.Text = "";

                    }

                    if (dgvWaitQC.Rows[a].Cells["NO_OF_BALES"].Value != null)
                        lblNO_OF_BALES.Text = dgvWaitQC.Rows[a].Cells["NO_OF_BALES"].Value.ToString();
                    else lblNO_OF_BALES.Text = "";
                    if (dgvWaitQC.Rows[a].Cells["WEIGHT_TICKET_NO"].Value != null)
                        lblWEIGHT_TICKET_NO.Text = dgvWaitQC.Rows[a].Cells["WEIGHT_TICKET_NO"].Value.ToString();
                    else lblWEIGHT_TICKET_NO.Text = "";

                    if (dgvWaitQC.Rows[a].Cells["QCInfo_MOIST_PER_SAMPLE"].Value != null)
                        txtQCInfo_MOIST_PER_SAMPLE.Text = dgvWaitQC.Rows[a].Cells["QCInfo_MOIST_PER_SAMPLE"].Value.ToString();
                    else
                    {
                        txtQCInfo_MOIST_PER_SAMPLE.Text = "";
                    }
                    if (dgvWaitQC.Rows[a].Cells["QCInfo_MOIST_Count"].Value != null)
                    {
                        txtQCInfo_MOIST_Count.Text = dgvWaitQC.Rows[a].Cells["QCInfo_MOIST_Count"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_MOIST_Count.Text = "";
                    }

                    //if(iQcInfoID!=0)
                    //      lblQCInfo_PumpingPackets.Text = QCInfoDAL.GetIntDRAWCount(iQcInfoID).ToString();
                    // lblQcUserName
                    if (dgvWaitQC.Rows[a].Cells["QCInfo_PumpingPackets"].Value != null)
                        lblQCInfo_PumpingPackets.Text = dgvWaitQC.Rows[a].Cells["QCInfo_PumpingPackets"].Value.ToString();
                    else lblQCInfo_PumpingPackets.Text = "";

                    //if (dgvWaitQC.Rows[a].Cells["QCInfo_Dictionary_ID"].Value != null)
                    //{
                    //    if (cbxQCInfoState.DataSource != null)
                    //        cbxQCInfoState.SelectedIndex = EMEWE.Common.Converter.ToInt(dgvWaitQC.Rows[a].Cells["QCInfo_Dictionary_ID"].Value.ToString());
                    //}
                    if (dgvWaitQC.Rows[a].Cells["Dictionary_Name"].Value != null)
                    {
                        lblCurrentQcState.Text = dgvWaitQC.Rows[a].Cells["Dictionary_Name"].Value.ToString().Trim();
                        if (lblCurrentQcState.Text.Trim() == "终止质检")
                        {
                            ckbQCState.Checked = true;
                        }
                    }
                    else lblCurrentQcState.Text = "";
                    if (dgvWaitQC.Rows[a].Cells["QCInfo_REMARK"].Value != null)
                    {
                        txtQCInfo_REMARK.Text = dgvWaitQC.Rows[a].Cells["QCInfo_REMARK"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_REMARK.Text = "";
                    }

                    #region 新增2012-11-18


                    if (dgvWaitQC.Rows[a].Cells["QCInfo_MOIST_EXAMINER"].Value != null)
                    {
                        cbxUserName.Text = dgvWaitQC.Rows[a].Cells["QCInfo_MOIST_EXAMINER"].Value.ToString();
                    }
                    #endregion
                    ISLblCanle();
                    ISOpenSave();
                }
            }
            catch
            { }
        }

        /// <summary>
        ///  根据水分检测设置计算当前拆包前和拆包后检测点数
        /// </summary>
        /// <param name="WaterTestConfigureSet_frequency">第几次检测</param>
        /// <param name="QCInfo_ID">质检编号</param>
        private void ReckonWaterTestCount(int QCInfo_ID)
        {

            IEnumerable<WaterTestConfigureSet> wsIE = WaterTestConfigureSetDAL.Query();
            int cbqTest_id = 0;//拆包前测试项目编号
            int cbhTest_id = 0;//拆包后测试项目编号
            int QCInfo_PumpingPackets = 0;//抽包数
            DataTable CBQDT = LinQBaseDao.Query("select TestItems_ID from TestItems where TestItems_NAME='拆包前水分检测'").Tables[0];
            if (CBQDT.Rows.Count > 0)
            {
                cbqTest_id = Convert.ToInt32(CBQDT.Rows[0][0]);
            }
            DataTable CBHDT = LinQBaseDao.Query("select TestItems_ID from TestItems where TestItems_NAME='拆包后水分检测'").Tables[0];
            if (CBHDT.Rows.Count > 0)
            {
                cbhTest_id = Convert.ToInt32(CBHDT.Rows[0][0]);
            }

            DataTable QCInfo_PumpingPacketsDT = LinQBaseDao.Query("SELECT QCInfo_PumpingPackets FROM QCInfo WHERE QCInfo_ID =" + QCInfo_ID).Tables[0];
            if (QCInfo_PumpingPacketsDT.Rows.Count > 0)
            {
                QCInfo_PumpingPackets = Convert.ToInt32(QCInfo_PumpingPacketsDT.Rows[0][0]);
            }
            foreach (WaterTestConfigureSet item in wsIE)
            {
                #region 一次水分检测
                if (item.WaterTestConfigureSet_frequency == 1)
                {
                    //方案一 包数*每包针数
                    if (item.WaterTestConfigureSet_Project == 1)
                    {
                        //拆包前
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbqTest_id)
                        {
                            OneCQCount = Convert.ToInt32(item.WaterTestConfigureSet_PackCount * item.WaterTestConfigureSet_NeedleCount);
                        }
                        //拆包后
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbhTest_id)
                        {
                            OneCHCount = Convert.ToInt32(item.WaterTestConfigureSet_PackCount * item.WaterTestConfigureSet_NeedleCount);
                        }
                    }

                    //方案二 （DTS包数+随机包数）* 每包针数
                    if (item.WaterTestConfigureSet_Project == 2)
                    {
                        //拆包前
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbqTest_id)
                        {
                            OneCQCount = Convert.ToInt32((QCInfo_PumpingPackets + item.WaterTestConfigureSet_PackCount) * item.WaterTestConfigureSet_NeedleCount);
                        }
                        //拆包后
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbhTest_id)
                        {
                            OneCHCount = Convert.ToInt32((QCInfo_PumpingPackets + item.WaterTestConfigureSet_PackCount) * item.WaterTestConfigureSet_NeedleCount);
                        }
                    }
                    //方案三 总针数
                    if (item.WaterTestConfigureSet_Project == 3)
                    {
                        //拆包前
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbqTest_id)
                        {
                            OneCQCount = Convert.ToInt32(item.WaterTestConfigureSet_NeedleCount);
                        }
                        //拆包后
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbhTest_id)
                        {
                            OneCHCount = Convert.ToInt32(item.WaterTestConfigureSet_NeedleCount);
                        }
                    }

                }
                #endregion

                #region 二次水分检测
                else if (item.WaterTestConfigureSet_frequency == 2)
                {
                    //方案一 包数*每包针数
                    if (item.WaterTestConfigureSet_Project == 1)
                    {
                        //拆包前
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbqTest_id)
                        {
                            TwoCQCount = Convert.ToInt32(item.WaterTestConfigureSet_PackCount * item.WaterTestConfigureSet_NeedleCount);
                        }
                        //拆包后
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbhTest_id)
                        {
                            TwoCHCount = Convert.ToInt32(item.WaterTestConfigureSet_PackCount * item.WaterTestConfigureSet_NeedleCount);
                        }
                    }

                    //方案二 （DTS包数+随机包数）* 每包针数
                    if (item.WaterTestConfigureSet_Project == 2)
                    {
                        //拆包前
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbqTest_id)
                        {
                            TwoCQCount = Convert.ToInt32((QCInfo_PumpingPackets + item.WaterTestConfigureSet_PackCount) * item.WaterTestConfigureSet_NeedleCount);
                        }
                        //拆包后
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbhTest_id)
                        {
                            TwoCHCount = Convert.ToInt32((QCInfo_PumpingPackets + item.WaterTestConfigureSet_PackCount) * item.WaterTestConfigureSet_NeedleCount);
                        }
                    }
                    //方案三 总针数
                    if (item.WaterTestConfigureSet_Project == 3)
                    {
                        //拆包前
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbqTest_id)
                        {
                            TwoCQCount = Convert.ToInt32(item.WaterTestConfigureSet_NeedleCount);
                        }
                        //拆包后
                        if (item.WaterTestConfigureSet_TestItems_TestItems_ID == cbhTest_id)
                        {
                            TwoCHCount = Convert.ToInt32(item.WaterTestConfigureSet_NeedleCount);
                        }
                    }

                }
                #endregion
            }
        }
        /// <summary>
        /// 清除计算结果lba显示
        /// </summary>
        private void ISLblCanle()
        {
            lblQCInfo_MOIST_PER_SAMPLE.Text = "";

        }

        #endregion
        #region 水分检测模块

        bool bo = true;
        /// <summary>
        /// 绑定初始化水分检测列表
        /// </summary>
        private void BindDgvWateOne()
        {
            //form_loading = new Form_Loading();
            //form_loading.ShowLoading(this);
            //form_loading.Text = "数据加载中...";
            try
            {
                bo = true;
                Stime = DateTime.Now;
                if (iQcInfoID != 0)
                {
                    Common.WriteTextLog("水分显示第八步");
                    boolBindDGV = true;
                    IAsyncResult asyncResult3 = BeginInvoke(new AsyncInvokedelegatethree(Asyncdatathree));
                    EndInvoke(asyncResult3);

                    // List<View_QCRecordInfo> list = QCRecordDAL.GetListQCRecordWate(iQcInfoID, iQCRetest_ID, "水分检测", "启动", "暂停");

                    //if (Mts != 0)
                    //{
                    //    Thread.Sleep(Mts);
                    //}

                   
                    DataSet ds = LinQBaseDao.Query("select QCRecord_ID,QCRecord_DRAW,TestItems_NAME,QCRecord_QCCOUNT,QCRecord_RESULT,QCRecord_TestUserName,QCRecord_Client_ID,QCRecord_NUMBER,QCRecord_COUNT from View_QCRecordInfo  where  QCRecord_QCInfo_ID = " + iQcInfoID + " and TestItems_NAME='水分检测' and  Dictionary_Value !='注销'  order by QCRecord_QCCOUNT ");

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        Common.WriteTextLog("水分显示第九步");
                        //_iCurrentRowCount = 0;
                        //ISUpdatedvgOne = true;
                        //iWateRowCount = list.Count;
                        //txtQCInfo_MOIST_Count.Text = iWateRowCount.ToString();
                        //dgvWateOne.DataSource = list;
                        //Common.SumWaterCount = list.Count;

                        _iCurrentRowCount = 0;
                        ISUpdatedvgOne = true;

                        iWateRowCount = ds.Tables[0].Rows.Count;

                        iWateRowCount = ds.Tables[0].Rows.Count;
                        IAsyncResult asyncResult2 = BeginInvoke(new AsyncInvokedelegatetwo(Asyncdatatwo));
                        EndInvoke(asyncResult2);

                        IAsyncResult asyncResult4 = BeginInvoke(new AsyncInvokedelegatefour(Asyncdatafour), ds);
                        EndInvoke(asyncResult4);
                       
                        Common.SumWaterCount = ds.Tables[0].Rows.Count;

                        #region 质检中获取当前下标，如果为最后一条弹出提示进行计算
                        if (strQCState == "质检中")
                        {
                            _iCurrentRowCount = GetCurrentRowCount(iQcInfoID);
                            if ((_iCurrentRowCount == iWateRowCount) && dgvWateOne.Rows[_iCurrentRowCount - 1].Cells["QCRecord_RESULT"].Value != null)
                            {
                                bo = false;
                                MessageBox.Show("水分已检测完成，请修改数据或保存！", "水分检测提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CalculateAfterAVG();
                                ISTimesBusy = true;
                            }
                        }
                        #endregion
                    }
                    //if (list.Count > 0)//已质检信息
                    //{
                    //    Common.WriteTextLog("水分显示第九步");
                    //    _iCurrentRowCount = 0;
                    //    ISUpdatedvgOne = true;
                    //    iWateRowCount = list.Count;
                    //    txtQCInfo_MOIST_Count.Text = iWateRowCount.ToString();
                    //    dgvWateOne.DataSource = list;
                    //    Common.SumWaterCount = list.Count;

                    //    #region 质检中获取当前下标，如果为最后一条弹出提示进行计算
                    //    if (strQCState == "质检中")
                    //    {
                    //        _iCurrentRowCount = GetCurrentRowCount(iQcInfoID);
                    //        if ((_iCurrentRowCount == iWateRowCount) && dgvWateOne.Rows[_iCurrentRowCount - 1].Cells["QCRecord_RESULT"].Value != null)
                    //        {
                    //            bo = false;
                    //            MessageBox.Show("水分已检测完成，请修改数据或保存！", "水分检测提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            CalculateAfterAVG();
                    //            ISTimesBusy = true;
                    //        }
                    //    }
                    //    #endregion
                    //}
                    else if (strQCState == "质检中")//如果质检状态是“质检中”，为水分质检信息生成对应拆包后水分、拆包前水分检测检测质检数据
                    {
                        Common.WriteTextLog("水分显示第十步");
                        if (Common.ISPackets == "是")
                        {
                            if (!Packets(iQcInfoID))
                            {
                                MessageBox.Show(this, "还没有抽包，请抽包后检测水分！");
                                return;
                            }
                        }

                        //总水分点
                        Common.GetSumWaterCount(iQcInfoID);//总水分点数
                        iWateRowCount = Common.SumWaterCount;
                        AddDgvWateOneAndQCRecord(Common.OrganizationID, out iWateRowCount);
                        txtQCInfo_MOIST_Count.Text = iWateRowCount.ToString();
                        _iCurrentRowCount = 0;
                        //iWateRowCount = k;
                        LinQBaseDao.Query("update QCInfo set QCInfo_PumpingPackets=" + Common.testBags.Count() + ",QCInfo_MOIST_Count=" + iWateRowCount + " where QCInfo_ID=" + iQcInfoID);


                    }
                    if (_iCurrentRowCount == 0)
                    {
                        HelpClass.GetData.IOldSNO = 0;
                    }
                    DataGridViewCurrent(dgvWateOne, _iCurrentRowCount);

                    boolBindDGV = false;
                    //ISTimesBusy = false;
                }

                Etime = DateTime.Now;
                if (Etime.Minute.Equals(Stime.Minute))
                {
                    ts = Etime.Second - Stime.Second;
                }
                else
                {
                    ts = Etime.Second + 60 - Stime.Second;
                }
                if (ts > 3)
                {
                    Application.DoEvents();
                    // ISUpdatedvgOne = false;
                    // return;
                }
                Common.WriteTextLog("执行开始时间:" + Stime + ", 执行结束时间:" + Etime + ", 时间差：" + ts, "定时器执行显示水分数据 ");

                //form_loading.CloseLoading(this);

            }
            catch (Exception err)
            {
                Common.WriteTextLog("BindDgvWateOne:" + err.Message, "错误日志 ");
            }
           
        }



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
        /// 移动光标到获取当前行
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="icrow"></param>
        private void DataGridViewCurrent(DataGridView dgv, int icrow)
        {
            try
            {
                if (dgv.RowCount > 0)
                {
                    if (icrow >= dgv.Rows.Count)
                    {
                        icrow = icrow - 1;
                    }
                    dgv.CurrentCell = dgv.Rows[icrow].Cells[0];
                    dgv.EditMode = DataGridViewEditMode.EditProgrammatically;//.EditOnKeystroke;//.EditOnEnter;
                    dgv.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLogWate("DataGridViewCurrent" + ex.Message + Convert.ToString(icrow));
            }

        }
        /// <summary>
        /// 新增dgvWateOne一行数据并新增QCRecord表一条数据
        /// </summary>
        /// <param name="k">dgvWateOne当前行</param>
        /// <param name="i">质检记录序号</param>
        /// <param name="bagCount">总包数</param>
        public void AdddgvWateOneAndQCRecord(int k, int i, string TestItems_Name, string bagNo)
        {
            int rint = 0;

            if (TestItems_Name == "拆包前水分检测")
                Addqcrecord(i, iUnpackBeforeMOISTItems, out rint, bagNo);
            else if (TestItems_Name == "拆包后水分检测")
            {
                Addqcrecord(i, iUnpackBackMOIST, out rint, bagNo);
            }
            else if (TestItems_Name == "水分检测")
            {
                if (!Addqcrecord(i, iWateItemId, out rint, bagNo))
                {
                    Addqcrecord(i, iWateItemId, out rint, bagNo);
                }
            }

            //增加一条数据后 如果现实列表里面数据条数 < 总条数 列表增加一行
            if (dgvWateOne.Rows.Count < iWateRowCount)
            {
                dgvWateOne.Rows.Add();
                dgvWateOne.Rows[k].Cells["QCRecord_QCCOUNT"].Value = i.ToString();
                dgvWateOne.Rows[k].Cells["QCRecord_TestItems_Name"].Value = TestItems_Name;
                dgvWateOne.Rows[k].Cells["QCRecord_ID1"].Value = rint;
                dgvWateOne.Rows[k].Cells["QCRecord_RESULT"].Value = "";
                dgvWateOne.Rows[k].Cells["QCRecord_NUMBER"].Value = "";
                dgvWateOne.Rows[k].Cells["QCRecord_COUNT"].Value = "";
                dgvWateOne.Rows[k].Cells["QCRecord_DRAW"].Value = bagNo;
            }
        }

        /// <summary>
        /// 根据公司水份生成规则，新增dgvWateOne所有水份数据并新增QCRecord表所有水份数据数据
        /// </summary>
        /// <param name="company">公司名称</param>
        public void AddDgvWateOneAndQCRecord(string company, out int wateRowCount)
        {
            WateAddProxy proxy = new WateAddProxy(company);
            wateRowCount = proxy.AddDgvWateOneAndQCRecord(this);

            //if (company == "江西理文")
            //{
            //    if (JiangXiMoistureDetectionRule.ItemMoist == "每包")
            //    {
            //        for (int i = 0; i < Common.testBags.Count(); i++)
            //        {
            //            for (int j = 0; j < Common.testBagWaterCount; j++)
            //            {
            //                AdddgvWateOneAndQCRecord(wateRowCount, wateRowCount + 1, "水分检测", Common.testBags[i]);
            //                wateRowCount++;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        int bct = Common.testBags.Count();
            //        if (Common.SumWaterCount >= bct && Common.SumWaterCount % bct == 0)
            //        {
            //            Common.testBagWaterCount = Common.SumWaterCount / bct;
            //            for (int i = 0; i < Common.testBags.Count(); i++)
            //            {
            //                for (int j = 0; j < Common.testBagWaterCount; j++)
            //                {
            //                    AdddgvWateOneAndQCRecord(wateRowCount, wateRowCount + 1, "水分检测", Common.testBags[i]);
            //                    wateRowCount++;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for (int j = 0; j < iWateRowCount; j++)
            //            {
            //                AdddgvWateOneAndQCRecord(wateRowCount, wateRowCount + 1, "水分检测", Common.testBags[0]);
            //                wateRowCount++;
            //            }
            //        }
            //    }
            //}
            //else if (company == "重庆理文")
            //{
            //    for (int i = 0; i < Common.testBags.Count(); i++)
            //    {
            //        for (int j = 0; j < Common.testBagWaterCount; j++)
            //        {
            //            AdddgvWateOneAndQCRecord(wateRowCount, wateRowCount + 1, "水分检测", Common.testBags[i]);
            //            wateRowCount++;
            //        }
            //    }
            //}
        }




        /// <summary>
        /// 添加质检记录信息
        /// </summary>
        /// <param name="iQCRecord_QCCOUNT">质检次序号</param>
        /// <param name="iQCRecord_TestItems_ID">质检项目</param>
        /// <param name="rint">QCRecord_ID自动增长列</param>
        /// <returns></returns>
        private bool Addqcrecord(int iQCRecord_QCCOUNT, int iQCRecord_TestItems_ID, out int rint, string bagNo)
        {
            bool rbool = true;
            rint = 0;
            try
            {
                QCRecord qcRecord = new QCRecord();
                qcRecord.QCRecord_QCInfo_ID = iQcInfoID;
                qcRecord.QCRecord_Client_ID = Common.CLIENTID;
                qcRecord.QCRecord_Dictionary_ID = DictionaryDAL.GetDictionaryID("启动");
                qcRecord.QCRecord_TestItems_ID = iQCRecord_TestItems_ID;
                qcRecord.QCRecord_QCCOUNT = iQCRecord_QCCOUNT;
                qcRecord.QCRecord_ISLEDSHOW = true;
                qcRecord.QCRecord_DRAW = Convert.ToDecimal(bagNo);
                if (iQCRetest_ID > 0)
                {
                    qcRecord.QCRecord_ISRETEST = true;
                    qcRecord.QCRecord_QCRetest_ID = iQCRetest_ID;
                }
                else
                {
                    qcRecord.QCRecord_ISRETEST = false;
                }


                rbool = QCRecordDAL.InsertOneQCRecord(qcRecord, out rint);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }


        /// <summary>
        /// 比较上一行水分编号与当前获取行的编号是否为递增如果是将当前数据附加上去，如果不是，则要求依次检测
        /// </summary>
        /// <param name="iNext">上一行水分编号</param>
        /// <param name="iCurrent">当前行水分编号</param>
        /// <returns></returns>
        private bool CompareWateID(int iOnline, int iCurrent)
        {
            if (iOnline + 1 == iCurrent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 定时检测水分仪新数据并保存到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerData_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!ISTimesBusy)
                {
                    Common.WriteTextLog("判断水分是否获取值：" + ISTimesBusy.ToString());
                    Common.WriteTextLog("判断水分获取值时间节点：" + dtime);
                    //20180109 emewe 103,加上判断，第一个执行完才可执行下一次
                    ISTimesBusy = true;
                    //Common.WriteTextLog("水分显示第二步");
                    if (lblCurrentQcState.Text.Trim() != "质检中")
                    {
                        ISTimesBusy = true;
                        return;
                    }
                    //object objcount = LinQBaseDao.GetSingle("select * from Moisture_test where Moisture_test_Collection_ID in (" + Common.StrCollectionIDS + ") and Moisture_test_status=0 and Moisture_test_Time>'" + dtime + "' ");
                    //20180109修改成只取一条数据，emewe103 
                    object objcount = LinQBaseDao.GetSingle("select top 1 * from Moisture_test where Moisture_test_Collection_ID in (" + Common.StrCollectionIDS + ") and Moisture_test_status=0 and Moisture_test_Time>'" + dtime + "' ");

                    if (Common.GetInt(objcount) <= 0)
                    {
                        ISTimesBusy = false;
                        return;
                    }
                    _iCurrentRowCount = GetCurrentRowCount(iQcInfoID);
                    if (_iCurrentRowCount > iWateRowCount)
                    {
                        ISTimesBusy = true;
                        return;
                    }
                    //Common.WriteTextLog("水分显示第三步");
                    string strsql = "select count(0) from QCRecord where QCRecord_QCInfo_ID=" + iQcInfoID + "  and QCRecord_Dictionary_ID=2 and QCRecord_QCCOUNT>0";
                    int wcount = Convert.ToInt32(LinQBaseDao.GetSingle(strsql));
                    int abscount = wcount % 2 == 1 ? 1 : 0;

                    //ykWarning：临时方案，还需优化
                    if (Common.OrganizationID == "JiangXiPaper")
                    {
                        #region 1#水分检测员
                        int startQCRecord_QCCOUNT = 1;
                        int endQCRecord_QCCOUNT = wcount / 2 + abscount;
                        UpdateFirstDetectorWate(1, endQCRecord_QCCOUNT);
                        #endregion

                        #region 2#水分检测员
                        startQCRecord_QCCOUNT = wcount / 2 + abscount + JiangXiMoistureDetectionRule.ItemMoistCount / 2;
                        endQCRecord_QCCOUNT = wcount;

                        //ykWarning:针对最后4针水分值都进不去问题，做出如下调整
                        //int updatedRowNum = UpdateSecondDetectorWate(startQCRecord_QCCOUNT, endQCRecord_QCCOUNT);
                        //_updatedSecondDetectorWateRowNum += updatedRowNum;

                        UpdateSecondDetectorWate(startQCRecord_QCCOUNT, endQCRecord_QCCOUNT);
                        int updatedSecondDetectorWateRowNum = GetSecondDetectorWateUpdatedRowNum(iQcInfoID);
                        Console.WriteLine("updatedSecondDetectorWateRowNum=" + updatedSecondDetectorWateRowNum);
                        if (updatedSecondDetectorWateRowNum >= wcount / 2 - JiangXiMoistureDetectionRule.ItemMoistCount / 2)
                        {
                            startQCRecord_QCCOUNT = wcount / 2 + abscount;
                            endQCRecord_QCCOUNT = wcount / 2 + abscount + JiangXiMoistureDetectionRule.ItemMoistCount / 2;
                            UpdateSecondDetectorWate(startQCRecord_QCCOUNT, endQCRecord_QCCOUNT);
                            //_updatedSecondDetectorWateRowNum += updatedRowNum;
                        }
                        //if (_updatedSecondDetectorWateRowNum >= wcount / 2)
                        //    _updatedSecondDetectorWateRowNum = 0;
                        #endregion
                    }
                    else if (Common.OrganizationID == "ChongQingPaper")
                    {
                        //Common.WriteTextLog("水分显示第四步");

                        //UpdateChongqingDetectorWate(1, wcount);

                        //获取当前线程的唯一标识符
                        int id = Thread.CurrentThread.ManagedThreadId;

                        Common.WriteTextLog("定时器线程标识符：" + id.ToString(), "线程");

                        Does(1, wcount);

                    }
                }
            }
            catch (Exception ex)
            {
                ISTimesBusy = false;
                //  timerData.Stop();
                Common.WriteTextLogWate("FormWate.timerData_Tick定时器获取水分数据时异常：" + ex.Message);
            }
        }

        #region 江西水份检测员
        private int UpdateFirstDetectorWate(int startQCRecord_QCCOUNT, int endQCRecord_QCCOUNT)
        {
            int updatedRowNum = 0;
            int qccount = 0;
            string strsql = "select QCRecord_ID,QCRecord_QCCOUNT from QCRecord where QCRecord_QCInfo_ID=" + iQcInfoID + " and QCRecord_RESULT is null and QCRecord_Dictionary_ID=2 and QCRecord_QCCOUNT>='" + startQCRecord_QCCOUNT + "' and QCRecord_QCCOUNT<='" + endQCRecord_QCCOUNT + "' order by QCRecord_QCCOUNT";
            DataTable dtqcr = LinQBaseDao.Query(strsql).Tables[0];
            for (int i = 0; i < dtqcr.Rows.Count; i++)
            {
                int iqcRecordID = Common.GetInt(dtqcr.Rows[i]["QCRecord_ID"].ToString());
                DataTable dt = LinQBaseDao.Query("select * from Moisture_test where Moisture_test_Collection_ID in (" + Common.StrCollectionIDS + ") and Moistrue_test_Instrument_ID in (" + Common.StrInstrumentIDS + ") and Moisture_test_status=0 and Moisture_test_Numwater=0 and Moisture_test_Time>'" + dtime + "' ").Tables[0];
                if (dt.Rows.Count <= 0)
                    break;
                qccount = Common.GetInt(dtqcr.Rows[i]["QCRecord_QCCOUNT"].ToString());
                double waterValue = Convert.ToDouble(dt.Rows[0]["Moisture_test_value"].ToString());
                if (waterValue < Common.MOISTUREMIN || waterValue > Common.MOISTUREMAX)//水分值小于设定的最小值不接受
                {
                    LinQBaseDao.Query("update Moisture_test set Moisture_test_status=1 where Moisture_test_ID=" + dt.Rows[0]["Moisture_test_ID"].ToString());
                    break;
                }
                string cname = cbxUserName.Text.ToString();

                //LinQBaseDao.ExecuteSql("update QCRecord set QCRecord_NUMBER='" + dt.Rows[0]["Moisture_test_NO"].ToString() + "',QCRecord_RESULT=" + dt.Rows[0]["Moisture_test_value"].ToString() + ",QCRecord_COUNT='" + dt.Rows[0]["Moisture_test_count"].ToString() + "',QCRecord_UserId=" + Common.GetInt(Common.USERID) + ",QCRecord_TIME=getdate(),QCRecord_TestUserName='" + cname + "',QCRecord_Dictionary_ID=" + DictionaryDAL.GetDictionaryID("启动") + ",QCRecord_ISLEDSHOW=0 where QCRecord_ID=" + iqcRecordID + " and QCRecord_RESULT is  null");
                //LinQBaseDao.Query("update Moisture_test set Moisture_test_status=1 where Moisture_test_ID=" + dt.Rows[i]["Moisture_test_ID"].ToString());
                //num++;
                //updateDGV(iQcInfoID);

                #region 事务处理：1、抓取业务数据，2、修改流水表状态为1（已抓取）。
                string QCRecord_NUMBERem = dt.Rows[0]["Moisture_test_NO"] + string.Empty;
                string QCRecord_RESULTem = dt.Rows[0]["Moisture_test_value"] + string.Empty;
                string QCRecord_COUNTem = dt.Rows[0]["Moisture_test_count"] + string.Empty;
                string QCRecord_UserIdem = Common.GetInt(Common.USERID) + string.Empty;
                string QCRecord_Dictionary_IDem = DictionaryDAL.GetDictionaryID("启动") + string.Empty;
                string Moisture_test_IDem = dt.Rows[0]["Moisture_test_ID"] + string.Empty;

                List<SqlParameter> sps = new List<SqlParameter>{
                                       new SqlParameter(){ ParameterName="@QCRecord_NUMBER", Value=QCRecord_NUMBERem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_RESULT", Value=QCRecord_RESULTem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_COUNT", Value=QCRecord_COUNTem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_UserId", Value=QCRecord_UserIdem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_TestUserName", Value=(cname+string.Empty).Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_Dictionary_ID", Value=QCRecord_Dictionary_IDem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_ID", Value=(iqcRecordID+string.Empty).Trim()},
                                       new SqlParameter(){ ParameterName="@Moisture_test_ID", Value=Moisture_test_IDem.Trim()},
                                       new SqlParameter(){ ParameterName="@result", Value=Moisture_test_IDem.Trim(), Direction= ParameterDirection.Output, Size=2000}
                                       };
                LinQBaseDao.ExecuteDataset(CommandType.StoredProcedure, "sp_QCRecord_fun", sps.ToArray());
                string result = sps[8].Value + string.Empty;
                if (result.Trim().Equals("success"))
                {
                    updatedRowNum++;
                    Common.WriteLogData("新增成功", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                }
                else
                {
                    Common.WriteLogData("新增失败", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                }
                #endregion
            }
            if (updatedRowNum > 0)
            {
                BindDgvWateOne();
                // updateDGV(iQcInfoID);
                //this.dgvWateOne.Rows[iCurrentRowCount - 1].ReadOnly = true;
                ISUpdatedvgOne = false;
                _iCurrentRowCount = GetCurrentRowCount(iQcInfoID);
                if (_iCurrentRowCount == iWateRowCount)
                    CalculateAfterAVG();
                else
                    DataGridViewCurrent(dgvWateOne, qccount);
            }
            return updatedRowNum;
        }

        private int _updatedSecondDetectorWateRowNum = 0;
        private int GetSecondDetectorWateUpdatedRowNum(int iQcInfoID)
        {
            string strsql = "select top 1 count(1) as totalCount from [dbo].[QCRecord](nolock) a where ISNULL(QCRecord_QCCOUNT,'')<>'' and ISNULL(CAST(QCRecord_RESULT AS nvarchar),'')<>'' and ISNULL(QCRecord_TestUserName,'')<>'门警轮值' and QCRecord_QCInfo_ID=" + iQcInfoID;
            DataTable dtqcr = LinQBaseDao.Query(strsql).Tables[0];
            return null != dtqcr && dtqcr.Rows.Count > 0 ? (int)dtqcr.Rows[0]["totalCount"] : -1;
        }

        private int UpdateSecondDetectorWate(int startQCRecord_QCCOUNT, int endQCRecord_QCCOUNT)
        {
            int updatedRowNum = 0;
            int qccount = 0;
            string strsql = "select QCRecord_ID,QCRecord_QCCOUNT from QCRecord where QCRecord_QCInfo_ID=" + iQcInfoID + " and QCRecord_RESULT is null and QCRecord_Dictionary_ID=2 and QCRecord_QCCOUNT>'" + startQCRecord_QCCOUNT + "' and QCRecord_QCCOUNT<='" + endQCRecord_QCCOUNT + "' order by QCRecord_QCCOUNT";
            DataTable dtqcr = LinQBaseDao.Query(strsql).Tables[0];
            for (int i = 0; i < dtqcr.Rows.Count; i++)
            {
                int iqcRecordID = Common.GetInt(dtqcr.Rows[i]["QCRecord_ID"].ToString());
                DataTable dt = LinQBaseDao.Query("select * from Moisture_test where Moisture_test_Collection_ID in (" + Common.StrCollectionIDS + ") and Moistrue_test_Instrument_ID in (" + Common.StaInstrumentIDS + ") and Moisture_test_status=0 and Moisture_test_Numwater=1 and Moisture_test_Time>'" + dtime + "' ").Tables[0];
                if (dt.Rows.Count <= 0)
                    break;
                qccount = Common.GetInt(dtqcr.Rows[i]["QCRecord_QCCOUNT"].ToString());
                double waterValue = Convert.ToDouble(dt.Rows[0]["Moisture_test_value"].ToString());
                if (waterValue < Common.MOISTUREMIN || waterValue > Common.MOISTUREMAX)//水分值小于设定的最小值不接受
                {
                    LinQBaseDao.Query("update Moisture_test set Moisture_test_status=1 where Moisture_test_ID=" + dt.Rows[0]["Moisture_test_ID"].ToString());
                    break;
                }
                string cname = cbxUserNameTwo.Text.ToString();
                #region 事务处理：1、抓取业务数据，2、修改流水表状态为1（已抓取）。
                string QCRecord_NUMBERem = dt.Rows[0]["Moisture_test_NO"] + string.Empty;
                string QCRecord_RESULTem = dt.Rows[0]["Moisture_test_value"] + string.Empty;
                string QCRecord_COUNTem = dt.Rows[0]["Moisture_test_count"] + string.Empty;
                string QCRecord_UserIdem = Common.GetInt(Common.USERID) + string.Empty;
                string QCRecord_Dictionary_IDem = DictionaryDAL.GetDictionaryID("启动") + string.Empty;
                string Moisture_test_IDem = dt.Rows[0]["Moisture_test_ID"] + string.Empty;

                List<SqlParameter> sps = new List<SqlParameter>{
                                       new SqlParameter(){ ParameterName="@QCRecord_NUMBER", Value=QCRecord_NUMBERem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_RESULT", Value=QCRecord_RESULTem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_COUNT", Value=QCRecord_COUNTem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_UserId", Value=QCRecord_UserIdem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_TestUserName", Value=(cname+string.Empty).Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_Dictionary_ID", Value=QCRecord_Dictionary_IDem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_ID", Value=(iqcRecordID+string.Empty).Trim()},
                                       new SqlParameter(){ ParameterName="@Moisture_test_ID", Value=Moisture_test_IDem.Trim()},
                                       new SqlParameter(){ ParameterName="@result", Value=Moisture_test_IDem.Trim(), Direction= ParameterDirection.Output, Size=2000}
                                       };
                LinQBaseDao.ExecuteDataset(CommandType.StoredProcedure, "sp_QCRecord_fun", sps.ToArray());
                string result = sps[8].Value + string.Empty;
                if (result.Trim().Equals("success"))
                {
                    updatedRowNum++;
                    Common.WriteLogData("新增成功", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                }
                else
                {
                    Common.WriteLogData("新增失败", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                }
                #endregion

                //LinQBaseDao.ExecuteSql("update QCRecord set QCRecord_NUMBER='" + dt.Rows[0]["Moisture_test_NO"].ToString() + "',QCRecord_RESULT=" + dt.Rows[0]["Moisture_test_value"].ToString() + ",QCRecord_COUNT='" + dt.Rows[0]["Moisture_test_count"].ToString() + "',QCRecord_UserId=" + Common.GetInt(Common.USERID) + ",QCRecord_TIME=getdate(),QCRecord_TestUserName='" + cname + "',QCRecord_Dictionary_ID=" + DictionaryDAL.GetDictionaryID("启动") + ",QCRecord_ISLEDSHOW=0 where QCRecord_ID=" + iqcRecordID + " and QCRecord_RESULT is  null");
                //LinQBaseDao.Query("update Moisture_test set Moisture_test_status=1 where Moisture_test_ID=" + dt.Rows[i]["Moisture_test_ID"].ToString());
                //Common.WriteLogData("新增", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                //num++;
                //updateDGV(iQcInfoID);
            }
            if (updatedRowNum > 0)
            {
                BindDgvWateOne();
                ISUpdatedvgOne = false;
                _iCurrentRowCount = GetCurrentRowCount(iQcInfoID);
                if (_iCurrentRowCount == iWateRowCount)
                    CalculateAfterAVG();
                else
                    DataGridViewCurrent(dgvWateOne, qccount);
            }

            return updatedRowNum;
        }
        #endregion

        #region 重庆水份检测员

        private delegate void AsyncInvokedelegate(DataTable dt,int i);

        private delegate void AsyncInvokedelegatetwo();

        private delegate void AsyncInvokedelegatethree();

        private delegate void AsyncInvokedelegatefour(DataSet ds);


        // private AsyncInvokedelegate _Asyncdelegate;

        private string str = "";

        private Task DoWork(int startQCRecord_QCCOUNT, int endQCRecord_QCCOUN)
        {
            //this.is
            return Task.Run(() =>
            {
               
                ////获取当前线程的唯一标识符
                //int id = Thread.CurrentThread.ManagedThreadId;

                //Common.WriteTextLog("线程标识符：" + id.ToString(), "线程");
                //IAsyncResult  asyncResult  =  BeginInvoke(new AsyncInvokedelegate(Asyncdata), startQCRecord_QCCOUNT, endQCRecord_QCCOUN);
                //EndInvoke(asyncResult);

                try
                {
                    int num = 0;
                    int qccount = 0;
                    Stime = DateTime.Now;
                    //20180109 .emewe 103 只取一条数据
                    // string strsql = "select QCRecord_ID,QCRecord_QCCOUNT from QCRecord where QCRecord_QCInfo_ID=" + iQcInfoID + " and QCRecord_RESULT is null and QCRecord_Dictionary_ID=2 and QCRecord_QCCOUNT>=1 and QCRecord_QCCOUNT<='" + (endQCRecord_QCCOUNT) + "' order by QCRecord_QCCOUNT";
                    string strsql = "select top 1 QCRecord_ID,QCRecord_QCCOUNT from QCRecord where QCRecord_QCInfo_ID=" + iQcInfoID + " and QCRecord_RESULT is null and QCRecord_Dictionary_ID=2 and QCRecord_QCCOUNT>=1 and QCRecord_QCCOUNT<='" + (endQCRecord_QCCOUN) + "' order by QCRecord_QCCOUNT";
                    DataTable dtqcr = LinQBaseDao.Query(strsql).Tables[0];
                    if (dtqcr.Rows.Count > 0)
                    {
                        //Common.WriteTextLog("水分显示第五步");
                        for (int i = 0; i < dtqcr.Rows.Count; i++)
                        {
                            int iqcRecordID = Common.GetInt(dtqcr.Rows[i]["QCRecord_ID"].ToString());
                            //20180109 ,emewe 103 只取一条水分数据
                            //DataTable dt = LinQBaseDao.Query("select * from Moisture_test where Moisture_test_Collection_ID in (" + Common.StrCollectionIDS + ")  and Moisture_test_status=0  and Moisture_test_Time>'" + dtime + "' ").Tables[0];
                            DataTable dt = LinQBaseDao.Query("select top 1 * from Moisture_test where Moisture_test_Collection_ID in (" + Common.StrCollectionIDS + ")  and Moisture_test_status=0  and Moisture_test_Time>'" + dtime + "' ").Tables[0];
                            //Common.WriteTextLog("水分显示第六步");
                            if (dt.Rows.Count <= 0)
                            {
                                break;
                            }
                            //Thread.Sleep(10000);
                            // Delay(10000);
                            //  Thread.Sleep(10000);

                            //if (Mts != 0)
                            //{
                            //    Thread.Sleep(Mts);
                            //}
                            //获取当前线程的唯一标识符
                            int id = Thread.CurrentThread.ManagedThreadId;

                            Common.WriteTextLog("异步线程标识符：" + id.ToString(), "线程");

                            // int num = Thread.ManagedThreadId;
                            qccount = Common.GetInt(dtqcr.Rows[i]["QCRecord_QCCOUNT"].ToString());
                            double waterValue = Convert.ToDouble(dt.Rows[0]["Moisture_test_value"].ToString());
                            if (waterValue < Common.MOISTUREMIN || waterValue > Common.MOISTUREMAX) //水分值小于设定的最小值 不接受
                            {
                                LinQBaseDao.Query("update Moisture_test set Moisture_test_status=1 where Moisture_test_ID=" + dt.Rows[0]["Moisture_test_ID"].ToString());
                                ISTimesBusy = false;
                                break;
                            }
                            IAsyncResult asyncResult = BeginInvoke(new AsyncInvokedelegate(Asyncdata),dt,i);
                            EndInvoke(asyncResult);
                            //string cname = dt.Rows[i]["Moisture_test_Numwater"].ToString() == "1" ? cbxUserName.Text : cbxUserNameTwo.Text;
                            string QCRecord_NUMBERem = dt.Rows[0]["Moisture_test_NO"] + string.Empty;
                            string QCRecord_RESULTem = dt.Rows[0]["Moisture_test_value"] + string.Empty;
                            string QCRecord_COUNTem = dt.Rows[0]["Moisture_test_count"] + string.Empty;
                            string QCRecord_UserIdem = Common.GetInt(Common.USERID) + string.Empty;
                            string QCRecord_Dictionary_IDem = DictionaryDAL.GetDictionaryID("启动") + string.Empty;
                            string Moisture_test_IDem = dt.Rows[0]["Moisture_test_ID"] + string.Empty;

                            List<SqlParameter> sps = new List<SqlParameter>{
                                       new SqlParameter(){ ParameterName="@QCRecord_NUMBER", Value=QCRecord_NUMBERem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_RESULT", Value=QCRecord_RESULTem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_COUNT", Value=QCRecord_COUNTem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_UserId", Value=QCRecord_UserIdem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_TestUserName", Value=(cname+string.Empty).Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_Dictionary_ID", Value=QCRecord_Dictionary_IDem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_ID", Value=(iqcRecordID+string.Empty).Trim()},
                                       new SqlParameter(){ ParameterName="@Moisture_test_ID", Value=Moisture_test_IDem.Trim()},
                                       new SqlParameter(){ ParameterName="@result", Value=Moisture_test_IDem.Trim(), Direction= ParameterDirection.Output, Size=2000}
                                       };
                            LinQBaseDao.ExecuteDataset(CommandType.StoredProcedure, "sp_QCRecord_fun", sps.ToArray());

                            string result = sps[8].Value + string.Empty;
                            //成功
                            if (result.Trim().Equals("success"))
                            {

                                num++;
                                Etime = DateTime.Now;
                                Common.WriteLogData("新增成功", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                            }
                            else
                            {
                                Common.WriteLogData("新增失败", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                            }


                            if (Etime.Minute.Equals(Stime.Minute))
                            {
                                ts = Etime.Second - Stime.Second;
                            }
                            else
                            {
                                ts = Etime.Second + 60 - Stime.Second;
                            }
                            Common.WriteTextLog("执行开始时间:" + Stime + ", 执行结束时间:" + Etime + ", 时间差：" + ts, "定时器执行水分数据写入到主表 ");
                            //if (ts > 3)
                            //{
                            //    //   Thread.Join();
                            //    Application.DoEvents();
                            //    // ISUpdatedvgOne = false;
                            //    // return;
                            //}

                            if (num > 0)
                            {
                                //Common.WriteTextLog("水分显示第七步");
                                // Thread.Sleep(2000);
                                //  BeginInvoke(new AsyncInvokedelegate(BindDgvWateOne);
                                // updateDGV(iQcInfoID);
                                //this.dgvWateOne.Rows[iCurrentRowCount - 1].ReadOnly = true;
                               BindDgvWateOne();
                                //IAsyncResult asyncResult2 = BeginInvoke(new AsyncInvokedelegatetwo(BindDgvWateOne));
                                //EndInvoke(asyncResult2);
                                ISUpdatedvgOne = false;
                                _iCurrentRowCount = GetCurrentRowCount(iQcInfoID);
                                if (_iCurrentRowCount == iWateRowCount)
                                {
                                    CalculateAfterAVG();
                                }
                                else
                                {
                                    DataGridViewCurrent(dgvWateOne, qccount);
                                    ISTimesBusy = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    //MessageBox.Show(err.Message);

                    Common.WriteTextLog("DoWork" + err.Message, "错误日志");
                }

            }
            );
            //new Thread(() =>
            //{
            //  //  this
            //    // string str = "123";

            //    this.BeginInvoke(new AsyncInvokedelegate(Asyncdata), startQCRecord_QCCOUNT, endQCRecord_QCCOUN);
            //}).Start();
        }

        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }

        private string cname = "";
        private void Asyncdata(DataTable dt, int i)
        {
             cname = dt.Rows[i]["Moisture_test_Numwater"].ToString() == "1" ? cbxUserName.Text : cbxUserNameTwo.Text;          
        }

        private void Asyncdatatwo()
        {
             txtQCInfo_MOIST_Count.Text = iWateRowCount.ToString();
        }
        private string strQCState = "";
        private void Asyncdatathree()
        {
            dgvWateOne.AutoGenerateColumns = false;
            dgvWateOne.DataSource = null;
            strQCState = lblCurrentQcState.Text.Trim();
        }

        private void Asyncdatafour(DataSet ds)
        {
            dgvWateOne.DataSource = ds.Tables[0];
        }
        private async void Does(int startQCRecord_QCCOUNT, int endQCRecord_QCCOUN)
        {
            await DoWork(startQCRecord_QCCOUNT, endQCRecord_QCCOUN);

        }

        private void UpdateChongqingDetectorWate(int startQCRecord_QCCOUNT, int endQCRecord_QCCOUNT)
        {

            try
            {
                int num = 0;
                int qccount = 0;
                Stime = DateTime.Now;
                //20180109 .emewe 103 只取一条数据
                // string strsql = "select QCRecord_ID,QCRecord_QCCOUNT from QCRecord where QCRecord_QCInfo_ID=" + iQcInfoID + " and QCRecord_RESULT is null and QCRecord_Dictionary_ID=2 and QCRecord_QCCOUNT>=1 and QCRecord_QCCOUNT<='" + (endQCRecord_QCCOUNT) + "' order by QCRecord_QCCOUNT";
                string strsql = "select top 1 QCRecord_ID,QCRecord_QCCOUNT from QCRecord where QCRecord_QCInfo_ID=" + iQcInfoID + " and QCRecord_RESULT is null and QCRecord_Dictionary_ID=2 and QCRecord_QCCOUNT>=1 and QCRecord_QCCOUNT<='" + (endQCRecord_QCCOUNT) + "' order by QCRecord_QCCOUNT";
                DataTable dtqcr = LinQBaseDao.Query(strsql).Tables[0];
                if (dtqcr.Rows.Count > 0)
                {
                    //Common.WriteTextLog("水分显示第五步");
                    for (int i = 0; i < dtqcr.Rows.Count; i++)
                    {
                        int iqcRecordID = Common.GetInt(dtqcr.Rows[i]["QCRecord_ID"].ToString());
                        //20180109 ,emewe 103 只取一条水分数据
                        //DataTable dt = LinQBaseDao.Query("select * from Moisture_test where Moisture_test_Collection_ID in (" + Common.StrCollectionIDS + ")  and Moisture_test_status=0  and Moisture_test_Time>'" + dtime + "' ").Tables[0];
                        DataTable dt = LinQBaseDao.Query("select top 1 * from Moisture_test where Moisture_test_Collection_ID in (" + Common.StrCollectionIDS + ")  and Moisture_test_status=0  and Moisture_test_Time>'" + dtime + "' ").Tables[0];
                        //Common.WriteTextLog("水分显示第六步");
                        if (dt.Rows.Count <= 0)
                        {
                            break;
                        }
                        // Thread.Sleep(Mts);
                        // Thread.Sleep(10000);
                        qccount = Common.GetInt(dtqcr.Rows[i]["QCRecord_QCCOUNT"].ToString());
                        double waterValue = Convert.ToDouble(dt.Rows[0]["Moisture_test_value"].ToString());
                        if (waterValue < Common.MOISTUREMIN || waterValue > Common.MOISTUREMAX) //水分值小于设定的最小值 不接受
                        {
                            LinQBaseDao.Query("update Moisture_test set Moisture_test_status=1 where Moisture_test_ID=" + dt.Rows[0]["Moisture_test_ID"].ToString());
                            ISTimesBusy = false;
                            break;
                        }
                        string cname = dt.Rows[i]["Moisture_test_Numwater"].ToString() == "1" ? cbxUserName.Text : cbxUserNameTwo.Text;
                        string QCRecord_NUMBERem = dt.Rows[0]["Moisture_test_NO"] + string.Empty;
                        string QCRecord_RESULTem = dt.Rows[0]["Moisture_test_value"] + string.Empty;
                        string QCRecord_COUNTem = dt.Rows[0]["Moisture_test_count"] + string.Empty;
                        string QCRecord_UserIdem = Common.GetInt(Common.USERID) + string.Empty;
                        string QCRecord_Dictionary_IDem = DictionaryDAL.GetDictionaryID("启动") + string.Empty;
                        string Moisture_test_IDem = dt.Rows[0]["Moisture_test_ID"] + string.Empty;

                        List<SqlParameter> sps = new List<SqlParameter>{
                                       new SqlParameter(){ ParameterName="@QCRecord_NUMBER", Value=QCRecord_NUMBERem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_RESULT", Value=QCRecord_RESULTem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_COUNT", Value=QCRecord_COUNTem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_UserId", Value=QCRecord_UserIdem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_TestUserName", Value=(cname+string.Empty).Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_Dictionary_ID", Value=QCRecord_Dictionary_IDem.Trim()},
                                       new SqlParameter(){ ParameterName="@QCRecord_ID", Value=(iqcRecordID+string.Empty).Trim()},
                                       new SqlParameter(){ ParameterName="@Moisture_test_ID", Value=Moisture_test_IDem.Trim()},
                                       new SqlParameter(){ ParameterName="@result", Value=Moisture_test_IDem.Trim(), Direction= ParameterDirection.Output, Size=2000}
                                       };
                        LinQBaseDao.ExecuteDataset(CommandType.StoredProcedure, "sp_QCRecord_fun", sps.ToArray());

                        string result = sps[8].Value + string.Empty;
                        //成功
                        if (result.Trim().Equals("success"))
                        {

                            num++;
                            Etime = DateTime.Now;
                            Common.WriteLogData("新增成功", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                        }
                        else
                        {
                            Common.WriteLogData("新增失败", "质检编号：" + iQcInfoID.ToString() + "，新增水份检测记录编号：" + iqcRecordID, "");
                        }


                        if (Etime.Minute.Equals(Stime.Minute))
                        {
                            ts = Etime.Second - Stime.Second;
                        }
                        else
                        {
                            ts = Etime.Second + 60 - Stime.Second;
                        }
                        Common.WriteTextLog("执行开始时间:" + Stime + ", 执行结束时间:" + Etime + ", 时间差：" + ts, "定时器执行水分数据写入到主表 ");
                        if (ts > 3)
                        {
                            ISUpdatedvgOne = false;
                            return;
                        }

                        if (num > 0)
                        {
                            //Common.WriteTextLog("水分显示第七步");
                            // Thread.Sleep(2000);
                            BindDgvWateOne();
                            // updateDGV(iQcInfoID);
                            //this.dgvWateOne.Rows[iCurrentRowCount - 1].ReadOnly = true;
                            ISUpdatedvgOne = false;
                            _iCurrentRowCount = GetCurrentRowCount(iQcInfoID);
                            if (_iCurrentRowCount == iWateRowCount)
                            {
                                CalculateAfterAVG();
                            }
                            else
                            {
                                DataGridViewCurrent(dgvWateOne, qccount);
                                ISTimesBusy = false;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message);
                Common.WriteTextLog(""+err.Message, "错误日志 ");
            }
            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new SetWater(Settxt), txt);

            //    txt.Text = "第" + num + "次：修改的内容";
            //}

        }
        #endregion


        /// <summary>
        /// 从数据库获取当前行
        /// </summary>
        private int GetCurrentRowCount(int qcinfoid)
        {
            //ykWarning:调试使用,查找水份检测完成后卡死原因
            //#if DEBUG
            //            Common.WriteTextLogWate("started： object obj = LinQBaseDao.GetSingle(select  COUNT(1).");
            //#endif
            object obj = LinQBaseDao.GetSingle("select  COUNT(1)  from QCRecord where QCRecord_TestItems_ID in(1,3,4) and QCRecord_RESULT is not null and QCRecord_QCInfo_ID=" + qcinfoid + " and QCRecord_Dictionary_ID=" + DictionaryDAL.GetDictionaryID("启动"));
            //ykWarning:调试使用,查找水份检测完成后卡死原因
            //#if DEBUG
            //            Common.WriteTextLogWate("executed： object obj = LinQBaseDao.GetSingle(select  COUNT(1) ." +　" curRowCount=" + obj);
            //#endif
            return obj != null ? Convert.ToInt32(obj) : 0;
        }


        /// <summary>
        /// 更新列表水分值
        /// </summary>
        /// <param name="qcinfoid"></param>
        private void updateDGV(int qcinfoid)
        {
            try
            {
                if (dgvWateOne.Rows.Count >= _iCurrentRowCount)
                {
                    _iCurrentRowCount = GetCurrentRowCount(qcinfoid);
                    if (_iCurrentRowCount == 0)
                    {
                        return;
                    }
                    else
                    {
                        if (dgvWateOne.Rows[_iCurrentRowCount - 1].Cells["QCRecord_RESULT"].Value == null && _iCurrentRowCount > 0)
                        {
                            DataTable dtResult = LinQBaseDao.Query("select QCRecord_RESULT,QCRecord_TestUserName from QCRecord where QCRecord_TestItems_ID in(1,3,4) and QCRecord_QCCOUNT=" + _iCurrentRowCount + " and QCRecord_RESULT is not null and QCRecord_Dictionary_ID=" + DictionaryDAL.GetDictionaryID("启动") + " and QCRecord_QCInfo_ID=" + qcinfoid).Tables[0];

                            if (dtResult.Rows.Count > 0)
                            {
                                dgvWateOne.Rows[_iCurrentRowCount - 1].Cells["QCRecord_RESULT"].Value = dtResult.Rows[0]["QCRecord_RESULT"].ToString();
                                dgvWateOne.Rows[_iCurrentRowCount - 1].Cells["QCRecord_TestUserName"].Value = dtResult.Rows[0]["QCRecord_TestUserName"].ToString();
                                DataGridViewCurrent(dgvWateOne, _iCurrentRowCount);
                            }

                        }

                        if (_iCurrentRowCount == 1 && dgvWateOne.Rows[0].Cells["QCRecord_RESULT"].Value == null)
                        {
                            DataTable dtResult = LinQBaseDao.Query("select QCRecord_RESULT,QCRecord_TestUserName from QCRecord where QCRecord_TestItems_ID in(1,3,4) and QCRecord_QCCOUNT=" + _iCurrentRowCount + " and QCRecord_RESULT is not null and QCRecord_Dictionary_ID=" + DictionaryDAL.GetDictionaryID("启动") + "  and QCRecord_QCInfo_ID=" + qcinfoid).Tables[0];

                            if (dtResult.Rows.Count > 0)
                            {
                                dgvWateOne.Rows[_iCurrentRowCount - 1].Cells["QCRecord_RESULT"].Value = dtResult.Rows[0]["QCRecord_RESULT"].ToString();
                                dgvWateOne.Rows[_iCurrentRowCount - 1].Cells["QCRecord_TestUserName"].Value = dtResult.Rows[0]["QCRecord_TestUserName"].ToString();
                                DataGridViewCurrent(dgvWateOne, _iCurrentRowCount);
                            }

                        }
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 查找当前水分质检的下标 
        /// </summary>
        /// <returns>true 找到为空行，false未找到为空行</returns>
        private bool ISDgvWateOneRowISNull()
        {
            bool rbool = true;
            try
            {
                if (dgvWateOne.Rows.Count >= _iCurrentRowCount && iWateRowCount >= _iCurrentRowCount)
                {

                    if (dgvWateOne.Rows[_iCurrentRowCount].Cells["QCRecord_RESULT"].Value != null && dgvWateOne.Rows[_iCurrentRowCount].Cells["QCRecord_NUMBER"].Value != null && dgvWateOne.Rows[_iCurrentRowCount].Cells["QCRecord_ID1"].Value != null)
                    {
                        rbool = false;
                        if (_iCurrentRowCount != iWateRowCount)
                        {
                            //iCurrentRowCount++;

                            if (ISDgvWateOneRowISNull()) { return rbool; }
                        }
                    }
                    else
                    {
                        rbool = true;
                    }
                }
            }
            catch
            {


            }


            return rbool;
        }

        private void dgvWateOne_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            DataGridView temp = (DataGridView)sender;
            using (SolidBrush b = new SolidBrush(temp.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, System.Globalization.CultureInfo.CurrentUICulture), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 5);
            }
        }
        private void dgvWateOne_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                Common.SetSelectionBackColor(dgvWateOne);
                DataGridViewCurrent(dgvWateOne, e.RowIndex);
                if ((bool)dgvWateOne.Rows[e.RowIndex].Cells["xz"].EditedFormattedValue != false)
                {
                    dgvWateOne.Rows[e.RowIndex].Cells["xz"].Value = false;
                    dgvWateOne.Rows[e.RowIndex].Selected = false;
                    ids.Remove(e.RowIndex);
                    if (dgvWateOne.Rows[e.RowIndex].Cells["QCRecord_ID1"].Value != null)
                    {
                        ids.Remove(e.RowIndex);
                        int qcrid = EMEWE.Common.Converter.ToInt(dgvWateOne.Rows[e.RowIndex].Cells["QCRecord_ID1"].Value.ToString());
                        listrecordID.Remove(qcrid);
                    }
                }
                else
                {
                    dgvWateOne.Rows[e.RowIndex].Cells["xz"].Value = true;
                    dgvWateOne.Rows[e.RowIndex].Selected = true;
                    ids.Add(e.RowIndex);

                    // string str = dgvWateOne.Rows[e.RowIndex].Cells["QCRecord_ID1"].Value.ToString();

                    if (dgvWateOne.Rows[e.RowIndex].Cells["QCRecord_ID1"].Value != null)
                    {
                        ids.Add(e.RowIndex);
                        int qcrid = EMEWE.Common.Converter.ToInt(dgvWateOne.Rows[e.RowIndex].Cells["QCRecord_ID1"].Value.ToString());
                        listrecordID.Remove(qcrid);
                        listrecordID.Add(qcrid);

                    }
                }
            }
            SelectedDgvWateOne();


        }

        /// <summary>
        /// 当前被选中的列
        /// </summary>
        private void SelectedDgvWateOne()
        {
            //选中多行
            if (ids.Count > 0)
            {
                if (dgvWateOne.DataSource != null)
                {
                    for (int i = 0; i < ids.Count; i++)
                    {
                        dgvWateOne.Rows[ids[i]].Selected = true;

                        dgvWateOne.Rows[ids[i]].Cells["xz"].Value = true;
                    }
                }
            }
        }
        #endregion




        #region 单条记录模块
        /// <summary>
        /// 单条记录列表条件
        /// </summary>
        //   private Expression<Func<View_QCRecordInfo, bool>> p = null;//n => n.Tes_TestItems_ID == iWateItemId;
        /// <summary>
        /// 单条记录查询条件
        /// </summary>
        Expression<Func<View_QCRecordInfo, bool>> expr = null;
        //Expression<Func<View_QCRecordInfo, bool>> expr = (Expression<Func<View_QCRecordInfo, bool>>)PredicateExtensionses.True<View_QCRecordInfo>();
        /// <summary>
        /// 搜索质检单条记录数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQCOneInfo_Click(object sender, EventArgs e)
        {

            if (btnQCOneInfo.Enabled)
            {
                btnQCOneInfo.Enabled = false;
                //p = null;
                //fy = new FenYe();
                //fy.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
                expr = null;
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
                LoadData();
                btnQCOneInfo.Enabled = true;
            }
        }
        /// <summary>
        /// 搜索单条质检记录条件
        /// </summary>
        /// <param name="expr">Expression<Func<View_QCRecordInfo, bool>> </param>
        private void GetCondition()
        {
            try
            {
                var i = 0;
                if (expr == null)
                {
                    expr = (Expression<Func<View_QCRecordInfo, bool>>)PredicateExtensionses.True<View_QCRecordInfo>();
                }

                if (!string.IsNullOrEmpty(txtWEIGHT_TICKET_NO.Text))
                {
                    //expr = expr.And( n => n.WEIGHT_TICKET_NO == txtWEIGHT_TICKET_NO.Text.Trim().ToUpper());
                    expr = expr.And(n => SqlMethods.Like(n.WEIGHT_TICKET_NO, "%" + txtWEIGHT_TICKET_NO.Text.Trim() + "%"));

                    i++;
                }
                if (txtCarNO.Text.Trim() != "")
                {
                    //expr = expr.And( n => n.CNTR_NO == txtCarNO.Text.Trim());

                    expr = expr.And(n => SqlMethods.Like(n.CNTR_NO, "%" + txtCarNO.Text.Trim() + "%"));
                    i++;
                }

                if (cboxState.SelectedValue != null)
                {
                    int stateID = Converter.ToInt(cboxState.SelectedValue.ToString());

                    if (stateID > 0)
                    {
                        expr = expr.And(n => n.Dictionary_ID == stateID);
                        i++;
                    }

                }
                if (cboxTestItems.SelectedValue != null)
                {
                    int itemId = Converter.ToInt(cboxTestItems.SelectedValue.ToString());
                    if (itemId > -1)
                    {
                        if (itemId == iWateItemId)
                        {
                            expr = expr.And(n => n.Tes_TestItems_ID == itemId);
                        }
                        else
                            expr = expr.And(n => n.TestItems_ID == itemId);
                        i++;

                    }
                }
                string beginTime = txtbeginTime1.Value.ToString("yyyy-MM -dd ");
                string endTime = txtendTime1.Value.ToString("yyyy-MM -dd ");
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "
                if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                {
                    expr = expr.And(c => (c.QCRecord_TIME >= DateTime.Parse(begin) && c.QCRecord_TIME <= DateTime.Parse(end)));
                    i++;
                }
                if (i == 0)
                {
                    expr = null;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWate.GetCondition异常" + ex.Message.ToString());
            }

        }


        /// <summary>
        /// 绑定水分检测下拉列表框数据
        /// </summary>
        private void BindCboxTestItems()
        {
            cboxTestItems.DataSource = TestItemsDAL.GetListWhereTestItemName("水分检测", "启动");
            this.cboxTestItems.DisplayMember = "TestItems_NAME";
            cboxTestItems.ValueMember = "TestItems_ID";
        }
        /// <summary>
        /// 绑定水分检测状态下拉列表框数据
        /// </summary>
        private void BindCboxState()
        {
            cboxState.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
            this.cboxState.DisplayMember = "Dictionary_Name";
            cboxState.ValueMember = "Dictionary_ID";
        }
        /// <summary>
        /// 双击单条水分检查列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_SFJC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Common.SetSelectionBackColor(dgv_SFJC);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {

            try
            {
                GetCondition();
                this.dgv_SFJC.AutoGenerateColumns = false;
                dgv_SFJC.DataSource = page.BindBoundControl<View_QCRecordInfo>("", txtCurrentPage1, lblPageCount1, expr, "QCRecord_TIME desc");

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWate.LoadData异常：" + ex.Message.ToString());
            }

        }
        /// <summary>
        /// 单条记录分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorQCRecord_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            dgv_SFJC.DataSource = page.BindBoundControl<View_QCRecordInfo>(e.ClickedItem.Name, txtCurrentPage1, lblPageCount1, expr, "QCRecord_TIME desc");
        }
        /// <summary>
        /// 单条记录分页改变每页条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fy = new FenYe();
            //fy.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();

            page = new PageControl();
            page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
            LoadData();
        }


        #endregion

        #region 平均水分计算


        /// <summary>
        /// 计算平均数
        /// 2012-08-16 周意
        /// </summary>
        private void CalculateAfterAVG()
        {
            if (iQcInfoID != 0)
            {
                string strzavg = "";
                StringBuilder sb = new StringBuilder();
                //                #if DEBUG  //ykWarning:调试使用,查找水份检测完成后卡死原因
                //                Common.WriteTextLogWate("QCRecordDAL.GetQCRecord_RESULT_AVERAGE started.");
                //#endif
                List<QCRecordAVGEnitiy> list = QCRecordDAL.GetQCRecord_RESULT_AVERAGE(iQcInfoID, iQCRetest_ID, "水分检测");
                //#if DEBUG  //ykWarning:调试使用,查找水份检测完成后卡死原因
                //Common.WriteTextLogWate("QCRecordDAL.GetQCRecord_RESULT_AVERAGE executed.");
                //#endif
                foreach (QCRecordAVGEnitiy qc in list)
                {
                    if (qc.Avg <= 0 || double.IsNaN(qc.Avg))
                    {
                        Common.WriteTextLogWate("qc.Avg =" + Convert.ToString(qc.Avg));
                        continue;
                    }
                    string stravg = GetData.GetStringMath(qc.Avg.ToString(), 2);
                    if (qc.TestItemName == "水分检测")
                    {
                        strzavg = stravg;
                        lblQCInfo_MOIST_PER_SAMPLE.Text = strzavg;
                    }
                }
                if (!string.IsNullOrEmpty(lblQCInfo_MOIST_PER_SAMPLE.Text))
                {
                    txtQCInfo_MOIST_PER_SAMPLE.Text = strzavg;
                    ISTimesBusy = true;
                }
                else
                {
                    sb.Append("水分平均值不能计算，请检查水分点数是否已检测完成。");
                    MessageBox.Show(sb.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // txtOneWateAvg.Text = QCRecordDAL.GetQCRecord_RESULT_AVERAGE(txtWEIGHTTICKETNO.Text.Trim());
            }
        }


        /// <summary>
        /// 修改水分检测员选择下标
        /// </summary>
        private void editWaterManSelect()
        {
            if (cbxUserName.SelectedIndex != Common.waterManSelect || cbxUserNameTwo.SelectedIndex != Common.waterManSelectTwo)
            {
                try
                {
                    string filepath = System.IO.Directory.GetCurrentDirectory() + "\\SystemSet.xml";
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filepath);
                    XmlNode xn = xmlDoc.SelectSingleNode("param");//查找<bookstore>
                    XmlNodeList xnl = xn.ChildNodes;
                    if (xnl.Count > 0)
                    {
                        foreach (XmlNode xnf in xnl)
                        {
                            XmlElement xe = (XmlElement)xnf;
                            xe.SetAttribute("waterManSelect", cbxUserName.SelectedIndex.ToString());
                            xe.SetAttribute("waterManSelectTwo", cbxUserNameTwo.SelectedIndex.ToString());
                        }
                        xmlDoc.Save(filepath);
                        Common.waterManSelect = cbxUserName.SelectedIndex;
                        Common.waterManSelectTwo = cbxUserNameTwo.SelectedIndex;
                    }
                }
                catch (Exception ex)
                {
                    Common.WriteTextLogWate("参数保存到SystemSet.xml失败。失败原因为：" + ex.Message);
                }
            }
        }


        /// <summary>
        /// 保存当前检查的水分平均值
        /// 先计算在保存
        /// 修改：质检人为界面选择的质检员名称。 2012-11-18
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled)
            {
                btnSave.Enabled = false;
                if (iQcInfoID != 0)
                {
                    try
                    {
                        editWaterManSelect();
                        //#if DEBUG  //ykWarning:调试使用，查找系统卡死原因
                        //                        Common.WriteTextLogWate("QCInfo qc = QCInfoDAL.GetQCInfo(iQcInfoID) started.");
                        //#endif
                        QCInfo qc = QCInfoDAL.GetQCInfo(iQcInfoID);
                        //                        #if DEBUG  //ykWarning:调试使用，查找系统卡死原因
                        //                        Common.WriteTextLogWate("QCInfo qc = QCInfoDAL.GetQCInfo(iQcInfoID) ended.");
                        //#endif
                        if (ckbQCState.Checked == true)//选择终止质检必须填写终止原因
                        {
                            if (string.IsNullOrEmpty(txtQCInfo_REMARK.Text))
                            {
                                mf.ShowToolTip(ToolTipIcon.Warning, "保存水分检测平均结果", "终止原因不能为空", txtQCInfo_REMARK, this);
                                btnSave.Enabled = true;
                                return;
                            }
                            else
                            {
                                qc.QCInfo_Dictionary_ID = DictionaryDAL.GetDictionaryID("终止质检");
                                qc.QCInfo_REMARK = txtQCInfo_REMARK.Text.Trim();
                            }
                        }
                        qc.QCInfo_MOIST_EXAMINER = cbxUserName.Text + "，" + cbxUserNameTwo.Text;
                        qc.QCInfo_MOIST_Count = iWateRowCount;
                        if (!string.IsNullOrEmpty(txtQCInfo_MOIST_PER_SAMPLE.Text))
                        {
                            qc.QCInfo_MOIST_PER_SAMPLE = Converter.ToDecimal(txtQCInfo_MOIST_PER_SAMPLE.Text);
                            bool b = ADDUnusual(iQcInfoID, "水分检测", Convert.ToDouble(qc.QCInfo_MOIST_PER_SAMPLE), "Unusualstandard_DEGRADE_MOISTURE_PERCT");
                            if (b == true)
                            {
                                qc.QCInfo_Dictionary_ID = DictionaryDAL.GetDictionaryID("暂停质检");
                                MessageBox.Show("水分检测平均值异常！");
                            }
                        }

                        #region 临时只检测水分  2011-12-14  周意
                        bool zjwc = false;//质检是否完成
                        if (!string.IsNullOrEmpty(txtQCInfo_MOIST_PER_SAMPLE.Text))
                        {
                            zjwc = true;
                        }
                        #endregion

                        //#if DEBUG //ykWarning:调试使用，查找系统卡死原因
                        //Common.WriteTextLogWate("rbool = QCInfoDAL.UpdateInfo(qc) started.");
                        //#endif
                        bool rbool = QCInfoDAL.UpdateInfo(qc);

                        //#if DEBUG //ykWarning:调试使用，查找系统卡死原因
                        //Common.WriteTextLogWate("rbool = QCInfoDAL.UpdateInfo(qc) executed." + rbool.ToString());
                        //#endif
                        if (rbool)
                        {
                            if (ckbQCState.Checked == true)
                            {
                                lblCurrentQcState.Text = "终止质检";
                            }
                            if (zjwc)
                            {
                                ids.Clear();
                                listrecordID.Clear();
                            }
                            string strContent = "质检编号为：" + iQcInfoID.ToString() + "，修改水分检测汇总结果";
                            Common.WriteLogData("修改", strContent, "");
                            ISTimesBusy = true;
                            btnSave.Enabled = true;
                            MessageBox.Show("保存水分检测结果", "保存成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            #region 是否发送到U9
                            string sql = "select	U9Bool from dbo.U9Start where U9Name='水分检测'";
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
                            #endregion
                        }
                        else
                        {
                            btnSave.Enabled = true;
                        }
                    }
                    catch (Exception ex) //ykWarning:加入异常处理，以查找系统卡死原因
                    {
                        btnSave.Enabled = true;
                        MessageBox.Show("保存水分检测结果", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Common.WriteTextLogWate(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("保存水分检测平均结果", "请在待水分检测信息中选择检测信息！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = true;
                    return;
                }
            }


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

                lblCurrentQcState.Text = "暂停质检";

                DataTable TestItems_NAME_DT = LinQBaseDao.Query("select TestItems_ID from TestItems where TestItems_Name='" + testItemName + "'").Tables[0];
                if (TestItems_NAME_DT.Rows.Count > 0)
                {


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


                    #region 取内容
                    //string[] SMSContent = SMSDs.Tables[0].Rows[0]["SMSConfigure_SendContent"].ToString().Split(',');
                    //string[] SMSContentTxt = SMSDs.Tables[0].Rows[0]["SMSConfigure_SendContentText"].ToString().Split(',');
                    //string Contents = SMSSendContent(SMSContent, u.Unusual_Id, SMSContentTxt, u.Unusual_content);//存储短信内容
                    #endregion

                    #region 取号码、姓名,并对其号码发送短信（号码和姓名同位置是一组数据）
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
                    #endregion

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

        /// <summary>
        /// 改变多选框“终止质检”时将终止原因填入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbQCState_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbQCState.Checked == true)
            {

                txtQCInfo_REMARK.Enabled = true;
            }
            else
            {
                txtQCInfo_REMARK.Enabled = false;
            }
        }
        #endregion



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
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOFFQcRecord_Click(object sender, EventArgs e)
        {
            if (btnOFFQcRecord.Enabled)//判断按钮是否被点击，如果点击按钮设置成Enabled = false，如果验证未通过Enabled = true;
            {
                btnOFFQcRecord.Enabled = false;

                int userId1 = 0;
                int userId2 = 0;
                int userId3 = 0;
                string strcardControl1 = cardControl1.TXTCARD;
                string strcardControl2 = cardControl2.TXTCARD;
                string strcardControl3 = cardControl3.TXTCARD;
                try
                {
                    if (listrecordID.Count <= 0)
                    {
                        btnOFFQcRecord.Enabled = true;
                        TabControl1.SelectedTab.Name = "tabPageWater1";
                        mf.ShowToolTip(ToolTipIcon.Warning, "确认修改", "请选择修改项", dgvWateOne, this);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQCRecord_UPDATE_REASON.Text.ToString()))
                    {
                        btnOFFQcRecord.Enabled = true;
                        mf.ShowToolTip(ToolTipIcon.Warning, "确认修改", "修改原因不能为空", txtQCRecord_UPDATE_REASON, this);
                        return;
                    }
                    #region 这个三个顺序不能颠倒 ,界面依次放，验证时依次放
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
                    #region 理文2卡验证，屏掉
                    //if (!mf.VerificationCard(cardControl3, this, out userId3))
                    //{
                    //    //btnOFFQcRecord.Enabled = true;
                    //    //SetCarControlISNUll(strcardControl1, strcardControl2, "");
                    //    //return;
                    //}
                    //else if (cardControl2.LblCardNO == cardControl3.LblCardNO || cardControl1.LblCardNO == cardControl3.LblCardNO)
                    //{
                    //    btnOFFQcRecord.Enabled = true;
                    //    cardControl3.ISPass = true;
                    //    SetCarControlISNUll(strcardControl1, strcardControl2, "");
                    //    mf.ShowToolTip(ToolTipIcon.Warning, "确认修改", "当前卡号与其它卡号相同，请重新刷卡", cardControl2, this);

                    //    return;
                    //}
                    #endregion
                    #endregion

                    ///“注销”状态编号
                    int iDictionaryID = DictionaryDAL.GetDictionaryID("注销");
                    ///“已审批”状态编号
                    int isp = DictionaryDAL.GetDictionaryID("已审批");
                    bool rbool = false;
                    foreach (int iqcrecordID in listrecordID)
                    {
                        QCRecord record1 = QCRecordDAL.GetQCInfo(iqcrecordID);
                        QCRecord record = record1;
                        record.QCRecord_UserId_UpdateID = EMEWE.Common.Converter.ToInt(Common.USERID);
                        DateTime dt = DateTime.Now;
                        record.QCRecord_UPDATE_TIME = dt;
                        record.QCRecord_UPDATE_REASON = txtQCRecord_UPDATE_REASON.Text.Trim();
                        record.QCRecord_Dictionary_ID = iDictionaryID;//注销
                        record.QCRecord_UpdateCardUserName = cardControl1.TXTCARD + "," + cardControl2.TXTCARD;
                        rbool = QCRecordDAL.UpdateOneQcRecord(record);
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
                                //if (rbool)
                                //{
                                //    OFFQCRecord offqc3 = new OFFQCRecord();
                                //    offqc3.OFFQCRecord_QCRecord_ID = iqcrecordID;
                                //    offqc3.OFFQCRecord_Dictionary_ID = isp;//审批
                                //    offqc3.OFFQCRecord_Time = dt;
                                //    offqc3.OFFQCRecord_UserId = userId3;//卡号对应的用户编号
                                //    rbool = OFFQCRecordDAL.InsertOFFQCRecord(offqc3, out ioff);
                                //}
                            }
                            else
                            {
                                QCRecordDAL.UpdateOneQcRecord(record1);
                            }

                        }
                        if (rbool)
                        {
                            string strContent = "质检编号为：" + iQcInfoID.ToString() + "，权限授权注销水份检测记录编号为" + iqcrecordID + ",刷卡授权人：" + cardControl1.TXTCARD + "、" + cardControl2.TXTCARD + "、" + cardControl3.TXTCARD;
                            Common.WriteLogData("注销", strContent, "");
                            int rintid = 0;//record1.QCRecord_QCCOUNT, record1.QCRecord_TestItems_ID
                            Addqcrecord(record1.QCRecord_QCCOUNT.Value, record1.QCRecord_TestItems_ID.Value, out rintid, record1.QCRecord_DRAW.ToString());
                        }
                    }
                    #region 清除之前的数据
                    ClearOFFQCRecord();

                    TabControl1.SelectedIndex = 1;
                    #endregion
                }
                catch (Exception ex)
                {
                    btnOFFQcRecord.Enabled = true;
                    Common.WriteTextLog("确认修改FormWate.btnOFFQcRecord_Click异常：" + ex.Message);
                }
            }

        }
        /// <summary>
        /// 清空修改数据和关闭修改确认按钮
        /// </summary>
        private void ClearOFFQCRecord()
        {
            btnOFFQcRecord.Enabled = false;
            ids.Clear();
            // listrecordID.Clear();
            txtQCRecord_UPDATE_REASON.Text = "";
            cardControl1.TXTCARD = "";
            cardControl2.TXTCARD = "";
            cardControl3.TXTCARD = "";

            cardControl1.ISPass = true;
            cardControl2.ISPass = true;
            cardControl3.ISPass = true;
        }



        /// <summary>
        /// 质检信息中选择修改质检状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxQCInfoState_SelectedIndexChanged(object sender, EventArgs e)
        {


            //if (cbxQCInfoState.SelectedIndex != null)
            //{
            //    int stateID = cbxQCInfoState.SelectedIndex;

            //    if (stateID >= 0)
            //    {
            //        Dictionary dic = cbxQCInfoState.SelectedItem as Dictionary;
            //        if (dic.Dictionary_Value == "终止质检")
            //        {
            //            txtQCInfo_REMARK.Enabled = true;
            //        }
            //        else
            //        {
            //            txtQCInfo_REMARK.Enabled = false;
            //        }
            //    }

            //}


        }



        //无卡 有权限直接修改
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
            foreach (int iqcrecordID in listrecordID)
            {
                QCRecord record1 = QCRecordDAL.GetQCInfo(iqcrecordID);
                QCRecord record = record1;
                record.QCRecord_UserId_UpdateID = EMEWE.Common.Converter.ToInt(Common.USERID);
                DateTime dt = DateTime.Now;
                record.QCRecord_UPDATE_TIME = dt;
                record.QCRecord_UPDATE_REASON = txtQCRecord_UPDATE_REASON.Text.Trim();
                record.QCRecord_Dictionary_ID = iDictionaryID;//注销

                rbool = QCRecordDAL.UpdateOneQcRecord(record);

                if (rbool)
                {
                    string strContent = "质检编号为：" + iQcInfoID.ToString() + "，权限授权注销水份检测记录编号为" + iqcrecordID + ",刷卡授权人：" + cardControl1.TXTCARD + "、" + cardControl2.TXTCARD + "、" + cardControl3.TXTCARD;
                    Common.WriteLogData("注销", strContent, "");
                    int rintid = 0;//record1.QCRecord_QCCOUNT, record1.QCRecord_TestItems_ID
                    Addqcrecord(record1.QCRecord_QCCOUNT.Value, record1.QCRecord_TestItems_ID.Value, out rintid, record1.QCRecord_DRAW.ToString());
                }
            }
            #region 清除之前的数据
            ClearOFFQCRecord();

            TabControl1.SelectedIndex = 1;
            #endregion

        }

        private void FormWate_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormWate.fwt = null;
        }

        private void FormWate_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormWate.fwt = null;
        }

        private void txtWaitWEIGHTTICKETNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                waitexprp = null;
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                BindDgvWaitQC();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timerData.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timerData.Stop();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Mts = textBox1.Text != null ? Convert.ToInt32( textBox1.Text) : 0;
        }
    }
}
