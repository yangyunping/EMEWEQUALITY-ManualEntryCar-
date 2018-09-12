using System;
using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Text;

using System.Windows.Forms;

using System.Drawing.Printing;

using System.IO;
using System.Configuration;
using System.Collections;

namespace EMEWEQUALITY.HelpClass
{
    public class PrintDemo
    {
        public PrintDemo()
        {
            printFont = new Font("Arial", 50);
            pd = new PrintDocument();
         //开始打印事件
            pd.BeginPrint += new PrintEventHandler(pd_BeginPrint);
            //订阅BeginPrint事件,用于得到被打印的内容
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);


            ////订阅事件

            ////订阅PinrtPage事件,用于绘制各个页内容
            //pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
            //
            //pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
            ////订阅EndPrint事件,用于释放资源
            //pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);

            ////２、页面设置的打印文档设置为需要打印的文档
            //dlgPageSetup.Document = pdDocument;

        }
        Bitmap bmp = (Bitmap)Bitmap.FromFile(Application.StartupPath + @"\DaYinMap.png");
        //打印编码
        string printCode = "ZB-AA02-02";
        //DbHelperSQL dbs = new DbHelperSQL();
        string printType = "";
        //文本格式
        private Font printFont;


        //---------------------------------------->1
        //打印高度
        const int LineHeight = 300;
        //打印行
        const int LinesToPrint = 10;
        //多少行
        int Lines = 0;
        //------------------------------------------>2

        //打印文本
        PrintDocument pd = new PrintDocument();
        //打印预览对话框
        PrintPreviewDialog ppd = new PrintPreviewDialog();
        //打印对话框
        #region 打印参数
        
   PrintDialog printDialog = new PrintDialog();

        //打印内容
        /// <summary>
        /// 标题
        /// </summary>
        private string PrintDemoTitle = Common.PrintDemoTitle;
        /// <summary>
        /// 供应商
        /// </summary>
        public string GongYingShang;//
        /// <summary>
        /// 车牌照
        /// </summary>
        public string carNo;//
        string str = "";
        /// <summary>
        /// 打印时间
        /// </summary>
        public string printTime; //
        /// <summary>
        /// 堆号
        /// </summary>
        public string DuiHao;//
        /// <summary>
        /// 采购编号
        /// </summary>
        public string PO_NO;//
        /// <summary>
        /// 开始检测时间
        /// </summary>
        public string BeginTime;//
        /// <summary>
        /// 完成时间
        /// </summary>
        public string EndTime;//
        /// <summary>
        /// 送货通知编号
        /// </summary>
        public string SHIPMENT_NO;//
        /// <summary>
        /// 过磅编号
        /// </summary>
        public string WEIGHT_TICKET_NO;//
        /// <summary>
        /// 检测编号
        /// </summary>
        public string QcInfoID;//

        /// <summary>
        /// 送货重量
        /// </summary>
        public string ShouHuoKG;
        /// <summary>
        /// 抽检总量
        /// </summary>
        public string ChouJianKG;
        /// <summary>
        /// 杂纸重量
        /// </summary>
        public string ZaZhiKG;
        /// <summary>
        /// 杂质重量
        /// </summary>
        public string zazhiKG;
        /// <summary>
        /// 送货件数
        /// </summary>
        public string ShouHuoCount;
        /// <summary>
        /// 抽检件数
        /// </summary>
        public string ChouJianCount;
        /// <summary>
        /// 杂纸含量
        /// </summary>
        public string ZaZhiHanLiang;
        /// <summary>
        /// 杂质含量
        /// </summary>
        public string zazhihanliang;
        #region 周意 2012-07-19 修改
/// <summary>
/// 每页打印的最大高度
/// </summary>
int iMaxHeight = 1090;
        /// <summary>
        /// 拆包前一检水份
        /// </summary>
     public    ArrayList listCBQ = new ArrayList();
      
          /// <summary>
        /// 拆包后一检水份
        /// </summary>
    public     ArrayList listCBH = new ArrayList();
       /// <summary>
       /// 复测水分点数
       /// </summary>
  public  ArrayList listRetest = new ArrayList();
/// <summary>
  /// 每行表格显示的个数 
  /// 拆包前一检水份和复测水分点数每行打印的个数
/// </summary>
  int iRowCount = 6;
       
