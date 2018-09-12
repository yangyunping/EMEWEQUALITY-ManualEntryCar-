using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEQUALITY.HelpClass;
using EMEWEEntity;
using System.Linq.Expressions;
using EMEWEDAL;
using System.Data.Linq.SqlClient;
using EMEWEQUALITY;
namespace EMEWEQUALITY
{
    public partial class RandomNumberFrom : Form
    {
        public RandomNumberFrom()
        {
            InitializeComponent();
        }
        public MainFrom mf;//主窗体
        private void button1_Click(object sender, EventArgs e)
        {



           


            
        }

        /// <summary>
        /// 生成抽包随机数
        /// </summary>
        /// <param name="debarNumber">存放数据集合</param>
        /// <param name="debarNumber">需要排除的数字</param>
        /// <param name="count">生成随机数的个数</param>
        /// <param name="maxNumber">最大数</param>
        /// <param name="minNumber">最小数</param>
        private void returnRandomNumbers(List<int> nums, int[] debarNumber, int count, int minNumber, int maxNumber)
        {
            try
            {

                GetRandomNumber(nums, debarNumber, count, minNumber, maxNumber);
                string str = "";
                foreach (int item in nums)
                {
                    str += item + " | ";
                }
                MessageBox.Show(str);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
            

        }
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="debarNumber">需要排除的数字</param>
        /// <param name="count">生成随机数的个数</param>
        /// <param name="maxNumber">最大数</param>
        /// <param name="minNumber">最小数</param>
        private void GetRandomNumber(List<int> nums, int[] debarNumber, int count, int minNumber, int maxNumber)
        {
            bool isHave = false;
            Random r = new Random();

            int num = r.Next(minNumber, maxNumber);

            //循环判断是不是需要排除的数
            for (int i = 0; i < debarNumber.Length; i++)
            {
                if (debarNumber[i] == num)
                {
                    isHave = true;
                    break;
                }
            }
            //循环判断list里面是否重复
            foreach (int  item in nums)
            {
                if (item == num)
                {
                    isHave = true;
                    break;
                }
            }
            if (isHave == false)
            {
                nums.Add(num);
            }
            if (nums.Count == count)
            {
                
                return ;
            }
            else
            {
                GetRandomNumber(nums, debarNumber, count, minNumber, maxNumber);
            }
            
        }
 
    }
}
