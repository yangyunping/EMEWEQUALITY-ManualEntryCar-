using System;
using System.Collections.Generic;
using System.Linq;
using EMEWEQUALITY.LWU9WebReference;
using System.Windows.Forms;
using System.Linq.Expressions;
using EMEWEDAL;
using System.Data;
using EMEWEEntity;

namespace EMEWEQUALITY.HelpClass
{
    //2014-12-9  杨敦钦 
    public static class U9Class
    {
        /// <summary>
        /// 调用U9接口
        /// </summary>
        /// <param name="DRAW_EXAM_INTERFACE_ID">登记编号</param>
        /// <param name="QcInfoid">质检编号</param>
        /// <returns></returns>
        public static string updataIsU9(int DRAW_EXAM_INTERFACE_ID, string QcInfoid)
        {
            try
            {
                Expression<Func<DRAW_EXAM_INTERFACE, bool>> funDRAW_EXAM_INTERFACE = n => n.DRAW_EXAM_INTERFACE_ID == DRAW_EXAM_INTERFACE_ID;
                if (isIdebarJoinU9(DRAW_EXAM_INTERFACEDAL.Query(funDRAW_EXAM_INTERFACE).FirstOrDefault().PROD_ID))
                {
                    DataTable issendDT = LinQBaseDao.Query("select issend from RegisterLoosePaperDistribution where R_DRAW_EXAM_INTERFACE_ID=" + DRAW_EXAM_INTERFACE_ID).Tables[0];
                    if (issendDT.Rows.Count > 0)
                    {
                        object str = issendDT.Rows[0][0];
                        bool b = Convert.ToBoolean(str);
                        if (!b)
                        {
                            string sql = "select QCInfo_PumpingPackets,QCInfo_BAGWeight,ExtensionField2,QCInfo_ID,SHIPMENT_NO,DepartmentCode  from View_QCInfo where QCInfo_ID='" + QcInfoid + "'";
                            DataSet ds = LinQBaseDao.Query(sql);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    int count = int.Parse(ds.Tables[0].Rows[0]["QCInfo_PumpingPackets"].ToString());
                                    double whiet = Convert.ToDouble(ds.Tables[0].Rows[0]["QCInfo_BAGWeight"].ToString());//重量
                                    string dw = ds.Tables[0].Rows[0]["ExtensionField2"].ToString();
                                    string bd = ds.Tables[0].Rows[0]["QCInfo_ID"].ToString();
                                    string cgd = ds.Tables[0].Rows[0]["SHIPMENT_NO"].ToString();
                                    string bmdm = ds.Tables[0].Rows[0]["DepartmentCode"].ToString();
                                    object CountObj = LinQBaseDao.GetSingle("select count(*) from QCRecord where QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='废纸包重') and QCRecord_UPDATE_REASON is NULL and QCRecord_QCInfo_ID=" + QcInfoid);
                                    if (count != (int)CountObj)
                                    {
                                        DialogResult dr = MessageBox.Show("质检信息不一致是否继续发送数据到U9", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                                        if (dr.Equals(DialogResult.No))
                                        {
                                            return "失败";
                                        }
                                    }
                                    if (count > 0 && whiet > 0 && dw != "" && bd != "" && cgd != "" && bmdm != "")
                                    {
                                        bool isSend = U9Class.sendDate(count, whiet, dw, bd, cgd, bmdm);
                                        if (isSend)
                                        {
                                            //发送成功修改标识
                                            int result = LinQBaseDao.ExecuteSql("update dbo.RegisterLoosePaperDistribution set  issend=1 where R_DRAW_EXAM_INTERFACE_ID=" + DRAW_EXAM_INTERFACE_ID);
                                            return "";
                                        }
                                        else
                                        {
                                            return "失败";
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("数据不完整无法发送数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                        return "失败";
                                    }
                                }
                            }
                        }
                    }
                }
                return "";
            }
            catch(Exception err)
            {
                return "失败";
            }
        }

        /// <summary>
        /// 是否需要和U9交互
        /// </summary>
        /// <param name="PROD_ID"></param>
        public static bool isIdebarJoinU9(string PROD_ID)
        {

            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            List<idebarJoinU9> ijList = dc.idebarJoinU9.ToList();
            foreach (idebarJoinU9 item in ijList)
            {
                if (item.idebarJoinU9_PROD_ID == PROD_ID)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 发送数据到U9
        /// </summary>
        /// <param name="count">扎数</param>
        /// <param name="Weight">重量</param>
        /// <param name="ExtensionField2">堆位</param>
        /// <param name="ExtensionField3">过磅单号</param>
        /// <param name="ShipmentNumber">采购单</param>
        /// <param name="DepartmentCode">部门代码</param>
        public static bool sendDate(int count, double Weight, string ExtensionField2, string ExtensionField3, string ShipmentNumber, string DepartmentCode)
        {
            try
            {
                LWU9WebReference.LooseWastePaperManagement_v001 lw = new EMEWEQUALITY.LWU9WebReference.LooseWastePaperManagement_v001();

                string OrganizationID = Common.OrganizationID;//公司代号
                LoosePaperTruckLoadU9Type TruckLoad = new LoosePaperTruckLoadU9Type();
                WeightType wt = new WeightType();
                wt.Value = Convert.ToDecimal(Weight);
                WeightUnitType wut = WeightUnitType.kg;

                Weight = (double)Weight / (double)1000;
                TruckLoad.Count = count;//扎数
                TruckLoad.Weight = Weight;//重量
                TruckLoad.ExtensionField2 = ExtensionField2;//堆位
                TruckLoad.ExtensionField3 = ExtensionField3;//过磅单号
                TruckLoad.WorkshopMachineID = "201311111111";
                TruckLoad.DepartmentCode = DepartmentCode;//部门代码
                TruckLoad.BusinessDate = DateTime.Now;
                TruckLoad.TruckLoadDocumentID = "10";

                RequestHeaderType rht = new RequestHeaderType();
                rht.SourceID = Common.SourceID;
                lw.Url = Common.SendURL;
                lw.RequestHeader = rht;//数据源（发起请求应用名）
                LoosePaperContainerShareU9Type[] Distribution = new LoosePaperContainerShareU9Type[1];
                LoosePaperContainerShareU9Type lpc = new LoosePaperContainerShareU9Type();
                //必填
                lpc.ContainerID = "30";
                lpc.IsGettingShare = true;
                lpc.PONumber = "20";
                lpc.ShipmentNumber = ShipmentNumber;//采购单
                lpc.UnloadPlatform = "备注";
                lpc.Memo = "20.质检纸";
                lpc.TypeOfWeighedWastePaper = TypeOfWeighedWastePaperType.BaleLocal;//纸种
                lpc.TypeOfWeighedWastePaperSpecified = true;

                Distribution[0] = lpc;
                string DistributionMemo = "cs1";

                System.Xml.XmlAttribute[] AnyAttr = new System.Xml.XmlAttribute[0];
                lpc.AnyAttr = AnyAttr;

                ReplyMessageType message = lw.RegisterLoosePaperDistribution(OrganizationID, TruckLoad, DistributionMemo, Distribution, AnyAttr);
                if (message.ResultCode.ToString() == "Successful")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
