using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class Form_Loading : Form
    {
        public Form_Loading()
        {
            InitializeComponent();
        }
        public void CloseLoading(Form source)
        {
            //source.Enabled = true;
            this.TopMost = false;
            this.Close();
        }
        public void ShowLoading(Form source)
        {
            //source.Enabled = false;
            this.TopMost = true;
            this.Show();
        }
    }
}
