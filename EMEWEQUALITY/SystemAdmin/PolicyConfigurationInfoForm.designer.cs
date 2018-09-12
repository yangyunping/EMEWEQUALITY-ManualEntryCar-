namespace EMEWEQUALITY.SystemAdmin
{
    partial class PolicyConfigurationInfoForm
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
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.bdnInfo = new System.Windows.Forms.BindingNavigator(this.components);
            this.tslHomPage1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tslPreviousPage1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txtCurrentPage2 = new System.Windows.Forms.ToolStripTextBox();
            this.lblPageCount2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tslNextPage1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tslLastPage1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.tscbxPageSize2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tslNotSelect = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslSelectAll = new System.Windows.Forms.ToolStripLabel();
            this.dgvPolicyConfigurationInfo = new System.Windows.Forms.DataGridView();
            this.PolicyConfiguration_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Collection_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PolicyConfiguration_Instrument_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label65 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cob_Collection_Name = new System.Windows.Forms.ComboBox();
            this.cob_ClientName = new System.Windows.Forms.ComboBox();
            this.clbInstrument_Name = new System.Windows.Forms.CheckedListBox();
            this.btnqk = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).BeginInit();
            this.bdnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPolicyConfigurationInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.bdnInfo);
            this.groupBox12.Controls.Add(this.dgvPolicyConfigurationInfo);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox12.Location = new System.Drawing.Point(0, 101);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(646, 275);
            this.groupBox12.TabIndex = 12;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "仪表领取信息列表";
            // 
            // bdnInfo
            // 
            this.bdnInfo.AddNewItem = null;
            this.bdnInfo.AutoSize = false;
            this.bdnInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.bdnInfo.CountItem = null;
            this.bdnInfo.DeleteItem = null;
            this.bdnInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bdnInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.bdnInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bdnInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslHomPage1,
            this.toolStripSeparator8,
            this.tslPreviousPage1,
            this.toolStripSeparator9,
            this.toolStripLabel4,
            this.txtCurrentPage2,
            this.lblPageCount2,
            this.toolStripSeparator10,
            this.tslNextPage1,
            this.toolStripSeparator11,
            this.tslLastPage1,
            this.toolStripSeparator12,
            this.toolStripLabel8,
            this.tscbxPageSize2,
            this.toolStripSeparator14,
            this.tsbtnDel,
            this.toolStripSeparator15,
            this.tsbtnUpdate,
            this.toolStripSeparator13,
            this.tslNotSelect,
            this.toolStripSeparator1,
            this.tslSelectAll});
            this.bdnInfo.Location = new System.Drawing.Point(3, 16);
            this.bdnInfo.MoveFirstItem = null;
            this.bdnInfo.MoveLastItem = null;
            this.bdnInfo.MoveNextItem = null;
            this.bdnInfo.MovePreviousItem = null;
            this.bdnInfo.Name = "bdnInfo";
            this.bdnInfo.PositionItem = null;
            this.bdnInfo.Size = new System.Drawing.Size(640, 33);
            this.bdnInfo.TabIndex = 75;
            this.bdnInfo.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bdnInfo_ItemClicked);
            // 
            // tslHomPage1
            // 
            this.tslHomPage1.Name = "tslHomPage1";
            this.tslHomPage1.Size = new System.Drawing.Size(29, 30);
            this.tslHomPage1.Text = "首页";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 33);
            // 
            // tslPreviousPage1
            // 
            this.tslPreviousPage1.Name = "tslPreviousPage1";
            this.tslPreviousPage1.Size = new System.Drawing.Size(41, 30);
            this.tslPreviousPage1.Text = "上一页";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(53, 30);
            this.toolStripLabel4.Text = "当前页数";
            // 
            // txtCurrentPage2
            // 
            this.txtCurrentPage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentPage2.Name = "txtCurrentPage2";
            this.txtCurrentPage2.Size = new System.Drawing.Size(50, 33);
            // 
            // lblPageCount2
            // 
            this.lblPageCount2.Name = "lblPageCount2";
            this.lblPageCount2.Size = new System.Drawing.Size(35, 30);
            this.lblPageCount2.Text = "共1页";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 33);
            // 
            // tslNextPage1
            // 
            this.tslNextPage1.Name = "tslNextPage1";
            this.tslNextPage1.Size = new System.Drawing.Size(41, 30);
            this.tslNextPage1.Text = "下一页";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 33);
            // 
            // tslLastPage1
            // 
            this.tslLastPage1.Name = "tslLastPage1";
            this.tslLastPage1.Size = new System.Drawing.Size(29, 30);
            this.tslLastPage1.Text = "尾页";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(53, 30);
            this.toolStripLabel8.Text = "每页条数";
            // 
            // tscbxPageSize2
            // 
            this.tscbxPageSize2.Items.AddRange(new object[] {
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "90",
            "100",
            "200",
            "300",
            "500",
            "800",
            "1000"});
            this.tscbxPageSize2.Name = "tscbxPageSize2";
            this.tscbxPageSize2.Size = new System.Drawing.Size(75, 33);
            this.tscbxPageSize2.Text = "10";
            this.tscbxPageSize2.SelectedIndexChanged += new System.EventHandler(this.tscbxPageSize2_SelectedIndexChanged);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbtnDel
            // 
            this.tsbtnDel.Enabled = false;
            this.tsbtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDel.Name = "tsbtnDel";
            this.tsbtnDel.Size = new System.Drawing.Size(33, 30);
            this.tsbtnDel.Text = "删除";
            this.tsbtnDel.Visible = false;
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbtnUpdate
            // 
            this.tsbtnUpdate.Enabled = false;
            this.tsbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUpdate.Name = "tsbtnUpdate";
            this.tsbtnUpdate.Size = new System.Drawing.Size(33, 30);
            this.tsbtnUpdate.Text = "修改";
            this.tsbtnUpdate.ToolTipText = "修改";
            this.tsbtnUpdate.Visible = false;
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 33);
            // 
            // tslNotSelect
            // 
            this.tslNotSelect.Name = "tslNotSelect";
            this.tslNotSelect.Size = new System.Drawing.Size(53, 30);
            this.tslNotSelect.Text = "取消全选";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // tslSelectAll
            // 
            this.tslSelectAll.Name = "tslSelectAll";
            this.tslSelectAll.Size = new System.Drawing.Size(29, 30);
            this.tslSelectAll.Text = "全选";
            // 
            // dgvPolicyConfigurationInfo
            // 
            this.dgvPolicyConfigurationInfo.AllowUserToAddRows = false;
            this.dgvPolicyConfigurationInfo.AllowUserToDeleteRows = false;
            this.dgvPolicyConfigurationInfo.AllowUserToOrderColumns = true;
            this.dgvPolicyConfigurationInfo.AllowUserToResizeRows = false;
            this.dgvPolicyConfigurationInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPolicyConfigurationInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PolicyConfiguration_Id,
            this.Client_NAME,
            this.Collection_Name,
            this.PolicyConfiguration_Instrument_ID});
            this.dgvPolicyConfigurationInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvPolicyConfigurationInfo.Location = new System.Drawing.Point(3, 49);
            this.dgvPolicyConfigurationInfo.Name = "dgvPolicyConfigurationInfo";
            this.dgvPolicyConfigurationInfo.ReadOnly = true;
            this.dgvPolicyConfigurationInfo.RowTemplate.Height = 23;
            this.dgvPolicyConfigurationInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPolicyConfigurationInfo.Size = new System.Drawing.Size(640, 223);
            this.dgvPolicyConfigurationInfo.TabIndex = 3;
            // 
            // PolicyConfiguration_Id
            // 
            this.PolicyConfiguration_Id.DataPropertyName = "PolicyConfiguration_Id";
            this.PolicyConfiguration_Id.HeaderText = "编号";
            this.PolicyConfiguration_Id.Name = "PolicyConfiguration_Id";
            this.PolicyConfiguration_Id.ReadOnly = true;
            // 
            // Client_NAME
            // 
            this.Client_NAME.DataPropertyName = "Client_NAME";
            this.Client_NAME.HeaderText = "客户端";
            this.Client_NAME.Name = "Client_NAME";
            this.Client_NAME.ReadOnly = true;
            // 
            // Collection_Name
            // 
            this.Collection_Name.DataPropertyName = "Collection_Name";
            this.Collection_Name.HeaderText = "采集端";
            this.Collection_Name.Name = "Collection_Name";
            this.Collection_Name.ReadOnly = true;
            // 
            // PolicyConfiguration_Instrument_ID
            // 
            this.PolicyConfiguration_Instrument_ID.DataPropertyName = "PolicyConfiguration_Instrument_ID";
            this.PolicyConfiguration_Instrument_ID.HeaderText = "仪表";
            this.PolicyConfiguration_Instrument_ID.Name = "PolicyConfiguration_Instrument_ID";
            this.PolicyConfiguration_Instrument_ID.ReadOnly = true;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(-88, 28);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(47, 12);
            this.label65.TabIndex = 10;
            this.label65.Text = "客户端:";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(406, 35);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(41, 12);
            this.label67.TabIndex = 17;
            this.label67.Text = "仪表：";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(208, 35);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(53, 12);
            this.label66.TabIndex = 18;
            this.label66.Text = "采集端：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "客户端：";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(236, 72);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "登记";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button4_Click);
            // 
            // cob_Collection_Name
            // 
            this.cob_Collection_Name.FormattingEnabled = true;
            this.cob_Collection_Name.Location = new System.Drawing.Point(267, 29);
            this.cob_Collection_Name.Name = "cob_Collection_Name";
            this.cob_Collection_Name.Size = new System.Drawing.Size(121, 20);
            this.cob_Collection_Name.TabIndex = 14;
            this.cob_Collection_Name.SelectedIndexChanged += new System.EventHandler(this.cob_Collection_Name_SelectedIndexChanged);
            // 
            // cob_ClientName
            // 
            this.cob_ClientName.FormattingEnabled = true;
            this.cob_ClientName.Location = new System.Drawing.Point(67, 29);
            this.cob_ClientName.Name = "cob_ClientName";
            this.cob_ClientName.Size = new System.Drawing.Size(121, 20);
            this.cob_ClientName.TabIndex = 15;
            // 
            // clbInstrument_Name
            // 
            this.clbInstrument_Name.FormattingEnabled = true;
            this.clbInstrument_Name.Location = new System.Drawing.Point(470, 11);
            this.clbInstrument_Name.Name = "clbInstrument_Name";
            this.clbInstrument_Name.Size = new System.Drawing.Size(120, 84);
            this.clbInstrument_Name.TabIndex = 20;
            // 
            // btnqk
            // 
            this.btnqk.Location = new System.Drawing.Point(372, 72);
            this.btnqk.Name = "btnqk";
            this.btnqk.Size = new System.Drawing.Size(75, 23);
            this.btnqk.TabIndex = 22;
            this.btnqk.Text = "取消修改";
            this.btnqk.UseVisualStyleBackColor = true;
            this.btnqk.Click += new System.EventHandler(this.btnqk_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(113, 72);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "确认修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // PolicyConfigurationInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 376);
            this.Controls.Add(this.btnqk);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.clbInstrument_Name);
            this.Controls.Add(this.label67);
            this.Controls.Add(this.label66);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cob_Collection_Name);
            this.Controls.Add(this.cob_ClientName);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.label65);
            this.Name = "PolicyConfigurationInfoForm";
            this.Text = "策略配置";
            this.Load += new System.EventHandler(this.LingQuBiaoForm_Load);
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).EndInit();
            this.bdnInfo.ResumeLayout(false);
            this.bdnInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPolicyConfigurationInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.DataGridView dgvPolicyConfigurationInfo;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cob_Collection_Name;
        private System.Windows.Forms.ComboBox cob_ClientName;
        private System.Windows.Forms.BindingNavigator bdnInfo;
        private System.Windows.Forms.ToolStripLabel tslHomPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripLabel tslPreviousPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox txtCurrentPage2;
        private System.Windows.Forms.ToolStripLabel lblPageCount2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripLabel tslNextPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripLabel tslLastPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripComboBox tscbxPageSize2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton tsbtnDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton tsbtnUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripLabel tslNotSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslSelectAll;
        private System.Windows.Forms.CheckedListBox clbInstrument_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn PolicyConfiguration_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn Collection_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn PolicyConfiguration_Instrument_ID;
        private System.Windows.Forms.Button btnqk;
        private System.Windows.Forms.Button btnUpdate;
    }
}