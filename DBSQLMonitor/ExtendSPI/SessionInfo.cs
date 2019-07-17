using System;

namespace DBSQLMonitor.ExtendSPI
{
    public class SessionInfo
    {
        public int spid { get; set; }

        public int blocked { get; set; }

        public int kpid { get; set; }

        public string status { get; set; }

        public string lastWaitType { get; set; }
        
        public string waitResource { get; set; }

        public long waitTime { get; set; }
                
        public string dbName { get; set; }

        public string loginName { get; set; }

        public string programName { get; set; }

        public string hostName { get; set; }

        public string hostProcess { get; set; }

        public DateTime lastBatch { get; set; }

        public string remarks { get; set; }

        public string sqlText { get; set; }

        public decimal cpu_time { get; set; }

        public decimal logical_reads { get; set; }

        public decimal disk_reads { get; set; }

        public decimal elapsed_time { get; set; }
    }

}
