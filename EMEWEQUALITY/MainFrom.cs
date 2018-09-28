using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEQUALITY.SystemAdmin;
using EMEWEQUALITY.QCAdmin;
using System.Configuration;
using System.Threading;
using System.Diagnostics;
using EMEWEQUALITY.HelpClass;
using EMEWEQUALITY.MyControl;
using EMEWEEntity;
using EMEWEDAL;
using EMEWEQUALITY.Statistics;
using System.Collections;
using System.Runtime.Remoting;
using EMEWEQUALITY.NewAdd;
using EMEWEQUALITY.GetPhoto;
using System.Linq.Expressions;
using System.IO.Ports;

namespace EMEWEQUALITY
{
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
        }

        public Login login;
        public Thread threadDelivers;
        public LED.LEDSHOWB ledShow;
        public string UserID;//用户ID
        public string UserNmae;
        public double weight = 0;//小磅数据重量
        MoistureMeter axMoisture1 = new MoistureMeter();
        MoistureMeter axMoisture2 = new MoistureMeter();
        /// <summary>
        /// 引用正在加载
        /// </summary>
        public Form_Loading form_loading = null;
        public void SETLEDSHOW()
        {
            try
            {
                //System.Diagnostics.Process.Start(Application.StartupPath + @"\WindowLEDSHOW.exe");
                LED.LEDSHOWB led = new LED.LEDSHOWB();
                ledShow = led;
                led.Show();
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("打卡LEDSHOW异常：" + ex.Message);
            }
        }
        public void CloseLEDSHOW()
        {
            try
            {
                //ProcessKill();
                ledShow.Close();
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("关闭LEDSHOW异常：" + ex.Message);
            }
        }

        #region 窗体位置
        /// <summary>
        /// 获取屏幕的分辨率设置几个按钮的位置
        /// </summary>
        private void GetScreen()
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }
        #region
        private void Control()
        {
            int CHeight = this.Height;
            int CWidth = this.Width;

            int ph = (CHeight - 20) / 4;
            int pw = CWidth / 4;
           
            GetRealPlay(4);
        }
        /// <summary>
        /// 用户句柄
        /// </summary>
        private int m_lRealHandle = -1;
        /// <summary>      
        /// 实时视频信息
        /// </summary>   
        /// <param name="channelnum">视频路数</param>  
        private void GetRealPlay(int channelnum)
        {
            try
            {
                #region 方案三
                CHCNetSDK.NET_DVR_CLIENTINFO[] arr = new CHCNetSDK.NET_DVR_CLIENTINFO[channelnum];
                IntPtr pUser = new IntPtr();
                for (int i = 0; i < channelnum; i++)
                {
                    CHCNetSDK.NET_DVR_CLIENTINFO lpClientInfo = new CHCNetSDK.NET_DVR_CLIENTINFO()
                    {
                        lChannel = i + 1,
                        lLinkMode = 0,
                        sMultiCastIP = "",
                        hPlayWnd = Controls.Find("RealPlayWnd" + (i + 1), true)[0].Handle
                    };
                    arr[i] = lpClientInfo;
                    m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V30(m_lUserID, ref arr[i], null, pUser, 1);
                    if (this.m_lRealHandle == -1)
                    {
                        uint nError = CHCNetSDK.NET_DVR_GetLastError();
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        /// <summary>
        /// 子窗体的高度
        /// </summary>
        int VisibleHeight = 0;
        /// <summary>
        /// 子窗体的宽度
        /// </summary>
        int VisibleWidth = 0;
        /// <summary>
        /// 当前打开的子窗体
        /// </summary>
        public Form OpenForm;
        public void ShowChildForm(Form childFrm)
        {
            try
            {
                #region 周意
                TraverseFormCtrlAndBindTree(childFrm);//绑定权限
                #endregion
                this.CloseOpenForm();
                this.OpenForm = childFrm;
                childFrm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                childFrm.Owner = this;
                childFrm.ShowIcon = false;
                childFrm.ShowInTaskbar = false;
                // childFrm.DoubleBuffered = true;
                childFrm.StartPosition = FormStartPosition.CenterScreen;
                childFrm.Show();
            }
            catch (Exception)
            {

            }

            //  childFrm.Location = new Point((this.Width - menuStrip1.Width - childFrm.Width) / 2 + menuStrip1.Width, (this.Height - childFrm.Height) / 2);
        }
        /// <summary>
        /// 关闭当前打开的窗体
        /// </summary>
        public void CloseOpenForm()
        {
            if (OpenForm != null)
                OpenForm.Close();
        }
        #endregion
        private void MainFrom_Load(object sender, EventArgs e)
        {

             lab_Version.Text = Common.version ;//设置软件版本号
            form_loading = new Form_Loading();
            form_loading.ShowLoading(this);
            form_loading.Text = "初始化系统配置...";
            // this.skinEngine1.SkinFile = "DiamondBlue.ssk";
            this.Text += " (" + Common.OrganizationID + ") " + Common.Version;
            pictureBox1.Size = new Size(24, 24);
            pictureBox2.Size = new Size(24, 24);
            timer1.Start();
            timer2.Start();
            timer3.Start();
            GetScreen();
            LoadSubMenu1();//判断权限
            try
            {
                lblUserName.Text += Common.NAME;
                lblClientName.Text += Common.CLIENT_NAME;
                SetPrivert();

                // SETLEDSHOW();
                //threadDelivers = new Thread(new ThreadStart(SETLEDSHOW));
                //threadDelivers.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("串口链接失败！无法自动控制", "运行信息", MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
                return;
            }

            try
            {
                InitSDK();
                SetLogin();
            }
            catch (Exception ex)
            {

                MessageBox.Show(" 登陆硬盘录像机失败！" + ex.Message);
                return;
            }
            //  Control();
            form_loading.CloseLoading(this);
        }
        int m_lUserID = -1;
        /// <summary>
        /// 登录硬盘录像机(默认)
        /// </summary>
        public void SetLogin()
        {
            string DVRIPAddress = Common.DVRIP;
            short DVRPortNumber = short.Parse(Common.DVRServerPort);
            string DVRUserName = Common.DVRLoginName;
            string DVRPassword = Common.DVRPwd;
            m_lUserID = SDKCommon.SetLogin(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword);
            if (m_lUserID != -1)
            {
                //DebugInfo("登录硬盘录像机成功！");
            }
            else
            {
                //DebugInfo("登录硬盘录像机失败！");
                return;
            }

        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitSDK()
        {
            if (!SDKCommon.InitSDK())
            {
                MessageBox.Show("硬盘录像机初始化失败！");
            }
        }
        /// <summary>
        /// 账户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIUser_Click(object sender, EventArgs e)
        {
            UserAdmin userAdmin = new UserAdmin(UserNmae);
            ShowChildForm(userAdmin);
        }
        /// <summary>
        /// 字典管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIDictionary_Click(object sender, EventArgs e)
        {
            DictionaryAdmin dictionaryAdmin = new DictionaryAdmin();
            dictionaryAdmin.mf = this;

            ShowChildForm(dictionaryAdmin);
        }

        /// <summary>
        /// 串口设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIPParity_Click(object sender, EventArgs e)
        {
            EMEWEQUALITY.SystemAdmin.SystemSet systemSet = new EMEWEQUALITY.SystemAdmin.SystemSet();
            systemSet.mf = this;
            systemSet.tabControl1.SelectedIndex = 0;
            ShowChildForm(systemSet);
        }

        /// <summary>
        /// LED设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMILED_Click(object sender, EventArgs e)
        {
            EMEWEQUALITY.SystemAdmin.SystemSet systemSet = new EMEWEQUALITY.SystemAdmin.SystemSet();
            systemSet.mf = this;//.SelectedTabIndex = 1;
            systemSet.tabControl1.SelectedIndex = 1;
            ShowChildForm(systemSet);
        }

        /// <summary>
        /// 打印标题设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIUpdateQCTime_Click(object sender, EventArgs e)
        {
            EMEWEQUALITY.SystemAdmin.SystemSet systemSet = new EMEWEQUALITY.SystemAdmin.SystemSet();
            systemSet.tabControl1.SelectedIndex = 2;
            ShowChildForm(systemSet);
        }

        /// <summary>
        /// 初始化设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIPunch_Click(object sender, EventArgs e)
        {
            EMEWEQUALITY.SystemAdmin.SystemSet systemSet = new EMEWEQUALITY.SystemAdmin.SystemSet();
            systemSet.mf = this;
            systemSet.tabControl1.SelectedIndex = 3;
            ShowChildForm(systemSet);
        }

        /// <summary>
        /// 重量检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIFormWeight_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormWeight.fw == null)
                {
                    FormWeight.fw = new FormWeight();
                    FormWeight.fw.mf = this;
                    TraverseFormCtrlAndBindTree(FormWeight.fw);
                    FormWeight.fw.Show();
                }
                else
                {
                    FormWeight.fw.Activate();
                }
            }
            catch(Exception err)
            {

                Common.WriteTextLog("重量检测窗体加载异常：" + err.Message);
            }
            //FormWeight frmWeight = new FormWeight();
            //frmWeight.mf = this;
            //ShowChildForm(frmWeight);
        }

        /// <summary>
        ///周转区水分检测 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIWate_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormWate.fwt == null)
                {
                    FormWate.fwt = new FormWate();
                    FormWate.fwt.mf = this;
                    TraverseFormCtrlAndBindTree(FormWate.fwt);
                    FormWate.fwt.Show();
                }
                else
                {
                    FormWate.fwt.Activate();
                }
            }
            catch
            {

            }
            //FormWate frmWate = new FormWate();
            //frmWate.mf = this;
            //ShowChildForm(frmWate);
        }

        #region 公共方法 2011.11.24
        /// <summary>
        /// 显示工具提示
        /// </summary>
        /// <param name="tti">ToolTipIcon.Info、None、Warning、Error</param>
        /// <param name="str">工具提示标题（ToolTipTitle）</param>
        /// <param name="strMessage">提示信息</param>
        /// <param name="controlName">提示框显示的控件</param>
        /// <param name="form">提示框显示的窗体</param>
        public void ShowToolTip(ToolTipIcon tti, string strTitle, string strMessage, Control controlName, Form form)
        {
            if (!form.IsDisposed)
            {
                toolTip1.ToolTipIcon = tti;//ToolTipIcon.Error;
                toolTip1.ToolTipTitle = strTitle;
                Point showLocation = new Point(
                    controlName.Location.X + controlName.Width,
                    controlName.Location.Y);
                toolTip1.Show(strMessage, form, showLocation, 5000);
                //controlName.SelectAll();
                controlName.Focus();
            }
        }

        /// <summary>
        /// 验证卡号是否有权限
        /// </summary>
        /// <param name="conrol">控件</param>
        /// <param name="userid">获得用户编号</param>
        /// <returns></returns>
        public bool VerificationCard(CardControl conrol, Form form, out int userid)
        {
            bool rbool = true;
            userid = 0;
            int UserID = 0;
            int RoleID = 0;
            if (conrol.Users == null)
            {
                this.ShowToolTip(ToolTipIcon.Warning, "确认修改", "卡号不能为空！", conrol, form);
                return false; ;
            }
            else
            { //判断是否有权限修改


                UserInfo userinfo = conrol.Users;
                if (userinfo != null)
                    userid = userinfo.UserId;
                UserID = userinfo.UserId;
                if (userid > 0)
                {
                    //--------------根据用户ID查询出角色ID----------------------------------------------
                    Expression<Func<View_UserInfo_D_R_d, bool>> funroleinfo = n => n.UserId == UserID;
                    var view_UserInfo_D_R_d = UserInfoDAL.Query(funroleinfo);
                    if (view_UserInfo_D_R_d != null)
                    {
                        foreach (var m in view_UserInfo_D_R_d)
                        {
                            if (m.Role_Id > 0)
                            {
                                RoleID = m.Role_Id.Value;
                            }
                            break;

                        }
                    }
                    if (VerificationCard(form, UserID, RoleID))
                    {
                        conrol.ISPass = true;
                        this.ShowToolTip(ToolTipIcon.Warning, "确认修改", "请刷有修改权限的卡！", conrol, form);
                        rbool = false;
                    }
                    else
                    {
                        conrol.TXTCARD = userinfo.UserName;
                        conrol.ISPass = false;
                    }


                }
            }
            return rbool;
        }

        #endregion
        public bool VerificationCard(Form form, int userid, int roleid)
        {
            bool rbool = true;
            //----------------------根据窗体名称查出ID--------------------------------------------
            Expression<Func<View_MenuInfoRole, bool>> func = n => n.Menu_FromName == form.Name;
            var view_Menu_ControlRole = MenuInfoDAL.QueryView_MenuInfoRole(func);
            if (view_Menu_ControlRole == null)
            {
                rbool = true;
            }
            foreach (var vm in view_Menu_ControlRole)
            {
                if (vm.Menu_ID > 0)
                {
                    Expression<Func<View_Menu_ControlRole, bool>> funcView_Menu_ControlRoleRole_Id = n => n.Menu_OtherID == vm.Menu_ID && n.Permissions_Role_Id == roleid;
                    if (MenuInfoDAL.QueryView_Menu_ControlRole(funcView_Menu_ControlRoleRole_Id) != null)//角色权限
                    {
                        var view_Menu_ControlRoleRole_Id = MenuInfoDAL.QueryView_Menu_ControlRole(funcView_Menu_ControlRoleRole_Id);
                        if (view_Menu_ControlRoleRole_Id != null)
                        {
                            foreach (var m in view_Menu_ControlRoleRole_Id)
                            {
                                if (m.Menu_ControlName != null)
                                {
                                    if (m.Menu_ControlName == "btnOFFQcRecord")
                                    {
                                        rbool = false;
                                        break;
                                    }
                                }
                            }

                        }
                        //TraverseFormCtrlAndBindTreerole(form);
                    }
                    Expression<Func<View_Menu_ControlRole, bool>> funcView_Menu_ControlRoleUserId = n => n.Menu_OtherID == vm.Menu_ID && n.Permissions_UserId == userid;
                    if (MenuInfoDAL.QueryView_Menu_ControlRole(funcView_Menu_ControlRoleUserId) != null)//用户权限
                    {
                        var view_Menu_ControlRoleUserId = MenuInfoDAL.QueryView_Menu_ControlRole(funcView_Menu_ControlRoleUserId);
                        if (view_Menu_ControlRoleUserId != null)
                        {
                            foreach (var m in view_Menu_ControlRoleUserId)
                            {
                                if (m.Menu_ControlName != null)
                                {
                                    if (m.Menu_ControlName == "btnOFFQcRecord")
                                    {
                                        rbool = false;
                                        break;
                                    }
                                }
                            }

                        }
                    }

                }
                break;
            }
            return rbool;
        }

        /// <summary>
        /// 一检管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIOneQCAdmin_Click(object sender, EventArgs e)
        {
            OneQCAdmin oneQCAdmin = new OneQCAdmin();
            ShowChildForm(oneQCAdmin);
        }

        WeightSerialPort w = null;
        WeightSerialPort w2 = null;
        #region 串口
        /// <summary>
        /// 打开串口
        /// </summary>
        public void SetPrivert()
        {
            //打开小磅1端口
            if (Common.WEIGHTCOM > 0)
            {
                w = new WeightSerialPort();
                w.platformScaleName = Common.platformScaleName;
                w.Open(Common.WEIGHTCOMBaudRate, "COM" + Common.WEIGHTCOM.ToString(), 8);
            }
            //打开小磅2端口
            if (Common.WEIGHTCOMTwo > 0)
            {
                w2 = new WeightSerialPort();
                w2.platformScaleName = Common.platformScaleNameTwo;
                w2.Open(Common.WEIGHTCOMTwoBaudRate, "COM" + Common.WEIGHTCOMTwo.ToString(), 8);
            }

            // this.pictureBox1.BringToFront();

            //水分1
            lblNum1.Text = Common.MOISTURENAME+"状态";
            lblNum2.Text = Common.MOISTURENAMETwo + "状态";
            axMoisture1.OpenCom(Common.MOISTURECOM, 9600, StopBits.One, 8, Parity.None);
            axMoisture2.OpenCom(Common.MOISTURECOMTwo, 9600, StopBits.One, 8, Parity.None);
        }

        #region 水分仪
        public string strWater = "";
        public delegate void myDelegate();

        public void bluetoothsucceed()
        {
            lblState.Text = "已连接";
            pictureBox1.BackgroundImage = Properties.Resources.bluetooth1;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }


        public void bluetoothfail()
        {
            lblState.Text = "未连接";
            pictureBox1.BackgroundImage = Properties.Resources.bluetooth2;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;

        }

        //第二个蓝牙
        public void bluetoothsucceedTwo()
        {
            lblState2.Text = "已连接";
            pictureBox2.BackgroundImage = Properties.Resources.bluetooth1;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
        }


        public void bluetoothfailTwo()
        {
            lblState2.Text = "未连接";
            pictureBox2.BackgroundImage = Properties.Resources.bluetooth2;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        #endregion

        #endregion
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (MessageBox.Show("确定退出系统？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            //{
            //}
            try
            {
                this.CloseOpenForm();
                if (login != null)
                {
                    login.Close();
                }
                if (ledShow != null)
                {
                    ledShow.Close();
                }

                //this.Close();
                //this.Dispose();
                ProcessKill();
                GC.Collect();
                Application.ExitThread();
                Application.Exit();
                Process.GetCurrentProcess().Kill();
                //System.Environment.Exit(System.Environment.ExitCode);
                //Application.ExitThread();

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainForm.pbExitSystem_Click:" + ex.Message.ToString());
                Process.GetCurrentProcess().Kill();
            }
        }

        /// <summary>
        /// 杀死进程
        /// </summary>
        private void ProcessKill()
        {
            try
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.ProcessName.ToLower().StartsWith("windowledshow"))
                    {
                        p.Kill();//杀死该进程
                    }
                }

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainForm.ProcessKill:" + ex.Message);
            }
        }

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIRole_Click(object sender, EventArgs e)
        {
            UserRole userRole = new UserRole();
            //userRole.mf = this;
            ShowChildForm(userRole);
        }
        /// <summary>
        /// 车辆登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMICarRegister_Click(object sender, EventArgs e)
        {
            FormCarRegister fc = new FormCarRegister();
            ShowChildForm(fc);
            //FormCarRegister fc = new FormCarRegister();
            //ShowChildForm(fc);
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIExits_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定退出系统？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    this.CloseOpenForm();
                    if (login != null)
                    {
                        login.Close();
                    }
                    if (ledShow != null)
                    {
                        ledShow.Close();
                    }

                    this.Close();
                    // ProcessKill();
                    GC.Collect();
                    Application.ExitThread();
                    Application.Exit();
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainForm.pbExitSystem_Click:" + ex.Message.ToString());
                Process.GetCurrentProcess().Kill();
            }
        }

        private void 系统日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemOperate so = new SystemOperate();
            ShowChildForm(so);
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPassWord spw = new SetPassWord();
            ShowChildForm(spw);
        }

        private void 值班管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Beonduty_GuanLǐ bd = new Beonduty_GuanLǐ();
            ShowChildForm(bd);
        }

        /// <summary>
        /// 车辆质检统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMICarStatistics_Click(object sender, EventArgs e)
        {
            CarStatisticsForm csf = new CarStatisticsForm();
            ShowChildForm(csf);

        }
        /// <summary>
        /// 质检数据统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIQCStatistics_Click(object sender, EventArgs e)
        {
            QCStatisticsForm qcf = new QCStatisticsForm();
            ShowChildForm(qcf);
        }


        /// <summary>
        /// 修改数据统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIUpdateQCStatistics_Click(object sender, EventArgs e)
        {
            UpdateQCStatisticsForm uqf = new UpdateQCStatisticsForm();
            ShowChildForm(uqf);
        }

        private void LoadSubMenu1()
        {
            foreach (ToolStripMenuItem MenuItem2 in menuStrip1.Items)//遍历menustrip的 一级菜单并将其显示到树上
            {
                YesNoVisibleEnabled(MenuItem2);//判断权限
                Menuitem(MenuItem2);//
            }
        }

        /// <summary>
        /// 遍历子菜单
        /// </summary>
        /// <param name="MenuItem2"></param>
        private void Menuitem(ToolStripMenuItem MenuItem2)
        {
            foreach (ToolStripMenuItem menuitm in MenuItem2.DropDownItems)
            {
                YesNoVisibleEnabled(menuitm);
                Menuitem(menuitm);//递归
            }
        }


        /// <summary>
        /// 判断是否可见或可操作[菜单]
        /// </summary>
        /// <param name="menuitm"></param>
        private void YesNoVisibleEnabled(ToolStripMenuItem menuitm)
        {
            try
            {
                if (Common.USERNAME != "emewe")
                {
                    Expression<Func<View_MenuInfoRole, bool>> fun = n => n.Permissions_Role_Id == Common.ROLE || n.Permissions_UserId.ToString() == Common.USERID;
                    var ie = MenuInfoDAL.QueryView_MenuInfoRole(fun);
                    if (ie == null) return;
                    if (menuitm != null)
                    {
                        foreach (var m in ie)
                        {
                            if (!string.IsNullOrEmpty(m.Menu_ControlText))//判断取出的会值不为空
                            {
                                //Console.WriteLine(menuitm.Text);
                                if (menuitm.Text == m.Menu_ControlText)//判断取出的菜单值与当前绑定的一致
                                {
                                    if (m.Permissions_Visible.Value)
                                    {
                                        menuitm.Visible = true;
                                        if (m.Permissions_Enabled.Value)
                                        {
                                            menuitm.Enabled = true;
                                        }
                                        else
                                        {
                                            menuitm.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        menuitm.Visible = false;
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    if (menuitm != null)
                    {
                        menuitm.Visible = true;
                        menuitm.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainFrom.YesNoVisibleEnabled()" + ex.Message.ToString());
            }

        }
        IEnumerable<View_Menu_ControlRole> iev = null;
        IEnumerable<MenuInfo> iev1 = null;

        private void TraverseFormCtrlAndBindTree(Form form)
        {
            try
            {
                int menuid = 0;

                //----------------------根据窗体名称查出ID--------------------------------------------
                if (Common.USERNAME == "emewe")
                {

                    TraverseFormCtrlAndBindTreerole1(form);
                }
                else
                {
                    Expression<Func<View_MenuInfoRole, bool>> func = n => n.Menu_FromName == form.Name;
                    var view_Menu_ControlRole = MenuInfoDAL.QueryView_MenuInfoRole(func);
                    if (view_Menu_ControlRole == null) return;
                    foreach (var vm in view_Menu_ControlRole)
                    {
                        if (vm.Menu_ID > 0)
                        {
                            menuid = vm.Menu_ID.Value;
                            Expression<Func<View_Menu_ControlRole, bool>> funcView_Menu_ControlRoleRole_Id = n => n.Menu_OtherID == menuid && n.Permissions_Role_Id == Common.ROLE;
                            if (MenuInfoDAL.QueryView_Menu_ControlRole(funcView_Menu_ControlRoleRole_Id) != null)//角色权限
                            {
                                iev = MenuInfoDAL.QueryView_Menu_ControlRole(funcView_Menu_ControlRoleRole_Id);
                                TraverseFormCtrlAndBindTreerole(form);
                            }
                            Expression<Func<View_Menu_ControlRole, bool>> funcView_Menu_ControlRoleUserId = n => n.Menu_OtherID == menuid && n.Permissions_UserId.ToString() == Common.USERID;
                            if (MenuInfoDAL.QueryView_Menu_ControlRole(funcView_Menu_ControlRoleUserId) != null)//用户权限
                            {
                                iev = MenuInfoDAL.QueryView_Menu_ControlRole(funcView_Menu_ControlRoleUserId);
                                TraverseFormCtrlAndBindTreerole(form);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainFrom.TraverseFormCtrlAndBindTree()" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 递归遍历一个Form中的控件是否为button,或者是toopstrip中的toolstripbutton，如果是将基显示到树上，
        /// </summary>
        /// <param name="ctrl">窗体的控件</param>
        private void TraverseFormCtrlAndBindTreerole(Form form)
        {
            try
            {
                if (iev == null) return;


                //-------------Button 控件权限判断------------------------------------------------------
                foreach (Control c in form.Controls)//遍历窗体中的控件
                {
                    if (c is Button) //判断是否为Button
                    {


                        foreach (var m in iev.Where(n => n.MenuType_Name == "Button"))
                        {

                            if (!string.IsNullOrEmpty(m.Menu_ControlName))//判断取出的会值不为空
                            {

                                if (c.Name == m.Menu_ControlName)//判断取出的菜单值与当前绑定的一致
                                {


                                    if (m.Permissions_Visible.Value == true)//判断是否可见
                                    {
                                        c.Visible = true;

                                        if (m.Permissions_Enabled.Value == true)
                                        {
                                            c.Enabled = true;
                                        }
                                        else
                                        {
                                            c.Enabled = false;
                                        }

                                    }
                                    else
                                    {
                                        c.Visible = false;
                                    }

                                }


                            }

                        }

                    }

                    //-------------------------------BindingNavigator 控件权限判断-----------------------------------
                    if (c is BindingNavigator) //判断是否为BindingNavigator
                    {
                        BindingNavigator BN = (BindingNavigator)c;

                        for (int i = 0; i < BN.Items.Count; i++)
                        {

                            if (BN.Items[i].GetType().ToString() == "System.Windows.Forms.ToolStripButton")//判断是否为ToolStripButton
                            {
                                foreach (var m in iev.Where(n => n.MenuType_Name == "ToolStripButton"))
                                {
                                    if (!string.IsNullOrEmpty(m.Menu_ControlName))//判断取出的会值不为空
                                    {
                                        if (BN.Items[i].Name == m.Menu_ControlName)
                                        {
                                            if (m.Permissions_Visible.Value == true)//判断是否可见
                                            {
                                                BN.Items[i].Visible = true;

                                                //if (m.Menu_Enabled.Value != null)//判断取出的会值不为空
                                                //{
                                                if (m.Permissions_Enabled.Value == true)
                                                {
                                                    BN.Items[i].Enabled = true;
                                                }
                                                else
                                                {
                                                    BN.Items[i].Enabled = false;
                                                }
                                                //}
                                            }
                                            else
                                            {
                                                BN.Items[i].Visible = false;
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }
                    //-------------------------GroupBox-----------------------------------------------------------------------------
                    if (c is GroupBox)
                    {

                        TraverseFormCtrlAndBindTreeDiGui(c);
                    }
                    //----------------------------TabControl--------------------------------------------
                    if (c is TabControl) //判断是否为TabControl
                    {
                        TabControl tc = (TabControl)c;//将c强强制转为为TabControl类型
                        foreach (TabPage tp in tc.TabPages)//遍历TabControl中的TabPage选项
                        {
                            TraverseFormCtrlAndBindTreeDiGui(tp);
                        }
                    }
                    //-------------------------Panel---------------------------------------------
                    if (c is Panel)
                    {
                        TraverseFormCtrlAndBindTreeDiGui(c);
                    }

                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainFrom.TraverseFormCtrlAndBindTreerole()" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 递归遍历一个Form中的控件是否为button,或者是toopstrip中的toolstripbutton，如果是将基显示到树上，
        /// </summary>
        /// <param name="ctrl">窗体的控件</param>
        private void TraverseFormCtrlAndBindTreerole1(Form form)
        {

            try
            {
                //if (iev1 == null) return;


                //-------------Button 控件权限判断------------------------------------------------------
                foreach (Control c in form.Controls)//遍历窗体中的控件
                {
                    if (c is Button) //判断是否为Button
                    {
                        c.Visible = true;

                        c.Enabled = true;


                    }

                    //-------------------------------BindingNavigator 控件权限判断-----------------------------------
                    if (c is BindingNavigator) //判断是否为BindingNavigator
                    {
                        BindingNavigator BN = (BindingNavigator)c;

                        for (int i = 0; i < BN.Items.Count; i++)
                        {

                            if (BN.Items[i].GetType().ToString() == "System.Windows.Forms.ToolStripButton")//判断是否为ToolStripButton
                            {

                                BN.Items[i].Visible = true;


                                BN.Items[i].Enabled = true;


                            }


                        }
                    }
                    //-------------------------GroupBox-----------------------------------------------------------------------------
                    if (c is GroupBox)
                    {

                        TraverseFormCtrlAndBindTreeDiGui1(c);
                    }
                    //----------------------------TabControl--------------------------------------------
                    if (c is TabControl) //判断是否为TabControl
                    {
                        TabControl tc = (TabControl)c;//将c强强制转为为TabControl类型
                        foreach (TabPage tp in tc.TabPages)//遍历TabControl中的TabPage选项
                        {
                            TraverseFormCtrlAndBindTreeDiGui1(tp);
                        }
                    }
                    //-------------------------Panel---------------------------------------------
                    if (c is Panel)
                    {
                        TraverseFormCtrlAndBindTreeDiGui1(c);
                    }


                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainFrom.TraverseFormCtrlAndBindTreerole1()" + ex.Message.ToString());
            }
        }

        private void TraverseFormCtrlAndBindTreeDiGui(Control C)
        {
            try
            {
                foreach (Control c in C.Controls)//遍历窗体中的控件
                {

                    if (c is Button) //判断是否为Button
                    {

                        foreach (var m in iev.Where(n => n.MenuType_Name == "Button"))
                        {

                            if (!string.IsNullOrEmpty(m.Menu_ControlText))//判断取出的会值不为空
                            {

                                if (c.Name == m.Menu_ControlName)//判断取出的菜单值与当前绑定的一致
                                {

                                    if (m.Permissions_Visible.Value == true)//判断是否可见
                                    {
                                        c.Visible = true;

                                        if (m.Permissions_Enabled.Value == true)
                                        {
                                            c.Enabled = true;
                                        }
                                        else
                                        {
                                            c.Enabled = false;
                                        }

                                    }
                                    else
                                    {
                                        c.Visible = false;
                                    }

                                }


                            }

                        }
                    }
                    //-------------------------------BindingNavigator 控件权限判断-----------------------------------
                    if (c is BindingNavigator) //判断是否为BindingNavigator
                    {
                        BindingNavigator BN = (BindingNavigator)c;

                        for (int i = 0; i < BN.Items.Count; i++)
                        {
                            if (BN.Items[i].GetType().ToString() == "System.Windows.Forms.ToolStripButton")//判断是否为ToolStripButton
                            {
                                foreach (var m in iev.Where(n => n.MenuType_Name == "ToolStripButton"))
                                {
                                    if (!string.IsNullOrEmpty(m.Menu_ControlText))//判断取出的会值不为空
                                    {
                                        if (BN.Items[i].Name == m.Menu_ControlName)
                                        {
                                            if (m.Permissions_Visible.Value == true)//判断是否可见
                                            {
                                                BN.Items[i].Visible = true;

                                                //if (m.Menu_Enabled.Value != null)//判断取出的会值不为空
                                                //{
                                                if (m.Permissions_Enabled.Value == true)
                                                {
                                                    BN.Items[i].Enabled = true;
                                                }
                                                else
                                                {
                                                    BN.Items[i].Enabled = false;
                                                }
                                                //}
                                            }
                                            else
                                            {
                                                BN.Items[i].Visible = false;
                                            }
                                        }

                                    }
                                }

                            }
                        }


                    }
                    //-----------------GroupBox----------------------------------------
                    if (c is GroupBox)
                    {

                        TraverseFormCtrlAndBindTreeDiGui(c);
                    }
                    //------------------TabControl----------------------------------------------------
                    if (c is TabControl) //判断是否为TabControl
                    {
                        TabControl tc = (TabControl)c;//将c强强制转为为TabControl类型
                        foreach (TabPage tp in tc.TabPages)//遍历TabControl中的TabPage选项
                        {
                            TraverseFormCtrlAndBindTreeDiGui(tp);
                        }
                    }
                    //-------------------------Panel---------------------------------------------
                    if (c is Panel)
                    {
                        TraverseFormCtrlAndBindTreeDiGui(c);
                    }

                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainFrom.TraverseFormCtrlAndBindTreeDiGui()" + ex.Message.ToString());
            }


        }
        private void TraverseFormCtrlAndBindTreeDiGui1(Control C)
        {
            try
            {
                foreach (Control c in C.Controls)//遍历窗体中的控件
                {

                    if (c is Button) //判断是否为Button
                    {
                        c.Visible = true;
                        c.Enabled = true;
                    }
                    //-------------------------------BindingNavigator 控件权限判断-----------------------------------
                    if (c is BindingNavigator) //判断是否为BindingNavigator
                    {
                        BindingNavigator BN = (BindingNavigator)c;

                        for (int i = 0; i < BN.Items.Count; i++)
                        {
                            if (BN.Items[i].GetType().ToString() == "System.Windows.Forms.ToolStripButton")//判断是否为ToolStripButton
                            {
                                //foreach (var m in iev1)
                                //{
                                //    if (!string.IsNullOrEmpty(m.Menu_ControlText))//判断取出的会值不为空
                                //    {
                                //        if (BN.Items[i].Name == m.Menu_ControlName)
                                //        {

                                BN.Items[i].Visible = true;

                                BN.Items[i].Enabled = true;

                                //        }
                                //    }

                                //}
                            }


                        }
                    }
                    //-----------------GroupBox----------------------------------------
                    if (c is GroupBox)
                    {

                        TraverseFormCtrlAndBindTreeDiGui1(c);
                    }
                    //------------------TabControl----------------------------------------------------
                    if (c is TabControl) //判断是否为TabControl
                    {
                        TabControl tc = (TabControl)c;//将c强强制转为为TabControl类型
                        foreach (TabPage tp in tc.TabPages)//遍历TabControl中的TabPage选项
                        {
                            TraverseFormCtrlAndBindTreeDiGui1(tp);
                        }
                    }
                    //-------------------------Panel---------------------------------------------
                    if (c is Panel)
                    {
                        TraverseFormCtrlAndBindTreeDiGui1(c);
                    }

                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("MainFrom.TraverseFormCtrlAndBindTreeDiGui1()" + ex.Message.ToString());
            }




        }


        #region 预置皮重弹出窗体 、 检测项目弹出窗体
        /// <summary>
        /// 预置皮重弹出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPresetTare_Click(object sender, EventArgs e)
        {
            PresetTareForm ptf = new PresetTareForm();
            ptf.mf = this;
            ShowChildForm(ptf);

        }
        /// <summary>
        /// 检测项目弹出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiTestItems_Click(object sender, EventArgs e)
        {
            TestItemsForm testitems = new TestItemsForm();
            ShowChildForm(testitems);
        }
        /// <summary>
        /// 手动修改质检信息弹出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 手动修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateQC uqc = new frmUpdateQC();
            ShowChildForm(uqc);
        }
        #endregion

        /// <summary>
        ///接收水分测试界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiWateTest_Click(object sender, EventArgs e)
        {
            frmWateTest frm = new frmWateTest();
            frm.mf = this;
            ShowChildForm(frm);
        }
        /// <summary>
        /// 接收重量测试界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiWeightTest_Click(object sender, EventArgs e)
        {
            frmWeightTest frm = new frmWeightTest();
            frm.mf = this;
            ShowChildForm(frm);
        }

        private void 退货统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReturnStillPackForm rspf = new ReturnStillPackForm();
            rspf.mf = this;
            ShowChildForm(rspf);
        }
        #region 唐磊
        /// <summary>
        /// 菜单管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMIMenu_Click(object sender, EventArgs e)
        {
            MenuInfoManager mim = new MenuInfoManager();
            ShowChildForm(mim);
        }
        #endregion

        #region 2012-11-14
        /// <summary>
        /// 判断是否是当前客户端，如果不是，点击对话框“Yse”进行修改客户端，点击“No”不进行修改客户不进行检测
        /// </summary>
        /// <param name="strQcinfoId">当前QCInfoID</param>
        /// <returns>true当前客户端或已修改为当前客户，false不是当前客户端</returns>
        public bool ISCurrentClientAndUpdateClient(object strQcinfoId)
        {
            string strClient = "";
            bool rbool = true;
            if (!string.IsNullOrEmpty(strQcinfoId.ToString()))
            {
                object obj = LinQBaseDao.GetSingle("select QCInfo_Client_ID from QCInfo where QCInfo_ID=" + strQcinfoId.ToString());
                if (obj != null)
                {
                    strClient = obj.ToString();

                    if (!string.IsNullOrEmpty(strClient))
                    {

                        if (strClient.Trim() != Common.CLIENTID.ToString())
                        {
                            rbool = false;
                            if (MessageBox.Show("是否更改为前客户端？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                rbool = true;
                                // QCInfo
                                string strUpdate = "update QCInfo set  QCInfo_Client_ID=" + Common.CLIENTID + " where QCInfo_ID=" + strQcinfoId.ToString();
                                LinQBaseDao.ExecuteSql(strUpdate);

                            }

                        }

                    }
                }
            }

            return rbool;
        }
        #endregion

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserAdmin2 userAdmin = new UserAdmin2(UserNmae);
            //ControlName = "UserAdmin";

            ShowChildForm(userAdmin);
        }

        private void 接口表管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsetfaceFrom rspf = new InsetfaceFrom();
            rspf.mf = this;
            ShowChildForm(rspf);
        }

        private void 水分检测设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormQCWateSet f = new FormQCWateSet();
            f.mf = this;
            ShowChildForm(f);
        }

        private void 异常信息处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExceptionHandlingForm ehf = new ExceptionHandlingForm();
            ehf.mf = this;
            ShowChildForm(ehf);
        }

        private void 短信设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SMSSendSetForm ssf = new SMSSendSetForm();
            ssf.mf = this;
            ShowChildForm(ssf);
        }

        private void 检测项目异常标准ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnusualstandardForm u = new UnusualstandardForm();
            u.mf = this;
            ShowChildForm(u);
        }

        private void 异常短信记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SMSSendRecordForm srf = new SMSSendRecordForm();
            srf.mf = this;
            ShowChildForm(srf);
        }

        private void 异常照片记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintRecordForm prf = new PrintRecordForm();
            prf.mf = this;
            ShowChildForm(prf);
        }

        private void 手动发送短信ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SMSManualSendForm smsf = new SMSManualSendForm();
            smsf.mf = this;
            ShowChildForm(smsf);

        }


        //小磅是否有新数据

        //小磅1
        /// <summary>
        /// 1号磅状态
        /// </summary>
        public bool isNewWeight;
        /// <summary>
        /// 1号磅重量
        /// </summary>
        public string NewWeight = "";//重量
        //小磅2
        /// <summary>
        /// 2号磅状态
        /// </summary>
        public bool isNewWeight2;
        /// <summary>
        /// 2号磅重量
        /// </summary>
        public string NewWeight2 = "";//重量
        private void timer1_Tick(object sender, EventArgs e)
        {
            //小磅1
            if (w.isNewValue)
            {
                isNewWeight = true;
                NewWeight = w.oldValue;
            }
            else
            {
                isNewWeight = false;
                NewWeight = "";
            }

            //小磅2
            if (w2.isNewValue)
            {
                isNewWeight2 = true;
                NewWeight2 = w2.oldValue;

            }
            else
            {
                isNewWeight2 = false;
                NewWeight2 = "";
            }
        }

        private void 抽包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PacktsForm pf = new PacktsForm();
            pf.mf = this;
            ShowChildForm(pf);
        }

        private void 重量检测设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormWeightSet fws = new FormWeightSet();
            fws.mf = this;
            ShowChildForm(fws);
        }

        private void 客户端管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientForm ci = new ClientForm();
            ci.mf = this;
            ShowChildForm(ci);
        }

        private void 采集端管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollectionForm clf = new CollectionForm();
            clf.mf = this;
            ShowChildForm(clf);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (axMoisture1.ISOpen)
            {
                serial();
                bluetoothsucceed();
            }
            else
            {
                bluetoothfail();
            }
        }
        int couT = 0;
        private void serialT()
        {
            if (axMoisture2.count > 0 && couT != axMoisture2.count)
            {
                ////水分结果
                try
                {
                    string water = axMoisture2.Water;
                    couT = axMoisture2.count;
                    strWater = DateTime.Now.ToString() + " 序号：" + couT + " 次数：" + couT + " 水分值：" + water;
                    string strwater = GetData.GetStringMath(water.ToString(), 1);
                    int numwater = 0;
                    int moist = 2;
                    DataTable dt = LinQBaseDao.Query("select Instrument_Type,Instrument_ID from InstrumentInfo where Instrument_Collection_ID='" + Common.CollectionID + "' and  Instrument_Name='" + Common.MOISTURENAMETwo + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        numwater = Common.GetInt(dt.Rows[0][0]);
                        moist = Common.GetInt(dt.Rows[0][1]);
                    }
                    LinQBaseDao.Query("insert into Moisture_test (Moisture_test_Collection_ID,	Moistrue_test_Instrument_ID	,Moisture_test_value	,Moisture_test_NO,	Moisture_test_count,Moisture_test_status,	Moisture_test_Time,Moisture_test_Numwater) values(" + Common.CollectionID + ",'" + moist + "'," + strwater + "," + couT + "," + strwater + ",0,getdate(),'" + numwater + "')");
                }
                catch (Exception ex)
                {
                    Common.WriteTextLogWate("serialT异常：" + ex.Message.ToString());
                }
            }
        }
        int cou = 0;
        private void serial()
        {
            ////水分结果
            try
            {
                if (axMoisture1.count > 0 && cou != axMoisture1.count)
                {
                    string water = axMoisture1.Water;//水分值
                    cou = axMoisture1.count;
                    strWater = DateTime.Now.ToString() + " 序号：" + cou + " 次数：" + cou + " 水分值：" + water;
                    string strwater = GetData.GetStringMath(water.ToString(), 1);
                    int numwater = 0;
                    int moist = 1;
                    DataTable dt = LinQBaseDao.Query("select Instrument_Type,Instrument_ID from InstrumentInfo where Instrument_Collection_ID='" + Common.CollectionID + "' and  Instrument_Name='" + Common.MOISTURENAME + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        numwater = Common.GetInt(dt.Rows[0][0]);
                        moist = Common.GetInt(dt.Rows[0][1]);
                    }
                    LinQBaseDao.Query("insert into Moisture_test (Moisture_test_Collection_ID,	Moistrue_test_Instrument_ID	,Moisture_test_value,Moisture_test_NO,	Moisture_test_count,Moisture_test_status,	Moisture_test_Time,Moisture_test_Numwater) values(" + Common.CollectionID + "," + moist + "," + strwater + "," + cou + "," + cou + ",0,getdate(),'" + numwater + "')");
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLogWate("serial异常：" + ex.Message.ToString());
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            if (axMoisture2.ISOpen)
            {
                serialT();
                bluetoothsucceedTwo();
            }
            else
            {
                bluetoothfailTwo();
            }
        }

        private void 策略配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PolicyConfigurationInfoForm pf = new PolicyConfigurationInfoForm();
            pf.Show();
        }

        private void 仪表管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstrumentInfoForm ifn = new InstrumentInfoForm();
            ifn.Show();
        }
    }
}

