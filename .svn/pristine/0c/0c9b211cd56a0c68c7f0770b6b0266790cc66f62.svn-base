using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;
using EMEWEDAL;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;

namespace EMEWEQUALITY
{
    public class WordPringOut
    {
        WinWordControl.WinWordControl winWordControl1;// = new WinWordControl.WinWordControl();
        public WordPringOut(WinWordControl.WinWordControl winWordControl)
        {
            Login();
        }
        public WordPringOut(string qcinf, WinWordControl.WinWordControl winWordControl)
        {
            Common.intQCInfo_ID = int.Parse(qcinf);
            winWordControl1 = winWordControl;
            Login();
        }
        public void Login()
        {
            string file = Common.PrintDemoTitleRoute;//模板路径
            if (!Common.rbool)
            {
                file = Common.PrintDemoTitleRouteTemporary;
            }
            OpenWord(winWordControl1, file);
        }
        //打开word
        public void OpenWord(WinWordControl.WinWordControl winWordControl1, string wordUrl)
        {
            if (string.IsNullOrEmpty(wordUrl))
            {
                wordUrl = GetPath() + @"\Template\国废验收报告-标准模版.doc";
            }
            //else
            //{
            // wordUrl = GetPath() + wordUrl;
            //}

            try
            {
                winWordControl1.CloseControl();

            }
            catch { }
            finally
            {
                winWordControl1.document = null;
                WinWordControl.WinWordControl.wd = null;
                WinWordControl.WinWordControl.wordWnd = 0;
            }
            try
            {
                winWordControl1.LoadDocument(wordUrl);
                WordComent(winWordControl1.document);

            }
            catch (Exception ex)
            {
                String err = ex.Message;
            }
        }
        public string GetPath()
        {
            return Application.StartupPath;
        }
        #region 填充方法
        #endregion
        public void WordComent(Microsoft.Office.Interop.Word.Document WordDoc)
        {

            #region  测试
            #endregion
            object oMissing = System.Reflection.Missing.Value;
            FindReplace(WordDoc, "[QCInfo_ID]", Common.intQCInfo_ID.ToString(), 0);
            List<string> sqlString = new List<string>();
            sqlString = GetTableSQL(WordDoc);          //获取表格数据的SQL语句
            string[] bags = null;
            string qcinfoid = "0";//质检编号
            int moistcount = 0;
            #region 填充数据

            #region 标题
            if (Common.rboolBT)
            {
                FindReplace(WordDoc, "[标题]", Common.PrintDemoTitle, 0);            //查找指定字段并进行替换操作
            }
            else
            {
                FindReplace(WordDoc, "[标题]", Common.AbnormalPrintDemoTitle, 0);    //查找指定字段并进行替换操作
            }
            #endregion
            string strsql = "";
            string bNum = "";
            #region 非表格字段的数据替换
            if (sqlString.Count >= 1 && WordDoc.Content.Tables.Count >= 1)
            {
                List<string> otherFields = new List<string>();
                otherFields = GetOtherFields(WordDoc);
                strsql = sqlString[0];//.ElementAt(0); 
                if (!string.IsNullOrEmpty(strsql))
                {
                    //strsql = String.Format("select * from View_QCInfo_InTerface where QCInfo_ID={0}", Common.intQCInfo_ID);
                    DataSet dsView_QCInfo_InTerface = EMEWEDAL.LinQBaseDao.Query(strsql);
                    if (dsView_QCInfo_InTerface != null && dsView_QCInfo_InTerface.Tables[0].Rows.Count > 0)
                    {
                        try
                        {
                            moistcount = Convert.ToInt32(dsView_QCInfo_InTerface.Tables[0].Rows[0]["QCInfo_MOIST_Count"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                        //赋值包号\质检编号
                        try
                        {
                            bags = dsView_QCInfo_InTerface.Tables[0].Rows[0]["QCInfo_DRAW"].ToString().Split(',');
                        }
                        catch (Exception)
                        {
                        }
                        bNum = dsView_QCInfo_InTerface.Tables[0].Rows[0]["QCInfo_DRAW"].ToString();
                        qcinfoid = dsView_QCInfo_InTerface.Tables[0].Rows[0]["QCInfo_ID"].ToString();
                        for (int inti = 0; inti < dsView_QCInfo_InTerface.Tables[0].Rows.Count; inti++)
                        {
                            for (int i = 0; i < otherFields.Count; i++)
                            {
                                string oldtext = otherFields[i];
                                string txt = otherFields[i];
                                if (dsView_QCInfo_InTerface.Tables[0].Rows[inti][txt] != null)
                                {
                                    string newtext = dsView_QCInfo_InTerface.Tables[0].Rows[inti][txt].ToString();
                                    if (newtext.Length < 12)
                                    {
                                        newtext = ISNullMethod(newtext, 12, 0);
                                    }
                                    else
                                    {
                                        if (IsDateTime(newtext))
                                        {
                                            if (oldtext == "QCInfo_TIMETWO" || oldtext == "QCIInfo_EndTime")
                                            {
                                                newtext = newtext.Substring(newtext.IndexOf("-") + 1, newtext.Length - newtext.IndexOf("-") - 4);
                                                //if (!string.IsNullOrEmpty(newtext))
                                                //{
                                                //    newtext = Convert.ToDateTime(newtext).ToString("yyyyMMdd HHmm");
                                                //}
                                            }
                                            else
                                            {

                                                newtext = Convert.ToDateTime(newtext).ToString("yyyy-MM-dd");

                                            }
                                        }
                                    }

                                    FindReplace(WordDoc, "[" + oldtext + "]", newtext, 0);//查找指定字段并进行替换操作
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
            #region 拆包后水份和废纸包重量
            if (sqlString.Count >= 2 && WordDoc.Content.Tables.Count >= 3)
            {
                strsql = sqlString[1];//.ElementAt(2);
                if (!string.IsNullOrEmpty(strsql))
                {
                    ds = new DataSet();
                    ds = LinQBaseDao.Query(strsql);
                    if (ds != null)
                    {
                        int num = 1;//记录打印水分点超过4点增加一行
                        int currentRow = 2;//从第二行开始
                        int currentColumn = 3;//每行从第3列开始
                        int beiShu = 0;//需要创建行的数量*倍数（beiShu）
                        double zhen = Convert.ToDouble((moistcount / bags.Length)) / 4;
                        for (int i = 1; i < 100; i++)
                        {
                            if (zhen > Convert.ToDouble(i))
                            {
                                beiShu = i + 1;
                            }
                            else if (zhen == Convert.ToDouble(i))
                            {
                                beiShu = i;
                            }
                        }

                        //因为播报上本来有一行可以填充数据 就少创建一行
                        for (int i = 0; i < (beiShu * bags.Length) - 1; i++)
                        {
                            WordDoc.Content.Tables[2].Rows.Add(ref oMissing);
                        }
                        WordDoc.Content.Tables[2].Cell(1, 2).Range.Text = "质检抽包：" + bNum;
                        for (int i = 0; i < bags.Length; i++)
                        {
                            WordDoc.Content.Tables[2].Cell(currentRow, 1).Range.Text = (i + 1).ToString();
                            double weight = 0;
                            //获取包重
                            DataTable dt = LinQBaseDao.Query("select QCRecord_RESULT from QCRecord where QCRecord_QCInfo_ID=" + qcinfoid + " and QCRecord_DRAW=" + bags[i] + "and QCRecord_Dictionary_ID=(select Dictionary_ID from Dictionary where Dictionary_Value='启动') and QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='废纸包重')").Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                                {
                                    weight = Convert.ToDouble(dt.Rows[0][0]);
                                }
                            }
                            DataTable dtAVG = LinQBaseDao.Query("select avg(QCRecord_RESULT) from QCRecord where QCRecord_QCInfo_ID=" + qcinfoid + " and QCRecord_DRAW=" + bags[i] + " and QCRecord_Dictionary_ID=(select Dictionary_ID from Dictionary where Dictionary_Value='启动') and QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='水分检测')").Tables[0];
                            string stravg = "";
                            if (dtAVG.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(dtAVG.Rows[0][0].ToString()))
                                {
                                    stravg = Convert.ToDouble(dtAVG.Rows[0][0].ToString()).ToString();
                                }
                            }
                            WordDoc.Content.Tables[2].Cell(currentRow, 2).Range.Text = "第" + bags[i] + "扎，" + weight + "KG 平均水分" + stravg;
                            int currentCount = 0;//当前针数
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                int count = ds.Tables[0].Rows.Count / bags.Length;//每包针数
                                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                {
                                    if (ds.Tables[0].Rows[j]["QCRecord_DRAW"].ToString() == bags[i])
                                    {
                                        //序号
                                        WordDoc.Content.Tables[2].Cell(currentRow, currentColumn).Range.Text = num.ToString();
                                        //水分值
                                        WordDoc.Content.Tables[2].Cell(currentRow, currentColumn + 1).Range.Text = ds.Tables[0].Rows[j]["QCRecord_RESULT"].ToString();
                                        currentCount++;
                                        currentColumn += 2;
                                        num++;
                                        if (currentColumn == 11 && currentCount != count)
                                        {
                                            currentColumn = 3;
                                            currentRow++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int count = 0;
                                if (bags.Length <= 5)
                                {
                                    count = 8;
                                }
                                else
                                {
                                    count = 4;
                                }
                                for (int s = 0; s < count; s++)
                                {
                                    //序号
                                    WordDoc.Content.Tables[2].Cell(currentRow, currentColumn).Range.Text = num.ToString();
                                    //水分值
                                    WordDoc.Content.Tables[2].Cell(currentRow, currentColumn + 1).Range.Text = "";

                                    currentCount++;
                                    currentColumn += 2;
                                    num++;
                                    if (currentColumn == 11 && currentCount != count)
                                    {
                                        currentColumn = 3;
                                        currentRow++;
                                    }
                                }

                            }
                            currentColumn = 3;
                            currentRow++;
                        }
                    }
                }
                //合并单元格 目前只能合并2行
                for (int i = 0; i < WordDoc.Content.Tables[2].Rows.Count; i++)
                {


                    string str = WordDoc.Content.Tables[2].Cell(i + 1, 1).Range.Text;
                    if (str == "\r\a")
                    {
                        try
                        {
                            WordDoc.Content.Tables[2].Cell(i + 1, 1).Merge(WordDoc.Content.Tables[2].Cell(i, 1));
                            WordDoc.Content.Tables[2].Cell(i + 1, 2).Merge(WordDoc.Content.Tables[2].Cell(i, 2));
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        continue;
                    }


                }



                if (strsql != "")
                {
                    FindReplace(WordDoc, "{" + strsql + "}", "", 0);
                }

            #endregion
            }
            #endregion
            #region 打印
            WordDoc.PrintOut(ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                          ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                          ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                          ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            Thread.Sleep(1000);//等待打印时间
            #endregion
        }

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
                int a = 1;
            }

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
