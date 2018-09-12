using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoistureDetectionRules
{
    public class ChongQingMoistureDetectionRule : MoistureDetectionRule
    {
        public void LoadCfg(string cfgPath)
        {
            //Console.WriteLine("load cfg for ChongQingMoistureDetectionRule.");
        }

        public void SaveCfg(string cfgPath)
        {
        }

        public  int GetSumWaterCount(string[] testBags, out int testBagWaterCount)
        {
            testBagWaterCount = testBags.Count() <= 5 ? 8 : 4;
            return testBags.Count() * testBagWaterCount;
        }
    }

}
