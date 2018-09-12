namespace EMEWEQUALITY.SystemAdmin
{
    partial class FormWeightSet
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
            this.cbxTestItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxWeightNum = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxWeightNum2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxTestItem2 = new System.Windows.Forms.ComboBox();
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
            this.dgvView_WaterSet = new System.Windows.Forms.DataGridView();
            this.WeightSet_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestItems_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeightSet_weightNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).BeginInit();
            this.bdnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView_WaterSet)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxTestItem
            // 
            this.cbxTestItem.FormattingEnabled = true;
            this.cbxTestItem.Location = new System.Drawing.Point(143, 36);
            this.cbxTestItem.Name = "cbxTestItem";
            this.cbxTestItem.Size = new System.Drawing.Size(121, 20);
            this.cbxTestItem.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "重量检测项目：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "选择磅：";
            // 
            // cbxWeightNum
            // 
            this.cbxWeightNum.FormattingEnabled = true;
            this.cbxWeightNum.Items.AddRange(new object[] {
            "1号磅",
            "2号磅"});
            this.cbxWeightNum.Location = new System.Drawing.Point(401, 36);
            this.cbxWeightNum.Name = "cbxWeightNum";
            this.cbxWeightNum.Size = new System.Drawing.Size(121, 20);
            this.cbxWeightNum.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(583, 35);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelect);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbxWeightNum2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbxTestItem2);
            this.groupBox2.Controls.Add(this.bdnInfo);
            this.groupBox2.Controls.Add(this.dgvView_WaterSet);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 324);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "重量检测配置列表";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(583, 27);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 58;
            this.btnSelect.Text = "查询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(336, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 57;
            this.label3.Text = "选择磅：";
            // 
            // cbxWeightNum2
            // 
            this.cbxWeightNum2.FormattingEnabled = true;
            this.cbxWeightNum2.Items.AddRange(new object[] {
            "",
            "1号磅",
            "2号磅"});
            this.cbxWeightNum2.Location = new System.Drawing.Point(401, 28);
            this.cbxWeightNum2.Name = "cbxWeightNum2";
            this.cbxWeightNum2.Size = new System.Drawing.Size(121, 20);
            this.cbxWeightNum2.TabIndex = 56;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 55;
            this.label4.Text = "重量检测项目：";
            // 
            // cbxTestItem2
            // 
            this.cbxTestItem2.FormattingEnabled = true;
            this.cbxTestItem2.Location = new System.Drawing.Point(143, 28);
            this.cbxTestItem2.Name = "cbxTestItem2";
            this.cbxTestItem2.Size = new System.Drawing.Size(121, 20);
            this.cbxTestItem2.TabIndex = 54;
            // 
            // bdnInfo
            // 
            this.bdnInfo.AddNewItem = null;
            this.bdnInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bdnInfo.AutoSize = false;
            this.bdnInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.bdnInfo.CountItem = null;
            this.bdnInfo.DeleteItem = null;
            this.bdnInfo.Dock = System.Windows.Forms.DockStyle.None;
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
            this.bdnInfo.Location = new System.Drawing.Point(3, 66);
            this.bdnInfo.MoveFirstItem = null;
            this.bdnInfo.MoveLastItem = null;
            this.bdnInfo.MoveNextItem = null;
            this.bdnInfo.MovePreviousItem = null;
            this.bdnInfo.Name = "bdnInfo";
            this.bdnInfo.PositionItem = null;
            this.bdnInfo.Size = new System.Drawing.Size(721, 25);
            this.bdnInfo.TabIndex = 53;
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
            this.tsbtnUpdate.Click += new System.EventHandler(this.tsbtnUpdate_Click);
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
            this.tsbtnDel.Click += new System.EventHandler(this.tsbtnDel_Click);
            // 
            // dgvView_WaterSet
            // 
            this.dgvView_WaterSet.AllowUserToAddRows = false;
            this.dgvView_WaterSet.AllowUserToDeleteRows = false;
            this.dgvView_WaterSet.AllowUserToOrderColumns = true;
            this.dgvView_WaterSet.AllowUserToResizeRows = false;
            this.dgvView_WaterSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvView_WaterSet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WeightSet_id,
            this.TestItems_NAME,
            this.WeightSet_weightNum});
            this.dgvView_WaterSet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvView_WaterSet.Location = new System.Drawing.Point(3, 94);
            this.dgvView_WaterSet.Name = "dgvView_WaterSet";
            this.dgvView_WaterSet.ReadOnly = true;
            this.dgvView_WaterSet.RowTemplate.Height = 23;
            this.dgvView_WaterSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvView_WaterSet.Size = new System.Drawing.Size(721, 227);
            this.dgvView_WaterSet.TabIndex = 1;
            this.dgvView_WaterSet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvView_WaterSet_CellClick);
            this.dgvView_WaterSet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvView_WaterTestConfigure_CellContentClick);
            // 
            // WeightSet_id
            // 
            this.WeightSet_id.DataPropertyName = "WeightSet_id";
            this.WeightSet_id.HeaderText = "编号";
            this.WeightSet_id.Name = "WeightSet_id";
            this.WeightSet_id.ReadOnly = true;
            // 
            // TestItems_NAME
            // 
            this.TestItems_NAME.DataPropertyName = "TestItems_NAME";
            this.TestItems_NAME.HeaderText = "检测项目名称";
            this.TestItems_NAME.Name = "TestItems_NAME";
            this.TestItems_NAME.ReadOnly = true;
            this.TestItems_NAME.Width = 120;
            // 
            // WeightSet_weightNum
            // 
            this.WeightSet_weightNum.DataPropertyName = "WeightSet_weightNum";
            this.WeightSet_weightNum.HeaderText = "小磅号";
            this.WeightSet_weightNum.Name = "WeightSet_weightNum";
            this.WeightSet_weightNum.ReadOnly = true;
            // 
            // FormWeightSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 423);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxWeightNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxTestItem);
            this.Name = "FormWeightSet";
            this.Text = "重量检测设置";
            this.Load += new System.EventHandler(this.FormWeightSet_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).EndInit();
            this.bdnInfo.ResumeLayout(false);
            this.bdnInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView_WaterSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxTestItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxWeightNum;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
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
        private System.Windows.Forms.ToolStripLabel tslSelectAll2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel tslNotSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbtnUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbtnDel;
        private System.Windows.Forms.DataGridView dgvView_WaterSet;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxWeightNum2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxTestItem2;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeightSet_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestItems_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeightSet_weightNum;
    }
}