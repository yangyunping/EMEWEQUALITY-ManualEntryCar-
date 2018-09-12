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
using System.Threading;
using System.Threading.Tasks;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class ImageForm : Form
    {
        public ImageForm(string str)
        {
            InitializeComponent();
            qcinfoid = str;
        }
        public ImageForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 质检编号
        /// </summary>
        string qcinfoid = "";
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            BindData();
            if (IsShowImage(Common.OrganizationID))
                ShowImage();
        }

        private async void BindData()
        {
            await GetWeightData();
        }

        private delegate void AsynUpdateUI();

        private async Task GetWeightData()
        {
            await Task.Factory.StartNew(() =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AsynUpdateUI(delegate ()
                    {
                        DataTable dt = LinQBaseDao.Query(" select * from View_QCInfo where QCInfo_ID=" + qcinfoid).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            lblQCRecord_ID.Text = dt.Rows[0]["QCInfo_ID"].ToString();
                            lblCNTR_NO.Text = dt.Rows[0]["CNTR_NO"].ToString();
                            lblDictionary_Name.Text = dt.Rows[0]["Dictionary_Name"].ToString();
                            lblQCInfo_BAGWeight.Text = dt.Rows[0]["QCInfo_BAGWeight"].ToString();
                            lblQCInfo_MATERIAL_SCALE.Text = dt.Rows[0]["QCInfo_MATERIAL_SCALE"].ToString();
                            lblQCInfo_MATERIAL_WEIGHT.Text = dt.Rows[0]["QCInfo_MATERIAL_WEIGHT"].ToString();
                            lblQCInfo_PAPER_SCALE.Text = dt.Rows[0]["QCInfo_PAPER_SCALE"].ToString();
                            lblQCInfo_PAPER_WEIGHT.Text = dt.Rows[0]["QCInfo_PAPER_WEIGHT"].ToString();
                            lblQCInfo_PumpingPackets.Text = dt.Rows[0]["QCInfo_PumpingPackets"].ToString();
                            lbQCInfo_RECV_RMA_METHOD.Text = dt.Rows[0]["QCInfo_RECV_RMA_METHOD"].ToString();
                            lbwate.Text = dt.Rows[0]["QCInfo_MOIST_PER_SAMPLE"].ToString();
                            lblUserName.Text = dt.Rows[0]["UserName"].ToString();
                            lblWEIGHT_TICKET_NO.Text = dt.Rows[0]["WEIGHT_TICKET_NO"].ToString();
                        }

                    }));
                }
            });
        }

        private async void BindData(string QCRecord_ID)
        {
            await GetWeightDatatwo(QCRecord_ID);
        }

        private async Task GetWeightDatatwo(string QCRecord_ID)
        {
            await Task.Factory.StartNew(() =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AsynUpdateUI(delegate ()
                    {
                        DataTable dt = LinQBaseDao.Query(" select QCRecord_ID, QCRecord_DRAW,QCRecord_RESULT,QCRecord_TARE,QCRecord_TIME,TestItems_NAME,UserName from View_QCRecordInfo where QCRecord_ID=" + QCRecord_ID).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            lblQCRecord_DRAW.Text = dt.Rows[0]["QCRecord_DRAW"].ToString();
                            lbrest.Text = dt.Rows[0]["QCRecord_RESULT"].ToString();
                            lblQCRecord_TARE.Text = dt.Rows[0]["QCRecord_TARE"].ToString();
                            lblQCRecord_TIME.Text = dt.Rows[0]["QCRecord_TIME"].ToString();
                            lblTestItems_NAME.Text = dt.Rows[0]["TestItems_NAME"].ToString();
                            lblUserName.Text = dt.Rows[0]["UserName"].ToString();
                        }

                    }));
                }
            });
        }

        private bool IsShowImage(string organizationID)
        {
            if (Common.OrganizationID == "JiangXiPaper")
                return false;
            return true;
        }

        /// <summary>
        /// 显示图片
        /// </summary>
        private async void ShowImage()
        {
            await GetWeightDataThree();
        }

        private async Task GetWeightDataThree()
        {
            await Task.Factory.StartNew(() =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AsynUpdateUI(delegate ()
                    {
                        string sql = "select * from dbo.ImageRecord where ImageRecord_QCRecord_ID in(select QCRecord_ID from QCRecord where QCRecord_QCInfo_ID=" + qcinfoid + ")";
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
                                    pb.Name = "pictureBox" + list[k].ImageRecord_QCRecord_ID.ToString();
                                    pb.Tag = path + list[k].ImageRecord_ImageName;
                                    pb.ImageLocation = path + list[k].ImageRecord_ImageName;
                                    this.gbShowImage.Controls.Add(pb);
                                    k++;
                                }
                            }
                        }

                    }));
                }
            });
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
                        ShowD(pic.Tag.ToString(), pic.Name);
                        string str = pic.Name.Replace("pictureBox", "");
                        BindData(str);
                    }
                }
            }
            catch
            {
            }
        }
        public Bitmap b = null;
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="FileName"></param>
        public void ShowD(string FileName, string name)
        {
            groupBox7.BringToFront();
            b = new Bitmap(FileName);
            pictureBox1.Image = b;
            groupBox7.Visible = true;
            string qid = name.Replace("pictureBox", "");
            BindData(qid);
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
