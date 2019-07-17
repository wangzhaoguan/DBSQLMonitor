using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBSQLMonitor.ExtendSPI
{
    public class ConnStringBuilder
    {
        public string DataSource { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string InitialCatalog { get; set; }

        public int ConnectionTimeout { get; set; }

        public int MaxPoolSize { get; set; }
    }
}
