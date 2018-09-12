namespace EMEWEQUALITY
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblClose = new System.Windows.Forms.Label();
            this.pLogin = new System.Windows.Forms.Panel();
            this.lab_Version = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Location = new System.Drawing.Point(467, 321);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(223, 14);
            this.txtName.TabIndex = 1;
            // 
            // txtPwd
            // 
            this.txtPwd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPwd.Location = new System.Drawing.Point(467, 353);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(223, 14);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyDown);
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.AutoSize = true;
            this.lblClose.BackColor = System.Drawing.Color.Transparent;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblClose.Location = new System.Drawing.Point(945, 9);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(57, 12);
            this.lblClose.TabIndex = 4;
            this.lblClose.Text = "退出系统";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // pLogin
            // 
            this.pLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pLogin.BackColor = System.Drawing.Color.Transparent;
            this.pLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pLogin.Location = new System.Drawing.Point(651, 416);
            this.pLogin.Name = "pLogin";
            this.pLogin.Size = new System.Drawing.Size(104, 37);
            this.pLogin.TabIndex = 3;
            this.pLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lab_Version
            // 
            this.lab_Version.AutoSize = true;
            this.lab_Version.BackColor = System.Drawing.Color.Transparent;
            this.lab_Version.ForeColor = System.Drawing.Color.Black;
            this.lab_Version.Location = new System.Drawing.Point(288, 11);
            this.lab_Version.Name = "lab_Version";
            this.lab_Version.Size = new System.Drawing.Size(41, 12);
            this.lab_Version.TabIndex = 5;
            this.lab_Version.Text = "label1";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::EMEWEQUALITY.Properties.Resources.jlmd_login1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.lab_Version);
            this.Controls.Add(this.pLogin);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtName);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Panel pLogin;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lab_Version;
        //  private Sunisoft.IrisSkin.SkinEngine skinEngine1;
    }
}