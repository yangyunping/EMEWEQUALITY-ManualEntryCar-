using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Data.Linq;
using EMEWE.Common;
using System.Linq.Expressions;
namespace EMEWEDAL
{
    public class DictionaryDAL
    {

        //DataContext dc = BaseDao.conStr();
        /// <summary>
        /// 增加字典信息
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static bool Insert(Dictionary dictionary)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.Dictionary.InsertOnSubmit(dictionary);
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    rbool = false;
                }
                finally { db.Connection.Close(); }
            }
            return rbool;
        }
        /// <summary>
        /// 新增一条质检记录
        /// </summary>
        /// <param name="qcRecord">质检实体</param>
        /// <returns></returns>
        public static bool InsertOneDictionary(Dictionary qcRecord)
        {
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.Dictionary.InsertOnSubmit(qcRecord);
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    rbool = false;
                }
                finally { db.Connection.Close(); }
            }
            return rbool; ;
        }
        /// <summary>
        /// 单项更改
        /// 检索到的一个Dictionary对象做出的更新保持回数据库
        /// </summary>
        /// <param name="dictionary">Dictionary实体</param>
        /// <returns></returns>
        public static bool Update(Dictionary dictionary)
        {
            bool rbool = true;
            try
            {
                using (DCQUALITYDataContext db = new DCQUALITYDataContext())
                {
                    Dictionary dic = db.Dictionary.First(c => c.Dictionary_ID == dictionary.Dictionary_ID);
                    dic.Dictionary_Name = dictionary.Dictionary_Name;
                    dic.Dictionary_Value = dictionary.Dictionary_Value;
                    dic.Dictionary_OtherID = dictionary.Dictionary_OtherID;
                    dic.Dictionary_State = dictionary.Dictionary_State;
                    dic.Dictionary_Remark = dictionary.Dictionary_Remark;
                    db.SubmitChanges();

                }
            }
            catch (Exception ex)
            {
                rbool = false;
            }
            return rbool;
        }
        /// <summary>
        /// 多项更改
        /// 使用SubmitChanges将对检索到的进行的更新保持回数据库
        /// </summary>
        /// <param name="dictionary">Dictionary实体对象</param>
        /// <returns></returns>
        public static bool UpdateMore(Dictionary dictionary)
        {
            bool rbool = true;

            try
            {
                using (DCQUALITYDataContext db = new DCQUALITYDataContext())
                {

                    var q = from p in db.Dictionary
                            where p.Dictionary_ID == dictionary.Dictionary_ID
                            select p;
                    foreach (var p in q)
                    {
                        p.Dictionary_Name = dictionary.Dictionary_Name;
                        p.Dictionary_OtherID = dictionary.Dictionary_OtherID;
                        p.Dictionary_Remark = dictionary.Dictionary_Remark;
                        p.Dictionary_State = dictionary.Dictionary_State;
                        p.Dictionary_Value = dictionary.Dictionary_Value;
                    }
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                rbool = false;
            }
            return rbool;

        }



        public static List<Dictionary> GetListDictionary()
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.Dictionary.ToList();
            }
        }

        /// <summary>
        /// 根据启动状态和字典值获取字典信息
        /// </summary>
        /// <param name="strValue">字典值</param>
        /// <returns></returns>
        public static List<Dictionary> GetValueStateDictionary(string strValue)
        {
            List<Dictionary> list = new List<Dictionary>();
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    int itemid = db.Dictionary.First(d => (d.Dictionary_Value == strValue && d.Dictionary_State == true)).Dictionary_ID;

                    var v = (from c in db.Dictionary
                             where (c.Dictionary_ID == itemid || c.Dictionary_OtherID.Value == itemid) && c.Dictionary_State == true
                             select c.Dictionary_ID).ToArray();
                    list = (from c in db.Dictionary
                            where c.Dictionary_State == true && v.Contains(c.Dictionary_OtherID.Value)
                            select c).ToList();
                    Dictionary dic = new Dictionary();
                    dic.Dictionary_ID = 0;
                    dic.Dictionary_Name = "全部";
                    list.Add(dic);
                }
                catch (Exception ex)
                {
                }
                finally { db.Connection.Close(); }
            }
            return list;

        }

        /// <summary>
        /// 所属列表信息
        /// 根据启动状态和有下级标识获取字典信息
        /// </summary>
        /// <returns></returns>
        public static List<Dictionary> GetStateDictionary()
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                var objTable = from c in db.Dictionary
                               where c.Dictionary_State == true && c.Dictionary_ISLower == true
                               select c;
                return objTable.ToList();
            }
        }
        /// <summary>
        /// 根据字典名称获取字典编号
        /// </summary>
        /// <param name="strName">字典值</param>
        /// <returns></returns>
        public static int GetDictionaryID(string strName)
        {
            int rint = 0;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                rint = db.Dictionary.First(d => (d.Dictionary_Value == strName && d.Dictionary_State == true)).Dictionary_ID;
            }
            return rint;

        }
        /// <summary>
        /// summary>
        /// 按条件查询信息Dictionary
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Dictionary> Query(Expression<Func<Dictionary, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.Dictionary.Where(fun).ToList();
            }
        }
        /// <summary>
        /// summary>
        /// 按条件查询信息Dictionary
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Dictionary> Query()
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.Dictionary.ToList();
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
        public static bool UpdateDictionary(Expression<Func<Dictionary, bool>> fun, Action<Dictionary> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.Dictionary.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<Dictionary, bool>> fun)
        {
            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<Dictionary>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }
    }
}
