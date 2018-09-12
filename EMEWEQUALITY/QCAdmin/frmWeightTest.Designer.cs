namespace EMEWEQUALITY.QCAdmin
{
    partial class frmWeightTest
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
            this.components = new System.ComponentModel.Container();
            this.txtRecord = new System.Windows.Forms.TextBox();
            this.timerData = new System.Windows.Forms.Timer(this.components);
            this.txtRecord2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtRecord
            // 
            this.txtRecord.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtRecord.Location = new System.Drawing.Point(0, 0);
            this.txtRecord.Multiline = true;
            this.txtRecord.Name = "txtRecord";
            this.txtRecord.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRecord.Size = new System.Drawing.Size(277, 273);
            this.txtRecord.TabIndex = 130;
            // 
            // timerData
            // 
            this.timerData.Enabled = true;
            this.timerData.Tag = "检测是否有新数据需要获取";
            this.timerData.Tick += new System.EventHandler(this.timerData_Tick);
            // 
            // txtRecord2
            // 
            this.txtRecord2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRecord2.Location = new System.Drawing.Point(277, 0);
            this.txtRecord2.Multiline = true;
            this.txtRecord2.Name = "txtRecord2";
            this.txtRecord2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRecord2.Size = new System.Drawing.Size(252, 273);
            this.txtRecord2.TabIndex = 131;
            // 
            // frmWeightTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 273);
            this.Controls.Add(this.txtRecord2);
            this.Controls.Add(this.txtRecord);
            this.Name = "frmWeightTest";
            this.Text = "接收重量测试";
            this.Load += new System.EventHandler(this.frmWeightTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRecord;
        private System.Windows.Forms.Timer timerData;
        private System.Windows.Forms.TextBox txtRecord2;
    }
}