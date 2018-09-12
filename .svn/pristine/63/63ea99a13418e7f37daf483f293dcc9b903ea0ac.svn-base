using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEDAL;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class frmWateTest : Form
    {
        public MainFrom mf;
        public frmWateTest()
        {
            InitializeComponent();
        }
        DateTime dtime;
        string cid = "";
        private void timerData_Tick(object sender, EventArgs e)
        {
            //if (mf.strWater != "")
            //{
            //    txtRecord.Text = "——" + mf.strWater + txtRecord.Text;
            //    mf.strWater = "";
            //}
            try
            {
                DataTable dt = LinQBaseDao.Query("select * from Moisture_test where Moisture_test_Collection_ID in (" + cid + ") and Moisture_test_status=0 and Moisture_test_Time>'" + dtime + "' ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    //string instrumentID =  dt.Rows[0]["Moistrue_test_Instrument_ID"].ToString();
                    string strWater = Common.GetServersTime().ToString() + " 序号：" + dt.Rows[0]["Moisture_test_NO"].ToString() + " 次数：" + dt.Rows[0]["Moisture_test_count"].ToString() + " 水分值：" + dt.Rows[0]["Moisture_test_value"].ToString();
                    //if (instrumentID == "1")
                    //{                        
                    //    txtRecord.Text = strWater + "\r\n" + txtRecord.Text;
                    //}
                    //else
                    //{
                    //    txtRecord2.Text = strWater + "\r\n" + txtRecord2.Text;
                    //}
                    txtRecord.Text = strWater + "\r\n" + txtRecord.Text;
                    LinQBaseDao.Query("update Moisture_test set Moisture_test_status=1 where Moisture_test_ID=" + dt.Rows[0]["Moisture_test_ID"].ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        private void frmWateTest_Load(object sender, EventArgs e)
        {
            dtime = Common.GetServersTime();
            DataTable dt = LinQBaseDao.Query("select * from CollectionInfo where Collection_Client_ID=" + Common.CLIENTID).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cid += dt.Rows[i][0].ToString() + ",";
                }
                cid = cid.TrimEnd(',');
            }
        }
    }
}
