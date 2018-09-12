namespace EMEWEQUALITY.NewAdd
{
    partial class PrintRecordForm
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
            this.btnCler = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQCInfo_QCInfo_ID = new System.Windows.Forms.TextBox();
            this.dgvImageRecord = new System.Windows.Forms.DataGridView();
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
            this.tslNotSelect = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslSelectAll = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tslExport = new System.Windows.Forms.ToolStripButton();
            this.ImageRecord_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REF_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unusual_handle_Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageRecord_Unusual_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestItems_NAM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unusual_content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageRecord_ImageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageRecord_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageRecord_Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImageRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).BeginInit();
            this.bdnInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCler);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtQCInfo_QCInfo_ID);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(987, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCler
            // 
            this.btnCler.Location = new System.Drawing.Point(698, 71);
            this.btnCler.Name = "btnCler";
            this.btnCler.Size = new System.Drawing.Size(75, 23);
            this.btnCler.TabIndex = 20;
            this.btnCler.Text = "清空";
            this.btnCler.UseVisualStyleBackColor = true;
            this.btnCler.Click += new System.EventHandler(this.btnCler_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(211, 73);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(143, 21);
            this.dateTimePicker2.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "结束时间:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(211, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(143, 21);
            this.dateTimePicker1.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "起止时间:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(698, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(422, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "质检编号:";
            // 
            // txtQCInfo_QCInfo_ID
            // 
            this.txtQCInfo_QCInfo_ID.Location = new System.Drawing.Point(497, 19);
            this.txtQCInfo_QCInfo_ID.Name = "txtQCInfo_QCInfo_ID";
            this.txtQCInfo_QCInfo_ID.Size = new System.Drawing.Size(143, 21);
            this.txtQCInfo_QCInfo_ID.TabIndex = 0;
            // 
            // dgvImageRecord
            // 
            this.dgvImageRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImageRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ImageRecord_Id,
            this.REF_NO,
            this.carNo,
            this.UserName,
            this.Unusual_handle_Result,
            this.ImageRecord_Unusual_Id,
            this.TestItems_NAM,
            this.Unusual_content,
            this.ImageRecord_ImageName,
            this.ImageRecord_Time,
            this.ImageRecord_Remark});
            this.dgvImageRecord.Location = new System.Drawing.Point(2, 144);
            this.dgvImageRecord.Name = "dgvImageRecord";
            this.dgvImageRecord.ReadOnly = true;
            this.dgvImageRecord.RowTemplate.Height = 23;
            this.dgvImageRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImageRecord.Size = new System.Drawing.Size(988, 477);
            this.dgvImageRecord.TabIndex = 82;
            // 
            // bdnInfo
            // 
            this.bdnInfo.AddNewItem = null;
            this.bdnInfo.AutoSize = false;
            this.bdnInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.bdnInfo.CountItem = null;
            this.bdnInfo.DeleteItem = null;
            this.bdnInfo.Dock = System.Windows.Forms.DockStyle.None;
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
            this.tslNotSelect,
            this.toolStripSeparator1,
            this.tslSelectAll,
            this.toolStripSeparator13,
            this.tslExport});
            this.bdnInfo.Location = new System.Drawing.Point(3, 116);
            this.bdnInfo.MoveFirstItem = null;
            this.bdnInfo.MoveLastItem = null;
            this.bdnInfo.MoveNextItem = null;
            this.bdnInfo.MovePreviousItem = null;
            this.bdnInfo.Name = "bdnInfo";
            this.bdnInfo.PositionItem = null;
            this.bdnInfo.Size = new System.Drawing.Size(987, 25);
            this.bdnInfo.TabIndex = 83;
            this.bdnInfo.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bdnInfo_ItemClicked_1);
            // 
            // tslHomPage1
            // 
            this.tslHomPage1.Name = "tslHomPage1";
            this.tslHomPage1.Size = new System.Drawing.Size(29, 22);
            this.tslHomPage1.Text = "首页";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tslPreviousPage1
            // 
            this.tslPreviousPage1.Name = "tslPreviousPage1";
            this.tslPreviousPage1.Size = new System.Drawing.Size(41, 22);
            this.tslPreviousPage1.Text = "上一页";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel4.Text = "当前页数";
            // 
            // txtCurrentPage2
            // 
            this.txtCurrentPage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentPage2.Name = "txtCurrentPage2";
            this.txtCurrentPage2.Size = new System.Drawing.Size(50, 25);
            // 
            // lblPageCount2
            // 
            this.lblPageCount2.Name = "lblPageCount2";
            this.lblPageCount2.Size = new System.Drawing.Size(35, 22);
            this.lblPageCount2.Text = "共1页";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tslNextPage1
            // 
            this.tslNextPage1.Name = "tslNextPage1";
            this.tslNextPage1.Size = new System.Drawing.Size(41, 22);
            this.tslNextPage1.Text = "下一页";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // tslLastPage1
            // 
            this.tslLastPage1.Name = "tslLastPage1";
            this.tslLastPage1.Size = new System.Drawing.Size(29, 22);
            this.tslLastPage1.Text = "尾页";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(53, 22);
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
            this.tscbxPageSize2.Size = new System.Drawing.Size(75, 25);
            this.tscbxPageSize2.Text = "10";
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnDel
            // 
            this.tsbtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDel.Name = "tsbtnDel";
            this.tsbtnDel.Size = new System.Drawing.Size(33, 22);
            this.tsbtnDel.Text = "删除";
            this.tsbtnDel.Visible = false;
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // tslNotSelect
            // 
            this.tslNotSelect.Name = "tslNotSelect";
            this.tslNotSelect.Size = new System.Drawing.Size(53, 22);
            this.tslNotSelect.Text = "取消全选";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslSelectAll
            // 
            this.tslSelectAll.Name = "tslSelectAll";
            this.tslSelectAll.Size = new System.Drawing.Size(29, 22);
            this.tslSelectAll.Text = "全选";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // tslExport
            // 
            this.tslExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tslExport.Name = "tslExport";
            this.tslExport.Size = new System.Drawing.Size(63, 22);
            this.tslExport.Text = "导出Excel";
            this.tslExport.ToolTipText = "修改";
            this.tslExport.Visible = false;
            this.tslExport.Click += new System.EventHandler(this.tslExport_Click_1);
            // 
            // ImageRecord_Id
            // 
            this.ImageRecord_Id.DataPropertyName = "ImageRecord_Id";
            this.ImageRecord_Id.HeaderText = "编号";
            this.ImageRecord_Id.Name = "ImageRecord_Id";
            this.ImageRecord_Id.ReadOnly = true;
            this.ImageRecord_Id.Visible = false;
            // 
            // REF_NO
            // 
            this.REF_NO.DataPropertyName = "REF_NO";
            this.REF_NO.HeaderText = "参号";
            this.REF_NO.Name = "REF_NO";
            this.REF_NO.ReadOnly = true;
            // 
            // carNo
            // 
            this.carNo.DataPropertyName = "CNTR_NO";
            this.carNo.HeaderText = "车牌";
            this.carNo.Name = "carNo";
            this.carNo.ReadOnly = true;
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "处理人";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            // 
            // Unusual_handle_Result
            // 
            this.Unusual_handle_Result.DataPropertyName = "Unusual_handle_Result";
            this.Unusual_handle_Result.HeaderText = "处理结果";
            this.Unusual_handle_Result.Name = "Unusual_handle_Result";
            this.Unusual_handle_Result.ReadOnly = true;
            // 
            // ImageRecord_Unusual_Id
            // 
            this.ImageRecord_Unusual_Id.DataPropertyName = "ImageRecord_Unusual_Id";
            this.ImageRecord_Unusual_Id.HeaderText = "异常编号";
            this.ImageRecord_Unusual_Id.Name = "ImageRecord_Unusual_Id";
            this.ImageRecord_Unusual_Id.ReadOnly = true;
            // 
            // TestItems_NAM
            // 
            this.TestItems_NAM.DataPropertyName = "TestItems_NAM";
            this.TestItems_NAM.HeaderText = "异常类型";
            this.TestItems_NAM.Name = "TestItems_NAM";
            this.TestItems_NAM.ReadOnly = true;
            // 
            // Unusual_content
            // 
            this.Unusual_content.DataPropertyName = "Unusual_content";
            this.Unusual_content.HeaderText = "异常内容";
            this.Unusual_content.Name = "Unusual_content";
            this.Unusual_content.ReadOnly = true;
            // 
            // ImageRecord_ImageName
            // 
            this.ImageRecord_ImageName.DataPropertyName = "ImageRecord_ImageName";
            this.ImageRecord_ImageName.HeaderText = "图片名称";
            this.ImageRecord_ImageName.Name = "ImageRecord_ImageName";
            this.ImageRecord_ImageName.ReadOnly = true;
            // 
            // ImageRecord_Time
            // 
            this.ImageRecord_Time.DataPropertyName = "ImageRecord_Time";
            this.ImageRecord_Time.HeaderText = "上传时间";
            this.ImageRecord_Time.Name = "ImageRecord_Time";
            this.ImageRecord_Time.ReadOnly = true;
            // 
            // ImageRecord_Remark
            // 
            this.ImageRecord_Remark.DataPropertyName = "ImageRecord_Remark";
            this.ImageRecord_Remark.HeaderText = "备注";
            this.ImageRecord_Remark.Name = "ImageRecord_Remark";
            this.ImageRecord_Remark.ReadOnly = true;
            // 
            // PrintRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 623);
            this.Controls.Add(this.bdnInfo);
            this.Controls.Add(this.dgvImageRecord);
            this.Controls.Add(this.groupBox1);
            this.Name = "PrintRecordForm";
            this.Text = "照片记录查询";
            this.Load += new System.EventHandler(this.PrintRecordForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImageRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).EndInit();
            this.bdnInfo.ResumeLayout(false);
            this.bdnInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvImageRecord;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQCInfo_QCInfo_ID;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCler;
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
        private System.Windows.Forms.ToolStripLabel tslNotSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton tslExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageRecord_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn REF_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn carNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unusual_handle_Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageRecord_Unusual_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestItems_NAM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unusual_content;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageRecord_ImageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageRecord_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageRecord_Remark;
    }
}