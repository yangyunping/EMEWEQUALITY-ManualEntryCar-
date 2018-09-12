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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();

            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //var count = (int)e.Argument;

                for (int i = 1; i <= 1000; i++)
                {
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    bw.ReportProgress(i);
                    Thread.Sleep(200);      // 模拟耗时的任务
                }

            } catch (Exception err)
            {
                MessageBox.Show(err.Message);

            }
            

        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.textBox1.Text += e.UserState.ToString(); //主窗体传过来的值，通过e.UserState.ToString()来接受
          //  textBox1.Text += DateTime.Now + "\r\n";

        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                textBox1.Text += "任务取消。" + "\r\n";
            else if (e.Error != null)
                textBox1.Text += "出现异常: " + e.Error + "\r\n";
            else
                textBox1.Text += "任务完成。 " + "\r\n";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 10;

            textBox1.Text = "任务开始..." + "\r\n";

            bw.RunWorkerAsync(); // 启动
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bw.CancelAsync();// 取消
        }
    }
}
