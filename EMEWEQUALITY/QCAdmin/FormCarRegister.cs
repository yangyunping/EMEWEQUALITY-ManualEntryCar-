using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EMEWEEntity;
using EMEWEQUALITY.HelpClass;
using System.Data.Linq.SqlClient;
using System.Drawing.Printing;
using System.Linq.Expressions;
using EMEWEDAL;
using System.Data;
using System.Text;
using EMEWEQUALITY.QCAdmin;
using EMEWEQUALITY.SystemAdmin;

namespace EMEWEQUALITY
{
    partial class FormCarRegister : Form
    {

        /// <summary>
        /// 生明无返回值的委托
        /// </summary>
        private delegate void AsynUpdateUI();

        /// <summary>
        /// 引用正在加载
        /// </summary>
        public Form_Loading form_loading = null;

        public FormCarRegister()
        {
            InitializeComponent();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            TimeEndIsNull();
            cob_DengJi.SelectedIndex = 2;
            Bangding_Load(RegistrationWhere());
            tsbtnDel.Visible = (Common.RoleID == 1 || Common.RoleID == 2) ? true : false;
        }
        /// <summary>
        /// 初始化时间
        /// </summary>
        private void TimeEndIsNull()
        {
            //# 2017年08月26日 103修改 
            //DateTime Time = DateTime.Now.AddDays(-1);
            DateTime Time = DateTime.Now.AddDays(0);
            txtbeginTime.Value = Time;
            txtendTime.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));

        }
        /// <summary>
        /// 条件
        /// </summary>
        /// <returns>条件</returns>
        public string RegistrationWhere()
        {
            string where = "";
            try
            {
                if (txt_WEIGHT_TICKET_NO.Text.Trim() != "")//磅单号
                {
                    where = " WEIGHT_TICKET_NO like '%" + txt_WEIGHT_TICKET_NO.Text.Trim() + "%'";

                }
                if (txt_CNTR_NO.Text.Trim() != "")//车牌号
                {
                    if (where != "")
                    {
                        where = where + " and CNTR_NO like '%" + txt_CNTR_NO.Text.Trim() + "%'";
                    }
                    else
                    {
                        where = "  CNTR_NO like '%" + txt_CNTR_NO.Text.Trim() + "%'";
                    }
                }
                if (txt_DRAW_EXAM_INTERFACE_ID.Text.Trim() != "")//抽样编号
                {
                    if (where != "")
                    {
                        where = where + " and DRAW_EXAM_INTERFACE_ID like '%" + txt_DRAW_EXAM_INTERFACE_ID.Text.Trim() + "%'";
                    }
                    else
                    {
                        where = "  DRAW_EXAM_INTERFACE_ID like '%" + txt_DRAW_EXAM_INTERFACE_ID.Text.Trim() + "%'";
                    }
                }
                if (txt_SHIPMENT_NO.Text.Trim() != "")//送货单号
                {
                    if (where != "")
                    {
                        where = where + " and SHIPMENT_NO like '%" + txt_SHIPMENT_NO.Text.Trim() + "%'";
                    }
                    else
                    {
                        where = "  SHIPMENT_NO like '%" + txt_SHIPMENT_NO.Text.Trim() + "%'";
                    }
                }
                if (!string.IsNullOrEmpty(cob_DengJi.Text))//登记状态
                {

                    if (cob_DengJi.Text == "已登记")
                    {
                        if (where != "")
                        {
                            where = where + " and IS_DengJi ='Y'";
                        }
                        else
                        {
                            where = "  IS_DengJi ='Y'";
                        }
                    }
                    else if (cob_DengJi.Text == "未登记")
                    {
                        if (where != "")
                        {
                            where = where + " and IS_DengJi is null";
                        }
                        else
                        {
                            where = "  IS_DengJi is null";
                        }
                    }

                }
                if (!string.IsNullOrEmpty(CBChoosePacks.Text))//是否现场抽包
                {

                    if (CBChoosePacks.Text == "是")
                    {
                        if (where != "")
                        {
                            where = where + " and Packets_this  !=''";
                        }
                        else
                        {
                            where = "  Packets_this  !=''";
                        }
                    }
                    else if (CBChoosePacks.Text == "否")
                    {
                        if (where != "")
                        {
                            where = where + " and Packets_this =''";
                        }
                        else
                        {
                            where = "  Packets_this =''";
                        }
                    }

                }
                if (!string.IsNullOrEmpty(txt_PO_NO.Text.Trim()))//采购单
                {
                    if (where != "")
                    {
                        where = where + " and  PO_NO like '%" + txt_PO_NO.Text.Trim() + "%'";
                    }
                    else
                    {
                        where = "  PO_NO like '%" + txt_PO_NO.Text.Trim() + "%'";
                    }

                }
                string beginTime = txtbeginTime.Value.ToString("yyyy-MM -dd ");
                string endTime = txtendTime.Value.ToString("yyyy-MM -dd ");
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "
                if (beginTime != "")//开始时间
                {
                    if (where != "")
                    {
                        where = where + " and  CREATE_DTTM > '" + begin + "'";
                    }
                    else
                    {
                        where = "  CREATE_DTTM > '" + begin + "'";
                    }
                }
                if (endTime != "")//结束时间   QCInfo表字段:QCIInfo_EndTime
                {
                    if (where != "")
                    {
                        where = where + " and  CREATE_DTTM < '" + end + "'";
                    }
                    else
                    {
                        where = "  CREATE_DTTM < '" + end + "'";
                    }
                }



            }
            catch { }
            return where;
        }
        int zongyeshu = 0;
        /// <summary>
        /// 数据绑定事件
        /// </summary>
        /// <param name="where"></param>
        public void Bangding_Load(string where)
        {
            form_loading = new Form_Loading();
            form_loading.ShowLoading(this);
            form_loading.Text = "车辆登记数据加载中...";
            int numh = int.Parse(tscbxPageSize2.Text);
            int numy = int.Parse(txtCurrentPage2.Text);
            string countsql = "select COUNT (1) from dbo.DRAW_EXAM_INTERFACE left join dbo.Packets on DRAW_EXAM_INTERFACE.DRAW_EXAM_INTERFACE_ID=Packets.Packets_QCInfo_DRAW_EXAM_INTERFACE_ID ";
            if (where != "")
            {
                countsql = countsql + " where " + where;
            }
            //根据拼接出来的SQL语句查询出总行数
            string tiaoshu = LinQBaseDao.GetSingle(countsql).ToString();
            zongyeshu = (numh + int.Parse(tiaoshu)) / numh;
            lblPageCount2.Text = "共" + zongyeshu + "页|共" + tiaoshu + "条";//把查询出来的总行数显示出来


            int countnum = int.Parse(LinQBaseDao.GetSingle(countsql).ToString());//总行数
            int rows = int.Parse(tscbxPageSize2.Text);//每页显示的行数
            //lieramo begin update 20160901
            //string sql = "select top(" + numh + ") DRAW_EXAM_INTERFACE_ID,REF_NO,CNTR_NO,SHIPMENT_NO,COMPANY_ID,PO_NO,PROD_ID,Packets_ID,WEIGHT_TICKET_NO,WEIGHT_DATE,NO_OF_BALES,IS_FINISHED,CREATE_BY,CREATE_DTTM,FINISHED_BY,FINISHED_DTTM,DEGRADE_MOISTURE_PERCT,DEGRADE_MATERIAL_PERCT,DEGRADE_OUTTHROWS_PERCT,IS_DengJi,Packets_DTS,Packets_this,Packets_Time from dbo.DRAW_EXAM_INTERFACE left join dbo.Packets on DRAW_EXAM_INTERFACE.DRAW_EXAM_INTERFACE_ID=Packets.Packets_QCInfo_DRAW_EXAM_INTERFACE_ID where ";
            string sql = "select top(" + numh + ") supplyName,DRAW_EXAM_INTERFACE_ID,REF_NO,CNTR_NO,SHIPMENT_NO,COMPANY_ID,PO_NO,PROD_ID,Packets_ID,WEIGHT_TICKET_NO,WEIGHT_DATE,NO_OF_BALES,IS_FINISHED,CREATE_BY,CREATE_DTTM,FINISHED_BY,FINISHED_DTTM,DEGRADE_MOISTURE_PERCT,DEGRADE_MATERIAL_PERCT,DEGRADE_OUTTHROWS_PERCT,IS_DengJi,Packets_DTS,Packets_this,Packets_Time from dbo.DRAW_EXAM_INTERFACE left join dbo.Packets on DRAW_EXAM_INTERFACE.DRAW_EXAM_INTERFACE_ID=Packets.Packets_QCInfo_DRAW_EXAM_INTERFACE_ID where ";
            //end update
            string sql2 = "DRAW_EXAM_INTERFACE_ID not in (select top(" + (numh * (numy - 1)) + ")DRAW_EXAM_INTERFACE_ID from dbo.DRAW_EXAM_INTERFACE left join dbo.Packets on DRAW_EXAM_INTERFACE.DRAW_EXAM_INTERFACE_ID=Packets.Packets_QCInfo_DRAW_EXAM_INTERFACE_ID  ";
            string orderby = " order by WEIGHT_DATE desc ";
            if (where.Length > 0)
            {
                sql = sql + where + " and " + sql2 + " where " + where + orderby + " ) " + orderby;
            }
            else
            {
                sql = sql + sql2 + orderby + " ) " + orderby;

            }
            DataSet ds = LinQBaseDao.Query(sql);
            try
            {
                lvwUserList.DataSource = ds.Tables[0];

            }
            catch (Exception)
            {
            }

            form_loading.CloseLoading(this);
        }
        /// <summary>
        /// 是否登记过
        /// </summary>
        /// <returns>true 已登记， false 未登记</returns>
        private bool YesNoDengJi(int DRAW_EXAM_INTERFACE_ID)
        {
            bool rbool = true;


            string strSQl = "select count(*) from DRAW_EXAM_INTERFACE where DRAW_EXAM_INTERFACE_ID=" + DRAW_EXAM_INTERFACE_ID + " and IS_DengJi='Y'";
            object obj = LinQBaseDao.GetSingle(strSQl);
            int rint = 0;
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out rint);
                if (rint <= 0)
                {
                    rbool = false;
                }
                else
                {
                    rbool = true;
                }

            }
            return rbool;

        }
        DCQUALITYDataContext dc = new DCQUALITYDataContext();
        /// <summary>
        /// 选中行列表
        /// </summary>
        List<int> ids = new List<int>();
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
        /// 抽包
        /// </summary>
        /// <param name="qcinfoid"></param>
        private void getPackets(int qcinfoid, int did)
        {
            try
            {
                //DTS包号
                List<string> listbags = QCInfoDAL.GetBags(qcinfoid);
                //总包数
                DataTable dtNO_OF_BALES = LinQBaseDao.Query("select NO_OF_BALES  from dbo.DRAW_EXAM_INTERFACE where DRAW_EXAM_INTERFACE_ID=" + did).Tables[0];
                string Packets_DTS = "";//DTS   string类型包号
                foreach (string item in listbags)
                {
                    Packets_DTS += item + ",";
                }
                Packets_DTS = Packets_DTS.TrimEnd(',');
                Packets p = new Packets();
                p.Packets_DTS = Packets_DTS;
                p.Packets_this = "";
                p.Packets_QCInfo_DRAW_EXAM_INTERFACE_ID = did;
                p.Packets_Time = DateTime.Now;
                PacketsDAL.InsertOneQCRecord(p);
                Common.GetSumWaterCount(qcinfoid);
                LinQBaseDao.Query("update QCInfo set QCInfo_DRAW='" + Packets_DTS + "',QCInfo_PumpingPackets=" + Packets_DTS.Split(',').Count() + ",QCInfo_MOIST_Count=" + Common.SumWaterCount + " where QCInfo_ID=" + qcinfoid);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 登记方法
        /// </summary>
        private void QcInfoAdd()
        {
            try
            {
                if (lvwUserList.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(lvwUserList.SelectedRows[0].Cells["DRAW_EXAM_INTERFACE_ID"].Value);
                    StringBuilder sbMessage = new StringBuilder();
                    int QCInfo_Dictionary_ID = 0;
                    int QCInfo_STATE = 0;
                    var dictionary = DictionaryDAL.Query();
                    foreach (var m in dictionary)
                    {
                        if (m.Dictionary_Name == "质检中")
                        {
                            QCInfo_Dictionary_ID = m.Dictionary_ID;
                        }
                        if (m.Dictionary_Name == "启动")
                        {
                            QCInfo_STATE = m.Dictionary_ID; //数据状态（启动）
                        }
                    }
                    int irnt = 0;

                    #region ykWarning： 和U9交互 纸种为 LSOP1的不和U9交互 ，测试的时候，可以删除U9交互，发布的时候务必要还原。
                    //#if !DEBUG
                    string PROD_ID = lvwUserList.SelectedRows[0].Cells["PROD_ID"].Value.ToString();
                    if (isIdebarJoinU9(PROD_ID))
                    {
                        DataSet ds = LinQBaseDao.Query("select SHIPMENT_NO,CNTR_NO from dbo.DRAW_EXAM_INTERFACE where DRAW_EXAM_INTERFACE_ID=" + id);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string SHIPMENT_NO = ds.Tables[0].Rows[0][0].ToString();
                            if (!gainData(SHIPMENT_NO, id))
                            {
                                string CNTR_NO = ds.Tables[0].Rows[0][0].ToString();
                                MessageBox.Show(CNTR_NO + "从U9获取数据失败，不能登记！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("登记失败！");
                            return;
                        }
                    }
                    else
                    {
                        DataSet ds = LinQBaseDao.Query("select SHIPMENT_NO,CNTR_NO from dbo.DRAW_EXAM_INTERFACE where DRAW_EXAM_INTERFACE_ID=" + id);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RegisterLoosePaperDistribution rpdb = new RegisterLoosePaperDistribution();
                            rpdb.R_DRAW_EXAM_INTERFACE_ID = id;
                            rpdb.issend = false;
                            LinQBaseDao.InsertOne(dc, rpdb);
                        }
                    }
                    //#endif
                    #endregion

                    if (YesNoDengJi(id))
                    {
                        MessageBox.Show("该信息已登记过", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Bangding_Load(RegistrationWhere());//更新数据
                        ids.Clear();
                        return;
                    }
                    var qf = new QCInfo();

                    qf.QCInfo_Dictionary_ID = QCInfo_Dictionary_ID;
                    qf.QCInfo_STATE = QCInfo_STATE;
                    int k = QCInfoDAL.GetIntDRAWCount(id);//抽样总次数
                    qf.QCInfo_PumpingPackets = k;
                    #region 周意 规则加上后需要修改
                    //e ReckonWaterTestCount(qf.QCInfo_ID);
                    qf.QCInfo_UnpackBefore_MOIST_PER_COUNT = OneCQCount + TwoCQCount;
                    qf.QCInfo_UnpackBack_MOIST_COUNT = OneCHCount + TwoCHCount;
                    #endregion
                    qf.QCInfo_DRAW_EXAM_INTERFACE_ID = id;
                    qf.QCInfo_DEGREE = 1;         //检测次数
                    qf.QCInfo_TIME = DateTime.Now;//日期时间
                    qf.QCInfo_Client_ID = Common.CLIENTID;//客户端配置编号
                    qf.QCInfo_UserId = EMEWE.Common.Converter.ToInt(Common.USERID); //记录人：当前登录人

                    //修改抽样表状态
                    Action<DRAW_EXAM_INTERFACE> ap = n =>
                    {
                        //liemrao begin update 20160901
                        //n.IS_DengJi = Convert.ToChar("Y");
                        n.IS_DengJi = "Y";
                        //end update
                    };
                    Expression<Func<DRAW_EXAM_INTERFACE, bool>> p1 = n => n.DRAW_EXAM_INTERFACE_ID == id;
                    bool rboolDRAW_EXAM_INTERFACEDAL = DRAW_EXAM_INTERFACEDAL.Update(p1, ap);
                    if (rboolDRAW_EXAM_INTERFACEDAL)
                    {
                        if (QCInfoDAL.addQCInfoDAL(qf))
                        {
                            irnt++;
                            sbMessage.Append(id + ",");
                            Common.WriteLogData("新增", "登记数据抽样编号为：" + id, Common.NAME); //操作日志
                            #region 抽包
                            getPackets(qf.QCInfo_ID, id);
                            #endregion
                        }
                        else
                        {
                            //修改抽样表状态
                            Action<DRAW_EXAM_INTERFACE> ap2 = n =>
                            {
                                n.IS_DengJi = null;
                            };
                            Expression<Func<DRAW_EXAM_INTERFACE, bool>> p2 = n => n.DRAW_EXAM_INTERFACE_ID == id;
                            DRAW_EXAM_INTERFACEDAL.Update(p2, ap2);
                        }
                    }
                    if (!string.IsNullOrEmpty(sbMessage.ToString()))
                    {
                        DialogResult dia = MessageBox.Show("登记数据抽样编号为：" + sbMessage.ToString() + "成功\r\n是否现场抽包？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dia.Equals(DialogResult.Yes))
                        {
                            tChoosePacks.Enabled = false;
                            ChoosePacks();
                            tChoosePacks.Enabled = true;
                        }
                    }
                    else//没有选中
                    {
                        MessageBox.Show("请选择列表再开始质检！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            catch
            {
                MessageBox.Show("登记失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                Bangding_Load(RegistrationWhere());//更新数据
            }


        }

        /// <summary>
        /// 获取U9数据
        /// </summary>
        private bool gainData(string DeliveryNoteID, int DRAW_EXAM_INTERFACE_ID)
        {
            try
            {
                LWGETWebReference.QualityPaperManagement_v001 LWGET = new LWGETWebReference.QualityPaperManagement_v001();
                LWGET.Url = Common.GainURL;
                string OrganizationID = Common.OrganizationID;
                string DepartmentName = "";//部门名称
                string BinID = "";//堆位ID
                string BinName = "";//堆位名称
                LWGET.UseDefaultCredentials = true;
                LWGET.RequestHeader = new LWGETWebReference.RequestHeaderType() { SourceID = Common.SourceID };
                //返回部门ID
                //DeliveryNoteID = "DHPOF00077015";
                string DepartmentID = LWGET.GetBinForDeliveryNote(OrganizationID, DeliveryNoteID, out DepartmentName, out BinID, out BinName);
                if (!string.IsNullOrEmpty(DepartmentID))
                {
                    //限制只能有一条数据
                    DataTable dtRLPD = LinQBaseDao.Query("select * from RegisterLoosePaperDistribution where R_DRAW_EXAM_INTERFACE_ID=" + DRAW_EXAM_INTERFACE_ID).Tables[0];
                    if (dtRLPD.Rows.Count == 0)
                    {
                        RegisterLoosePaperDistribution rpdb = new RegisterLoosePaperDistribution();
                        rpdb.OrganizationID = OrganizationID;
                        rpdb.DepartmentCode = DepartmentID;
                        rpdb.ExtensionField2 = BinID;
                        rpdb.R_DRAW_EXAM_INTERFACE_ID = DRAW_EXAM_INTERFACE_ID;
                        rpdb.issend = false;
                        if (LinQBaseDao.InsertOne(dc, rpdb))
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
                        return true;
                    }
                }
                else
                {
                    return false;
                }
                //DepartmentID：050703  BinName：15-25國廢倉 BinID：15-25    DepartmentName：生産部-二十一車間(PM13)-21車間制漿
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 是否需要和U9交互
        /// </summary>
        /// <param name="PROD_ID"></param>
        private bool isIdebarJoinU9(string PROD_ID)
        {
            dc = new DCQUALITYDataContext();
            List<idebarJoinU9> ijList = dc.idebarJoinU9.ToList();

            foreach (idebarJoinU9 item in ijList)
            {
                if (item.idebarJoinU9_PROD_ID == PROD_ID)
                {
                    return false;
                }
            }
            return true;
        }
        PageControl page = new PageControl();
        FenYe fy = new FenYe();
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Name == "tsbtnADD")//开始质检
            {
                if (tsbtnADD.Enabled)
                {
                    tsbtnADD.Enabled = false;
                    QcInfoAdd();
                    tsbtnADD.Enabled = true;
                }
                return;
            }
            if (e.ClickedItem.Name == "tsbtnDel")//删除登记
            {
                if (tsbtnDel.Enabled)
                {
                    tsbtnDel.Enabled = false;
                    QcInfoDel();
                    tsbtnDel.Enabled = true;

                }
                return;
            }
            if (e.ClickedItem.Name == "tChoosePacks")//现场抽包
            {
                tChoosePacks.Enabled = false;
                ChoosePacks();
                tChoosePacks.Enabled = true;
                return;
            }
            if (e.ClickedItem.Name == "printMenu")//打印模板
            {
                printMenu.Enabled = false;
                if (lvwUserList.SelectedRows.Count > 0)
                {
                    dayin(lvwUserList.SelectedRows[0].Cells["DRAW_EXAM_INTERFACE_ID"].Value.ToString());
                }
                printMenu.Enabled = true;
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
            if (e.ClickedItem.Name == "tsprintB")//打印DTS包号
            {
                count = 0;
                Print();
            }
            // Bangding_Load("");

            if (e.ClickedItem.Name == "toolStrip_ManaualEntryCar")//手动录入登记
            {
                FormManaualEntryCar carinfo = new FormManaualEntryCar();

                carinfo.ShowDialog();
            }
        }

        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tslNotSelect_Click()
        {

            for (int i = 0; i < lvwUserList.Rows.Count; i++)
            {
                lvwUserList.Rows[i].Selected = false;
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tslSelectAll_Click()
        {

            for (int i = 0; i < lvwUserList.Rows.Count; i++)
            {
                lvwUserList.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 打印方法水分空报表
        /// </summary>
        private void dayin(string DRAW_EXAM_INTERFACE_id)
        {
            try
            {
                int id = Convert.ToInt32(DRAW_EXAM_INTERFACE_id);
                Common.intQCInfo_ID = Convert.ToInt32(LinQBaseDao.GetSingle(" select QCInfo_ID from QCInfo where QCInfo_DRAW_EXAM_INTERFACE_ID=" + id).ToString());
                DialogResult di = MessageBox.Show("是否预览再打印？", "系统提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (di.Equals(DialogResult.Yes))
                {
                    Form1 fr = new Form1();
                    fr.Show();
                }
                else if (di.Equals(DialogResult.No))
                {
                    WordPringOut word = new WordPringOut(Common.intQCInfo_ID.ToString(), winWordControl1);
                }

            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 现场抽包
        /// </summary>
        public void ChoosePacks()
        {
            if (lvwUserList.SelectedRows.Count == 1)
            {
                try
                {
                    string Id = lvwUserList.SelectedRows[0].Cells["DRAW_EXAM_INTERFACE_ID"].Value.ToString();//抽样编号
                    lblDRAW_EXAM_INTERFACE_ID.Text = Id;
                    string SQLid = "select IS_DengJi from dbo.DRAW_EXAM_INTERFACE where DRAW_EXAM_INTERFACE_ID=" + Id;
                    if (LinQBaseDao.GetSingle(SQLid).ToString() == "Y")
                    {
                        lblCNTR_NO.Text = lvwUserList.SelectedRows[0].Cells["CNTR_NO"].Value.ToString();//车牌号
                        lblSHIPMENT_NO.Text = lvwUserList.SelectedRows[0].Cells["SHIPMENT_NO"].Value.ToString();//送货单号
                        lblPO_NO.Text = lvwUserList.SelectedRows[0].Cells["PO_NO"].Value.ToString();//采购单号
                        lblPROD_ID.Text = lvwUserList.SelectedRows[0].Cells["PROD_ID"].Value.ToString();//货品
                        lblNO_OF_BALES.Text = lvwUserList.SelectedRows[0].Cells["NO_OF_BALES"].Value.ToString();//总包数
                        string sql = "select Packets_ID,Packets_DTS,Packets_this from Packets where Packets_QCInfo_DRAW_EXAM_INTERFACE_ID=" + Id;
                        DataTable dt = LinQBaseDao.Query(sql).Tables[0];
                        string[] str = null ;
                        //if (dt != null && dt.Rows.Count > 0)
                        //{
                            lblPackets_ID.Text = dt.Rows[0]["Packets_ID"].ToString();//抽包编号
                            lblPackets_DTS.Text = dt.Rows[0]["Packets_DTS"].ToString();//DTS抽包
                             str = dt.Rows[0]["Packets_this"].ToString().Split(',');//现场抽包
                      //  }
                        
                        txtPack1.Text = "";
                        txtPack2.Text = "";
                        txtPack3.Text = "";
                        txtPack4.Text = "";
                        if ( str != null && str.Length > 0)
                        {
                            for (int i = 0; i < str.Length; i++)
                            {
                                foreach (Control item in groupBox1.Controls)
                                {
                                    if (item.Name == "txtPack" + (i + 1))
                                    {
                                        if (str[i] != "")
                                        {
                                            if (item != null)
                                            {
                                                item.Text = str[i];
                                            }
                                        }
                                        else
                                        {
                                            item.Text = "";
                                        }
                                    }
                                }

                            }
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("请先登记该车辆信息再抽包！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("请先登记该车辆信息再抽包！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (lvwUserList.SelectedRows.Count > 1)
            {
                MessageBox.Show("系统每次只能对一辆车进行抽包！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("请先选择车辆信息再抽包！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 删除登记 支持多选
        /// 2014-09-20 杨敦钦
        /// </summary>
        private void QcInfoDel()
        {

            if (lvwUserList.SelectedRows.Count > 0)//选中行
            {

                try
                {

                    if (lvwUserList.SelectedRows.Count > 0)
                    {
                        int id = Convert.ToInt32(lvwUserList.SelectedRows[0].Cells["DRAW_EXAM_INTERFACE_ID"].Value);

                        if (MessageBox.Show("确定要删除质检吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string strMessage = "";
                            string content = "删除质检是修改抽样表编号" + id + "的状态为未登记,注销质检信息QCInfo含有抽样表编号的数据，注销质检流水记录表QCRecord含有抽样表编号的数据。";

                            //修改抽样表状态为未登记
                            string strDelDRAW_EXAM_INTERFACE = "update DRAW_EXAM_INTERFACE set IS_DengJi = NULL   where DRAW_EXAM_INTERFACE_ID = " + id + "";
                            int irnt = LinQBaseDao.ExecuteSql(strDelDRAW_EXAM_INTERFACE);
                            if (irnt > 0)
                            {
                                strMessage += "删除质检成功共" + irnt + "条数据";
                            }
                            //删除QCREcord记录
                            string strDelQcrecord = "delete  QCRecord  where QcRecord_QCInfo_ID in (select distinct QCInfo_ID from  QCInfo where QCInfo_DRAW_EXAM_INTERFACE_ID=" + id + ")";
                            irnt = LinQBaseDao.ExecuteSql(strDelQcrecord);
                            if (irnt > 0)
                            {
                                strMessage += ",删除质检记录（QCREcord）成功共" + irnt + "条数据";
                            }
                            //删除QCInfo记录
                            string strDel = "delete QCInfo  where QCInfo_DRAW_EXAM_INTERFACE_ID=" + id + "";
                            irnt = LinQBaseDao.ExecuteSql(strDel);
                            if (irnt > 0)
                            {
                                strMessage += ",删除质检总记录（QCInfo）成功共" + irnt + "条数据";
                            }
                            irnt = 0;
                            strDel = "delete Packets where Packets_QCInfo_DRAW_EXAM_INTERFACE_ID=" + id;
                            irnt = LinQBaseDao.ExecuteSql(strDel);
                            if (irnt > 0)
                            {
                                strMessage += ",删除质检总记录（Packets）成功共" + irnt + "条数据";
                            }
                            //日志记录
                            if (Common.NAME != "")
                            {
                                Common.WriteLogData("修改", content + strMessage, Common.NAME);
                            }
                            else
                            {
                                Common.WriteLogData("修改", content + strMessage, Common.USERNAME);
                            }

                            ids.Clear();
                            page = new PageControl();
                            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                            Bangding_Load(RegistrationWhere());//更新数据
                            if (!string.IsNullOrEmpty(strMessage))
                            {
                                MessageBox.Show(strMessage, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                    }
                    else//没有选中
                    {
                        MessageBox.Show("请选择列表再删除质检！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    Common.WriteTextLog("删除质检异常：" + ex.Message);
                }
            }
        }

        private void tsprintB_Click(object sender, EventArgs e)
        {

        }
        private PrintDocument printDocument;
        private PageSetupDialog pageSetupDialog;
        private PrintPreviewDialog printPreviewDialog;
        private void Print()
        {
            try
            {
                if (lvwUserList.SelectedRows.Count > 0)
                {
                    printDocument = new PrintDocument();
                    printPreviewDialog = new PrintPreviewDialog();
                    printDocument.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
                    printDocument.Print();
                    printPreviewDialog.Close();
                }
            }
            catch (Exception)
            {

            }
        }
        int count = 0;
        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int y = 0;
                int rowGap = 30;
                int colGap = 30;
                int x = 1;
                Font captionFont = new Font("宋体", 18, FontStyle.Bold);
                Brush brush = new SolidBrush(Color.Black);
                for (int i = 0; i < lvwUserList.SelectedRows.Count; i++)
                {
                    if (count != 0 && i == 0)
                    {
                        i = count + 1;
                    }

                    #region 杨敦钦 填充打印内容 和打印
                    e.Graphics.DrawString("重庆理文造纸有限公司国废质检抽包", captionFont, brush, rowGap, y, StringFormat.GenericDefault);
                    int id = Convert.ToInt32(lvwUserList.SelectedRows[i].Cells["DRAW_EXAM_INTERFACE_ID"].Value);
                    string prtSql = "select CNTR_NO,WEIGHT_TICKET_NO,PROD_ID,DRAW_ONE,DRAW_TWO,DRAW_THREE,DRAW_FOUR,DRAW_FIVE,DRAW_SIX,DRAW_7,DRAW_8,DRAW_9,DRAW_10,DRAW_11,DRAW_12,DRAW_13,DRAW_14 from dbo.DRAW_EXAM_INTERFACE  where  DRAW_EXAM_INTERFACE_ID=" + id;
                    DataTable dtprt = LinQBaseDao.Query(prtSql).Tables[0];
                    if (dtprt.Rows.Count > 0)
                    {
                        y += colGap;//换行
                        string strattent = dtprt.Rows[0]["CNTR_NO"].ToString();
                        e.Graphics.DrawString("车牌号：" + strattent, captionFont, brush, rowGap, y, StringFormat.GenericDefault);//添加打印内容
                        strattent = dtprt.Rows[0]["WEIGHT_TICKET_NO"].ToString();
                        e.Graphics.DrawString("磅单号：" + strattent, captionFont, brush, 280, y, StringFormat.GenericDefault);//添加打印内容
                        strattent = dtprt.Rows[0]["PROD_ID"].ToString();
                        e.Graphics.DrawString("货品：" + strattent, captionFont, brush, 540, y, StringFormat.GenericDefault);//添加打印内容

                        y += colGap;//换行
                        strattent = "";
                        string ziduan = "DRAW_ONE,DRAW_TWO,DRAW_THREE,DRAW_FOUR,DRAW_FIVE,DRAW_SIX,DRAW_7,DRAW_8,DRAW_9,DRAW_10,DRAW_11,DRAW_12,DRAW_13,DRAW_14";
                        string[] danziduan = ziduan.Split(',');
                        for (int j = 0; j < danziduan.Length; j++)
                        {
                            strattent = hbbh(dtprt, strattent, danziduan[j]);
                        }
                        if (strattent != "")
                        {
                            strattent = strattent.Substring(0, strattent.Length - 1);
                        }
                        e.Graphics.DrawString("包号：" + strattent, captionFont, brush, rowGap, y, StringFormat.GenericDefault);//添加打印内容
                        y += 50;
                    }
                    if (x >= 10)
                    {
                        e.HasMorePages = true;//分页
                        x = 1;
                        count = i;
                        if (i == lvwUserList.SelectedRows.Count - 1)
                        {
                            e.HasMorePages = false;//打印
                        }
                        break;
                    }
                    if (i == lvwUserList.SelectedRows.Count - 1)
                    {
                        e.HasMorePages = false;//打印
                    }
                    x++;
                    #endregion
                }

            }
            catch
            {
            }
        }
        /// <summary>
        /// 合并包号
        /// 杨敦钦 合并包好的方法
        /// </summary>
        /// <param name="dtprt"></param>
        /// <param name="strattent"></param>
        /// <returns></returns>
        private static string hbbh(DataTable dtprt, string strattent, string ziduan)
        {
            if (dtprt.Rows[0][ziduan].ToString() != "" && dtprt.Rows[0][ziduan].ToString() != "0")
            {
                strattent = strattent + dtprt.Rows[0][ziduan].ToString() + "，";
            }
            return strattent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Packets_this = "";
                if (txtPack1.Text.Trim() != "")
                {
                    Packets_this += txtPack1.Text.Trim() + ",";
                }
                if (txtPack2.Text.Trim() != "")
                {
                    Packets_this += txtPack2.Text.Trim() + ",";
                }
                if (txtPack3.Text.Trim() != "")
                {
                    Packets_this += txtPack3.Text.Trim() + ",";
                }
                if (txtPack4.Text.Trim() != "")
                {
                    Packets_this += txtPack4.Text.Trim() + ",";
                }
                Packets_this = Packets_this.TrimEnd(',');

                string[] Packets_thisList = Packets_this.Split(',');
                string[] dtsList = lblPackets_DTS.Text.Split(',');

                foreach (string item in Packets_thisList)
                {
                    foreach (string it in dtsList)
                    {
                        if (it == item)
                        {
                            MessageBox.Show("抽包号码“" + it + "”与DTS重复！");
                            return;
                        }
                    }
                }
                DataTable dtq = LinQBaseDao.Query("select QCInfo_ID from QCInfo where QCInfo_DRAW_EXAM_INTERFACE_ID=" + lblDRAW_EXAM_INTERFACE_ID.Text + " order by QCInfo_ID desc ").Tables[0];
                if (dtq.Rows.Count > 0)
                {
                    int moistcount = Convert.ToInt32(LinQBaseDao.GetSingle("select COUNT(0) from QCRecord where QCRecord_QCInfo_ID =" + dtq.Rows[0][0].ToString() + " and QCRecord_TestItems_ID in (1,3,4)"));
                    if (moistcount > 0)
                    {
                        MessageBox.Show(this, "已开始测水，不能再抽包");
                        return;
                    }
                }

                if (lblPackets_ID.Text.Trim() != "")
                {
                    Expression<Func<Packets, bool>> fnP = n => n.Packets_ID == Convert.ToInt32(lblPackets_ID.Text.Trim());

                    Action<Packets> a = n =>
                    {
                        n.Packets_this = Packets_this;
                    };

                    if (PacketsDAL.Update(fnP, a))
                    {
                        //抽包后修改qcinfo表 抽包总数和总抽包号
                        string bags = lblPackets_DTS.Text + "," + Packets_this;
                        if (dtq.Rows.Count > 0)
                        {
                            Common.GetSumWaterCount(Convert.ToInt32(dtq.Rows[0][0].ToString()));
                            LinQBaseDao.Query("update QCInfo set QCInfo_DRAW='" + bags + "',QCInfo_PumpingPackets=" + bags.Split(',').Count() + ",QCInfo_MOIST_Count=" + Common.SumWaterCount + "  where QCInfo_DRAW_EXAM_INTERFACE_ID=" + lblDRAW_EXAM_INTERFACE_ID.Text);
                        }
                        else
                        {
                            LinQBaseDao.Query("update QCInfo set QCInfo_DRAW='" + bags + "',QCInfo_PumpingPackets=" + bags.Split(',').Count() + " where QCInfo_DRAW_EXAM_INTERFACE_ID=" + lblDRAW_EXAM_INTERFACE_ID.Text);
                        }
                        DialogResult dia = MessageBox.Show("操作成功！是否打印空的水分报表？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if (dia.Equals(DialogResult.Yes))
                        {
                            dayin(lblDRAW_EXAM_INTERFACE_ID.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }

                    Bangding_Load(RegistrationWhere());

                }

            }
            catch(Exception err)
            {


            }


        }

        private void btnSeacher_Click(object sender, EventArgs e)
        {
            Bangding_Load(RegistrationWhere());
        }

        private void tslHomPage1_Click(object sender, EventArgs e)
        {
            txtCurrentPage2.Text = "1";
            Bangding_Load(RegistrationWhere());
        }

        private void tslLastPage1_Click(object sender, EventArgs e)
        {
            txtCurrentPage2.Text = zongyeshu.ToString();
            Bangding_Load(RegistrationWhere());
        }

        private void tslPreviousPage1_Click(object sender, EventArgs e)
        {
            if (txtCurrentPage2.Text.Trim() != "")
            {
                int numye = int.Parse(txtCurrentPage2.Text.Trim());
                if (numye > 1)
                {
                    txtCurrentPage2.Text = (numye - 1).ToString();
                }
                else
                {
                    MessageBox.Show("已经是首页", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                txtCurrentPage2.Text = "1";
            }
            Bangding_Load(RegistrationWhere());
        }

        private void tslNextPage1_Click(object sender, EventArgs e)
        {
            int yeshu = int.Parse(txtCurrentPage2.Text);
            if (yeshu < zongyeshu)
            {
                txtCurrentPage2.Text = (yeshu + 1).ToString();
            }
            else
            {
                MessageBox.Show("这已经最后页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Bangding_Load(RegistrationWhere());
        }

        private void tscbxPageSize2_TextChanged(object sender, EventArgs e)
        {
            Bangding_Load(RegistrationWhere());
        }

        private void txtPack1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                btnSave_Click(null, null);
            }
        }

        private void txt_CNTR_NO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                Bangding_Load(RegistrationWhere());
            }
        }

    }
}
