using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Data.Linq;

namespace EMEWEDAL
{
    public class ClientDAL
    {    /// </summary>
        /// 新增一条客户端记录
        /// </summary>
        /// <param name="qcRecord">QCRecord质检实体</param>
        /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneClientInfo(ClientInfo clientInfo, out int rint)
        {

            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.ClientInfo.InsertOnSubmit(clientInfo);
                    db.SubmitChanges();
                    rbool = true;
                    rint = clientInfo.Client_ID;
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
        /// 是否存在客户端名称
        /// </summary>
        /// <param name="strName">客户端名称</param>
        /// <returns>存在true 不存在true</returns>
        public static bool ISClientInfoName(string strName)
        {

            bool rbool = false;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    ClientInfo cl = db.ClientInfo.First(c => c.Client_NAME == strName);
                    if (cl != null)
                    {
                        rbool = true;
                    }
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
        /// 根据质检记录编号获取质检记录实体
        /// </summary>
        /// <param name="iqcRecordID">客户端编号</param>
        /// <returns>ClientInfo</returns>
        public static ClientInfo GetClientInfo(int iClient_ID)
        {
            ClientInfo qcInfo = null;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    qcInfo = db.ClientInfo.First(c => c.Client_ID == iClient_ID);

                }
                catch (Exception ex)
                {
                }
                finally
                {
                    db.Connection.Close();
                }
            }
            return qcInfo;
        }

        /// <summary>
        /// 修改客户端信息
        /// 注意：请在编写实体之前先获取实体再进行修改的字段编写
        /// </summary>
        /// <param name="record">客户端实体（ClientInfo）</param>
        /// <returns></returns>
        public static bool UpdateOneQcRecord(ClientInfo client)
        {
            bool rbool = false;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    ClientInfo qInfo = db.ClientInfo.First(c => c.Client_ID == client.Client_ID);
                    qInfo.Client_ADDRESS = client.Client_ADDRESS;
                    qInfo.Client_Dictionary_ID = client.Client_Dictionary_ID;
                    qInfo.Client_NAME = client.Client_NAME;
                    qInfo.Client_Phone = client.Client_Phone;
                    qInfo.Client_REMARK = client.Client_REMARK;
                    qInfo.Client_TestItems_ID = client.Client_TestItems_ID;
                    db.SubmitChanges();
                    rbool = true;
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    db.Connection.Close();
                }
            }

            return rbool;
        }

        /// <summary>
        /// summary>
        /// 按条件查询信息Beonduty
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<View_ClientInfo> Query(Expression<Func<View_ClientInfo, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.View_ClientInfo.Where(fun).ToList();
            }

        }
        public static IEnumerable<ClientInfo> Query(Expression<Func<ClientInfo, bool>> fun)
        {
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.ClientInfo.Where(fun).ToList();
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
        public static bool Update(Expression<Func<ClientInfo, bool>> fun, Action<ClientInfo> action)
        {
            bool rbool = true;
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                var table = dc.ClientInfo.Where(fun).ToList();
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
        public static bool DeleteToMany(Expression<Func<ClientInfo, bool>> fun)
        {
            bool rbool = true;
            try
            {
                LinQBaseDao.DeleteToMany<ClientInfo>(new DCQUALITYDataContext(), fun);
            }
            catch
            {
                rbool = false;
            }
            return rbool;

        }
    }
}
