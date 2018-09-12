namespace EMEWEQUALITY.Statistics
{
    partial class QCDetailsForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSHIPMENT_NO = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPO_NO = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxState = new System.Windows.Forms.ComboBox();
            this.txtCarNO = new System.Windows.Forms.TextBox();
            this.btnQCOneInfo = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWEIGHT_TICKET_NO = new System.Windows.Forms.TextBox();
            this.dvgCarList = new System.Windows.Forms.DataGridView();
            this.CarNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dictionary_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHIPMENT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PO_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROD_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NO_OF_BALES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_PumpingPackets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_MATERIAL_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_MATERIAL_SCALE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_PAPER_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_PAPER_SCALE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_BAGWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_DEGREE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_MOIST_PER_SAMPLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCIInfo_EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bdnInfo = new System.Windows.Forms.BindingNavigator(this.components);
            this.tslHomPage1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tslPreviousPage2 = new System.Windows.Forms.ToolStripLabel();
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
            this.tsbSelectAll = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNotSelectAll = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgCarList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).BeginInit();
            this.bdnInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSHIPMENT_NO);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtPO_NO);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cboxState);
            this.groupBox2.Controls.Add(this.txtCarNO);
            this.groupBox2.Controls.Add(this.btnQCOneInfo);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtWEIGHT_TICKET_NO);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(992, 657);
            this.groupBox2.TabIndex = 58;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "搜    索";
            // 
            // txtSHIPMENT_NO
            // 
            this.txtSHIPMENT_NO.Location = new System.Drawing.Point(269, 71);
            this.txtSHIPMENT_NO.MaxLength = 13;
            this.txtSHIPMENT_NO.Name = "txtSHIPMENT_NO";
            this.txtSHIPMENT_NO.Size = new System.Drawing.Size(91, 21);
            this.txtSHIPMENT_NO.TabIndex = 56;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(188, 76);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 55;
            this.label17.Text = "送货单：";
            // 
            // txtPO_NO
            // 
            this.txtPO_NO.Location = new System.Drawing.Point(694, 22);
            this.txtPO_NO.MaxLength = 13;
            this.txtPO_NO.Name = "txtPO_NO";
            this.txtPO_NO.Size = new System.Drawing.Size(91, 21);
            this.txtPO_NO.TabIndex = 54;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(644, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 53;
            this.label18.Text = "采购单：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(411, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "数据状态：";
            this.label4.Visible = false;
            // 
            // cboxState
            // 
            this.cboxState.FormattingEnabled = true;
            this.cboxState.Location = new System.Drawing.Point(482, 70);
            this.cboxState.Name = "cboxState";
            this.cboxState.Size = new System.Drawing.Size(112, 20);
            this.cboxState.TabIndex = 28;
            this.cboxState.Visible = false;
            // 
            // txtCarNO
            // 
            this.txtCarNO.Location = new System.Drawing.Point(269, 22);
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.Size = new System.Drawing.Size(91, 21);
            this.txtCarNO.TabIndex = 25;
            // 
            // btnQCOneInfo
            // 
            this.btnQCOneInfo.Location = new System.Drawing.Point(719, 73);
            this.btnQCOneInfo.Name = "btnQCOneInfo";
            this.btnQCOneInfo.Size = new System.Drawing.Size(75, 23);
            this.btnQCOneInfo.TabIndex = 9;
            this.btnQCOneInfo.Text = "搜    索";
            this.btnQCOneInfo.UseVisualStyleBackColor = true;
            this.btnQCOneInfo.Click += new System.EventHandler(this.btnQCOneInfo_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(188, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "车牌号：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(414, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "磅单号：";
            // 
            // txtWEIGHT_TICKET_NO
            // 
            this.txtWEIGHT_TICKET_NO.Location = new System.Drawing.Point(482, 22);
            this.txtWEIGHT_TICKET_NO.MaxLength = 13;
            this.txtWEIGHT_TICKET_NO.Name = "txtWEIGHT_TICKET_NO";
            this.txtWEIGHT_TICKET_NO.Size = new System.Drawing.Size(91, 21);
            this.txtWEIGHT_TICKET_NO.TabIndex = 11;
            // 
            // dvgCarList
            // 
            this.dvgCarList.AllowUserToAddRows = false;
            this.dvgCarList.AllowUserToDeleteRows = false;
            this.dvgCarList.AllowUserToOrderColumns = true;
            this.dvgCarList.AllowUserToResizeRows = false;
            this.dvgCarList.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dvgCarList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgCarList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CarNO,
            this.QCInfo_ID,
            this.Dictionary_Name,
            this.SHIPMENT_NO,
            this.PO_NO,
            this.PROD_ID,
            this.NO_OF_BALES,
            this.QCInfo_PumpingPackets,
            this.QCInfo_MATERIAL_WEIGHT,
            this.QCInfo_MATERIAL_SCALE,
            this.QCInfo_PAPER_WEIGHT,
            this.QCInfo_PAPER_SCALE,
            this.QCInfo_BAGWeight,
            this.QCInfo_DEGREE,
            this.QCInfo_MOIST_PER_SAMPLE,
            this.QCInfo_TIME,
            this.UserName,
            this.QCIInfo_EndTime,
            this.WEIGHT_DATE});
            this.dvgCarList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvgCarList.Location = new System.Drawing.Point(0, 139);
            this.dvgCarList.Name = "dvgCarList";
            this.dvgCarList.ReadOnly = true;
            this.dvgCarList.RowTemplate.Height = 23;
            this.dvgCarList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgCarList.Size = new System.Drawing.Size(992, 484);
            this.dvgCarList.TabIndex = 61;
            // 
            // CarNO
            // 
            this.CarNO.DataPropertyName = "CNTR_NO";
            this.CarNO.HeaderText = "车牌号";
            this.CarNO.Name = "CarNO";
            this.CarNO.ReadOnly = true;
            // 
            // QCInfo_ID
            // 
            this.QCInfo_ID.DataPropertyName = "QCInfo_ID";
            this.QCInfo_ID.HeaderText = "质检编号";
            this.QCInfo_ID.Name = "QCInfo_ID";
            this.QCInfo_ID.ReadOnly = true;
            this.QCInfo_ID.Visible = false;
            // 
            // Dictionary_Name
            // 
            this.Dictionary_Name.DataPropertyName = "Dictionary_Name";
            this.Dictionary_Name.HeaderText = "质检状态";
            this.Dictionary_Name.Name = "Dictionary_Name";
            this.Dictionary_Name.ReadOnly = true;
            // 
            // SHIPMENT_NO
            // 
            this.SHIPMENT_NO.DataPropertyName = "SHIPMENT_NO";
            this.SHIPMENT_NO.HeaderText = "送货单";
            this.SHIPMENT_NO.Name = "SHIPMENT_NO";
            this.SHIPMENT_NO.ReadOnly = true;
            // 
            // PO_NO
            // 
            this.PO_NO.DataPropertyName = "PO_NO";
            this.PO_NO.HeaderText = "采购单";
            this.PO_NO.Name = "PO_NO";
            this.PO_NO.ReadOnly = true;
            // 
            // PROD_ID
            // 
            this.PROD_ID.DataPropertyName = "PROD_ID";
            this.PROD_ID.HeaderText = "货品";
            this.PROD_ID.Name = "PROD_ID";
            this.PROD_ID.ReadOnly = true;
            // 
            // NO_OF_BALES
            // 
            this.NO_OF_BALES.DataPropertyName = "NO_OF_BALES";
            this.NO_OF_BALES.HeaderText = "总包数";
            this.NO_OF_BALES.Name = "NO_OF_BALES";
            this.NO_OF_BALES.ReadOnly = true;
            // 
            // QCInfo_PumpingPackets
            // 
            this.QCInfo_PumpingPackets.DataPropertyName = "QCInfo_PumpingPackets";
            this.QCInfo_PumpingPackets.HeaderText = "抽样总包数";
            this.QCInfo_PumpingPackets.Name = "QCInfo_PumpingPackets";
            this.QCInfo_PumpingPackets.ReadOnly = true;
            // 
            // QCInfo_MATERIAL_WEIGHT
            // 
            this.QCInfo_MATERIAL_WEIGHT.DataPropertyName = "QCInfo_MATERIAL_WEIGHT";
            this.QCInfo_MATERIAL_WEIGHT.HeaderText = "杂质重量";
            this.QCInfo_MATERIAL_WEIGHT.Name = "QCInfo_MATERIAL_WEIGHT";
            this.QCInfo_MATERIAL_WEIGHT.ReadOnly = true;
            // 
            // QCInfo_MATERIAL_SCALE
            // 
            this.QCInfo_MATERIAL_SCALE.DataPropertyName = "QCInfo_MATERIAL_SCALE";
            this.QCInfo_MATERIAL_SCALE.HeaderText = "杂质比例";
            this.QCInfo_MATERIAL_SCALE.Name = "QCInfo_MATERIAL_SCALE";
            this.QCInfo_MATERIAL_SCALE.ReadOnly = true;
            // 
            // QCInfo_PAPER_WEIGHT
            // 
            this.QCInfo_PAPER_WEIGHT.DataPropertyName = "QCInfo_PAPER_WEIGHT";
            this.QCInfo_PAPER_WEIGHT.HeaderText = "杂纸重量";
            this.QCInfo_PAPER_WEIGHT.Name = "QCInfo_PAPER_WEIGHT";
            this.QCInfo_PAPER_WEIGHT.ReadOnly = true;
            // 
            // QCInfo_PAPER_SCALE
            // 
            this.QCInfo_PAPER_SCALE.DataPropertyName = "QCInfo_PAPER_SCALE";
            this.QCInfo_PAPER_SCALE.HeaderText = "杂纸比例";
            this.QCInfo_PAPER_SCALE.Name = "QCInfo_PAPER_SCALE";
            this.QCInfo_PAPER_SCALE.ReadOnly = true;
            // 
            // QCInfo_BAGWeight
            // 
            this.QCInfo_BAGWeight.DataPropertyName = "QCInfo_BAGWeight";
            this.QCInfo_BAGWeight.HeaderText = "抽样废纸包总重";
            this.QCInfo_BAGWeight.Name = "QCInfo_BAGWeight";
            this.QCInfo_BAGWeight.ReadOnly = true;
            // 
            // QCInfo_DEGREE
            // 
            this.QCInfo_DEGREE.DataPropertyName = "QCInfo_DEGREE";
            this.QCInfo_DEGREE.HeaderText = "检测次数";
            this.QCInfo_DEGREE.Name = "QCInfo_DEGREE";
            this.QCInfo_DEGREE.ReadOnly = true;
            // 
            // QCInfo_MOIST_PER_SAMPLE
            // 
            this.QCInfo_MOIST_PER_SAMPLE.DataPropertyName = "QCInfo_MOIST_PER_SAMPLE";
            this.QCInfo_MOIST_PER_SAMPLE.HeaderText = "水分值（%）";
            this.QCInfo_MOIST_PER_SAMPLE.Name = "QCInfo_MOIST_PER_SAMPLE";
            this.QCInfo_MOIST_PER_SAMPLE.ReadOnly = true;
            // 
            // QCInfo_TIME
            // 
            this.QCInfo_TIME.DataPropertyName = "QCInfo_TIME";
            this.QCInfo_TIME.HeaderText = "登记时间";
            this.QCInfo_TIME.Name = "QCInfo_TIME";
            this.QCInfo_TIME.ReadOnly = true;
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "质检记录人";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            // 
            // QCIInfo_EndTime
            // 
            this.QCIInfo_EndTime.DataPropertyName = "QCIInfo_EndTime";
            this.QCIInfo_EndTime.HeaderText = "完成时间";
            this.QCIInfo_EndTime.Name = "QCIInfo_EndTime";
            this.QCIInfo_EndTime.ReadOnly = true;
            // 
            // WEIGHT_DATE
            // 
            this.WEIGHT_DATE.DataPropertyName = "WEIGHT_DATE";
            this.WEIGHT_DATE.HeaderText = "过净磅时期";
            this.WEIGHT_DATE.Name = "WEIGHT_DATE";
            this.WEIGHT_DATE.ReadOnly = true;
            // 
            // bdnInfo
            // 
            this.bdnInfo.AddNewItem = null;
            this.bdnInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.tslPreviousPage2,
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
            this.tsbSelectAll,
            this.toolStripSeparator1,
            this.tsbNotSelectAll,
            this.toolStripSeparator13,
            this.tsbDelete,
            this.toolStripSeparator15});
            this.bdnInfo.Location = new System.Drawing.Point(5, 107);
            this.bdnInfo.MoveFirstItem = null;
            this.bdnInfo.MoveLastItem = null;
            this.bdnInfo.MoveNextItem = null;
            this.bdnInfo.MovePreviousItem = null;
            this.bdnInfo.Name = "bdnInfo";
            this.bdnInfo.PositionItem = null;
            this.bdnInfo.Size = new System.Drawing.Size(983, 29);
            this.bdnInfo.TabIndex = 62;
            this.bdnInfo.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bdnInfo_ItemClicked);
            // 
            // tslHomPage1
            // 
            this.tslHomPage1.Name = "tslHomPage1";
            this.tslHomPage1.Size = new System.Drawing.Size(29, 26);
            this.tslHomPage1.Text = "首页";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 29);
            // 
            // tslPreviousPage2
            // 
            this.tslPreviousPage2.Name = "tslPreviousPage2";
            this.tslPreviousPage2.Size = new System.Drawing.Size(41, 26);
            this.tslPreviousPage2.Text = "上一页";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(53, 26);
            this.toolStripLabel4.Text = "当前页数";
            // 
            // txtCurrentPage2
            // 
            this.txtCurrentPage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentPage2.Name = "txtCurrentPage2";
            this.txtCurrentPage2.Size = new System.Drawing.Size(50, 29);
            // 
            // lblPageCount2
            // 
            this.lblPageCount2.Name = "lblPageCount2";
            this.lblPageCount2.Size = new System.Drawing.Size(35, 26);
            this.lblPageCount2.Text = "共1页";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 29);
            // 
            // tslNextPage1
            // 
            this.tslNextPage1.Name = "tslNextPage1";
            this.tslNextPage1.Size = new System.Drawing.Size(41, 26);
            this.tslNextPage1.Text = "下一页";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 29);
            // 
            // tslLastPage1
            // 
            this.tslLastPage1.Name = "tslLastPage1";
            this.tslLastPage1.Size = new System.Drawing.Size(29, 26);
            this.tslLastPage1.Text = "尾页";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(53, 26);
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
            this.tscbxPageSize2.Size = new System.Drawing.Size(75, 29);
            this.tscbxPageSize2.Text = "10";
            this.tscbxPageSize2.SelectedIndexChanged += new System.EventHandler(this.tscbxPageSize2_SelectedIndexChanged);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 29);
            // 
            // tsbSelectAll
            // 
            this.tsbSelectAll.Name = "tsbSelectAll";
            this.tsbSelectAll.Size = new System.Drawing.Size(29, 26);
            this.tsbSelectAll.Text = "全选";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // tsbNotSelectAll
            // 
            this.tsbNotSelectAll.Name = "tsbNotSelectAll";
            this.tsbNotSelectAll.Size = new System.Drawing.Size(53, 26);
            this.tsbNotSelectAll.Text = "取消全选";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 29);
            this.toolStripSeparator13.Visible = false;
            // 
            // tsbDelete
            // 
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(33, 26);
            this.tsbDelete.Text = "删除";
            this.tsbDelete.Visible = false;
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 29);
            this.toolStripSeparator15.Visible = false;
            // 
            // QCDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 623);
            this.Controls.Add(this.bdnInfo);
            this.Controls.Add(this.dvgCarList);
            this.Controls.Add(this.groupBox2);
            this.Name = "QCDetailsForm";
            this.Text = "质检数据统计详情";
            this.Load += new System.EventHandler(this.QCDetailsForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgCarList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).EndInit();
            this.bdnInfo.ResumeLayout(false);
            this.bdnInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSHIPMENT_NO;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPO_NO;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxState;
        private System.Windows.Forms.TextBox txtCarNO;
        private System.Windows.Forms.Button btnQCOneInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtWEIGHT_TICKET_NO;
        private System.Windows.Forms.DataGridView dvgCarList;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dictionary_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIPMENT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PO_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROD_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO_OF_BALES;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_PumpingPackets;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_MATERIAL_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_MATERIAL_SCALE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_PAPER_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_PAPER_SCALE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_BAGWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_DEGREE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_MOIST_PER_SAMPLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCIInfo_EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT_DATE;
        private System.Windows.Forms.BindingNavigator bdnInfo;
        private System.Windows.Forms.ToolStripLabel tslHomPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripLabel tslPreviousPage2;
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
        private System.Windows.Forms.ToolStripLabel tsbSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tsbNotSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
    }
}