using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using EMEWEDAL;
using EMEWEEntity;
using System.Data;

namespace EMEWEQUALITY.HelpClass
{
    public class PrintDemo2012_08_23
    {

        /// 是否为日期字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string StrSource)
        {
            bool rbool = true;
            DateTime dt;
            if (DateTime.TryParse(StrSource, out dt))
            {
                rbool = true;
            }
            else
            {
                rbool = false;
            }
            return rbool;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="intQCInfo_ID">质检编号</param>
        public void AddPrintDemoMethod()
        {
            #region 判断是否安装Word
            if (IsInstallWord() == false)
            {
                MessageBox.Show("请先安装Microsoft Office办公软件");
                return;
            }
            #endregion

            //#region 复制模版
            string file = Common.PrintDemoTitleRoute;//模板路径
            if (!Common.rbool)
            {
                file = Common.PrintDemoTitleRouteTemporary;
            }


            //string newWordFile = CopyWord(file);

            //#endregion

            #region 加载模版
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.ApplicationClass WordApp;
            Microsoft.Office.Interop.Word.Document WordDoc;
            WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            object fileName = file;
            WordDoc = WordApp.Documents.Open(ref fileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            #endregion

            FindReplace(WordDoc, "[QCInfo_ID]", Common.intQCInfo_ID.ToString(), 0);
            List<string> sqlString = new List<string>();
            sqlString = GetTableSQL(WordDoc);          //获取表格数据的SQL语句


            #region 填充数据

            #region 标题
            if (Common.rboolBT)
            {
                FindReplace(WordDoc, "[标题]", Common.PrintDemoTitle, 0);            //查找指定字段并进行替换操作
            }
            else
            {
                FindReplace(WordDoc, "[标题]", Common.AbnormalPrintDemoTitle, 0);            //查找指定字段并进行替换操作
            }
            #endregion
            string strsql = "";
            #region 非表格字段的数据替换
            if (sqlString.Count >= 1 && WordDoc.Content.Tables.Count >= 1)
            {
                List<string> otherFields = new List<string>();
                otherFields = GetOtherFields(WordDoc);
                strsql = sqlString[0];//.ElementAt(0); 
                if (!string.IsNullOrEmpty(strsql))
                {
                    //strsql = String.Format("select * from View_QCInfo_InTerface where QCInfo_ID={0}", Common.intQCInfo_ID);
                    DataSet dsView_QCInfo_InTerface = LinQBaseDao.Query(strsql);
                    if (dsView_QCInfo_InTerface != null && dsView_QCInfo_InTerface.Tables[0].Rows.Count > 0)
                    {
                        for (int inti = 0; inti < dsView_QCInfo_InTerface.Tables[0].Rows.Count; inti++)
                        {
                            for (int i = 0; i < otherFields.Count; i++)
                            {
                                string oldtext = otherFields.ElementAt(i);
                                string txt = otherFields.ElementAt(i);
                                if (dsView_QCInfo_InTerface.Tables[0].Rows[inti][txt] != null)
                                {
                                    string newtext = dsView_QCInfo_InTerface.Tables[0].Rows[inti][txt].ToString();
                                    if (newtext.Length < 12)
                                    {
                                        newtext = ISNullMethod(newtext, 12, 0);
                                    }
                                    if (IsDateTime(newtext))
                                    {
                                        newtext = newtext.Substring(0, 10);
                                    }
                                    FindReplace(WordDoc, "[" + oldtext + "]", newtext, 0);            //查找指定字段并进行替换操作
                                }
                            }

                        }
                    }
                    if (strsql != "")
                    {
                        FindReplace(WordDoc, "{" + strsql + "}", "", 0);

                    }
                }
            }

            #endregion

        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
            #region 拆包前水份
        if (sqlString.Count >= 2 && WordDoc.Content.Tables.Count >= 2)
        {
            int intTablesIndex = 2;
            strsql = sqlString[1];//.ElementAt(1);
            if (!string.IsNullOrEmpty(strsql))
            {

                ds = LinQBaseDao.Query(strsql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    int RowNum = ds.Tables[0].Rows.Count;
                    int ColumnNum = WordDoc.Content.Tables[intTablesIndex].Columns.Count;

                    //读模板
                    int r = 0;         //从第几行计算BlockColumn和IDColumn;
                    for (int i = 1; i <= WordDoc.Content.Tables[intTablesIndex].Rows.Count; i++)
                    {

                        for (int j = 1; j <= WordDoc.Content.Tables[intTablesIndex].Rows[i].Cells.Count; j++)
                        {

                            if (WordDoc.Content.Tables[intTablesIndex].Cell(i, j).Range.Text.Substring(0, WordDoc.Content.Tables[intTablesIndex].Cell(i, j).Range.Text.Length - 2) == "")
                            {
                                r = i;
                                break;
                            }
                        }
                        if (r != 0)
                            break;
                    }

                    int IDColumn = 0, BlockColumn = 0;   //一行可以显示BlockColumn个数据
                    for (int i = 1; i <= WordDoc.Content.Tables[intTablesIndex].Columns.Count; i++)
                    {
                        if (WordDoc.Content.Tables[intTablesIndex].Cell(r, i).Range.Text.Substring(0, WordDoc.Content.Tables[intTablesIndex].Cell(r, i).Range.Text.Length - 2) != "")
                            IDColumn++;
                        else
                            BlockColumn++;
                    }

                    if (RowNum % BlockColumn != 0)
                    {
                        RowNum = RowNum / BlockColumn + 1;
                    }
                    else
                    {
                        RowNum = RowNum / BlockColumn;
                    }


                    for (int k = WordDoc.Content.Tables[intTablesIndex].Rows.Count + 1; k <= RowNum; k++)            //添加RowNum-1行
                    {
                        WordDoc.Content.Tables[intTablesIndex].Rows.Add(ref oMissing);
                        for (int c = 1; c <= WordDoc.Content.Tables[intTablesIndex].Columns.Count; c++)            //初始化新添加行
                        {
                            string str = WordDoc.Content.Tables[intTablesIndex].Cell(k - 1, c).Range.Text.Substring(0, WordDoc.Content.Tables[intTablesIndex].Cell(k - 1, c).Range.Text.Length - 2);      //获取表格原来的值
                            if (str == "")
                            {
                                WordDoc.Content.Tables[intTablesIndex].Cell(k, c).Range.Text = str;
                            }
                            else
                            {
                                int x = Convert.ToInt32(str);
                                int y = Convert.ToInt32(IDColumn);
                                WordDoc.Content.Tables[intTablesIndex].Cell(k, c).Range.Text = Convert.ToString(x + y);
                            }
                        }

                    }


                    /*往表里添加数据*/
                    int pRow = 1, qColumn = 1;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int find = 1;       //1表示没有找到填充格
                        string str = WordDoc.Content.Tables[intTablesIndex].Cell(pRow, qColumn).Range.Text.Substring(0, WordDoc.Content.Tables[intTablesIndex].Cell(pRow, qColumn).Range.Text.Length - 2);
                        while (find == 1)
                        {
                            while (str == "")
                            {
                                WordDoc.Content.Tables[intTablesIndex].Cell(pRow, qColumn).Range.Text = ds.Tables[0].Rows[i][0].ToString();
                                find = 0;
                                break;
                            }
                            qColumn++;
                            if (qColumn % WordDoc.Content.Tables[intTablesIndex].Columns.Count == 1)
                            {
                                pRow++;
                                qColumn = 1;
                            }
                            str = WordDoc.Content.Tables[intTablesIndex].Cell(pRow, qColumn).Range.Text.Substring(0, WordDoc.Content.Tables[intTablesIndex].Cell(pRow, qColumn).Range.Text.Length - 2);
                        }
                    }

                }
                if (strsql != "")
                {
                    FindReplace(WordDoc, "{" + strsql + "}", "", 0);

                }
            }
        }
            #endregion

            #region 拆包后水份和废纸包重量
        if (sqlString.Count >= 4 && WordDoc.Content.Tables.Count>=3)
        {
            #region 拆包后水分模板

            int RowNum2 = 0;//行数
            // int ColumnNum2 = WordDoc.Content.Tables[3].Columns.Count;
            //读模板
            int r2 = 0;         //从第几行计算BlockColumn和IDColumn;
            for (int i = 1; i <= WordDoc.Content.Tables[3].Rows.Count; i++)
            {

                for (int j = 1; j <= WordDoc.Content.Tables[3].Rows[i].Cells.Count; j++)
                {
                    if (WordDoc.Content.Tables[3].Cell(i, j).Range.Text.Substring(0, WordDoc.Content.Tables[3].Cell(i, j).Range.Text.Length - 2) == "")
                    {
                        r2 = i;
                        break;
                    }
                }
                if (r2 != 0)
                    break;
            }
            int IDColumn2 = 1, BlockColumn2 = 1;   //一行可以显示BlockColumn2个数据
            for (int i = 1; i <= WordDoc.Content.Tables[3].Columns.Count; i++)
            {
                if (WordDoc.Content.Tables[3].Cell(r2, i).Range.Text.Substring(0, WordDoc.Content.Tables[3].Cell(r2, i).Range.Text.Length - 2) != "")
                    IDColumn2++;
                else
                    BlockColumn2++;
            }
            #endregion
            #region 拆包后水份水分不为空
            //*************根据拆包后水份显示行数*******************************************************
            strsql = sqlString[3];//.ElementAt(2);
            if (!string.IsNullOrEmpty(strsql))
            {
                ds = new DataSet();
                ds = LinQBaseDao.Query(strsql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    RowNum2 = ds.Tables[0].Rows.Count;
                    if (RowNum2 % BlockColumn2 != 0)
                    {
                        RowNum2 = RowNum2 / BlockColumn2 + 1 + 1;       //因为第一行不显示数据，所以要多加一行
                    }
                    else
                    {
                        RowNum2 = RowNum2 / BlockColumn2 + 1;
                    }
                }
                if (strsql != "")
                {
                    FindReplace(WordDoc, "{" + strsql + "}", "", 0);
                }
            }
            //*************根据废纸包数显示行数*******************************************************
            string strsql1 = sqlString[2];//ElementAt(1);
            if (!string.IsNullOrEmpty(strsql1))
            {
          
                ds1 = LinQBaseDao.Query(strsql1);
                int intcount = ds1.Tables[0].Rows.Count;
                if (RowNum2 > 0)//拆包后水份有数据
                {
                    if (ds1 != null && intcount > RowNum2)//废纸包有数据
                    {
                        RowNum2 = ds1.Tables[0].Rows.Count;
                    }
                }
                else//拆包后水份无数据
                {
                    if (ds1 != null && intcount > RowNum2)//废纸包有数据
                    {
                        RowNum2 = ds1.Tables[0].Rows.Count;
                        if (RowNum2 % BlockColumn2 != 0)
                        {
                            RowNum2 = RowNum2 / BlockColumn2 + 1 + 1;       //因为第一行不显示数据，所以要多加一行
                        }
                        else
                        {
                            RowNum2 = RowNum2 / BlockColumn2 + 1;
                        }
                    }
                }
                FindReplace(WordDoc, "{" + strsql1 + "}", "", 0);
            }
            //****************新添加拆包后水份和废纸包重量行数************************************************
            for (int k = WordDoc.Content.Tables[3].Rows.Count + 1; k <= RowNum2; k++)            //添加RowNum2-1行
            {
                WordDoc.Content.Tables[3].Rows.Add(ref oMissing);
                for (int c = 1; c <= WordDoc.Content.Tables[3].Columns.Count; c++)            //初始化新添加行
                {
                    string str = WordDoc.Content.Tables[3].Cell(k - 1, c).Range.Text.Substring(0, WordDoc.Content.Tables[3].Cell(k - 1, c).Range.Text.Length - 2);      //获取表格原来的值
                    if (str == "")
                    {
                        WordDoc.Content.Tables[3].Cell(k, c).Range.Text = str;
                    }
                    else
                    {
                        if (c == 2)
                        {
                            WordDoc.Content.Tables[3].Cell(k, c).Range.Text = str;
                        }
                        else if (c == 1)
                        {
                            WordDoc.Content.Tables[3].Cell(k, c).Range.Text = GetChineseNum(str);
                        }
                        else
                        {
                            int x = Convert.ToInt32(str);
                            int y = Convert.ToInt32(BlockColumn2);
                            WordDoc.Content.Tables[3].Cell(k, c).Range.Text = Convert.ToString(x + y);
                        }
                    }
                }

            }


            /***********************往表里添加数据*******************************/
            int pRow2 = 2, qColumn2 = 1;
            int intindex = 0;
            int ids = 0;//水分值的下标
            int ids2 = 0;//废纸包值的下标
            if (ds.Tables[0].Rows.Count > 0 || ds1.Tables[0].Rows.Count > 0)
            {
                while (pRow2 <= WordDoc.Content.Tables[3].Rows.Count)//如果当前行pRow2小于表格的总行数
                {

                    string str = WordDoc.Content.Tables[3].Cell(pRow2, qColumn2).Range.Text.Substring(0, WordDoc.Content.Tables[3].Cell(pRow2, qColumn2).Range.Text.Length - 2);

                    if (str == "")//如果为空单元格填写水分值 
                    {
                        if (ids < ds.Tables[0].Rows.Count)
                        {
                            if (ds.Tables[0].Rows[ids][0] != null)
                            {
                                WordDoc.Content.Tables[3].Cell(pRow2, qColumn2).Range.Text = ds.Tables[0].Rows[ids][0].ToString();
                                ids++;
                            }
                        }
                    }
                    else
                    {//如果不为空单元格填写废纸包重量
                        if (ids2 < ds1.Tables[0].Rows.Count && str.Contains("扎"))//如果当前行数小于废纸包行数，单元格中包含“扎”字
                        {

                            if (ds1.Tables[0].Rows[ids2][0] != null)
                            {
                                // intindex++;
                                string strbox = "第" + ds1.Tables[0].Rows[ids2]["QCRecord_DRAW"].ToString() + "扎," + ds1.Tables[0].Rows[ids2]["QCRecord_RESULT"].ToString() + "KG".Trim();
                                WordDoc.Content.Tables[3].Cell(pRow2, qColumn2).Range.Text = strbox;
                                ids2++;
                            }
                        }
                    }

                    qColumn2++;
                    if (qColumn2 % WordDoc.Content.Tables[3].Columns.Count == 1)
                    {
                        pRow2++;
                        qColumn2 = 1;

                    }


                }

            }
            #endregion

        
        }
            #endregion

            #region 复测

            //strsql = sqlString.ElementAt(3);
            //ds = new DataSet();
            //ds = LinQBaseDao.Query(strsql);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{

            //    int RowNum = ds.Tables[0].Rows.Count;
            //    int ColumnNum = WordDoc.Content.Tables[6].Columns.Count;

            //    //读模板
            //    int r = 0;         //从第几行计算BlockColumn和IDColumn;
            //    for (int i = 1; i <= WordDoc.Content.Tables[6].Rows.Count; i++)
            //    {

            //        for (int j = 1; j <= WordDoc.Content.Tables[6].Rows[i].Cells.Count; j++)
            //        {

            //            if (WordDoc.Content.Tables[6].Cell(i, j).Range.Text.Substring(0, WordDoc.Content.Tables[6].Cell(i, j).Range.Text.Length - 2) == "")
            //            {
            //                r = i;
            //                break;
            //            }
            //        }
            //        if (r != 0)
            //            break;
            //    }

            //    int IDColumn = 0, BlockColumn = 0;   //一行可以显示BlockColumn个数据
            //    for (int i = 1; i <= WordDoc.Content.Tables[6].Columns.Count; i++)
            //    {
            //        if (WordDoc.Content.Tables[6].Cell(r, i).Range.Text.Substring(0, WordDoc.Content.Tables[6].Cell(r, i).Range.Text.Length - 2) != "")
            //            IDColumn++;
            //        else
            //            BlockColumn++;
            //    }

            //    if (RowNum % BlockColumn != 0)
            //    {
            //        RowNum = RowNum / BlockColumn + 1;
            //    }
            //    else
            //    {
            //        RowNum = RowNum / BlockColumn;
            //    }

            //    for (int k = WordDoc.Content.Tables[6].Rows.Count + 1; k <= RowNum; k++)            //添加RowNum-1行
            //    {
            //        WordDoc.Content.Tables[6].Rows.Add(ref oMissing);
            //        for (int c = 1; c <= WordDoc.Content.Tables[6].Columns.Count; c++)            //初始化新添加行
            //        {
            //            string str = WordDoc.Content.Tables[6].Cell(k - 1, c).Range.Text.Substring(0, WordDoc.Content.Tables[6].Cell(k - 1, c).Range.Text.Length - 2);      //获取表格原来的值
            //            if (str == "")
            //            {
            //                WordDoc.Content.Tables[6].Cell(k, c).Range.Text = str;
            //            }
            //            else
            //            {
            //                int x = Convert.ToInt32(str);
            //                int y = Convert.ToInt32(IDColumn);
            //                WordDoc.Content.Tables[6].Cell(k, c).Range.Text = Convert.ToString(x + y);
            //            }
            //        }

            //    }

            //}
            //if (strsql != "")
            //{
            //    FindReplace(WordDoc, "{" + strsql + "}", "", 0);

            //}
            #endregion
            #endregion

            #region 打印预览
            WordApp.Visible = true;
            WordDoc.PrintPreview();//打印预览
            WordDoc.ReadingLayoutSizeX=1024;
            WordDoc.ReadingLayoutSizeY= 756;
            #endregion


            #region 打印设置
            ////打印设置
            //try
            //{
            //    int dialogResult = WordApp.Dialogs[Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint].Show(ref oMissing);
            //    if (dialogResult == 1)
            //    {
            //        //打印
            //        WordDoc.PrintOut(ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //              ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //              ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //              ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            //    }
            //    while (WordApp.BackgroundPrintingStatus > 0)
            //    {
            //        System.Threading.Thread.Sleep(250);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    //Common.WriteTextLog("打印 GetPrint()" + ex.Message.ToString());
            //}
            //finally
            //{
            //    //消除Word 进程
            //    object SaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;

            //    WordDoc.Close(ref SaveChanges, ref oMissing, ref oMissing);
            //    if (WordApp != null)
            //        System.Runtime.InteropServices.Marshal.ReleaseComObject(WordDoc);
            //    WordDoc = null;


            //    WordApp.Quit(ref SaveChanges, ref oMissing, ref oMissing);
            //    GC.Collect();

            //}

            //object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF;
            //object Target = System.Windows.Forms.Application.StartupPath + "//temp.rtf";
            //// 转换格式
            //oDoc.SaveAs(ref Target, ref format,
            //           ref oMissing, ref oMissing, ref oMissing,
            //           ref oMissing, ref oMissing, ref oMissing,
            //           ref oMissing, ref oMissing, ref oMissing,
            //           ref oMissing, ref oMissing, ref oMissing,
            //           ref oMissing, ref oMissing);
            #endregion

            //    OnClose(WordApp);
            //    MessageBox.Show("报表已生成！");

            //}
            //else
            //{
            //    return;
            //}
        }

        #region 查找个替换方法FindReplace
        /// <summary>
        /// 查找个替换方法FindReplace
        /// </summary>
        /// <param name="oDoc"></param>
        /// <param name="strOldText">查找替换的内容</param>
        /// <param name="strNewText">要替换的内容</param>
        /// <param name="intindex"> 0 - 替换找到的所有项。1 - 不替换找到的任何项。2 - 替换找到的第一项。</param>
        public static void FindReplace(Microsoft.Office.Interop.Word.Document oDoc, string strOldText, string strNewText, int intindex)
        {
            oDoc.Content.Find.Text = strOldText;
            object FindText, ReplaceWith, Replace;// 
            object MissingValue = Type.Missing;
            FindText = strOldText;//要查找的文本
            ReplaceWith = strNewText;//替换文本
            Replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;        /**//*wdReplaceAll - 替换找到的所有项。
                                                                                               * wdReplaceNone - 不替换找到的任何项。
                                                                                               * wdReplaceOne - 替换找到的第一项。
                                                                                               * */
            oDoc.Content.Find.ClearFormatting();//移除Find的搜索文本和段落格式设置
            if (oDoc.Content.Find.Execute(
                ref FindText, ref MissingValue,
                ref MissingValue, ref MissingValue,
                ref MissingValue, ref MissingValue,
                ref MissingValue, ref MissingValue, ref MissingValue,
                ref ReplaceWith, ref Replace,
                ref MissingValue, ref MissingValue,
                ref MissingValue, ref MissingValue))
            {

            }

        }
        #endregion

        #region 判断系统是否装word
        /**/
        /// <summary>
        /// 判断系统是否装word
        /// </summary>
        /// <returns></returns>
        private bool IsInstallWord()
        {
            RegistryKey machineKey = Registry.LocalMachine;
            if (IsInstallWordByVersion("12.0", machineKey))
            {
                return true;
            }
            if (IsInstallWordByVersion("11.0", machineKey))
            {
                return true;
            }
            return false;
        }

        /**/
        /// <summary>
        /// 判断系统是否装某版本的word
        /// </summary>
        /// <param name="strVersion">版本号</param>
        /// <param name="machineKey"></param>
        /// <returns></returns>
        private bool IsInstallWordByVersion(string strVersion, RegistryKey machineKey)
        {
            try
            {
                RegistryKey installKey = machineKey.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Office").OpenSubKey(strVersion).OpenSubKey("Word").OpenSubKey("InstallRoot");
                if (installKey == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 模版文件拷贝
        private string CopyWord(string filename)
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            //模板文件
            string TemplateFile = filename;
            //生成的具有模板样式的新文件
            string aaa = Path.GetDirectoryName(filename);
            string FileName = aaa + @"\" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".rtf";
            //模板文件拷贝到新文件
            File.Copy(TemplateFile, FileName);
            return FileName;
        }

        private void OnClose(Microsoft.Office.Interop.Word.Application app)
        {
            //避免弹出normal.dotm被使用的对话框,自动保存模板   
            app.NormalTemplate.Saved = true;

            //先关闭打开的文档（注意saveChanges选项）   
            Object saveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdSaveChanges;
            Object originalFormat = Type.Missing;
            Object routeDocument = Type.Missing;
            app.Documents.Close(ref saveChanges, ref originalFormat, ref routeDocument);
            object obj = Type.Missing;
            //若已经没有文档存在，则关闭应用程序   
            if (app.Documents.Count == 0)
            {
                app.Quit(ref obj, ref obj, ref obj);
            }

        }
        #endregion

        #region 表格三第一列的数据填充，中文一，二，三，四.......
        private string GetChineseNum(string num)
        {
            string no = "";
            switch (num)
            {
                case "一": no = "二"; break;
                case "二": no = "三"; break;
                case "三": no = "四"; break;
                case "四": no = "五"; break;
                case "五": no = "六"; break;
                case "六": no = "七"; break;
                case "七": no = "八"; break;
                case "八": no = "九"; break;
                case "九": no = "十"; break;
                case "1": no = "2"; break;
                case "2": no = "3"; break;
                case "3": no = "4"; break;
                case "4": no = "5"; break;
                case "5": no = "6"; break;
                case "6": no = "7"; break;
                case "7": no = "8"; break;
                case "8": no = "9"; break;
                case "9": no = "10"; break;


            }
            return no;
        }
        #endregion

        #region 获取表格数据的SQL语句
        private List<string> GetTableSQL(Microsoft.Office.Interop.Word.Document oDoc)
        {
            List<string> sqlStr = new List<string>();
            oDoc.ActiveWindow.Selection.WholeStory();
            oDoc.ActiveWindow.Selection.Copy();
            IDataObject data = Clipboard.GetDataObject();
            string txtFileContent = data.GetData(DataFormats.Text).ToString();
            Regex reg = new Regex(@"\{.*?\}");         //？跟在其他通配符後表示非贪婪模式，非贪婪模式会匹配尽可能小的字符串。
            MatchCollection mc = reg.Matches(txtFileContent);
            foreach (Match match in mc)
            {
                string txt = match.Value;
                txt = txt.Substring(1, txt.Length - 2);
                if (txt != "")
                {
                    sqlStr.Add(txt);
                }
            }

            return sqlStr;


        }

        #endregion

        #region 收集要填充的非表格字段GetOtherFields
        private List<string> GetOtherFields(Microsoft.Office.Interop.Word.Document oDoc)
        {
            List<string> FieldsList = new List<string>();
            oDoc.ActiveWindow.Selection.WholeStory();
            oDoc.ActiveWindow.Selection.Copy();
            IDataObject data = Clipboard.GetDataObject();
            string txtFileContent = data.GetData(DataFormats.Text).ToString();
            Regex reg = new Regex(@"\[.*?\]");         //？跟在其他通配符後表示非贪婪模式，非贪婪模式会匹配尽可能小的字符串。
            MatchCollection mc = reg.Matches(txtFileContent);
            foreach (Match match in mc)
            {
                string txt = match.Value;
                txt = txt.Substring(1, txt.Length - 2);
                if (txt != "")
                {
                    FieldsList.Add(txt);
                }
            }

            return FieldsList;
        }
        #endregion
        /// <summary>
        /// 返回字符串+空格 或返回空格
        /// </summary>
        /// <param name="str"></param>
        /// <param name="intindex">最大字符长度</param>
        ///  <param name="intdx">0:返回字符串+空格 1:返回空格</param>
        /// <returns></returns>
        private string ISNullMethod(string str, int intindex, int intdx)
        {
            string strtext = "";
            string strNull = "";
            if (str == null)
            {
                str = "";
            }
            int inti = intindex - str.Length;
            switch (inti)
            {
                case 0:
                    strtext = str;
                    strNull = "";
                    break;
                case 1:
                    strtext = str + " ";
                    strNull = " ";
                    break;
                case 2:
                    strtext = str + "  ";
                    strNull = "  ";
                    break;
                case 3:
                    strtext = str + "   ";
                    strNull = "   ";
                    break;
                case 4:
                    strtext = str + "    ";
                    strNull = "    ";
                    break;
                case 5:
                    strtext = str + "     ";
                    strNull = "     ";
                    break;
                case 6:
                    strtext = str + "      ";
                    strNull = "      ";
                    break;
                case 7:
                    strtext = str + "       ";
                    strNull = "       ";
                    break;
                case 8:
                    strtext = str + "        ";
                    strNull = "        ";
                    break;

                case 9:
                    strtext = str + "         ";
                    strNull = "         ";
                    break;
                case 10:
                    strtext = str + "          ";
                    strNull = "          ";
                    break;
                case 11:
                    strtext = str + "           ";
                    strNull = "           ";
                    break;

                case 12:
                    strtext = str + "            ";
                    strNull = "            ";
                    break;
                case 13:
                    strtext = str + "             ";
                    strNull = "             ";
                    break;
                case 14:
                    strtext = str + "              ";
                    strNull = "              ";
                    break;
                case 15:
                    strtext = str + "               ";
                    strNull = "               ";
                    break;
                case 16:
                    strtext = str + "                ";
                    strNull = "                ";
                    break;
                case 17:
                    strtext = str + "                 ";
                    strNull = "                 ";
                    break;
                case 18:
                    strtext = str + "                  ";
                    strNull = "                  ";
                    break;
                case 19:
                    strtext = str + "                   ";
                    strNull = "                   ";
                    break;
                default:
                    strtext = str + "                      ";
                    strNull = "                      ";
                    break;

            }
            if (intdx != 0)
            {
                strtext = strNull;

            }

            return strtext;
        }
    }
}
