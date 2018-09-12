using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEDAL;
using EMEWEEntity;

namespace EMEWEQUALITY.NewAdd
{
    public partial class SMSManualSendForm : Form
    {
        public SMSManualSendForm()
        {
            InitializeComponent();
        }

        public SMSManualSendForm(int qcinfoId)
        {
            QcinfoID = qcinfoId;
            InitializeComponent();
        }
        int QcinfoID = 0;


        public MainFrom mf;//主窗体
        /// <summary>
        /// 单击发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSMSSend_Click(object sender, EventArgs e)
        {
           
            //短信内容
            
            if (string.IsNullOrEmpty(txtcontent.Text.Trim()))
            {
                MessageBox.Show("请输入短信内容");
                return;
            }

            //质检状态改为暂停质检
            LinQBaseDao.Query("update QCInfo set QCInfo_Dictionary_ID=(select Dictionary_ID from Dictionary where Dictionary_Name='暂停质检') where QCInfo_ID=" + QcinfoID);

            //添加异常信息
            //insert into Unusual values(质检编号,异常类型编号,异常类型编号,'内容','未处理',经手人编号,0)
            Unusual un = new Unusual();
            un.Unusual_QCInfo_ID = QcinfoID;
            un.Unusual_UnusualType_Id = Convert.ToInt32(cboUnusualType_Name.SelectedValue);
            un.Unusual_TestItems_ID = Convert.ToInt32(cboUnusualType_Name.SelectedValue);
            un.Unusual_content = cboUnusualType_Name.Text + "异常：" + txtcontent.Text.Trim();
            un.Unusual_State = "未处理";
            un.Unusual_ISSMSSend = false;
            un.Unusual_time = LinQBaseDao.getDate();

            int u_id = 0;
            if (UnusualDAL.InsertOneQCRecord(un,out u_id))
            {
                MessageBox.Show("短信发送成功！");


                int TestItems_ID = Convert.ToInt32(cboUnusualType_Name.SelectedValue);
                //发送内容及发送人
                DataSet SMSDs = LinQBaseDao.Query("select top(1)* from SMSConfigure where SMSConfigure_TestItems_ID=" + TestItems_ID);//只会存在一条

                #region 取内容
                string[] SMSContent = SMSDs.Tables[0].Rows[0]["SMSConfigure_SendContent"].ToString().Split(',');
                string[] SMSContentTxt = SMSDs.Tables[0].Rows[0]["SMSConfigure_SendContentText"].ToString().Split(',');
                string Contents = SMSSendContent(SMSContent, un.Unusual_Id, SMSContentTxt, un.Unusual_content);//存储短信内容
                #endregion

                #region 取号码、姓名,并对其号码发送短信（号码和姓名同位置是一组数据）
                string[] SMSPhon = SMSDs.Tables[0].Rows[0]["SMSConfigure_ReceivePhone"].ToString().Split(';');//号码
                string[] SMSName = SMSDs.Tables[0].Rows[0]["SMSConfigure_Receive"].ToString().Split(';');//姓名

                for (int y = 0; y < SMSPhon.Count(); y++)//循环要发送的人数的电话号码
                {
                    if (SMSPhon[y] != "")
                    {
                        SmsSend ss = new SmsSend();
                        ss.SmsSend_Phone = SMSPhon[y];
                        ss.SmsSend_Text = Contents;
                        ss.SmsSend_userName = SMSName[y];
                        ss.SmsSend_IsSend = "0";
                        ss.SmsSend_Unusunal_ID = un.Unusual_Id;
                       bool b = SmsSendDAL.Insert(ss);


                    }

                }



                #endregion



                Emety();
            }
            else
            {
                MessageBox.Show("短信发送失败！");
                return;
            }
        }
        /// <summary>
        /// 取短信发送内容
        /// </summary>
        /// <param name="SMSContent">字段组合</param>
        /// <param name="UnusualType_Id">异常ID</param>
        /// <returns></returns>
        private string SMSSendContent(string[] SMSContent, int Unusual_Id, string[] SMSContentTxt, string sendText)
        {

            try
            {
                string NumContent = sendText + "  ";
                for (int i = 0; i < SMSContent.Count(); i++)
                {

                    DataSet ds = LinQBaseDao.Query("select top(1)" + SMSContent[i] + " from View_Unusual_SMS where Unusual_Id=" + Unusual_Id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        NumContent += SMSContentTxt[i] + "：" + ds.Tables[0].Rows[0][0].ToString() + "  ";
                    }
                   
                }
                return NumContent;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            return "";

        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SMSManualSendForm_Load(object sender, EventArgs e)
        {
            UnusualType();
        }

        /// <summary>
        /// 绑定异常类型
        /// </summary>
        private void UnusualType()
        {
            string Sql = "select * from dbo.TestItems";
            DataSet ds = LinQBaseDao.Query(Sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.cboUnusualType_Name.DataSource = ds.Tables[0];
                this.cboUnusualType_Name.DisplayMember = "TestItems_NAME";
                this.cboUnusualType_Name.ValueMember = "TestItems_ID";
                cboUnusualType_Name.SelectedIndex = cboUnusualType_Name.Items.Count - 1;

            }
            else
            {
                cboUnusualType_Name.DataSource = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Emety();
        }

        private void Emety() 
        {
            txtcontent.Text = "";
           
            cboUnusualType_Name.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtcontent_TextChanged(object sender, EventArgs e)
        {
            if (txtcontent.Text.Length >50 )
            {
                MessageBox.Show("短信内容超长，请控制在50字以内！");
                btnSMSSend.Enabled = false;

            }
            else
            {
                btnSMSSend.Enabled = true;
            }
        }
    }
}
                