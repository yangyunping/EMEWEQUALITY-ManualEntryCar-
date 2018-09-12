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
    public class UserInfo2DAL
    {
        public static IEnumerable<UserInfo2> GetItemsForListing(string strsql)
        {
            DCQUALITYDataContext db = new DCQUALITYDataContext();

            var products = db.ExecuteQuery<UserInfo2>(strsql).AsEnumerable();
            return products;

        }
        public static List<UserInfo2> GetListWhereUserInfoName(string strDutyName, string strSate)
        {
            List<UserInfo2> list = new List<UserInfo2>();
          
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    if (!string.IsNullOrEmpty(strDutyName))
                    {
                        list = (from c in db.UserInfo2
                                 where (c.DutyInfo.Duty_Name == strDutyName && c.Dictionary.Dictionary_Name == strSate)
                                select c ).ToList();
                    }
                    else
                    {
                        list = (from c in db.UserInfo2
                                where ( c.Dictionary.Dictionary_Name == strSate)
                                select c).ToList(); // select c.UserName
                    }
                    UserInfo2 u = new UserInfo2();
                    u.UserName = "--请选择--";
                    list.Add(u);
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
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<UserInfo2> Query()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.UserInfo2.ToList();
            }
        }
        /// <summary>
        /// summary>
        /// 按条件查询信息UserInfo2
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<UserInfo2> Query(Expression<Func<UserInfo2, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.UserInfo2.Where(fun).ToList();
            }
        }
        /// <summary>
        /// summary>
        /// 按条件查询信息View_UserInfo2_D_R_d
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_UserInfo2_D_R_d> Query(Expression<Func<View_UserInfo2_D_R_d, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_UserInfo2_D_R_d.Where(fun).ToList();
            }
        }


        /// <summary>
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">用户实体</param>
        ///    /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(UserInfo2 qcRecord, out int rint)
        {
            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.UserInfo2.InsertOnSubmit(qcRecord);
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
        public static bool Update(Expression<Func<UserInfo2, bool>> fun, Action<UserInfo2> action)
        {

            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.UserInfo2.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<UserInfo2, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<UserInfo2>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;
        }

    }
}
