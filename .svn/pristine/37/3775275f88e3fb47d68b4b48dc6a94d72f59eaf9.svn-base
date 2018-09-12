using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Data.Linq;
using System.Linq.Expressions;

namespace EMEWEDAL
{
   public class TestItemsDAL
    {
       /// <summary>
       /// 根据项目名称和是否启动状态
       /// </summary>
       /// <param name="strTestItemName">项目名称</param>
       /// <returns></returns>
       public static List<TestItems> GetListWhereTestItemName(string strTestItemName,string strSate)
       {
           List<TestItems> list = new List<TestItems>();
           using (DCQUALITYDataContext db = new DCQUALITYDataContext())
           {
               try
               {
                   if (!string.IsNullOrEmpty(strTestItemName))
                   {
                       int itemid = db.TestItems.First(d => (d.TestItems_NAME == strTestItemName && d.Dictionary.Dictionary_Name == strSate)).TestItems_ID;

                       var v = (from c in db.TestItems
                                where (c.TestItems_ID == itemid || c.Tes_TestItems_ID.Value == itemid) && c.Dictionary.Dictionary_Name == strSate
                                select c.TestItems_ID).ToArray();
                       list = (from c in db.TestItems
                               where c.Dictionary.Dictionary_Name == strSate && (v.Contains(c.TestItems_ID) || v.Contains(c.Tes_TestItems_ID.Value) && (c.TestItems_COUNT.Value != 0 || c.TestItems_COUNT.Value != null))
                               select c).ToList();
                   }
                   else {
                       list = (from c in db.TestItems
                               where c.Dictionary.Dictionary_Name == strSate  
                               select c).ToList();
                   }
               }
               catch (Exception ex)
               {
                   throw new Exception(string.Format("根据项目名称和是否启动状态：{0}", ex.Message));
               }
               finally { db.Connection.Close(); }

           }
           return list;
       }
       
       /// <summary>
       /// 获取项目编号
       /// </summary>
       /// <param name="strTestItemName">项目名称</param>
       /// <returns></returns>
       public static int GetTestItemId(string strTestItemName)
       {

           int rint = 0;
           using (DCQUALITYDataContext db = new DCQUALITYDataContext()) {
               try
               {
                   rint = db.TestItems.First(d => (d.TestItems_NAME == strTestItemName && d.Dictionary.Dictionary_Name == "启动")).TestItems_ID;
               }
               catch (Exception ex)
               {
                   throw new Exception(string.Format("获取项目编号：{0}", ex.Message));
               }
               finally { db.Connection.Close(); }
           
           }
           return rint;
       }
       
       /// <summary>
       /// 连接Linq To Sql 实例
       /// </summary>
       /// <param name="strsql"></param>
       /// <returns></returns>
       public static IEnumerable<TestItems> GetItemsForListing(string strsql)
       {
           DCQUALITYDataContext db = new DCQUALITYDataContext();

           var products = db.ExecuteQuery<TestItems>(strsql).AsEnumerable();
           return products;

       }

       /// <summary>
       /// 查询所有信息预置皮重信息
       /// </summary>
       /// <returns></returns>
       public static IEnumerable<View_TestItems_Dictionary> Query()
       {

           using (DCQUALITYDataContext db = new DCQUALITYDataContext())
           {
               return db.View_TestItems_Dictionary.ToList();
           }

       }

       /// <summary>
       /// summary>
       /// 按条件查询信息View_TestItems_Dictionary
       /// </summary>
       /// <returns></returns>
       public static IEnumerable<View_TestItems_Dictionary> Query(Expression<Func<View_TestItems_Dictionary, bool>> fun)
       {
           using (DCQUALITYDataContext db = new DCQUALITYDataContext())
           {
               return db.View_TestItems_Dictionary.Where(fun).ToList();
           }
       }
       public static IEnumerable<TestItems> Query(Expression<Func<TestItems, bool>> fun)
       {
           using (DCQUALITYDataContext db = new DCQUALITYDataContext())
           {
               return db.TestItems.Where(fun).ToList();
           }
       }
       /// <summary>
       /// 查询单条 返回实体
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="fun"></param>
       /// <returns></returns>
       public static View_TestItems_Dictionary Single(Expression<Func<View_TestItems_Dictionary, bool>> fun)
       {
           using (DCQUALITYDataContext db = new DCQUALITYDataContext())
           {
               return db.View_TestItems_Dictionary.SingleOrDefault(fun);
           }
       }

       /// <summary>
       /// 查询单条 返回实体
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="fun"></param>
       /// <returns></returns>
       public static TestItems Single(Expression<Func<TestItems, bool>> fun)
       {
           using (DCQUALITYDataContext db = new DCQUALITYDataContext())
           {
               return db.TestItems.SingleOrDefault(fun);
           }
       }
       /// <summary>
       /// 新增一条质检记录
       /// </summary>
       /// <param name="qcRecord">质检实体</param>
       /// <returns></returns>
       public static bool InsertOneQCRecord(TestItems qcRecord)
       {
           bool rbool = true;
           using (DCQUALITYDataContext db = new DCQUALITYDataContext())
           {
               try
               {
                   db.TestItems.InsertOnSubmit(qcRecord);
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
       /// LINQ更新方法
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="dc"></param>
       /// <param name="fun"></param>
       /// <param name="tentity"></param>
       /// <param name="action"></param>
       public static bool Update(Expression<Func<TestItems, bool>> fun, Action<TestItems> action)
       {
           bool rbool = true;
           DCQUALITYDataContext dc = new DCQUALITYDataContext();
           try
           {
               var table = dc.TestItems.Where(fun).ToList();
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
       public static bool DeleteToMany(Expression<Func<TestItems, bool>> fun)
       {

           bool rbool = true;
           try
           {
               LinQBaseDao.DeleteToMany<TestItems>(new DCQUALITYDataContext(), fun);
           }
           catch
           {
               rbool = false;
           }
           return rbool;

       }

    }
}
