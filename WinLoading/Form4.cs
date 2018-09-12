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

    public partial class Form4 : Form
    {

        //创建代理。
        private Form5 myProcessBar = null;//弹出的子窗体(用于显示进度条)
        private delegate bool IncreaseHandle(int nValue, string vinfo);//代理创建
        private IncreaseHandle myIncrease = null;//声明代理，用于后面的实例化代理
        private int vMax = 100;//用于实例化进度条，可以根据自己的需要，自己改变

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thdSub = new Thread(new ThreadStart(ThreadFun));
            thdSub.Start();

        }

        private void ThreadFun()
        {
            MethodInvoker mi = new MethodInvoker(ShowProcessBar);
            this.BeginInvoke(mi);
            Thread.Sleep(100);
            object objReturn = null;
            for (int i = 0; i < vMax; i++)
            {
                objReturn = this.Invoke(this.myIncrease, new object[] { 2, i.ToString() + "\r\n" });
                Thread.Sleep(50);
            }
        }

        private void ShowProcessBar()
        {
            myProcessBar = new Form5(vMax);
            myIncrease = new IncreaseHandle(myProcessBar.Increase);
            myProcessBar.ShowDialog();
            myProcessBar = null;
        }


    }
}
