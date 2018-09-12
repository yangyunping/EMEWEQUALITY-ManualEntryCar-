using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEEntity;
using EMEWEDAL;
using System.Linq.Expressions;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class SetPassWord : Form
    {
        public SetPassWord()
        {
            InitializeComponent();
        }
        public MainFrom mf;
        private void LoadData()
        {
            mf = new MainFrom();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnUpdate.Enabled)
            {
                btnUpdate.Enabled = false;
                update();
                Common.WriteLogData("修改", "用户"+Common.USERNAME+"修改密码", Common.NAME); //操作日志
                btnUpdate.Enabled = true;
            }
        }
        private void update()
        {
            try
            {
                if (txtOldPwd.Text.Trim() == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "旧密码不能为空！", txtOldPwd, this);
                    txtOldPwd.Focus();
                    return;
                }
                else
                {
                    if (txtOldPwd.Text.Trim() != Common.PWD)
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "提示", "旧密码不正确！", txtOldPwd, this);
                        txtOldPwd.Focus();
                        return;
                    }
                }
                if (txtNewPwd.Text.Trim() == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "新密码不能为空！", txtNewPwd, this);
                    txtNewPwd.Focus();
                    return;
                }
                if (txtSurePwd.Text.Trim() == "")
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "两次输入的密码不一致，请重新输入！", txtNewPwd, this);
                    txtNewPwd.Text = "";
                    txtSurePwd.Text = "";
                    txtNewPwd.Focus();
                    return;
                }
                Expression<Func<UserInfo, bool>> funUserinfo = n => n.UserLoginId == Common.USERNAME;
                Action<UserInfo> action = n =>
                    {
                        n.UserLoginPwd = txtNewPwd.Text.Trim();
                    };
                if (UserInfoDAL.Update(funUserinfo, action))
                {
                    MessageBox.Show("密码修改成功","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("修改失败", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                Common.WriteTextLog("SetPassWord +update()"+ex.Message.ToString());
            }

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetPassWord_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
