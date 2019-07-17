//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Devart.Data.Oracle;

//namespace DBSQLMonitor.ExtendSPI
//{
//    public class OracleDevartImpl : IDBMonitor
//    {
//        public bool CheckConnAndAuth(ConnStringBuilder connBuilder)
//        {
//            OracleConnectionStringBuilder connstrBuilder = new OracleConnectionStringBuilder();
//            connstrBuilder.Server = connBuilder.DataSource;
//            connstrBuilder.UserId = connBuilder.UserName;
//            connstrBuilder.Password = connBuilder.Password;
//            connstrBuilder.Direct = true;
            
//            //connstrBuilder.SelfTuning = false;

//            using (OracleConnection conn = new OracleConnection(connstrBuilder.ConnectionString))
//            {
                
//                OracleCommand cmd = conn.CreateCommand();

//                string s1 = @"
//                        SELECT COUNT(1)
//                        FROM (
//                               SELECT 1
//                               FROM user_sys_privs t 
//                               WHERE t.privilege = 'SELECT ANY DICTIONARY'
//                               UNION ALL
//                               SELECT 1
//                               FROM user_role_privs r 
//                                   JOIN role_sys_privs p ON r.GRANTED_ROLE = p.ROLE
//                               WHERE p.privilege = 'SELECT ANY DICTIONARY'
//                        ) T ";
//                string s2 = @"                        
//                        SELECT COUNT(1)
//                        FROM (
//                               SELECT 1
//                               FROM user_sys_privs t 
//                               WHERE t.privilege = 'ALTER SYSTEM'
//                               UNION ALL
//                               SELECT 1
//                               FROM user_role_privs r 
//                                   JOIN role_sys_privs p ON r.GRANTED_ROLE = p.ROLE
//                               WHERE p.privilege = 'ALTER SYSTEM'
//                        ) S ";

//                string sql = string.Format("SELECT ({0}) * ({1}) AS C FROM DUAL", s1, s2);
//                cmd.CommandText = sql;

//                conn.Open();
//                object userAth = cmd.ExecuteScalar();
//                if (userAth == null || userAth == DBNull.Value || Convert.ToInt32(userAth) == 0)
//                {
//                    throw new Exception("用户需要[SELECT ANY DICTIONARY]和[ALTER SYSTEM]的权限");
//                }
//            }

//            return true;
//        }

//        public List<SessionInfo> GetSessionList(ConnStringBuilder connBuilder)
//        {
//            OracleConnectionStringBuilder connstrBuilder = new OracleConnectionStringBuilder();
//            connstrBuilder.Server = connBuilder.DataSource;
//            connstrBuilder.UserId = connBuilder.UserName;
//            connstrBuilder.Password = connBuilder.Password;
//            connstrBuilder.Direct = true;

//            using (OracleConnection conn = new OracleConnection(connstrBuilder.ConnectionString))
//            {
//                OracleCommand cmd = conn.CreateCommand();
//                //cmd.CommandText = @"SELECT t.SID, t.SERIAL#, t.STATUS, t.EVENT, t.BLOCKING_SESSION, t.BLOCKING_SESSION_STATUS, t.USERNAME, t.MACHINE, t.PROGRAM, t.PROCESS, t.SECONDS_IN_WAIT, t.P1TEXT, t.P2TEXT, t.P3TEXT, t.P1, t.P2, t.P3, NVL(s1.SQL_ID, s2.SQL_ID) AS SQL_ID, NVL(s1.SQL_TEXT, s2.SQL_TEXT) AS SQL_TEXT FROM V$SESSION t LEFT JOIN V$SQL s1 ON t.SQL_ID = s1.SQL_ID AND t.SQL_CHILD_NUMBER = s1.CHILD_NUMBER LEFT JOIN V$SQL s2 ON t.PREV_SQL_ID = s2.SQL_ID AND t.PREV_CHILD_NUMBER = s2.CHILD_NUMBER WHERE t.TYPE = 'USER' AND t.SID != USERENV('SID')";
//                cmd.CommandText = @"
//SELECT t.SID, t.SERIAL#, t.STATUS, t.EVENT, t.BLOCKING_SESSION, t.BLOCKING_SESSION_STATUS, t.USERNAME, t.MACHINE, t.PROGRAM, t.PROCESS, t.SECONDS_IN_WAIT /* , t.SQL_EXEC_START   -- 11g */
//       , t.P1TEXT, t.P2TEXT, t.P3TEXT, t.P1, t.P2, t.P3, NVL(s1.SQL_ID, s2.SQL_ID) AS SQL_ID, NVL(s1.SQL_TEXT, s2.SQL_TEXT) AS SQL_TEXT
//       , NVL(s1.CPU_TIME, s2.CPU_TIME) AS CPU_TIME, NVL(s1.BUFFER_GETS, s2.BUFFER_GETS) AS BUFFER_GETS, NVL(s1.DISK_READS, s2.DISK_READS) AS DISK_READS, NVL(s1.ELAPSED_TIME, s2.ELAPSED_TIME) AS ELAPSED_TIME
//FROM V$SESSION t
//  LEFT JOIN V$SQL s1 ON t.SQL_ID = s1.SQL_ID AND t.SQL_CHILD_NUMBER = s1.CHILD_NUMBER
//  LEFT JOIN V$SQL s2 ON t.PREV_SQL_ID = s2.SQL_ID AND t.PREV_CHILD_NUMBER = s2.CHILD_NUMBER
//WHERE t.TYPE = 'USER' AND t.SID != USERENV('SID')";

//                cmd.CommandTimeout = 300;

