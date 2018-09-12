using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using EMEWEDAL;
using EMEWEEntity;
using EMEWEQUALITY.HelpClass;
using EMEWEQUALITY.GetPhoto;
using System.Threading;

namespace EMEWEQUALITY.LED
{
    public partial class LEDSHOWB : Form
    {
        /// <summary>
        /// 储存显示过的ID
        /// </summary>
        private static string ID1;
        private static string ID2;
        private static string ID3;

        public LEDSHOWB()
        {
            InitializeComponent();
            //Setfont();
        }
        /// <summary>
        /// 取完成质检的时间段变量
        /// </summary>
        int endTime = 5;
        //加载方法
        private void LEDSHOW_Load(object sender, EventArgs e)
        {
            FormLocation();
            this.Location = new Point(Common.LEDFROMX, Common.LEDFROMY);
            if (Common.LEDTabTime.Trim() != "")
            {
                timer1.Interval = Convert.ToInt32(Common.LEDTabTime);
                timer2.Interval = Convert.ToInt32(Common.LEDTabTime);
            }

            if (Common.LEDEndTime.Trim() != "")
            {
                endTime = 0 - Convert.ToInt32(Common.LEDEndTime);
            }
            timer1.Start();
            timer2.Start();
        }


        //设置LED显示的位置
        private void FormLocation()
        {
            try
            {
                Screen[] screens = Screen.AllScreens;
                Screen screen = screens[0];
                //this.Location = new Point(Common.LEDFROMX, Common.LEDFROMY);
                //this.Location = new Point(150, 150);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog(ex.Message);
            }
        }
        int num = 0;//取数据行编号
        int sy = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //查询质检中的数据并且已检测水分或正在检测水分的数据
                DataTable QcInfoDT = LinQBaseDao.Query(" SELECT  top(100)* from  QCInfo,DRAW_EXAM_INTERFACE  where   QCInfo_DRAW_EXAM_INTERFACE_ID=DRAW_EXAM_INTERFACE_ID  and     QCInfo_ID in (select QCRecord_QCInfo_ID from  QCRecord,TestItems where QCRecord_TestItems_ID = 1 ) and QCInfo_Dictionary_ID =8   order by QCInfo_ID desc ").Tables[0];

