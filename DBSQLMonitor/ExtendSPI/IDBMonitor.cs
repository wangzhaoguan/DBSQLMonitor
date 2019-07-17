using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DBSQLMonitor.ExtendSPI
{
    public interface IDBMonitor
    {
        /// <summary>
        /// 检查数据库连接可用性和系统表、数据字典等对象的必要访问权限
        /// </summary>
        /// <returns></returns>
        bool CheckConnAndAuth(ConnStringBuilder connBuilder);

        /// <summary>
        /// 获取当前所有会话信息（状态、等待事件等）
        /// </summary>
        /// <param name="connBuilder"></param>
        /// <returns></returns>
        List<SessionInfo> GetSessionList(ConnStringBuilder connBuilder);

        /// <summary>
        /// kill阻塞源头
        /// </summary>
        /// <param name="connBuilder"></param>
        void KillRootBlockingSession(ConnStringBuilder connBuilder, List<SessionInfo> rootSessionList);
    }
}