        #endregion
  #region 周意 2012-07-20 注释：拆包前一检水份
  //拆包前一检水份
  /// <summary>
  /// 拆包前一检水份值1
  /// </summary>
  public string CBC_values1;
  /// <summary>
  /// 拆包前一检水份值2
  /// </summary>
  public string CBC_values2;
  /// <summary>
  /// 拆包前一检水份值3
  /// </summary>
  public string CBC_values3;
  /// <summary>
  /// 拆包前一检水份值4
  /// </summary>
  public string CBC_values4;
  /// <summary>
  /// 拆包前一检水份值5
  /// </summary>
  public string CBC_values5;
  /// <summary>
  /// 拆包前一检水份值6
  /// </summary>
  public string CBC_values6;
  /// <summary>
  /// 拆包前一检水份值7
  /// </summary>
  public string CBC_values7;
  /// <summary>
  /// 拆包前一检水份值8
  /// </summary>
  public string CBC_values8;
  /// <summary>
  /// 拆包前一检水份值9
  /// </summary>
  public string CBC_values9;
  /// <summary>
  /// 拆包前一检水份值10
  /// </summary>
  public string CBC_values10;
  /// <summary>
  /// 拆包前一检水份值11
  /// </summary>
  public string CBC_values11;
  /// <summary>
  /// 拆包前一检水份值12
  /// </summary>
  public string CBC_values12;
  /// <summary>
  /// 拆包前一检水份值13
  /// </summary>
  public string CBC_values13;
  /// <summary>
  /// 拆包前一检水份值14
  /// </summary>
  public string CBC_values14;
  /// <summary>
  /// 拆包前一检水份值15
  /// </summary>
  public string CBC_values15;
  /// <summary>
  /// 拆包前一检水份值16
  /// </summary>
  public string CBC_values16;
  /// <summary>
  /// 拆包前一检水份值17
  /// </summary>
  public string CBC_values17;
  /// <summary>
  /// 拆包前一检水份值18
  /// </summary>
  public string CBC_values18; 
  #endregion
        /// <summary>
        /// 拆包前平均值
        /// </summary>
        public string CBC_PingJun;//
        /// <summary>
        /// 拆包前检测人
        /// </summary>
        public string CBC_QcInfoUserName;//

        #region 周意 2012-07-20 注释：系统随机抽检
        //系统随机抽检
        /// <summary>
        /// 系统随机抽检值1
        /// </summary>
        public string XX_values1;
        /// <summary>
        /// 系统随机抽检值2
        /// </summary>
        public string XX_values2;
        /// <summary>
        /// 系统随机抽检值3
        /// </summary>
        public string XX_values3;
        /// <summary>
        /// 系统随机抽检值4
        /// </summary>
        public string XX_values4;
        /// <summary>
        /// 系统随机抽检值5
        /// </summary>
        public string XX_values5;
        /// <summary>
        /// 系统随机抽检值6
        /// </summary>
        public string XX_values6;
        /// <summary>
        /// 系统随机抽检值7
        /// </summary>
        public string XX_values7;
        /// <summary>
        /// 系统随机抽检值8
        /// </summary>
        public string XX_values8;
        /// <summary>
        /// 系统随机抽检值9
        /// </summary>
        public string XX_values9;
        /// <summary>
        /// 系统随机抽检值10
        /// </summary>
        public string XX_values10;
        /// <summary>
        /// 系统随机抽检值11
        /// </summary>
        public string XX_values11;
        /// <summary>
        /// 系统随机抽检值12
        /// </summary>
        public string XX_values12; 
        #endregion
        /// <summary>
        /// 拆包后平均值
        /// </summary>
        public string XX_PingJun;//
        /// <summary>
        /// 拆包后总平均水份
        /// </summary>
        public string CBC_ZPingJun;
        /// <summary>
        /// 拆包后检测人
        /// </summary>
        public string XX_QcInfoUser;



