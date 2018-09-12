using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public  class PermissionsInfoDAL
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PermissionsInfo> Query()
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.PermissionsInfo.ToList();
            }
        }
        /// <summary>
        /// 查询所有信息View_MenuInfoRole
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_MenuInfoRole> QueryView_MenuInfoRole()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_MenuInfoRole.ToList();
            }

        }
        /// <summary>
        /// 查询所有信息View_Menu_ControlRole
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_Menu_ControlRole> QueryView_Menu_ControlRole()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_Menu_ControlRole.ToList();
            }

        }
        /// <summary>
        /// summary>
        /// 按条件查询信息PermissionsInfo
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PermissionsInfo> Query(Expression<Func<PermissionsInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.PermissionsInfo.Where(fun).ToList();
            }

        }

        /// <summary>
        /// summary>
        /// 按条件查询信息View_MenuInfoRole
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_MenuInfoRole> Query(Expression<Func<View_MenuInfoRole, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_MenuInfoRole.Where(fun).ToList();
            }
        }
        /// <summary>
        /// summary>
        /// 按条件查询信息View_Menu_ControlRole
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_Menu_ControlRole> QueryView_Menu_ControlRole(Expression<Func<View_Menu_ControlRole, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_Menu_ControlRole.Where(fun).ToList();
            }

        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static PermissionsInfo Single(Expression<Func<PermissionsInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.PermissionsInfo.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">质检实体</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(PermissionsInfo qcRecord)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.PermissionsInfo.InsertOnSubmit(qcRecord);
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
        public static bool Update(Expression<Func<PermissionsInfo, bool>> fun, Action<PermissionsInfo> action)
        {

            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.PermissionsInfo.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<PermissionsInfo, bool>> fun)
        {
            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<PermissionsInfo>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;



        }
        
    }
}
