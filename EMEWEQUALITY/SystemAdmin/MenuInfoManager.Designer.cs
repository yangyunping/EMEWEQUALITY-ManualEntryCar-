namespace EMEWEQUALITY.SystemAdmin
{
    partial class MenuInfoManager
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbText = new System.Windows.Forms.GroupBox();
            this.tvMenuList = new System.Windows.Forms.TreeView();
            this.cmsTvMenuInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.gbManager = new System.Windows.Forms.GroupBox();
            this.lbFromName = new System.Windows.Forms.Label();
            this.lbDictionary = new System.Windows.Forms.Label();
            this.lbControlName = new System.Windows.Forms.Label();
            this.btnOut = new System.Windows.Forms.Button();
            this.btnADD = new System.Windows.Forms.Button();
            this.tbFromName = new System.Windows.Forms.TextBox();
            this.lbFromText = new System.Windows.Forms.Label();
            this.lbControlText = new System.Windows.Forms.Label();
            this.tbControlText = new System.Windows.Forms.TextBox();
            this.cbDictionary = new System.Windows.Forms.ComboBox();
            this.tbControlName = new System.Windows.Forms.TextBox();
            this.tbFromText = new System.Windows.Forms.TextBox();
            this.lbMenuTypeName = new System.Windows.Forms.Label();
            this.cbControlType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbEnabled = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbVisble = new System.Windows.Forms.ComboBox();
            this.gbText.SuspendLayout();
            this.cmsTvMenuInfo.SuspendLayout();
            this.gbManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbText
            // 
            this.gbText.Controls.Add(this.tvMenuList);
            this.gbText.Location = new System.Drawing.Point(12, 11);
            this.gbText.Name = "gbText";
            this.gbText.Size = new System.Drawing.Size(281, 456);
            this.gbText.TabIndex = 0;
            this.gbText.TabStop = false;
            this.gbText.Text = "菜单显示：";
            // 
            // tvMenuList
            // 
            this.tvMenuList.ContextMenuStrip = this.cmsTvMenuInfo;
            this.tvMenuList.Location = new System.Drawing.Point(10, 17);
            this.tvMenuList.Name = "tvMenuList";
            this.tvMenuList.Size = new System.Drawing.Size(260, 432);
            this.tvMenuList.TabIndex = 0;
            this.tvMenuList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMenuList_AfterSelect);
            this.tvMenuList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMenuList_NodeMouseClick);
            // 
            // cmsTvMenuInfo
            // 
            this.cmsTvMenuInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMenu,
            this.UpdateMenu,
            this.DeleteMenu});
            this.cmsTvMenuInfo.Name = "cmsTvMenuInfo";
            this.cmsTvMenuInfo.Size = new System.Drawing.Size(131, 70);
            // 
            // AddMenu
            // 
            this.AddMenu.Name = "AddMenu";
            this.AddMenu.Size = new System.Drawing.Size(130, 22);
            this.AddMenu.Text = "添加新节点";
            this.AddMenu.Click += new System.EventHandler(this.AddMenu_Click);
            // 
            // UpdateMenu
            // 
            this.UpdateMenu.Name = "UpdateMenu";
            this.UpdateMenu.Size = new System.Drawing.Size(130, 22);
            this.UpdateMenu.Text = "修改此节点";
            this.UpdateMenu.Click += new System.EventHandler(this.UpdateMenu_Click);
            // 
            // DeleteMenu
            // 
            this.DeleteMenu.Name = "DeleteMenu";
            this.DeleteMenu.Size = new System.Drawing.Size(130, 22);
            this.DeleteMenu.Text = "删除此节点";
            this.DeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
            // 
            // gbManager
            // 
            this.gbManager.Controls.Add(this.lbFromName);
            this.gbManager.Controls.Add(this.lbDictionary);
            this.gbManager.Controls.Add(this.lbControlName);
            this.gbManager.Controls.Add(this.btnOut);
            this.gbManager.Controls.Add(this.btnADD);
            this.gbManager.Controls.Add(this.tbFromName);
            this.gbManager.Controls.Add(this.lbFromText);
            this.gbManager.Controls.Add(this.lbControlText);
            this.gbManager.Controls.Add(this.tbControlText);
            this.gbManager.Controls.Add(this.cbDictionary);
            this.gbManager.Controls.Add(this.tbControlName);
            this.gbManager.Controls.Add(this.tbFromText);
            this.gbManager.Controls.Add(this.lbMenuTypeName);
            this.gbManager.Controls.Add(this.cbControlType);
            this.gbManager.Controls.Add(this.label4);
            this.gbManager.Controls.Add(this.cbEnabled);
            this.gbManager.Controls.Add(this.label3);
            this.gbManager.Controls.Add(this.cbVisble);
            this.gbManager.Location = new System.Drawing.Point(299, 11);
            this.gbManager.Name = "gbManager";
            this.gbManager.Size = new System.Drawing.Size(384, 456);
            this.gbManager.TabIndex = 9;
            this.gbManager.TabStop = false;
            this.gbManager.Text = "添加菜单";
            // 
            // lbFromName
            // 
            this.lbFromName.AutoSize = true;
            this.lbFromName.Location = new System.Drawing.Point(19, 144);
            this.lbFromName.Name = "lbFromName";
            this.lbFromName.Size = new System.Drawing.Size(59, 12);
            this.lbFromName.TabIndex = 26;
            this.lbFromName.Text = "菜单名称:";
            // 
            // lbDictionary
            // 
            this.lbDictionary.AutoSize = true;
            this.lbDictionary.Location = new System.Drawing.Point(19, 41);
            this.lbDictionary.Name = "lbDictionary";
            this.lbDictionary.Size = new System.Drawing.Size(59, 12);
            this.lbDictionary.TabIndex = 13;
            this.lbDictionary.Text = "菜单状态:";
            // 
            // lbControlName
            // 
            this.lbControlName.AutoSize = true;
            this.lbControlName.Location = new System.Drawing.Point(19, 88);
            this.lbControlName.Name = "lbControlName";
            this.lbControlName.Size = new System.Drawing.Size(59, 12);
            this.lbControlName.TabIndex = 25;
            this.lbControlName.Text = "控件名称:";
            // 
            // btnOut
            // 
            this.btnOut.Location = new System.Drawing.Point(230, 285);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(75, 23);
            this.btnOut.TabIndex = 28;
            this.btnOut.Text = "退出";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // btnADD
            // 
            this.btnADD.Location = new System.Drawing.Point(72, 285);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(75, 23);
            this.btnADD.TabIndex = 27;
            this.btnADD.Text = "添加";
            this.btnADD.UseVisualStyleBackColor = true;
            this.btnADD.Click += new System.EventHandler(this.btnADD_Click);
            // 
            // tbFromName
            // 
            this.tbFromName.Location = new System.Drawing.Point(108, 141);
            this.tbFromName.Name = "tbFromName";
            this.tbFromName.Size = new System.Drawing.Size(82, 21);
            this.tbFromName.TabIndex = 20;
            // 
            // lbFromText
            // 
            this.lbFromText.AutoSize = true;
            this.lbFromText.Location = new System.Drawing.Point(196, 144);
            this.lbFromText.Name = "lbFromText";
            this.lbFromText.Size = new System.Drawing.Size(89, 12);
            this.lbFromText.TabIndex = 23;
            this.lbFromText.Text = "菜单中文名称：";
            // 
            // lbControlText
            // 
            this.lbControlText.AutoSize = true;
            this.lbControlText.Location = new System.Drawing.Point(196, 88);
            this.lbControlText.Name = "lbControlText";
            this.lbControlText.Size = new System.Drawing.Size(89, 12);
            this.lbControlText.TabIndex = 24;
            this.lbControlText.Text = "控件中文名称：";
            // 
            // tbControlText
            // 
            this.tbControlText.Location = new System.Drawing.Point(286, 85);
            this.tbControlText.Name = "tbControlText";
            this.tbControlText.Size = new System.Drawing.Size(83, 21);
            this.tbControlText.TabIndex = 22;
            // 
            // cbDictionary
            // 
            this.cbDictionary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDictionary.FormattingEnabled = true;
            this.cbDictionary.Location = new System.Drawing.Point(108, 38);
            this.cbDictionary.Name = "cbDictionary";
            this.cbDictionary.Size = new System.Drawing.Size(82, 20);
            this.cbDictionary.TabIndex = 11;
            // 
            // tbControlName
            // 
            this.tbControlName.Location = new System.Drawing.Point(108, 85);
            this.tbControlName.Name = "tbControlName";
            this.tbControlName.Size = new System.Drawing.Size(82, 21);
            this.tbControlName.TabIndex = 18;
            // 
            // tbFromText
            // 
            this.tbFromText.Location = new System.Drawing.Point(287, 141);
            this.tbFromText.Name = "tbFromText";
            this.tbFromText.Size = new System.Drawing.Size(82, 21);
            this.tbFromText.TabIndex = 19;
            // 
            // lbMenuTypeName
            // 
            this.lbMenuTypeName.AutoSize = true;
            this.lbMenuTypeName.Location = new System.Drawing.Point(228, 41);
            this.lbMenuTypeName.Name = "lbMenuTypeName";
            this.lbMenuTypeName.Size = new System.Drawing.Size(35, 12);
            this.lbMenuTypeName.TabIndex = 14;
            this.lbMenuTypeName.Text = "类型:";
            // 
            // cbControlType
            // 
            this.cbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbControlType.FormattingEnabled = true;
            this.cbControlType.Location = new System.Drawing.Point(287, 38);
            this.cbControlType.Name = "cbControlType";
            this.cbControlType.Size = new System.Drawing.Size(82, 20);
            this.cbControlType.TabIndex = 10;
            this.cbControlType.SelectionChangeCommitted += new System.EventHandler(this.cbControlType_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "是否可用:";
            // 
            // cbEnabled
            // 
            this.cbEnabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEnabled.FormattingEnabled = true;
            this.cbEnabled.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cbEnabled.Location = new System.Drawing.Point(287, 190);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(38, 20);
            this.cbEnabled.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "是否可编辑:";
            // 
            // cbVisble
            // 
            this.cbVisble.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVisble.FormattingEnabled = true;
            this.cbVisble.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cbVisble.Location = new System.Drawing.Point(108, 190);
            this.cbVisble.Name = "cbVisble";
            this.cbVisble.Size = new System.Drawing.Size(39, 20);
            this.cbVisble.TabIndex = 12;
            // 
            // MenuInfoManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 474);
            this.Controls.Add(this.gbText);
            this.Controls.Add(this.gbManager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuInfoManager";
            this.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "菜单管理";
            this.Load += new System.EventHandler(this.MenuInfoManager_Load);
            this.gbText.ResumeLayout(false);
            this.cmsTvMenuInfo.ResumeLayout(false);
            this.gbManager.ResumeLayout(false);
            this.gbManager.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbText;
        private System.Windows.Forms.TreeView tvMenuList;
        private System.Windows.Forms.ContextMenuStrip cmsTvMenuInfo;
        private System.Windows.Forms.GroupBox gbManager;
        private System.Windows.Forms.Label lbFromName;
        private System.Windows.Forms.Label lbDictionary;
        private System.Windows.Forms.Label lbControlName;
        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.Button btnADD;
        private System.Windows.Forms.TextBox tbFromName;
        private System.Windows.Forms.Label lbFromText;
        private System.Windows.Forms.Label lbControlText;
        private System.Windows.Forms.TextBox tbControlText;
        private System.Windows.Forms.ComboBox cbDictionary;
        private System.Windows.Forms.TextBox tbControlName;
        private System.Windows.Forms.TextBox tbFromText;
        private System.Windows.Forms.Label lbMenuTypeName;
        private System.Windows.Forms.ComboBox cbControlType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbEnabled;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbVisble;
        private System.Windows.Forms.ToolStripMenuItem AddMenu;
        private System.Windows.Forms.ToolStripMenuItem UpdateMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenu;

    }
}
