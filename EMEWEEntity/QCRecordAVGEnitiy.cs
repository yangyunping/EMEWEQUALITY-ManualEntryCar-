using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMEWEEntity
{
   public class QCRecordAVGEnitiy
    {
        private string testItemName;
       /// <summary>
       /// 项目名称
       /// </summary>
        public string TestItemName
        {
            get { return testItemName; }
            set { testItemName = value; }
        }
       /// <summary>
       /// 车牌号
       /// </summary>
        private string cNTR_NO;
        public string CNTR_NO
        {
            get { return cNTR_NO; }
            set { cNTR_NO = value; }
        }
        private double sum;
       /// <summary>
       /// 水分总值
       /// </summary>
        public double Sum
        {
            get { return sum; }
            set { sum = value; }
        }
        private double sum1;
        /// <summary>
        /// 杂质总重量
        /// </summary>
        public double Sum1
        {
            get { return sum1; }
            set { sum1 = value; }
        }
        private double sum2;
        /// <summary>
        /// 杂纸总重量
        /// </summary>
        public double Sum2
        {
            get { return sum2; }
            set { sum2 = value; }
        }
        private  double avg;
       /// <summary>
       /// 平均值
       /// </summary>
        public double Avg
        {
            get { return avg; }
            set { avg = value; }
        }
        private double avg1;
        /// <summary>
        /// 平均值
        /// </summary>
        public double Avg1
        {
            get { return avg1; }
            set { avg1 = value; }
        }
        private double avg2;
        /// <summary>
        /// 平均值
        /// </summary>
        public double Avg2
        {
            get { return avg2; }
            set { avg2 = value; }
        }
        private int count;
       /// <summary>
       /// 总条数
       /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private int beginTime;
        /// <summary>
        /// 登记时间
        /// </summary>
        public int BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }
        private int endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public int EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

    }
}
