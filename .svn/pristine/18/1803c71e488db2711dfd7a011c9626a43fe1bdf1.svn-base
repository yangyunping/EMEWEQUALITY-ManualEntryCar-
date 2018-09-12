using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEEntity;
using EMEWEDAL;
using System.Linq.Expressions;

namespace EMEWEQUALITY.MyControl
{
    public partial class CardControl : UserControl
    {
        public CardControl()
        {
            InitializeComponent();
        }
        public UserInfo userInfo=null;
        [Category("重要属性")]
        [Description("输入卡号获取姓名")]
        public string TXTCARD
        {
            get
            {
                return this.txtCardID.Text.Trim();
            }
            set
            {
                this.txtCardID.Text = value;

            }
        }
        [Category("重要属性")]
        [Description("输入lblCardNO名称 卡号")]
        public string LblCardNO
        {
            get
            {
                return this.lblCardNO.Text.Trim();
            }
            set
            {
                this.lblCardNO.Text = value;

            }
        }

        [Category("重要属性")]
        [Description("输入lable名称")]
        public string LblName
        {
            get
            {
                return this.lblName.Text.Trim();
            }
            set
            {
                this.lblName.Text = value;

            }
        }

        [Category("重要属性")]
        [Description("txtCardID.UseSystemPasswordChar和txtCardID.Enabled")]
        public bool ISPass
        {
            get
            {
                return txtCardID.UseSystemPasswordChar;
            }
            set
            {
                txtCardID.UseSystemPasswordChar= value;
                txtCardID.Enabled = value;
                if (value == true)
                {
                    lblName.Text = "卡号:";
                    this.TXTCARD = "";
                    this.Users = null;
                    this.LblCardNO = "";
                }
                else {
                    lblName.Text = "姓名:";
                }

            }
        }
        [Category("重要属性")]
        [Description("当前卡号用户实体")]
        public UserInfo Users {

            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;
            }
        }


        private void txtCardID_TextChanged(object sender, EventArgs e)
        {
            if (txtCardID.Enabled)
            {

                if (txtCardID.Text.Trim() != "" && txtCardID.Text.Trim().Length == 10)
                {

                    txtCardID.Enabled = false;
                    string cardid = txtCardID.Text.Trim();
                    if (GetCardName(cardid))
                    {
                        txtCardID.Text = HelpClass.GetData.Person;
                    }
                    else
                    {
                        txtCardID.Enabled = true;
                    }
                }
                else
                {
                    txtCardID.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 获取卡相关的用户名
        /// </summary>
        /// <param name="strCardID"></param>
        private bool GetCardName(string strCardID)
        {
            bool rbool = false;
            try
            {
               int userid = 0;
                Expression<Func<UserInfo, bool>> p = n => n.UserCarId == strCardID.Trim();
                UserInfo userinfo = UserInfoDAL.Single(p);
                if (userinfo != null)
                    userid = userinfo.UserId;
                if (userid > 0)
                {
                    userInfo = userinfo;
                    HelpClass.GetData.Person = userinfo.UserName;
                    lblCardNO.Text = strCardID;
                    this.ISPass = false;
                   //txtCardID.Text = userinfo.UserName;
                    rbool = true;
                    /*
                    Expression<Func<RoleInfo, bool>> funroleinfo = n => n.Role_Id == userinfo.User_Role_Id;
                    foreach (var n in RoleInfoDAL.Query(funroleinfo))
                    {
                        string peri = n.Role_Permission;
                        //水分管理
                        if (peri.Substring(5, 1) == "1")
                        {
                            lblName.Text = "姓名";
                            HelpClass.GetData.Person = userinfo.UserName;
                            txtCardID.Text = userinfo.UserName;
                            txtCardID.UseSystemPasswordChar = false;
                            txtCardID.Enabled = false;
                        }
                        else
                        {
                            txtCardID.Text = "";
                            MessageBox.Show("请刷有用户关联的卡！", "操作提示", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                        }
                        break;
                    }*/
                }
                else {
                 ISPass = true;
                    MessageBox.Show("请刷与用户关联的卡！", "操作提示", MessageBoxButtons.OK,
                                   MessageBoxIcon.Warning);
                }
            
            }
            catch (Exception ex)
            {
                ISPass = true;
                Common.WriteTextLog("读取卡号异常：" + ex.Message);
            }
            return rbool;
        }

   
    }
}
