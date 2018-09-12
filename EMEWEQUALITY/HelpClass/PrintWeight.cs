using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using EMEWEDAL;

namespace EMEWEQUALITY.HelpClass
{
    public class PrintWeight0
    {
        //打印文本
        PrintDocument pd = new PrintDocument();
        //打印预览对话框
        PrintPreviewDialog ppd = new PrintPreviewDialog();
        //打印对话框
        PrintDialog printDialog = new PrintDialog();
        /// <summary>
        /// 检测编号
        /// </summary>
        public string QcInfo_ID;

        /// <summary>
        /// 过磅单号
        /// </summary>
        public string WEIGHT_TICKET_NO;

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string CNTR_NO;

        /// <summary>
        /// 供应商
        /// </summary>
        public string REF_NO;

        /// <summary>
        /// 采购单号
        /// </summary>
        public string PO_NO;

        /// <summary>
        /// 送货通知书编号
        /// </summary>
        public string SHIPMENT_NO;

        /// <summary>
        /// 送货件数
        /// </summary>
        public string NO_OF_BALES;

        /// <summary>
        /// 抽检件数
        /// </summary>
        public string QCInfo_PumpingPackets;

        /// <summary>
        /// 司称员
        /// </summary>
        public string QCRecord_Name;
        /// <summary>
        /// 称重类型
        /// </summary>

        /// <summary>
        /// 抽检称重时间
        /// </summary>
        public string QCRecord_TIME;

        /// <summary>
        /// 抽检重量
        /// </summary>
        ///
        public string QCInfo_BAGWeight;

        /// <summary>
        /// 杂质重量
        /// </summary>
        ///
        public string QCInfo_MATERIAL_WEIGHT;//杂质重量

        /// <summary>
        /// 退货还包称重时间
        /// </summary>
        /// 
        public string QCInfo_RETURNBAG_TIME;

        /// <summary>
        /// 退货还包重量
        /// </summary>

        public string QCInfo_RETURNBAG_WEIGH;

        /// <summary>
        /// 杂纸重量
        /// </summary>
        public string QCInfo_PAPER_WEIGHT;

        /// <summary>
        /// 货品
        /// </summary>
        public string PROD_ID;

        /// <summary>
        /// 堆位
        /// </summary>
        public string ExtensionField2;

