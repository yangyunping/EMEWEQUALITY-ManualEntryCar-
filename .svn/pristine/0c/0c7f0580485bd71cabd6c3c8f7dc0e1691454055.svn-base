using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public class MenuInfoDAL
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MenuInfo> Query()
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.MenuInfo.ToList();
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
        /// summary>
        /// 按条件查询信息QueryView_MenuInfoRole
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_MenuInfoRole> QueryView_MenuInfoRole(Expression<Func<View_MenuInfoRole, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_MenuInfoRole.Where(fun).ToList();
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
        /// summary>
        /// 按条件查询信息MenuInfo
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MenuInfo> Query(Expression<Func<MenuInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.MenuInfo.Where(fun).ToList();
            }

        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static MenuInfo Single(Expression<Func<MenuInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.MenuInfo.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">质检实体</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(MenuInfo qcRecord)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.MenuInfo.InsertOnSubmit(qcRecord);
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
        public static bool Update(Expression<Func<MenuInfo, bool>> fun, Action<MenuInfo> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.MenuInfo.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<MenuInfo, bool>> fun)
        {
            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<MenuInfo>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }
        /// <summary>
        /// 根据菜单名称 返回菜单ID
        /// </summary>
        /// <param name="strValue">菜单名称</param>
        /// <returns></returns>
        public static int GetMenuID(string strValue)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    return db.MenuInfo.First(d => (d.Menu_ControlText == strValue)).Menu_ID;


                }
                catch (Exception ex)
                {
                    return 0;

                }
                finally { db.Connection.Close(); }


            }

        }
        /// <summary>
        /// 根据菜单ID返回菜单名称 
        /// </summary>
        /// <param name="strValue">菜单ID</param>
        /// <returns></returns>
        public static string GetValueState(int MenuID)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    return db.MenuInfo.First(d => (d.Menu_ID == MenuID)).Menu_ControlText;


                }
                catch (Exception ex)
                {
                    return "";

                }
                finally { db.Connection.Close(); }


            }

        }

        /// <summary>
        /// 添加一条新的菜单信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static bool InsertMenuInfo(MenuInfo menu)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.MenuInfo.InsertOnSubmit(menu);
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
    }
}
