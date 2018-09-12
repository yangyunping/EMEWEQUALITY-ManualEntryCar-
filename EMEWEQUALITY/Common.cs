﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using EMEWE.Common;
using EMEWEQUALITY.MyControl;
using EMEWEEntity;
using EMEWEDAL;
using System.Collections;
using System.Configuration;
using MoistureDetectionRules;

namespace EMEWEQUALITY
{
    public class Common
    {
        /// <summary>
        /// 抽包号数组
        /// </summary>
        public static string[] testBags = null;

        public static string version = "V 7.1.1.1";

        /// <summary>
        /// 每包水分点数
        /// </summary>
        public static int testBagWaterCount = 0;
        /// <summary>
        /// 总水分点数
        /// </summary>
        public static int SumWaterCount = 0;
        /// <summary>
        /// 根据质检编号获取总水分点数
        /// </summary>
        /// <param name="QcInfoID">质检编号</param>
        /// <returns></returns>
        public static void GetSumWaterCount(int QcInfoID)
        {
            try
            {
                //确定规则后这里获取包数和每包针数要修改.如果完全由DTS抽包就到接口表获取抽包号
                //如果系统也要抽包就到Packets表里面获取DTS和质检系统抽包。
                //关于针数可以按照总包数<=50每包测8针  总包数>50每包测4针
                DataTable dt = LinQBaseDao.Query("select Packets_DTS,Packets_this from Packets where Packets_QCInfo_DRAW_EXAM_INTERFACE_ID=(select QCInfo_DRAW_EXAM_INTERFACE_ID from QCInfo where QCInfo_ID=" + QcInfoID + ")").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string Packets_DTS = dt.Rows[0]["Packets_DTS"].ToString();
                    string Packets_this = dt.Rows[0]["Packets_this"].ToString();

                    if (Packets_this.Length > 0)
                    {
                        Packets_DTS += "," + Packets_this;
                    }
                    testBags = Packets_DTS.Split(',');

                    //if (ItemMoist == 0)
                    //{
                    //    testBagWaterCount = ItemMoistCount;
                    //    SumWaterCount = testBags.Count() * testBagWaterCount;
                    //}
                    //else
                    //{
                    //    testBagWaterCount = 0;
                    //    SumWaterCount = ItemMoistCount;
                    //}
                    //yk2016.4.25号暂时修改
                    MoistureDetectionRulesCreator ruleCreator = new MoistureDetectionRulesCreator();
                    SumWaterCount = ruleCreator.GetSumWaterCount(Common.OrganizationID, testBags, out testBagWaterCount);
                }
                else
                {
                    SumWaterCount = 0;
                }
            }
            catch (Exception)
            {
                SumWaterCount = 0;
            }
        }


        #region 随机抽包
        /// <summary>
        /// 生成抽包随机数
        /// </summary>
        /// <param name="debarNumber">存放数据集合</param>
        /// <param name="debarNumber">需要排除的数字</param>
        /// <param name="count">生成随机数的个数</param>
        /// <param name="maxNumber">最大数</param>
        /// <param name="minNumber">最小数</param>
        private static Random r = null;

