using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public class MATERIAL_QC_INTERFACEDAL
    {
        /// <summary>
        /// 新增一条接口表重量记录
        /// </summary>
        /// <param name="qcRecord">MATERIAL_QC_INTERFACE实体</param>
        /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneMATERIAL_QC_INTERFACE(MATERIAL_QC_INTERFACE qcRecord, out int rint)
        {
            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.MATERIAL_QC_INTERFACE.InsertOnSubmit(qcRecord);
                    db.SubmitChanges();
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
        /// summary>
        /// 按条件查询信息View_QCInfo_InTerface
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MATERIAL_QC_INTERFACE> Query(Expression<Func<MATERIAL_QC_INTERFACE, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.MATERIAL_QC_INTERFACE.Where(fun).ToList();
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
        public static bool Update(Expression<Func<MATERIAL_QC_INTERFACE, bool>> fun, Action<MATERIAL_QC_INTERFACE> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.MATERIAL_QC_INTERFACE.Where(fun).ToList();
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
        /// 设置并新增一条重量检测信息到接口表
        /// </summary>
        /// <returns></returns>
        public static bool SetAndInsertMATERIAL_QC_INTERFACE(View_QCInfo vqcinfo, out int rint)
        {
            bool rbool = false; rint = 0;

            try
            {
                // MATERIAL_QC_INTERFACE qInfo = db.MATERIAL_QC_INTERFACE.First(c => c.CNTR_NO == qc.QCInfo_ID);
                MATERIAL_QC_INTERFACE entity = new MATERIAL_QC_INTERFACE();
                entity.MATERIAL_QC_INTERFACE_ID = vqcinfo.QCInfo_ID;
                //entity.ALUMINIUM_FOIL=//铝箔纸 (kg)
                //entity.BITUMEN_PAPER//沥青纸 (kg)
                entity.CNTR_NO = vqcinfo.CNTR_NO;
                entity.COMPANY_ID = vqcinfo.COMPANY_ID;
                // entity.DOUBLE_WAXED_PAPER = //双面蜡纸 (kg)
                // entity.DRAW_NO==//包号抽样包号码
                //entity.GRAY_PAPER=//灰咭涂布纸 (kg)
                //entity.ITEM_NO//序号
                entity.NET_WEIGHT = vqcinfo.QCInfo_BAGWeight;//重量
                entity.NET_WEIGHT_UOM = "KG";//重量单位
                entity.OTHER_MATERIAL = vqcinfo.QCInfo_MATERIAL_WEIGHT;	//暂时将杂质重量 设置为其它杂质(kg)	
                entity.OTHER_OUTTHROW = vqcinfo.QCInfo_PAPER_WEIGHT;//暂时将杂纸重量 其它杂纸 (kg)	
                entity.PO_NO = vqcinfo.PO_NO;
                entity.PROD_EXAMINER = vqcinfo.QCInfo_PAPER_EXAMINER;
                entity.PROD_ID = vqcinfo.PROD_ID;
                entity.QC_RPT_NO = vqcinfo.QC_PRINTNUM;//化验单(打印编号)
                entity.REF_NO = vqcinfo.REF_NO;
                entity.SHIPMENT_NO = vqcinfo.SHIPMENT_NO;
                //entity.SINGLE_WAXED_PAPER//单面蜡纸 (kg)
                //entity.SUPPLIER_ID//供货商
                // entity.TRANS_TO_DTS_DTTM
                //entity.TRANS_TO_DTS_FLAG
                //entity.TRANS_TO_WPIS_DTTM
                // entity.TRANS_TO_WPIS_FLAG
                entity.TTL_BALES = vqcinfo.NO_OF_BALES;//总包数
                entity.TTL_SAMPLE = vqcinfo.QCInfo_PumpingPackets;//总抽样数
                entity.VERSION_NO = "1";
                entity.WEIGHT = vqcinfo.QCInfo_BAGWeight;//抽样捆重量
                entity.WEIGHT_TICKET_NO = vqcinfo.WEIGHT_TICKET_NO;

                //TRANS_TO_WPIS_FLAG="Y"DTS过数到废纸检测系统时，该值为“Y”，废纸检测系统不需理会。 TRANS_TO_DTS_FLAG="Y"当质检完成时，DTS会把数据抄走，同时把该字段设为“Y”，废纸检测系统不需理会。
                rbool = InsertOneMATERIAL_QC_INTERFACE(entity, out rint);
            }
            catch (Exception ex)
            {

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
        public static bool DeleteToMany(Expression<Func<MATERIAL_QC_INTERFACE, bool>> fun)
        {
            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<MATERIAL_QC_INTERFACE>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }

    }
}
