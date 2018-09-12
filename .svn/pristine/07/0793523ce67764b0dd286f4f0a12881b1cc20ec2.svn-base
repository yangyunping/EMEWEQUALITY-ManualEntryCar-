using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public class OCC_MOIST_INTERFACEDAL
    {
        /// <summary>
        /// 新增一条接口表水分记录
        /// </summary>
        /// <param name="qcRecord">OCC_MOIST_INTERFACE实体</param>
        /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneOCC_MOIST_INTERFACE(OCC_MOIST_INTERFACE qcRecord, out int rint)
        {
            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.OCC_MOIST_INTERFACE.InsertOnSubmit(qcRecord);
                    db.SubmitChanges();
                    rint = qcRecord.OCC_MOIST_INTERFACE_ID;
                    rbool = true;
                }
                catch
                {
                    rbool = false;
                }
                finally { db.Connection.Close(); }

            }
            return rbool;
        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static OCC_MOIST_INTERFACE Single(Expression<Func<OCC_MOIST_INTERFACE, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.OCC_MOIST_INTERFACE.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// summary>
        /// 按条件查询信息View_QCInfo_InTerface
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<OCC_MOIST_INTERFACE> Query(Expression<Func<OCC_MOIST_INTERFACE, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.OCC_MOIST_INTERFACE.Where(fun).ToList();
            }

        }
        /// <summary>
        /// LINQ更新方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="fun"></param>
        /// <param name="tentity"></param>
        /// <param name="action"></param>
        public static bool Update(Expression<Func<OCC_MOIST_INTERFACE, bool>> fun, Action<OCC_MOIST_INTERFACE> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.OCC_MOIST_INTERFACE.Where(fun).ToList();
                foreach (var item in table)
                {
                    action(item);
                }
                dc.SubmitChanges();
            }
            catch
            {
                dc.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);
                dc.SubmitChanges();
                rbool = false;
            }
            finally
            {
                dc.Connection.Close();
            }
            return rbool;
        }
        /// <summary>
        /// 设置并新增一条水分检测信息到接口表
        /// </summary>
        /// <returns></returns>
        public static bool SetAndInsertOCC_MOIST_INTERFACE(View_QCInfo vqcinfo,out int rint)
        {
            bool rbool = false; rint = 0;
            try
            {
                OCC_MOIST_INTERFACE entity = new OCC_MOIST_INTERFACE();
                decimal wateValue = 0;
                if (vqcinfo.QCInfo_MOIST_PER_SAMPLE != null)
                {
                  wateValue = vqcinfo.QCInfo_MOIST_PER_SAMPLE.Value;//水分百分比
                }
                entity.OCC_MOIST_INTERFACE_ID = vqcinfo.QCInfo_ID;
                entity.REF_NO = "";//REF_NO抽包
                entity.CNTR_NO = vqcinfo.CNTR_NO;
                entity.COMPANY_ID = vqcinfo.COMPANY_ID;
                //  entity.DRAW_NO=//包号
                // entity.ITEM_NO=//序号
                entity.MOIST_EXAMINER = vqcinfo.QCInfo_MOIST_EXAMINER;
                entity.MOIST_PER_SAMPLE = vqcinfo.QCInfo_MOIST_PER_SAMPLE;
                entity.PO_NO = vqcinfo.PO_NO;
                entity.PRE_DRY_WT = 100;
                entity.POST_DRY_WT = 100 * (1 - wateValue/100);
                entity.PROD_ID = vqcinfo.PROD_ID;
                entity.QC_RPT_NO = vqcinfo.QC_PRINTNUM;//化验单
                entity.REF_NO = vqcinfo.REF_NO;
                entity.SHIPMENT_NO = vqcinfo.SHIPMENT_NO;
                //entity.SUPPLIER_ID//供应商编号;
                //entity.TRANS_TO_DTS_DTTM = vqcinfo.TRANS_TO_DTS_DTTM;
                // entity.TRANS_TO_DTS_FLAG = vqcinfo.TRANS_TO_DTS_FLAG;
                // entity.TRANS_TO_WPIS_DTTM = vqcinfo.TRANS_TO_WPIS_DTTM;
                // entity.TRANS_TO_WPIS_FLAG = vqcinfo.TRANS_TO_WPIS_FLAG;
                entity.TTL_BALES = vqcinfo.NO_OF_BALES;
                entity.TTL_SAMPLE = vqcinfo.QCInfo_PumpingPackets;
                entity.VERSION_NO = "1";
                entity.WEIGHT_TICKET_NO = vqcinfo.WEIGHT_TICKET_NO;

                //TRANS_TO_WPIS_FLAG="Y"DTS过数到废纸检测系统时，该值为“Y”，废纸检测系统不需理会。 TRANS_TO_DTS_FLAG="Y"当质检完成时，DTS会把数据抄走，同时把该字段设为“Y”，废纸检测系统不需理会。

                 rbool = InsertOneOCC_MOIST_INTERFACE(entity, out rint);
            }
            catch (Exception ex) { 
             
            }
           return rbool;
        }

        /// <summary>
        /// 按条件删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="tentitys"></param>
        /// <param name="fun"></param>
        public static bool DeleteToMany(Expression<Func<OCC_MOIST_INTERFACE, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<OCC_MOIST_INTERFACE>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;
        }
    }
}
