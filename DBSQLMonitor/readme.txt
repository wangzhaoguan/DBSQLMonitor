
DB SQL Monitor 
    Designed by Wang Zhaoguan 		www.cnblogs.com/zhaoguan_wang


˵��
------------------------------------------------------------------------------------
	�ӻỰ״̬�͵ȴ��¼��ĽǶȣ��˽����ݿ�������������DBA�Ϳ�����Ա�����Ż����ο�
    �ȴ��¼��ǻ��ڡ���ʵ���͡����ݡ��ģ����ǡ��Ʋ⡱�����Oracle AWR��ASH��Sqlserver DMV��Windows�������������ǿ��԰����ܷ�����Ϊ���ܵķ����ۡ�


Ҫ��
------------------------------------------------------------------------------------
  	WinXP/Vista/Win7/Win8/Win10/Windows Server(2003/2008/2012/2016)
    Microsoft .NET Framework 4.0
    SQL Server 2005 ����߰汾	 ServerAdmin �� SysAdmin ��ɫ������sa�˻�
    Oracle 10g ����߰汾		 Select Any Dictionary �� Alter System Ȩ�ޣ�����system
	

ע��
------------------------------------------------------------------------------------
	1������Oracle���ݿ⣬��������Oracle�ͻ��ˣ���ʹ��EZCONNECT��ʽ���ӣ���IP/SID��������192.168.1.1/fsdb
	2������������Ҫ���벻Ҫѡ��KILL�����ߡ�����ѡ��ѡ����߻�ɱ������Դͷ��Ӧ�ĻỰ��
	3���������ء���ť�󣬹��߻��Զ��������ù���������־��һ�㲻��Ҫ�����乤�����������߲�������־����
	4��ȱʡ���ó���5000����¼�����Զ�ת��ΪExcel�ļ���һ�㲻Ҫȡ�����Զ�ת����־����ȡ����ѡ��������ʱ��û����ջ򱣴���־������ɹ����ڴ�ռ��������
	5��������ڹرհ�ťʱ�����߻���С�����������������С����ȷʵҪ�رո��٣����Ҽ�����������ͼ�꣬ѡ���˳���
	6������ΪWinform����ѡ�����ʱ�벻Ҫע����ǰwindows�û���������ǿ�ƹرմ˳���
	7�����ݿ���ֹ���ʱ��Ϊ�ռ����ݶ�ִ�е�SQL����Ҳ���ܻ�����޷����е���������������Դ˹��߲���Ϊ���Ϸ������ã���ʱ��Ҫ�鿴���ݿ���־��


����SQL���£�
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
	
	
	
������־
------------------------------------------------------------------------------------
Updated 2017.10.20 v1.7.7
���ӡ�CPU���߼������������ִ��ʱ�䡱����ֵ
Remark�У�Oracle���ݿ���ʾSQL_ID��Sqlserver��ʾִ�мƻ�����active�Ự�Ż������ݣ�
Oracle.ManagedDataAccess�İ汾��Ϊ4.121.1.0������ʼ�汾�������.net framework 4.0�汾�£�����Oracle���û�������������bug


Updated 2017.04.05 v1.7.6
���ӡ�ֹͣʱ�䡱��ȱʡΪ��ǰ���ں�14��
����չ��ܸ�Ϊ�б�ؼ����Ҽ�����
�б�ؼ���ѡ������������ʱ�����԰�Ctrl+C����ѡ�е����ݵ�������
������־ʱ���������ڴ�����־Ŀ¼�ļ��У�����ת������־��������Ϊ10000

Updated 2017.03.23 v1.7.5
����remarks�У�Oracle���ݿ��Ӧ����sql_id��Ϣ��sqlserver���ݿ��Ӧ��cmd
���ݿ�������Ϣ����������뷨������ÿ�δ򿪻��޸�ʱ��ǰ����Ϊ���ĵ���Ҫ�������뷨

Updated 2017.03.21 v1.7.4
�޸���¼ʱ���Oracle�˻�Ȩ�޲�׼ȷ������
Ҫ���Oracle�û�Ȩ��Ϊselect any Dictionary �� alter system
 
Updated 2017.02.24 v1.7.3
�޸�����ִ�С�RAC�ڵ��������ϵͳ����������ɹ��߱�������
       1��Sequence contains more than one matching element
       2��Sequence contains no matching element
 
Updated 2017.02.20 v1.7.0
�޸����ΪExcel ʱ������ı����ݹ��������ֳ���32K���ƵĴ���
 
Updated 2017.02.15 v1.6.5
�޸����������Ϣʱ����������лỰ���б�
�Զ�ת���������������Ϊ5000�����ٹ��ߵ��ڴ�ռ��
  
Updated 2017.01.20 v1.6
���ӵȴ��¼��Ͷ�Ӧ����Դ������Ӧ�÷�������Ӧ�ý������ƺͽ���ID
����ΪExcel�ļ�����鿴��ɸѡ������

Created 2016.04.08 v1.5 
SQL�������
    ����ʱ����Զ������ã������Ự״̬��SQL�ű���Ϣ
    ֧��Oracle���ݿ⣨��������Oracle�ͻ��ˣ�
    �Զ����ɼ����־txt 