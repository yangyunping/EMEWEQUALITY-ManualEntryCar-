using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public class UserInfoDAL
    {
        public static IEnumerable<UserInfo> GetItemsForListing(string strsql)
        {
            DCQUALITYDataContext db = new DCQUALITYDataContext();

            var products = db.ExecuteQuery<UserInfo>(strsql).AsEnumerable();
            return products;

        }
     
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_UserInfo_D_R_d> Query()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_UserInfo_D_R_d.ToList();
            }
        }
        /// <summary>
        /// summary>
        /// 按条件查询信息View_UserInfo_D_R_d
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_UserInfo_D_R_d> Query(Expression<Func<View_UserInfo_D_R_d, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_UserInfo_D_R_d.Where(fun).ToList();
            }

        }

        public static IEnumerable<UserInfo> Query(Expression<Func<UserInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.UserInfo.Where(fun).ToList();
            }

        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static UserInfo Single(Expression<Func<UserInfo, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.UserInfo.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static View_UserInfo_D_R_d Single(Expression<Func<View_UserInfo_D_R_d, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_UserInfo_D_R_d.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">用户实体</param>
        ///    /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(UserInfo qcRecord, out int rint)
        {
            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.UserInfo.InsertOnSubmit(qcRecord);
                    db.SubmitChanges();
                    rint = qcRecord.UserId;
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
        public static bool Update(Expression<Func<UserInfo, bool>> fun, Action<UserInfo> action)
        {

            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.UserInfo.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<UserInfo, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<UserInfo>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;
        }

    }
}
