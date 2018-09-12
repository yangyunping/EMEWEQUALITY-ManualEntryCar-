using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EMEWEEntity
{
    public partial class DCQUALITYDataContext
    {
        public DCQUALITYDataContext() :
            base(ConfigurationManager.ConnectionStrings["EMEWEQCConnectionString"].ConnectionString)
        {
            OnCreated();
        }
    }
}