        public static List<int> returnRandomNumbers(List<int> debarNumber, int count, int minNumber, int maxNumber)
        {
            try
            {
                List<int> nums = new List<int>();
                r = new Random();

                GetRandomNumber(nums, debarNumber, count, minNumber, maxNumber);


                return nums;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

            return null;
        }


        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="debarNumber">需要排除的数字</param>
        /// <param name="count">生成随机数的个数</param>
        /// <param name="maxNumber">最大数</param>
        /// <param name="minNumber">最小数</param>
        public static void GetRandomNumber(List<int> nums, List<int> debarNumber, int count, int minNumber, int maxNumber)
        {
            bool isHave = false;

            int num = r.Next(minNumber, maxNumber);

            //循环判断是不是需要排除的数
            for (int i = 0; i < debarNumber.Count; i++)
            {
                if (debarNumber[i] == num)
                {
                    isHave = true;
                    break;
                }
            }
            //循环判断list里面是否重复
            foreach (int item in nums)
            {
                if (item == num)
                {
                    isHave = true;
                    break;
                }
            }
            if (isHave == false)
            {
                nums.Add(num);
            }
            if (nums.Count == count)
            {

                return;
            }
            else
            {
                GetRandomNumber(nums, debarNumber, count, minNumber, maxNumber);
            }

        }
        #endregion



        #region 系统设置内容
        /// <summary>
        /// 硬盘录像机的IP
        /// </summary>
        public static string DVRIP;
        /// <summary>
        /// 硬盘录像机的服务器端口号
        /// </summary>
        public static string DVRServerPort;
        /// <summary>
        /// 硬盘录像机的登录名称
        /// </summary>
        public static string DVRLoginName;
        /// <summary>
        /// 硬盘录像机的登录密码
        /// </summary>
        public static string DVRPwd;
        /// <summary>
        /// 保存地址
        /// </summary>
        public static string SaveFiel;
        /// <summary>
        /// 拍照路数包重
        /// </summary>
        public static int Channel;
        /// <summary>
        /// 拍照路数杂纸
        /// </summary>
        public static int Channel2;
        /// <summary>
        /// 拍照路数杂质
        /// </summary>
        public static int Channel3;

        /// <summary>
        /// 获取和设置包含该应用程序的目录的名称。
        /// result: X:\xxx\xxx\ (.exe文件所在的目录+"\CarPhoto")
        /// </summary>
        public static string BaseFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + ConfigurationManager.ConnectionStrings["Folder"].ToString() + "\\";
        public static string Version = "";
        public static string LEDEndTime = "";
        public static string LEDTabTime = "";
        public static string OrganizationID = "";
        public static string SourceID = "";
        public static string GainURL = "";
        public static string SendURL = "";
        public static int removePackage = 0;
        /// <summary>
        /// 是否现场抽包
        /// </summary>
        public static string ISPackets = "";

        /// <summary>
        /// 是否进入打印模板设置界面
        /// </summary>
        public static bool rbool = true;
        /// <summary>
        /// 是否退货
        /// </summary>
        public static bool rboolBT = true;
        /// <summary>
        /// 打印质检编号
        /// </summary>
        public static int intQCInfo_ID = 0;
        /// <summary>
        /// 打印标题
        /// </summary>
        public static string PrintDemoTitle = "国废验收报告（第     次）";
        /// <summary>
        /// 退货打印标题
        /// </summary>
        public static string AbnormalPrintDemoTitle = "国废退货报告（第     次）";
        /// <summary>
        /// 打印模板默认路径
        /// </summary>
        public static string PrintDemoTitleRoute = Application.StartupPath + @"\Template\国废验收报告-标准模版.doc";
        /// <summary>
        /// 打印模板临时路径
        /// </summary>
        public static string PrintDemoTitleRouteTemporary = Application.StartupPath + @"\Template\国废验收报告-标准模版.doc";
        /// <summary>
        /// 获取车辆进厂时间与当前间隔时间
        /// 默认为30分钟
        /// </summary>
        public static int CTIME = 30;
        /// <summary>
        /// 可修改质检数据有效时间
        /// 默认为3天
        /// </summary>
        public static int IQC = 3;
        /// <summary>
        /// 加密锁识别码
        /// </summary>
        public static string NTCODE = "LWCQEmewe";//"LWJXEmewe"; //LWCQEmewe

        //磅串口通讯属性
        public static DataTable dtScaleComAttribute = new DataTable();
        
        /// <summary>
        /// 小磅1串口号
        /// </summary>
        public static int WEIGHTCOM = 0;
        public static int WEIGHTCOMBaudRate = 1200; //小磅1波特率
        public static string platformScaleName = "";// 小磅1型号

        
        /// <summary>
        /// 小磅2串口号
        /// </summary>
        public static int WEIGHTCOMTwo = 0;
        public static int WEIGHTCOMTwoBaudRate = 1200;
        public static string platformScaleNameTwo = "";// 小磅2型号
        

        /// <summary>
        /// 水分仪串口号
        /// </summary>
        public static int MOISTURECOM = 0;


        /// <summary>
        /// 水分仪串口号2
        /// </summary>
        public static int MOISTURECOMTwo = 0;
        /// <summary>
        /// 水分最大值
        /// </summary>
        public static float MOISTUREMAX = 50;
        /// <summary>
        /// 水分最小值
        /// </summary>
        public static float MOISTUREMIN = 7;


        /// <summary>
        /// 水分检测页面1号水分检测员选择下标
        /// </summary>
        public static int waterManSelect = 0;

        /// <summary>
        /// 水分检测页面2号水分检测员选择下标
        /// </summary>
        public static int waterManSelectTwo = 0;


        /// <summary>
        /// 水分仪序号
        /// </summary>
        public static string MOISTURENAME = "";

        
        /// <summary>
        /// 水分仪序号2
        /// </summary>
        public static string MOISTURENAMETwo = "";

        
        /// <summary>
        /// LED屏幕X坐标
        /// </summary>
        public static int LEDFROMX = 1940;
        /// <summary>
        /// LED屏幕Y坐标
        /// </summary>
        public static int LEDFROMY = 60;
        /// <summary>
        /// LED内容起X坐标
        /// </summary>
        public static int LEDX = 0;
        /// <summary>
        /// LED内容起Y坐标
        /// </summary>
        public static int LEDY = 0;
        /// <summary>
        /// 客户端编号
        /// </summary>
        public static int CLIENTID = 0;
        /// <summary>
        /// 客户端名称
        /// </summary>
        public static string CLIENT_NAME = "test";
        /// <summary>
        /// 采集端编号
        /// </summary>
        public static int CollectionID = 0;
        /// <summary>
        /// 采集端名称
        /// </summary>
        public static string CollectionNAME = "test";
        /// <summary>
        /// 仪表编号
        /// </summary>
        public static int InstrumentID = 0;

        /// <summary>
        /// 采集端编号集合
        /// </summary>
        public static string StrCollectionIDS = "";
        /// <summary>
        /// 水分检测员使用仪表编号
        /// </summary>
        public static string StrInstrumentIDS = "";
        /// <summary>
        /// 保安部水分检测员使用仪表编号
        /// </summary>
        public static string StaInstrumentIDS = "";

        ///// <summary>
        ///// 检测规则，0每包 1总计
        ///// </summary>
        //public static int ItemMoist = 0;
        ///// <summary>
        ///// 水分单包或总计检测次数
        ///// </summary>
        //public static int ItemMoistCount = 8;

        #endregion
        #region 测试水分的设置
        /// <summary>
        /// 总包数
        /// </summary>
        public static int IBAGCOUNT = 3;
        /// <summary>
        /// 每包测试水分点数
        /// 拆包后水分检测
        /// </summary>
        public static int BAGWATE = 4;
        /// <summary>
        /// 整车测试水分点数
        /// 拆包前水分检测
        /// </summary>
        public static int TOTALCARWATE = 24;

        /// <summary>
        /// 当前是否有获取的水分仪数据 true 为获取，False为不获取
        /// </summary>
        public static bool ISCURRENTDATA = false;


        /// <summary>
        /// 当前是否有获取的2号水分仪数据 true 为获取，False为不获取
        /// </summary>
        public static bool ISCURRENTDATATwo = false;
        ///// <summary>
        ///// 拆包前水分编号
        ///// </summary>
        //public static int iUnpackBeforeMOISTItems = 0;
        ///// <summary>
        ///// 拆包后水分编号
        ///// </summary>
        //public static int iUnpackBackMOIST = 0;

        /// <summary>
        /// 水分仪最大序号
        /// </summary>
        public static int SFYCOUNT = 127;
        #endregion
        #region 用户信息
        /// <summary>
        /// 用户角色ID
        /// </summary>
        /// <summary>
        public static int ROLE = 0;
        /// 登录名
        /// </summary>
        public static string USERNAME = "Admin";
        /// <summary>
        /// 真实姓名
        /// </summary>
        public static string NAME = "Admin";
        /// <summary>
        /// 用户编号
        /// </summary>
        public static string USERID;
        /// <summary>
        /// 用户密码
        /// </summary>
        public static string PWD;
        public static string PERI;

        #endregion
        #region 菜单权限信息
        public static ArrayList arraylist;//存放菜单ID
        public static int RoleID = 0;//角色ID
        public static int UserID = 0;//用户ID
        #endregion

        #region 类型转换

        /// <summary>
        /// 转换为double类型的数据
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double GetDouble(object num)
        {
            try
            {
                return double.Parse(num.ToString());
            }
            catch (Exception)
            {

                return 0.00;
            }

        }
        /// <summary>
        /// 转换为decimal类型的数据
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal GetDecimal(object num)
        {
            try
            {
                return decimal.Parse(num.ToString());
            }
            catch (Exception)
            {

                return 0;
            }

        }

        /// <summary>
        /// 转化换为int类型的数据
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetInt(object num)
        {
            try
            {
                return int.Parse(num.ToString());
            }
            catch (Exception)
            {

                return 0;
            }

        }


        /// <summary>
        /// 转化换为int类型的数据，输入默认值
        /// </summary>
        /// <param name="num">转换对象</param>
        /// <param name="rint">默认值</param>
        /// <returns></returns>
        public static int GetInt(object num, int rint)
        {

            try
            {
                int r = int.Parse(num.ToString());
                if (r == 0)
                {
                    r = rint;
                }
                return r;
            }
            catch (Exception)
            {

                return rint;
            }

        }

        /// <summary>
        /// 转化换为string类型的数据
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetString(object num)
        {
            try
            {
                return num.ToString();
            }
            catch (Exception)
            {

                return " ";
            }

        }
        /// <summary>
        /// 转换为Datetime类型的数据默认值为1900-01-01 00:00:00.000
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object num)
        {
            try
            {
                if (num != null)
                {
                    return DateTime.Parse(num.ToString());
                }
                else
                {
                    return DateTime.Parse("1900-01-01 00:00:00.000");
                }
            }
            catch (Exception)
            {
                return DateTime.Parse("1900-01-01 00:00:00.000");
            }
        }


