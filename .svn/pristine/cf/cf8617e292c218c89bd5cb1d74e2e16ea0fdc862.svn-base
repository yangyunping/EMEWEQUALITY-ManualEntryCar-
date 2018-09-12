using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Data.Linq;
using System.Linq.Expressions;

namespace EMEWEDAL
{
   public class CollectionDAL
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
       public static IEnumerable<CollectionInfo> Query()
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.CollectionInfo.ToList();
            }

        }
        /// <summary>
        /// summary>
        /// 按条件查询信息CollectionInfo
        /// </summary>
        /// <returns></returns>
       public static IEnumerable<CollectionInfo> Query(Expression<Func<CollectionInfo, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.CollectionInfo.Where(fun).ToList();
            }

        }
        /// <summary>
        /// summary>
        /// </summary>
        /// <returns></returns>
       public static IEnumerable<View_Collection> Query(Expression<Func<View_Collection, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_Collection.Where(fun).ToList();
            }

        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static CollectionInfo Single(Expression<Func<CollectionInfo, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.CollectionInfo.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="qcRecord">质检实体</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(CollectionInfo qcRecord)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.CollectionInfo.InsertOnSubmit(qcRecord);
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
        public static bool Update(Expression<Func<CollectionInfo, bool>> fun, Action<CollectionInfo> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.CollectionInfo.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<CollectionInfo, bool>> fun)
        {
            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<CollectionInfo>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;
        }
    }
}