                string qcids = "";
                string strsql = "";
                if (QcInfoDT.Rows.Count > 0)
                {
                    for (int s = 0; s < QcInfoDT.Rows.Count; s++)
                    {
                        //筛选出正在检测水分的数据
                        int mcount = Convert.ToInt32(LinQBaseDao.GetSingle(" select COUNT(0) from QCRecord where QCRecord_QCInfo_ID=" + QcInfoDT.Rows[s]["QCInfo_ID"].ToString() + " and QCRecord_TestItems_ID=1 and QCRecord_RESULT  is  null").ToString());
                        if (!string.IsNullOrEmpty(QcInfoDT.Rows[s]["QCInfo_MOIST_Count"].ToString()))
                        {
                            if (mcount != 0)
                            {
                                if (mcount < Convert.ToInt32(QcInfoDT.Rows[s]["QCInfo_MOIST_Count"].ToString()))
                                {
                                    if (QcInfoDT.Rows[s]["QCInfo_Dictionary_ID"].ToString() != "19")
                                    {
                                        qcids += QcInfoDT.Rows[s]["QCInfo_ID"].ToString() + ",";
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(qcids))
                    {
                        strsql = " SELECT top(100) * from  QCInfo,DRAW_EXAM_INTERFACE  where   QCInfo_DRAW_EXAM_INTERFACE_ID=DRAW_EXAM_INTERFACE_ID and     QCInfo_ID in (" + qcids.TrimEnd(',') + ") order by QCInfo_ID desc";
                        QcInfoDT = LinQBaseDao.Query(strsql).Tables[0];
                    }
                    else
                    {
                        if (sy > 5)
                        {
                            sy = 0;
                            lblCarNoA.Text = "";
                            lbltypeA.Text = "";
                            lblWaterA.Text = "";
                            lblAVGA.Text = "";
                            lblOA1.Text = "";
                            lblOA2.Text = "";
                            lblOA3.Text = "";
                            lblOA4.Text = "";

                            lblCarNOB.Text = "";
                            lblTypeB.Text = "";
                            lblWaterB.Text = "";
                            lblAVGB.Text = "";
                            lblOB1.Text = "";
                            lblOB2.Text = "";
                            lblOB3.Text = "";
                            lblOB4.Text = "";
                        }
                        sy++;

                        return;
                    }

                    if (QcInfoDT.Rows.Count == 1)
                    {
                        lblCarNOB.Text = "";
                        lblTypeB.Text = "";
                        lblWaterB.Text = "";
                        lblAVGB.Text = "";
                        lblOB1.Text = "";
                        lblOB2.Text = "";
                        lblOB3.Text = "";
                        lblOB4.Text = "";
                    }

                    #region
                    //循环质检信息 每次2条信息
                    for (int i = 0; i < 2; i++)
                    {
                        if (num + i >= QcInfoDT.Rows.Count)
                        {
                            num = 0;
                            return;
                        }
                        int QCInfo_ID = Convert.ToInt32(QcInfoDT.Rows[num + i]["QCInfo_ID"]);//质检编号
                        string CNTR_NO = QcInfoDT.Rows[num + i]["CNTR_NO"].ToString();//车牌
                        string PROD_ID = QcInfoDT.Rows[num + i]["PROD_ID"].ToString();//纸种
                        if (PROD_ID.Length >= 8)
                        {
                            PROD_ID = PROD_ID.Substring(0, 8);
                        }
                        string BWeight = QcInfoDT.Rows[num + i]["QCInfo_BAGWeight"].ToString();//总包重
                        string PWeight = QcInfoDT.Rows[num + i]["QCInfo_PAPER_SCALE"].ToString();//杂纸比例
                        // string PWeight = QcInfoDT.Rows[num + i]["QCInfo_PAPER_WEIGHT"].ToString() ;//杂纸
                        string MWeight = QcInfoDT.Rows[num + i]["QCInfo_MATERIAL_SCALE"].ToString();//杂质比例
                        //string MWeight = QcInfoDT.Rows[num + i]["QCInfo_MATERIAL_WEIGHT"].ToString() ;//杂质
                        if (!string.IsNullOrEmpty(BWeight))
                        {
                            BWeight = Convert.ToInt32(Convert.ToDouble(BWeight)).ToString();
                        }
                        if (!string.IsNullOrEmpty(PWeight))
                        {
                            PWeight = Convert.ToDouble(PWeight).ToString();
                        }
                        if (!string.IsNullOrEmpty(MWeight))
                        {
                            MWeight = Convert.ToDouble(MWeight).ToString();
                        }
                        string avgWate = "";
                        if (QcInfoDT.Rows[num + i]["QCInfo_MOIST_PER_SAMPLE"].ToString() != "")
                        {
                            //总平均水分
                            avgWate = QcInfoDT.Rows[num + i]["QCInfo_MOIST_PER_SAMPLE"].ToString();//平均水分
                        }
                        else
                        {
                            // 拆包后平均水分
                            if (QcInfoDT.Rows[num + i]["QCInfo_UnpackBack_MOIST_PER_SAMPLE"].ToString() != "")
                            {
                                avgWate = QcInfoDT.Rows[num + i]["QCInfo_UnpackBack_MOIST_PER_SAMPLE"].ToString();
                            }
                            else
                            {
                                //拆包前
                                if (QcInfoDT.Rows[num + i]["QCInfo_UnpackBefore_MOIST_PER_SAMPLE"].ToString() != "")
                                {
                                    avgWate = QcInfoDT.Rows[num + i]["QCInfo_UnpackBefore_MOIST_PER_SAMPLE"].ToString();
                                }
                            }
                        }
                        string wate = "";//实时水分
                        string state = "";//状态


                        //根据质检编号查询QCRecord_RESULT > 0 的质检详细信息 排倒序
                        DataTable QCRecordDT = LinQBaseDao.Query("select top(2)* from QCRecord,TestItems where QCRecord_TestItems_ID = TestItems_ID and QCRecord_QCInfo_ID=" + QCInfo_ID + " order by QCRecord_ID desc").Tables[0];
                        if (QCRecordDT.Rows.Count > 0)
                        {

                            string testItem = QCRecordDT.Rows[0]["TestItems_NAME"].ToString();//检测项目名称

                            //根据质检编号查询QCRecord_RESULT > 0 的2条水分质检详细信息 排倒序
                            DataTable wateDT = LinQBaseDao.Query("select top(2)* from QCRecord,TestItems where QCRecord_TestItems_ID = TestItems_ID and QCRecord_RESULT > 0 and QCRecord_QCInfo_ID=" + QCInfo_ID + " and Tes_TestItems_ID=1 order by QCRecord_ID desc").Tables[0];

                            //赋值实时水分，这里有2条就显示2条，否则显示一条
                            if (wateDT.Rows.Count > 1)
                            {
                                wate = wateDT.Rows[0]["QCRecord_RESULT"].ToString();

                            }
                            else if (wateDT.Rows.Count == 1)
                            {
                                wate = wateDT.Rows[0]["QCRecord_RESULT"].ToString();
                            }

                            if (testItem == "水分检测" || testItem == "拆包后水分检测" || testItem == "拆包前水分检测")
                            {
                                //状态为测水判断
                                state = "测水";
                            }

                            //状态为分拣判断
                            if (testItem == "重量检测" || testItem == "废纸包重" || testItem == "杂质" || testItem == "杂纸")
                            {
                                state = "分拣";
                            }

                            //状态为退货判断
                            if (testItem == "退货还包" || QcInfoDT.Rows[num + i]["QCInfo_RECV_RMA_METHOD"].ToString() == "退货")
                            {
                                state = "退货";
                            }

                            if (QcInfoDT.Rows[num + i]["QCInfo_RECV_RMA_METHOD"].ToString() == "收货")
                            {
                                state = "收货";
                            }
                        }
                        //if (CEQCINFO_ID==1)
                        //{
                        //   i=i + 1;
                        //}
                        switch (i)
                        {
                            //第一行
                            case 0:
                                if (lblCarNoA.Text != CNTR_NO)
                                {
                                    lblCarNoA.Text = CNTR_NO;
                                }
                                if (lbltypeA.Text != PROD_ID)
                                {
                                    lbltypeA.Text = PROD_ID;
                                }
                                if (lblWaterA.Text != wate)
                                {
                                    lblWaterA.Text = wate;
                                }
                                if (lblAVGA.Text != avgWate)
                                {
                                    lblAVGA.Text = avgWate;
                                }
                                lblOA1.Text = BWeight;
                                lblOA2.Text = PWeight;
                                lblOA3.Text = MWeight;
                                lblOA4.Text = state;
                                break;


                            //第二行
                            case 1:
                                if (lblCarNOB.Text != CNTR_NO)
                                {
                                    lblCarNOB.Text = CNTR_NO;
                                }
                                if (lblTypeB.Text != PROD_ID)
                                {
                                    lblTypeB.Text = PROD_ID;
                                }

                                if (lblWaterB.Text != wate)
                                {
                                    lblWaterB.Text = wate;
                                }
                                if (lblAVGB.Text != avgWate)
                                {
                                    lblAVGB.Text = avgWate;
                                }
                                lblOB1.Text = BWeight;
                                lblOB2.Text = PWeight;
                                lblOB3.Text = MWeight;
                                lblOB4.Text = state;
                                break;
                            default:
                                break;
                        }
                    }
                    #endregion
                    num += 2;
                }
            }
            catch
            {


            }


        }

        //获取显示的字体设置信息
        public void Setfont()
        {
            try
            {
                string AppPath = Application.StartupPath;
                try
                {
                    PrivateFontCollection font = new PrivateFontCollection();
                    font.AddFontFile(AppPath + @"\font\msyh.ttf");
                    Font myFont = new Font(font.Families[0].Name, 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                }
                catch
                {
                    MessageBox.Show("字体不存在或加载失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog(ex.Message);
            }
        }

        private void labelI_Paint(object sender, PaintEventArgs e)
        {



        }

        private void LEDSHOW_Paint(object sender, PaintEventArgs e)
        {
            //Color c = Color.FromName("RED");
            //Pen Pen1 = new Pen(c, 5);//5是线的粗细，随便调
            //Pen Pen2 = new Pen(c, 1);
            //Graphics g = e.Graphics;
            ////边框
            //g.DrawLine(Pen1, 2, 2, 458, 2);//边框顶上横线(第一、二个数字是开始画的坐标，后面是结束的坐标)
            //g.DrawLine(Pen1, 2, 194, 458, 194);//边框下边横
            //g.DrawLine(Pen1, 2, 2, 2, 194);//边框左边竖线
            //g.DrawLine(Pen1, 458, 2, 458, 194);//边框右边竖线
            ////内部线
            ////竖线
            //g.DrawLine(Pen2, 74, 0, 74, 196);
            //g.DrawLine(Pen2, 172, 0, 172, 196);
            //g.DrawLine(Pen2, 265, 0, 265, 196);
            //g.DrawLine(Pen2, 374, 0, 374, 196);
            //g.DrawLine(Pen2, 219, 27, 219, 196);
            //g.DrawLine(Pen2, 294, 27, 294, 196);
            //g.DrawLine(Pen2, 322, 27, 322, 196);
            //g.DrawLine(Pen2, 349, 27, 349, 196);
            ////横线
            //g.DrawLine(Pen2, 172, 26, 269, 26);
            //g.DrawLine(Pen2, 265, 26, 374, 26);
            //g.DrawLine(Pen2, 0, 60, 460, 60);
            //g.DrawLine(Pen2, 0, 106, 460, 106);
            //g.DrawLine(Pen2, 0, 152, 460, 152);
        }
        int num1 = 0;
        int CEQCINFO_ID = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                string qcids = "";
                string qcid = "";
                //查询所有质检中的数据+完成质检并且规定时间以内的
                DataTable QcInfoDT = LinQBaseDao.Query(" SELECT  top(100)* from  QCInfo,DRAW_EXAM_INTERFACE  where   QCInfo_DRAW_EXAM_INTERFACE_ID=DRAW_EXAM_INTERFACE_ID  and (QCInfo_Dictionary_ID in (7,8) or (QCIInfo_EndTime >= '" + DateTime.Now.AddMinutes(endTime) + "' and QCInfo_Dictionary_ID=19))   order by QCInfo_ID desc ").Tables[0];
                if (QcInfoDT.Rows.Count > 0)
                {
                    for (int s = 0; s < QcInfoDT.Rows.Count; s++)
                    {
                        //筛选出正在检测水分的数据
                        int qcrcount = Convert.ToInt32(LinQBaseDao.GetSingle(" select COUNT(0) from QCRecord where QCRecord_QCInfo_ID=" + QcInfoDT.Rows[s]["QCInfo_ID"].ToString()).ToString());
                        if (qcrcount <= 0)
                        {
                            qcids += QcInfoDT.Rows[s]["QCInfo_ID"].ToString() + ",";
                        }
                        int mcount = Convert.ToInt32(LinQBaseDao.GetSingle(" select COUNT(0) from QCRecord where QCRecord_QCInfo_ID=" + QcInfoDT.Rows[s]["QCInfo_ID"].ToString() + " and QCRecord_TestItems_ID=1 and QCRecord_RESULT  is  null").ToString());
                        if (!string.IsNullOrEmpty(QcInfoDT.Rows[s]["QCInfo_MOIST_Count"].ToString()))
                        {
                            if (mcount != 0)
                            {
                                if (mcount < Convert.ToInt32(QcInfoDT.Rows[s]["QCInfo_MOIST_Count"].ToString()))
                                {
                                    if (QcInfoDT.Rows[s]["QCInfo_Dictionary_ID"].ToString() != "19")
                                    {
                                        qcids += QcInfoDT.Rows[s]["QCInfo_ID"].ToString() + ",";
                                    }
                                }
                            }
                            else
                            {
                                object obj = LinQBaseDao.GetSingle("select QCRecord_TIME from QCRecord where QCRecord_QCInfo_ID =" + QcInfoDT.Rows[s]["QCInfo_ID"].ToString() + " and QCRecord_TestItems_ID in (1,3,4) order by QCRecord_QCCOUNT desc");
                                if (obj != null)
                                {
                                    DateTime dtqcrtime = Convert.ToDateTime(obj.ToString());
                                    TimeSpan tspan = Common.GetServersTime() - dtqcrtime;
                                    if (tspan.TotalSeconds < 30)
                                    {
                                        qcid += QcInfoDT.Rows[s]["QCInfo_ID"].ToString() + ",";

                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(qcid))
                    {
                        //完成水分检测30秒以内的数据
                        string strsql = " SELECT top(100) * from  QCInfo,DRAW_EXAM_INTERFACE  where   QCInfo_DRAW_EXAM_INTERFACE_ID=DRAW_EXAM_INTERFACE_ID  and     QCInfo_ID  in (" + qcid.TrimEnd(',') + ")  order by QCInfo_ID desc ";
                        QcInfoDT = LinQBaseDao.Query(strsql).Tables[0];
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(qcids))
                        {
                            string strsql = " SELECT  top(100)* from  QCInfo,DRAW_EXAM_INTERFACE  where   QCInfo_DRAW_EXAM_INTERFACE_ID=DRAW_EXAM_INTERFACE_ID  and     QCInfo_ID not in (" + qcids.TrimEnd(',') + ")  and (QCInfo_Dictionary_ID in (7,8) or (QCIInfo_EndTime >= '" + DateTime.Now.AddMinutes(endTime) + "' and QCInfo_Dictionary_ID=19))   order by QCInfo_ID desc ";
                            QcInfoDT = LinQBaseDao.Query(strsql).Tables[0];
                        }
                    }
                    if (QcInfoDT.Rows.Count == 1)
                    {
                        lblCarNoD.Text = "";
                        lblTypeD.Text = "";
                        lblWaterD.Text = "";
                        lblAVGD.Text = "";
                        lblOD1.Text = "";
                        lblOD2.Text = "";
                        lblOD3.Text = "";
                        lblOD4.Text = "";
                    }

                    #region
                    //循环质检信息 每次2条信息
                    for (int i = 0; i < 2; i++)
                    {
                        if (num1 + i >= QcInfoDT.Rows.Count)
                        {
                            num1 = 0;
                            return;
                        }
                        int QCInfo_ID = Convert.ToInt32(QcInfoDT.Rows[num1 + i]["QCInfo_ID"]);//质检编号
                        string CNTR_NO = QcInfoDT.Rows[num1 + i]["CNTR_NO"].ToString();//车牌
                        string PROD_ID = QcInfoDT.Rows[num1 + i]["PROD_ID"].ToString();//纸种

                        if (PROD_ID.Length >= 8)
                        {
                            PROD_ID = PROD_ID.Substring(0, 8);
                        }
                        string BWeight = QcInfoDT.Rows[num1 + i]["QCInfo_BAGWeight"].ToString();//总包重
                        string PWeight = QcInfoDT.Rows[num1 + i]["QCInfo_PAPER_SCALE"].ToString();//杂纸比例
                        // string PWeight = QcInfoDT.Rows[num1 + i]["QCInfo_PAPER_WEIGHT"].ToString() ;//杂纸
                        string MWeight = QcInfoDT.Rows[num1 + i]["QCInfo_MATERIAL_SCALE"].ToString();//杂质比例
                        //string MWeight = QcInfoDT.Rows[num1 + i]["QCInfo_MATERIAL_WEIGHT"].ToString() ;//杂质
                        if (!string.IsNullOrEmpty(BWeight))
                        {
                            BWeight = Convert.ToInt32(Convert.ToDouble(BWeight)).ToString();
                        }
                        if (!string.IsNullOrEmpty(PWeight))
                        {
                            PWeight = Convert.ToDouble(PWeight).ToString();
                        }
                        if (!string.IsNullOrEmpty(MWeight))
                        {
                            MWeight = Convert.ToDouble(MWeight).ToString();
                        }
                        string avgWate = "";
                        if (QcInfoDT.Rows[num1 + i]["QCInfo_MOIST_PER_SAMPLE"].ToString() != "")
                        {
                            //总平均水分
                            avgWate = QcInfoDT.Rows[num1 + i]["QCInfo_MOIST_PER_SAMPLE"].ToString();//平均水分
                        }
                        else
                        {
                            // 拆包后平均水分
                            if (QcInfoDT.Rows[num1 + i]["QCInfo_UnpackBack_MOIST_PER_SAMPLE"].ToString() != "")
                            {
                                avgWate = QcInfoDT.Rows[num1 + i]["QCInfo_UnpackBack_MOIST_PER_SAMPLE"].ToString();
                            }
                            else
                            {
                                //拆包前
                                if (QcInfoDT.Rows[num1 + i]["QCInfo_UnpackBefore_MOIST_PER_SAMPLE"].ToString() != "")
                                {
                                    avgWate = QcInfoDT.Rows[num1 + i]["QCInfo_UnpackBefore_MOIST_PER_SAMPLE"].ToString();
                                }

                            }
                        }
                        string wate = "";//实时水分
                        string state = "";//状态

                        //根据质检编号查询QCRecord_RESULT > 0 的质检详细信息 排倒序
                        DataTable QCRecordDT = LinQBaseDao.Query("select top(2)* from QCRecord,TestItems where QCRecord_TestItems_ID = TestItems_ID and QCRecord_QCInfo_ID=" + QCInfo_ID + " order by QCRecord_ID desc").Tables[0];
                        if (QCRecordDT.Rows.Count > 0)
                        {

                            string testItem = QCRecordDT.Rows[0]["TestItems_NAME"].ToString();//检测项目名称

                            //根据质检编号查询QCRecord_RESULT > 0 的2条水分质检详细信息 排倒序
                            DataTable wateDT = LinQBaseDao.Query("select top(2)* from QCRecord,TestItems where QCRecord_TestItems_ID = TestItems_ID and QCRecord_RESULT > 0 and QCRecord_QCInfo_ID=" + QCInfo_ID + " and Tes_TestItems_ID=1 order by QCRecord_ID desc").Tables[0];

                            //赋值实时水分，这里有2条就显示2条，否则显示一条
                            if (wateDT.Rows.Count > 1)
                            {
                                string testItemB = wateDT.Rows[1]["TestItems_NAME"].ToString();

                                wate = wateDT.Rows[0]["QCRecord_RESULT"].ToString();

                            }
                            else if (wateDT.Rows.Count == 1)
                            {
                                wate = wateDT.Rows[0]["QCRecord_RESULT"].ToString();
                            }

                            if (testItem == "水分检测" || testItem == "拆包后水分检测" || testItem == "拆包前水分检测")
                            {
                                //状态为测水判断
                                state = "测水";
                            }

                            //状态为分拣判断
                            if (testItem == "重量检测" || testItem == "废纸包重" || testItem == "杂质" || testItem == "杂纸")
                            {
                                state = "分拣";
                            }

                            //状态为退货判断
                            if (testItem == "退货还包" || QcInfoDT.Rows[num1 + i]["QCInfo_RECV_RMA_METHOD"].ToString() == "退货")
                            {
                                state = "退货";
                            }

                            if (QcInfoDT.Rows[num1 + i]["QCInfo_RECV_RMA_METHOD"].ToString() == "收货")
                            {
                                state = "收货";
                            }
                            if (string.IsNullOrEmpty(BWeight))
                            {
                                object objq = LinQBaseDao.GetSingle("select SUM(QCRecord_RESULT) from QCRecord where QCRecord_QCInfo_ID=" + QCInfo_ID + " and QCRecord_TestItems_ID=5");
                                if (objq != null)
                                {
                                    BWeight = Convert.ToInt32(objq).ToString();
                                }
                            }

                            if (state == "测水")
                            {
                                if (string.IsNullOrEmpty(wate))
                                {
                                    if (BWeight == "" && PWeight == "" && MWeight == "")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        switch (i)
                        {
                            //第三行
                            case 0:
                                if (lblCarNoC.Text != CNTR_NO)
                                {
                                    lblCarNoC.Text = CNTR_NO;
                                }
                                if (lblTypeC.Text != PROD_ID)
                                {
                                    lblTypeC.Text = PROD_ID;
                                }

                                if (lblWaterC.Text != wate)
                                {
                                    lblWaterC.Text = wate;
                                }
                                if (lblAVGC.Text != avgWate)
                                {
                                    lblAVGC.Text = avgWate;
                                }

                                lblOC1.Text = BWeight;
                                lblOC2.Text = PWeight;
                                lblOC3.Text = MWeight;
                                lblOC4.Text = state;
                                break;
                            case 1:
                                if (lblCarNoD.Text != CNTR_NO)
                                {
                                    lblCarNoD.Text = CNTR_NO;
                                }
                                if (lblTypeD.Text != PROD_ID)
                                {
                                    lblTypeD.Text = PROD_ID;
                                }

                                if (lblWaterD.Text != wate)
                                {
                                    lblWaterD.Text = wate;
                                }
                                if (lblAVGD.Text != avgWate)
                                {
                                    lblAVGD.Text = avgWate;
                                }

                                lblOD1.Text = BWeight;
                                lblOD2.Text = PWeight;
                                lblOD3.Text = MWeight;
                                lblOD4.Text = state;
                                break;
                            default:
                                break;
                        }

                    }
                    #endregion

                    num1 += 2;

                    if (!string.IsNullOrEmpty(qcid))
                    {
                        Thread.Sleep(30000);
                    }
                }
            }
            catch
            {


            }
        }
    }
}
