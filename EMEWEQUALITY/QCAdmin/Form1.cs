using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;
using EMEWEDAL;
using System.Drawing.Printing;
using System.Reflection;

namespace EMEWEQUALITY
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string qcinf)
        {

            Common.intQCInfo_ID = int.Parse(qcinf);
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string file = Common.PrintDemoTitleRoute;//ģ��·��
            if (!Common.rbool)
            {
                file = Common.PrintDemoTitleRouteTemporary;
            }
            OpenWord(winWordControl1, file);

        }
        private void btnOpenWord_Click(object sender, EventArgs e)
        {
            OpenWord(winWordControl1, txtFilePath.Text.Trim());
        }

        private void btnSaveWord_Click(object sender, EventArgs e)
        {
            string path = SaveWord(winWordControl1, "");
        }

        //��word
        public void OpenWord(WinWordControl.WinWordControl winWordControl1, string wordUrl)
        {
            if (string.IsNullOrEmpty(wordUrl))
            {
                wordUrl = GetPath() + @"\Template\�������ձ���-��׼ģ��.doc";
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

        /// <summary>
        /// ����word
        /// </summary>
        /// <param name="winWordControl1">���ؼ�</param>
        /// <param name="pFileName">�ĵ�����</param>     
        /// <returns></returns>
        public string SaveWord(WinWordControl.WinWordControl winWordControl1, string pFileName)
        {
            if (string.IsNullOrEmpty(pFileName))
            {
                pFileName = DateTime.Now.ToString("yyMMddHHmmss");
            }
            string path = @"\SystemWord\" + DateTime.Now.ToShortDateString() + "\\";
            string savePath = path + pFileName + ".doc";

            string dic = GetPath() + path;
            if (!System.IO.Directory.Exists(dic))
            {
                System.IO.Directory.CreateDirectory(dic);
            }

            string wordUrl = dic + pFileName + ".doc";

            object myNothing = System.Reflection.Missing.Value;

            object myFileName = wordUrl;

            object myWordFormatDocument = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;

            object myLockd = false;

            object myPassword = "";

            object myAddto = true;

            try
            {
                ////winWordControl1.document.SaveAs(ref myFileName, ref myWordFormatDocument, ref myLockd, ref myPassword, ref myAddto, ref myPassword,
                ////        ref myLockd, ref myLockd, ref myLockd, ref myNothing, ref myNothing);
                /*
                this.winWordControl1.document.SaveAs(ref myFileName, ref myWordFormatDocument, ref myLockd, ref myPassword, ref myAddto, ref myPassword,
                ref myLockd, ref myLockd, ref myLockd, ref myLockd, ref myNothing, ref myNothing, ref myNothing,
                ref myNothing, ref myNothing, ref myNothing);
                */
            }
            catch
            {
                throw new Exception("����word�ĵ�ʧ��!");
            }
            return savePath;
        }

        public string GetPath()
        {
            return Application.StartupPath;
        }

        #region ��䷽��

        public void WordComent(Microsoft.Office.Interop.Word.Document WordDoc)
        {

            #region  ����
            #endregion
            object oMissing = System.Reflection.Missing.Value;
            FindReplace(WordDoc, "[QCInfo_ID]", Common.intQCInfo_ID.ToString(), 0);
            List<string> sqlString = new List<string>();
            sqlString = GetTableSQL(WordDoc);          //��ȡ������ݵ�SQL���
            string[] bags = null;
            string qcinfoid = "0";//�ʼ���
            int moistcount = 0;
            #region �������

            #region ����
            if (Common.rboolBT)
            {
                FindReplace(WordDoc, "[����]", Common.PrintDemoTitle, 0);            //����ָ���ֶβ������滻����
            }
            else
            {
                FindReplace(WordDoc, "[����]", Common.AbnormalPrintDemoTitle, 0);    //����ָ���ֶβ������滻����
            }
            #endregion
            string strsql = "";
            string bNum = "";
            #region �Ǳ���ֶε������滻
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
                        moistcount = Convert.ToInt32(dsView_QCInfo_InTerface.Tables[0].Rows[0]["QCInfo_MOIST_Count"].ToString());
                        //��ֵ����\�ʼ���
                        bags = dsView_QCInfo_InTerface.Tables[0].Rows[0]["QCInfo_DRAW"].ToString().Split(',');
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

                                    FindReplace(WordDoc, "[" + oldtext + "]", newtext, 0);//����ָ���ֶβ������滻����
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
            #region �����ˮ�ݺͷ�ֽ������
            if (sqlString.Count >= 2 && WordDoc.Content.Tables.Count >= 3)
            {
                strsql = sqlString[1];//.ElementAt(2);
                if (!string.IsNullOrEmpty(strsql))
                {
                    ds = new DataSet();
                    ds = LinQBaseDao.Query(strsql);
                    if (ds != null)
                    {
                        int num = 1;//��¼��ӡˮ�ֵ㳬��4������һ��
                        int currentRow = 2;//�ӵڶ��п�ʼ
                        int currentColumn = 3;//ÿ�дӵ�3�п�ʼ
                        int beiShu = 0;//��Ҫ�����е�����*������beiShu��
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

                        //��Ϊ�����ϱ�����һ�п���������� ���ٴ���һ��
                        for (int i = 0; i < (beiShu * bags.Length) - 1; i++)
                        {
                            WordDoc.Content.Tables[2].Rows.Add(ref oMissing);
                        }
                        WordDoc.Content.Tables[2].Cell(1, 2).Range.Text = "�ʼ�����" + bNum;
                        for (int i = 0; i < bags.Length; i++)
                        {
                            WordDoc.Content.Tables[2].Cell(currentRow, 1).Range.Text = (i + 1).ToString();
                            double weight = 0;
                            //��ȡ����
                            DataTable dt = LinQBaseDao.Query("select QCRecord_RESULT from QCRecord where QCRecord_QCInfo_ID=" + qcinfoid + " and QCRecord_DRAW=" + bags[i] + "and QCRecord_Dictionary_ID=(select Dictionary_ID from Dictionary where Dictionary_Value='����') and QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='��ֽ����')").Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                                {
                                    weight = Convert.ToDouble(dt.Rows[0][0]);
                                }
                            }

                            DataTable dtAVG = LinQBaseDao.Query("select avg(QCRecord_RESULT) from QCRecord where QCRecord_QCInfo_ID=" + qcinfoid + " and QCRecord_DRAW=" + bags[i] + " and QCRecord_Dictionary_ID=(select Dictionary_ID from Dictionary where Dictionary_Value='����') and QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='ˮ�ּ��')").Tables[0];

                            string stravg = "";
                            if (dtAVG.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(dtAVG.Rows[0][0].ToString()))
                                {
                                    stravg = Convert.ToDouble(dtAVG.Rows[0][0].ToString()).ToString();
                                }
                            }
                            WordDoc.Content.Tables[2].Cell(currentRow, 2).Range.Text = "��" + bags[i] + "����" + weight + "KG ƽ��ˮ��" + stravg;

                            int currentCount = 0;//��ǰ����
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                int count = ds.Tables[0].Rows.Count / bags.Length;//ÿ������
                                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                {
                                    if (ds.Tables[0].Rows[j]["QCRecord_DRAW"].ToString() == bags[i])
                                    {
                                        //���
                                        WordDoc.Content.Tables[2].Cell(currentRow, currentColumn).Range.Text = num.ToString();
                                        //ˮ��ֵ
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

                                    //���
                                    WordDoc.Content.Tables[2].Cell(currentRow, currentColumn).Range.Text = num.ToString();
                                    //ˮ��ֵ
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
                //�ϲ���Ԫ�� Ŀǰֻ�ܺϲ�2��
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
                        catch (Exception exs)
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

        #endregion


            #region ��ӡԤ��
            WinWordControl.WinWordControl.wd.Visible = true;
            //WordDoc.ReadingLayoutSizeX = 1024;
            //WordDoc.ReadingLayoutSizeY = 650;
            WordDoc.PrintPreview();//��ӡԤ��
            winWordControl1.Enabled = true;
            //WordDoc.PrintOut(ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //              ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //              ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //              ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            //this.Close();
            //WordDoc.ReadingLayoutSizeX = 1024;
            //btnPrint_Click(new object(), new EventArgs());
            #endregion
        }

        /// �Ƿ�Ϊ�����ַ���
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
        /// �����·�С��10Ϊtrue�����ڵ���10Ϊfalse
        /// </summary>
        /// <param name="StrSource"></param>
        /// <returns></returns>
        public static bool IsDateMonth(string StrSource)
        {
            bool rbool = true;
            DateTime dt;
            int i = 1;
            if (DateTime.TryParse(StrSource, out dt))
            {
                i = dt.Month;
                if (i < 10)
                {
                    rbool = true;
                }
                else
                {
                    rbool = false;
                }
            }
            else
            {
                rbool = false;
            }
            return rbool;
        }

        #region ���Ҹ��滻����FindReplace
        /// <summary>
        /// ���Ҹ��滻����FindReplace
        /// </summary>
        /// <param name="oDoc"></param>
        /// <param name="strOldText">�����滻������</param>
        /// <param name="strNewText">Ҫ�滻������</param>
        /// <param name="intindex"> 0 - �滻�ҵ��������1 - ���滻�ҵ����κ��2 - �滻�ҵ��ĵ�һ�</param>
        public static void FindReplace(Microsoft.Office.Interop.Word.Document oDoc, string strOldText, string strNewText, int intindex)
        {
            oDoc.Content.Find.Text = strOldText;
            object FindText, ReplaceWith, Replace;// 
            object MissingValue = Type.Missing;
            FindText = strOldText;//Ҫ���ҵ��ı�
            ReplaceWith = strNewText;//�滻�ı�
            Replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;        /**//*wdReplaceAll - �滻�ҵ��������
                                                                                               * wdReplaceNone - ���滻�ҵ����κ��
                                                                                               * wdReplaceOne - �滻�ҵ��ĵ�һ�
                                                                                               * */
            oDoc.Content.Find.ClearFormatting();//�Ƴ�Find�������ı��Ͷ����ʽ����
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

        #region �ж�ϵͳ�Ƿ�װword
        /**/
        /// <summary>
        /// �ж�ϵͳ�Ƿ�װword
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
        /// �ж�ϵͳ�Ƿ�װĳ�汾��word
        /// </summary>
        /// <param name="strVersion">�汾��</param>
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

        #region ģ���ļ�����
        private string CopyWord(string filename)
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            //ģ���ļ�
            string TemplateFile = filename;
            //���ɵľ���ģ����ʽ�����ļ�
            string aaa = Path.GetDirectoryName(filename);
            string FileName = aaa + @"\" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".rtf";
            //ģ���ļ����������ļ�
            File.Copy(TemplateFile, FileName);
            return FileName;
        }

        private void OnClose(Microsoft.Office.Interop.Word.Application app)
        {
            //���ⵯ��normal.dotm��ʹ�õĶԻ���,�Զ�����ģ��   
            app.NormalTemplate.Saved = true;

            //�ȹرմ򿪵��ĵ���ע��saveChangesѡ�   
            Object saveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdSaveChanges;
            Object originalFormat = Type.Missing;
            Object routeDocument = Type.Missing;
            app.Documents.Close(ref saveChanges, ref originalFormat, ref routeDocument);
            object obj = Type.Missing;
            //���Ѿ�û���ĵ����ڣ���ر�Ӧ�ó���   
            if (app.Documents.Count == 0)
            {
                app.Quit(ref obj, ref obj, ref obj);
            }

        }
        #endregion

        #region �������һ�е�������䣬����һ������������.......
        private string GetChineseNum(string num)
        {
            string no = "";
            switch (num)
            {
                case "һ": no = "��"; break;
                case "��": no = "��"; break;
                case "��": no = "��"; break;
                case "��": no = "��"; break;
                case "��": no = "��"; break;
                case "��": no = "��"; break;
                case "��": no = "��"; break;
                case "��": no = "��"; break;
                case "��": no = "ʮ"; break;
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

        #region ��ȡ������ݵ�SQL���
        private List<string> GetTableSQL(Microsoft.Office.Interop.Word.Document oDoc)
        {
            List<string> sqlStr = new List<string>();
            oDoc.ActiveWindow.Selection.WholeStory();
            oDoc.ActiveWindow.Selection.Copy();
            IDataObject data = Clipboard.GetDataObject();
            string txtFileContent = data.GetData(DataFormats.Text).ToString();
            Regex reg = new Regex(@"\{.*?\}");         //����������ͨ������ʾ��̰��ģʽ����̰��ģʽ��ƥ�価����С���ַ�����
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

        #region �ռ�Ҫ���ķǱ���ֶ�GetOtherFields
        private List<string> GetOtherFields(Microsoft.Office.Interop.Word.Document oDoc)
        {
            List<string> FieldsList = new List<string>();
            oDoc.ActiveWindow.Selection.WholeStory();
            oDoc.ActiveWindow.Selection.Copy();
            IDataObject data = Clipboard.GetDataObject();
            string txtFileContent = data.GetData(DataFormats.Text).ToString();
            Regex reg = new Regex(@"\[.*?\]");         //����������ͨ������ʾ��̰��ģʽ����̰��ģʽ��ƥ�価����С���ַ�����
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
        /// �����ַ���+�ո� �򷵻ؿո�
        /// </summary>
        /// <param name="str"></param>
        /// <param name="intindex">����ַ�����</param>
        ///  <param name="intdx">0:�����ַ���+�ո� 1:���ؿո�</param>
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


        private void btnPrint_Click(object sender, EventArgs e)
        {
            #region ��ӡ����
            object oMissing = null;
            //��ӡ����
            try
            {
                int dialogResult = WinWordControl.WinWordControl.wd.Dialogs[Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint].Show(ref oMissing);
                if (dialogResult == 1)
                {
                    //��ӡ
                    winWordControl1.document.PrintOut(ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                          ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                          ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                          ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                }
                while (WinWordControl.WinWordControl.wd.BackgroundPrintingStatus > 0)
                {
                    //System.Threading.Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("��ӡ GetPrint()" + ex.Message.ToString());
            }
            finally
            {

                winWordControl1.CloseControl();
            }
            #endregion
            this.Close();
        }




    }
}