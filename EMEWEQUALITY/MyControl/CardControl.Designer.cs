namespace EMEWEQUALITY.MyControl
{
    partial class CardControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.lblCardNO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 12);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "卡号:";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(48, 7);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(112, 21);
            this.txtCardID.TabIndex = 1;
            this.txtCardID.UseSystemPasswordChar = true;
            this.txtCardID.TextChanged += new System.EventHandler(this.txtCardID_TextChanged);
            // 
            // lblCardNO
            // 
            this.lblCardNO.AutoSize = true;
            this.lblCardNO.Location = new System.Drawing.Point(3, 22);
            this.lblCardNO.Name = "lblCardNO";
            this.lblCardNO.Size = new System.Drawing.Size(41, 12);
            this.lblCardNO.TabIndex = 2;
            this.lblCardNO.Text = "label1";
            this.lblCardNO.Visible = false;
            // 
            // CardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCardNO);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.lblName);
            this.Name = "CardControl";
            this.Size = new System.Drawing.Size(172, 34);
            this.Tag = "刷卡信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label lblCardNO;
    }
}
