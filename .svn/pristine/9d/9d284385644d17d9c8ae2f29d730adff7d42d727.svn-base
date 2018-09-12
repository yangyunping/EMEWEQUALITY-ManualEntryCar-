using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(PrintForm_Paints);
        }

        //画线
        void PrintForm_Paints(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 2);
            Point point1;
            Point point2;
            int x = 10;
            int y = 10;
            int w = 300;
            int h = 600;
            int num = 300;
            int h1 = 70;
            int x1 = 100;
            int w1 = 450;
            int y1 = 70;
            int x3 = 55;
            int w3 = 450;
            int h3 = 475;
            int count = 25;
            for (int i = 0; i < 3; i++)
            {
                int h2 = 100;
                if (i > 0)
                {
                    x = x + num;
                    w = w + num;
                    x1 += num;
                    x3 += num;
                }
                point1 = new Point(x, y);
                point2 = new Point(x, h);
                e.Graphics.DrawLine(blackPen, point1, point2);//竖线
                point1 = new Point(x, y);
                point2 = new Point(w, y);
                e.Graphics.DrawLine(blackPen, point1, point2);//横线
                point1 = new Point(w, y);
                point2 = new Point(w, h);
                e.Graphics.DrawLine(blackPen, point1, point2);//竖线
                point1 = new Point(x, h);
                point2 = new Point(w, h);
                e.Graphics.DrawLine(blackPen, point1, point2);//横线

                point1 = new Point(x, h1);
                point2 = new Point(w, h1);
                e.Graphics.DrawLine(blackPen, point1, point2);//竖线
                for (int s = 0; s < 17; s++)
                {
                    if (s > 0)
                    {
                        h2 = h2 + count;
                    }
                    point1 = new Point(x, h2);
                    point2 = new Point(w, h2);
                    e.Graphics.DrawLine(blackPen, point1, point2);//横线
                }
                point1 = new Point(x1, y1);
                point2 = new Point(x1, w1 - count);
                e.Graphics.DrawLine(blackPen, point1, point2);//竖线
                point1 = new Point(x3, w3 - count);
                point2 = new Point(x3, h3 - count);
                e.Graphics.DrawLine(blackPen, point1, point2);//竖线
                point1 = new Point(x3 + count * 4, w3 - count);
                point2 = new Point(x3 + count * 4, h3 - count);
                e.Graphics.DrawLine(blackPen, point1, point2);//竖线
                point1 = new Point(x3 + count * 6, w3 - count * 2);
                point2 = new Point(x3 + count * 6, h3 - count);
                e.Graphics.DrawLine(blackPen, point1, point2);//竖线
                point1 = new Point(x, h3 + count * 3);
                point2 = new Point(w, h3 + count * 3);
                e.Graphics.DrawLine(blackPen, point1, point2);//横线
                point1 = new Point(x + count * 6, h3 + count);
                point2 = new Point(x + count * 6, h);
                e.Graphics.DrawLine(blackPen, point1, point2);//竖线
            }
            LogBingContents(e);

        }

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

        /// <summary>
        /// 填写内容
        /// </summary>
        /// <param name="e"></param>
        private void LogBingContents(PaintEventArgs e)
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
                e.Graphics.DrawString(QCInfo_BAGWeight + " KG", captionFont, brush, x + 10, y + 405, StringFormat.GenericDefault);
                //e.Graphics.DrawString(" 空 重 ", captionFont, brush, x + 150, y + 428, StringFormat.GenericDefault);
                //e.Graphics.DrawString(CarRecord_Tare, captionFont, brush, x + 150, y + 405, StringFormat.GenericDefault);
                e.Graphics.DrawString(" 杂 质 ", captionFont, brush, x - 30, y + 430, StringFormat.GenericDefault);
                e.Graphics.DrawString(QCInfo_MATERIAL_WEIGHT + "KG", captionFont, brush, x + 20, y + 430, StringFormat.GenericDefault);
                e.Graphics.DrawString(" 杂 纸 ", captionFont, brush, x + 100, y + 430, StringFormat.GenericDefault);
                e.Graphics.DrawString(QCInfo_PAPER_WEIGHT + "KG", captionFont, brush, x + 150, y + 430, StringFormat.GenericDefault);
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
}
