using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//导入需要的应用包
using EMEWEEntity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    /// <summary>
    /// 预置皮重表信息的DAL类
    /// </summary>
    public class PresetTareDAL
    {
        /// <summary>
        /// 连接Linq To Sql 实例
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public static IEnumerable<PresetTare> GetItemsForListing(string strsql)
        {
            DCQUALITYDataContext db = new DCQUALITYDataContext();

            var products = db.ExecuteQuery<PresetTare>(strsql).AsEnumerable();
            return products;

        }

        /// <summary>
        /// 查询所有信息预置皮重信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_PresetTare_Dictionary> Query()
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_PresetTare_Dictionary.ToList();
            }

        }

        /// <summary>
        /// summary>
        /// 按条件查询信息View_PresetTare_Dictionary
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_PresetTare_Dictionary> Query(Expression<Func<View_PresetTare_Dictionary, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_PresetTare_Dictionary.Where(fun).ToList();
            }

        }

        public static IEnumerable<PresetTare> Query(Expression<Func<PresetTare, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.PresetTare.Where(fun).ToList();
            }

        }

        public static List<PresetTare> GetListPresetTare(Expression<Func<PresetTare, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.PresetTare.Where(fun).ToList();
            }
        }

        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static PresetTare Single(Expression<Func<PresetTare, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.PresetTare.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">质检实体</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(PresetTare qcRecord)
        {
           bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.PresetTare.InsertOnSubmit(qcRecord);
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
        public static bool Update(Expression<Func<PresetTare, bool>> fun, Action<PresetTare> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.PresetTare.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<PresetTare, bool>> fun)
        {

            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<PresetTare>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }
    }
}