        #region 周意 2012-07-20 注释：复测水份每个点数
        //复测水份
        /// <summary>
        /// 复测水份值1
        /// </summary>
        public string FC_values1;
        /// <summary>
        /// 复测水份值2
        /// </summary>
        public string FC_values2;
        /// <summary>
        /// 复测水份值3
        /// </summary>
        public string FC_values3;
        /// <summary>
        /// 复测水份值4
        /// </summary>
        public string FC_values4;
        /// <summary>
        /// 复测水份值5
        /// </summary>
        public string FC_values5;
        /// <summary>
        /// 复测水份值6
        /// </summary>
        public string FC_values6;
        /// <summary>
        /// 复测水份值7
        /// </summary>
        public string FC_values7;
        /// <summary>
        /// 复测水份值8
        /// </summary>
        public string FC_values8;
        /// <summary>
        /// 复测水份值9
        /// </summary>
        public string FC_values9;
        /// <summary>
        /// 复测水份值10
        /// </summary>
        public string FC_values10;
        /// <summary>
        /// 复测水份值11
        /// </summary>
        public string FC_values11;
        /// <summary>
        /// 复测水份值12
        /// </summary>
        public string FC_values12;
        /// <summary>
        /// 复测水份值13
        /// </summary>
        public string FC_values13;
        /// <summary>
        /// 复测水份值14
        /// </summary>
        public string FC_values14;
        /// <summary>
        /// 复测水份值15
        /// </summary>
        public string FC_values15;
        /// <summary>
        /// 复测水份值16
        /// </summary>
        public string FC_values16;
        /// <summary>
        /// 复测水份值17
        /// </summary>
        public string FC_values17;
        /// <summary>
        /// 复测水份值18
        /// </summary>
        public string FC_values18; 
        #endregion
        /// <summary>
        /// 复测平均水份
        /// </summary>
        public string FC_PingJun;//
        /// <summary>
        /// 复测检测人
        /// </summary>
        public string FC_QcInfoUserName;
        /// <summary>
        /// 复测负责人
        /// </summary>
        public string FC_FuZeUserName;
        /// <summary>
        /// 复测日期
        /// </summary>
        public string FC_Time;
        #endregion
        /// <summary>
        /// 绘制需要打印的页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (printType != "")
            {
                e.Cancel = true;//是否取消打印作业
            }
            //打印字体
            Font drawFont = new Font("Arial", 9);
            //标题
            Font title = new Font("Arial", 14);
            Font title1 = new Font("Arial", 10);
            Font title2 = new Font("Arial", 25);
            //绘制直线、曲线
            Pen blackPen = new Pen(Color.Black);
            //填充,绘图
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            int center = 54;//居中宽度
            int x = 0;//打印页x坐标
            int y = 70;//打印页y坐标
            int width = 0;//打印宽度
            int height = 30;//打印高度
            int smallWidth = 100;
            int largeWidth = 200;
            int checkWidth = center + (smallWidth + largeWidth) * 2 + 10;
            int codeWidth = center + (smallWidth + largeWidth) * 2 - 80;
            int checkNameWidth = center + 100;

            int checkheight = 70;//质检说明高度
            int checkSmalWords = 82;
            int checkLargeWords = 82;
            int checkWordsLength = 0;
            #region 页面内容

            //--------标题---------------------------------------------------------
            //标题字体
            // Font title=new  Font();
            //y = y + height;
            e.Graphics.DrawImage(bmp, Rectangle.FromLTRB(30, 0, bmp.Width, bmp.Height)); //标题


            //e.Graphics.DrawString(bmp, title, drawBrush, 200, y + Lines);
            //e.Graphics.DrawString("_____________________________", title, drawBrush, 200, y + Lines);
            //e.Graphics.DrawString("国废验收记录（第     次）", title, drawBrush, 220, y + Lines+10);
            //---------联数
            //e.Graphics.DrawString("垛位："+HelpClass.GetData.DuoWei+"", drawFont, drawBrush, center, 32 + Lines);
            //e.Graphics.DrawString(GetData.GetDatePrint(checkNo,state), drawFont, drawBrush, codeWidth, 32 + Lines);
            y = y + height;
            e.Graphics.DrawString("日期：", title1, drawBrush, codeWidth + 20, 32 + Lines + y - 40);
            str = "_____________";
            e.Graphics.DrawString(str, title1, drawBrush, codeWidth + 30 + 25 + 10, 32 + Lines + y - 40);
            str = printTime;
            e.Graphics.DrawString(str, title1, drawBrush, codeWidth + 30 + 25 + 10 + 10, 32 + Lines + y - 40);


            y = y + height;
            str = PrintDemoTitle;
            //"国废验收记录（第     次）"
            e.Graphics.DrawString(str, title, drawBrush, 250, y + Lines - 10);

            ////高宽
            x = center + 20;
            y = y + height;
            str = "供      应      商：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 4 + x - 3, 8 + y + Lines);

