using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;

namespace EMEWEDAL
{

    public class LinQBaseDao
    {
        public static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EMEWEQCConnectionString"].ToString();


        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public static DateTime getDate()
        {
            return Convert.ToDateTime(GetSingle("select getdate() "));

        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {

                        throw e;
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 返回DataSet类型的方法,使用存储过程,有参数
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
        {
            using (SqlConnection objSqlConnectionB = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objSqlConnectionB;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                cmd.CommandTimeout = 1000000000;
                if (cmdParameters != null)
                {
                    foreach (SqlParameter parm in cmdParameters)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);

                cmd.Parameters.Clear();
                return ds;
            }
        }


        /// <summary>
        /// 查询所有的记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(DataContext dc) where T : class
        {

            return dc.GetTable<T>().AsEnumerable<T>().ToList();
        }
        /// <summary>
        /// 按条件查询记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(DataContext dc, Expression<Func<T, bool>> fun) where T : class
        {
            return dc.GetTable<T>().Where<T>(fun).AsEnumerable<T>().ToList();

        }
    
        public static IEnumerable<T> Query<T>(DataContext dc, Action<T> tentity) where T : class
        {
            return dc.GetTable<T>().AsEnumerable<T>();
        }
        /// <summary>
        /// 查询单条 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static T Single<T>(DataContext dc, Expression<Func<T, bool>> fun) where T : class
        {
            return dc.GetTable<T>().Single<T>(fun);
        }

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="tentity"></param>
        public static bool InsertOne<T>(DataContext dc, T tentity) where T : class
        {
            bool rbool = true;
            try
            {
                var table = dc.GetTable<T>();
                table.InsertOnSubmit(tentity);
                dc.SubmitChanges();
                return rbool;
            }
            catch (Exception ex)
            {
                throw new Exception("添加一条记录时异常:" + ex.Message);

            }

        }

        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="tentitys"></param>
        public static void InsertToMany<T>(DataContext dc, IEnumerable<T> tentitys) where T : class
        {
            var table = dc.GetTable<T>();
            table.InsertAllOnSubmit(tentitys);
            dc.SubmitChanges();
        }


        /// <summary>
        /// 按条件删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="tentitys"></param>
        /// <param name="fun"></param>
        public static void DeleteToMany<T>(DataContext dc, Expression<Func<T, bool>> fun) where T : class
        {
            var table = dc.GetTable<T>();
            var result = table.Where<T>(fun).AsEnumerable<T>();
            table.DeleteAllOnSubmit<T>(result);
            dc.SubmitChanges();
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="tentitys"></param>
        public static void DeleteToManyByCondition<T>(DataContext dc, IEnumerable<T> tentitys) where T : class
        {
            var table = dc.GetTable<T>();
            table.DeleteAllOnSubmit<T>(tentitys);
            dc.SubmitChanges();
        }

        /// <summary>
        /// LINQ更新方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <param name="fun"></param>
        /// <param name="tentity"></param>
        /// <param name="action"></param>
        public static void Update<T>(DataContext dc, Expression<Func<T, bool>> fun, Action<T> action) where T : class
        {
            var table = dc.GetTable<T>().Single<T>(fun);
            //var table = dc.GetTable<T>().Where<T>(fun).Single<T>();
            action(table);
            dc.SubmitChanges();
        }
        /// <summary>
        /// 传入SQL 查询
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetItemsForListing<T>(string strsql) where T : class
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.ExecuteQuery<T>(strsql).ToList();
            }


        }

        public static List<T> ListT<T>(DataContext dc, Expression<Func<T, bool>> fun) where T : class
        {
            return dc.GetTable<T>().Where<T>(fun).AsEnumerable<T>().ToList();

        }

        #region  排序分页结果集

        /// <summary>
        /// 排序IQueryable<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="source">结果集</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDirection">排序方式</param>
        /// <returns>IQueryable<T></returns>
        public static IQueryable<T> DataSorting<T>(IQueryable<T> source, string sortExpression, string sortDirection)
        {
            string sortingDir = string.Empty;
            if (sortDirection.ToUpper().Trim() == "ASC")
                sortingDir = "OrderBy";
            else if (sortDirection.ToUpper().Trim() == "DESC")
                sortingDir = "OrderByDescending";
            ParameterExpression param = Expression.Parameter(typeof(T), sortExpression);
            PropertyInfo pi = typeof(T).GetProperty(sortExpression);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sortExpression), param));
            IQueryable<T> query = source.AsQueryable().Provider.CreateQuery<T>(expr);
            return query;
        }
        /// <summary>
        /// 分页IQueryable<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="source">结果集</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>IQueryable<T></returns>
        public static IQueryable<T> DataPaging<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            return source.Skip(pageNumber * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 排序分页返回IQueryable<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="source">结果集IQueryable<T></param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDirection">排序方式</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public static IQueryable<T> SortingAndPaging<T>(IQueryable<T> source, string sortExpression, string sortDirection, int pageNumber, int pageSize)
        {
            IQueryable<T> query = DataSorting<T>(source, sortExpression, sortDirection);
            return DataPaging(query, pageNumber, pageSize);
        }

        #endregion

        ///// <summary>
        /////检测连接是否可以打开
        ///// </summary>
        ///// <param name="conn">连接字符串</param>
        ///// <returns></returns>
        //public static bool DetectionConn()
        //{
        //    bool f = false;
        //    try
        //    {
        //        using (DCQUALITYDataContext db = new DCQUALITYDataContext())
        //        {
        //            db.Connection.Open();
        //            f = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        f = false;
        //    }
        //    return f;
        //}

        /// <summary>
        ///检测连接是否可以打开
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        public static bool DetectionConn(string conn)
        {
            bool f = false;
            try
            {
                using (DataContext dc = new DataContext(conn))
                {

                    dc.Connection.Open();
                    f = true; 
                    dc.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                f = false;
                // Util.WriteTextLog("DetectionConn():" + ex.ToString());
                //throw ex;
            }
            return f;
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static System.Data.DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter command = null;
                try
                {
                    connection.Open();
                    command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return ds;
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {

                        throw e;
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }
     
    }
}
