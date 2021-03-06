﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;

namespace EMEWEDAL
{
    public class QCRecordDAL
    {
        /// <summary>
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">QCRecord质检实体</param>
        /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(QCRecord qcRecord, out int rint)
        {
            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.QCRecord.InsertOnSubmit(qcRecord);
                    db.SubmitChanges();
                    rint = qcRecord.QCRecord_ID;
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
        /// 根据质检记录编号获取质检记录实体
        /// </summary>
        /// <param name="iqcRecordID">质检记录编号</param>
        /// <returns>QCRecord</returns>
        public static QCRecord GetQCInfo(int iqcRecordID)
        {
            QCRecord qcInfo = null;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    qcInfo = db.QCRecord.First(c => c.QCRecord_ID == iqcRecordID);

                }
                catch (Exception ex)
                {
                    throw new Exception("根据质检记录编号获取质检记录实体（QCRecord）:" + ex.Message);

                }
                finally
                {
                    db.Connection.Close();
                }

            }
            return qcInfo;
        }



        /// <summary>
        /// 根据质检记录编号获取质检记录实体
        /// </summary>
        /// <param name="iqcRecordID">质检记录编号</param>
        /// <returns>QCRecord</returns>
        public static QCRecord GetQCInfo(int iqcRecordID,string testItemName)
        {
            QCRecord qcInfo = null;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    qcInfo = db.QCRecord.First(c => c.QCRecord_ID == iqcRecordID);

                }
                catch (Exception ex)
                {
                    throw new Exception("根据质检记录编号获取质检记录实体（QCRecord）:" + ex.Message);

                }
                finally
                {
                    db.Connection.Close();
                }

            }
            return qcInfo;
        }
        /// <summary>
        /// 修改质检记录信息
        /// 注意：请在编写实体之前先获取实体再进行修改的字段编写
        /// </summary>
        /// <param name="record">质检记录实体（QCRecord）</param>
        /// <returns></returns>
        public static bool UpdateOneQcRecord(QCRecord record)
        {
            bool rbool = false;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    QCRecord qInfo = db.QCRecord.First(c => c.QCRecord_ID == record.QCRecord_ID);

                    qInfo.QCRecord_Client_ID = record.QCRecord_Client_ID;
                    qInfo.QCRecord_COUNT = record.QCRecord_COUNT;
                    qInfo.QCRecord_Dictionary_ID = record.QCRecord_Dictionary_ID;
                    qInfo.QCRecord_DRAW = record.QCRecord_DRAW;
                   // qInfo.QCRecord_ISLEDSHOW = record.QCRecord_ISLEDSHOW;
                    qInfo.QCRecord_ISRETEST = record.QCRecord_ISRETEST;
                    qInfo.QCRecord_NAME = record.QCRecord_NAME;
                    qInfo.QCRecord_NUMBER = record.QCRecord_NUMBER;
                    qInfo.QCRecord_OFF_TIME = record.QCRecord_OFF_TIME;
                    qInfo.QCRecord_QCCOUNT = record.QCRecord_QCCOUNT;
                    qInfo.QCRecord_QCInfo_ID = record.QCRecord_QCInfo_ID;
                    qInfo.QCRecord_QCRetest_ID = record.QCRecord_QCRetest_ID;
                    qInfo.QCRecord_REMARK = record.QCRecord_REMARK;
                    qInfo.QCRecord_RESULT = record.QCRecord_RESULT;
                    qInfo.QCRecord_TARE = record.QCRecord_TARE;
                    qInfo.QCRecord_TestItems_ID = record.QCRecord_TestItems_ID;
                    qInfo.QCRecord_TIME = record.QCRecord_TIME;
                    qInfo.QCRecord_TYPE = record.QCRecord_TYPE;
                    qInfo.QCRecord_UPDATE_REASON = record.QCRecord_UPDATE_REASON;
                    qInfo.QCRecord_UPDATE_TIME = record.QCRecord_UPDATE_TIME;
                    qInfo.QCRecord_UserId = record.QCRecord_UserId;
                    qInfo.QCRecord_UserId_UpdateID = record.QCRecord_UserId_UpdateID;
                    qInfo.QCRecord_UpdateCardUserName = record.QCRecord_UpdateCardUserName;
                    db.SubmitChanges();
                    rbool = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("修改质检信息（QCInfo）:" + ex.Message);
                }
                finally
                {
                    db.Connection.Close();
                }
            }

            return rbool;
        }
        /// <summary>
        /// 获取质检记录的最大的编号
        /// </summary>
        /// <returns></returns>
        public static int GetMaxQCRecordId()
        {
            int rint = 0;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    rint = db.QCRecord.Max(p => p.QCRecord_ID);
                }
                catch (Exception ex)
                {
                    throw new Exception("获取质检记录的最大的编号:" + ex.Message);
                }
                finally { db.Connection.Close(); }
            }
            return rint;
        }
        /// <summary>
        /// 获得质检记录表中的水分各项平均值
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        /// <param name="QCRecord_QCRetest_ID">复测编号</param>
        /// <returns></returns>
        public static List<QCRecordAVGEnitiy> GetQCRecord_RESULT_AVERAGE(int iqcinfoID, int iQCRecord_QCRetest_ID, string strTestItems_NAME)
        {
            List<QCRecordAVGEnitiy> list = new List<QCRecordAVGEnitiy>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    Expression<Func<QCRecord, bool>> expr = (Expression<Func<QCRecord, bool>>)PredicateExtensionses.True<QCRecord>();
                    if (iqcinfoID > 0)
                    {
                        expr = expr.And(p => (p.QCRecord_QCInfo_ID == iqcinfoID));
                    }
                    if (iQCRecord_QCRetest_ID > 0)
                    {
                        expr = expr.And(p => (p.QCRecord_QCRetest_ID == iQCRecord_QCRetest_ID));
                    }
                    if (strTestItems_NAME != "")
                    {
                        int testItemsID = db.TestItems.First(d => (d.TestItems_NAME == strTestItems_NAME && d.Dictionary.Dictionary_Value == "启动")).TestItems_ID;
                        expr = expr.And(p => (p.TestItems.Tes_TestItems_ID == testItemsID));
                    }
                    expr = expr.And(p => (p.Dictionary.Dictionary_Value == "启动" && (p.QCRecord_RESULT != null && p.QCRecord_RESULT > 0)));

                    var c = (from p in db.QCRecord.Where(expr)
                             group p by new
                                 {
                                     p.TestItems.TestItems_NAME,
                                     p.QCRecord_QCInfo_ID,
                                     p.QCRecord_QCRetest_ID
                                 } into g
                             select new
                             {
                                 TestItemName = g.Key.TestItems_NAME,
                                 Count = g.Count(),
                                 Avg = g.Average(p => p.QCRecord_RESULT)
                             }).ToList();

                    if (c != null)
                    {
                        QCRecordAVGEnitiy enitiy;
                        for (int i = 0; i < c.Count; i++)
                        {
                            enitiy = new QCRecordAVGEnitiy();
                            enitiy.TestItemName = c[i].TestItemName;
                            enitiy.Count = c[i].Count;
                            enitiy.Avg = (double)c[i].Avg.Value;
                            list.Add(enitiy);
                        }
                        #region 计算水分检测平均值
                        string strwateItem = "水分检测";
                        int testItemsID = db.TestItems.First(d => (d.TestItems_NAME == strwateItem && d.Dictionary.Dictionary_Value == "启动")).TestItems_ID;
                        enitiy = new QCRecordAVGEnitiy();
                        enitiy.TestItemName = strwateItem;
                        enitiy.Count = 0;
                        if (iQCRecord_QCRetest_ID > 0) {
                            var f = (from p in db.QCRecord
                                     where p.QCRecord_QCInfo_ID == iqcinfoID && p.QCRecord_QCRetest_ID == iQCRecord_QCRetest_ID && p.TestItems.Tes_TestItems_ID == testItemsID && p.Dictionary.Dictionary_Value == "启动" && (p.QCRecord_RESULT != null && p.QCRecord_RESULT > 0)
                                     select p.QCRecord_RESULT).Average();
                            if(f!=null)
                            enitiy.Avg = (double)f.Value;
                        }
                        else
                        {
                            var f = (from p in db.QCRecord
                                     where p.QCRecord_QCInfo_ID == iqcinfoID && p.TestItems.Tes_TestItems_ID == testItemsID && p.Dictionary.Dictionary_Value == "启动" && (p.QCRecord_RESULT != null && p.QCRecord_RESULT > 0)
                                     select p.QCRecord_RESULT).Average();
                            if (f != null)
                            enitiy.Avg = (double)f.Value;
                        }
                 
                        list.Add(enitiy);
                        #endregion
                    }
                    #region 注释
                    //int testItemsID = db.TestItems.First(d => (d.TestItems_NAME == strTestItems_NAME && d.Dictionary.Dictionary_Value == "启动")).TestItems_ID;
                    //if (iQCRecord_QCRetest_ID >= 1)
                    //{
                    //    var c = (from p in db.QCRecord
                    //             where p.QCRecord_QCInfo_ID == iqcinfoID && p.TestItems.Tes_TestItems_ID == testItemsID && p.Dictionary.Dictionary_Value == "启动" && (p.QCRecord_RESULT != null && p.QCRecord_RESULT > 0) && (p.QCRecord_QCRetest_ID != null && p.QCRecord_QCRetest_ID == iQCRecord_QCRetest_ID)
                    //             group p by new
                    //             {
                    //                 p.TestItems.TestItems_NAME
                    //                 //, p.QCRecord_QCInfo_ID,

                    //                 //p.QCRecord_QCRetest_ID
                    //             } into g
                    //             select new
                    //             {
                    //                 TestItemName = g.Key.TestItems_NAME,
                    //                 Count = g.Count(),
                    //                 Avg = g.Average(p => p.QCRecord_RESULT)
                    //             }).ToList();
                    //    if (c != null)
                    //    {
                    //        QCRecordAVGEnitiy enitiy;
                    //        for (int i = 0; i < c.Count; i++)
                    //        {
                    //            enitiy = new QCRecordAVGEnitiy();
                    //            enitiy.TestItemName = c[i].TestItemName;
                    //            enitiy.Count = c[i].Count;
                    //            enitiy.Avg = (double)c[i].Avg.Value;
                    //            list.Add(enitiy);
                    //        }
                    //        var f = (from p in db.QCRecord
                    //                 where p.QCRecord_QCInfo_ID == iqcinfoID && p.TestItems.Tes_TestItems_ID == testItemsID && p.Dictionary.Dictionary_Value == "启动" && (p.QCRecord_RESULT != null && p.QCRecord_RESULT > 0) && (p.QCRecord_QCRetest_ID != null && p.QCRecord_QCRetest_ID == iQCRecord_QCRetest_ID)
                    //                 select p.QCRecord_RESULT).Average();
                    //        enitiy = new QCRecordAVGEnitiy();
                    //        enitiy.TestItemName = strTestItems_NAME;
                    //        enitiy.Count = 0;
                    //        enitiy.Avg = (double)f.Value;
                    //        list.Add(enitiy);
                    //    }

                    //}
                    //else
                    //{
                    //    var c = (from p in db.QCRecord
                    //             where p.QCRecord_QCInfo_ID == iqcinfoID && p.TestItems.Tes_TestItems_ID == testItemsID && p.Dictionary.Dictionary_Value == "启动" && (p.QCRecord_RESULT != null && p.QCRecord_RESULT > 0)
                    //             group p by new
                    //             {
                    //                 // p.QCRecord_QCInfo_ID,
                    //                 p.TestItems.TestItems_NAME
                    //                 // ,  p.QCRecord_QCRetest_ID
                    //             } into g
                    //             select new
                    //             {
                    //                 TestItemName = g.Key.TestItems_NAME,
                    //                 Count = g.Count(),
                    //                 Avg = g.Average(p => p.QCRecord_RESULT)
                    //             }).ToList();
                    //    if (c != null)
                    //    {
                    //        QCRecordAVGEnitiy enitiy;
                    //        for (int i = 0; i < c.Count; i++)
                    //        {
                    //            enitiy = new QCRecordAVGEnitiy();
                    //            enitiy.TestItemName = c[i].TestItemName;
                    //            enitiy.Count = c[i].Count;
                    //            enitiy.Avg = (double)c[i].Avg.Value;
                    //            list.Add(enitiy);
                    //        }
                    //        var f = (from p in db.QCRecord
                    //                 where p.QCRecord_QCInfo_ID == iqcinfoID && p.TestItems.Tes_TestItems_ID == testItemsID && p.Dictionary.Dictionary_Value == "启动" && (p.QCRecord_RESULT != null && p.QCRecord_RESULT > 0)
                    //                 select p.QCRecord_RESULT).Average();
                    //        enitiy = new QCRecordAVGEnitiy();
                    //        enitiy.TestItemName = strTestItems_NAME;
                    //        enitiy.Count = 0;
                    //        enitiy.Avg = (double)f.Value;
                    //        list.Add(enitiy);
                    //    }

                    //}
                    #endregion
                    return list;
                }
                catch (Exception ex)
                {
                    list = null;
                    throw new Exception(string.Format("获得质检记录表中的水分各项平均值：{0}", ex.Message));
                    //log           
                }
                finally { db.Connection.Close(); }
            }

        }
        /// <summary>
        /// 获得质检记录表中的重量各项总和
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        /// <param name="QCRecord_QCRetest_ID">复测编号</param>
        /// <returns></returns>
        public static List<QCRecordAVGEnitiy> GetQCRecord_RESULT_SUM(int iqcinfoID, int iQCRecord_QCRetest_ID, string strTestItems_NAME)
        {
            List<QCRecordAVGEnitiy> list = new List<QCRecordAVGEnitiy>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    Expression<Func<QCRecord, bool>> expr = (Expression<Func<QCRecord, bool>>)PredicateExtensionses.True<QCRecord>();
                    if (iqcinfoID > 0)
                    {
                        expr = expr.And(p => (p.QCRecord_QCInfo_ID == iqcinfoID));
                    }
                    if (iQCRecord_QCRetest_ID > 0)
                    {
                        expr = expr.And(p => (p.QCRecord_QCRetest_ID == iQCRecord_QCRetest_ID));
                    }
                    if (strTestItems_NAME != "")
                    {
                        int testItemsID = db.TestItems.First(d => (d.TestItems_NAME == strTestItems_NAME && d.Dictionary.Dictionary_Value == "启动")).TestItems_ID;
                        expr = expr.And(p => (p.TestItems.Tes_TestItems_ID == testItemsID));
                    }
                    expr = expr.And(p => (p.Dictionary.Dictionary_Value == "启动" && (p.QCRecord_RESULT != null && p.QCRecord_RESULT > 0)));

                    var c = (from p in db.QCRecord.Where(expr)
                             group p by new
                             {
                                 p.TestItems.TestItems_NAME,
                                 p.QCRecord_QCInfo_ID,
                                 p.QCRecord_QCRetest_ID
                             } into g
                             select new
                             {
                                 TestItemName = g.Key.TestItems_NAME,
                                 Count = g.Count(),
                                 Avg = g.Sum(p => p.QCRecord_RESULT)
                             }).ToList();

                    if (c != null)
                    {
                        QCRecordAVGEnitiy enitiy;
                        for (int i = 0; i < c.Count; i++)
                        {
                            enitiy = new QCRecordAVGEnitiy();
                            enitiy.TestItemName = c[i].TestItemName;
                            enitiy.Count = c[i].Count;
                            enitiy.Avg = (double)c[i].Avg.Value;
                            list.Add(enitiy);
                        }
                       
                    }

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
        /// 根据磅单号、状态、检测项目编号获取质检记录信息
        /// </summary>
        /// <param name="strWEIGHTTICKETNO">磅单号</param>
        /// <param name="strState">状态</param>
        /// <param name="iTestItemID">检测项目编号</param>
        /// <returns></returns>
        public static List<QCRecord> GetListQCRecord(string strWEIGHTTICKETNO, string strState, int iTestItemID)
        {
            List<QCRecord> list = new List<QCRecord>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    list = (from c in db.QCRecord
                            where c.QCInfo.DRAW_EXAM_INTERFACE.WEIGHT_TICKET_NO == strWEIGHTTICKETNO && c.Dictionary.Dictionary_Name == strState && c.QCRecord_TestItems_ID == iTestItemID
                            select c).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("根据磅单号、状态、检测项目编号获取质检记录信息（QCRecord）:" + ex.Message);
                }
                finally { db.Connection.Close(); }
            }
            return list;
        }


        //查所有信息
        public static List<QCRecord> GetListQCRecord(string strWEIGHTTICKETNO, string strState, string TestItemName)
        {
            List<QCRecord> list = new List<QCRecord>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    list = (from c in db.QCRecord
                            where c.QCInfo.DRAW_EXAM_INTERFACE.WEIGHT_TICKET_NO == strWEIGHTTICKETNO && c.Dictionary.Dictionary_Name == strState && c.TestItems.TestItems_NAME == TestItemName
                            select c).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("查所有信息（QCInfo）:" + ex.Message);
                }
                finally
                {
                    db.Connection.Close();
                }

            }
            return list;
        }

        /// <summary>
        /// 获取质检记录数据
        /// </summary>
        /// <param name="rowCount">总行数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">查询页数</param>
        /// <param name="expr">查询条件 Expression<Func<View_QCRecordInfo, bool>></param>
        /// <returns></returns>
        public static List<View_QCRecordInfo> GetListView_QCRecordInfo(ref int rowCount, int pageSize, int pageIndex, Expression<Func<View_QCRecordInfo, bool>> expr)
        {

            List<View_QCRecordInfo> list = new List<View_QCRecordInfo>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {

                    int pageCount = 0;


                    var hs = from h in db.View_QCRecordInfo.Where(expr) orderby h.TestItems_NAME, h.QCRecord_ID descending select h;//延迟加载,数据库没有任何操作
                    if (pageIndex == 1)//如果是第一次取数据,需要获取符合条件的总记录条数
                    {
                        rowCount = hs.Count();//数据库进行一次Count操作
                    }
                    //else//之后的记录条数,从分页控件持久态的属性中获取,省去一次Count查询
                    //{
                    //    rowCount = rowCount;
                    //}
                    pageCount = rowCount > pageSize ? Convert.ToInt32((rowCount - 1) / pageSize) + 1 : 1;//通用分页算法
                    if (pageIndex > pageCount)
                    {
                        pageIndex = pageCount;
                    }

                    list = hs.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();   //这里也是延迟加载,数据库此时不操作
                    // list =((from h in db.View_QCRecordInfo.Where(expr) orderby h.TestItems_NAME,h.QCRecord_ID descending select  h ).Skip(pageSize * (pageIndex - 1)).Take(pageSize) ).ToList();//延迟加载,数据库没有任何操作

                }
                catch (Exception ex)
                {
                    throw new Exception(" 获取质检记录数据（View_QCRecordInfo）:" + ex.Message);
                }
                finally
                {
                    db.Connection.Close();
                }

            }
            return list;
        }

        //public IQueryable<QCRecord> GetQCRecord(out int count, int pageSize, int pageIndex)
        //{
        //    count = db.Customer.Count();
        //    int startIndex = pageSize * (pageIndex - 1);
        //    //动态生成查询表达式
        //    if (startIndex <= 0)
        //    {
        //        return db.Customer.OrderBy(customer => customer.CustomerID)
        //           .Take(pageSize);
        //    }
        //    else
        //    {
        //        return db.Customer.OrderBy(customer => customer.CustomerID)
        //            .Skip().Take(pageSize);
        //        //返回查询的表达式目录树
        //    }

        //}

        /// <summary>
        /// 获取当前质检信息
        /// </summary>
        /// <param name="qcinfoId">质检编号</param>
        /// <param name="iQCRecord_QCRetest_ID">复测编号</param>
        /// <param name="itemName">质检信息</param>
        /// <param name="Dictionary_Name">状态</param>
        /// <returns>List<View_QCRecordInfo> </returns>
        public static List<View_QCRecordInfo> GetListQCRecordWate(
            int qcinfoId, 
            int iQCRecord_QCRetest_ID, 
            string itemName, 
            string Dictionary_Name,
            string Dictionary_Name2)
        {
            List<View_QCRecordInfo> list = new List<View_QCRecordInfo>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    int iTestItemID = TestItemsDAL.GetTestItemId(itemName);
                    if (qcinfoId > 0 && iQCRecord_QCRetest_ID > 0)
                    {
                        list = (from c in db.View_QCRecordInfo
                                where c.QCInfo_ID == qcinfoId && c.QCRecord_QCRetest_ID == iQCRecord_QCRetest_ID && (c.Dictionary_Value == Dictionary_Name || c.Dictionary_Value == Dictionary_Name2) && c.Tes_TestItems_ID == iTestItemID
                                orderby c.QCRecord_QCCOUNT
                                select c).ToList();
                    } 
                    else
                    {
                        list = (from c in db.View_QCRecordInfo
                                where c.QCInfo_ID == qcinfoId && c.Tes_TestItems_ID == iTestItemID && (c.Dictionary_Name == Dictionary_Name || c.Dictionary_Name == Dictionary_Name2)
                                orderby c.QCRecord_QCCOUNT
                                select c).ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("获取当前质检信息（View_QCRecordInfo）:" + ex.Message);
                }
                finally { db.Connection.Close(); }
            }
            return list;
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_QCRecordInfo> Query(Expression<Func<View_QCRecordInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_QCRecordInfo.Where(fun).ToList();
            }
        }

        /// <summary>
        /// 修改LED显示的最后一条数据
        /// 说明：在质检完成时将质检数据最后一条有效数据更改为LED未显示
        /// </summary>
        /// <param name="iqcinfoId">质检编号</param>
        /// <param name="iTes_TestItems_ID">质检类型（水分检测、重量检测）</param>
        /// <returns></returns>
        public static bool UpdateQcRecordQCInfoID(int iqcinfoId, int iTes_TestItems_ID)
        {
            bool rbool = false;
            int iqcRecordID = 0;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    iqcRecordID = db.QCRecord.Where(c => c.QCRecord_QCInfo_ID == iqcinfoId && c.Dictionary.Dictionary_Name == "启动" && c.TestItems.Tes_TestItems_ID == iTes_TestItems_ID).Max(p => p.QCRecord_ID);
                    QCRecord qcInfo = db.QCRecord.First(c => c.QCRecord_ID == iqcRecordID);
                   // qcInfo.QCRecord_ISLEDSHOW = false;
                    rbool = UpdateOneQcRecord(qcInfo);
                    //db.SubmitChanges();
                    // rbool = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("获取质检记录的最大的编号:" + ex.Message);
                }
                finally { db.Connection.Close(); }
            }
            return rbool;

        }


        /// <summary>
        /// 按条件查询信息View_QCRecordInfo
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static List<QCRecord> Query(Expression<Func<QCRecord, bool>> expr)
        {
            List<QCRecord> list = new List<QCRecord>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {

                try
                {
                    list = (from h in db.QCRecord.Where(expr) select h).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("按条件查询信息(View_QCRecordInfo) QCRecordDAL.Query异常:" + ex.Message);
                }
                finally
                {
                    db.Connection.Close();
                }
            }
            return list;

        }

        /// LINQ更新方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="fun"></param>
        /// <param name="tentity"></param>
        /// <param name="action"></param>
        public static bool Update(Expression<Func<QCRecord, bool>> fun, Action<QCRecord> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.QCRecord.Where(fun).ToList();
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
        /// 按条件删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="tentitys"></param>
        /// <param name="fun"></param>
        public static bool DeleteToMany(Expression<Func<QCRecord, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<QCRecord>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }
        /// <summary>
        /// 根据质检编号获取质检实体
        /// </summary>
        /// <param name="iqcinfoID">质检编号</param>
        /// <returns>QCInfo</returns>
        public static View_QCRecordInfo GetViewQCRecordInfo(int iqcinfoID)
        {
            View_QCRecordInfo qcInfo = null;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    qcInfo = db.View_QCRecordInfo.First(c => c.QCInfo_ID == iqcinfoID);

                }
                catch (Exception ex)
                {
                    throw new Exception("根据质检编号获取质检实体（QCInfo）:" + ex.Message);

                }
                finally
                {
                    db.Connection.Close();
                }

            }
            return qcInfo;
        }
     

    }
}