            str = GongYingShang;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x + 10, 8 + y + Lines);

            x = x + largeWidth + 35;
            width = smallWidth;
            str = "车      牌     号：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 4 + x - 10, 8 + y + Lines);

            str = carNo;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x, 8 + y + Lines);

            x = x + largeWidth + 40;
            width = width + smallWidth;
            str = " 堆                号：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 4 + x, 8 + y + Lines);
            str = DuiHao;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x, 8 + y + Lines);

            //--------------------------------------------------------------------第二行
            x = center + 20;
            y = y + height;
            str = "采 购 单 编 号：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 9 + x + 24, 8 + y + Lines);
            str = PO_NO;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x + 10, 8 + y + Lines);


            x = x + largeWidth + 35;
            width = smallWidth;
            str = "开始检测时间：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 9 + x - 8, 8 + y + Lines);
            str = BeginTime;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x, 8 + y + Lines);

            x = x + largeWidth + 40;
            width = width + smallWidth;
            str = "结束检测时间：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 9 + x - 8, 8 + y + Lines);
            str = EndTime;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x, 8 + y + Lines);
            //-------------------------------------------------------------------第三行
            x = center + 20;
            y = y + height;
            str = "送货通知编号：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 9 + x, 8 + y + Lines);
            str = SHIPMENT_NO;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x + 10, 8 + y + Lines);

            x = x + largeWidth + 35;
            width = smallWidth;
            str = "过磅单编号：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 9 + x, 8 + y + Lines);
            str = WEIGHT_TICKET_NO;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x, 8 + y + Lines);

            x = x + largeWidth + 40;
            width = width + smallWidth;
            str = "检  测  编  号：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 9 + x + 10 + 25, 8 + y + Lines);
            str = QcInfoID;
            //在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, 80 + x, 8 + y + Lines);
            //---------------------------------------------------------------------第四行
            x = center;
            y = y + height + 5;
            str = "注意:水份检测员需对水份检测结果负责,如复测结果与一检差异超过3%,检测人将被无偿解雇。";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 2 + x + 60, 8 + y + Lines);
            //x = center;
            //y = y + 18;
            //str = "";
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 2 + x, 8 + y + Lines);
            //---------------------------------------------------------------------第五行
            x = center;
            y = y + height;
            str = "一、水份检测记录";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 5 + x - 20 + 3, 8 + y + Lines);
            //---------------------------------------------------------------------第六行
            x = center;
            y = y + height;
            str = "1、拆包前一检" + listCBQ.Count + "个点的水份";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 5 + x + 13, 8 + y + Lines);
            #region 1、拆包前检测水份
            //-----------------------------1、拆包前检测水份----------------------------------------------------


            #region 2012-07-19 周意 拆包前检测水份每个点循环添加

            for (int i = 1; i <= listCBQ.Count; i++)
            {
                if (i == 1)//第一行第一列
                {
                    smallWidth = smallWidth - 57;
                }
                else
                {
                    x = x + smallWidth;//x赋值smallWidth赋值在后在前顺序不能颠倒
                    smallWidth = smallWidth - 36;

                }
                if (i % iRowCount == 1) //每行第一列执行设置
                {
                    x = center;
                    y = y + height;
                }



                str = i.ToString();
                //绘制一个矩形框
                e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                ////在矩形框中绘制文本
                e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
                x = x + smallWidth;
                smallWidth = smallWidth + 36;
                // str = CBC_values1;
                str = listCBQ[i - 1].ToString();
                //if (i % iRowCount == 0)//每行的最后一个
                //{
                //    //绘制一个矩形框
                //    e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth + 9, height);
                //}
                //else
                //{
                //绘制一个矩形框
                e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                //}

                ////在矩形框中绘制文本
                e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);



            }
            #endregion
            #region 2012-07-19 周意 注释:重复循环不科学新增测试水分点也不能添加
            //x = center;
            //y = y + height;
            ////smallWidth = smallWidth - 70;
            //smallWidth = smallWidth - 57;
            //str = "1";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "2";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values2;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "3";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "4";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values4;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "5";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values5;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "6";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values6;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth + 9, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            ////-------------------------------------2
            //x = center;
            //y = y + height;
            //smallWidth = smallWidth - 35;
            //str = "7";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values7;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "8";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values8;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "9";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values9;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "10";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values10;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "11";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values11;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "12";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values12;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth + 9, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            ////---------------------------------------------------3
            //x = center;
            //y = y + height;
            //smallWidth = smallWidth - 35;
            //str = "13";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values13;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "14";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values14;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "15";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values15;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "16";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values16;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "17";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values17;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "18";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = CBC_values18;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth + 9, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines); 
            #endregion


            //--------------------
            x = center;
            y = y + height;
            //在矩形框中绘制文本
            str = "拆包前平均水份：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 5 + x - 3, 8 + y + Lines);
            str = "________%";
            e.Graphics.DrawString(str, title1, drawBrush, x + 5 + 140 - 35, 8 + y + Lines);
            str = CBC_PingJun;
            e.Graphics.DrawString(str, title1, drawBrush, x + 5 + 145 - 25, 8 + y + Lines);



            x = x + smallWidth;
            str = "检测人：";
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth * 9) / 2 - str.Length * 5 + x + 110, 8 + y + Lines);
            str = "________________";
            e.Graphics.DrawString(str, title1, drawBrush, x + (5 + 130) * 3 + 20 + 65 + 5, 8 + y + Lines);
            str = CBC_QcInfoUserName;
            e.Graphics.DrawString(str, title1, drawBrush, x + (5 + 130) * 3 + 20 + 75 + 5, 8 + y + Lines);


            //---------------------------------------------------------------------第八行
            x = center;
            y = y + height + 5;
            str = "2、由系统随机抽每车3扎纸包检测，拆包后对每扎纸包测4个点,共12个点需进行水份检测";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 2 + 37 + x + 5, 8 + y + Lines);
            //x = center;
            //y = y + 18;
            //str = "共12个点需进行水份检测";
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 2 + x, 8 + y + Lines);
            #endregion
            #region 2系统随机抽
            //------------------------------------------------------------------------------------------------------------------
            //----------------------1行-----------------------
            //高宽
            x = center;
            y = y + height;
            smallWidth = smallWidth + 47;
            ////绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth + 20, height);
            //在矩形框中绘制文本
            str = "抽测纸包号、重量";
            e.Graphics.DrawString(str, drawFont, drawBrush, (smallWidth) / 2 - str.Length * 7 + x + 10, 8 + y + Lines);
            //--------------值------------------------------------------------
            x = x + smallWidth;
            //绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, largeWidth * 2 + 60, height);
            e.Graphics.DrawRectangle(blackPen, x + 20, y + Lines, (smallWidth + 140) * 2 + 52, height);
            str = "拆包后每扎纸包测" + Common.BAGWATE + "个点，共" + listCBH.Count + "个点需进行水份值";
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, drawFont, drawBrush, 65 + x, 8 + y + Lines);
            //--------------------------
            #region 周意  2012-07-19 修改后
            int iCBHRowCount = 4;

            for (int i = 1; i <= listCBH.Count; i++)
            {

                if (i % iCBHRowCount == 1) //每行第一列执行设置
                {
                    x = center;
                    y = y + height;
                    if (i == 1)//第一行第一列
                    {
                        smallWidth = smallWidth - 80;
                    }
                    else
                    {
                        smallWidth = smallWidth - 54;
                    }

                    ////绘制一个矩形框
                    e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                    //在矩形框中绘制文本
                    switch (i / iCBHRowCount + 1)
                    {
                        case 1: str = "一";
                            break;
                        case 2: str = "二";
                            break;
                        case 3: str = "三";
                            break;
                        case 4: str = "四";
                            break;
                        case 5: str = "五";
                            break;
                        case 6: str = "六";
                            break;
                        case 7: str = "七";
                            break;
                        case 8: str = "八";
                            break;
                        case 9: str = "九";
                            break;
                        case 10: str = "十";
                            break;
                        case 11: str = "十一";
                            break;
                        default: str = "一"; break;
                    }


                    e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

                    x = x + smallWidth;
                    smallWidth = smallWidth + 54;
                    str = "第   扎，   KG";
                    //绘制一个矩形框
                    e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                    ////在矩形框中绘制文本
                    e.Graphics.DrawString(str, drawFont, drawBrush, (smallWidth) / 2 - str.Length * 4 + 4 + x, 8 + y + Lines);


                }


                x = x + smallWidth;
                smallWidth = smallWidth - 54;
                str = i.ToString();
                //绘制一个矩形框
                e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                ////在矩形框中绘制文本
                e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
                x = x + smallWidth;
                smallWidth = smallWidth + 54;
                str = listCBH[i - 1].ToString();
                //绘制一个矩形框
                e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                ////在矩形框中绘制文本
                e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);



                //  str = i.ToString();
                //  //绘制一个矩形框
                //  e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                //  ////在矩形框中绘制文本
                //  e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

                //  x = x + smallWidth;
                //  smallWidth = smallWidth + 36;
                //str = listRetest[i - 1].ToString();

                //     // 绘制一个矩形框
                //      e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);


                //  ////在矩形框中绘制文本
                //  e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);



            }
            #endregion
            #region 周意  2012-07-19 注释：一个一个写修改太麻烦没有扩张性
            //x = center;
            //y = y + height;
            //smallWidth = smallWidth - 80;
            //////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            ////在矩形框中绘制文本
            //str = "一";
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = "第  扎，   KG";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, drawFont, drawBrush, (smallWidth) / 2 - str.Length * 4 + 4 + x, 8 + y + Lines);

            ////----------------
            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "1";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "2";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values2;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "3";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "4";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values4;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            ////----------------------------2-------------
            //x = center;
            //y = y + height;
            //smallWidth = smallWidth - 55;
            //////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            ////在矩形框中绘制文本
            //str = "二";
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = "第   扎，   KG";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, drawFont, drawBrush, (smallWidth) / 2 - str.Length * 4 + 4 + x, 8 + y + Lines);

            ////----------------
            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "5";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values5;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "6";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values6;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "7";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values7;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "8";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values8;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            ////----------------------------3-------------
            //x = center;
            //y = y + height;
            //smallWidth = smallWidth - 55;
            //////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            ////在矩形框中绘制文本
            //str = "三";
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = "第   扎，   KG";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, drawFont, drawBrush, (smallWidth) / 2 - str.Length * 4 + 4 + x, 8 + y + Lines);

            ////----------------
            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "9";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values9;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "10";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values10;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "11";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values11;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 55;
            //str = "12";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 55;
            //str = XX_values12;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines); 
            #endregion


            #endregion
            //---------------------------------------------------------------------第九行
            x = center;
            y = y + height;
            //在矩形框中绘制文本
            str = "拆包后平均水份：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 5 + x - 13, 8 + y + Lines);
            str = XX_PingJun;
            e.Graphics.DrawString(str, title1, drawBrush, x + 5 + 130 + 10 - 30 + 5, 8 + y + Lines);
            str = "_______%";
            e.Graphics.DrawString(str, title1, drawBrush, x + 5 + 130 - 30 + 5, 8 + y + Lines);

            x = x + smallWidth + 50;
            ////smallWidth = smallWidth * 4;
            str = "一检总平均水份：";
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth * 4) / 2 - str.Length * 12 + x + 50 - 10, 8 + y + Lines);
            str = "_______%";
            e.Graphics.DrawString(str, title1, drawBrush, x + (10 + 130) * 2 - 21, 8 + y + Lines);
            str = CBC_ZPingJun;
            e.Graphics.DrawString(str, title1, drawBrush, x + (10 + 130) * 2 + 10 - 21, 8 + y + Lines);

            x = x + smallWidth - 50;
            str = "检测人：";
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth * 5) / 2 - str.Length * 10 + x + 110, 8 + y + Lines);
            str = XX_QcInfoUser;
            e.Graphics.DrawString(str, title1, drawBrush, x + (5 + 130) * 3 - 40 + 20, 8 + y + Lines);
            str = "________________";
            e.Graphics.DrawString(str, title1, drawBrush, x + (5 + 130) * 3 - 40 + 10, 8 + y + Lines);


            //---------------------------------------------------------------------第十行

            x = center;
            y = y + height + 5;
            //在矩形框中绘制文本
            str = "二、按每车3扎纸包挑选杂纸杂质检测结果：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth * 3) / 2 - str.Length * 6 + x - 37, 8 + y + Lines);


            //---------------------------------------------------------------------第十一行

            //---------------------1
            smallWidth = smallWidth / 4 + 50;
            largeWidth = largeWidth - 10;
            x = center - 12;
            y = y + height;
            str = "送货重量：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 6 + x, 8 + y + Lines);
            str = "___________KG";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length + x + 51 - 5, 8 + y + Lines);
            str = ShouHuoKG;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + 5 + x + 51 - 5, 8 + y + Lines);

            x = x + largeWidth;
            str = "抽检总量：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 6 + x + 5, 8 + y + Lines);
            str = "___________KG";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length + x + 51, 8 + y + Lines);
            str = ChouJianKG;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + 5 + x + 51 - 5, 8 + y + Lines);

            x = x + largeWidth;
            str = " 杂纸重量：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 6 + x + 5, 8 + y + Lines);
            str = "___________KG";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length + x + 51, 8 + y + Lines);
            str = ZaZhiKG;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + 5 + x + 51 - 5, 8 + y + Lines);



            x = x + largeWidth;
            str = "杂质重量：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 6 + x + 5, 8 + y + Lines);
            str = "___________KG";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length + x + 51, 8 + y + Lines);
            str = zazhiKG;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + 5 + x + 51 - 5, 8 + y + Lines);
            //------------------------2
            x = center - 12;
            y = y + height;
            str = "送货件数：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 6 + x, 8 + y + Lines);
            str = "___________件";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length + x + 51 - 5, 8 + y + Lines);
            str = ShouHuoCount;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + 5 + x + 51 - 5, 8 + y + Lines);

            x = x + largeWidth;
            str = " 抽检件数：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 6 + x + 5, 8 + y + Lines);

            str = "___________件";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length + x + 51, 8 + y + Lines);
            str = ChouJianCount;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + 5 + x + 51 - 5, 8 + y + Lines);


            x = x + largeWidth;
            str = "杂纸含量：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 6 + x + 5, 8 + y + Lines);
            str = "___________%";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length + x + 51, 8 + y + Lines);
            str = ZaZhiHanLiang;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + 5 + x + 51 - 5, 8 + y + Lines);

            x = x + largeWidth;
            str = "杂质含量：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 6 + x + 5, 8 + y + Lines);
            str = "___________%";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length + x + 51, 8 + y + Lines);
            str = zazhihanliang;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + 5 + x + 51 - 5, 8 + y + Lines);

            //---------------------------------------------------------------------第十二行
            //-------画三个大框
            x = center;
            smallWidth = smallWidth + 43;
            y = y + height + 5;
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth + 125, height * 3);
            //x = x + largeWidth;
            e.Graphics.DrawRectangle(blackPen, x + smallWidth + 125, y + Lines, smallWidth + 125, height * 3);
            //x = x + largeWidth;
            e.Graphics.DrawRectangle(blackPen, x + 2 * (smallWidth + 125), y + Lines, smallWidth + 125, height * 3);
            //-------------1
            x = center + 20;
            e.Graphics.DrawRectangle(blackPen, x, y + Lines + 20, 20, 20);
            str = "收货";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20, 8 + y + Lines + 15);

            x = x + 20 + center;
            e.Graphics.DrawRectangle(blackPen, x, y + Lines + 20, 20, 20);
            str = "退货";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20, 8 + y + Lines + 15);

            x = center;
            y = y + height + 20;
            str = "质检签名:";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20, 8 + y + Lines);
            str = "____________";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + 80 - 10, 8 + y + Lines);
            //str = "张三";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 20 + 90, 8 + y + Lines);

            //-------------2

            e.Graphics.DrawRectangle(blackPen, x + smallWidth + 145, y + Lines - height, 20, 20);
            str = "收货";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + smallWidth + 145, 8 + y + Lines + 15 - height - 20);

            x = x + 20 + center;
            e.Graphics.DrawRectangle(blackPen, x + smallWidth + 145, y + Lines - height, 20, 20);
            str = "退货";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + smallWidth + 145, 8 + y + Lines + 15 - height - 20);

            x = center;
            str = "车间签名:";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + smallWidth + 145 - 20, 8 + y + Lines);
            str = "____________";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + smallWidth + 145 - 20 + 80 - 10, 8 + y + Lines);
            //str = "张三";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 20 + smallWidth + 145 - 20+90, 8 + y + Lines);

            //------------------3
            e.Graphics.DrawRectangle(blackPen, x + 20 + (smallWidth + 125) * 2, y + Lines - height, 20, 20);
            str = "收货";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + (smallWidth + 125) * 2 + 20, 8 + y + Lines + 15 - height - 20);

            x = x + 20 + center;
            e.Graphics.DrawRectangle(blackPen, x + 20 + (smallWidth + 125) * 2, y + Lines - height, 20, 20);
            str = "退货";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + (smallWidth + 125) * 2 + 20, 8 + y + Lines + 15 - height - 20);

            x = center;
            str = "采购签名:";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + (smallWidth + 145 - 20) * 2, 8 + y + Lines);
            str = "____________";
            e.Graphics.DrawString(str, title1, drawBrush, x + 20 + (smallWidth + 145 - 20) * 2 + 80 - 10, 8 + y + Lines);
            //str = "张三";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 20 + (smallWidth + 145 - 20) * 2+90, 8 + y + Lines);

            //---------------------------------------------------------------------第十三行
            x = center;
            y = y + height + 20;
            //在矩形框中绘制文本
            str = "三、复测水份";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x - 14, 8 + y + Lines);
            x = x + smallWidth;
            str = "复测日期:";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth * 7) / 2 - str.Length * 2 + x + 20, 8 + y + Lines);

            str = "____________";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth * 7) / 2 + x + 40 + 50 - 10, 8 + y + Lines);
            str = FC_Time;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth * 7) / 2 + x + 50 + 50 - 10, 8 + y + Lines);
            #region 1、复测水分
            //-----------------------------1、复测水分----------------------------------------------------
            #region 周意 2012-07-20 修改后的
            //listRetest


            for (int i = 1; i <= listRetest.Count; i++)
            {
                if (i == 1)//第一行第一列
                {
                    smallWidth = 43;
                }
                else
                {
                    x = x + smallWidth;//x赋值smallWidth赋值在后在前顺序不能颠倒
                    smallWidth = smallWidth - 36;

                }
                if (i % iRowCount == 1) //每行第一列执行设置
                {
                    x = center;
                    y = y + height;// + 4;

                }

                str = i.ToString();
                //绘制一个矩形框
                e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                ////在矩形框中绘制文本
                e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

                x = x + smallWidth;
                smallWidth = smallWidth + 36;
                str = listRetest[i - 1].ToString();
                //if (i % iRowCount == 0)//每行的最后一个
                //{
                //    //绘制一个矩形框
                //    e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth + 9, height);
                //}
                //else
                //{
                // 绘制一个矩形框
                e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
                //}

                ////在矩形框中绘制文本
                e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);



            }

            #endregion
            #region 周意 2012-07-20注释：一个一个的写太繁琐
            //x = center;
            //y = y + height + 4;
            //smallWidth = 43;
            //str = "1";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "2";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values2;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "3";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "4";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values4;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "5";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values5;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "6";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values6;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            ////-------------------------------------2
            //x = center;
            //y = y + height;
            //smallWidth = smallWidth - 35;
            //str = "7";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values7;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "8";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values8;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "9";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values9;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "10";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values10;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "11";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values11;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "12";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values12;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            ////---------------------------------------------------3
            //x = center;
            //y = y + height;
            //smallWidth = smallWidth - 35;
            //str = "13";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values13;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "14";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values14;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "15";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values15;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "16";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values16;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "17";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values17;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines);

            //x = x + smallWidth;
            //smallWidth = smallWidth - 35;
            //str = "18";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x, 8 + y + Lines);
            //x = x + smallWidth;
            //smallWidth = smallWidth + 35;
            //str = FC_values18;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title1, drawBrush, x + 5, 8 + y + Lines); 
            #endregion

            //---------------------------------------------------------------------第十四行
            x = center;
            y = y + height;
            smallWidth = 80;
            //在矩形框中绘制文本
            str = "复测平均水份：";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 5 + x - 7, 8 + y + Lines);
            str = "________%";
            e.Graphics.DrawString(str, title1, drawBrush, smallWidth + x + 50 - 35 + 3, 8 + y + Lines);
            str = FC_PingJun;
            e.Graphics.DrawString(str, title1, drawBrush, smallWidth + x + 60 - 35, 8 + y + Lines);

            x = x + smallWidth + 40;
            smallWidth = smallWidth * 4;
            str = "检测人：";
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 8 + x + 50, 8 + y + Lines);
            str = "________________";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + x + 80 - 10, 8 + y + Lines);
            str = FC_QcInfoUserName;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + x + 90 - 10, 8 + y + Lines);

            x = x + smallWidth / 2;
            smallWidth = smallWidth + 60;
            str = "负责人：";
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 - str.Length * 5 + x + 70 + 5, 8 + y + Lines);
            str = "________________";
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + x + 110 - 7 + 5, 8 + y + Lines);
            str = FC_FuZeUserName;
            e.Graphics.DrawString(str, title1, drawBrush, (smallWidth) / 2 + x + 120 - 7 + 5, 8 + y + Lines);
            #endregion 
            #endregion
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="type">打印/打印预览</param>
        public void GetPrintData()
        {

            PrintPreviewDialog ClassicPreview = new PrintPreviewDialog();
            ClassicPreview.Document = pd;
            //ClassicPreview.
            //显示打印预览框
            try
            {
                ClassicPreview.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("请确认打印机是否已连接！");
            }
        }





        /// <summary>
        /// 开始打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pd_BeginPrint(object sender, PrintEventArgs e)
        {
            //打印行为0
            Lines = 0;
        }
        /// <summary>
        /// 验证当前需要打印的高度是否超过最大高度
        /// </summary>
        /// <param name="icurrentHeight">当前需要的高度</param>
        /// <returns>如果超过返回false,未超过返回true</returns>
        bool ISMaxHeight(int icurrentHeight)
        {
            bool rbool = false;
            if (icurrentHeight <= iMaxHeight) {
                rbool = true;
            }
            return rbool;
        }

    }
}
