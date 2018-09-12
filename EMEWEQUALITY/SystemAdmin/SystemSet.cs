﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using EMEWE.Common;
using System.Threading;
using EMEWEEntity;
using EMEWEDAL;
using EMEWEQUALITY.HelpClass;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;
using MoistureDetectionRules;
using System.Windows.Forms;
using EMEWEQUALITY.LED;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class SystemSet : Form
    {
        public SystemSet()
        {
            InitializeComponent();
        }
        public MainFrom mf;
        public int tabControlIndex = 0;
        public Login login;
        DCQUALITYDataContext dc = null;
        PageControl page = new PageControl();

        Expression<Func<idebarJoinU9, bool>> expr = null;
        int currentId = 0;
        private void SystemSet_Load(object sender, EventArgs e)
        {
            BindcomboBox();
            BindClient();
            InitComName();
            bangdinU9();
        }

        private void bangdinU9()
        {
            try
            {
                string sql = "select U9ID,	U9Name,	U9Bool from dbo.U9Start";
                DataTable td = LinQBaseDao.Query(sql).Tables[0];
                foreach (DataRow item in td.Rows)
                {
                    if (item["U9Name"].ToString() == "水分检测")
                    {
                        if (item["U9Bool"].ToString() == "True")
                        {
                            Water1.Checked = true;
                        }
                        else
                        {
                            Water2.Checked = true;
                        }
                    }

                    if (item["U9Name"].ToString() == "重量检测")
                    {
                        if (item["U9Bool"].ToString() == "True")
                        {
                            Weight1.Checked = true;
                        }
                        else
                        {
                            Weight2.Checked = true;
                        }
                    }


                    if (item["U9Name"].ToString() == "一检管理")
                    {
                        if (item["U9Bool"].ToString() == "True")
                        {
                            inspection1.Checked = true;
                        }
                        else
                        {
                            inspection2.Checked = true;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public void BindcomboBox()
        {
            cobWeightTwo.Items.Add(" ");
            cobMOISTURETwo.Items.Add(" ");
            for (int i = 0; i < 100; i++)
            {
                cobWeight.Items.Add(i);
                cobWeightTwo.Items.Add(i);
                cobMOISTURE.Items.Add(i);
                cobMOISTURETwo.Items.Add(i);
            }

            //cbxplatformScaleName.DataSource = Common.dtScaleComAttribute;
            //cbxplatformScaleName.DisplayMember = "name";
            //cbxplatformScaleName.ValueMember = "rate";
            //cbxplatformScaleName.SelectedIndex = 0;
            List<string> nameList = Common.GetPlatformScalesName();
            foreach (string scaleName in nameList)
            {
                cbxplatformScaleName.Items.Add(scaleName);
                cbxplatformScaleNameTwo.Items.Add(scaleName);
            }

            //cbxplatformScaleNameTwo.DataSource = Common.dtScaleComAttribute;
            //cbxplatformScaleNameTwo.DisplayMember = "name";
            //cbxplatformScaleNameTwo.ValueMember = "rate";
            //cbxplatformScaleNameTwo.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定客户端
        /// </summary>
        private void BindClient()
        {
            DataTable dt = LinQBaseDao.Query("select Client_ID,Client_NAME from ClientInfo where Client_Dictionary_ID=2").Tables[0];
            if (dt.Rows.Count > 0)
            {
                cmbClient_NAME.DataSource = dt;
                cmbClient_NAME.DisplayMember = "Client_NAME";
                cmbClient_NAME.ValueMember = "Client_ID";
                cmbClient_NAME.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitComName()
        {
            cobWeight.Text = Common.WEIGHTCOM.ToString();
            cobMOISTURE.Text = Common.MOISTURECOM.ToString();
            cbxplatformScaleName.Text = Common.platformScaleName;
            cobWeightTwo.Text = Common.WEIGHTCOMTwo.ToString();
            cobMOISTURETwo.Text = Common.MOISTURECOMTwo.ToString();
            cbxplatformScaleNameTwo.Text = Common.platformScaleNameTwo;

            cmbClient_NAME.SelectedValue = Common.CLIENTID;
            cmbCollectionName.SelectedValue = Common.CollectionID;
            txtLEDFromX.Text = Common.LEDFROMX.ToString();
            txtLEDFromY.Text = Common.LEDFROMY.ToString();
            txtLEDX.Text = Common.LEDX.ToString();
            txtLEDY.Text = Common.LEDY.ToString();
            txtOrganizationID.Text = Common.OrganizationID;
            txtSourceID.Text = Common.SourceID;
            txtGainURL.Text = Common.GainURL;
            txtSendURL.Text = Common.SendURL;
            txt_PrintDemoTitle.Text = Common.PrintDemoTitle;
            txt_AbnormalPrintDemoTitle.Text = Common.AbnormalPrintDemoTitle;
            txt_PrintDemoTitleRoute.Text = Common.PrintDemoTitleRoute.Replace(Application.StartupPath, "");
            cbxEndTime.Text = Common.LEDEndTime;
            cbxTabTime.Text = Common.LEDTabTime;
            txtDVRIP1.Text = Common.DVRIP;
            txtDVRServerPort1.Text = Common.DVRServerPort;
            txtDVRLoginName1.Text = Common.DVRLoginName;
            txtDVRPwd1.Text = Common.DVRPwd;
            txtSaveFiel1.Text = Common.SaveFiel;
            txtlChannel1.Text = Common.Channel.ToString();
            txtlChannel2.Text = Common.Channel2.ToString();
            txtlChannel3.Text = Common.Channel3.ToString();
            cmbCB.Text = Common.ISPackets;
            //cmbitem.SelectedIndex = Common.ItemMoist;
            //txtMoistCount.Text = Common.ItemMoistCount.ToString();
            cmbMOISTURENAME.Text = Common.MOISTURENAME;
            cmbMOISTURENAMETWO.Text = Common.MOISTURENAMETwo;
            GetDataSet();
        }
        /// <summary>
        /// 初始化数据库连接字符串
        /// </summary>
        private void GetDataSet()
        {
            string oldSqlStr = System.Configuration.ConfigurationManager.ConnectionStrings["EMEWEQCConnectionString"].ToString();
            if (oldSqlStr != "")
            {
                string[] sqlStr = oldSqlStr.Split(';');
                if (sqlStr.Length > 1)
                {
                    foreach (string str1 in sqlStr)
                    {
                        if (str1 != "")
                        {
                            string[] str = str1.Split('=');
                            if (str.Length > 1)
                            {
                                if (str[0] == "Data Source")
                                {
                                    this.tbDataSource.Text = str[1].ToString();
                                }
                                else if (str[0] == "User ID")
                                {
                                    this.tbUserName.Text = str[1].ToString();
                                }
                                else if (str[0] == "Password")
                                {
                                    this.tbPasswd.Text = str[1].ToString();
                                }
                                else if (str[0] == "Initial Catalog")
                                {
                                    this.tbDatabase.Text = str[1].ToString();
                                }
                            }
                        }

                    }
                }
            }
        }
        private bool StrIsNullOrEmpty()
        {
            int k = 0;
            bool rbool = true;
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(cobMOISTURE.Text))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "水分仪串口号不能为空！", cobMOISTURE, this);
                rbool = false;
            }
            if (string.IsNullOrEmpty(cmbMOISTURENAME.Text))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "水分仪编号不能为空！", cmbMOISTURENAME, this);
                rbool = false;
            }

            if (string.IsNullOrEmpty(cobWeight.Text))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "小磅串口号不能为空！", cobWeight, this);
                rbool = false;
            }

            if (string.IsNullOrEmpty(txtLEDFromX.Text))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "LED屏幕X坐标不能为空！", txtLEDFromX, this);
                rbool = false;
            }
            else
            {
                if (!int.TryParse(txtLEDFromX.Text, out k))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "请在LED屏幕X坐标框中输入整数！", txtLEDFromX, this);
                    rbool = false;
                }
            }
            if (string.IsNullOrEmpty(txtLEDFromY.Text))
            {
                mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "LED屏幕Y坐标不能为空！", txtLEDFromY, this);
                rbool = false;
            }
            else
            {
                if (!int.TryParse(txtLEDFromY.Text, out k))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "请在LED屏幕Y坐标框中输入整数！", txtLEDFromY, this);
                    rbool = false;
                }
            }
            if (!string.IsNullOrEmpty(txtLEDX.Text))
            {
                if (!int.TryParse(txtLEDX.Text, out k))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "请在LED内容起X坐标框中输入整数！", txtLEDX, this);
                    rbool = false;
                }
            }
            if (!string.IsNullOrEmpty(txtLEDY.Text))
            {
                if (!int.TryParse(txtLEDY.Text, out k))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "设置提示", "请在LED内容起Y坐标框中输入整数！", txtLEDY, this);
                    rbool = false;
                }
            }
            return rbool;
        }
        private void btnParity_Click(object sender, EventArgs e)
        {
            if (StrIsNullOrEmpty())
            {
                Save();
                Common.WriteLogData("修改", "串口设置", Common.NAME); //操作日志
            }
        }

        private LEDCreator _LEDCreator = new LEDCreator();
        private void btnLEDSet_Click(object sender, EventArgs e)
        {
            //if (StrIsNullOrEmpty())
            //{
            //    Save();
            //    Common.WriteLogData("修改", "LED设置", Common.NAME); //操作日志
            //}
            //Form formLEDSetup = _LEDCreator.GetFormLEDSetup(Common.OrganizationID);
            //formLEDSetup.ShowDialog(); 
        }

        private void Save()
        {
            try
            {
                string MOISTURECOM = cobMOISTURE.Text.Trim();
                string platformScaleName = cbxplatformScaleName.Text.Trim();
                string MOISTURECOMTwo = cobMOISTURETwo.Text.Trim();
                string platformScaleNameTwo = cbxplatformScaleNameTwo.Text.Trim();
                string WEIGHTCOM = cobWeight.Text.Trim();
                string WEIGHTCOMTwo = cobWeightTwo.Text.Trim();

                //string NTCODE = txtNTCODE.Text.Trim();
                string LEDFROMX = txtLEDFromX.Text.Trim();
                string LEDFROMY = txtLEDFromY.Text.Trim();
                string LEDX = txtLEDX.Text.Trim();
                string LEDY = txtLEDY.Text.Trim();
                string PrintDemoTitle = txt_PrintDemoTitle.Text.Trim();
                string AbnormalPrintDemoTitle = txt_AbnormalPrintDemoTitle.Text.Trim();
                string PrintDemoTitleRoute = txt_PrintDemoTitleRoute.Text.Trim();
                string OrganizationID = txtOrganizationID.Text.Trim();
                string SourceID = txtSourceID.Text.Trim();
                string GainURL = txtGainURL.Text.Trim();
                string SendURL = txtSendURL.Text.Trim();
                string LEDTabTime = cbxTabTime.Text.Trim();
                string LEDEndTime = cbxEndTime.Text.Trim();
                string ISPackets = cmbCB.Text.Trim();

                string filepath = System.IO.Directory.GetCurrentDirectory() + "\\SystemSet.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                XmlNode xn = xmlDoc.SelectSingleNode("param");//查找<bookstore>
                XmlNodeList xnl = xn.ChildNodes;
                if (xnl.Count > 0)
                {
                    foreach (XmlNode xnf in xnl)
                    {
                        XmlElement xe = (XmlElement)xnf;

                        xe.SetAttribute("MOISTURECOM", MOISTURECOM);
                        xe.SetAttribute("platformScaleName", platformScaleName);
                        xe.SetAttribute("MOISTURECOMTwo", MOISTURECOMTwo);
                        xe.SetAttribute("platformScaleNameTwo", platformScaleNameTwo);
                        xe.SetAttribute("WEIGHTCOM", WEIGHTCOM);
                        xe.SetAttribute("WEIGHTCOMTwo", WEIGHTCOMTwo);

                        xe.SetAttribute("LEDFROMX", LEDFROMX);
                        xe.SetAttribute("LEDFROMY", LEDFROMY);
                        xe.SetAttribute("LEDX", LEDX);
                        xe.SetAttribute("LEDY", LEDY);
                        xe.SetAttribute("PrintDemoTitle", PrintDemoTitle);
                        xe.SetAttribute("AbnormalPrintDemoTitle", AbnormalPrintDemoTitle);
                        xe.SetAttribute("PrintDemoTitleRoute", PrintDemoTitleRoute);

                        xe.SetAttribute("OrganizationID", OrganizationID);
                        xe.SetAttribute("SourceID", SourceID);
                        xe.SetAttribute("GainURL", GainURL);
                        xe.SetAttribute("SendURL", SendURL);
                        xe.SetAttribute("LEDTabTime", LEDTabTime);
                        xe.SetAttribute("LEDEndTime", LEDEndTime);
                        xe.SetAttribute("ISPackets", ISPackets);

                    }
                    xmlDoc.Save(filepath);

                    Common.PrintDemoTitle = PrintDemoTitle;
                    Common.PrintDemoTitleRoute = PrintDemoTitleRoute;
                    Common.AbnormalPrintDemoTitle = AbnormalPrintDemoTitle;
                    Common.MOISTURECOM = Converter.ToInt(MOISTURECOM, 1);
                    Common.platformScaleName = platformScaleName;
                    Common.MOISTURECOMTwo = Converter.ToInt(MOISTURECOMTwo, 1);
                    Common.platformScaleNameTwo = platformScaleNameTwo;
                    Common.WEIGHTCOM = Converter.ToInt(WEIGHTCOM, 2);
                    Common.WEIGHTCOMTwo = Converter.ToInt(WEIGHTCOMTwo, 2);

                    Common.LEDFROMX = Converter.ToInt(LEDFROMX);
                    Common.LEDFROMY = Converter.ToInt(LEDFROMY);
                    Common.LEDX = Converter.ToInt(LEDX);
                    Common.LEDY = Converter.ToInt(LEDY);
                    Common.OrganizationID = OrganizationID;
                    Common.SourceID = SourceID;
                    Common.GainURL = GainURL;
                    Common.SendURL = SendURL;

                    Common.LEDTabTime = LEDTabTime;
                    Common.LEDEndTime = LEDEndTime;
                    Common.ISPackets = ISPackets;

                    MessageBox.Show("系统配置成功！", "操作提示", MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                    szcg = true;
                }
            }
            catch
            {
                MessageBox.Show("系统配置失败！", "操作提示", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
            }
        }
        bool szcg = false;
        private void btnLEDApplication_Click(object sender, EventArgs e)
        {
            //mf.CloseLEDSHOW();
            //mf.SETLEDSHOW();

            //if (Common.OrganizationID == "JiangXiPaper")
            //{
            //    FormLEDJiangXiSetup formLEDSetup = (FormLEDJiangXiSetup)_LEDCreator.GetFormLEDSetup(Common.OrganizationID);
            //    formLEDSetup.ShowLED();
            //}
            //else if (Common.OrganizationID == "ChongQingPaper")
            //{
            //    FormLEDChongQingSetup formLEDSetup = (FormLEDChongQingSetup)_LEDCreator.GetFormLEDSetup(Common.OrganizationID);
            //    formLEDSetup.ShowLED();
            //}

            //LEDCtrlPoxy ledCtrlProxy = new LEDCtrlPoxy(Common.OrganizationID);
            //ledCtrlProxy.ShowLed();
        }

        private void btnParityApplication_Click(object sender, EventArgs e)
        {
            if (szcg)
            {
                Thread.Sleep(20);
                Common.WEIGHTCOM = int.Parse(cobWeight.Text);
                Common.WEIGHTCOMBaudRate = Common.GetComBaudrate(cbxplatformScaleName.Text);
                Common.WEIGHTCOMTwo = int.Parse(cobWeightTwo.Text);
                Common.WEIGHTCOMTwoBaudRate = Common.GetComBaudrate(cbxplatformScaleNameTwo.Text);
                Common.MOISTURENAME = cobMOISTURE.Text;
                Common.MOISTURENAMETwo = cobMOISTURETwo.Text;
                mf.SetPrivert();
            }
            else
            {
                MessageBox.Show("请先设置在应用！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btn_TitleSet_Click(object sender, EventArgs e)
        {
            if (StrIsNullOrEmpty())
            {
                Save();
                Common.WriteLogData("修改", "打印标题设置", Common.NAME); //操作日志
            }
        }

        private void btnClientSetSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbClient_NAME.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "客户端设置提示", "客户端名称不能为空！", cmbClient_NAME, this);
                    return;
                }
                if (string.IsNullOrEmpty(cmbCollectionName.Text))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "客户端设置提示", "采集端名称不能为空！", cmbCollectionName, this);
                    return;
                }
                //if (string.IsNullOrEmpty(txtMoistCount.Text))
                //{
                //    mf.ShowToolTip(ToolTipIcon.Info, "客户端设置提示", "检测次数不能为空！", txtMoistCount, this);
                //    return;
                //}
                SaveClientid();
                SaveMoistureDetectionCfg();
                Common.WriteLogData("修改", "客户端设置", Common.NAME); //操作日志
            }
            catch (Exception ex)
            {
            }
        }

        private void SaveMoistureDetectionCfg()
        {
            MoistureDetectionRulesCreator creator = new MoistureDetectionRulesCreator();
            string cfgPath = System.IO.Directory.GetCurrentDirectory() + "\\moistureDetectRule.xml";
            creator.SaveMoistureDetectionParams(Common.OrganizationID, cfgPath);
        }
        /// <summary>
        /// 保存客户端
        /// </summary>
        private void SaveClientid()
        {
            try
            {
                string MOISTURENAME = cmbMOISTURENAME.Text.Trim();
                string MOISTURENAMETwo = cmbMOISTURENAMETWO.Text.Trim();
                string CLIENTID = cmbClient_NAME.SelectedValue.ToString();
                string CLIENTNAME = cmbClient_NAME.Text.Trim();
                string CollectionID = cmbCollectionName.SelectedValue.ToString();
                string CollectionNAME = cmbCollectionName.Text.Trim();
                //string itemmoist = cmbitem.Text;
                //if (itemmoist == "总计")
                //{
                //    itemmoist = "1";
                //}
                //else
                //{
                //    itemmoist = "0";
                //}
                //string moistcount = txtMoistCount.Text;
                string filepath = System.IO.Directory.GetCurrentDirectory() + "\\SystemSet.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                XmlNode xn = xmlDoc.SelectSingleNode("param");//查找<bookstore>
                XmlNodeList xnl = xn.ChildNodes;
                if (xnl.Count > 0)
                {
                    foreach (XmlNode xnf in xnl)
                    {
                        XmlElement xe = (XmlElement)xnf;

                        xe.SetAttribute("MOISTURENAME", MOISTURENAME);
                        xe.SetAttribute("MOISTURENAMETwo", MOISTURENAMETwo);
                        xe.SetAttribute("CLIENTID", CLIENTID);
                        xe.SetAttribute("CLIENTNAME", CLIENTNAME);
                        xe.SetAttribute("CollectionID", CollectionID);
                        xe.SetAttribute("CollectionNAME", CollectionNAME);
                        //xe.SetAttribute("ItemMoist", itemmoist);
                        //xe.SetAttribute("ItemMoistCount", moistcount);
                    }
                    xmlDoc.Save(filepath);
                    Common.MOISTURENAME = MOISTURENAME;
                    Common.MOISTURENAMETwo = MOISTURENAMETwo;
                    Common.CLIENTID = Converter.ToInt(CLIENTID);
                    Common.CLIENT_NAME = CLIENTNAME;
                    Common.CollectionID = Converter.ToInt(CollectionID);
                    Common.CollectionNAME = CollectionNAME;
                    //Common.ItemMoist = Converter.ToInt(itemmoist);
                    //Common.ItemMoistCount = Converter.ToInt(moistcount);

                    MessageBox.Show("系统配置成功！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("系统配置失败！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 保存数据源连接地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveDB_Click(object sender, EventArgs e)
        {

            if (this.tbDataSource.Text == "")
            {
                MessageBox.Show("数据源不能为空！", "操作提示", MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                return;
            }
            if (this.tbDatabase.Text == "")
            {
                MessageBox.Show("数据库不能为空！", "操作提示", MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                return;
            }
            if (this.tbUserName.Text == "")
            {
                MessageBox.Show("数据库用户名不能为空！", "操作提示", MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                return;
            }
            if (this.tbPasswd.Text == "")
            {
                MessageBox.Show("数据库密码不能为空！", "操作提示", MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                return;
            }

            try
            {
                string filepath = Application.ExecutablePath + ".config";
                string conString = "Data Source=" + this.tbDataSource.Text + ";User ID=" + this.tbUserName.Text + ";Password=" + this.tbPasswd.Text + ";Initial Catalog=" + this.tbDatabase.Text;
                string autoDataBaseString = "Data Source=" + this.tbDataSource.Text + ";User ID=" + this.tbUserName.Text + ";Password=" + this.tbPasswd.Text + ";Initial Catalog=master";


                bool boolCon = LinQBaseDao.DetectionConn(conString);
                if (!boolCon)
                {
                    MessageBox.Show("此数据源配置无法连接，重新输入正确的数据源配置！", "操作提示", MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    return;
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(filepath);
                XmlNodeList node = doc.SelectSingleNode("configuration").ChildNodes;
                if (node.Count > 0)
                {
                    XmlElement ele = (XmlElement)node[1];
                    XmlNodeList nodeList = ele.ChildNodes;
                    if (nodeList.Count > 0)
                    {
                        XmlElement ele2 = (XmlElement)nodeList[0];
                        ele2.SetAttribute("connectionString", conString);
                        doc.Save(filepath);
                    }
                }
                btWriteReg(conString);
                //btWriteReg2(autoDataBaseString);
                MessageBox.Show("修改成功！请退出系统，重新启动程序！", "操作提示", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                //System.Environment.Exit(System.Environment.ExitCode);
                //Application.ExitThread();
                Common.WriteLogData("修改", "数据源配置", Common.NAME); //操作日志
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改数据配置失败！", "操作提示", MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
            }

        }
        private void btWriteReg(string conString)
        {
            string filepath = System.IO.Directory.GetCurrentDirectory() + "\\EMEWEQUALITY.exe.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            XmlNodeList node = doc.SelectSingleNode("configuration").ChildNodes;
            if (node.Count > 0)
            {
                XmlElement ele = (XmlElement)node[1];
                XmlNodeList nodeList = ele.ChildNodes;
                if (nodeList.Count > 0)
                {
                    XmlElement ele2 = (XmlElement)nodeList[1];
                    ele2.SetAttribute("connectionString", conString);
                    doc.Save(filepath);
                }
            }

        }
        /// <summary>
        /// 页面关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (login != null)
            {
                mf.Close();
                System.Environment.Exit(System.Environment.ExitCode);
                Application.ExitThread();
            }
        }

        private void btn_Route_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_Route.Enabled)
                {
                    btn_Route.Enabled = false;
                }
                PrintTemplateSetUpForm ptsuf = new PrintTemplateSetUpForm();
                mf = new MainFrom();
                mf.ShowChildForm(ptsuf);
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("SystemSet.btn_Route_Click()" + ex.Message.ToString());
            }
            finally
            {
                btn_Route.Enabled = true;
            }


        }

        /// <summary>
        /// 保存DVR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string DVRIP = this.txtDVRIP1.Text.Trim();
                string DVRServerPort = this.txtDVRServerPort1.Text.Trim();
                string DVRLoginName = this.txtDVRLoginName1.Text.Trim();
                string DVRPwd = this.txtDVRPwd1.Text.Trim();
                string SaveFiel = this.txtSaveFiel1.Text.Trim();
                int Channel1 = int.Parse(txtlChannel1.Text.Trim());
                int Channel2 = int.Parse(txtlChannel2.Text.Trim());
                int Channel3 = int.Parse(txtlChannel3.Text.Trim());

                string filepath = System.IO.Directory.GetCurrentDirectory() + "\\SystemSet.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                XmlNode xn = xmlDoc.SelectSingleNode("param");//查找<bookstore>
                XmlNodeList xnl = xn.ChildNodes;
                if (xnl.Count > 0)
                {
                    foreach (XmlNode xnf in xnl)
                    {
                        XmlElement xe = (XmlElement)xnf;

                        //硬盘录像机一
                        xe.SetAttribute("DVRIP", DVRIP);  // 硬盘录像机的IP
                        xe.SetAttribute("DVRServerPort", DVRServerPort); // 硬盘录像机的服务器端口号
                        xe.SetAttribute("DVRLoginName", DVRLoginName);   // 硬盘录像机的登录名称
                        xe.SetAttribute("DVRPwd", DVRPwd); // 硬盘录像机的登录密码
                        xe.SetAttribute("SaveFiel", SaveFiel);
                        xe.SetAttribute("Channel1", Channel1.ToString());
                        xe.SetAttribute("Channel2", Channel2.ToString());
                        xe.SetAttribute("Channel3", Channel3.ToString());
                    }
                    xmlDoc.Save(filepath);
                    Common.DVRIP = DVRIP;
                    Common.DVRServerPort = DVRServerPort;
                    Common.DVRLoginName = DVRLoginName;
                    Common.DVRPwd = DVRPwd;
                    Common.SaveFiel = SaveFiel;
                    Common.Channel = Channel1;
                    Common.Channel2 = Channel2;
                    Common.Channel3 = Channel3;
                }
                MessageBox.Show("保存成功！");
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败！");
            }

        }
        /// <summary>
        /// 现场抽包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 不与U9传数据列表修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dc = new DCQUALITYDataContext();
                idebarJoinU9 ijU9 = dc.idebarJoinU9.Where(n => n.idebarJoinU9_id == currentId).First();
                txtPRODID.Text = ijU9.idebarJoinU9_PROD_ID;
                txtRemork.Text = ijU9.idebarJoinU9_remark;
                btnSave.Text = "修改";

                LoadData("");
            }
            catch (Exception ex)
            {
                MessageBox.Show("请选择数据！");
                return;
            }
        }
        private void LoadData(string strName)
        {
            if (expr == null)
            {
                expr = (Expression<Func<idebarJoinU9, bool>>)PredicateExtensionses.True<idebarJoinU9>();
            }

            if (txtPRODID2.Text != "")
            {
                expr = expr.And(n => SqlMethods.Like(n.idebarJoinU9_PROD_ID, "%" + txtPRODID2.Text.Trim() + "%"));
            }

            dgvView_WaterSet.AutoGenerateColumns = false;
            dgvView_WaterSet.DataSource = page.BindBoundControl<idebarJoinU9>(strName, txtCurrentPage1, lblPageCount1, expr, "idebarJoinU9_id");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                dc = new DCQUALITYDataContext();
                idebarJoinU9 ijU9 = dc.idebarJoinU9.Where(n => n.idebarJoinU9_id == currentId).First();
                dc.idebarJoinU9.DeleteOnSubmit(ijU9);
                dc.SubmitChanges();
                MessageBox.Show("操作成功！");
                LoadData("");
            }
            catch (Exception)
            {
                MessageBox.Show("操作失败！");
                return;
            }


        }

        private void dgvView_WaterSet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                currentId = Convert.ToInt32(dgvView_WaterSet.SelectedRows[0].Cells[0].Value);
            }
            catch { }
        }

        private void dgvView_WaterSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                currentId = Convert.ToInt32(dgvView_WaterSet.SelectedRows[0].Cells[0].Value);

            }
            catch { }
        }


        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPRODID.Text.Trim() == "")
                {
                    MessageBox.Show("货品名称不能为空！");
                    return;
                }

                idebarJoinU9 ij = new idebarJoinU9();
                dc = new DCQUALITYDataContext();
                if (btnSave.Text == "添加")
                {

                    ij.idebarJoinU9_PROD_ID = txtPRODID.Text.Trim();
                    ij.idebarJoinU9_remark = txtRemork.Text.Trim();
                    dc = new DCQUALITYDataContext();
                    dc.idebarJoinU9.InsertOnSubmit(ij);
                    dc.SubmitChanges();
                    MessageBox.Show("操作成功！", "提示");

                }
                else if (btnSave.Text == "修改" && currentId > 0)
                {
                    ij = dc.idebarJoinU9.Where(n => n.idebarJoinU9_id == currentId).First();
                    ij.idebarJoinU9_PROD_ID = txtPRODID.Text.Trim();
                    ij.idebarJoinU9_remark = txtRemork.Text.Trim();
                    dc.SubmitChanges();
                    MessageBox.Show("操作成功！", "提示");

                }
                btnSave.Text = "添加";

                txtPRODID.Text = "";
                txtRemork.Text = "";
                LoadData("");

            }
            catch (Exception ex)
            {

                MessageBox.Show("操作失败！", "提示");
                return;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            LoadData("");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "idebarJoinU9Page")
            {
                LoadData("");
            }
        }

        private void tslNextPage1_Click(object sender, EventArgs e)
        {

        }

        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name != "tsbtnUpdate" && e.ClickedItem.Name != "tsbtnDel")
            {
                LoadData(e.ClickedItem.Name);
            }
        }
        /// <summary>
        /// 绑定采集端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClient_NAME_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCollectionName.DataSource = null;
                if (cmbClient_NAME.Text != "" && cmbClient_NAME.Text != "System.Data.DataRowView")
                {
                    DataTable dt = LinQBaseDao.Query("select Collection_ID,Collection_Name from CollectionInfo where Collection_Client_ID=" + cmbClient_NAME.SelectedValue).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        cmbCollectionName.DataSource = dt;
                        cmbCollectionName.DisplayMember = "Collection_Name";
                        cmbCollectionName.ValueMember = "Collection_ID";
                        cmbCollectionName.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// U9数据传输设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            bool Water = false;

            if (Water1.Checked)
            {
                Water = true;
            }
            else
            {
                Water = false;
            }
            string sql = "update U9Start set U9Bool='" + Water + "'where U9Name='水分检测'   ";
            if (Weight1.Checked)
            {

                Water = true;
            }
            else
            {
                Water = false;
            }
            sql = sql + "update U9Start set U9Bool='" + Water + "'where U9Name='重量检测'   ";
            if (inspection1.Checked)
            {
                Water = true;
            }
            else
            {
                Water = false;
            }
            sql = sql + "update U9Start set U9Bool='" + Water + "'where U9Name='一检管理'   ";
            int count = LinQBaseDao.ExecuteSql(sql);
            if (count > 0)
            {
                MessageBox.Show("保存成功！");

            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        /// <summary>
        /// 绑定仪表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCollectionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCollectionName.SelectedValue != null)
                {
                    string sql = "select Instrument_ID,Instrument_Name from InstrumentInfo where Instrument_Collection_ID='" + cmbCollectionName.SelectedValue + "'";
                    DataTable dt = LinQBaseDao.Query(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        cmbMOISTURENAME.DataSource = dt;
                        cmbMOISTURENAME.DisplayMember = "Instrument_Name";
                        cmbMOISTURENAME.ValueMember = "Instrument_ID";
                        cmbMOISTURENAME.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbMOISTURENAME.DataSource = null;
                    }

                    DataTable dts = LinQBaseDao.Query(sql).Tables[0];
                    if (dts.Rows.Count > 0)
                    {
                        cmbMOISTURENAMETWO.DataSource = dts;
                        cmbMOISTURENAMETWO.DisplayMember = "Instrument_Name";
                        cmbMOISTURENAMETWO.ValueMember = "Instrument_ID";
                        cmbMOISTURENAMETWO.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbMOISTURENAMETWO.DataSource = null;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        //private void txtMoistCount_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string str = txtMoistCount.Text.Trim();
        //        if (!string.IsNullOrEmpty(str))
        //        {
        //            int s = Convert.ToInt32(str);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        txtMoistCount.Text = "";
        //    }
        //}


        private void buttonMoistureDetectionRules_Click(object sender, EventArgs e)
        {
            MoistureDetectionRulesCreator creator = new MoistureDetectionRulesCreator();
            creator.ShowSetupForm(Common.OrganizationID);
        }


        private void label69_Click(object sender, EventArgs e)
        {
        }
        
    }
}
