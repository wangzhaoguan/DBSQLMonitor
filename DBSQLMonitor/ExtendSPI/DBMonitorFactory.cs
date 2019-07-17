using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DBSQLMonitor.ExtendSPI
{
    class DBMonitorFactory
    {
        internal static IDBMonitor CreateDBMonitor(string dbType)
        {
            try
            {
                //string dbImpl = ConfigurationManager.AppSettings[dbType];
                //string[] asmNamespace = dbImpl.Split(',');
                //IDBMonitor db = Assembly.LoadFrom(asmNamespace[0]).CreateInstance(asmNamespace[1]) as IDBMonitor;
                //return db;

                if (string.Compare(dbType, "sqlserver", true) == 0)
                {
                    return new SqlserverImpl();
                }
                else if (string.Compare(dbType, "Oracle", true) ==0)
                {
                    return new OracleImpl();
                    //return new OracleDevartImpl();
                }
                else
                {
                    throw new Exception("No Such DB Type supported.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("配置文件被修改，无法加载对应的数据库的适配器！");
                System.Environment.Exit(0);
                return null;
            }
            finally
            {
            }
        }
    }
}
