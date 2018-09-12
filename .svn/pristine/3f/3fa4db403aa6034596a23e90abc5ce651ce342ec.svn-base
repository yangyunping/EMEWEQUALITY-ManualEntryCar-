using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;

namespace EMEWEDAL
{
    public  class QCGroupDAL
    {
        /// <summary>
        /// 查询所有信息 QCGroup
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<QCGroup> Query()
        {

            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                return db.QCGroup.ToList();
            }

        }
    }
}