        public PrintWeight0()
        {
            pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintDemoFormWeight_Paint);
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void PrintDemoFormWeightPrint()
        {
            try
            {
                pd.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

        }
        //打印内容填写
        void PrintDemoFormWeight_Paint(object sender, PrintPageEventArgs e)
        {

            Font font = new Font("宋体", 16);
            Font headingFont = new Font("宋体", 8);
            Font headingFonts = new Font("宋体", 6);
            Font captionFont = new Font("宋体", 10);
            Font captionFontT = new Font("宋体", 14);
            Brush brush = new SolidBrush(Color.Black);

            int x = 0;
            int y = 0;
            int y1 = 38;
            string Datatime = Common.GetServersTime().ToString();//打印时间
            string[] ContentArray = new string[13];//内容
            ContentArray[0] = WEIGHT_TICKET_NO;
            ContentArray[1] = "";
            ContentArray[2] = REF_NO;
            ContentArray[3] = PROD_ID;
            ContentArray[4] = QCRecord_Name;
            ContentArray[5] = "国废";
            ContentArray[6] = "";
            ContentArray[7] = PO_NO;
            ContentArray[8] = "";
            ContentArray[9] = CNTR_NO;
            ContentArray[10] = QCRecord_TIME;
            ContentArray[11] = "";
            ContentArray[12] = SHIPMENT_NO;
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    x += 10;
                    count = x;
                }
                if (i == 2)
                {
                    x = x - 18;
                }
                if (i == 1)
                {
                    x = count;
                    x = x - 15;
                }

                if (i > 0)
                {
                    x += 300;
                }
                e.Graphics.DrawString(Datatime, captionFont, brush, x + 70, y1 + 5, StringFormat.GenericDefault);
                int y2 = y1 + 24;
                int x2 = x + 50;
                for (int s = 0; s < 13; s++)
                {
                    e.Graphics.DrawString(ContentArray[s], captionFont, brush, x2, y2 + (s * 25), StringFormat.GenericDefault);
                }
                if (i == 1)
                {

                }

                //e.Graphics.DrawString("送货\r\n件数", headingFont, brush, x, y + 398, StringFormat.GenericDefault);
                e.Graphics.DrawString(NO_OF_BALES, captionFont, brush, x - 10, y + 380, StringFormat.GenericDefault);
                //e.Graphics.DrawString("退货\r\n件数", headingFont, brush, x + 100, y + 398, StringFormat.GenericDefault);
                e.Graphics.DrawString(QCInfo_PumpingPackets, captionFont, brush, x + 80, y + 380, StringFormat.GenericDefault);
                //e.Graphics.DrawString("实收\r\n件数", headingFont, brush, x + 210, y + 398, StringFormat.GenericDefault);
                //e.Graphics.DrawString(CarRecordTakeQuantity, captionFont, brush, x + 170, y + 380, StringFormat.GenericDefault);
                //e.Graphics.DrawString(" 毛 重 ", captionFont, brush, x, y + 428, StringFormat.GenericDefault);
                e.Graphics.DrawString(QCInfo_BAGWeight + "KG", captionFont, brush, x + 10, y + 405, StringFormat.GenericDefault);
                //e.Graphics.DrawString(" 空 重 ", captionFont, brush, x + 150, y + 428, StringFormat.GenericDefault);
                //e.Graphics.DrawString(CarRecord_Tare, captionFont, brush, x + 150, y + 405, StringFormat.GenericDefault);
                if (!string.IsNullOrEmpty(QCInfo_MATERIAL_WEIGHT))
                {
                    e.Graphics.DrawString(" 杂 质 ", captionFont, brush, x - 30, y + 430, StringFormat.GenericDefault);
                    e.Graphics.DrawString(QCInfo_MATERIAL_WEIGHT + "KG", captionFont, brush, x + 20, y + 430, StringFormat.GenericDefault);
                }
                if (!string.IsNullOrEmpty(QCInfo_PAPER_WEIGHT))
                {
                    e.Graphics.DrawString(" 杂 纸 ", captionFont, brush, x + 100, y + 430, StringFormat.GenericDefault);
                    e.Graphics.DrawString(QCInfo_PAPER_WEIGHT + "KG", captionFont, brush, x + 150, y + 430, StringFormat.GenericDefault);
                }
                //e.Graphics.DrawString(" 堆 位 ", captionFont, brush, x, y + 480, StringFormat.GenericDefault);
                e.Graphics.DrawString(ExtensionField2, captionFont, brush, x + 10, y + 455, StringFormat.GenericDefault);
                ////e.Graphics.DrawString("卸货点 ", captionFont, brush, x + 150, y + 480, StringFormat.GenericDefault);
                //e.Graphics.DrawString(TheUnloadingPoint, captionFont, brush, x + 150, y + 455, StringFormat.GenericDefault);
                ////e.Graphics.DrawString("实点数\r\n件数(Y1) ", captionFont, brush, x, y + 510, StringFormat.GenericDefault);
                // e.Graphics.DrawString(TheUnloadingPoint, captionFont, brush, x + 20, y + 490, StringFormat.GenericDefault);
                ////e.Graphics.DrawString("点数人员: ", captionFont, brush, x + 150, y + 510, StringFormat.GenericDefault);
                //e.Graphics.DrawString("李小五", captionFont, brush, x + 165, y + 480, StringFormat.GenericDefault);
                ////e.Graphics.DrawString("叉车编号: ", captionFont, brush, x + 150, y + 530, StringFormat.GenericDefault);
                //e.Graphics.DrawString("CQ213", captionFont, brush, x + 165, y + 505, StringFormat.GenericDefault);
                ////e.Graphics.DrawString("卸货散纸\r\n件数(Y2) ", captionFont, brush, x, y + 565, StringFormat.GenericDefault);
                //e.Graphics.DrawString("12", font, brush, x + 35, y + 535, StringFormat.GenericDefault);
                ////e.Graphics.DrawString("点数时间:", captionFont, brush, x + 150, y + 565, StringFormat.GenericDefault);
                //e.Graphics.DrawString("2014-12-12 23:12:9", headingFont, brush, x + 100, y + 555, StringFormat.GenericDefault);

            }

        }
    }

    public class PrintWeight1
    {
        /// <summary>
        /// 检测编号
        /// </summary>
        public string QcInfo_ID;

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string CNTR_NO;

        /// <summary>
        /// 供应商
        /// </summary>
        public string REF_NO;

        /// <summary>
        /// 采购单号
        /// </summary>
        public string PO_NO;

        /// <summary>
        /// 送货通知书编号
        /// </summary>
        public string SHIPMENT_NO;

        /// <summary>
        /// 送货件数
        /// </summary>
        public string NO_OF_BALES;

        /// <summary>
        /// 抽检件数
        /// </summary>
        public string QCInfo_PumpingPackets;

        /// <summary>
        /// 司称员
        /// </summary>
        public string QCRecord_Name;
        /// <summary>
        /// 称重类型
        /// </summary>

        /// <summary>
        /// 抽检称重时间
        /// </summary>
        public string QCRecord_TIME;

        /// <summary>
        /// 抽检重量
        /// </summary>
        ///
        public string QCInfo_BAGWeight;

        /// <summary>
        /// 杂质重量
        /// </summary>
        ///
        public string QCInfo_MATERIAL_WEIGHT;//杂质重量

        /// <summary>
        /// 退货还包称重时间
        /// </summary>
        /// 
        public string QCInfo_RETURNBAG_TIME;

        /// <summary>
        /// 退货还包重量
        /// </summary>

        public string QCInfo_RETURNBAG_WEIGH;

        /// <summary>
        /// 杂纸重量
        /// </summary>
        public string QCInfo_PAPER_WEIGHT;

        /// <summary>
        /// 货品
        /// </summary>
        public string PROD_ID;

        public string WEIGHT_TICKET_NO;
        public string ExtensionField2;

        string str = "";
        public PrintWeight1()
        {
            printFont = new Font("Arial", 50);
            pd = new PrintDocument();
            pd.BeginPrint += new PrintEventHandler(pd_BeginPrint);
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
        }

        //打印编码
        string printCode = "ZB-AA02-02";
        //DbHelperSQL dbs = new DbHelperSQL();
        string printType = "";
        //文本格式
        private Font printFont;
        //---------------------------------------->1
        //打印高度
        //const int LineHeight = 300;
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
        PrintDialog printDialog = new PrintDialog();


         public void PrintDemoFormWeightPrint()
        {
            try
            {
                pd.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

        }


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
            Font title = new Font("Arial", 17);
            Font title1 = new Font("Arial", 13);
            Font title2 = new Font("Arial", 12);
            //绘制直线、曲线
            Pen blackPen = new Pen(Color.Black);
            //填充,绘图
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            //int center = 115;//居中宽度
            int center = 38;//居中宽度
            //int x = 0;//打印页x坐标
            //int y = 70;//打印页y坐标
            int x = 0;//打印页x坐标
            int y = 30;//打印页y坐标
            int y1 = y;//打印页y坐标
            int x1 = 0;//打印页y坐标
            int width = 0;//打印宽度
            int width1 = 20;//打印宽度
            int height = 45;//打印高度
            int smallWidth = 501;
            int smallWidth1 = 501;
            int largeWidth = 200;

            int checkWidth = center + (smallWidth + largeWidth) * 2 + 10;
            int codeWidth = center + (smallWidth + largeWidth) * 2 - 80;
            int checkNameWidth = center + 100;

            int checkheight = 70;//质检说明高度
            int checkSmalWords = 82;
            int checkLargeWords = 82;
            int checkWordsLength = 0;




            x = center;
            //  y = y - 10;
            y = y1;
            str = "重庆理文质检称量单";
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth * 2 / 3, height);
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title, drawBrush, (x * 3) - 30, y + Lines + width1 - 4);
            //--------------2行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "检测编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QcInfo_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------3行----------------

            //--------------1货品----------------------

            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "货品";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PROD_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);



            //----------货品---------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "车牌号码";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + 20);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = CNTR_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------4行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "供应商";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = REF_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------5行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "采购单号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PO_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------6行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货通知书编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = SHIPMENT_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //-------------7行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = NO_OF_BALES;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------8行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PumpingPackets;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------9行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "司称员";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_Name;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------10行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------11行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);
            //--------------12行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂质重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_MATERIAL_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------12行----------------


            //--------------杂纸----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂纸重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PAPER_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------杂纸----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_BAGWeight;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            //--------------13行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_RETURNBAG_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);
            //--------------14行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);

            str = QCInfo_RETURNBAG_WEIGH;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            x = center;
            y = y + height + 10;
            smallWidth1 = smallWidth / 12;
            str = "第一联：地磅房留存";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines);
            x1 = x + smallWidth * 2 / 3;




            y = y1;
            x = x1 + center;
            //  y = y - 10;
            str = "重庆理文质检称量单";
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth * 2 / 3, height);
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title, drawBrush, x + center + 10, y + Lines + width1 - 4);
            //--------------2行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "检测编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QcInfo_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------3行----------------



            //--------------2货品----------------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "货品";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PROD_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);





            //----------2货品---------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "车牌号码";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = CNTR_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------4行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "供应商";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = REF_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------5行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "采购单号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PO_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------6行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货通知书编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = SHIPMENT_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //-------------7行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = NO_OF_BALES;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------8行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PumpingPackets;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------9行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "司称员";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_Name;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------10行----------------
            //x = x1 + center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------11行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);



            //--------------12行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂质重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_MATERIAL_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------12行----------------


            //--------------杂纸----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂纸重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PAPER_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------杂纸----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_BAGWeight;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            //--------------13行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_RETURNBAG_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);
            //--------------14行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);

            str = QCInfo_RETURNBAG_WEIGH;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            x = x1 + center;
            y = y + height + 10;
            smallWidth1 = smallWidth / 12;
            str = "第二联：质检部留存	";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines);

            x1 = x + smallWidth * 2 / 3;



            y = y1;
            x = x1 + center;
            //  y = y - 10;
            str = "重庆理文质检称量单";
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth * 2 / 3, height);
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title, drawBrush, x + center + 10, y + Lines + width1 - 4);
            //--------------2行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "检测编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QcInfo_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------3行----------------



            //--------------3货品----------------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "货品";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PROD_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);





            //----------货品---------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "车牌号码";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = CNTR_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------4行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "供应商";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = REF_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------5行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "采购单号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PO_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------6行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货通知书编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = SHIPMENT_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //-------------7行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = NO_OF_BALES;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------8行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PumpingPackets;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------9行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "司称员";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_Name;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------10行----------------
            //x = x1 + center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------11行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);

            //--------------12行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂质重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_MATERIAL_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------12行----------------


            //--------------杂纸----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂纸重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PAPER_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------杂纸----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_BAGWeight;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            //--------------13行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_RETURNBAG_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);
            //--------------14行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);

            str = QCInfo_RETURNBAG_WEIGH;

            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            x = x1 + center;
            y = y + height + 10;
            smallWidth1 = smallWidth / 12;
            str = "第三联：供应商留存	";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines);
            ////-----------------------------表1-------------------------------------------------------
            //x = center;
            //y = y - 40;
            //str = "广东理文质检称量单";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title, drawBrush, x * 3, y + Lines + width1 + 2);
            ////--------------2行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "检测编号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QcInfo_ID;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------3行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "车牌号码";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = CNTR_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------4行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "供应商";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = REF_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------5行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "采购单号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = PO_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------6行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "送货通知书编号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = SHIPMENT_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////-------------7行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "送货件数";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = NO_OF_BALES;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------8行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检件数";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_PumpingPackets;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------9行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "司称员";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCRecord_Name;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------10行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------11行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检称重时间";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCRecord_TIME;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------12行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检重量";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_BAGWeight;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //str = "KG";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines + width1);

            ////--------------13行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "退货还包称重时间";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_RETURNBAG_TIME;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------14行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "退货还包重量";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);

            //str = QCInfo_RETURNBAG_WEIGH;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //str = "KG";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines + width1);

            ////-----------------------------表2-------------------------------------------------------
            //x = center;
            //y = y + height * 2;
            //str = "广东理文质检称量单";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title, drawBrush, x * 3, y + Lines + width1 + 2);
            ////--------------2行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "检测编号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QcInfo_ID;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------3行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "车牌号码";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = CNTR_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------4行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "供应商";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = REF_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------5行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "采购单号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = PO_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------6行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "送货通知书编号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = SHIPMENT_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////-------------7行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "送货件数";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = NO_OF_BALES;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------8行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检件数";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_PumpingPackets;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------9行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "司称员";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCRecord_Name;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------10行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------11行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检称重时间";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCRecord_TIME;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------12行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检重量";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_BAGWeight;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //str = "KG";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines + width1);

            ////--------------13行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "退货还包称重时间";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_RETURNBAG_TIME;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------14行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "退货还包重量";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);

            //str = QCInfo_RETURNBAG_WEIGH;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //str = "KG";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines + width1);




            ////-----------------------------表3-------------------------------------------------------
            //x = center;
            //y = y + height * 2;
            //str = "广东理文质检称量单";
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth, height);
            //////在矩形框中绘制文本
            //e.Graphics.DrawString(str, title, drawBrush, x * 3, y + Lines + width1 + 2);
            ////--------------2行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "检测编号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QcInfo_ID;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------3行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "车牌号码";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = CNTR_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------4行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "供应商";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = REF_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------5行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "采购单号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = PO_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------6行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "送货通知书编号";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = SHIPMENT_NO;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////-------------7行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "送货件数";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = NO_OF_BALES;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------8行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检件数";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_PumpingPackets;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------9行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "司称员";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCRecord_Name;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------10行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------11行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检称重时间";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCRecord_TIME;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------12行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "抽检重量";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_BAGWeight;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //str = "KG";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines + width1);

            ////--------------13行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "退货还包称重时间";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);
            //str = QCInfo_RETURNBAG_TIME;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------14行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "退货还包重量";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1 , height);

            //str = QCInfo_RETURNBAG_WEIGH;
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //str = "KG";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines + width1);

        }
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="type">打印/打印预览</param>
        public void GetPrintData()
        {
            pd.DefaultPageSettings.Landscape = true; //横向打印
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
                MessageBox.Show("请确认打印机是否已连接！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        //        /// word 应用对象  
        //        ///   
        //        private Microsoft.Office.Interop.Word._Application WordApp;
        //        ///   
        //        /// word 文件对象  
        //        ///   
        //        private Microsoft.Office.Interop.Word._Document WordDoc;
        //        public void CreateAWord()
        //        {
        //            try
        //            {
        //                //实例化word应用对象  
        //                this.WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
        //                object Nothing = System.Reflection.Missing.Value;
        //                this.WordDoc = this.WordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);


        //            //     object oMissing = System.Reflection.Missing.Value;
        //            //Word._Application oWord = null;
        //            //Word._Document oDoc;
        //            //oWord = new Word.Application();
        //            //oWord.Visible = true;
        //            //oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

        //                InsertText();
        //            }
        //            catch(Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.CreateAWord()" + ex.Message.ToString());
        //            }
        //        }
        //        ///   
        //        /// 添加页眉  
        //        ///   
        //        ///   
        //        public void SetPageHeader(string pPageHeader)
        //        {
        //            try
        //            {
        //                //.wdOutlineView,wdSeekPrimaryHeader;
        //                //添加页眉  
        //                this.WordApp.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdOutlineView;
        //                this.WordApp.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekPrimaryHeader;
        //                //this.WordApp.ActiveWindow.ActivePane.Selection.InsertAfter(pPageHeader);
        //                //设置中间对齐  
        //                this.WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        //                //跳出页眉设置  
        //                this.WordApp.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;
        //                //WordApp.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsMessageBox;
        //            }
        //            catch (Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.SetPageHeader()" + ex.Message.ToString());
        //            }

        //        }
        //        ///   
        //        /// 插入文字  
        //        ///   
        //        /// 文本信息  
        //        /// 字体打小  
        //        /// 字体颜色  
        //        /// 字体粗体  
        //        /// 方向  
        //        public void InsertText()
        //        {
        //            string name = "";
        //            try
        //            {
        //                object TableLenth = 14;
        //                float Columns1 = 150f;//列长度
        //                float Columns2 = 280f;//列长度
        //                Object Nothing = System.Reflection.Missing.Value;
        //                name = DateTime.Now.ToString("yyyy-mm-dd") + ".doc";
        //                //Directory.CreateDirectory(Application.StartupPath +@"\"+ name);  //创建文件所在目录
        //                object filename = Application.StartupPath + @"\PrintDemoFormWeight\" + name;  //文件保存路径
        //                WordApp.Selection.ParagraphFormat.LineSpacing = 1;//设置文档的行间距

        //                //object start = 0;
        //                //object end = 0;
        //                //Word.Range tableLocation = WordDoc.Range(ref start, ref end);
        //                //WordDoc.Tables.Add(tableLocation, 3, 4, ref Nothing, ref Nothing);
        //                //--------------------------------------表1------------------------------------------------------------------
        //                //移动焦点并换行
        //                object count = 6;
        //                object WdLine = Word.WdUnits.wdLine;//换一行;
        //                //WordApp.Selection.MoveDown(ref Nothing, ref Nothing, ref Nothing);//移动焦点
        //                //WordApp.Selection.TypeParagraph();//插入段落
        //                //文档中创建表格
        //                Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, 2, 2, ref Nothing, ref Nothing);
        //                //设置表格样式
        //                newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable.Columns[1].Width = Columns1;
        //                newTable.Columns[2].Width = Columns2;
        //                newTable.Rows[1].Height = 1;
        //                newTable.Rows[2].Height = 1;
        //                newTable.Range.Font.Size = 5F;
        //                newTable.Range.Font.Name = "宋体";
        //                newTable.Range.Font.Bold = 0;
        //                newTable.Rows.Height = 0;
        //                //填充表格内容
        //                newTable.Cell(1, 1).Range.Text = "广东理文质检称量单";
        //                newTable.Cell(1, 1).Range.Bold = 5;//设置单元格中字体为粗体
        //                newTable.Cell(1, 1).Range.Font.Size = 5F;
        //                ////合并单元格
        //                newTable.Cell(1, 1).Merge(newTable.Cell(1, 2));
        //                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
        //                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中   
        //                //填充表格内容
        //                newTable.Cell(2, 1).Range.Text = "检测编号";
        //                newTable.Cell(2, 2).Range.Text = QcInfo_ID;
        //                newTable.Cell(3, 1).Range.Text = "车牌号码";
        //                newTable.Cell(3, 2).Range.Text = CNTR_NO;
        //                newTable.Cell(4, 1).Range.Text = "供应商";
        //                newTable.Cell(4, 2).Range.Text = REF_NO;
        //                newTable.Cell(5, 1).Range.Text = "采购单号";
        //                newTable.Cell(5, 2).Range.Text = PO_NO;
        //                newTable.Cell(6, 1).Range.Text = "送货通知书编号";
        //                newTable.Cell(6, 2).Range.Text = SHIPMENT_NO;
        //                newTable.Cell(7, 1).Range.Text = "送货件数";
        //                newTable.Cell(7, 2).Range.Text = NO_OF_BALES;
        //                newTable.Cell(8, 1).Range.Text = "抽检件数";
        //                newTable.Cell(8, 2).Range.Text = QCInfo_PumpingPackets;
        //                newTable.Cell(9, 1).Range.Text = "司称员";
        //                newTable.Cell(9, 2).Range.Text = "123";
        //                newTable.Cell(10, 1).Range.Text = "称重类型";
        //                newTable.Cell(10, 2).Range.Text = "123";
        //                newTable.Cell(11, 1).Range.Text = "抽检称重时间";
        //                newTable.Cell(11, 2).Range.Text = "123";
        //                newTable.Cell(12, 1).Range.Text = "抽检重量";
        //                newTable.Cell(12, 2).Range.Text = QCInfo_BAGWeight + "           KG";
        //                newTable.Cell(13, 1).Range.Text = "退货还包称重时间";
        //                newTable.Cell(13, 2).Range.Text = "123";
        //                newTable.Cell(14, 1).Range.Text = "退货还包重量";
        //                newTable.Cell(14, 2).Range.Text = "123           KG";

        //                //////--------------------------------------表2------------------------------------------------------------------
        //                WordApp.Selection.MoveDown(ref WdLine, ref TableLenth, ref Nothing);//移动焦点
        //                WordApp.Selection.TypeParagraph();//插入段落

        //                //文档中创建表格
        //                Word.Table newTable2 = WordDoc.Tables.Add(WordApp.Selection.Range, 14, 2, ref Nothing, ref Nothing);
        //                //设置表格样式
        //                newTable2.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable2.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable2.Columns[1].Width = Columns1;
        //                newTable2.Columns[2].Width = Columns2;
        //                newTable2.Range.Font.Size = 5F;
        //                newTable2.Range.Font.Name = "宋体";
        //                newTable2.Range.Font.Bold = 0;
        //                newTable2.Rows.Height = 0;
        //                //填充表格内容
        //                newTable2.Cell(1, 1).Range.Text = "广东理文质检称量单";
        //                newTable2.Cell(1, 1).Range.Bold = 5;//设置单元格中字体为粗体
        //                newTable2.Cell(1, 1).Range.Font.Size = 5F;
        //                ////合并单元格
        //                newTable2.Cell(1, 1).Merge(newTable2.Cell(1, 2));
        //                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
        //                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中   
        //                //填充表格内容
        //                newTable2.Cell(2, 1).Range.Text = "检测编号";
        //                newTable2.Cell(2, 2).Range.Text = QcInfo_ID;
        //                newTable2.Cell(3, 1).Range.Text = "车牌号码";
        //                newTable2.Cell(3, 2).Range.Text = CNTR_NO;
        //                newTable2.Cell(4, 1).Range.Text = "供应商";
        //                newTable2.Cell(4, 2).Range.Text = REF_NO;
        //                newTable2.Cell(5, 1).Range.Text = "采购单号";
        //                newTable2.Cell(5, 2).Range.Text = PO_NO;
        //                newTable2.Cell(6, 1).Range.Text = "送货通知书编号";
        //                newTable2.Cell(6, 2).Range.Text = SHIPMENT_NO;
        //                newTable2.Cell(7, 1).Range.Text = "送货件数";
        //                newTable2.Cell(7, 2).Range.Text = NO_OF_BALES;
        //                newTable2.Cell(8, 1).Range.Text = "抽检件数";
        //                newTable2.Cell(8, 2).Range.Text = QCInfo_PumpingPackets;
        //                newTable2.Cell(9, 1).Range.Text = "司称员";
        //                newTable2.Cell(9, 2).Range.Text = "123";
        //                newTable2.Cell(10, 1).Range.Text = "称重类型";
        //                newTable2.Cell(10, 2).Range.Text = "123";
        //                newTable2.Cell(11, 1).Range.Text = "抽检称重时间";
        //                newTable2.Cell(11, 2).Range.Text = "123";
        //                newTable2.Cell(12, 1).Range.Text = "抽检重量";
        //                newTable2.Cell(12, 2).Range.Text = QCInfo_BAGWeight + "           KG";
        //                newTable2.Cell(13, 1).Range.Text = "退货还包称重时间";
        //                newTable2.Cell(13, 2).Range.Text = "123";
        //                newTable2.Cell(14, 1).Range.Text = "退货还包重量";
        //                newTable2.Cell(14, 2).Range.Text = "123           KG";


        //                ////////--------------------------------------表3------------------------------------------------------------------
        //                WordApp.Selection.MoveDown(ref WdLine, ref TableLenth, ref Nothing);//移动焦点
        //                WordApp.Selection.TypeParagraph();//插入段落

        //                //文档中创建表格
        //                Word.Table newTable3 = WordDoc.Tables.Add(WordApp.Selection.Range, 14, 2, ref Nothing, ref Nothing);


        //                //设置表格样式
        //                newTable3.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable3.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable3.Columns[1].Width = Columns1;
        //                newTable3.Columns[2].Width = Columns2;
        //                newTable3.Range.Font.Size = 5F;
        //                newTable3.Range.Font.Name = "宋体";
        //                newTable3.Range.Font.Bold = 0;
        //                newTable3.Rows.Height = 0;
        //                //填充表格内容
        //                newTable3.Cell(1, 1).Range.Text = "广东理文质检称量单";
        //                newTable3.Cell(1, 1).Range.Bold = 5;//设置单元格中字体为粗体
        //                newTable3.Cell(1, 1).Range.Font.Size = 5F;
        //                ////合并单元格
        //                newTable3.Cell(1, 1).Merge(newTable3.Cell(1, 2));
        //                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
        //                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中   
        //                ////填充表格内容
        //                //newTable3.Cell(2, 1).Range.Text = "产品基本信息";
        //                //newTable3.Cell(2, 1).Range.Font.Color = Word.WdColor.wdColorDarkBlue;//设置单元格内字体颜色
        //                //填充表格内容
        //                newTable3.Cell(2, 1).Range.Text = "检测编号";
        //                newTable3.Cell(2, 2).Range.Text = QcInfo_ID;
        //                newTable3.Cell(3, 1).Range.Text = "车牌号码";
        //                newTable3.Cell(3, 2).Range.Text = CNTR_NO;
        //                newTable3.Cell(4, 1).Range.Text = "供应商";
        //                newTable3.Cell(4, 2).Range.Text = REF_NO;
        //                newTable3.Cell(5, 1).Range.Text = "采购单号";
        //                newTable3.Cell(5, 2).Range.Text = PO_NO;
        //                newTable3.Cell(6, 1).Range.Text = "送货通知书编号";
        //                newTable3.Cell(6, 2).Range.Text = SHIPMENT_NO;
        //                newTable3.Cell(7, 1).Range.Text = "送货件数";
        //                newTable3.Cell(7, 2).Range.Text = NO_OF_BALES;
        //                newTable3.Cell(8, 1).Range.Text = "抽检件数";
        //                newTable3.Cell(8, 2).Range.Text = QCInfo_PumpingPackets;
        //                newTable3.Cell(9, 1).Range.Text = "司称员";
        //                newTable3.Cell(9, 2).Range.Text = "123";
        //                newTable3.Cell(10, 1).Range.Text = "称重类型";
        //                newTable3.Cell(10, 2).Range.Text = "123";
        //                newTable3.Cell(11, 1).Range.Text = "抽检称重时间";
        //                newTable3.Cell(11, 2).Range.Text = "123";
        //                newTable3.Cell(12, 1).Range.Text = "抽检重量";
        //                newTable3.Cell(12, 2).Range.Text = QCInfo_BAGWeight + "           KG";
        //                newTable3.Cell(13, 1).Range.Text = "退货还包称重时间";
        //                newTable3.Cell(13, 2).Range.Text = "123";
        //                newTable3.Cell(14, 1).Range.Text = "退货还包重量";
        //                newTable3.Cell(14, 2).Range.Text = "123           KG";

        //                SaveWord(filename);//保存文件
        //                PrintViewWord(filename);//打印预览

        //            }
        //            catch (Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.InsertText()" + ex.Message.ToString());
        //            }


        //        }
        //        /// <summary>
        //        /// 删除指定文件
        //        /// </summary>
        //        /// <param name="math"></param>
        //        private void DeleteSignedFile(string path)
        //        {
        //            try
        //            {
        //                DirectoryInfo di = new DirectoryInfo(path);
        //                //遍历该路径下的所有文件
        //                foreach (FileInfo fi in di.GetFiles())
        //                {
        //                    File.Delete(path + "\\" + fi.Name);//删除当前文件
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.DeleteSignedFile()" + ex.Message.ToString());
        //            }
        //        }

        //        ///   
        //        /// 换行  
        //        ///   
        //        public void NewLine()
        //        {
        //            //换行  
        //            this.WordApp.Application.Selection.TypeParagraph();
        //        }
        //        ///   
        //        /// 保存文件   
        //        ///   
        //        /// 保存的文件名  
        //        public void SaveWord(object pFileName)
        //        {
        //            if (File.Exists(pFileName.ToString()))
        //                File.Delete(pFileName.ToString());
        //            object myNothing = System.Reflection.Missing.Value;
        //            object myFileName = pFileName;
        //            object myWordFormatDocument = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;
        //            object myLockd = false;
        //            object myPassword = "";
        //            object myAddto = true;
        //            try
        //            {
        //                this.WordDoc.SaveAs(ref myFileName, ref myWordFormatDocument, ref myLockd, ref myPassword, ref myAddto, ref myPassword,
        //                    ref myLockd, ref myLockd, ref myLockd, ref myLockd, ref myNothing, ref myNothing, ref myNothing,
        //                    ref myNothing, ref myNothing, ref myNothing);
        //            }
        //            catch
        //            {
        //                throw new Exception("导出word文档失败!");
        //            }
        //        }

        //        /// <summary>
        //        /// 预览
        //        /// </summary>
        //        /// <param name="fileNmae">文件路径</param>
        //        public void PrintViewWord(object fileNmae)
        //        {
        //            try
        //            {
        //                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
        //                {
        //                    p.Kill();
        //                }
        //                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
        //                object Missing = System.Reflection.Missing.Value;
        //                object readOnly = false;
        //                Microsoft.Office.Interop.Word.Document wordDoc = wordApp.Documents.Open(ref fileNmae, ref Missing, ref readOnly, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing);
        //                wordApp.Visible = true;
        //                wordDoc.PrintPreview();
        //            }
        //            catch (Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.PrintViewWord()" + ex.Message.ToString());
        //            }

        //        }
        //        /// <summary>
        //        /// 打印
        //        /// </summary>
        //        /// <param name="fileName"></param>
        //        public void Print(object fileName)
        //        {
        //            try
        //            {
        //                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
        //                {
        //                    p.Kill();
        //                }
        //                this.WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
        //                Object missing = System.Reflection.Missing.Value;
        //                object redOlny = false;
        //                this.WordDoc = this.WordApp.Documents.Open(ref fileName, ref missing, ref redOlny, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

        //                //------------------------使用Printout方法进行打印------------------------------
        //                object background = false; //这个很重要，否则关闭的时候会提示请等待Word打印完毕后再退出，加上这个后可以使Word所有
        //                WordDoc.PrintOut(ref background, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref

        //missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,

        //    ref missing);
        //                object saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdSaveChanges;
        //                this.WordDoc.Close(ref saveOption, ref missing, ref missing); //关闭当前文档，如果有多个模版文件进行操作，则执行完这一步后接着执行打开Word文档的方法即可
        //                saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
        //                this.WordApp.Quit(ref saveOption, ref missing, ref missing); //关闭Word进程
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("没安装打印机或者打印机出故障");

        //            }

        //        }

    }

    public class PrintWeight
    {
        /// <summary>
        /// 检测编号
        /// </summary>
        public string QcInfo_ID;

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string CNTR_NO;

        /// <summary>
        /// 供应商
        /// </summary>
        public string REF_NO;

        /// <summary>
        /// 采购单号
        /// </summary>
        public string PO_NO;

        /// <summary>
        /// 送货通知书编号
        /// </summary>
        public string SHIPMENT_NO;

        /// <summary>
        /// 送货件数
        /// </summary>
        public string NO_OF_BALES;

        /// <summary>
        /// 抽检件数
        /// </summary>
        public string QCInfo_PumpingPackets;

        /// <summary>
        /// 司称员
        /// </summary>
        public string QCRecord_Name;
        /// <summary>
        /// 称重类型
        /// </summary>

        /// <summary>
        /// 抽检称重时间
        /// </summary>
        public string QCRecord_TIME;

        /// <summary>
        /// 抽检重量
        /// </summary>
        ///
        public string QCInfo_BAGWeight;

        /// <summary>
        /// 杂质重量
        /// </summary>
        ///
        public string QCInfo_MATERIAL_WEIGHT;//杂质重量

        /// <summary>
        /// 退货还包称重时间
        /// </summary>
        /// 
        public string QCInfo_RETURNBAG_TIME;

        /// <summary>
        /// 退货还包重量
        /// </summary>

        public string QCInfo_RETURNBAG_WEIGH;

        /// <summary>
        /// 杂纸重量
        /// </summary>
        public string QCInfo_PAPER_WEIGHT;

        public string WEIGHT_TICKET_NO;
        public string ExtensionField2;
        /// <summary>
        /// 货品
        /// </summary>
        public string PROD_ID;

        string str = "";
        public PrintWeight()
        {
            printFont = new Font("Arial", 50);
            pd = new PrintDocument();
            pd.BeginPrint += new PrintEventHandler(pd_BeginPrint);
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
        }

        //打印编码
        string printCode = "ZB-AA02-02";
        //DbHelperSQL dbs = new DbHelperSQL();
        string printType = "";
        //文本格式
        private Font printFont;
        //---------------------------------------->1
        //打印高度
        //const int LineHeight = 300;
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
        PrintDialog printDialog = new PrintDialog();

        public void PrintDemoFormWeightPrint()
        {
            try
            {
                pd.DefaultPageSettings.Landscape = true; //横向打印
                pd.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

        }

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
            Font title = new Font("Arial", 17);
            Font title1 = new Font("Arial", 13);
            Font title2 = new Font("Arial", 12);
            //绘制直线、曲线
            Pen blackPen = new Pen(Color.Black);
            //填充,绘图
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            //int center = 115;//居中宽度
            int center = 38;//居中宽度
            //int x = 0;//打印页x坐标
            //int y = 70;//打印页y坐标
            int x = 0;//打印页x坐标
            int y = 30;//打印页y坐标
            int y1 = y;//打印页y坐标
            int x1 = 0;//打印页y坐标
            int width = 0;//打印宽度
            int width1 = 20;//打印宽度
            int height = 45;//打印高度
            int smallWidth = 501;
            int smallWidth1 = 501;
            int largeWidth = 200;

            int checkWidth = center + (smallWidth + largeWidth) * 2 + 10;
            int codeWidth = center + (smallWidth + largeWidth) * 2 - 80;
            int checkNameWidth = center + 100;

            int checkheight = 70;//质检说明高度
            int checkSmalWords = 82;
            int checkLargeWords = 82;
            int checkWordsLength = 0;




            x = center;
            //  y = y - 10;
            y = y1;
            str = "重庆理文质检称量单";
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth * 2 / 3, height);
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title, drawBrush, (x * 3) - 30, y + Lines + width1 - 4);
            //--------------2行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "检测编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QcInfo_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------3行----------------

            //--------------1货品----------------------

            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "货品";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PROD_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);



            //----------货品---------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "车牌号码";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + 20);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = CNTR_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------4行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "供应商";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = REF_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------5行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "采购单号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PO_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------6行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货通知书编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = SHIPMENT_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //-------------7行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = NO_OF_BALES;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------8行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PumpingPackets;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------9行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "司称员";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_Name;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            ////--------------10行----------------
            //x = center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------11行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);
            //--------------12行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂质重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_MATERIAL_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------12行----------------


            //--------------杂纸----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂纸重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PAPER_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------杂纸----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_BAGWeight;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            //--------------13行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_RETURNBAG_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);
            //--------------14行----------------
            x = center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);

            str = QCInfo_RETURNBAG_WEIGH;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            x = center;
            y = y + height + 10;
            smallWidth1 = smallWidth / 12;
            str = "第一联：地磅房留存";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines);
            x1 = x + smallWidth * 2 / 3;




            y = y1;
            x = x1 + center;
            //  y = y - 10;
            str = "重庆理文质检称量单";
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth * 2 / 3, height);
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title, drawBrush, x + center + 10, y + Lines + width1 - 4);
            //--------------2行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "检测编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QcInfo_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------3行----------------



            //--------------2货品----------------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "货品";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PROD_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);





            //----------2货品---------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "车牌号码";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = CNTR_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------4行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "供应商";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = REF_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------5行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "采购单号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PO_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------6行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货通知书编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = SHIPMENT_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //-------------7行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = NO_OF_BALES;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------8行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PumpingPackets;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------9行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "司称员";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_Name;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------10行----------------
            //x = x1 + center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------11行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);



            //--------------12行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂质重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_MATERIAL_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------12行----------------


            //--------------杂纸----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂纸重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PAPER_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------杂纸----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_BAGWeight;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            //--------------13行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_RETURNBAG_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);
            //--------------14行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);

            str = QCInfo_RETURNBAG_WEIGH;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            x = x1 + center;
            y = y + height + 10;
            smallWidth1 = smallWidth / 12;
            str = "第二联：质检部留存	";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines);

            x1 = x + smallWidth * 2 / 3;



            y = y1;
            x = x1 + center;
            //  y = y - 10;
            str = "重庆理文质检称量单";
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth * 2 / 3, height);
            ////在矩形框中绘制文本
            e.Graphics.DrawString(str, title, drawBrush, x + center + 10, y + Lines + width1 - 4);
            //--------------2行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "检测编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QcInfo_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------3行----------------



            //--------------3货品----------------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "货品";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PROD_ID;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);





            //----------货品---------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "车牌号码";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = CNTR_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------4行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "供应商";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = REF_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------5行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "采购单号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = PO_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------6行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货通知书编号";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = SHIPMENT_NO;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //-------------7行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "送货件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = NO_OF_BALES;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------8行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检件数";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PumpingPackets;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------9行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "司称员";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_Name;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------10行----------------
            //x = x1 + center;
            //y = y + height;
            //smallWidth1 = smallWidth / 3;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            //////在矩形框中绘制文本
            //str = "称重类型";
            //e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //x = x + smallWidth1;
            ////绘制一个矩形框
            //e.Graphics.DrawRectangle(blackPen, x, y + Lines ,smallWidth1, height);
            ////str = "检测编号";
            ////e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            //--------------11行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCRecord_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);

            //--------------12行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂质重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_MATERIAL_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------12行----------------


            //--------------杂纸----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "杂纸重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_PAPER_WEIGHT;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);


            //--------------杂纸----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "抽检重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_BAGWeight;
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            //--------------13行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包称重时间";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            str = QCInfo_RETURNBAG_TIME;
            e.Graphics.DrawString(str, title2, drawBrush, x + 3, y + Lines + width1);
            //--------------14行----------------
            x = x1 + center;
            y = y + height;
            smallWidth1 = smallWidth / 3;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);
            ////在矩形框中绘制文本
            str = "退货还包重量";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            x = x + smallWidth1;
            //绘制一个矩形框
            e.Graphics.DrawRectangle(blackPen, x, y + Lines, smallWidth1, height);

            str = QCInfo_RETURNBAG_WEIGH;

            e.Graphics.DrawString(str, title1, drawBrush, x + 3, y + Lines + width1);
            str = "KG";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1 / 3 + 40, y + Lines + width1);

            x = x1 + center;
            y = y + height + 10;
            smallWidth1 = smallWidth / 12;
            str = "第三联：供应商留存	";
            e.Graphics.DrawString(str, title1, drawBrush, x + 3 + smallWidth1, y + Lines);
            ////-----------------------------表1-------------------------------------------------------

        }
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="type">打印/打印预览</param>
        public void GetPrintData()
        {
            pd.DefaultPageSettings.Landscape = true; //横向打印
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
                MessageBox.Show("请确认打印机是否已连接！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        //        /// word 应用对象  
        //        ///   
        //        private Microsoft.Office.Interop.Word._Application WordApp;
        //        ///   
        //        /// word 文件对象  
        //        ///   
        //        private Microsoft.Office.Interop.Word._Document WordDoc;
        //        public void CreateAWord()
        //        {
        //            try
        //            {
        //                //实例化word应用对象  
        //                this.WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
        //                object Nothing = System.Reflection.Missing.Value;
        //                this.WordDoc = this.WordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);


        //            //     object oMissing = System.Reflection.Missing.Value;
        //            //Word._Application oWord = null;
        //            //Word._Document oDoc;
        //            //oWord = new Word.Application();
        //            //oWord.Visible = true;
        //            //oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

        //                InsertText();
        //            }
        //            catch(Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.CreateAWord()" + ex.Message.ToString());
        //            }
        //        }
        //        ///   
        //        /// 添加页眉  
        //        ///   
        //        ///   
        //        public void SetPageHeader(string pPageHeader)
        //        {
        //            try
        //            {
        //                //.wdOutlineView,wdSeekPrimaryHeader;
        //                //添加页眉  
        //                this.WordApp.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdOutlineView;
        //                this.WordApp.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekPrimaryHeader;
        //                //this.WordApp.ActiveWindow.ActivePane.Selection.InsertAfter(pPageHeader);
        //                //设置中间对齐  
        //                this.WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
        //                //跳出页眉设置  
        //                this.WordApp.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;
        //                //WordApp.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsMessageBox;
        //            }
        //            catch (Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.SetPageHeader()" + ex.Message.ToString());
        //            }

        //        }
        //        ///   
        //        /// 插入文字  
        //        ///   
        //        /// 文本信息  
        //        /// 字体打小  
        //        /// 字体颜色  
        //        /// 字体粗体  
        //        /// 方向  
        //        public void InsertText()
        //        {
        //            string name = "";
        //            try
        //            {
        //                object TableLenth = 14;
        //                float Columns1 = 150f;//列长度
        //                float Columns2 = 280f;//列长度
        //                Object Nothing = System.Reflection.Missing.Value;
        //                name = DateTime.Now.ToString("yyyy-mm-dd") + ".doc";
        //                //Directory.CreateDirectory(Application.StartupPath +@"\"+ name);  //创建文件所在目录
        //                object filename = Application.StartupPath + @"\PrintDemoFormWeight\" + name;  //文件保存路径
        //                WordApp.Selection.ParagraphFormat.LineSpacing = 1;//设置文档的行间距

        //                //object start = 0;
        //                //object end = 0;
        //                //Word.Range tableLocation = WordDoc.Range(ref start, ref end);
        //                //WordDoc.Tables.Add(tableLocation, 3, 4, ref Nothing, ref Nothing);
        //                //--------------------------------------表1------------------------------------------------------------------
        //                //移动焦点并换行
        //                object count = 6;
        //                object WdLine = Word.WdUnits.wdLine;//换一行;
        //                //WordApp.Selection.MoveDown(ref Nothing, ref Nothing, ref Nothing);//移动焦点
        //                //WordApp.Selection.TypeParagraph();//插入段落
        //                //文档中创建表格
        //                Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, 2, 2, ref Nothing, ref Nothing);
        //                //设置表格样式
        //                newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable.Columns[1].Width = Columns1;
        //                newTable.Columns[2].Width = Columns2;
        //                newTable.Rows[1].Height = 1;
        //                newTable.Rows[2].Height = 1;
        //                newTable.Range.Font.Size = 5F;
        //                newTable.Range.Font.Name = "宋体";
        //                newTable.Range.Font.Bold = 0;
        //                newTable.Rows.Height = 0;
        //                //填充表格内容
        //                newTable.Cell(1, 1).Range.Text = "广东理文质检称量单";
        //                newTable.Cell(1, 1).Range.Bold = 5;//设置单元格中字体为粗体
        //                newTable.Cell(1, 1).Range.Font.Size = 5F;
        //                ////合并单元格
        //                newTable.Cell(1, 1).Merge(newTable.Cell(1, 2));
        //                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
        //                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中   
        //                //填充表格内容
        //                newTable.Cell(2, 1).Range.Text = "检测编号";
        //                newTable.Cell(2, 2).Range.Text = QcInfo_ID;
        //                newTable.Cell(3, 1).Range.Text = "车牌号码";
        //                newTable.Cell(3, 2).Range.Text = CNTR_NO;
        //                newTable.Cell(4, 1).Range.Text = "供应商";
        //                newTable.Cell(4, 2).Range.Text = REF_NO;
        //                newTable.Cell(5, 1).Range.Text = "采购单号";
        //                newTable.Cell(5, 2).Range.Text = PO_NO;
        //                newTable.Cell(6, 1).Range.Text = "送货通知书编号";
        //                newTable.Cell(6, 2).Range.Text = SHIPMENT_NO;
        //                newTable.Cell(7, 1).Range.Text = "送货件数";
        //                newTable.Cell(7, 2).Range.Text = NO_OF_BALES;
        //                newTable.Cell(8, 1).Range.Text = "抽检件数";
        //                newTable.Cell(8, 2).Range.Text = QCInfo_PumpingPackets;
        //                newTable.Cell(9, 1).Range.Text = "司称员";
        //                newTable.Cell(9, 2).Range.Text = "123";
        //                newTable.Cell(10, 1).Range.Text = "称重类型";
        //                newTable.Cell(10, 2).Range.Text = "123";
        //                newTable.Cell(11, 1).Range.Text = "抽检称重时间";
        //                newTable.Cell(11, 2).Range.Text = "123";
        //                newTable.Cell(12, 1).Range.Text = "抽检重量";
        //                newTable.Cell(12, 2).Range.Text = QCInfo_BAGWeight + "           KG";
        //                newTable.Cell(13, 1).Range.Text = "退货还包称重时间";
        //                newTable.Cell(13, 2).Range.Text = "123";
        //                newTable.Cell(14, 1).Range.Text = "退货还包重量";
        //                newTable.Cell(14, 2).Range.Text = "123           KG";

        //                //////--------------------------------------表2------------------------------------------------------------------
        //                WordApp.Selection.MoveDown(ref WdLine, ref TableLenth, ref Nothing);//移动焦点
        //                WordApp.Selection.TypeParagraph();//插入段落

        //                //文档中创建表格
        //                Word.Table newTable2 = WordDoc.Tables.Add(WordApp.Selection.Range, 14, 2, ref Nothing, ref Nothing);
        //                //设置表格样式
        //                newTable2.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable2.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable2.Columns[1].Width = Columns1;
        //                newTable2.Columns[2].Width = Columns2;
        //                newTable2.Range.Font.Size = 5F;
        //                newTable2.Range.Font.Name = "宋体";
        //                newTable2.Range.Font.Bold = 0;
        //                newTable2.Rows.Height = 0;
        //                //填充表格内容
        //                newTable2.Cell(1, 1).Range.Text = "广东理文质检称量单";
        //                newTable2.Cell(1, 1).Range.Bold = 5;//设置单元格中字体为粗体
        //                newTable2.Cell(1, 1).Range.Font.Size = 5F;
        //                ////合并单元格
        //                newTable2.Cell(1, 1).Merge(newTable2.Cell(1, 2));
        //                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
        //                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中   
        //                //填充表格内容
        //                newTable2.Cell(2, 1).Range.Text = "检测编号";
        //                newTable2.Cell(2, 2).Range.Text = QcInfo_ID;
        //                newTable2.Cell(3, 1).Range.Text = "车牌号码";
        //                newTable2.Cell(3, 2).Range.Text = CNTR_NO;
        //                newTable2.Cell(4, 1).Range.Text = "供应商";
        //                newTable2.Cell(4, 2).Range.Text = REF_NO;
        //                newTable2.Cell(5, 1).Range.Text = "采购单号";
        //                newTable2.Cell(5, 2).Range.Text = PO_NO;
        //                newTable2.Cell(6, 1).Range.Text = "送货通知书编号";
        //                newTable2.Cell(6, 2).Range.Text = SHIPMENT_NO;
        //                newTable2.Cell(7, 1).Range.Text = "送货件数";
        //                newTable2.Cell(7, 2).Range.Text = NO_OF_BALES;
        //                newTable2.Cell(8, 1).Range.Text = "抽检件数";
        //                newTable2.Cell(8, 2).Range.Text = QCInfo_PumpingPackets;
        //                newTable2.Cell(9, 1).Range.Text = "司称员";
        //                newTable2.Cell(9, 2).Range.Text = "123";
        //                newTable2.Cell(10, 1).Range.Text = "称重类型";
        //                newTable2.Cell(10, 2).Range.Text = "123";
        //                newTable2.Cell(11, 1).Range.Text = "抽检称重时间";
        //                newTable2.Cell(11, 2).Range.Text = "123";
        //                newTable2.Cell(12, 1).Range.Text = "抽检重量";
        //                newTable2.Cell(12, 2).Range.Text = QCInfo_BAGWeight + "           KG";
        //                newTable2.Cell(13, 1).Range.Text = "退货还包称重时间";
        //                newTable2.Cell(13, 2).Range.Text = "123";
        //                newTable2.Cell(14, 1).Range.Text = "退货还包重量";
        //                newTable2.Cell(14, 2).Range.Text = "123           KG";


        //                ////////--------------------------------------表3------------------------------------------------------------------
        //                WordApp.Selection.MoveDown(ref WdLine, ref TableLenth, ref Nothing);//移动焦点
        //                WordApp.Selection.TypeParagraph();//插入段落

        //                //文档中创建表格
        //                Word.Table newTable3 = WordDoc.Tables.Add(WordApp.Selection.Range, 14, 2, ref Nothing, ref Nothing);


        //                //设置表格样式
        //                newTable3.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable3.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
        //                newTable3.Columns[1].Width = Columns1;
        //                newTable3.Columns[2].Width = Columns2;
        //                newTable3.Range.Font.Size = 5F;
        //                newTable3.Range.Font.Name = "宋体";
        //                newTable3.Range.Font.Bold = 0;
        //                newTable3.Rows.Height = 0;
        //                //填充表格内容
        //                newTable3.Cell(1, 1).Range.Text = "广东理文质检称量单";
        //                newTable3.Cell(1, 1).Range.Bold = 5;//设置单元格中字体为粗体
        //                newTable3.Cell(1, 1).Range.Font.Size = 5F;
        //                ////合并单元格
        //                newTable3.Cell(1, 1).Merge(newTable3.Cell(1, 2));
        //                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
        //                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中   
        //                ////填充表格内容
        //                //newTable3.Cell(2, 1).Range.Text = "产品基本信息";
        //                //newTable3.Cell(2, 1).Range.Font.Color = Word.WdColor.wdColorDarkBlue;//设置单元格内字体颜色
        //                //填充表格内容
        //                newTable3.Cell(2, 1).Range.Text = "检测编号";
        //                newTable3.Cell(2, 2).Range.Text = QcInfo_ID;
        //                newTable3.Cell(3, 1).Range.Text = "车牌号码";
        //                newTable3.Cell(3, 2).Range.Text = CNTR_NO;
        //                newTable3.Cell(4, 1).Range.Text = "供应商";
        //                newTable3.Cell(4, 2).Range.Text = REF_NO;
        //                newTable3.Cell(5, 1).Range.Text = "采购单号";
        //                newTable3.Cell(5, 2).Range.Text = PO_NO;
        //                newTable3.Cell(6, 1).Range.Text = "送货通知书编号";
        //                newTable3.Cell(6, 2).Range.Text = SHIPMENT_NO;
        //                newTable3.Cell(7, 1).Range.Text = "送货件数";
        //                newTable3.Cell(7, 2).Range.Text = NO_OF_BALES;
        //                newTable3.Cell(8, 1).Range.Text = "抽检件数";
        //                newTable3.Cell(8, 2).Range.Text = QCInfo_PumpingPackets;
        //                newTable3.Cell(9, 1).Range.Text = "司称员";
        //                newTable3.Cell(9, 2).Range.Text = "123";
        //                newTable3.Cell(10, 1).Range.Text = "称重类型";
        //                newTable3.Cell(10, 2).Range.Text = "123";
        //                newTable3.Cell(11, 1).Range.Text = "抽检称重时间";
        //                newTable3.Cell(11, 2).Range.Text = "123";
        //                newTable3.Cell(12, 1).Range.Text = "抽检重量";
        //                newTable3.Cell(12, 2).Range.Text = QCInfo_BAGWeight + "           KG";
        //                newTable3.Cell(13, 1).Range.Text = "退货还包称重时间";
        //                newTable3.Cell(13, 2).Range.Text = "123";
        //                newTable3.Cell(14, 1).Range.Text = "退货还包重量";
        //                newTable3.Cell(14, 2).Range.Text = "123           KG";

        //                SaveWord(filename);//保存文件
        //                PrintViewWord(filename);//打印预览

        //            }
        //            catch (Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.InsertText()" + ex.Message.ToString());
        //            }


        //        }
        //        /// <summary>
        //        /// 删除指定文件
        //        /// </summary>
        //        /// <param name="math"></param>
        //        private void DeleteSignedFile(string path)
        //        {
        //            try
        //            {
        //                DirectoryInfo di = new DirectoryInfo(path);
        //                //遍历该路径下的所有文件
        //                foreach (FileInfo fi in di.GetFiles())
        //                {
        //                    File.Delete(path + "\\" + fi.Name);//删除当前文件
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.DeleteSignedFile()" + ex.Message.ToString());
        //            }
        //        }

        //        ///   
        //        /// 换行  
        //        ///   
        //        public void NewLine()
        //        {
        //            //换行  
        //            this.WordApp.Application.Selection.TypeParagraph();
        //        }
        //        ///   
        //        /// 保存文件   
        //        ///   
        //        /// 保存的文件名  
        //        public void SaveWord(object pFileName)
        //        {
        //            if (File.Exists(pFileName.ToString()))
        //                File.Delete(pFileName.ToString());
        //            object myNothing = System.Reflection.Missing.Value;
        //            object myFileName = pFileName;
        //            object myWordFormatDocument = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;
        //            object myLockd = false;
        //            object myPassword = "";
        //            object myAddto = true;
        //            try
        //            {
        //                this.WordDoc.SaveAs(ref myFileName, ref myWordFormatDocument, ref myLockd, ref myPassword, ref myAddto, ref myPassword,
        //                    ref myLockd, ref myLockd, ref myLockd, ref myLockd, ref myNothing, ref myNothing, ref myNothing,
        //                    ref myNothing, ref myNothing, ref myNothing);
        //            }
        //            catch
        //            {
        //                throw new Exception("导出word文档失败!");
        //            }
        //        }

        //        /// <summary>
        //        /// 预览
        //        /// </summary>
        //        /// <param name="fileNmae">文件路径</param>
        //        public void PrintViewWord(object fileNmae)
        //        {
        //            try
        //            {
        //                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
        //                {
        //                    p.Kill();
        //                }
        //                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
        //                object Missing = System.Reflection.Missing.Value;
        //                object readOnly = false;
        //                Microsoft.Office.Interop.Word.Document wordDoc = wordApp.Documents.Open(ref fileNmae, ref Missing, ref readOnly, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing);
        //                wordApp.Visible = true;
        //                wordDoc.PrintPreview();
        //            }
        //            catch (Exception ex)
        //            {
        //                Common.WriteTextLog("PrintDemoFormWeight.PrintViewWord()" + ex.Message.ToString());
        //            }

        //        }
        //        /// <summary>
        //        /// 打印
        //        /// </summary>
        //        /// <param name="fileName"></param>
        //        public void Print(object fileName)
        //        {
        //            try
        //            {
        //                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
        //                {
        //                    p.Kill();
        //                }
        //                this.WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
        //                Object missing = System.Reflection.Missing.Value;
        //                object redOlny = false;
        //                this.WordDoc = this.WordApp.Documents.Open(ref fileName, ref missing, ref redOlny, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

        //                //------------------------使用Printout方法进行打印------------------------------
        //                object background = false; //这个很重要，否则关闭的时候会提示请等待Word打印完毕后再退出，加上这个后可以使Word所有
        //                WordDoc.PrintOut(ref background, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref

        //missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,

        //    ref missing);
        //                object saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdSaveChanges;
        //                this.WordDoc.Close(ref saveOption, ref missing, ref missing); //关闭当前文档，如果有多个模版文件进行操作，则执行完这一步后接着执行打开Word文档的方法即可
        //                saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
        //                this.WordApp.Quit(ref saveOption, ref missing, ref missing); //关闭Word进程
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("没安装打印机或者打印机出故障");

        //            }

        //        }






    }
}
