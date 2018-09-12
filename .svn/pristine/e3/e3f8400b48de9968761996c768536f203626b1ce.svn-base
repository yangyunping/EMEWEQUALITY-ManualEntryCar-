using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;

namespace EMEWEDAL
{
   public class OFFQCRecordDAL
    {
        /// <summary>
        /// 新增一条质检记录审批人记录
        /// </summary>
        /// <param name="qcRecord">质检实体</param>
        /// <returns></returns>
       public static bool InsertOFFQCRecord(OFFQCRecord off, out int rint)
        {
            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.OFFQCRecord.InsertOnSubmit(off);
                    db.SubmitChanges();
                    rint = off.OFFQCRecord_ID;
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
