using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public  class RoleInfoDAL
    {
        //查询所有信息
        public static IEnumerable<RoleInfo> Query()
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.RoleInfo.ToList();
            }
        }
        /// <summary>
        /// 按条件查询 RoleInfo
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static IEnumerable<RoleInfo> Query(Expression<Func<RoleInfo,bool>>fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.RoleInfo.Where(fun).ToList();
            }
        }
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">QCRecord质检实体</param>
        /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneRoleInfo(RoleInfo qcRecord, out int rint)
        {
            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.RoleInfo.InsertOnSubmit(qcRecord);
                    db.SubmitChanges();
                    rint = qcRecord.Role_Id;
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
        /// 新增一条记录
        /// </summary>
        /// <param name="qcRecord">对象</param>
        /// <returns></returns>
        public static bool InsertOneRoleInfo(RoleInfo qcRecord)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.RoleInfo.InsertOnSubmit(qcRecord);
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
        /// 根据ID删除数据（单条）
        /// </summary>
        /// <param name="RoleInfoid">角色编号</param>
        /// <returns></returns>
        public static bool DeleteToMany(int RoleInfoid)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    var dty = db.RoleInfo.FirstOrDefault(p => p.Role_Id == RoleInfoid);
                    db.RoleInfo.DeleteOnSubmit(dty);
                    db.SubmitChanges();
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
        /// 按条件删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="tentitys"></param>
        /// <param name="fun"></param>
        public static bool DeleteToMany(Expression<Func<RoleInfo, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<RoleInfo>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;


        }
       /// <summary>
       /// 更新
       /// </summary>
        public static bool UpdateOneRoleInfo(Expression<Func<RoleInfo, bool>> fun, Action<RoleInfo> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.RoleInfo.Where(fun).ToList();
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
    
    }
}
