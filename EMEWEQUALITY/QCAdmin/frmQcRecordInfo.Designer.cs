namespace EMEWEQUALITY.QCAdmin
{
    partial class frmQcRecordInfo
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
            this.dgv_SFJC = new System.Windows.Forms.DataGridView();
            this.QCRecord_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_WEIGHT_TICKET_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestItemsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PO_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHIPMENT_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecordDRAW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecordRESULT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_TARE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_BAGWeight2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_PumpingPackets2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_MATERIAL_WEIGHT2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_MATERIAL_SCALE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_PAPER_WEIGHT2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCInfo_PAPER_SCALE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_UPDATE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_UPDATE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_UPDATE_REASON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_ISRETEST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCRecord_REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSHIPMENT_NO = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPO_NO = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxState = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxTestItems2 = new System.Windows.Forms.ComboBox();
            this.txtCarNO = new System.Windows.Forms.TextBox();
            this.btnQCOneInfo = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWEIGHT_TICKET_NO = new System.Windows.Forms.TextBox();
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
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSelectAll = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNotSelectAll = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SFJC)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).BeginInit();
            this.bdnInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_SFJC
            // 
            this.dgv_SFJC.AllowUserToAddRows = false;
            this.dgv_SFJC.AllowUserToDeleteRows = false;
            this.dgv_SFJC.AllowUserToOrderColumns = true;
            this.dgv_SFJC.AllowUserToResizeRows = false;
            this.dgv_SFJC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SFJC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QCRecord_ID,
            this.QCRecord_WEIGHT_TICKET_NO,
            this.CarNo,
            this.TestItemsName,
            this.RecordState,
            this.PO_NO,
            this.SHIPMENT_NO,
            this.QCRecordDRAW,
            this.QCRecordRESULT,
            this.QCRecord_TARE2,
            this.QCInfo_BAGWeight2,
            this.QCInfo_PumpingPackets2,
            this.QCInfo_MATERIAL_WEIGHT2,
            this.QCInfo_MATERIAL_SCALE2,
            this.QCInfo_PAPER_WEIGHT2,
            this.QCInfo_PAPER_SCALE2,
            this.QCRecord_UserName,
            this.QCRecord_TIME,
            this.QCRecord_UPDATE_NAME,
            this.QCRecord_UPDATE_TIME,
            this.QCRecord_UPDATE_REASON,
            this.QCRecord_ISRETEST,
            this.Client_NAME,
            this.QCRecord_REMARK});
            this.dgv_SFJC.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_SFJC.Location = new System.Drawing.Point(0, 137);
            this.dgv_SFJC.Name = "dgv_SFJC";
            this.dgv_SFJC.ReadOnly = true;
            this.dgv_SFJC.RowTemplate.Height = 23;
            this.dgv_SFJC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_SFJC.Size = new System.Drawing.Size(945, 468);
            this.dgv_SFJC.TabIndex = 55;
            // 
            // QCRecord_ID
            // 
            this.QCRecord_ID.DataPropertyName = "QCRecord_ID";
            this.QCRecord_ID.HeaderText = "质检记录编号";
            this.QCRecord_ID.Name = "QCRecord_ID";
            this.QCRecord_ID.ReadOnly = true;
            // 
            // QCRecord_WEIGHT_TICKET_NO
            // 
            this.QCRecord_WEIGHT_TICKET_NO.DataPropertyName = "WEIGHT_TICKET_NO";
            this.QCRecord_WEIGHT_TICKET_NO.HeaderText = "磅单号";
            this.QCRecord_WEIGHT_TICKET_NO.Name = "QCRecord_WEIGHT_TICKET_NO";
            this.QCRecord_WEIGHT_TICKET_NO.ReadOnly = true;
            // 
            // CarNo
            // 
            this.CarNo.DataPropertyName = "CNTR_NO";
            this.CarNo.HeaderText = "车牌号";
            this.CarNo.Name = "CarNo";
            this.CarNo.ReadOnly = true;
            // 
            // TestItemsName
            // 
            this.TestItemsName.DataPropertyName = "TestItems_NAME";
            this.TestItemsName.HeaderText = "检测项目";
            this.TestItemsName.Name = "TestItemsName";
            this.TestItemsName.ReadOnly = true;
            // 
            // RecordState
            // 
            this.RecordState.DataPropertyName = "Dictionary_Name";
            this.RecordState.HeaderText = "质检记录状态";
            this.RecordState.Name = "RecordState";
            this.RecordState.ReadOnly = true;
            // 
            // PO_NO
            // 
            this.PO_NO.DataPropertyName = "PO_NO";
            this.PO_NO.HeaderText = "采购单";
            this.PO_NO.Name = "PO_NO";
            this.PO_NO.ReadOnly = true;
            // 
            // SHIPMENT_NO
            // 
            this.SHIPMENT_NO.DataPropertyName = "SHIPMENT_NO";
            this.SHIPMENT_NO.HeaderText = "送货单";
            this.SHIPMENT_NO.Name = "SHIPMENT_NO";
            this.SHIPMENT_NO.ReadOnly = true;
            // 
            // QCRecordDRAW
            // 
            this.QCRecordDRAW.DataPropertyName = "QCRecord_DRAW";
            this.QCRecordDRAW.HeaderText = "抽检包序号";
            this.QCRecordDRAW.Name = "QCRecordDRAW";
            this.QCRecordDRAW.ReadOnly = true;
            // 
            // QCRecordRESULT
            // 
            this.QCRecordRESULT.DataPropertyName = "QCRecord_RESULT";
            this.QCRecordRESULT.HeaderText = "水分值/重量值";
            this.QCRecordRESULT.Name = "QCRecordRESULT";
            this.QCRecordRESULT.ReadOnly = true;
            this.QCRecordRESULT.Width = 120;
            // 
            // QCRecord_TARE2
            // 
            this.QCRecord_TARE2.DataPropertyName = "QCRecord_TARE";
            this.QCRecord_TARE2.HeaderText = "預置皮重";
            this.QCRecord_TARE2.Name = "QCRecord_TARE2";
            this.QCRecord_TARE2.ReadOnly = true;
            // 
            // QCInfo_BAGWeight2
            // 
            this.QCInfo_BAGWeight2.DataPropertyName = "QCInfo_BAGWeight";
            this.QCInfo_BAGWeight2.HeaderText = "废纸包总重量";
            this.QCInfo_BAGWeight2.Name = "QCInfo_BAGWeight2";
            this.QCInfo_BAGWeight2.ReadOnly = true;
            // 
            // QCInfo_PumpingPackets2
            // 
            this.QCInfo_PumpingPackets2.DataPropertyName = "QCInfo_PumpingPackets";
            this.QCInfo_PumpingPackets2.HeaderText = "总抽包数";
            this.QCInfo_PumpingPackets2.Name = "QCInfo_PumpingPackets2";
            this.QCInfo_PumpingPackets2.ReadOnly = true;
            // 
            // QCInfo_MATERIAL_WEIGHT2
            // 
            this.QCInfo_MATERIAL_WEIGHT2.DataPropertyName = "QCInfo_MATERIAL_WEIGHT";
            this.QCInfo_MATERIAL_WEIGHT2.HeaderText = "杂质重量";
            this.QCInfo_MATERIAL_WEIGHT2.Name = "QCInfo_MATERIAL_WEIGHT2";
            this.QCInfo_MATERIAL_WEIGHT2.ReadOnly = true;
            // 
            // QCInfo_MATERIAL_SCALE2
            // 
            this.QCInfo_MATERIAL_SCALE2.DataPropertyName = "QCInfo_MATERIAL_SCALE";
            this.QCInfo_MATERIAL_SCALE2.HeaderText = "杂质比例";
            this.QCInfo_MATERIAL_SCALE2.Name = "QCInfo_MATERIAL_SCALE2";
            this.QCInfo_MATERIAL_SCALE2.ReadOnly = true;
            // 
            // QCInfo_PAPER_WEIGHT2
            // 
            this.QCInfo_PAPER_WEIGHT2.DataPropertyName = "QCInfo_PAPER_WEIGHT";
            this.QCInfo_PAPER_WEIGHT2.HeaderText = "杂纸重量";
            this.QCInfo_PAPER_WEIGHT2.Name = "QCInfo_PAPER_WEIGHT2";
            this.QCInfo_PAPER_WEIGHT2.ReadOnly = true;
            // 
            // QCInfo_PAPER_SCALE2
            // 
            this.QCInfo_PAPER_SCALE2.DataPropertyName = "QCInfo_PAPER_SCALE";
            this.QCInfo_PAPER_SCALE2.HeaderText = "杂纸比例";
            this.QCInfo_PAPER_SCALE2.Name = "QCInfo_PAPER_SCALE2";
            this.QCInfo_PAPER_SCALE2.ReadOnly = true;
            // 
            // QCRecord_UserName
            // 
            this.QCRecord_UserName.DataPropertyName = "UserName";
            this.QCRecord_UserName.HeaderText = "检测员";
            this.QCRecord_UserName.Name = "QCRecord_UserName";
            this.QCRecord_UserName.ReadOnly = true;
            // 
            // QCRecord_TIME
            // 
            this.QCRecord_TIME.DataPropertyName = "QCRecord_TIME";
            this.QCRecord_TIME.HeaderText = "检测时间";
            this.QCRecord_TIME.Name = "QCRecord_TIME";
            this.QCRecord_TIME.ReadOnly = true;
            // 
            // QCRecord_UPDATE_NAME
            // 
            this.QCRecord_UPDATE_NAME.DataPropertyName = "QCRecord_UserId_UpdateID";
            this.QCRecord_UPDATE_NAME.HeaderText = "修改人";
            this.QCRecord_UPDATE_NAME.Name = "QCRecord_UPDATE_NAME";
            this.QCRecord_UPDATE_NAME.ReadOnly = true;
            // 
            // QCRecord_UPDATE_TIME
            // 
            this.QCRecord_UPDATE_TIME.DataPropertyName = "QCRecord_UPDATE_TIME";
            this.QCRecord_UPDATE_TIME.HeaderText = "修改时间";
            this.QCRecord_UPDATE_TIME.Name = "QCRecord_UPDATE_TIME";
            this.QCRecord_UPDATE_TIME.ReadOnly = true;
            // 
            // QCRecord_UPDATE_REASON
            // 
            this.QCRecord_UPDATE_REASON.DataPropertyName = "QCRecord_UPDATE_REASON";
            this.QCRecord_UPDATE_REASON.HeaderText = "修改原因";
            this.QCRecord_UPDATE_REASON.Name = "QCRecord_UPDATE_REASON";
            this.QCRecord_UPDATE_REASON.ReadOnly = true;
            // 
            // QCRecord_ISRETEST
            // 
            this.QCRecord_ISRETEST.DataPropertyName = "QCRecord_ISRETEST";
            this.QCRecord_ISRETEST.HeaderText = "是否复测";
            this.QCRecord_ISRETEST.Name = "QCRecord_ISRETEST";
            this.QCRecord_ISRETEST.ReadOnly = true;
            // 
            // Client_NAME
            // 
            this.Client_NAME.DataPropertyName = "Client_NAME";
            this.Client_NAME.HeaderText = "检测点名称";
            this.Client_NAME.Name = "Client_NAME";
            this.Client_NAME.ReadOnly = true;
            // 
            // QCRecord_REMARK
            // 
            this.QCRecord_REMARK.DataPropertyName = "QCRecord_REMARK";
            this.QCRecord_REMARK.HeaderText = "质检备注";
            this.QCRecord_REMARK.Name = "QCRecord_REMARK";
            this.QCRecord_REMARK.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSHIPMENT_NO);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtPO_NO);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cboxState);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboxTestItems2);
            this.groupBox2.Controls.Add(this.txtCarNO);
            this.groupBox2.Controls.Add(this.btnQCOneInfo);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtWEIGHT_TICKET_NO);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(945, 105);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "搜    索";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtSHIPMENT_NO
            // 
            this.txtSHIPMENT_NO.Location = new System.Drawing.Point(223, 65);
            this.txtSHIPMENT_NO.MaxLength = 13;
            this.txtSHIPMENT_NO.Name = "txtSHIPMENT_NO";
            this.txtSHIPMENT_NO.Size = new System.Drawing.Size(91, 21);
            this.txtSHIPMENT_NO.TabIndex = 56;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(170, 68);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 55;
            this.label17.Text = "送货单：";
            // 
            // txtPO_NO
            // 
            this.txtPO_NO.Location = new System.Drawing.Point(68, 65);
            this.txtPO_NO.MaxLength = 13;
            this.txtPO_NO.Name = "txtPO_NO";
            this.txtPO_NO.Size = new System.Drawing.Size(91, 21);
            this.txtPO_NO.TabIndex = 54;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(18, 68);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 53;
            this.label18.Text = "采购单：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(563, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "数据状态：";
            // 
            // cboxState
            // 
            this.cboxState.FormattingEnabled = true;
            this.cboxState.Location = new System.Drawing.Point(634, 25);
            this.cboxState.Name = "cboxState";
            this.cboxState.Size = new System.Drawing.Size(121, 20);
            this.cboxState.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(333, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "检测项目：";
            // 
            // cboxTestItems2
            // 
            this.cboxTestItems2.FormattingEnabled = true;
            this.cboxTestItems2.Location = new System.Drawing.Point(404, 25);
            this.cboxTestItems2.Name = "cboxTestItems2";
            this.cboxTestItems2.Size = new System.Drawing.Size(121, 20);
            this.cboxTestItems2.TabIndex = 26;
            // 
            // txtCarNO
            // 
            this.txtCarNO.Location = new System.Drawing.Point(67, 25);
            this.txtCarNO.Name = "txtCarNO";
            this.txtCarNO.Size = new System.Drawing.Size(91, 21);
            this.txtCarNO.TabIndex = 25;
            // 
            // btnQCOneInfo
            // 
            this.btnQCOneInfo.Location = new System.Drawing.Point(792, 63);
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
            this.label8.Location = new System.Drawing.Point(19, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "车牌号：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(170, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "磅单号：";
            // 
            // txtWEIGHT_TICKET_NO
            // 
            this.txtWEIGHT_TICKET_NO.Location = new System.Drawing.Point(223, 25);
            this.txtWEIGHT_TICKET_NO.MaxLength = 13;
            this.txtWEIGHT_TICKET_NO.Name = "txtWEIGHT_TICKET_NO";
            this.txtWEIGHT_TICKET_NO.Size = new System.Drawing.Size(91, 21);
            this.txtWEIGHT_TICKET_NO.TabIndex = 11;
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
            this.tsbDelete,
            this.toolStripSeparator13,
            this.tsbSelectAll,
            this.toolStripSeparator1,
            this.tsbNotSelectAll});
            this.bdnInfo.Location = new System.Drawing.Point(0, 105);
            this.bdnInfo.MoveFirstItem = null;
            this.bdnInfo.MoveLastItem = null;
            this.bdnInfo.MoveNextItem = null;
            this.bdnInfo.MovePreviousItem = null;
            this.bdnInfo.Name = "bdnInfo";
            this.bdnInfo.PositionItem = null;
            this.bdnInfo.Size = new System.Drawing.Size(945, 29);
            this.bdnInfo.TabIndex = 57;
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
            // tsbDelete
            // 
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(33, 26);
            this.tsbDelete.Text = "删除";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 29);
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
            // frmQcRecordInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 605);
            this.Controls.Add(this.bdnInfo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgv_SFJC);
            this.DoubleBuffered = true;
            this.Name = "frmQcRecordInfo";
            this.Text = "检测详情";
            this.Load += new System.EventHandler(this.frmQcRecordInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SFJC)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdnInfo)).EndInit();
            this.bdnInfo.ResumeLayout(false);
            this.bdnInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_SFJC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSHIPMENT_NO;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPO_NO;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboxTestItems2;
        private System.Windows.Forms.TextBox txtCarNO;
        private System.Windows.Forms.Button btnQCOneInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtWEIGHT_TICKET_NO;
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
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripLabel tsbSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tsbNotSelectAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_WEIGHT_TICKET_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestItemsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordState;
        private System.Windows.Forms.DataGridViewTextBoxColumn PO_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIPMENT_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecordDRAW;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecordRESULT;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_TARE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_BAGWeight2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_PumpingPackets2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_MATERIAL_WEIGHT2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_MATERIAL_SCALE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_PAPER_WEIGHT2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCInfo_PAPER_SCALE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_UPDATE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_UPDATE_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_UPDATE_REASON;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_ISRETEST;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCRecord_REMARK;
    }
}