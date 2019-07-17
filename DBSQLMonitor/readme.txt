
DB SQL Monitor 
    Designed by Wang Zhaoguan 		www.cnblogs.com/zhaoguan_wang


说明
------------------------------------------------------------------------------------
	从会话状态和等待事件的角度，了解数据库的运行情况，供DBA和开发人员分析优化做参考
    等待事件是基于“事实”和“数据”的，而非“推测”，结合Oracle AWR、ASH（Sqlserver DMV、Windows计数器），我们可以把性能分析变为可能的方法论。


要求
------------------------------------------------------------------------------------
  	WinXP/Vista/Win7/Win8/Win10/Windows Server(2003/2008/2012/2016)
    Microsoft .NET Framework 4.0
    SQL Server 2005 或更高版本	 ServerAdmin 或 SysAdmin 角色，例如sa账户
    Oracle 10g 或更高版本		 Select Any Dictionary 和 Alter System 权限，例如system
	

注意
------------------------------------------------------------------------------------
	1、连接Oracle数据库，不依赖于Oracle客户端，请使用EZCONNECT方式连接（即IP/SID），例如192.168.1.1/fsdb
	2、除非特殊需要，请不要选择“KILL阻塞者”（勾选此选项，工具会杀掉阻塞源头对应的会话）
	3、点击“监控”按钮后，工具会自动根据设置规则生成日志，一般不需要干扰其工作，分析工具产生的日志即可
	4、缺省配置超出5000条记录，会自动转储为Excel文件，一般不要取消“自动转储日志”（取消此选项后如果长时间没有清空或保存日志，会造成工具内存占用累增）
	5、点击窗口关闭按钮时，工具会最小化到任务栏继续运行。如果确实要关闭跟踪，请右键任务栏工具图标，选择退出。
	6、工具为Winform程序，选择跟踪时请不要注销当前windows用户，这样会强制关闭此程序。
	7、数据库出现故障时，为收集数据而执行的SQL自身也可能会出现无法运行的情况（报错），所以此工具不能为故障分析所用，此时需要查看数据库日志。


核心SQL如下：
--Oracle 
SELECT t.SID, t.SERIAL#, t.STATUS, t.EVENT, t.BLOCKING_SESSION, t.BLOCKING_SESSION_STATUS, t.USERNAME, t.MACHINE, t.PROGRAM, t.PROCESS, t.SECONDS_IN_WAIT /* , t.SQL_EXEC_START   -- 11g */
       , t.P1TEXT, t.P2TEXT, t.P3TEXT, t.P1, t.P2, t.P3, NVL(s1.SQL_ID, s2.SQL_ID) AS SQL_ID, NVL(s1.SQL_TEXT, s2.SQL_TEXT) AS SQL_TEXT
FROM V$SESSION t
  LEFT JOIN V$SQL s1 ON t.SQL_ID = s1.SQL_ID AND t.SQL_CHILD_NUMBER = s1.CHILD_NUMBER
  LEFT JOIN V$SQL s2 ON t.PREV_SQL_ID = s2.SQL_ID AND t.PREV_CHILD_NUMBER = s2.CHILD_NUMBER
WHERE t.TYPE = 'USER' AND t.SID != USERENV('SID')


--Sqlserver
select t.spid, t.kpid, t.blocked, t.status, t.lastwaittype, t.waitresource, t.waittime/1000 AS waittime, t.last_batch
    , DB_NAME(t.dbid) DbName, t.loginame, t.program_name, t.hostname, t.hostprocess, t.cmd, dc.text
from    master.sys.sysprocesses t  
    outer apply master.sys.dm_exec_sql_text(t.sql_handle) dc
where    t.spid >= 50 and t.spid != @@SPID



select t.spid, t.kpid, t.blocked, t.status, t.lastwaittype, t.waitresource, t.waittime/1000 AS waittime, t.last_batch
    , DB_NAME(t.dbid) DbName, t.loginame, t.program_name, t.hostname, t.hostprocess, t.cmd, dc.text
	, p.query_plan, tr.cpu_time, tr.logical_reads, tr.reads, tr.total_elapsed_time
