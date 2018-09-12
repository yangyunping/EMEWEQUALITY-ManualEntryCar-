using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public class DRAW_EXAM_INTERFACEDAL
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DRAW_EXAM_INTERFACE> Query(Expression<Func<DRAW_EXAM_INTERFACE,bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.DRAW_EXAM_INTERFACE.Where(fun).ToList();
            }
        }
        /// <summary>
        /// LINQ更新方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="fun"></param>
        /// <param name="tentity"></param>
        /// <param name="action"></param>
        public static bool Update(Expression<Func<DRAW_EXAM_INTERFACE, bool>> fun, Action<DRAW_EXAM_INTERFACE> action) 
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.DRAW_EXAM_INTERFACE.Where(fun).ToList();
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

        /// </summary>
        /// 新增一条车辆质检记录
        /// </summary>
        /// <param name="qcRecord">QCRecord质检实体</param>
        /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneCarInfo(DRAW_EXAM_INTERFACE DRAW_E_INI, out int rint)
        {

            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.DRAW_EXAM_INTERFACE.InsertOnSubmit(DRAW_E_INI);
                    db.SubmitChanges();
                    rbool = true;
                    rint = DRAW_E_INI.DRAW_EXAM_INTERFACE_ID;
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
