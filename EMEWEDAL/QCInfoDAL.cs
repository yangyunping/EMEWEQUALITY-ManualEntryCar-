using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data.Linq;
using System.Data;

namespace EMEWEDAL
{
    public class QCInfoDAL
    {
        /// <summary>
        /// 按条件查询信息QCInfo
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static IEnumerable<QCInfo> Query(Expression<Func<QCInfo, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.QCInfo.Where(fun).ToList();
            }
        }
        /// <summary>
        ///  获取待测项列表
        /// </summary>
        /// <param name="strState">质检状态(质检中)</param>
        /// <param name="strTestItemName">待测项目(水分检测,重量检测)</param>
        /// <param name="strWEIGHTTICKETNO">磅单号</param>
        /// <param name="strCarNo">车牌号</param>
        /// <returns></returns>
        public static List<View_QCInfo_InTerface> GetListQCInfo(Expression<Func<View_QCInfo_InTerface, bool>> expr)
        {
            List<View_QCInfo_InTerface> list = new List<View_QCInfo_InTerface>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    list = (from h in db.View_QCInfo_InTerface.Where(expr) select h).ToList();
                }
                finally { db.Connection.Close(); }

            }
            return list;
        }
        /// <summary>
        /// 获取抽样总数  
        /// </summary>
        /// <param name="DRAW_EXAM_INTERFACE_ID">抽样编号</param>
        /// <returns></returns>
        public static int GetIntDRAWCount(int iDRAW_EXAM_INTERFACE_ID)
        {
            int rint = 0;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.Connection.Open();
                    SqlCommand command = new SqlCommand(
         "SELECT ((case when DRAW_ONE is not null and DRAW_ONE >0 then  1 else 0 end ) + " +
 "(case when DRAW_TWO is not null and DRAW_TWO >0 then  1 else 0 end ) +" +
  "(case when DRAW_THREE is not null and  DRAW_THREE>0 then  1 else 0 end ) + " +
  "(case when DRAW_FOUR is not null and DRAW_FOUR>0 then  1 else 0 end ) +" +
  "(case when DRAW_FIVE is not null and  DRAW_FIVE >0 then  1 else 0 end) + " +
  "(case when DRAW_SIX is not null and  DRAW_SIX >0 then  1 else 0 end )) as kk from DRAW_EXAM_INTERFACE where DRAW_EXAM_INTERFACE_ID = " + iDRAW_EXAM_INTERFACE_ID, (SqlConnection)db.Connection);

                    object obj = command.ExecuteScalar();
                    if (obj != null)
                    {
                        rint = EMEWE.Common.Converter.ToInt(obj.ToString());



                    }

                }
                finally
                {
                    db.Connection.Close();
                }
            }
            return rint;
        }

        //添加质检信息
        public static bool addQCInfoDAL(QCInfo qcInfo)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.QCInfo.InsertOnSubmit(qcInfo);
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
        /// 根据质检编号获取质检实体
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        /// <returns>QCInfo</returns>
        public static QCInfo GetQCInfo(int iqcinfoID)
        {
            QCInfo qcInfo = null;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    qcInfo = db.QCInfo.First(c => c.QCInfo_ID == iqcinfoID);
                }
                catch (Exception ex)
                {
                    qcInfo = null;
                    throw ex;
                }
                finally
                {
                    db.Connection.Close();
                }

            }
            return qcInfo;
        }
        /// <summary>
        /// 根据车牌号获取质检实体
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        /// <returns>QCInfo</returns>
        public static View_CarTongJI GetQCInfoCarNO(string CarNo)
        {
            View_CarTongJI qcInfo = null;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    qcInfo = db.View_CarTongJI.First(c => c.CNTR_NO == CarNo);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    db.Connection.Close();
                }

            }
            return qcInfo;
        }

        /// <summary>
        /// 修改质检信息
        /// 注意：请在编写实体之前先获取实体再进行修改的字段编写
        /// </summary>
        /// <param name="qc">质检实体</param>
        /// <returns></returns>
        public static bool UpdateInfo(QCInfo qc)
        {
            bool rbool = false;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    QCInfo qInfo = db.QCInfo.First(c => c.QCInfo_ID == qc.QCInfo_ID);

                    qInfo.QC_PRINTNUM = qc.QC_PRINTNUM;
                    qInfo.QCInfo_BAGWeight = qc.QCInfo_BAGWeight;
                    qInfo.QCInfo_Client_ID = qc.QCInfo_Client_ID;
                    qInfo.QCInfo_DEGREE = qc.QCInfo_DEGREE;
                    qInfo.QCInfo_Dictionary_ID = qc.QCInfo_Dictionary_ID;
                    qInfo.QCInfo_DRAW_EXAM_INTERFACE_ID = qc.QCInfo_DRAW_EXAM_INTERFACE_ID;
                    qInfo.QCInfo_EXPLAIN = qc.QCInfo_EXPLAIN;
                    //qInfo.QCInfo_ISLEDSHOW = qc.QCInfo_ISLEDSHOW;
                    qInfo.QCInfo_MATERIAL_BAD_CSALE = qc.QCInfo_MATERIAL_BAD_CSALE;
                    qInfo.QCInfo_MATERIAL_EXAMINER = qc.QCInfo_MATERIAL_EXAMINER;
                    qInfo.QCInfo_MATERIAL_SCALE = qc.QCInfo_MATERIAL_SCALE;
                    qInfo.QCInfo_MATERIAL_WEIGHT = qc.QCInfo_MATERIAL_WEIGHT;
                    qInfo.QCInfo_MOIST_BAD_PER_SAMPLE = qc.QCInfo_MOIST_BAD_PER_SAMPLE;
                    qInfo.QCInfo_MOIST_EXAMINER = qc.QCInfo_MOIST_EXAMINER;
                    qInfo.QCInfo_MOIST_PER_SAMPLE = qc.QCInfo_MOIST_PER_SAMPLE;
                    qInfo.QCInfo_PAPER_BAD_SCALE = qc.QCInfo_PAPER_BAD_SCALE;
                    qInfo.QCInfo_PAPER_EXAMINER = qc.QCInfo_PAPER_EXAMINER;
                    qInfo.QCInfo_PAPER_SCALE = qc.QCInfo_PAPER_SCALE;
                    qInfo.QCInfo_PAPER_WEIGHT = qc.QCInfo_PAPER_WEIGHT;
                    qInfo.QCInfo_PumpingPackets = qc.QCInfo_PumpingPackets;
                    qInfo.QCInfo_QCGroup_ID = qc.QCInfo_QCGroup_ID;
                    qInfo.QCInfo_REMARK = qc.QCInfo_REMARK;
                    qInfo.QCInfo_STATE = qc.QCInfo_STATE;
                    qInfo.QCInfo_TIME = qc.QCInfo_TIME;
                    qInfo.QCInfo_UnpackBack_MOIST_COUNT = qc.QCInfo_UnpackBack_MOIST_COUNT;
                    qInfo.QCInfo_UnpackBack_MOIST_EXAMINER = qc.QCInfo_UnpackBack_MOIST_EXAMINER;
                    qInfo.QCInfo_UnpackBack_MOIST_PER_SAMPLE = qc.QCInfo_UnpackBack_MOIST_PER_SAMPLE;
                    qInfo.QCInfo_UnpackBefore_MOIST_EXAMINER = qc.QCInfo_UnpackBefore_MOIST_EXAMINER;
                    qInfo.QCInfo_UnpackBefore_MOIST_PER_COUNT = qc.QCInfo_UnpackBefore_MOIST_PER_COUNT;
                    qInfo.QCInfo_UnpackBefore_MOIST_PER_SAMPLE = qc.QCInfo_UnpackBefore_MOIST_PER_SAMPLE;
                    qInfo.QCInfo_UPDATE_NAME = qc.QCInfo_UPDATE_NAME;
                    qInfo.QCInfo_UPDATE_REASON = qc.QCInfo_UPDATE_REASON;
                    qInfo.QCInfo_UPDATE_TIME = qc.QCInfo_UPDATE_TIME;
                    qInfo.QCInfo_UserId = qc.QCInfo_UserId;
                    qInfo.QCIInfo_EndTime = qc.QCIInfo_EndTime;
                    qInfo.QCInfo_RETURNBAG_COUNT = qc.QCInfo_RETURNBAG_COUNT;
                    qInfo.QCInfo_RETURNBAG_EXAMINER = qc.QCInfo_RETURNBAG_EXAMINER;
                    qInfo.QCInfo_RETURNBAG_TIME = qc.QCInfo_RETURNBAG_TIME;
                    qInfo.QCInfo_RETURNBAG_WEIGH = qc.QCInfo_RETURNBAG_WEIGH;
                    qInfo.QCInfo_RECV_RMA_METHOD = qc.QCInfo_RECV_RMA_METHOD;
                    db.SubmitChanges();
                    rbool = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                    //log
                }
                finally
                {
                    db.Connection.Close();
                }
            }

            return rbool;

        }
        /// <summary>
        /// 查询所有信息View_QCInfo_InTerface
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_QCInfo_InTerface> Query()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_QCInfo_InTerface.ToList();
            }

        }
        /// <summary>
        /// summary>
        /// 按条件查询信息View_QCInfo_InTerface
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_QCInfo> Query(Expression<Func<View_QCInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_QCInfo.Where(fun).ToList();
            }
        }
        /// <summary>
        /// 按条件查询信息View_QCRecordInfo
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static IEnumerable<View_QCRecordInfo> Query(Expression<Func<View_QCRecordInfo, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_QCRecordInfo.Where(fun).ToList();
            }
        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static View_QCInfo Single(Expression<Func<View_QCInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_QCInfo.SingleOrDefault(fun);
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
        public static bool Update(Expression<Func<QCInfo, bool>> fun, Action<QCInfo> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.QCInfo.Where(fun).ToList();
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
        /// 根据质检编号对应的包号
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        /// <returns>QCInfo</returns>
        public static List<string> GetBags(int iqcinfoID)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "select DRAW_ONE,DRAW_TWO,DRAW_THREE,DRAW_FOUR,DRAW_FIVE,DRAW_SIX,DRAW_7,DRAW_8,DRAW_9,DRAW_10,DRAW_11,DRAW_12,DRAW_13,DRAW_14 from dbo.DRAW_EXAM_INTERFACE where DRAW_EXAM_INTERFACE_ID in(select  QCInfo_DRAW_EXAM_INTERFACE_ID from QCInfo where QCInfo_ID='" + iqcinfoID + "')";
                string ziduan = "DRAW_ONE,DRAW_TWO,DRAW_THREE,DRAW_FOUR,DRAW_FIVE,DRAW_SIX,DRAW_7,DRAW_8,DRAW_9,DRAW_10,DRAW_11,DRAW_12,DRAW_13,DRAW_14";
                DataTable dtprt = LinQBaseDao.Query(sql).Tables[0];
                string[] danziduan = ziduan.Split(',');
                for (int j = 0; j < danziduan.Length; j++)
                {
                    if (hbbh(dtprt, danziduan[j]) != "")
                    {
                        list.Add(hbbh(dtprt, danziduan[j]));
                    }
                }
            }
            catch (Exception)
            {
                list = null;
            }
            return list;
        }

        /// <summary>
        /// 合并包号
        /// </summary>
        /// <param name="dtprt"></param>
        /// <param name="strattent"></param>
        /// <returns></returns>
        private static string hbbh(DataTable dtprt, string ziduan)
        {
            string strattent = "";
            if (dtprt.Rows[0][ziduan].ToString() != "" && dtprt.Rows[0][ziduan].ToString() != "0")
            {
                strattent =  dtprt.Rows[0][ziduan].ToString() ;
            }
            return strattent;
        }

        /// <summary>
        /// 获得QCInfo表中的水分总值、杂质总重量、杂纸总重量的平均值
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        /// <returns></returns>
        public static List<QCRecordAVGEnitiy> GetQCInfo_RESULT_AVERAGE(int iqcinfoID, string CNTR_NO)
        {
            List<QCRecordAVGEnitiy> list = new List<QCRecordAVGEnitiy>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    Expression<Func<QCInfo, bool>> expr = (Expression<Func<QCInfo, bool>>)PredicateExtensionses.True<QCInfo>();
                    if (iqcinfoID > 0)
                    {
                        expr = expr.And(p => (p.QCInfo_ID == iqcinfoID));
                    }
                    if (CNTR_NO != "")
                    {
                        int qcinfoID = db.QCInfo.First(d => (d.DRAW_EXAM_INTERFACE.CNTR_NO == CNTR_NO && d.Dictionary.Dictionary_Value == "启动")).QCInfo_ID;
                        expr = expr.And(p => (p.QCInfo_ID == qcinfoID));
                    }
                    // 平均水分：QCInfo_MOIST_PER_SAMPLE  《《《 有疑问 》》》
                    expr = expr.And(p => (p.Dictionary.Dictionary_Value == "启动" && (p.QCInfo_MOIST_PER_SAMPLE != null && p.QCInfo_MOIST_PER_SAMPLE > 0)));

                    var c = (from p in db.QCInfo.Where(expr)
                             group p by new // 分组
                             {
                                 p.DRAW_EXAM_INTERFACE.CNTR_NO, // 车牌号
                                 p.QCInfo_STATE,                // 质检状态
                                 p.DRAW_EXAM_INTERFACE.WEIGHT_TICKET_NO, // 磅单号
                                 p.DRAW_EXAM_INTERFACE.PO_NO,            // 采购单
                                 p.DRAW_EXAM_INTERFACE.SHIPMENT_NO       // 送货单
                             } into g
                             select new
                             {
                                 CNTR_NO = g.Key.CNTR_NO, // 以车牌号为Key键
                                 Count = g.Count(),       // 每个车牌号出现的次数，及按车牌号统计数据
                                 Sum = g.Sum(p => p.QCInfo_MOIST_PER_SAMPLE),     // 总水分值
                                 Sum1 = g.Sum(p => p.QCInfo_MATERIAL_WEIGHT),     //  总杂质重量
                                 Sum2 = g.Sum(p => p.QCInfo_PAPER_WEIGHT),        //  总杂纸重量
                                 Avg = g.Average(p => p.QCInfo_MOIST_PER_SAMPLE), // 水分平均值
                                 Avg1 = g.Average(p => p.QCInfo_MATERIAL_WEIGHT), // 杂质重量平均值
                                 Avg2 = g.Average(p => p.QCInfo_PAPER_WEIGHT),    // 杂纸重量平均值
                             }).ToList();
                    if (c != null)
                    {
                        QCRecordAVGEnitiy enitiy;
                        for (int i = 0; i < c.Count; i++)
                        {
                            enitiy = new QCRecordAVGEnitiy();
                            enitiy.TestItemName = c[i].CNTR_NO;    // 车牌号
                            enitiy.Count = c[i].Count;             // 计算每个车牌号的车辆载重次数
                            enitiy.Sum = (double)c[i].Sum.Value;   // 总重量
                            enitiy.Sum1 = (double)c[i].Sum1.Value;
                            enitiy.Sum2 = (double)c[i].Sum2.Value;
                            enitiy.Avg = (double)c[i].Avg.Value;   // 平均值
                            enitiy.Avg1 = (double)c[i].Avg1.Value;
                            enitiy.Avg2 = (double)c[i].Avg2.Value;
                            list.Add(enitiy);
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally { db.Connection.Close(); }
            }
        }
        public static List<QCRecordAVGEnitiy> GetQCInfo_RESULT_AVG(Expression<Func<View_QCInfo_InTerface, bool>> expr)
        {
            List<QCRecordAVGEnitiy> list = new List<QCRecordAVGEnitiy>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    expr = (Expression<Func<View_QCInfo_InTerface, bool>>)PredicateExtensionses.True<View_QCInfo_InTerface>();
                    var c = (from p in db.View_QCInfo_InTerface.Where(expr)
                             group p by new  // 分组
                             {
                                 p.CNTR_NO,  // 车牌号
                                 p.WEIGHT_TICKET_NO, // 磅单号
                                 p.PO_NO,            // 采购单
                                 p.SHIPMENT_NO       // 送货单
                             } into g
                             select new
                             {
                                 CNTR_NO = g.Key.CNTR_NO, // 以车牌号为Key键
                                 Count = g.Count(),       // 每个车牌号出现的次数，及按车牌号统计数据
                                 Sum = g.Sum(p => p.QCInfo_MOIST_PER_SAMPLE),     // 总水分值
                                 Sum1 = g.Sum(p => p.QCInfo_MATERIAL_WEIGHT),     //  总杂质重量
                                 Sum2 = g.Sum(p => p.QCInfo_PAPER_WEIGHT),        //  总杂纸重量
                                 Avg = g.Average(p => p.QCInfo_MOIST_PER_SAMPLE), // 水分平均值
                                 Avg1 = g.Average(p => p.QCInfo_MATERIAL_WEIGHT), // 杂质重量平均值
                                 Avg2 = g.Average(p => p.QCInfo_PAPER_WEIGHT),    // 杂纸重量平均值
                             }).ToList();
                    if (c != null)
                    {
                        QCRecordAVGEnitiy enitiy;
                        for (int i = 0; i < c.Count; i++)
                        {
                            enitiy = new QCRecordAVGEnitiy();
                            enitiy.TestItemName = c[i].CNTR_NO;    // 车牌号
                            enitiy.Count = c[i].Count;             // 计算每个车牌号的车辆载重次数
                            enitiy.Sum = (double)c[i].Sum.Value;   // 总重量
                            enitiy.Sum1 = (double)c[i].Sum1.Value;
                            enitiy.Sum2 = (double)c[i].Sum2.Value;
                            enitiy.Avg = (double)c[i].Avg.Value;   // 平均值
                            enitiy.Avg1 = (double)c[i].Avg1.Value;
                            enitiy.Avg2 = (double)c[i].Avg2.Value;
                            //enitiy.BeginTime = (DateTime)c[i].BeginTime; // 日期时间
                            //enitiy.EndTime = (DateTime)c[i].EndTime;
                            list.Add(enitiy);
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("获得质检记录表中的水分各项平均值：{0}", ex.Message));
                }
                finally { db.Connection.Close(); }
            }
        }
        /// <summary>
        /// 获得质检记录表中的重量各项总和
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        /// <returns></returns>
        public static List<QCRecordAVGEnitiy> GetQCInfo_RESULT_SUM(int iqcinfoID, string strTestItems_NAME)
        {
            List<QCRecordAVGEnitiy> list = new List<QCRecordAVGEnitiy>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    #region  注 释
                    //        Expression<Func<View_QCInfo_InTerface, bool>> expr = (Expression<Func<View_QCInfo_InTerface, bool>>)PredicateExtensionses.True<View_QCInfo_InTerface>();
                    //        if (iqcinfoID > 0)
                    //        {
                    //            expr = expr.And(p => (p.QCInfo_ID == iqcinfoID));
                    //        }
                    //        if (strTestItems_NAME != "")
                    //        {
                    //            int testItemsID = db.TestItems.First(d => (d.TestItems_NAME == strTestItems_NAME && d.Dictionary.Dictionary_Value == "启动")).TestItems_ID;
                    //            expr = expr.And(p => (p.QCInfo_Dictionary_ID == testItemsID));
                    //        }
                    //        expr = expr.And(p => (p.Dictionary_Value == "启动" && (p.QCInfo_MOIST_PER_SAMPLE != null && p.QCInfo_MOIST_PER_SAMPLE > 0)));

                    //        var c = (from p in db.View_QCInfo_InTerface.Where(expr)
                    //                 group p by new
                    //                 {
                    //                     p.CNTR_NO,
                    //                     p.QCInfo_ID,
                    //                     p.QCInfo_STATE
                    //                 } into g
                    //                 select new
                    //                 {
                    //                     TestItemName = g.Key.CNTR_NO,
                    //                     Count = g.Count(),
                    //                     Avg = g.Sum(p => p.QCInfo_MOIST_PER_SAMPLE)
                    //                 }).ToList();
                    //        if (c != null)
                    //        {
                    //            QCRecordAVGEnitiy enitiy;
                    //            for (int i = 0; i < c.Count; i++)
                    //            {
                    //                enitiy = new QCRecordAVGEnitiy();
                    //                enitiy.TestItemName = c[i].TestItemName;
                    //                enitiy.Count = c[i].Count;
                    //                enitiy.Avg = (double)c[i].Avg.Value;
                    //                list.Add(enitiy);
                    //            }
                    //        }
                    #endregion
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("获得质检记录表中的重量各项总和：{0}", ex.Message));
                }
                finally { db.Connection.Close(); }
            }
        }

        /// <summary>
        /// 按条件删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="tentitys"></param>
        /// <param name="fun"></param>
        public static bool DeleteToMany(Expression<Func<QCInfo, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<QCInfo>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }

        /// </summary>
        /// 新增一条车辆质检记录
        /// </summary>
        /// <param name="qcRecord">QCRecord质检实体</param>
        /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneCarInfo(QCInfo DRAW_E_INI, out int rint)
        {

            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.QCInfo.InsertOnSubmit(DRAW_E_INI);
                    db.SubmitChanges();
                    rbool = true;
                    rint = DRAW_E_INI.QCInfo_ID;
                }
                catch
                {
                    rbool = false;
                }
                finally { db.Connection.Close(); }

            }
            return rbool;
        }


    }
}