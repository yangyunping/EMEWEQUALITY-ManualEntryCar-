using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EMEWEEntity;
using EMEWEDAL;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class PicForm : Form
    {
        public static int QCRecord_ID = 0;//编号
        public PicForm(int Record_ID)
        {
            QCRecord_ID = Record_ID;
            InitializeComponent();
        }

        public PicForm()
        {
            InitializeComponent();
        }

        
        private void PicForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback(a), null);

            DataTable dt = LinQBaseDao.Query(" select * from View_QCRecordInfo where QCRecord_ID=" + QCRecord_ID).Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblCNTR_NO.Text = dt.Rows[0]["CNTR_NO"].ToString();
                lblDictionary_Name.Text = dt.Rows[0]["Dictionary_Name"].ToString();
                lblQCInfo_BAGWeight.Text = dt.Rows[0]["QCInfo_BAGWeight"].ToString();
                lblQCInfo_MATERIAL_SCALE.Text = dt.Rows[0]["QCInfo_MATERIAL_SCALE"].ToString();
                lblQCInfo_MATERIAL_WEIGHT.Text = dt.Rows[0]["QCInfo_MATERIAL_WEIGHT"].ToString();
                lblQCInfo_PAPER_SCALE.Text = dt.Rows[0]["QCInfo_PAPER_SCALE"].ToString();
                lblQCInfo_PAPER_WEIGHT.Text = dt.Rows[0]["QCInfo_PAPER_WEIGHT"].ToString();
                lblQCInfo_PumpingPackets.Text = dt.Rows[0]["QCInfo_PumpingPackets"].ToString();
                lblQCRecord_DRAW.Text = dt.Rows[0]["QCRecord_DRAW"].ToString();
                lblQCRecord_ID.Text = dt.Rows[0]["QCRecord_ID"].ToString();
                lblQCRecord_RESULT.Text = dt.Rows[0]["QCRecord_RESULT"].ToString();
                lblQCRecord_TARE.Text = dt.Rows[0]["QCRecord_TARE"].ToString();
                lblQCRecord_TIME.Text = dt.Rows[0]["QCRecord_TIME"].ToString();
                lblTestItems_NAME.Text = dt.Rows[0]["TestItems_NAME"].ToString();
                lblUserName.Text = dt.Rows[0]["UserName"].ToString();
                lblWEIGHT_TICKET_NO.Text = dt.Rows[0]["WEIGHT_TICKET_NO"].ToString();
            }

        }


        public delegate void dAdd();
        public dAdd myAdd;
        public void a(object obj)
        {
            myAdd = new dAdd(ShowImage);
            gbShowImage.Invoke(myAdd);
        }
        /// <summary>
        /// 显示图片
        /// </summary>
        private void ShowImage()
        {
            string sql = "select * from ImageRecord where ImageRecord_QCRecord_ID="+QCRecord_ID;
            List<ImageRecord> list = new List<ImageRecord>();
            list = LinQBaseDao.GetItemsForListing<ImageRecord>(sql).ToList();
            if (list.Count >= 1)
            {
                //得到图片的路径
                string path = Common.SaveFiel;
                ////获取文件夹下的图片信息
                //List<string> strList = ImageFile.GetImage(path, false);
                //将得到的图片信息绑定到页面
                int listcount = list.Count();
                double wight = 0, height = 0;
                int sss = 0, mmm = 0;
                double doucount = Math.Sqrt(listcount);
                if (Convert.ToInt32(doucount) == doucount)
                {
                    wight = (480 - 10 * (doucount + 1)) / Convert.ToInt32(doucount);
                    sss = Convert.ToInt32(doucount);
                }
                else
                {
                    if (doucount - (int)(doucount) == 0)
                    {
                        sss = Convert.ToInt32(doucount);
                    }
                    else
                    {
                        sss = (int)(doucount + 1);
                    }
                    wight = (480 - 10 * (sss + 2)) / Convert.ToInt32(sss);

                }
                height = (430 - 10 * (doucount + 1)) / Convert.ToInt32(doucount);
                mmm = Math.Abs((int)(listcount / sss) - ((double)listcount / (double)sss)) == 0 ? (listcount / sss) : (int)(((double)listcount / (double)sss)) + 1;
                int x = 0;
                int y = 0;
                gbShowImage.Controls.Clear();
                int k = 0;
                for (int i = 0; i < mmm; i++)
                {
                    y = Convert.ToInt32(10 * (i + 1) + i * height) + 10;
                    for (int m = 0; m < sss; m++)
                    {
                        if (k >= listcount)
                        {
                            return;
                        }
                        x = Convert.ToInt32(10 * (m + 1) + m * wight) + 5;
                        PictureBox pb = new PictureBox();
                        pb.Location = new Point(x, y);
                        pb.Width = Convert.ToInt32(wight);
                        pb.Height = Convert.ToInt32(height);
                        pb.SizeMode = pictureBox1.SizeMode;
                        pb.MouseHover += new System.EventHandler(this.pbInImageOne_MouseHover);
                        pb.MouseLeave += new System.EventHandler(this.pbInImageOne_MouseLeave);
                        pb.Name = "pictureBox" + list[k].ImageRecord_Id.ToString();
                        pb.Tag = path + list[k].ImageRecord_ImageName;
                        pb.ImageLocation = path + list[k].ImageRecord_ImageName;
                        this.gbShowImage.Controls.Add(pb);
                        k++;
                    }
                }
            }
        }
        #region 图片放大缩小事件
        /// <summary>
        /// 鼠标离开控件可见部分时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbInImageOne_MouseLeave(object sender, EventArgs e)
        {
            ShowD();
        }
        /// <summary>
        /// 鼠标悬停一定时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbInImageOne_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    PictureBox pic = sender as PictureBox;
                    if (pic.Tag != null)
                    {
                        ShowD(pic.Tag.ToString());
                    }
                }

            }
            catch
            {
                Common.WriteTextLog("pbInImageOne_MouseHover()");
            }
        }
        public Bitmap b = null;
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="FileName"></param>
        public void ShowD(string FileName)
        {
            groupBox7.BringToFront();
            b = new Bitmap(FileName);
            pictureBox1.Image = b;
            groupBox7.Visible = true;

        }
        /// <summary>
        /// 隐藏图片
        /// </summary>
        /// <param name="pb"></param>
        public void ShowD()
        {
            try
            {
                groupBox7.Visible = false;
                b.Dispose();
                //移至底层
                groupBox7.SendToBack();
            }
            catch
            {
                // CommonalityEntityOne.WriteTextLog("ShowD()");
            }
        }
        #endregion
    }
}
