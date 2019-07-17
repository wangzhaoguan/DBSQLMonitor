using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBSQLMonitor.ExtendSPI
{
    public class SqlserverImpl : IDBMonitor
    {
        public bool CheckConnAndAuth(ConnStringBuilder connBuilder)
        {
            SqlConnectionStringBuilder connstrBuilder = new SqlConnectionStringBuilder();
            connstrBuilder.DataSource = connBuilder.DataSource;
            connstrBuilder.UserID = connBuilder.UserName;
            connstrBuilder.Password = connBuilder.Password;
            connstrBuilder.InitialCatalog = "master";

            using (SqlConnection conn = new SqlConnection(connstrBuilder.ConnectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = 6000;
                //cmd.CommandText = @"SELECT 1 FROM syslogins t WHERE t.name=@loginName AND (t.sysadmin=1 OR t.serveradmin=1)";
                //cmd.Parameters.Add("@loginName", SqlDbType.VarChar, 100);
                //cmd.Parameters["@loginName"].Value = connBuilder;
                cmd.CommandText = @"SELECT 1 FROM syslogins t WHERE t.name=SYSTEM_USER AND (t.sysadmin=1 OR t.serveradmin=1)";

                conn.Open();
                object userAth = cmd.ExecuteScalar();
                if (userAth == null || userAth == DBNull.Value)
                {
                    throw new Exception("用户需要拥有sysadmin或serveradmin的权限");
                }
            }

            return true;
        }

        public List<SessionInfo> GetSessionList(ConnStringBuilder connBuilder)
        {
            SqlConnectionStringBuilder connstrBuilder = new SqlConnectionStringBuilder();
            connstrBuilder.DataSource = connBuilder.DataSource;
            connstrBuilder.UserID = connBuilder.UserName;
            connstrBuilder.Password = connBuilder.Password;
            connstrBuilder.InitialCatalog = "master";

            using (SqlConnection sqlConnection = new SqlConnection(connstrBuilder.ConnectionString))
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //sqlCommand.CommandText = "SELECT t.spid, t.kpid, t.blocked, t.status, t.lastwaittype, t.waitresource, t.waittime/1000 waittime, t.last_batch, DB_NAME(t.dbid) DbName, t.loginame, t.program_name, t.hostname, t.hostprocess, t.cmd, dc.text FROM master.sys.sysprocesses t OUTER APPLY master.sys.dm_exec_sql_text(t.sql_handle) dc WHERE t.spid >= 50 and t.spid != @@SPID";
                sqlCommand.CommandText = @"
select t.spid, t.kpid, t.blocked, t.status, t.lastwaittype, t.waitresource, t.waittime/1000 AS waittime, t.last_batch
    , DB_NAME(t.dbid) DbName, t.loginame, t.program_name, t.hostname, t.hostprocess, t.cmd, dc.text
	, p.query_plan, tr.cpu_time, tr.logical_reads, tr.reads, tr.total_elapsed_time
from    master.sys.sysprocesses t
    outer apply master.sys.dm_exec_sql_text(t.sql_handle) dc
    left join master.sys.dm_exec_requests tr on t.spid = tr.session_id
    outer apply sys.dm_exec_query_plan(tr.plan_handle) p
where    t.spid >= 50 and t.spid != @@SPID";
                sqlCommand.CommandTimeout = 180;
                List<SessionInfo> list = new List<SessionInfo>(128);
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        list.Add(ConverToBockInfo(sqlDataReader));
                    }
                }

                return list;
            }
        }


        public void KillRootBlockingSession(ConnStringBuilder connBuilder, List<SessionInfo> rootSessionList)
        {
            try
            {
                SqlConnectionStringBuilder connstrBuilder = new SqlConnectionStringBuilder();
                connstrBuilder.DataSource = connBuilder.DataSource;
                connstrBuilder.UserID = connBuilder.UserName;
                connstrBuilder.Password = connBuilder.Password;
                connstrBuilder.InitialCatalog = "master";

                using (SqlConnection sqlConnection = new SqlConnection(connstrBuilder.ConnectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    foreach (SessionInfo item in rootSessionList)
                    {
                        sqlCommand.CommandText = string.Format("kill {0}", item.spid);
                        sqlCommand.ExecuteNonQuery();
                    }
                    return;
                }
            }
            catch
            {
            }
        }

        private static SessionInfo ConverToBockInfo(SqlDataReader dr)
        {
            SessionInfo entry = new SessionInfo();
            entry.spid = Convert.ToInt32(dr["spid"]);
            entry.kpid = Convert.ToInt32(dr["kpid"]);
            entry.blocked = Convert.ToInt32(dr["blocked"]);
            entry.status = Convert.ToString(dr["status"]).Trim();
            entry.lastWaitType = Convert.ToString(dr["lastwaittype"]);
            entry.waitResource = Convert.ToString(dr["waitresource"]);
            entry.waitTime = Convert.ToInt64(dr["waittime"]);
            entry.lastBatch = Convert.ToDateTime(dr["last_batch"]);
            entry.dbName = Convert.ToString(dr["dbname"]);
            entry.loginName = Convert.ToString(dr["loginame"]);
            entry.programName = Convert.ToString(dr["program_name"]);
            entry.hostName = Convert.ToString(dr["hostname"]);
            entry.hostProcess = Convert.ToString(dr["hostprocess"]);
            entry.remarks = Convert.ToString(dr["query_plan"]);
            entry.sqlText = Convert.ToString(dr["text"]);

            entry.cpu_time = dr["cpu_time"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["cpu_time"]);
            entry.logical_reads = dr["logical_reads"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["logical_reads"]);
            entry.disk_reads = dr["reads"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["reads"]);
            entry.elapsed_time = dr["total_elapsed_time"] == DBNull.Value ? 0 : (Convert.ToDecimal(dr["total_elapsed_time"]) / 1000);

            return entry;
        }
    }
}

//Sqlserver
//select t.spid, t.kpid, t.blocked, t.status, t.lastwaittype, t.waitresource, t.waittime/1000 AS waittime, t.last_batch
//    , DB_NAME(t.dbid) DbName, t.loginame, t.program_name, t.hostname, t.hostprocess, t.cmd, dc.text
//from    master.sys.sysprocesses t  
//    outer apply master.sys.dm_exec_sql_text(t.sql_handle) dc
//where    t.spid >= 50 and t.spid != @@SPID


//select t.spid, t.kpid, t.blocked, t.status, t.lastwaittype, t.waitresource, t.waittime/1000 AS waittime, t.last_batch
//  , DB_NAME(t.dbid) DbName, t.loginame, t.program_name, t.hostname, t.hostprocess, t.cmd, dc.text
//	, p.query_plan, tr.cpu_time, tr.logical_reads, tr.reads, tr.total_elapsed_time
//from    master.sys.sysprocesses t
//    outer apply master.sys.dm_exec_sql_text(t.sql_handle) dc
//    left join master.sys.dm_exec_requests tr on t.spid = tr.session_id
//    outer apply sys.dm_exec_query_plan(tr.plan_handle) p
//where    t.spid >= 50 and t.spid != @@SPID
//    and t.status != 'sleeping' and t.total_elapsed_time > 1000;

