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
    public partial class frmWeightTest : Form
    {
        public MainFrom mf;
        public frmWeightTest()
        {
            InitializeComponent();
        }

        private void frmWeightTest_Load(object sender, EventArgs e)
        {
        }

        private void timerData_Tick(object sender, EventArgs e)
        {
            if (mf.isNewWeight)
            {
                txtRecord.Text =  DateTime.Now + " 重量:" + mf.NewWeight + "\r\n"+ txtRecord.Text ;
                mf.isNewWeight = false;
                if (txtRecord.Text.Length > 20000)
                    txtRecord.Text = "";

                txtRecord2.Text = DateTime.Now + " 重量:" + mf.NewWeight2 + "\r\n" + txtRecord2.Text;
                mf.isNewWeight2 = false;
                if (txtRecord2.Text.Length > 20000)
                    txtRecord2.Text = "";
            }      
        }
    }
}
