using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinLoading
{
    public partial class Form5 : Form
    {
        public Form5(int vMax)
        {
            InitializeComponent();

            this.progressBar1.Maximum = vMax;
        }

        public bool Increase(int nValue, string nInfo)
        {
            if (nValue > 0)
            {
                if (progressBar1.Value + nValue < progressBar1.Maximum)
                {
                    progressBar1.Value += nValue;
                    this.textBox1.AppendText(nInfo);
                    Application.DoEvents();
                    progressBar1.Update();
                    progressBar1.Refresh();
                    this.textBox1.Update();
                    this.textBox1.Refresh();
                    return true;
                }
                else
                {
                    progressBar1.Value = progressBar1.Maximum;
                    this.textBox1.AppendText(nInfo);
                    //this.Close();//执行完之后，自动关闭子窗体
                    return false;
                }
            }
            return false;
        }

    }
}
