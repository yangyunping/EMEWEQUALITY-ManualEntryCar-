using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using EMEWE.Disk.Xml;

namespace MoistureDetectionRules
{
    public class JiangXiMoistureDetectionRule : MoistureDetectionRule
    {
        /// <summary>
        /// 检测规则，0每包 1总计
        /// </summary>
        public static string ItemMoist = "每包";

        /// <summary>
        /// 水分单包或总计检测次数
        /// </summary>
        public static int ItemMoistCount = 8;

        public static int SumWaterCount = 24;

        /// <summary>
        /// 加载初始参数
        /// </summary>
        public void LoadCfg(string cfgPath)
        {
            try
            {
                string itemMoist = XmlOpr.Read(cfgPath, "/cfg/rule[@company='江西理文']/ItemMoist", "");
                string itemMoistCount = XmlOpr.Read(cfgPath, "/cfg/rule[@company='江西理文']/ItemMoistCount", "");
                JiangXiMoistureDetectionRule.ItemMoist = itemMoist;
                JiangXiMoistureDetectionRule.ItemMoistCount = int.Parse(itemMoistCount);
            }
            catch
            {
            }
        }

        public void SaveCfg(string cfgPath)
        {
            XmlOpr.Update(cfgPath, "/cfg/rule[@company='江西理文']/ItemMoist", "", JiangXiMoistureDetectionRule.ItemMoist);
            XmlOpr.Update(cfgPath, "/cfg/rule[@company='江西理文']/ItemMoistCount", "", JiangXiMoistureDetectionRule.ItemMoistCount.ToString());
        }

        public int GetSumWaterCount(string[] testBags, out int testBagWaterCount)
        {
            Debug.Assert(null != testBags);
            Debug.Assert(testBags.Count() > 0);

            if (ItemMoist == "每包")
            {
                testBagWaterCount = ItemMoistCount;
                SumWaterCount = testBags.Count() * testBagWaterCount;
            }
            else
            {
                testBagWaterCount = 0;
                SumWaterCount = ItemMoistCount;
            }
            return SumWaterCount;
        }
    }
}