        public static string GetStr(object str)
        {
            string rstr = "0";
            if (str != null)
            {
                rstr = str.ToString().Replace("%", "");
            }
            if (rstr == "合格")
            {
                rstr = "0";
            }
            return rstr;
        }
        #endregion



        /// <summary>
        /// 获取水分上下限 设置  20130704 ZJ
        /// </summary>
        /// <param name="PROD"></param>
        public static void GetWaterSet(string PROD)
        {
            try
            {
                DataTable dt = LinQBaseDao.Query("select top(1) * from dbo.Unusualstandard where Unusualstandard_PROD ='" + PROD + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    MOISTUREMIN = Convert.ToInt32(dt.Rows[0]["Unusualstandard_MOISTUREMIN"]);
                    MOISTUREMAX = Convert.ToInt32(dt.Rows[0]["Unusualstandard_MOISTUREMAX"]);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取该客户端下的采集端，仪表,然后根据数据绑定水分数据
        /// </summary>
        public static void GetCollection()
        {
            try
            {
                StrCollectionIDS = "";
                DataTable dt = LinQBaseDao.Query("select Collection_ID from dbo.CollectionInfo where Collection_Client_ID ='" + CLIENTID + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StrCollectionIDS += dt.Rows[i][0].ToString() + ",";
                    }
                    StrCollectionIDS = StrCollectionIDS.TrimEnd(',');
                }
                if (StrCollectionIDS != "")
                {
                    StrInstrumentIDS = "";
                    StaInstrumentIDS = "";
                    dt = LinQBaseDao.Query("select Instrument_ID from dbo.InstrumentInfo where Instrument_Collection_ID in (" + StrCollectionIDS + ") and Instrument_Type=0").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            StrInstrumentIDS += dt.Rows[i][0].ToString() + ",";
                        }
                    }
                    dt = LinQBaseDao.Query("select Instrument_ID from dbo.InstrumentInfo where Instrument_Collection_ID in (" + StrCollectionIDS + ") and Instrument_Type=1").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            StaInstrumentIDS += dt.Rows[i][0].ToString() + ",";
                        }
                    }
                    StrInstrumentIDS = StrInstrumentIDS.TrimEnd(',');
                    StaInstrumentIDS = StaInstrumentIDS.TrimEnd(',');
                }
            }
            catch (Exception)
            {

            }
        }


        #region 日志记录

        /// <summary>
        /// 记录测试记事本
        /// </summary>
        /// <param name="text">信息</param>
        public static void WriteTextLogWate(string text)
        {
            try
            {
                string dirpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "Wate.txt";
                StreamWriter sw = new StreamWriter(dirpath, true);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + text);
                sw.Close();
            }
            catch
            {

            }
        }


        public static void WriteTextLog(string text, string fileName)
        {
            try
            {
                string dirpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Log//" + DateTime.Now.ToString("yyyyMMdd") + fileName + "log.txt";
                if (!File.Exists(dirpath))
                {
                    FileStream fs1 = new FileStream(dirpath, FileMode.Create, FileAccess.Write);
                    fs1.Close();
                }
                StreamWriter sw = new StreamWriter(dirpath, true);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + text);
                sw.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 记录测试记事本
        /// </summary>
        /// <param name="text">信息</param>
        public static void WriteTextLog(string text)
        {
            try
            {
                string dirpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "log.txt";
                StreamWriter sw = new StreamWriter(dirpath, true);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + text);
                sw.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 操作记录到数据库表fh_operateinfo
        /// </summary>
        /// <param name="operType">操作类型</param>
        /// <param name="content">内容</param>
        /// <param name="oper_name">操作人</param>
        public static void WriteLogData(string operType, string content, string oper_name)
        {
            try
            {
                LogInfo qcRecord = new LogInfo();
                if (oper_name == "")
                {
                    oper_name = Common.USERNAME;
                }
                qcRecord.Log_Name = oper_name;
                qcRecord.Log_Dictionary_ID = DictionaryDAL.GetDictionaryID(operType);
                qcRecord.Log_Content = content;
                qcRecord.Log_Time = DateTime.Now;
                LogInfoDAL.InsertOneQCRecord(qcRecord);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("Common.WriteLogData:" + ex.Message.ToString());
            }
        }
        #endregion


        /// <summary>
        /// 设置当前获取焦点的列
        /// </summary>
        /// <param name="dgv">控件名称</param>
        /// <param name="currnetRow">当前行数</param>
        /// <param name="cellName">设置的列名称</param>
        public static void SetCurrentCell(DataGridView dgv, int currnetRow, string cellName)
        {
            dgv.CurrentCell = dgv.Rows[0].Cells[cellName];
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;//.EditOnKeystroke;//.EditOnEnter;
            dgv.BeginEdit(true);
        }




        /// <summary>
        /// 点击选中行的颜色
        /// </summary>
        /// <param name="dgv"></param>
        public static void SetSelectionBackColor(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //点击选中整行
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.YellowGreen; //付给颜色
        }
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServersTime()
        {
            try
            {
                return DateTime.Parse(LinQBaseDao.Query("select GETDATE()").Tables[0].Rows[0][0].ToString());
            }
            catch
            {

                return DateTime.Parse("1900-01-01 00:00:00.000");
            }

        }


        public static List<string> GetPlatformScalesName()
        {
            List<string> nameList = new List<string>();
            for (int rowId = 0; rowId < dtScaleComAttribute.Rows.Count; rowId++)
            {
                DataRow dr = dtScaleComAttribute.Rows[rowId];
                string name = dr["name"] as string;
                nameList.Add(name);
            }
            return nameList;
        }

        public static int GetComBaudrate(string platformScaleName)
        {
            for (int rowId = 0; rowId < dtScaleComAttribute.Rows.Count; rowId++)
            {
                DataRow dr = dtScaleComAttribute.Rows[rowId];
                string name = dr["name"] as string;
                if (name  == platformScaleName)
                {
                    return int.Parse(dr["rate"].ToString());
                }
            }
            return -1;
        }

    }
}