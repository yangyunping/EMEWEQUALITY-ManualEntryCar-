using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using EMEWEEntity;
using EMEWEDAL;
using System.Linq.Expressions;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class FormQCWateSet : Form
    {



        public MainFrom mf;
        public FormQCWateSet()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormQCWateSet_Load(object sender, EventArgs e)
        {
            IEnumerable<WaterTestConfigureSet> list = WaterTestConfigureSetDAL.Query();

            foreach (WaterTestConfigureSet item in list)
            {
                bind(item);
            }


        }
        private void bind(WaterTestConfigureSet w)
        {
            List<TextBox> tbxList = new List<TextBox>();//包数和针数的txetBox集合
            foreach (TabPage item in tabControl1.TabPages)
            {
                //通过第几次检测找到对应的TabPage
                if (item.Tag != null && item.Tag.ToString() == w.WaterTestConfigureSet_frequency.ToString())
                {
                    DataTable dt = LinQBaseDao.Query("select  TestItems_NAME from TestItems where  TestItems_ID= " + w.WaterTestConfigureSet_TestItems_TestItems_ID).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (Control gpx in item.Controls)
                        {
                            //检测项目 
                            if (gpx.Text == dt.Rows[0][0].ToString())
                            {
                                //找到groupBox下所有RadioButton
                                List<RadioButton> rbtnList = new List<RadioButton>();
                                foreach (Control rbtn in gpx.Controls)
                                {
                                    if (rbtn is RadioButton)
                                    {
                                        rbtnList.Add((RadioButton)rbtn);
                                    }
                                }


                                RadioButton rbtnProject = null;
                                string rbtnTxt = "";

                                switch (w.WaterTestConfigureSet_Project)
                                {

                                    case 1:
                                        rbtnTxt = "方案一";

                                        break;
                                    case 2:
                                        rbtnTxt = "方案二";
                                        break;
                                    case 3:
                                        rbtnTxt = "方案三";
                                        break;
                                    default:
                                        break;
                                }
                                foreach (RadioButton rb in rbtnList)
                                {
                                    if (rb.Text == rbtnTxt)
                                    {
                                        rbtnProject = rb;
                                    }
                                }

                                rbtnProject.Checked = true;

                                foreach (Control tbx in gpx.Controls)
                                {
                                    if (tbx.Tag != null && tbx.Tag.ToString() == rbtnProject.Name)
                                    {
                                        //先添加到集合 最后再赋值
                                        tbxList.Add((TextBox)tbx);
                                    }
                                }

                            }
                        }
                    }
                }
                if (tbxList.Count > 0)
                {

                    if (tbxList.Count == 1)
                    {

                        tbxList[0].Text = w.WaterTestConfigureSet_NeedleCount.ToString();

                    }
                    else
                    {
                        tbxList[0].Text = w.WaterTestConfigureSet_PackCount.ToString();
                        tbxList[1].Text = w.WaterTestConfigureSet_NeedleCount.ToString();
                    }
                }

            }



        }
        private void setBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                num += addTestSet(gbxTest1);
                num += addTestSet(gbxTest2);
                num += addTestSet(gbxTestB1);
                num += addTestSet(gbxTestB2);
                if (num == 4)
                {
                    MessageBox.Show("操作成功!");
                }
            }
            catch
            {
                MessageBox.Show("操作失败!");
            }

        }

        //TextBox控件集合
        List<TextBox> tbxList;
        private int addTestSet(Control ctr)
        {
            //循环groupbox内控件，找到tag=groupbox名称的RadioButton
            int WaterTestConfigureSet_Project = 0;
            foreach (Control item in ctr.Controls)
            {
                if (item.Tag != null && item.Tag.ToString() == ctr.Name)
                {
                    RadioButton r = (RadioButton)item;

                    
                    //找到选中的RadioButton
                    if (r.Checked == true)
                    {
                        if (r.Text == "方案一")
                        {
                            WaterTestConfigureSet_Project = 1;
                        }
                        if (r.Text == "方案二")
                        {
                            WaterTestConfigureSet_Project = 2;
                        }
                        if (r.Text == "方案三")
                        {
                            WaterTestConfigureSet_Project = 3;
                        }
                        //循环控件，找到找到tag=RadioButton名称的textBox
                        tbxList = new List<TextBox>();
                        foreach (Control txtItem in ctr.Controls)
                        {
                            if (txtItem.Tag != null && txtItem.Tag.ToString() == r.Name)
                            {
                                tbxList.Add((TextBox)txtItem);

                            }
                        }
                    }
                }
            }
            //组合对象存入数据库

            if (ctr.Parent.Parent is TabControl)
            {
                WaterTestConfigureSet w = new WaterTestConfigureSet();
                w.WaterTestConfigureSet_frequency = Convert.ToInt32(((TabPage)ctr.Parent).Tag);//第几次检测

                DataTable dt = LinQBaseDao.Query("select TestItems_ID from TestItems where TestItems_NAME = '" + ctr.Text + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    w.WaterTestConfigureSet_TestItems_TestItems_ID = Convert.ToInt32(dt.Rows[0][0]);//检测项目编号
                }
                else
                {
                    return 0;
                }
                w.WaterTestConfigureSet_Project = WaterTestConfigureSet_Project;//方案号


                if (tbxList.Count == 2)
                {
                    w.WaterTestConfigureSet_PackCount = Convert.ToInt32(tbxList[0].Text == "" ? "0" : tbxList[0].Text);
                    w.WaterTestConfigureSet_NeedleCount = Convert.ToInt32(tbxList[1].Text == "" ? "0" : tbxList[1].Text);
                }
                if (tbxList.Count == 1)
                {
                    w.WaterTestConfigureSet_NeedleCount = Convert.ToInt32(tbxList[0].Text == "" ? "0" : tbxList[0].Text);
                }
                else if (tbxList.Count > 2)
                {
                    return 0;
                }
                Expression<Func<WaterTestConfigureSet, bool>> fn = n => n.WaterTestConfigureSet_frequency == w.WaterTestConfigureSet_frequency && n.WaterTestConfigureSet_TestItems_TestItems_ID == w.WaterTestConfigureSet_TestItems_TestItems_ID;

                if (WaterTestConfigureSetDAL.Single(fn) == null)
                {
                    bool b = WaterTestConfigureSetDAL.InsertOneQCRecord(w);

                }
                else
                {
                    Action<WaterTestConfigureSet> a = n =>
                    {
                        n.WaterTestConfigureSet_frequency = w.WaterTestConfigureSet_frequency;
                        n.WaterTestConfigureSet_TestItems_TestItems_ID = w.WaterTestConfigureSet_TestItems_TestItems_ID;
                        n.WaterTestConfigureSet_Project = w.WaterTestConfigureSet_Project;
                        n.WaterTestConfigureSet_NeedleCount = w.WaterTestConfigureSet_NeedleCount;
                        n.WaterTestConfigureSet_PackCount = w.WaterTestConfigureSet_PackCount;

                    };
                    bool b = WaterTestConfigureSetDAL.Update(fn, a);
                    return 1;
                }

            }
            return 0;
        }


        /// <summary>
        /// 清空其他未选中的文本
        /// </summary>
        /// <param name="ctr"></param>
        private void clearText(Control ctr)
        {
            foreach (Control item in ctr.Controls)
            {
                if (item is TextBox && item.Enabled == false)
                {
                    ((TextBox)item).Text = "";
                }
            }

        }


        private void rbrn1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Acqfa1b.Enabled = true;
                Acqfa1z.Enabled = true;
            }
            else
            {
                Acqfa1b.Enabled = false;
                Acqfa1z.Enabled = false;
            }

            clearText(((RadioButton)sender).Parent);
        }

        private void rbtn2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Acqfa2z.Enabled = true;
                Acqfa2b.Enabled = true;
            }
            else
            {
                Acqfa2b.Enabled = false;
                Acqfa2b.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void rbtn3_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Acqfa3z.Enabled = true;
            }
            else
            {
                Acqfa3z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void Arbtn4_CheckedChanged(object sender, EventArgs e)
        {

            if (((RadioButton)sender).Checked == true)
            {
                Achfa1b.Enabled = true;
                Achfa1z.Enabled = true;

            }
            else
            {
                Achfa1b.Enabled = false;
                Achfa1z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void Arbtn5_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Achfa2b.Enabled = true;
                Achfa2z.Enabled = true;

            }
            else
            {
                Achfa2b.Enabled = false;
                Achfa2z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }




        private void Brbtn1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Bcqfa1b.Enabled = true;
                Bcqfa1z.Enabled = true;
            }
            else
            {
                Bcqfa1b.Enabled = false;
                Bcqfa1z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void Brbtn2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Bcqfa2b.Enabled = true;
                Bcqfa2z.Enabled = true;
            }
            else
            {
                Bcqfa2b.Enabled = false;
                Bcqfa2z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void Brbtn3_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Bcqfa3z.Enabled = true;
            }
            else
            {
                Bcqfa3z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void Brbtn4_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Bchfa1b.Enabled = true;
                Bchfa1z.Enabled = true;
            }
            else
            {
                Bchfa1b.Enabled = false;
                Bchfa1z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void Brbtn5_CheckedChanged(object sender, EventArgs e)
        {

            if (((RadioButton)sender).Checked == true)
            {
                Bchfa2b.Enabled = true;
                Bchfa2z.Enabled = true;
            }
            else
            {
                Bchfa2b.Enabled = false;
                Bchfa2z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void Brbtn6_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Bchfa3z.Enabled = true;
            }
            else
            {
                Bchfa3z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }

        private void Arbtn6_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Achfa3z.Enabled = true;
            }
            else
            {
                Achfa3z.Enabled = false;
            }
            clearText(((RadioButton)sender).Parent);
        }
    }
}
