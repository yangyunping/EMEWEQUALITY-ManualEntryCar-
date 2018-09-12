using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml;
using EMEWEQUALITY.QCAdmin;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class PrintTemplateSetUpForm : Form
    {
        /// <summary>
        /// 从OneQcAdmin中传过来
        /// </summary>
        public int iQcInfoID = 0;

        public PrintTemplateSetUpForm()
        {
            InitializeComponent();
        }
        public MainFrom mf;
        /// <summary>
        ///根据名称选中
        /// </summary>
        /// <param name="strName"></param>
        private void NameCheckMethod(string strName)
        {
            for (int i = 0; i < chkl_Name.Items.Count; i++)
            {

                if (strName.Contains(chkl_Name.Items[i].ToString()))
                {
                    chkl_Name.SetItemChecked(i, true);
                }

            }
        }
        private void UserMethod()
        {
            try
            {
                Common.rbool = false;
                string strpath = Application.StartupPath + @"\Template\";
                ArrayList alst = new ArrayList();
                alst.Add(".DOC");
                chkl_Name.DataSource = GetFiles(strpath, alst).ToList();
                NameCheckMethod(Common.PrintDemoTitleRoute);//
            }
            catch (Exception ex)
            {
                
                Common.WriteTextLog("PrintTemplateSetUpForm.UserMethod()" + ex.Message.ToString());
            }
          
        }
        private void PrintTemplateSetUpForm_Load(object sender, EventArgs e)
        {
            try
            {
                UserMethod();
            }
            catch (Exception ex)
            {

                Common.WriteTextLog("PrintTemplateSetUpForm.PrintTemplateSetUpForm_Load()" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 获取文件夹下特定类型的文件
        /// </summary>
        /// <param name="strPath">文件的路径</param>
        /// <param name="lstExtend">包含参数名称的ArrayList</param>
        public List<string> GetFiles(string strPath, ArrayList lstExtend)
        {
            List<string> list = new List<string>();
            try
            {
                //获取文件夹下的所有文件
                DirectoryInfo fdir = new DirectoryInfo(strPath);
                FileInfo[] file = fdir.GetFiles();

                //遍历该文件夹下的所有文件
                foreach (FileInfo f in file)
                {

                    //如果文件的扩展名包含于该ArrayList内
                    if (lstExtend.Contains(f.Extension.ToUpper()) && !f.Name.Contains("$"))
                    {
                        list.Add(f.Name);
                        //Response.Write(f.FullName.ToString() + "<br/>");
                    }

                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("PrintTemplateSetUpForm.GetFiles()" + ex.Message.ToString());
            }
            return list;
        }

        private void btn_SetUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (iQcInfoID > 0)
                {
                    string strchecked = "";
                    for (int i = 0; i < chkl_Name.Items.Count; i++)
                    {
                        if (chkl_Name.GetItemChecked(i))
                        {
                            strchecked = (String.IsNullOrEmpty(strchecked) ? "" : ",") + chkl_Name.GetItemText(chkl_Name.Items[i]);
                        }
                    }
                    if (strchecked != "")
                    {
                        Common.PrintDemoTitleRouteTemporary = Application.StartupPath + @"\Template\" + strchecked;
                    }
              
                    Common.intQCInfo_ID = iQcInfoID;
                    Form1 frm = new Form1();
                    frm.Show();
                    this.Close();
                }
                else {
                    MessageBox.Show("请在一检管理列表中选择一行", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("PrintTemplateSetUpForm.btn_SetUp_Click()" + ex.Message.ToString());
            }

        }

        private void chkl_Name_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                for (int i = 0; i < chkl_Name.Items.Count; i++)
                {
                    if (i != e.Index) // 不是单击的项
                    {

                        //checkedListBox1.SetItemChecked(i,false);    这一句也可以
                        chkl_Name.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked); //设置单选核心代码


                    }

                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("PrintTemplateSetUpForm.chkl_Name_ItemCheck()" + ex.Message.ToString());
            }
        }

        private void btn_SetUpDefaultTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_SetUpDefaultTemplate.Enabled)
                {
                    btn_SetUpDefaultTemplate.Enabled = false;
                }
                Save();
                SystemSet ss = new SystemSet();
                ss.InitComName();
                mf = new MainFrom();
                //ss.tabControlIndex = 2;
                ss.tabControl1.SelectedIndex = 2;
                mf.ShowChildForm(ss);
               
                this.Close();
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("PrintTemplateSetUpForm.btn_SetUpDefaultTemplate_Click()" + ex.Message.ToString());
            }
            finally
            {
                btn_SetUpDefaultTemplate.Enabled = true;
            }

        }
        private void Save()
        {
            try
            {
                
                string PrintDemoTitleRoute = "";
               
                for (int i = 0; i < chkl_Name.Items.Count; i++)
                {
                    if (chkl_Name.GetItemChecked(i))
                    {
                        PrintDemoTitleRoute = (String.IsNullOrEmpty(PrintDemoTitleRoute) ? "" : ",") + chkl_Name.GetItemText(chkl_Name.Items[i]);
                    }
                }
                string filepath = System.IO.Directory.GetCurrentDirectory() + "\\SystemSet.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                XmlNode xn = xmlDoc.SelectSingleNode("param");//查找<bookstore>
                XmlNodeList xnl = xn.ChildNodes;
                if (xnl.Count > 0)
                {
                    foreach (XmlNode xnf in xnl)
                    {
                        XmlElement xe = (XmlElement)xnf;

                      
                        xe.SetAttribute("PrintDemoTitleRoute", PrintDemoTitleRoute);
                       
                    }
                    xmlDoc.Save(filepath);
                    Common.PrintDemoTitleRoute = PrintDemoTitleRoute;
                   
                    MessageBox.Show("系统配置成功！", "操作提示", MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                Common.WriteTextLog("PrintTemplateSetUpForm.Save()" + ex.Message.ToString());
            }
        }

        private void btn_NewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "打印模板文件(*.doc)|*.doc";
                openFileDialog1.ShowDialog();
                string sourcePath = openFileDialog1.FileName.ToString();
                if (sourcePath != string.Empty)
                {
                    string file = sourcePath.Substring(sourcePath.LastIndexOf("\\") + 1);//去掉了路径
                    //string name = file.Substring(0, file.LastIndexOf("."));//去掉了后缀名 
                    //  String sourcePath
                    String targetPath = Application.StartupPath + @"\Template\" + file;
                    bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之 
                    System.IO.File.Copy(sourcePath, targetPath, isrewrite);
                    MessageBox.Show("添加成功！", "操作提示", MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    UserMethod();
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("PrintTemplateSetUpForm.btn_NewTemplate_Click()" + ex.Message.ToString());
                MessageBox.Show("添加失败！", "操作提示", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
            }
        }
    }
}
