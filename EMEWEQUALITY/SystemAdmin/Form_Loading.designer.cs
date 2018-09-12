namespace EMEWEQUALITY.SystemAdmin
{
    partial class Form_Loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Loading));
            this.pbloading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // pbloading
            // 
            this.pbloading.BackColor = System.Drawing.Color.GreenYellow;
            this.pbloading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbloading.ForeColor = System.Drawing.Color.GreenYellow;
            this.pbloading.Location = new System.Drawing.Point(0, 0);
            this.pbloading.Name = "pbloading";
            this.pbloading.Size = new System.Drawing.Size(287, 38);
            this.pbloading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbloading.TabIndex = 3;
            // 
            // Form_Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(287, 38);
            this.Controls.Add(this.pbloading);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Loading";
            this.Opacity = 0.5D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "正在加载...";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ProgressBar pbloading;
    }
}