using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Configuration;
using System.Threading;
using Microsoft.Office.Interop.Excel;
namespace WindowsFormsApplication26
{
    public class Exceldc
    {
        //数据连接字符串
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EMEWEQCConnectionString"].ConnectionString;//ConfigurationSettings.AppSettings["conn"].ToString();
        //private string _connectionString = @"Data Source=XP-201111071750\SQLEXPRESSSBVO;PassWord =123456 ;User ID=sa;Initial Catalog=kangdepmcn2011-11-07;";
        //Excek[sheet]的数量
        private int sheetcount = Convert.ToInt32(ConfigurationSettings.AppSettings["sheetcount"].ToString());
        private double dbSheetSize = 65535;//
        private int intSheetTotalSize = 0;//
        private double dbTotalSize = 0;//
        public  Thread thread;
        Excel.Range m_objRange = null;
        Excel.ApplicationClass objExcel = null;
        Excel.Workbooks objBooks = null;
        Excel.Workbook objBook = null;
        Excel.Sheets m_objSheets = null;
        Excel.Worksheet objSheet = null;
        Excel.Worksheet m_objSheet = null;
        Excel.QueryTable tb = null;
        object missing = System.Reflection.Missing.Value;

        SqlConnection sqlConn = null;
        private string strConnect = string.Empty;
        SqlCommand sqlCmd = null;

        private string strFileName = ""; 
        //封装属性
        private string[] strTitle;

        public string[] StrTitle
        {
            get { return strTitle; }
            set { strTitle = value; }
        }
        private string strSql;

        public string StrSql
        {
            get { return strSql; }
            set { strSql = value; }
        }
        private string strTableName;

        public string StrTableName
        {
            get { return strTableName; }
            set { strTableName = value; }
        }
        private string strMastTitle;

        public string StrMastTitle
        {
            get { return strMastTitle; }
            set { strMastTitle = value; }
        }
        private string TableIDName;

        public string TableIDName1
        {
            get { return TableIDName; }
            set { TableIDName = value; }
        }
        private string sqlwhere;

        public string Sqlwhere
        {
            get { return sqlwhere; }
            set { sqlwhere = value; }
        }
        private string sqlwhere1;

        public string Sqlwhere1
        {
            get { return sqlwhere1; }
            set { sqlwhere1 = value; }
        }

