using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public class SMSConfigureDAL
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SMSConfigure> Query()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.SMSConfigure.ToList();
            }
        }
        /// <summary>
        /// summary>
        /// 按条件查询信息Unusual
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SMSConfigure> Query(Expression<Func<SMSConfigure, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.SMSConfigure.Where(fun).ToList();
            }

        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static SMSConfigure Single(Expression<Func<SMSConfigure, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.SMSConfigure.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">质检实体</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(SMSConfigure qcRecord)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.SMSConfigure.InsertOnSubmit(qcRecord);
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
        public static bool Update(Expression<Func<SMSConfigure, bool>> fun, Action<SMSConfigure> action)
        {

            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.SMSConfigure.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<SMSConfigure, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<SMSConfigure>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;
        }
    }
}
