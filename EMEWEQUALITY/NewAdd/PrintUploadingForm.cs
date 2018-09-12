using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Management;
using System.Threading;
using EMEWE.CarManagement.Commons.CommonClass;
using System.Configuration;
using System.Data.Linq.SqlClient;
using EMEWEDAL;
using EMEWEEntity;

namespace EMEWEQUALITY.NewAdd
{
    public partial class PrintUploadingForm : Form
    {
        public PrintUploadingForm()
        {
            InitializeComponent();
        }
        public PrintUploadingForm(int id)
        {

            InitializeComponent();
            UnusualId = id;
        }

        int UnusualId = 1;
        /// <summary>
        /// 服务起路径
        /// </summary>
        private static string FWpath = ConfigurationManager.AppSettings["FWpath"];
        /// <summary>
        /// 图片横坐标
        /// </summary>
        int cx = 15;
        /// <summary>
        /// 图片纵坐标
        /// </summary>
        int cy = 25;
        int index = 1;
        int demo = 0;
        List<string> ListPath = new List<string>();
        /// <summary>
        /// 单击浏览地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnpath_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                txtpath.Text = opf.FileName;
           
            //点击浏览后要展示图片显示
            try
            {
                if (index > 8)
                {
                    if (index == 9)
                    {
                        cx = 15;
                        cy = 110;
                    }
                    if (index > 16)
                    {
                        MessageBox.Show("请先上传后才浏览！");
                        return;
                    }
                }
                PictureBox pb = new PictureBox();
                pb.Location = new Point(cx, cy);
                pb.Size = new Size(110, 78);
                pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pb.ImageLocation = txtpath.Text;
                pb.Name = "pictureBox";
                pb.Image = Image.FromFile(txtpath.Text);
                pb.Visible = true;
                groupBox3.Controls.Add(pb);
                ListPath.Add(txtpath.Text);//存入地址
                cx += pb.Width + 10;
                index++;
                demo += 1;
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show("请选中图片路径,错误" + ex.Message);
                return;
            }
            }

        }
        /// <summary>
        /// 单击上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnuploading_Click(object sender, EventArgs e)
        {
            try
            {
                bool bo = false;
                //得到图片的路径
                string path = txtpath.Text.Trim();
                if (path.Trim() == "")
                {
                    MessageBox.Show("请选择图片地址");
                    return;
                }
                for (int i = 0; i < ListPath.Count; i++)
                {
                    string num = FWpath + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                    string picName = ImageFile.UpLoadFile(ListPath[i], num);//上传图片到指定路径
                    //添加上传记录
                    //insert into ImageRecord values(之间编号,'xx/aa.jpg','2013-05-06','啊啊啊啊啊')

                    if (picName != "")
                    {
                        ImageRecord img = new ImageRecord();
                        img.ImageRecord_Unusual_Id = UnusualId;//改为用异常编号关联
                        img.ImageRecord_ImageName = picName;
                        img.ImageRecord_Time = DateTime.Now;
                        img.ImageRecord_Remark = null;
                        if (!ImageRecordDAL.InsertOneQCRecord(img))
                        {
                            bo = false;
                            MessageBox.Show("上传失败");
                            return;
                        }
                        else
                        {
                            bo = true;
                        }
                    }

                }
                if (bo)
                {
                    MessageBox.Show("上传成功！");
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传时发生错误！错误原因是" + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //释放groupBox3控件上的控件
            Dispose();
        }

        private void Dispose()
        {
            groupBox3.Controls.Clear();
            txtpath.Text= "";
            ListPath.Clear();
            cx = 15;
            cy = 25;
            index = 1;
            demo = 0;
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintUploadingForm_Load(object sender, EventArgs e)
        {
            Loging();
        }
        /// <summary>
        /// 加载绑定数据
        /// </summary>
        private void Loging()
        {
            //QCInfo_ID

            DataTable dt = LinQBaseDao.Query("select Unusual_QCInfo_ID from Unusual where Unusual_Id=" + UnusualId).Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataSet ds = LinQBaseDao.Query("select CNTR_NO,QCInfo_ID,PO_NO,PROD_ID,WEIGHT_TICKET_NO,NO_OF_BALES from QCInfo left join dbo.DRAW_EXAM_INTERFACE on QCInfo.QCInfo_DRAW_EXAM_INTERFACE_ID=DRAW_EXAM_INTERFACE.DRAW_EXAM_INTERFACE_ID WHERE QCInfo_Id=" + dt.Rows[0][0].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCNTR_NO.Text = ds.Tables[0].Rows[0]["CNTR_NO"].ToString();
                    lblQCInfo_ID.Text = ds.Tables[0].Rows[0]["QCInfo_ID"].ToString();
                    lblPO_NO.Text = ds.Tables[0].Rows[0]["PO_NO"].ToString();
                    lblPROD_ID.Text = ds.Tables[0].Rows[0]["PROD_ID"].ToString();
                    lblWEIGHT_TICKET_NO.Text = ds.Tables[0].Rows[0]["WEIGHT_TICKET_NO"].ToString();
                    lblNO_OF_BALES.Text = ds.Tables[0].Rows[0]["NO_OF_BALES"].ToString();
                }
            }

        }
    }
}
