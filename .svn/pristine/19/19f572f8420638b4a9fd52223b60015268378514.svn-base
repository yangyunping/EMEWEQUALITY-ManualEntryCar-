using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoistureDetectionRules
{
    public partial class FormJiangxiMoistureRuleSet : Form
    {
        public FormJiangxiMoistureRuleSet()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            txtMoistCount.Text = JiangXiMoistureDetectionRule.ItemMoistCount.ToString();
            cmbitem.Text = JiangXiMoistureDetectionRule.ItemMoist;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckInputValue())
            {
                JiangXiMoistureDetectionRule.ItemMoist = cmbitem.Text;
                JiangXiMoistureDetectionRule.ItemMoistCount = int.Parse(txtMoistCount.Text);
                this.Close();
            }
        }

        private bool CheckInputValue()
        {
            try
            {
                int moistCount = int.Parse(txtMoistCount.Text);
                if (moistCount <= 0)
                    throw new Exception();
                return true;
            }
            catch
            {
                MessageBox.Show("次数输入不合理。");
                return false;
            }
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {

        }    
    }
}