from    master.sys.sysprocesses t  
    outer apply master.sys.dm_exec_sql_text(t.sql_handle) dc
	left join master.sys.dm_exec_requests tr on t.spid = tr.session_id
	outer apply master.sys.dm_exec_query_plan(tr.plan_handle) p
where    t.spid >= 50 and t.spid != @@SPID
	and t.status != 'sleeping' and tr.total_elapsed_time > 1000;




SELECT t.SID, t.SERIAL#, t.STATUS, t.EVENT, t.BLOCKING_SESSION, t.BLOCKING_SESSION_STATUS, t.USERNAME, t.MACHINE, t.PROGRAM, t.PROCESS, t.SECONDS_IN_WAIT /* , t.SQL_EXEC_START   -- 11g */
       , t.P1TEXT, t.P2TEXT, t.P3TEXT, t.P1, t.P2, t.P3, NVL(s1.SQL_ID, s2.SQL_ID) AS SQL_ID, NVL(s1.SQL_TEXT, s2.SQL_TEXT) AS SQL_TEXT
       , NVL(s1.CPU_TIME, s2.CPU_TIME) AS CPU_TIME, NVL(s1.BUFFER_GETS, s2.BUFFER_GETS) AS BUFFER_GETS, NVL(s1.DISK_READS, s2.DISK_READS) AS DISK_READS, NVL(s1.ELAPSED_TIME, s2.ELAPSED_TIME) AS ELAPSED_TIME
       , t.SECONDS_IN_WAIT, t.LOCKWAIT, t.WAIT_TIME, t.WAIT_TIME_MICRO
FROM V$SESSION t
  LEFT JOIN V$SQL s1 ON t.SQL_ID = s1.SQL_ID AND t.SQL_CHILD_NUMBER = s1.CHILD_NUMBER
  LEFT JOIN V$SQL s2 ON t.PREV_SQL_ID = s2.SQL_ID AND t.PREV_CHILD_NUMBER = s2.CHILD_NUMBER
WHERE t.TYPE = 'USER' AND t.SID != USERENV('SID')
      and t.STATUS = 'ACTIVE' and t.SECONDS_IN_WAIT > 1;
	
	
	
更新日志
------------------------------------------------------------------------------------
Updated 2017.10.20 v1.7.7
增加“CPU、逻辑读、物理读、执行时间”四列值
Remark列：Oracle数据库显示SQL_ID；Sqlserver显示执行计划（仅active会话才会有内容）
Oracle.ManagedDataAccess的版本改为4.121.1.0（即初始版本），解决.net framework 4.0版本下，连接Oracle报用户名或密码错误的bug


Updated 2017.04.05 v1.7.6
增加“停止时间”，缺省为当前日期后14天
将清空功能改为列表控件的右键操作
列表控件中选择若干行数据时，可以按Ctrl+C复制选中的内容到剪贴板
保存日志时，按照日期创建日志目录文件夹，触发转储的日志条数调整为10000

Updated 2017.03.23 v1.7.5
增加remarks列，Oracle数据库对应的是sql_id信息，sqlserver数据库对应的cmd
数据库连接信息界面禁用输入法，避免每次打开或修改时因当前输入为中文等需要更改输入法

Updated 2017.03.21 v1.7.4
修复登录时检查Oracle账户权限不准确的问题
要求的Oracle用户权限为select any Dictionary 和 alter system
 
Updated 2017.02.24 v1.7.3
修复因并行执行、RAC节点间阻塞或系统任务阻塞造成工具报错的情况
       1、Sequence contains more than one matching element
       2、Sequence contains no matching element
 
Updated 2017.02.20 v1.7.0
修复另存为Excel 时，如果文本内容过长，出现超出32K限制的错误
 
Updated 2017.02.15 v1.6.5
修复监控阻塞信息时，输出了所有会话的列表
自动转储的最大行数更改为5000，减少工具的内存占用
  
Updated 2017.01.20 v1.6
增加等待事件和对应的资源参数，应用服务器、应用进程名称和进程ID
保存为Excel文件方便查看、筛选、排序

Created 2016.04.08 v1.5 
SQL阻塞监控
    阻塞时间的自定义设置，包含会话状态和SQL脚本信息
    支持Oracle数据库（不依赖于Oracle客户端）
    自动生成监控日志txt 