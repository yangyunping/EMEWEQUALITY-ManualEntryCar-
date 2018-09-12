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

namespace EMEWEQUALITY.LED
{
    public partial class LEDSHOW : Form
    {
        /// <summary>
        /// 储存显示过的ID
        /// </summary>
        private static string ID1;
        private static string ID2;
        private static string ID3;

        public LEDSHOW()
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
                timer1.Interval=Convert.ToInt32(Common.LEDTabTime);
            }

            if (Common.LEDEndTime.Trim()!="")
            {
               endTime= 0 - Convert.ToInt32(Common.LEDEndTime);
            }
            timer1.Start();
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
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //查询所有质检中的数据+完成质检并且规定时间以内的
                DataTable QcInfoDT = LinQBaseDao.Query(" SELECT  * from  QCInfo,DRAW_EXAM_INTERFACE  where   QCInfo_DRAW_EXAM_INTERFACE_ID=DRAW_EXAM_INTERFACE_ID  and     QCInfo_ID in (select QCRecord_QCInfo_ID from  QCRecord,TestItems where QCRecord_TestItems_ID = TestItems_ID and QCRecord_RESULT > 0) and (QCInfo_Dictionary_ID in (7,8) or (QCIInfo_EndTime >= '" + DateTime.Now.AddMinutes(endTime) + "' and QCInfo_Dictionary_ID=19))   order by QCInfo_ID desc ").Tables[0];
                if (QcInfoDT.Rows.Count > 0)
                {
                    
                    //循环质检信息 每次3条信息
                    for (int i = 0; i < 3; i++)
                    {
                        if (num+i >= QcInfoDT.Rows.Count)
                        {
                            num = 0;
                        }

                        int QCInfo_ID = Convert.ToInt32(QcInfoDT.Rows[num+i]["QCInfo_ID"]);//质检编号
                        string CNTR_NO = QcInfoDT.Rows[num+i]["CNTR_NO"].ToString();//车牌
                        string PROD_ID = QcInfoDT.Rows[num+i]["PROD_ID"].ToString();//纸种
                        string avgWate = "";
                        if (QcInfoDT.Rows[num+i]["QCInfo_MOIST_PER_SAMPLE"].ToString() != "")
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
                        DataTable QCRecordDT = LinQBaseDao.Query("select top(2)* from QCRecord,TestItems where QCRecord_TestItems_ID = TestItems_ID and QCRecord_RESULT > 0 and QCRecord_QCInfo_ID=" + QCInfo_ID + " order by QCRecord_ID desc").Tables[0];
                        if (QCRecordDT.Rows.Count > 0)
                        {

                            string testItem = QCRecordDT.Rows[0]["TestItems_NAME"].ToString();//检测项目名称


                            //根据质检编号查询QCRecord_RESULT > 0 的2条水分质检详细信息 排倒序
                            DataTable wateDT = LinQBaseDao.Query("select top(2)* from QCRecord,TestItems where QCRecord_TestItems_ID = TestItems_ID and QCRecord_RESULT > 0 and QCRecord_QCInfo_ID=" + QCInfo_ID + " and Tes_TestItems_ID=1 order by QCRecord_ID desc").Tables[0];

                            //赋值实时水分，这里有2条就显示2条，否则显示一条
                            if (wateDT.Rows.Count > 1)
                            {
                                string testItemB = wateDT.Rows[1]["TestItems_NAME"].ToString();

                                wate = wateDT.Rows[1]["QCRecord_RESULT"].ToString() + "," + wateDT.Rows[0]["QCRecord_RESULT"].ToString();

                            }
                            else if(wateDT.Rows.Count == 1)
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
                            if (testItem == "退货还包" || QcInfoDT.Rows[num+i]["QCInfo_RECV_RMA_METHOD"].ToString() == "退货")
                            {
                                state = "退货";
                            }
                            
                            if (QcInfoDT.Rows[num+i]["QCInfo_RECV_RMA_METHOD"].ToString() == "收货")
                            {
                                state = "收货";
                            }




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

                                   
                                    if (state == "测水")
                                    {
                                        lblOA1.Text = "○";
                                        lblOA2.Text = "";
                                        lblOA3.Text = "";
                                        lblOA4.Text = "";
                                    }
                                    if (state == "分拣")
                                    {
                                        lblOA2.Text = "○";
                                        lblOA1.Text = "";
                                        lblOA3.Text = "";
                                        lblOA4.Text = "";
                                    } if (state == "收货")
                                    {
                                        lblOA3.Text = "○";
                                        lblOA1.Text = "";
                                        lblOA2.Text = "";
                                        lblOA4.Text = "";
                                    } if (state == "退货")
                                    {
                                        lblOA4.Text = "○";
                                        lblOA1.Text = "";
                                        lblOA2.Text = "";
                                        lblOA3.Text = "";
                                        
                                    }

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


                                    if (state == "测水")
                                    {
                                        lblOB1.Text = "○";
                                        lblOB2.Text = "";
                                        lblOB3.Text = "";
                                        lblOB4.Text = "";
                                    }
                                    if (state == "分拣")
                                    {
                                        lblOB2.Text = "○";

                                        lblOB1.Text = "";
                                        lblOB3.Text = "";
                                        lblOB4.Text = "";
                                    } if (state == "收货")
                                    {
                                        lblOB3.Text = "○";
                                        lblOB1.Text = "";
                                        lblOB2.Text = "";
                                        lblOB4.Text = "";
                                    } if (state == "退货")
                                    {
                                        lblOB4.Text = "○";
                                        lblOB1.Text = "";
                                        lblOB2.Text = "";
                                        lblOB3.Text = "";

                                    }

                                    break;
                                //第三行
                                case 2:
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


                                    if (state == "测水")
                                    {
                                        lblOC1.Text = "○";
                                        lblOC2.Text = "";
                                        lblOC3.Text = "";
                                        lblOC4.Text = "";
                                    }
                                    if (state == "分拣")
                                    {
                                        lblOC2.Text = "○";
                                        lblOC1.Text = "";
                                        lblOC3.Text = "";
                                        lblOC4.Text = "";
                                    } if (state == "收货")
                                    {
                                        lblOC3.Text = "○";
                                        lblOC1.Text = "";
                                        lblOC2.Text = "";
                                        lblOC4.Text = "";
                                    } if (state == "退货")
                                    {
                                        lblOC4.Text = "○";
                                        lblOC1.Text = "";
                                        lblOC2.Text = "";
                                        lblOC3.Text = "";

                                    }

                                    break;
                                default:
                                    break;
                            }

                        }

                    }

                    num += 3;
                  
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
    }
}
