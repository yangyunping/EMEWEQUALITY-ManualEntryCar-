using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using EMEWE.Common;
using System.Linq.Expressions;
using System.Data.Linq;
namespace EMEWEDAL
{
   public  class LogInfoDAL
    {
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
       public static IEnumerable<LogInfo> Query()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.LogInfo.ToList();
            }
        }
        /// <summary>
        /// summary>
       /// 按条件查询信息LogInfo
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<LogInfo> Query(Expression<Func<LogInfo, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.LogInfo.Where(fun).ToList();
            }
        }

        /// <summary>
        /// 查询所有信息View_LogInfo_Dictionary
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_LogInfo_Dictionary> QueryView_LogInfo_Dictionary()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_LogInfo_Dictionary.ToList();
            }

        }
        /// <summary>
        /// summary>
        /// 按条件查询信息View_LogInfo_Dictionary
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_LogInfo_Dictionary> QueryView_LogInfo_Dictionary(Expression<Func<View_LogInfo_Dictionary, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_LogInfo_Dictionary.Where(fun).ToList();
            }

        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static LogInfo Single(Expression<Func<LogInfo, bool>> fun)
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.LogInfo.SingleOrDefault(fun);
            }
        }
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="qcRecord">实体</param>
        /// <returns></returns>
        public static bool InsertOneQCRecord(LogInfo qcRecord)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.LogInfo.InsertOnSubmit(qcRecord);
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
        public static bool Update(Expression<Func<LogInfo, bool>> fun, Action<LogInfo> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.LogInfo.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<LogInfo, bool>> fun)
        {
            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<LogInfo>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }

     

       /// <summary>
        ///  添加日记
       /// </summary>
        /// <param name="Log_Dictionary_Name">日记类型</param>
        /// <param name="Log_Content">操作内容</param>
       /// <param name="UserNmae">操作人</param>
       /// <returns></returns>
        public static bool loginfoadd(string Log_Dictionary_Name, string Log_Content,string UserNmae)
        {
            bool rbool=true;
            try
            {
                var varloginfo = new LogInfo()
                {
                    Log_Time = DateTime.Now,
                    Log_Name = UserNmae,
                    Log_Dictionary_ID = DictionaryDAL.GetDictionaryID(Log_Dictionary_Name),//暂时用11
                    Log_Content = Log_Content

                };
                rbool = LogInfoDAL.InsertOneQCRecord(varloginfo);
             
            }
            catch 
            {
                rbool = false;
            }
            return rbool;
        }
      
    }
}