        public string StrFileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }
        /// <summary>
        ///获取Excel文件存放路径
        /// </summary>
        /// <param name="strFileName">初始存放文件名</param>
        /// <returns></returns>
        public  string SaveExcelApp(string strFileName)
        {
           
            string excelFileName = string.Empty;
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = strFileName + DateTime.Now.ToShortDateString();
            sf.Filter = "*.xls|*.*";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                excelFileName = sf.FileName.ToString() + ".xls";

            }
            else
            {
                return "";
            }
            return excelFileName;

        }
        /// <summary>
        /// 取得总记录数跟可分成几个Excel sheet.
        /// </summary>
        /// <param name="strTableName">被查询的数据库的表名</param>
        /// <param name="sqlwhere">查询条件【包含where】</param>
        /// <returns>可分成Excel Sheet的个数</returns>
        private int GetTotalSize(string strTableName, string sqlwhere)
        {
            try
            {
                sqlConn = new SqlConnection(_connectionString);
                string sql = "Select Count(*) From " + strTableName + sqlwhere;
                sqlCmd = new SqlCommand(sql, sqlConn);
                if (this.sqlConn.State == ConnectionState.Closed) sqlConn.Open();
                dbTotalSize = (int)sqlCmd.ExecuteScalar();
                sqlConn.Close();
                return (int)Math.Ceiling(dbTotalSize / this.dbSheetSize);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return (int)Math.Ceiling(dbTotalSize / this.dbSheetSize);
        }

        /// <summary>
        /// 创建一个Excel实例
        /// </summary>
        ///// <param name="strTitle">表头,需与查询sql语句对齐一致。</param>
        ///// <param name="strSql">查询的sql语句，表头的文字需与该sql语句对齐一致。</param>
        ///// <param name="strTableName">查询的表名</param>
        ///// <param name="strMastTitle">标题</param>
        ///// <param name="TableIDName">主键</param>
        ///// <param name="sqlwhere">查询条件包含[where]</param>
        //public void DeclareExcelApp(string[] strTitle, string strSql, string strTableName, string strMastTitle, string TableIDName, string sqlwhere)
        public void DeclareExcelApp()
        {

            // 创建一个Excel实例
            objExcel = new Excel.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excel.Workbooks)objExcel.Workbooks;
            objBook = (Excel.Workbook)(objBooks.Add(missing));
            objSheet = (Microsoft.Office.Interop.Excel.Worksheet)objBook.ActiveSheet;
            m_objSheets = (Excel.Sheets)objBook.Worksheets;

            if (strFileName == "")
            {
                MessageBox.Show("文件路径不能为空");
                return;
            }

            //string strFileName = SaveExcelApp(strMastTitle);//获取文件存放路径
            intSheetTotalSize = GetTotalSize(strTableName, sqlwhere1);//获取分成几个SHEET
            try
            {
                if (intSheetTotalSize <= sheetcount)
                {

                    if (intSheetTotalSize <= 3)
                    {
                        if (this.dbTotalSize <= this.dbSheetSize)
                        {
                            this.ExportDataByQueryTable(1, false, strTitle, strSql, strTableName, strMastTitle, TableIDName, sqlwhere, strFileName);

                        }
                        else if (this.dbTotalSize <= this.dbSheetSize * 2)
                        {
                            this.ExportDataByQueryTable(1, false, strTitle, strSql, strTableName, strMastTitle, TableIDName, sqlwhere, strFileName);
                            this.ExportDataByQueryTable(2, true, strTitle, strSql, strTableName, strMastTitle, TableIDName, sqlwhere, strFileName);

                        }
                        else
                        {
                            this.ExportDataByQueryTable(1, false, strTitle, strSql, strTableName, strMastTitle, TableIDName, sqlwhere, strFileName);
                            this.ExportDataByQueryTable(2, false, strTitle, strSql, strTableName, strMastTitle, TableIDName, sqlwhere, strFileName);
                            this.ExportDataByQueryTable(3, true, strTitle, strSql, strTableName, strMastTitle, TableIDName, sqlwhere, strFileName);

                        }
                    }
                    for (int i = 3; i < intSheetTotalSize; i++)
                    {
                        m_objSheets.Add(missing, m_objSheets.get_Item(i), missing, missing);
                    }
                    this.ExportDataByQueryTable(1, false, strTitle, strSql, strTableName, strMastTitle, TableIDName, sqlwhere, strFileName);
                    for (int i = 2; i <= m_objSheets.Count; i++)
                    {
                        this.ExportDataByQueryTable(i, true, strTitle, strSql, strTableName, strMastTitle, TableIDName, sqlwhere, strFileName);
                    }


                    //保存excel文件在服务器  

                    //关闭Microsoft.Office.Interop.Excel  

                    objBook.SaveAs(strFileName, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);

                    objBook.Close(false, missing, missing);

                    objBooks.Close();

                    objExcel.Quit();

                    MessageBox.Show("Excel成功导出");
                }
                else
                {
                    MessageBox.Show("导出的数据过大请重新设置查询条件");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }



            finally
            {

                //释放资源  

                if (!objSheet.Equals(null))
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
                if (objBook != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
                if (objBooks != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
                if (objExcel != null)

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);

                GC.Collect();
                KillProcess("EXCEL");

            }


        }

        /// <param name="strSql"></param>  
        /// <summary>
        /// 使用QueryTable从一个外部数据源创建Worksheet
        /// </summary>
        /// <param name="intSheetNumber">导出第几个sheet</param>
        /// <param name="blIsMoreThan">余下的数据是否大于指定的每个Sheet的最大记录数</param>
        /// <param name="strTitle">表头,需与查询sql语句对齐一致。</param>
        /// <param name="strSql">查询的sql语句，表头的文字需与该sql语句对齐一致。</param>
        /// <param name="strTablName">查询的表名</param>
        /// <param name="strMastTitle">标题</param>
        /// <param name="TableIDName">主键</param>
        /// <param name="sqlwhere">查询条件包含[where]</param>
        /// <param name="strFileName">Excel文件导出路径</param>
        public void ExportDataByQueryTable(int intSheetNumber, bool blIsMoreThan, string[] strTitle, string strSql, string strTablName, string strMastTitle, string TableIDName, string sqlwhere, string strFileName)
        {
            try
            {
                
                string strQuery = string.Empty;
                string sqlwhere1 = sqlwhere != "" ? "" : sqlwhere.Substring(7, sqlwhere.Length - 7);

                if (blIsMoreThan)
                {
                    strQuery = String.Format("select top {0} from {1} where not {2} in (select top {3} from {4} {5}) {6}", dbSheetSize + " " + strSql, strTablName, TableIDName, dbSheetSize * (intSheetNumber - 1) + TableIDName, strTablName, sqlwhere, sqlwhere1);

                }
                else
                {
                    strQuery = String.Format("Select Top {0} from {1} {2}", dbSheetSize + " " + strSql, strTablName, sqlwhere);

                }



                int strTitleNnamecount = strTitle.Length;
                m_objSheet = (Excel.Worksheet)(m_objSheets.get_Item(intSheetNumber));//操作哪个SHEET
                

                //m_objSheet.Name = strMastTitle + intSheetNumber.ToString() + DateTime.Now.ToShortDateString(); //sheet名称
                m_objSheet.Cells[1, 1] = strMastTitle;//标题
                m_objSheet.Cells[2, 1] = "打印日期" + DateTime.Now.ToShortDateString();
                //写入标题
                for (int i = 1; i <= strTitleNnamecount; i++)
                {
                    m_objSheet.Cells[3, i] = strTitle[i - 1].ToString();
                }


                m_objRange = m_objSheet.get_Range("A4", missing);//从第四行开始写入

                //格式设置
                m_objSheet.get_Range(m_objSheet.Cells[1, 1], m_objSheet.Cells[1, strTitleNnamecount]).MergeCells = true; //合并单元格
                m_objSheet.get_Range(m_objSheet.Cells[2, 1], m_objSheet.Cells[2, strTitleNnamecount]).MergeCells = true; //合并单元格

                //标题设置
                Excel.Range m_objRangeMastTitle = m_objSheet.get_Range(m_objSheet.Cells[1, 1], m_objSheet.Cells[1, strTitleNnamecount]);
                m_objRangeMastTitle.Font.Name = "黑体";//设置字体
                m_objRangeMastTitle.Font.Size = 16;//设置字体大小
                m_objRangeMastTitle.Font.Bold = true;//字体加粗
                m_objRangeMastTitle.HorizontalAlignment = XlHAlign.xlHAlignCenter;//水平居中
                m_objRangeMastTitle.VerticalAlignment = XlVAlign.xlVAlignCenter;//垂直居中

                //标题设置
                Excel.Range m_objRangeTitle = m_objSheet.get_Range(m_objSheet.Cells[3, 1], m_objSheet.Cells[3, strTitleNnamecount]);
                m_objRangeTitle.Font.Name = "黑体";//设置字体
                m_objRangeTitle.Font.Size = 12;//设置字体大小
                m_objRangeTitle.Font.Bold = true;//字体加粗
                m_objRangeTitle.HorizontalAlignment = XlHAlign.xlHAlignCenter;//水平居中
                m_objRangeTitle.VerticalAlignment = XlVAlign.xlVAlignCenter;//垂直居中

                //参数依次为：数据连接，填充起始单元格，查询SQL语句  
                tb = m_objSheet.QueryTables.Add("OLEDB;Provider=SQLOLEDB.1;" + _connectionString, m_objRange, strQuery);
                tb.Refresh(tb.BackgroundQuery);//是否异步查询 
                //区域删除【第4行】
                //Excel.Range range = objExcel.get_Range(objExcel.Cells[4, strTitleNnamecount], objExcel.Cells[4, strTitleNnamecount]);
                //range.Select();
                //if (true)//是否整行删除

                m_objRange.EntireRow.Delete(XlDeleteShiftDirection.xlShiftUp);
                //else
                //    range.Delete(XlDeleteShiftDirection.xlShiftUp);
 

             
              
            }
            catch
            {

            }
        }
        #region 杀死进程
        private void KillProcess(string processName)
        {
            //获得进程对象，以用来操作    
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程     
            try
            {
                //获得需要杀死的进程名    
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    //立即杀死进程    
                    thisproc.Kill();
                }
            }
            catch (Exception Exc)
            {
                throw new Exception("", Exc);
            }
        }
        #endregion   

    }
}
