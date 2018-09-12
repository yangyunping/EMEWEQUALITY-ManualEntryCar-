namespace EMEWEQUALITY.SystemAdmin
{
    partial class DictionaryAdmin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCanle = new System.Windows.Forms.Button();
            this.btnSeacher = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDictionary_Remark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxDictionary_State = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDictionary_OtherID = new System.Windows.Forms.ComboBox();
            this.txtDictionary_Value = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDictionary_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bdnInfo = new System.Windows.Forms.BindingNavigator(this.components);
            this.tslHomPage1 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tslPreviousPage1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtCurrentPage1 = new System.Windows.Forms.ToolStripTextBox();
            this.lblPageCount1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslNextPage1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslLastPage1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tslSelectAll = new System.Windows.Forms.ToolStripLabel();
            this.tscbxPageSize1 = new System.Windows.Forms.ToolStripComboBox();
            this.tslSelectAll2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslNotSelect = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDel = new System.Windows.Forms.ToolStripButton();
            this.dgvDictionary = new System.Windows.Forms.DataGridView();
            this.Dictionary_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dictionary_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dictionary_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dictionary_OtherID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dictionary_State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dictionary_ISLower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dictionary_Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).BeginInit();
            this.bdnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnCanle);
            this.groupBox1.Controls.Add(this.btnSeacher);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtDictionary_Remark);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxDictionary_State);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxDictionary_OtherID);
            this.groupBox1.Controls.Add(this.txtDictionary_Value);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDictionary_Name);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(832, 165);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字典内容";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(717, 114);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCanle
            // 
            this.btnCanle.Location = new System.Drawing.Point(717, 60);
            this.btnCanle.Name = "btnCanle";
            this.btnCanle.Size = new System.Drawing.Size(75, 23);
            this.btnCanle.TabIndex = 12;
            this.btnCanle.Text = "清   空";
            this.btnCanle.UseVisualStyleBackColor = true;
            this.btnCanle.Click += new System.EventHandler(this.btnCanle_Click);
            // 
            // btnSeacher
            // 
            this.btnSeacher.Location = new System.Drawing.Point(604, 114);
            this.btnSeacher.Name = "btnSeacher";
            this.btnSeacher.Size = new System.Drawing.Size(75, 23);
            this.btnSeacher.TabIndex = 11;
            this.btnSeacher.Text = "搜   索";
            this.btnSeacher.UseVisualStyleBackColor = true;
            this.btnSeacher.Click += new System.EventHandler(this.btnSeacher_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(604, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保   存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDictionary_Remark
            // 
            this.txtDictionary_Remark.Location = new System.Drawing.Point(86, 94);
            this.txtDictionary_Remark.Multiline = true;
            this.txtDictionary_Remark.Name = "txtDictionary_Remark";
            this.txtDictionary_Remark.Size = new System.Drawing.Size(452, 45);
            this.txtDictionary_Remark.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "备注:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(431, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "字典状态:";
            // 
            // cbxDictionary_State
            // 
            this.cbxDictionary_State.FormattingEnabled = true;
            this.cbxDictionary_State.Items.AddRange(new object[] {
            "启动",
            "注销"});
            this.cbxDictionary_State.Location = new System.Drawing.Point(496, 19);
            this.cbxDictionary_State.Name = "cbxDictionary_State";
            this.cbxDictionary_State.Size = new System.Drawing.Size(121, 20);
            this.cbxDictionary_State.TabIndex = 6;
            this.cbxDictionary_State.Text = "启动";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "字典所属:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cbxDictionary_OtherID
            // 
            this.cbxDictionary_OtherID.FormattingEnabled = true;
            this.cbxDictionary_OtherID.Location = new System.Drawing.Point(294, 20);
            this.cbxDictionary_OtherID.Name = "cbxDictionary_OtherID";
            this.cbxDictionary_OtherID.Size = new System.Drawing.Size(121, 20);
            this.cbxDictionary_OtherID.TabIndex = 4;
            // 
            // txtDictionary_Value
            // 
            this.txtDictionary_Value.Location = new System.Drawing.Point(91, 19);
            this.txtDictionary_Value.Name = "txtDictionary_Value";
            this.txtDictionary_Value.Size = new System.Drawing.Size(132, 21);
            this.txtDictionary_Value.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "字典值:";
            // 
            // txtDictionary_Name
            // 
            this.txtDictionary_Name.Location = new System.Drawing.Point(86, 58);
            this.txtDictionary_Name.Multiline = true;
            this.txtDictionary_Name.Name = "txtDictionary_Name";
            this.txtDictionary_Name.Size = new System.Drawing.Size(452, 30);
            this.txtDictionary_Name.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字典描述:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bdnInfo);
            this.groupBox2.Controls.Add(this.dgvDictionary);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(832, 320);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字典列表";
            // 
            // bdnInfo
            // 
            this.bdnInfo.AddNewItem = null;
            this.bdnInfo.AutoSize = false;
            this.bdnInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.bdnInfo.CountItem = null;
            this.bdnInfo.DeleteItem = null;
            this.bdnInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.bdnInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bdnInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslHomPage1,
            this.bindingNavigatorSeparator,
            this.tslPreviousPage1,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.txtCurrentPage1,
            this.lblPageCount1,
            this.toolStripSeparator1,
            this.tslNextPage1,
            this.toolStripSeparator2,
            this.tslLastPage1,
            this.toolStripSeparator6,
            this.tslSelectAll,
            this.tscbxPageSize1,
            this.tslSelectAll2,
            this.toolStripSeparator3,
            this.tslNotSelect,
            this.toolStripSeparator5,
            this.tsbtnUpdate,
            this.toolStripSeparator7,
            this.tsbtnDel});
            this.bdnInfo.Location = new System.Drawing.Point(3, 17);
            this.bdnInfo.MoveFirstItem = null;
            this.bdnInfo.MoveLastItem = null;
            this.bdnInfo.MoveNextItem = null;
            this.bdnInfo.MovePreviousItem = null;
            this.bdnInfo.Name = "bdnInfo";
            this.bdnInfo.PositionItem = null;
            this.bdnInfo.Size = new System.Drawing.Size(826, 25);
            this.bdnInfo.TabIndex = 53;
            this.bdnInfo.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bdnInfo_ItemClicked);
            // 
            // tslHomPage1
            // 
            this.tslHomPage1.Name = "tslHomPage1";
            this.tslHomPage1.Size = new System.Drawing.Size(29, 22);
            this.tslHomPage1.Text = "首页";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tslPreviousPage1
            // 
            this.tslPreviousPage1.Name = "tslPreviousPage1";
            this.tslPreviousPage1.Size = new System.Drawing.Size(41, 22);
            this.tslPreviousPage1.Text = "上一页";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel1.Text = "当前页数";
            // 
            // txtCurrentPage1
            // 
            this.txtCurrentPage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentPage1.Name = "txtCurrentPage1";
            this.txtCurrentPage1.Size = new System.Drawing.Size(50, 25);
            // 
            // lblPageCount1
            // 
            this.lblPageCount1.Name = "lblPageCount1";
            this.lblPageCount1.Size = new System.Drawing.Size(35, 22);
            this.lblPageCount1.Text = "共1页";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslNextPage1
            // 
            this.tslNextPage1.Name = "tslNextPage1";
            this.tslNextPage1.Size = new System.Drawing.Size(41, 22);
            this.tslNextPage1.Text = "下一页";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslLastPage1
            // 
            this.tslLastPage1.Name = "tslLastPage1";
            this.tslLastPage1.Size = new System.Drawing.Size(29, 22);
            this.tslLastPage1.Text = "尾页";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tslSelectAll
            // 
            this.tslSelectAll.Name = "tslSelectAll";
            this.tslSelectAll.Size = new System.Drawing.Size(53, 22);
            this.tslSelectAll.Text = "每页条数";
            // 
            // tscbxPageSize1
            // 
            this.tscbxPageSize1.Items.AddRange(new object[] {
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
            this.tscbxPageSize1.Name = "tscbxPageSize1";
            this.tscbxPageSize1.Size = new System.Drawing.Size(75, 25);
            this.tscbxPageSize1.Text = "10";
            this.tscbxPageSize1.SelectedIndexChanged += new System.EventHandler(this.tscbxPageSize1_SelectedIndexChanged);
            // 
            // tslSelectAll2
            // 
            this.tslSelectAll2.Name = "tslSelectAll2";
            this.tslSelectAll2.Size = new System.Drawing.Size(29, 22);
            this.tslSelectAll2.Text = "全选";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tslNotSelect
            // 
            this.tslNotSelect.Name = "tslNotSelect";
            this.tslNotSelect.Size = new System.Drawing.Size(53, 22);
            this.tslNotSelect.Text = "取消全选";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnUpdate
            // 
            this.tsbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUpdate.Name = "tsbtnUpdate";
            this.tsbtnUpdate.Size = new System.Drawing.Size(33, 22);
            this.tsbtnUpdate.Text = "修改";
            this.tsbtnUpdate.ToolTipText = "修改";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnDel
            // 
            this.tsbtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDel.Name = "tsbtnDel";
            this.tsbtnDel.Size = new System.Drawing.Size(33, 22);
            this.tsbtnDel.Text = "删除";
            // 
            // dgvDictionary
            // 
            this.dgvDictionary.AllowUserToAddRows = false;
            this.dgvDictionary.AllowUserToDeleteRows = false;
            this.dgvDictionary.AllowUserToOrderColumns = true;
            this.dgvDictionary.AllowUserToResizeRows = false;
            this.dgvDictionary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDictionary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Dictionary_ID,
            this.Dictionary_Value,
            this.Dictionary_Name,
            this.Dictionary_OtherID,
            this.Dictionary_State,
            this.Dictionary_ISLower,
            this.Dictionary_Remark});
            this.dgvDictionary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDictionary.Location = new System.Drawing.Point(3, 42);
            this.dgvDictionary.Name = "dgvDictionary";
            this.dgvDictionary.ReadOnly = true;
            this.dgvDictionary.RowTemplate.Height = 23;
            this.dgvDictionary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDictionary.Size = new System.Drawing.Size(826, 275);
            this.dgvDictionary.TabIndex = 1;
            // 
            // Dictionary_ID
            // 
            this.Dictionary_ID.DataPropertyName = "Dictionary_ID";
            this.Dictionary_ID.HeaderText = "字典编号";
            this.Dictionary_ID.Name = "Dictionary_ID";
            this.Dictionary_ID.ReadOnly = true;
            this.Dictionary_ID.Visible = false;
            // 
            // Dictionary_Value
            // 
            this.Dictionary_Value.DataPropertyName = "Dictionary_Value";
            this.Dictionary_Value.HeaderText = "字典值";
            this.Dictionary_Value.Name = "Dictionary_Value";
            this.Dictionary_Value.ReadOnly = true;
            // 
            // Dictionary_Name
            // 
            this.Dictionary_Name.DataPropertyName = "Dictionary_Name";
            this.Dictionary_Name.HeaderText = "字典描述";
            this.Dictionary_Name.Name = "Dictionary_Name";
            this.Dictionary_Name.ReadOnly = true;
            // 
            // Dictionary_OtherID
            // 
            this.Dictionary_OtherID.DataPropertyName = "Dictionary_OtherID";
            this.Dictionary_OtherID.HeaderText = "字典所属编号";
            this.Dictionary_OtherID.Name = "Dictionary_OtherID";
            this.Dictionary_OtherID.ReadOnly = true;
            this.Dictionary_OtherID.Width = 120;
            // 
            // Dictionary_State
            // 
            this.Dictionary_State.DataPropertyName = "Dictionary_State";
            this.Dictionary_State.HeaderText = "状态";
            this.Dictionary_State.Name = "Dictionary_State";
            this.Dictionary_State.ReadOnly = true;
            // 
            // Dictionary_ISLower
            // 
            this.Dictionary_ISLower.DataPropertyName = "Dictionary_ISLower";
            this.Dictionary_ISLower.HeaderText = "是否有下级";
            this.Dictionary_ISLower.Name = "Dictionary_ISLower";
            this.Dictionary_ISLower.ReadOnly = true;
            // 
            // Dictionary_Remark
            // 
            this.Dictionary_Remark.DataPropertyName = "Dictionary_Remark";
            this.Dictionary_Remark.HeaderText = "备注";
            this.Dictionary_Remark.Name = "Dictionary_Remark";
            this.Dictionary_Remark.ReadOnly = true;
            // 
            // DictionaryAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 505);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "DictionaryAdmin";
            this.Text = "字典管理";
            this.Load += new System.EventHandler(this.DictionaryAdmin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).EndInit();
            this.bdnInfo.ResumeLayout(false);
            this.bdnInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDictionary_OtherID;
        private System.Windows.Forms.TextBox txtDictionary_Value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDictionary_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxDictionary_State;
        private System.Windows.Forms.TextBox txtDictionary_Remark;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCanle;
        private System.Windows.Forms.Button btnSeacher;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvDictionary;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.BindingNavigator bdnInfo;
        private System.Windows.Forms.ToolStripLabel tslHomPage1;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripLabel tslPreviousPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtCurrentPage1;
        private System.Windows.Forms.ToolStripLabel lblPageCount1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslNextPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslLastPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel tslSelectAll;
        private System.Windows.Forms.ToolStripComboBox tscbxPageSize1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel tslNotSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbtnUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbtnDel;
        private System.Windows.Forms.ToolStripLabel tslSelectAll2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dictionary_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dictionary_Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dictionary_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dictionary_OtherID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dictionary_State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dictionary_ISLower;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dictionary_Remark;
        private System.Windows.Forms.Button btnUpdate;
    }
}