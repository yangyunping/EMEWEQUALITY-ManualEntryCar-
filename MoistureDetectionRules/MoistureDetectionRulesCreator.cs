using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoistureDetectionRules
{
    public interface MoistureDetectionRule
    {
        void LoadCfg(string cfgPath);
        void SaveCfg(string cfgPath);
       int GetSumWaterCount(string[] testBags, out int testBagWaterCount);      
    }

    public class MoistureDetectionRulesCreator
    {
        private Dictionary<string, string> _dicRules = new Dictionary<string, string>();
        private Dictionary<string, string> _dicSetupForms = new Dictionary<string, string>();
        public MoistureDetectionRulesCreator()
        {
            InitDicRules();
            InitDicSetupForms();
        }

        private void InitDicRules()
        {
            _dicRules.Add("JiangXiPaper", "MoistureDetectionRules.JiangXiMoistureDetectionRule");
            _dicRules.Add("ChongQingPaper", "MoistureDetectionRules.ChongQingMoistureDetectionRule");
        }       

        private void InitDicSetupForms()
        {
            _dicSetupForms.Add("JiangXiPaper", "MoistureDetectionRules.FormJiangxiMoistureRuleSet");
            //_dicRules.Add("重庆理文", "MoistureDetectionRules.ChongQingMoistureDetectionRules");
        }

        #region 获取和保存水份检测规则配置参数
        //public void LoadAllRulesParams(string cfgPath)
        //{
        //    foreach (string company in _dicRules.Keys)
        //        LoadMoistureDetectionRuleParams(company, cfgPath);
        //}
      
        public void LoadMoistureDetectionRuleParams(string company, string cfgPath)
        {
            MoistureDetectionRule rule = GetRule(company);
            if (null == rule)
                throw new Exception("rule is null.");
            rule.LoadCfg(cfgPath);
        }

        public void SaveMoistureDetectionParams(string company, string cfgPath)
        {
            MoistureDetectionRule rule = GetRule(company);
            if (null == rule)
                throw new Exception("rule is null.");
            rule.SaveCfg(cfgPath);
        }
        #endregion

        #region 获取水份检测点数
        public int GetSumWaterCount(string company, string[] testBags, out int testBagWaterCount)
        {
            MoistureDetectionRule rule = GetRule(company);
            if (null == rule)
                throw new Exception("rule is null.");
            return rule.GetSumWaterCount(testBags, out testBagWaterCount);
        }         
        #endregion

        private MoistureDetectionRule GetRule(string company)
        {
            if (_dicRules.Keys.Contains(company))
            {
                string rulesClassName = _dicRules[company];
                return (MoistureDetectionRule)Activator.CreateInstance(Type.GetType(rulesClassName));
            }
            return null;
        }

        #region 显示水份检测规则设置窗口
        public DialogResult ShowSetupForm(string company)
        {
            if (_dicSetupForms.Keys.Contains(company))
            {
                string setupFormsName = _dicSetupForms[company];
                Form form = (Form)Activator.CreateInstance(Type.GetType(setupFormsName));
                return form.ShowDialog();
            }
            return DialogResult.None;
        }
        #endregion

        
    }   
}
