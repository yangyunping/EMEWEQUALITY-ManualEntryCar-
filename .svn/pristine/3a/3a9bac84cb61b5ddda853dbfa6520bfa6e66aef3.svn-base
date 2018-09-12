using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using EMEWEDAL;

namespace EMEWEQUALITY.NewAdd
{
    public partial class UnusualPictureForm : Form
    {
        public UnusualPictureForm(int id)
        {
            InitializeComponent();
            UnusualID = id;
        }
        int UnusualID = 0;
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnusualPictureForm_Load(object sender, EventArgs e)
        {
            SelectImg();
        }
        /// <summary>
        /// 服务器路径
        /// </summary>
        private static string FWpath = ConfigurationManager.AppSettings["FWpath"];



        int ImageRecord_Id = 0;
        int cx = 19;
        int cy = 20;
        PictureBox pb = null;
        /// <summary>
        /// 查询异常图片
        /// </summary>
        private void SelectImg() {
            string Sql = "select top(1) ImageRecord_Id,ImageRecord_ImageName from ImageRecord where ImageRecord_Unusual_Id=" + UnusualID + " order by ImageRecord_Id";
            DataSet ds=LinQBaseDao.Query(Sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("没有异常图片");
                this.Close();
                return;//无图片
            }
            ImageRecord_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ImageRecord_Id"]);
            label1.Text = "1";
            pb = new PictureBox();
            pb.Location = new Point(15, 20);
            pb.Size = new Size(640, 460);
            pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pb.ImageLocation = ds.Tables[0].Rows[0]["ImageRecord_ImageName"].ToString();
            pb.Name = "pictureBox";
            pb.Image = Image.FromFile(ds.Tables[0].Rows[0]["ImageRecord_ImageName"].ToString());
            pb.Visible = true;
            groupBox1.Controls.Add(pb);
            ////循环展示图片，并计算位置
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if (cx > 540)
            //    {
            //        cy = 20 + 236;
            //        cx = 19;
            //    }
            //    try
            //    {
            //        pb = new PictureBox();
            //        pb.Location = new Point(cx, cy);
            //        pb.Size = new Size(290, 230);
            //        pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            //        pb.ImageLocation = ds.Tables[0].Rows[i]["ImageRecord_ImageName"].ToString();
            //        pb.Name = "pictureBox";
            //        pb.Image = Image.FromFile(ds.Tables[0].Rows[i]["ImageRecord_ImageName"].ToString());
            //        pb.Visible = true;
            //        groupBox1.Controls.Add(pb);
            //        cx += 290 + 30;
            //    }
            //    catch 
            //    {
                    
                     
            //    }
             
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label1.Text.Trim()))
            {
                //无图片
                return;
            }
            string Sql = "select top(1) ImageRecord_Id,ImageRecord_ImageName from ImageRecord where ImageRecord_Unusual_Id=" + UnusualID + "  and ImageRecord_Id>" + ImageRecord_Id + " order by ImageRecord_Id";
            DataSet ds = LinQBaseDao.Query(Sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("已经是最后一张图片");
                return;//无图片
            }
            groupBox1.Controls.Clear();
            ImageRecord_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ImageRecord_Id"]);
            label1.Text = (Convert.ToInt32(label1.Text.Trim()) + 1).ToString();
            pb = new PictureBox();
            pb.Location = new Point(15, 20);
            pb.Size = new Size(640, 460);
            pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pb.ImageLocation = ds.Tables[0].Rows[0]["ImageRecord_ImageName"].ToString();
            pb.Name = "pictureBox";
            pb.Image = Image.FromFile(ds.Tables[0].Rows[0]["ImageRecord_ImageName"].ToString());
            pb.Visible = true;
            groupBox1.Controls.Add(pb);
        }

        private void btnShang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label1.Text.Trim()))
            {
                //无图片
                return;
            }
            string Sql = "select top(1) ImageRecord_Id,ImageRecord_ImageName from ImageRecord where ImageRecord_Unusual_Id=" + UnusualID + "  and ImageRecord_Id<" + ImageRecord_Id + " order by ImageRecord_Id desc";
            DataSet ds = LinQBaseDao.Query(Sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("已经是第一张异常图片");
                return;//无图片
            }
            groupBox1.Controls.Clear();
            ImageRecord_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ImageRecord_Id"]);
            label1.Text = (Convert.ToInt32(label1.Text.Trim()) - 1).ToString();
            pb = new PictureBox();
            pb.Location = new Point(15, 20);
            pb.Size = new Size(640, 460);
            pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pb.ImageLocation = ds.Tables[0].Rows[0]["ImageRecord_ImageName"].ToString();
            pb.Name = "pictureBox";
            pb.Image = Image.FromFile(ds.Tables[0].Rows[0]["ImageRecord_ImageName"].ToString());
            pb.Visible = true;
            groupBox1.Controls.Add(pb);
        }
    }
}
