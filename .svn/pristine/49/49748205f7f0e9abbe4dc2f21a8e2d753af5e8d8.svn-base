using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace EMEWEQUALITY.HelpClass
{
    /// <summary>
    /// 抽取数据类
    /// </summary>
    public class GetData
    {
        /// <summary>
        /// 1水分仪编号
        /// </summary>
        public static int axMoistureNo = 1;
        public static string GetWater = "0";
        /// <summary>
        /// 2水分仪编号
        /// </summary>
        public static int axMoistureNoTwo = 1;
        public static string GetWaterTwo = "0";
        public static string GetExplain = "";
        public static string GetWeight = "0";
        public static string GetCardNo = "";
        public static string Grade = "0";
        public static int SCount = 0;
        public static string SNo = "0";
        public static int SCountTwo = 0;
        public static string SNoTwo = "0";
        /// <summary>
        /// 记录当前列表中接受的上一条数据的水分仪编号
        /// 每接受完成一条数据记录一次
        /// 每一辆车第一检测时和修改第一条检测时记录为0;
        /// </summary>
        public static int IOldSNO = 0;
        public static int IOldSNOTwo = 0;
       /// <summary>
       /// 上次记录的次数
       /// </summary>
        public static int IOldSCount=0;
        public static int IOldSCountTwo = 0;
        public static int SCount2 = 0;
        public static string SNo2 = "0";
        public static string DuoWei = "";
        public static string Person = "";


        public static int Subtotal = 0;

        /// <summary>
        /// 抽取数据编号
        /// </summary>
        public string DataNo { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public double DataSum { get; set; }

        /// <summary>
        /// 含水量
        /// </summary>
        public double DataWater { get; set; }

        /// <summary>
        /// 杂物比例
        /// </summary>
        public double DataSund { get; set; }

        /// <summary>
        /// 杂纸比例
        /// </summary>
        public double DataPaper { get; set; }

        /// <summary>
        /// 驾驶员
        /// </summary>
        public string DataDriver { get; set; }

        /// <summary>
        /// 车辆牌照
        /// </summary>
        public string DataCarNO { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string DataCustomer { get; set; }

        /// <summary>
        /// 小票号
        /// </summary>
        public string DataTicket { get; set; }

        /// <summary>
        /// 四舍五入公式
        /// </summary>
        /// <param name="math"></param>
        /// <returns></returns>
        //public static double GetDouble(double dmath)
        //{

        //    decimal rdec = Convert.ToDecimal(dmath);
        //  Math.Round(rdec, 0, MidpointRounding.AwayFromZero).ToString();
        //    //string math = dmath.ToString();

        //    //if (math.Contains(".") == false)
        //    //{
        //    //    math = math + ".0";
        //    //}


        //    //int WholeNum = Convert.ToInt32(math.Substring(0, math.IndexOf(".")));
        //    //int decl = Convert.ToInt32(math.Substring(math.IndexOf(".") + 1, 1));

        //    //if (decl >= 5)
        //    //{
        //    //    WholeNum = WholeNum + 1;
        //    //}
        //    //return WholeNum;
        //}


        public static string GetDatePrint(int checkNo,int state)
        {
            //没有打印编号的状态
            if (state == 0)
            {
                string year = DateTime.Now.Year.ToString();
                year = year.Substring(2, 2);
                string month = DateTime.Now.Month.ToString();
                if (month.Length <= 1)
                {
                    month = "0" + month;
                }
                string day = DateTime.Now.Day.ToString();
                if (day.Length <= 1)
                {
                    day = "0" + day;
                }

                string n = checkNo.ToString();
                if (n.Length <= 1)
                {
                    n = "00" + n;
                }
                else if (n.Length <= 2)
                {
                    n = "0" + n;
                }
                string d = year + month + day + n;
                return d;
            }
            else if(state==1)
            {
                return checkNo.ToString();
            }

            return "";
        }



        /// <summary>
        /// 保留小数点后面X个小数
        /// </summary>
        /// <param name="math">需转换的值</param>
        /// <param name="mathCount">小数点的位数</param>
        /// <returns></returns>
        public static string GetStringMath(string math, int mathCount)
        {
            string rMath = "";
            try
            {
                decimal rdec = Convert.ToDecimal(math);
                rMath = Math.Round(rdec, mathCount, MidpointRounding.AwayFromZero).ToString();
            //    if (math.Contains(".") == false)
            //    {
            //        if (string.IsNullOrEmpty(math))
            //        {
            //            math = "0.0";
            //        }
            //        else
            //            math = math + ".0";
            //    }

            //    int indexof = math.IndexOf(".");//小数点的坐标
            //    int ilenght = math.Length;//总长度
            //    int ix = ilenght - indexof - 1;//去掉小数点前几位的总位数
            //    if (ix >= mathCount)
            //    {
            //        int isub=indexof + mathCount;
            //        if (isub + 2 <= ilenght)
            //        {
            //            int iendtwo = EMEWE.Common.Converter.ToInt(math.Substring(isub, 1));
            //            int iend = EMEWE.Common.Converter.ToInt(math.Substring(isub + 1, 1));
            //            if (iend >= 5)
            //            {
            //                iendtwo = iendtwo + 1;
            //                rMath = math.Substring(0, isub)+iendtwo.ToString();
            //            }
            //            else rMath = math.Substring(0, isub+1);
            //        }
            //        else
            //        {
            //            rMath = math.Substring(0, isub + 1);
            //        }
            //    }
            //    else
            //    {
            //        rMath = math;
            //        int ic = mathCount - ix;//相差的位数
            //        for (int j = 0; j < ic; j++)//相差几位补几个0
            //        {
            //            rMath += "0";
            //        }
            //    }
            }
            catch (Exception ex) 
            {
                throw new Exception("保留小数点后面指定位数的小数处理异常："+ex.Message);
            }

                return rMath;
        }

        /// <summary>
        /// 保留小数点后面X个小数并变成百分数
        /// </summary>
        /// <param name="math">需转换的值</param>
        /// <param name="mathCount">小数点的位数</param>
        /// <returns></returns>
        public static string GetStringMathAndPercentage(string math, int mathCount)
        {
            string rMath = "";
            try
            {

                decimal rdec = Convert.ToDecimal(math) * 100;

               rMath= Math.Round(rdec, mathCount,MidpointRounding.AwayFromZero).ToString();
                #region 2012-10-15 周意 计算杂质比例时应先乘100在进行取保留2位的小数点并进行四舍五入。
                //if (!string.IsNullOrEmpty(math))
                //{
                //    math = (Convert.ToDecimal(math) * 100).ToString();
                //  }
                #endregion
                    //if (math.Contains(".") == false)
                    //{
                    //    if (string.IsNullOrEmpty(math))
                    //    {
                    //        math = "0.0";
                    //    }
                    //    else
                    //        math = math + ".0";
                    //}

                    //int indexof = math.IndexOf(".");//小数点的坐标
                    //int ilenght = math.Length;//总长度
                    //int ix = ilenght - indexof - 1;//去掉小数点前几位的总位数
                    //if (ix >= mathCount)
                    //{
                    //    int isub = indexof + mathCount;
                    //    if (isub + 2 <= ilenght)
                    //    {
                    //        int iendtwo = EMEWE.Common.Converter.ToInt(math.Substring(isub, 1));
                    //        int iend = EMEWE.Common.Converter.ToInt(math.Substring(isub + 1, 1));
                    //        if (iend >= 5)
                    //        {
                    //            iendtwo = iendtwo + 1;
                           
                    //            rMath = math.Substring(0, isub) + iendtwo.ToString();
                    //        }
                    //        else rMath = math.Substring(0, isub + 1);
                    //    }
                    //    else
                    //    {
                    //        rMath = math.Substring(0, isub + 1);
                    //    }
                    //}
                    //else
                    //{
                    //    rMath = math;
                    //    int ic = mathCount - ix;//相差的位数
                    //    for (int j = 0; j < ic; j++)//相差几位补几个0
                    //    {
                    //        rMath += "0";
                    //    }
                    //}
                    //#region 2012-10-25 去掉计算杂质比例时应先乘100在进行取保留2位的小数点并进行四舍五入。
                    //// rMath =( EMEWE.Common.Converter.ToDouble(rMath) * 100).ToString();
                    //#endregion
              
            }
            catch (Exception ex)
            {
                throw new Exception("保留小数点后面指定位数的小数处理异常：" + ex.Message);
            }

            return rMath;
        }
    }
}
