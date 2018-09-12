using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinLoading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 定义一个代理，用于更新ProgressBar的值（Value）及在执行方法的时候，返回方法的处理信息。
        /// </summary>
        /// <param name="ipos"></param>
        /// <param name="vinfo"></param>
        public delegate void SetPos(int ipos,string vinfo);


        /// <summary>
        /// 进度条值更新函数（参数必须跟声明的代理参数一样）
        /// </summary>
        /// <param name="ipos"></param>
        /// <param name="vinfo"></param>
        private void SetTextMessage(int ipos,string vinfo)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMessage);
                this.Invoke(setpos, new object[] { ipos, vinfo });
            }
            else
            {
                this.label1.Text = ipos.ToString() + "/1000";
                this.progressBar1.Value = Convert.ToInt32(ipos);
                this.textBox1.AppendText(vinfo);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(SleepT));
            thread.Start();
        }

        private void SleepT()
        {
            for (int i=0;i<500;i++)
            {
                System.Threading.Thread.Sleep(10);
                SetTextMessage(100*i/500,i.ToString()+"\r\n");
            }
        }
    }
}
