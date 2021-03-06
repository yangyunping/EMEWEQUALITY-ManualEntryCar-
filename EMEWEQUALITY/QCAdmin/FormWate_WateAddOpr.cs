﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MoistureDetectionRules;

namespace EMEWEQUALITY.QCAdmin
{
    public interface IWateAddOpr
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns> 返回wateRowCount</returns>
        int AddDgvWateOneAndQCRecord(FormWate form);
    }

    public class WateAddProxy : IWateAddOpr
    {
        private IWateAddOpr _wateAddOpr = null;
        private Dictionary<string, string> _dicWateAddOpr = new Dictionary<string, string>();
        public WateAddProxy(string company)
        {
            Debug.Assert(!string.IsNullOrEmpty(company));

            Init();
            _wateAddOpr = GetWateAddOpr(company);
        }

        private void Init()
        {
            _dicWateAddOpr.Add("JiangXiPaper", "EMEWEQUALITY.QCAdmin.JiangxiWateAddOpr");
            _dicWateAddOpr.Add("ChongQingPaper", "EMEWEQUALITY.QCAdmin.ChongqingWateAddOpr");
        }

        private IWateAddOpr GetWateAddOpr(string company)
        {
            if (_dicWateAddOpr.Keys.Contains(company))
            {
                string oprName = _dicWateAddOpr[company];
                return (IWateAddOpr)Activator.CreateInstance(Type.GetType(oprName));
            }
            return null;
        }

        public int AddDgvWateOneAndQCRecord(FormWate form)
        {
            Debug.Assert(null != _wateAddOpr);
            Debug.Assert(null != form);

            return _wateAddOpr.AddDgvWateOneAndQCRecord(form);
        }
    }

    public class JiangxiWateAddOpr : IWateAddOpr
    {
        public int AddDgvWateOneAndQCRecord(FormWate form)
        {
            Debug.Assert(null != form);

            int wateRowCount = 0;
            if (JiangXiMoistureDetectionRule.ItemMoist == "每包")
            {
                for (int i = 0; i < Common.testBags.Count(); i++)
                {
                    for (int j = 0; j < Common.testBagWaterCount; j++)
                    {
                        form.AdddgvWateOneAndQCRecord(wateRowCount, wateRowCount + 1, "水分检测", Common.testBags[i]);
                        wateRowCount++;
                    }
                }
            }
            else
            {
                int bct = Common.testBags.Count();
                if (Common.SumWaterCount >= bct && Common.SumWaterCount % bct == 0)
                {
                    Common.testBagWaterCount = Common.SumWaterCount / bct;
                    for (int i = 0; i < Common.testBags.Count(); i++)
                    {
                        for (int j = 0; j < Common.testBagWaterCount; j++)
                        {
                            form.AdddgvWateOneAndQCRecord(wateRowCount, wateRowCount + 1, "水分检测", Common.testBags[i]);
                            wateRowCount++;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < form.iWateRowCount; j++)
                    {
                        form.AdddgvWateOneAndQCRecord(wateRowCount, wateRowCount + 1, "水分检测", Common.testBags[0]);
                        wateRowCount++;
                    }
                }
            }
            return wateRowCount;
        }
    }

    public class ChongqingWateAddOpr : IWateAddOpr
    {
        public int AddDgvWateOneAndQCRecord(FormWate form)
        {
            Debug.Assert(null != form);

            int wateRowCount = 0;
            for (int bagID = 0; bagID < Common.testBags.Count(); bagID++)
            {
                for (int waterID = 0; waterID < Common.testBagWaterCount; waterID++)
                {
                    form.AdddgvWateOneAndQCRecord(wateRowCount, wateRowCount + 1, "水分检测", Common.testBags[bagID]);
                    wateRowCount++;
                }
            }
            return wateRowCount;
        }
    }
}
