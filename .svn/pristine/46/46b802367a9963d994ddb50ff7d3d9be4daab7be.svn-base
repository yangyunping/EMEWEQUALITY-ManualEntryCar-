using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using EMEWEEntity;
using System.Linq.Expressions;

namespace EMEWEDAL
{
    public class WaterTestConfigureSetDAL
    {

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<WaterTestConfigureSet> Query()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.WaterTestConfigureSet.ToList();
            }

        }
        /// <summary>
        /// summary>
        /// 按条件查询信息WaterTestConfigureSet
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<WaterTestConfigureSet> Query(Expression<Func<WaterTestConfigureSet, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.WaterTestConfigureSet.Where(fun).ToList();
            }

        }
        /// <summary>
        /// summary>
        /// 按条件查询信息View_Beonduty_D_Q
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_Beonduty_D_Q> Query(Expression<Func<View_Beonduty_D_Q, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_Beonduty_D_Q.Where(fun).ToList();
            }

        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static WaterTestConfigureSet Single(Expression<Func<WaterTestConfigureSet, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.WaterTestConfigureSet.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="qcRecord">质检实体</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(WaterTestConfigureSet qcRecord)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.WaterTestConfigureSet.InsertOnSubmit(qcRecord);
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
        public static bool Update(Expression<Func<WaterTestConfigureSet, bool>> fun, Action<WaterTestConfigureSet> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.WaterTestConfigureSet.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<WaterTestConfigureSet, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<WaterTestConfigureSet>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;
        }
    }
}




