using EMEWEEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMEWEDAL
{
  public  class RegisterLoosePaperDistributionDAL
    {
        /// </summary>
        /// 新增一条车辆质检记录
        /// </summary>
        /// <param name="qcRecord">QCRecord质检实体</param>
        /// <param name="rint">新增后自动增长编号</param>
        /// <returns></returns>
        public static bool InsertOneCarInfo(RegisterLoosePaperDistribution DRAW_E_INI, out int rint)
        {

            rint = 0;
            bool rbool = true;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                try
                {
                    db.RegisterLoosePaperDistribution.InsertOnSubmit(DRAW_E_INI);
                    db.SubmitChanges();
                    rbool = true;
                    rint = (int)DRAW_E_INI.R_DRAW_EXAM_INTERFACE_ID;
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
