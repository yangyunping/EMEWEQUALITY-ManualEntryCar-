using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using EMEWEQUALITY.GetPhoto;
using EMEWEQUALITY.HelpClass;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class CutPic : Form
    {
        public CutPic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前选中摄像头
        /// </summary>
        int channel = 0;

        /// <summary>
        /// 默认摄像头个数
        /// </summary>
        int channelnum = 1;
        /// <summary>
        /// 用户句柄
        /// </summary>
        private int m_lRealHandle = -1;
        /// <summary>
        /// 用户ID
        /// </summary>
        public static int m_lUserID = -1;

        public static CutPic ct = null;

        private void CutPic_Load(object sender, EventArgs e)
        {
            //InitSDK();
            //base.Hide();//
            //SetLogin();
            //GetRealPlay();
        }


        private void btnPhoto_Click(object sender, EventArgs e)
        {
            CapturePic(Common.Channel);
            this.Close();
        }

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
        /// 实时视频信息
        /// </summary>   
        private void GetRealPlay()
        {
            try
            {
                IntPtr pUser = new IntPtr();
                CHCNetSDK.NET_DVR_CLIENTINFO lpClientInfo = new CHCNetSDK.NET_DVR_CLIENTINFO()
                {
                    lChannel = Common.Channel,
                    lLinkMode = 0,
                    sMultiCastIP = "",
                    hPlayWnd = Controls.Find("pictureBox1", true)[0].Handle
                };
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V30(m_lUserID, ref lpClientInfo, null, pUser, 0);
                if (this.m_lRealHandle == -1)
                {
                    uint nError = CHCNetSDK.NET_DVR_GetLastError();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CapturePic(int iQcInfoID)
        {
            InitSDK();
            base.Hide();//
            SetLogin();
            string strDirectoryName = Common.BaseFile + "weightPic\\";
            if (!Directory.Exists(strDirectoryName))
            {
                Directory.CreateDirectory(strDirectoryName);
            }
            string strfileImage = SDKCommon.CaptureJPEGPicture(strDirectoryName, iQcInfoID, Common.Channel);

            //上传图片
            List<string> list = new List<string>();

            //得到图片的路径
            string path = Common.BaseFile + "weightPic\\" + strfileImage;

            ImageFile.UpLoadFile(path, Common.SaveFiel);//上传图片到指定路径

            ImageFile.Delete(path);//上传完成后，删除图片
            //保存图片信息
        }

        private void CutPic_FormClosing(object sender, FormClosingEventArgs e)
        {
            ct = null;
        }
    }
}
