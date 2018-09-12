using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//导入引用
using System.Data.SqlClient;
using EMEWEQUALITY.HelpClass;
using EMEWEEntity;
using EMEWEDAL;
using EMEWE.Common;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using WindowsFormsApplication26;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using WindowsFormsApplication14;
using Word = Microsoft.Office.Interop.Word;
using System.IO;

namespace EMEWEQUALITY.Statistics
{
    public partial class CarStatisticsForm : Form
    {
        public CarStatisticsForm()
        {
            InitializeComponent();
        }
        public MainFrom mf;//主窗体
        /// <summary>
        /// 车辆表  QCInfo
        /// </summary>
        Expression<Func<View_QCInfo, bool>> expr = null;
        //// 获取质检表中的车牌号
        Expression<Func<View_QCInfo, string>> p = n => n.CNTR_NO;
        string Cntr_No = "";
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();
        /// </summary>
        /// 排序字段及方式[以空格格开]
        /// </summary>
        private string orderbywhere = "QCInfo_ID desc";


        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private void LoadData(string strName)
        {
            try
            {
                this.dgvCarStatisticsQC.DataSource = null;
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                dgvCarStatisticsQC.DataSource = page.BindBoundControl<View_QCInfo>(strName, txtCurrentPage2, lblPageCount2, expr, orderbywhere);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("日记管理 车辆质检统计(CarStatisticsForm)加载显示" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {
            mf = new MainFrom();
            TimeEndIsNull();
            BindCheckState();//绑定质检状态
            this.dgvCarStatisticsQC.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            btnSearch_Click(null,null);
            //DateTimeBeginISNull();
            //DateTimeEndIsNull();
        }
        /// <summary>
        /// 初始化时间
        /// </summary>
        private void TimeEndIsNull()
        {
            DateTime Time = DateTime.Now.AddDays(0);
            txtbeginTime.Value = Time;
            txtendTime.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarStatisticsForm_Load(object sender, EventArgs e)
        {
            InitUser();
        }
        /// <summary>
        /// 绑定质检状态的comboBox值
        /// </summary>
        private void BindCheckState()
        {
            cbxQCInfoState.DataSource = DictionaryDAL.GetValueStateDictionary("质检状态");
            this.cbxQCInfoState.DisplayMember = "Dictionary_Name";
            cbxQCInfoState.ValueMember = "Dictionary_ID";
            if (cbxQCInfoState.DataSource != null)
            {
                
                cbxQCInfoState.SelectedIndex = cbxQCInfoState.Items.Count - 1;
            }
        }

        /// <summary>
        /// 搜 索 车辆质检统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LogInFoBang("");
        }

        private void LogInFoBang(string strname)
        {
            try
            {
                if (expr == null)
                {
                    expr = (Expression<Func<View_QCInfo, bool>>)PredicateExtensionses.True<View_QCInfo>();
                }
                int i = 0;
                #region 2013-3-23 ZJ 取消注释，已经增加了视图View_QCInfo

                //if (this.cbxQCInfoState.SelectedValue.ToString() != "0")//检测状态
                //{
                //    int stateID = Converter.ToInt(cbxQCInfoState.SelectedValue.ToString());
                //    if (stateID > 0)
                //    {
                //        expr = expr.And(n => n.Dictionary_ID == Converter.ToInt(cbxQCInfoState.SelectedValue.ToString()));
                //        i++;
                //    }
                //}
                #endregion
                if (this.txtQCInfoCarNo.Text != "")//车牌号
                {
                    expr = expr.And(n => n.CNTR_NO.Contains(txtQCInfoCarNo.Text));
                    i++;
                }
                if (this.txtWaitWEIGHTTICKETNO.Text != "")//磅单号
                {
                    expr = expr.And(n => n.WEIGHT_TICKET_NO.Contains(txtWaitWEIGHTTICKETNO.Text));
                    i++;
                }
                if (this.txtWaitPO_NO.Text != "")//采购单
                {
                    expr = expr.And(n => n.PO_NO.Contains(txtWaitPO_NO.Text));
                    i++;
                }
                if (this.txtWaitSHIPMENT_NO.Text != "")//送货单
                {
                    expr = expr.And(n => n.SHIPMENT_NO.Contains(txtWaitSHIPMENT_NO.Text));
                    i++;
                }
                #region 2013-3-23 ZJ 取消注释，已经增加了视图View_QCInfo

                string beginTime = txtbeginTime.Value.ToString("yyyy-MM -dd ");
                string endTime = txtendTime.Value.ToString("yyyy-MM -dd ");
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "
                if (beginTime != "" && endTime == "")//开始时间
                {
                    expr = expr.And(n => n.QCInfo_TIME >= Common.GetDateTime(begin));
                    i++;
                }
                if (beginTime == "" && endTime != "")//结束时间   QCInfo表字段:QCIInfo_EndTime
                {
                    expr = expr.And(n => n.QCInfo_TIME <= Common.GetDateTime(end));
                    i++;
                }
                if (beginTime != "" && endTime != "")
                {
                    if (Common.GetDateTime(beginTime) > Common.GetDateTime(endTime))
                    {
                        MessageBox.Show("查询开始时间不能大于结束时间！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtbeginTime.Text = "";
                        txtendTime.Text = "";
                        return;
                    }
                    else
                    {
                        expr = expr.And(n => n.QCInfo_TIME >= Common.GetDateTime(begin) && n.QCInfo_TIME <= Common.GetDateTime(end));
                        i++;
                    }

                }

                #endregion
                if (i == 0)
                {
                    expr = null;
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("项目检测管理 btnSearch_Click()" + ex.Message.ToString());
            }
            finally
            {
                LoadData(strname);
                expr = null;
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click()
        {
            for (int i = 0; i < dgvCarStatisticsQC.Rows.Count; i++)
            {
                dgvCarStatisticsQC.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click()
        {
            for (int i = 0; i < this.dgvCarStatisticsQC.Rows.Count; i++)
            {
                dgvCarStatisticsQC.Rows[i].Selected = false;
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Excel()
        {
            try
            {
                if (this.dgvCarStatisticsQC.SelectedRows.Count > 0)//选中行
                {
                    if (dgvCarStatisticsQC.SelectedRows.Count > 1 || dgvCarStatisticsQC.SelectedRows[0].Cells["CNTR_NO"].Value == null)
                    {
                        MessageBox.Show("打印只能选中一行且打印行质检编号信息不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        printdemoformweiht(Common.GetInt(dgvCarStatisticsQC.SelectedRows[0].Cells["CNTR_NO"].Value.ToString()));
                    }
                }
                else
                {
                    MessageBox.Show("请选择中要打印的行", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("CarStatisicsForm.printdemoformweiht() 打印事件" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="qcinfo_id">质检ID</param>
        private void printdemoformweiht(int qcinfo_id)
        {
            try
            {
                //if (MessageBox.Show("是否确定打印吗？", "打印提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    if (qcinfo_id <= 0) return;
                //    PrintDemoFormWeight pdfw = new PrintDemoFormWeight();
                //    Expression<Func<View_QCInfo_InTerface, bool>> fun = n => n.QCInfo_ID == qcinfo_id;
                //    var viemqcinfointerface = QCInfoDAL.Query(fun);
                //    if (viemqcinfointerface == null) return;
                //    foreach (var m in viemqcinfointerface)
                //    {
                //        if (m.QCInfo_ID > 0) //检测编号
                //        {
                //            pdfw.QcInfo_ID = m.QCInfo_ID.ToString();

                //        }
                //        if (!string.IsNullOrEmpty(m.CNTR_NO))//车牌号码
                //        {
                //            pdfw.CNTR_NO = m.CNTR_NO;
                //        }
                //        if (!string.IsNullOrEmpty(m.REF_NO))//REF_NO
                //        {
                //            pdfw.REF_NO = m.REF_NO;
                //        }
                //        if (!string.IsNullOrEmpty(m.PO_NO))//采购单号
                //        {
                //            pdfw.PO_NO = m.PO_NO;
                //        }
                //        if (!string.IsNullOrEmpty(m.SHIPMENT_NO))//送货通知书编号
                //        {
                //            pdfw.SHIPMENT_NO = m.SHIPMENT_NO;
                //        }
                //        if (m.NO_OF_BALES > 0)//送货件数
                //        {
                //            pdfw.NO_OF_BALES = m.NO_OF_BALES.ToString();
                //        }
                //        if (m.QCInfo_PumpingPackets > 0)//抽检件数
                //        {
                //            pdfw.QCInfo_PumpingPackets = m.QCInfo_PumpingPackets.ToString();
                //        }
                //        if (m.QCInfo_RETURNBAG_TIME != null)//退货还包称重时间
                //        {
                //            pdfw.QCInfo_RETURNBAG_TIME = m.QCInfo_RETURNBAG_TIME.ToString();
                //        }
                //        if (m.QCInfo_BAGWeight > 0) //抽检重量
                //        {
                //            pdfw.QCInfo_BAGWeight = m.QCInfo_BAGWeight.ToString();
                //        }
                //        if (m.QCInfo_RETURNBAG_WEIGH > 0)//退货重量
                //        {
                //            pdfw.QCInfo_RETURNBAG_WEIGH = m.QCInfo_RETURNBAG_WEIGH.ToString();
                //        }
                //        if (m.QCInfo_MATERIAL_WEIGHT > 0)//杂质重量
                //        {
                //            pdfw.QCInfo_MATERIAL_WEIGHT = m.QCInfo_MATERIAL_WEIGHT.ToString();
                //        }


                //        break;

                //    }
                //    Expression<Func<View_QCRecordInfo, bool>> funcvierqcrecordinfo = n => n.QCInfo_ID == qcinfo_id && n.TestItems_NAME != "拆包后水分检测" && n.TestItems_NAME != "拆包前水分检测" && n.TestItems_NAME != "拆包前水分检测";
                //    var view_QCRecordInfo = from n in QCInfoDAL.Query(funcvierqcrecordinfo)
                //                            orderby QCRecord_TIME descending
                //                            select n;
                //    if (view_QCRecordInfo != null)
                //    {
                //        foreach (var p in view_QCRecordInfo)
                //        {
                //            if (!string.IsNullOrEmpty(p.UserLoginId))//司称员
                //            {
                //                pdfw.QCRecord_Name = p.UserLoginId;
                //            }
                //            if (p.QCRecord_TIME != null)//抽检称重时间
                //            {
                //                pdfw.QCRecord_TIME = p.QCRecord_TIME.ToString();
                //            }
                //            break;
                //        }
                //    }
                //    pdfw.GetPrintData();//打印预览
                //}
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("FormWeight.printdemoformweiht()" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 分 页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tslSelectAll") // 全选
            {
                tslSelectAll_Click();
                return;
            }
            if (e.ClickedItem.Name == "tslNotSelect") // 取消全选
            {
                tslNotSelect_Click();
                return;
            }
            // 查看详情
            if (e.ClickedItem.Name == "tslDetails")
            {
                if (this.dgvCarStatisticsQC.SelectedRows.Count > 0) // 判断是否选中
                {
                    if (dgvCarStatisticsQC.SelectedRows.Count > 1) // 判断收选中多行
                    {
                        MessageBox.Show("查看详细只能选中一行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string No = this.dgvCarStatisticsQC.SelectedRows[0].Cells["CarNo"].Value.ToString();
                        Cntr_No = No;
                        if (Cntr_No != null || Cntr_No != "")
                        {
                            CarDetailsForm frm = new CarDetailsForm();
                            frm.Owner = this;
                            frm.cntr_no = Cntr_No;
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("选中行的车牌号不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择要查看详细的行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (e.ClickedItem.Name == "tslWord")
            {
                return;
            }
            if (e.ClickedItem.Name == "tslExport")
            {
                return;
            }
           //this.dgvCarStatisticsQC.DataSource = page.BindBoundControl<View_QCInfo>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr, orderbywhere);
            LogInFoBang(e.ClickedItem.Name);
        }
        /// <summary>
        /// 设置每页显示信息条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData("");
        }


        #region  DataTimePicker控件的自定义显示  和 KeyPress事件
        /// <summary>
        /// DataTimePicker控件的文本操作方法
        /// </summary>
        private void DateTimeBeginISNull()
        {
            this.txtbeginTime.Format = DateTimePickerFormat.Custom;
            this.txtbeginTime.CustomFormat = " ";
        }
        private void DateTimeEndIsNull()
        {
            this.txtendTime.Format = DateTimePickerFormat.Custom;
            this.txtendTime.CustomFormat = " ";
        }
        /// <summary>
        /// 将“DataTiemPicker”控件赋值为null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbeginTime_ValueChanged(object sender, EventArgs e)
        {
            this.txtbeginTime.Format = DateTimePickerFormat.Custom;
            this.txtbeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
        private void txtendTime_ValueChanged(object sender, EventArgs e)
        {
            this.txtendTime.Format = DateTimePickerFormat.Custom;
            this.txtendTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
        /// <summary>
        /// “DataTiemPicker”控件的键盘事件 KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbeginTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                DateTimeBeginISNull();
            }
        }
        private void txtendTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            if (e.KeyChar == '\b')
            {
                DateTimeEndIsNull();
            }
        }
        #endregion


        /// <summary>
        /// 导出文档 -- Ｅｘｃｅｌ的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslExport_Click(object sender, EventArgs e)
        {
            string fileName = "理文检测系统车辆质检统计Excel报表-" + DateTime.Now.ToShortDateString();
            tslExport_Excel(fileName, dgvCarStatisticsQC);
        }
        /// <summary>
        /// 导出Excel 的方法
        /// </summary>
        private void tslExport_Excel(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            //bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消 
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1 


            //写入标题
            for (int i = 1; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 1; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            Microsoft.Office.Interop.Excel.Range rang = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[myDGV.Rows.Count + 2, 2]);
            rang.NumberFormat = "000000000000";

            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    //fileSaved = true;
                }
                catch (Exception ex)
                {
                    //fileSaved = false;
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }

            }
            //else
            //{
            //    fileSaved = false;
            //}
            xlApp.Quit();
            GC.Collect();//强行销毁 
            // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
            MessageBox.Show(fileName + ",保存成功", "提示", MessageBoxButtons.OK);

        }

        /// <summary>
        /// 导出文档 -- Ｗｏｒｄ的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslWord_Click(object sender, EventArgs e)
        {
            tslExport_Word();
        }
        /// <summary>
        /// 导出Word 的方法
        /// </summary>
        private void tslExport_Word()
        {
            #region 方法一
            SaveFileDialog sfile = new SaveFileDialog();
            sfile.AddExtension = true;
            sfile.DefaultExt = ".doc";
            sfile.Filter = "(*.doc)|*.doc";
            sfile.FileName = "理文检测系统车辆质检统计Word报表" + DateTime.Now.ToShortDateString();
            if (sfile.ShowDialog() == DialogResult.OK)
            {
                object path = sfile.FileName;
                Object none = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document document = wordApp.Documents.Add(ref none, ref none, ref none, ref none);
                //建立表格
                Microsoft.Office.Interop.Word.Table table = document.Tables.Add(document.Paragraphs.Last.Range, dgvCarStatisticsQC.Rows.Count + 1, dgvCarStatisticsQC.Columns.Count, ref none, ref none);
                try
                {
                    for (int i = 0; i < dgvCarStatisticsQC.Columns.Count; i++)//设置标题
                    {
                        table.Cell(0, i + 1).Range.Text = dgvCarStatisticsQC.Columns[i].HeaderText;
                    }
                    //for (int i = 0; i < dgvCarStatisticsQC.Rows.Count; i++)//填充数据
                    //{
                    //    i++;
                    //    for (int j = 0; j < dgvCarStatisticsQC.Columns.Count; j++)
                    //    {
                    //        //if (dgvCarStatisticsQC[i-1, j].Value != null)
                    //        //{
                    //            table.Cell(i + 1, j + 1).Range.Text = dgvCarStatisticsQC.Rows[i-1].Cells[j].Value.ToString();
                    //        //}
                    //    }
                    //}

                    for (int i = 1; i <= dgvCarStatisticsQC.Rows.Count; i++)//填充数据
                    {
                        for (int j = 0; j < dgvCarStatisticsQC.Columns.Count; j++)//dgvCarStatisticsQC.Columns.Count
                        {
                            //table.Cell(i + 1, j + 1).Range.Text = dgvCarStatisticsQC[j, i - 1].Value.ToString();
                            if (dgvCarStatisticsQC[j, i - 1].Value != null)
                            {
                                // table.Cell(i + 1, j + 1).Range.Text = dgvCarStatisticsQC[j, i - 1].Value.ToString();
                                table.Cell(i + 1, j + 1).Range.Text = dgvCarStatisticsQC[j, i - 1].Value.ToString();
                            }
                            else
                            {
                                table.Cell(i + 1, j + 1).Range.Text = "";
                            }
                        }
                    }

                    document.SaveAs(ref path, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none);
                    document.Close(ref none, ref none, ref none);
                    MessageBox.Show("导出数据成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    wordApp.Quit(ref none, ref none, ref none);
                }
            }
            #endregion

            #region 方法二
            //Word.Document mydoc = new Word.Document();
            //Word.Table mytable = null;
            //Word.Selection mysel;
            //Object myobj;

            ////建立Word对象
            //Word.Application word = new Word.Application();
            //myobj = System.Reflection.Missing.Value;
            //mydoc = word.Documents.Add(ref myobj, ref myobj, ref myobj, ref myobj);
            //word.Visible = true;
            //mydoc.Select();
            //mysel = word.Selection;
            ////将数据生成Word表格文件
            //mytable = mydoc.Tables.Add(mysel.Range, this.dgvCarStatisticsQC.RowCount, this.dgvCarStatisticsQC.ColumnCount, ref myobj, ref myobj);
            ////设置列宽
            //mytable.Columns.SetWidth(30, Word.WdRulerStyle.wdAdjustNone);
            ////输出列标题数据
            //for (int i = 0; i < this.dgvCarStatisticsQC.ColumnCount; i++)
            //{
            //    mytable.Cell(1, i + 1).Range.InsertAfter(this.dgvCarStatisticsQC.Columns[i].HeaderText);
            //}
            ////输出控件中的记录
            //for (int i = 0; i < this.dgvCarStatisticsQC.RowCount - 1; i++)
            //{
            //    for (int j = 0; j < this.dgvCarStatisticsQC.ColumnCount; j++)
            //    {
            //        mytable.Cell(i + 2, j + 1).Range.InsertAfter(this.dgvCarStatisticsQC[j, i].Value.ToString());
            //    }
            //}
            #endregion
        }

        /// <summary>
        /// 导出文档 -- Ｔｘｔ的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void tslTxt_Click(object sender, EventArgs e)
        //{
        //    tslExport_Txt();
        //}
        /// <summary>
        /// 导出Txt 的方法
        /// </summary>
        private void tslExport_Txt()
        {
            this.saveFileDialog1.FileName = "理文检测系统车辆质检统计Txt报表" + DateTime.Now.ToShortDateString(); //默认文件名
            this.saveFileDialog1.Filter = "文本文档（*.txt）|*.txt";                //默认文件格式
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)               //用户点击保存
            {
                StreamWriter sw = File.CreateText(this.saveFileDialog1.FileName);
                string strLine = "";

                //写标题
                for (int i = 0; i <= this.dgvCarStatisticsQC.Columns.Count - 1; i++)
                {
                    strLine += this.dgvCarStatisticsQC.Columns[i].HeaderText + "       ";
                }
                sw.WriteLine(strLine);
                sw.WriteLine("----------------------------------------------------------------------------------------------");

                //写入数据
                System.Data.DataTable dt = (System.Data.DataTable)this.dgvCarStatisticsQC.DataSource;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    strLine = "";
                    for (int j = 0; j <= dt.Columns.Count - 1; j++)
                    {
                        strLine += dt.Rows[i][j].ToString() + "       ";
                    }
                    sw.WriteLine(strLine);
                }
                //写页脚
                sw.WriteLine("---------------------------------------------------------------------------------------------");
                sw.WriteLine("导出时间：" + DateTime.Now.ToString());
                sw.Flush();    //之前写入的是缓冲区，现在更新到文件中去
                MessageBox.Show("数据保存到" + this.saveFileDialog1.FileName, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}