//                List<SessionInfo> sessionList = new List<SessionInfo>(128);
//                conn.Open();
//                using (OracleDataReader dr = cmd.ExecuteReader())
//                {
//                    while (dr.Read())
//                    {
//                        sessionList.Add(ConverToBockInfo(dr));
//                    }
//                }

//                return sessionList;
//            }
//        }

//        public void KillRootBlockingSession(ConnStringBuilder connBuilder, List<SessionInfo> rootSessionList)
//        {
//            try
//            {
//                OracleConnectionStringBuilder connstrBuilder = new OracleConnectionStringBuilder();
//                connstrBuilder.Server = connBuilder.DataSource;
//                connstrBuilder.UserId = connBuilder.UserName;
//                connstrBuilder.Password = connBuilder.Password;
//                connstrBuilder.Direct = true;

//                using (OracleConnection conn = new OracleConnection(connstrBuilder.ConnectionString))
//                {
//                    conn.Open();
//                    OracleCommand cmd = conn.CreateCommand();
//                    foreach (var item in rootSessionList)
//                    {
//                        cmd.CommandText = string.Format("alter system kill session '{0},{1}'", item.spid, item.kpid);
//                        cmd.ExecuteNonQuery();
//                    }
//                }
//            }
//            catch
//            {

//            }
//        }

//        private static SessionInfo ConverToBockInfo(OracleDataReader dr)
//        {
//            SessionInfo entry = new SessionInfo();
//            entry.spid = Convert.ToInt32(dr["SID"]);
//            entry.kpid = Convert.ToInt32(dr["SERIAL#"]);
//            entry.blocked = dr["BLOCKING_SESSION"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BLOCKING_SESSION"]);
//            entry.status = Convert.ToString(dr["STATUS"]).Trim();
//            entry.lastWaitType = Convert.ToString(dr["EVENT"]);
//            entry.waitResource = string.Format("{0}; {1}; {2} : {3}; {4}; {5}", dr["P1TEXT"], dr["P2TEXT"], dr["P3TEXT"], dr["P1"], dr["P2"], dr["P3"]);
//            entry.waitTime = entry.blocked == 0 ? 0 : Convert.ToInt64(dr["SECONDS_IN_WAIT"]);
//            //entry.lastBatch = Convert.ToDateTime(dr["last_batch"]);
//            //entry.dbName = Convert.ToString(dr["dbname"]);
//            entry.loginName = Convert.ToString(dr["USERNAME"]);
//            entry.programName = Convert.ToString(dr["PROGRAM"]);
//            entry.hostName = Convert.ToString(dr["MACHINE"]);
//            entry.hostProcess = Convert.ToString(dr["PROCESS"]);
//            entry.remarks = Convert.ToString(dr["SQL_ID"]);
//            entry.sqlText = Convert.ToString(dr["SQL_TEXT"]);

//            entry.cpu_time = dr["CPU_TIME"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CPU_TIME"]);
//            entry.logical_reads = dr["BUFFER_GETS"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["BUFFER_GETS"]);
//            entry.disk_reads = dr["DISK_READS"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DISK_READS"]);
//            entry.elapsed_time = dr["SECONDS_IN_WAIT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SECONDS_IN_WAIT"]);

//            return entry;
//        }
//    }
//}


//// Oracle 
////SELECT t.SID, t.SERIAL#, t.STATUS, t.EVENT, t.BLOCKING_SESSION, t.BLOCKING_SESSION_STATUS, t.USERNAME, t.MACHINE, t.PROGRAM, t.PROCESS
////       , t.SECONDS_IN_WAIT, t.P1TEXT, t.P2TEXT, t.P3TEXT, t.P1, t.P2, t.P3, NVL(s1.SQL_ID, s2.SQL_ID) AS SQL_ID, NVL(s1.SQL_TEXT, s2.SQL_TEXT) AS SQL_TEXT
////FROM V$SESSION t
////  LEFT JOIN V$SQL s1 ON t.SQL_ID = s1.SQL_ID AND t.SQL_CHILD_NUMBER = s1.CHILD_NUMBER
////  LEFT JOIN V$SQL s2 ON t.PREV_SQL_ID = s2.SQL_ID AND t.PREV_CHILD_NUMBER = s2.CHILD_NUMBER
////WHERE t.TYPE = 'USER' AND t.SID != USERENV('SID')


////SELECT t.SID, t.SERIAL#, t.STATUS, t.EVENT, t.BLOCKING_SESSION, t.BLOCKING_SESSION_STATUS, t.USERNAME, t.MACHINE, t.PROGRAM, t.PROCESS, t.SECONDS_IN_WAIT /* , t.SQL_EXEC_START   -- 11g */
////       , t.P1TEXT, t.P2TEXT, t.P3TEXT, t.P1, t.P2, t.P3, NVL(s1.SQL_ID, s2.SQL_ID) AS SQL_ID, NVL(s1.SQL_TEXT, s2.SQL_TEXT) AS SQL_TEXT
////       , NVL(s1.CPU_TIME, s2.CPU_TIME) AS CPU_TIME, NVL(s1.BUFFER_GETS, s2.BUFFER_GETS) AS BUFFER_GETS, NVL(s1.DISK_READS, s2.DISK_READS) AS DISK_READS, NVL(s1.ELAPSED_TIME, s2.ELAPSED_TIME) AS ELAPSED_TIME
////FROM V$SESSION t
////  LEFT JOIN V$SQL s1 ON t.SQL_ID = s1.SQL_ID AND t.SQL_CHILD_NUMBER = s1.CHILD_NUMBER
////  LEFT JOIN V$SQL s2 ON t.PREV_SQL_ID = s2.SQL_ID AND t.PREV_CHILD_NUMBER = s2.CHILD_NUMBER
////WHERE t.TYPE = 'USER' AND t.SID != USERENV('SID')
