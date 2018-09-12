namespace EMEWEQUALITY.SystemAdmin
{
    partial class PrintTemplateSetUpForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_SetUp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkl_Name = new System.Windows.Forms.CheckedListBox();
            this.btn_SetUpDefaultTemplate = new System.Windows.Forms.Button();
            this.btn_NewTemplate = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_NewTemplate);
            this.groupBox2.Controls.Add(this.btn_SetUpDefaultTemplate);
            this.groupBox2.Controls.Add(this.btn_SetUp);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(490, 40);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // btn_SetUp
            // 
            this.btn_SetUp.Location = new System.Drawing.Point(411, 17);
            this.btn_SetUp.Name = "btn_SetUp";
            this.btn_SetUp.Size = new System.Drawing.Size(75, 23);
            this.btn_SetUp.TabIndex = 0;
            this.btn_SetUp.Text = "打  印";
            this.btn_SetUp.UseVisualStyleBackColor = true;
            this.btn_SetUp.Click += new System.EventHandler(this.btn_SetUp_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkl_Name);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 333);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模  板：";
            // 
            // chkl_Name
            // 
            this.chkl_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkl_Name.FormattingEnabled = true;
            this.chkl_Name.Location = new System.Drawing.Point(3, 17);
            this.chkl_Name.Name = "chkl_Name";
            this.chkl_Name.Size = new System.Drawing.Size(484, 308);
            this.chkl_Name.TabIndex = 0;
            this.chkl_Name.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkl_Name_ItemCheck);
            // 
            // btn_SetUpDefaultTemplate
            // 
            this.btn_SetUpDefaultTemplate.Location = new System.Drawing.Point(197, 17);
            this.btn_SetUpDefaultTemplate.Name = "btn_SetUpDefaultTemplate";
            this.btn_SetUpDefaultTemplate.Size = new System.Drawing.Size(91, 23);
            this.btn_SetUpDefaultTemplate.TabIndex = 1;
            this.btn_SetUpDefaultTemplate.Text = "设为默认模板";
            this.btn_SetUpDefaultTemplate.UseVisualStyleBackColor = true;
            this.btn_SetUpDefaultTemplate.Click += new System.EventHandler(this.btn_SetUpDefaultTemplate_Click);
            // 
            // btn_NewTemplate
            // 
            this.btn_NewTemplate.Location = new System.Drawing.Point(27, 17);
            this.btn_NewTemplate.Name = "btn_NewTemplate";
            this.btn_NewTemplate.Size = new System.Drawing.Size(75, 23);
            this.btn_NewTemplate.TabIndex = 2;
            this.btn_NewTemplate.Text = "增加新模板";
            this.btn_NewTemplate.UseVisualStyleBackColor = true;
            this.btn_NewTemplate.Click += new System.EventHandler(this.btn_NewTemplate_Click);
            // 
            // PrintTemplateSetUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 373);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "PrintTemplateSetUpForm";
            this.Text = "打印模板设置";
            this.Load += new System.EventHandler(this.PrintTemplateSetUpForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_SetUp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chkl_Name;
        private System.Windows.Forms.Button btn_SetUpDefaultTemplate;
        private System.Windows.Forms.Button btn_NewTemplate;
    }
}