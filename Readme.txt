
SQL Monitor 
	Developed By zhaoguan wang

说明
------------------------------------------------------------------------------------
    从SQL阻塞和等待事件的角度，了解数据库运行情况，供DBA和开发人员分析优化做参考


要求
------------------------------------------------------------------------------------
  	WinXP/Vista/Win7/Win8/Win10/Windows Server(2003/2008/2012/2014)
    Microsoft .NET Framework 4.0


特性
------------------------------------------------------------------------------------
	设置监视频率，可选仅监视阻塞或所有会话
	监视阻塞时，可以设置阻塞时间的过滤条件
	监视阻塞时，可以选择kill阻塞源头（请谨慎）
	监视日志，可以自动转储为文本文件（缺省按照日志条数>10000触发转储，约几兆）
    默认提供了SQLServer和Oracle两个平台的实现
		MSSQL 2005 或更高版本	 ServerAdmin 或 SysAdmin 角色，例如sa账户
		Oracle 10g 或更高版本	 Select Any Dictionary 和 Alter System 权限，例如system
		无需安装Oracle客户端，登录Oracle时使用tnsnames或IP/SID方式(192.168.0.1/ora11r2)


====================================================================================

TestSQL
	测试Oracle数据库的连通性，当前运行环境下不同连接方式的响应性能差异
		System.Data.OracleClient	.NET Framework为Oracle提供的缺省ADO.NET实现
		Oracle.DataAccess.Client	Oracle提供的官方ADO.NET适配（ODP.NET）
		Oracle.ManagedDataAccess	Oracle提供的全托管ADO.NET适配，不依赖于Oracle的非托管客户端
	测试微软缺省实现的bug，例如偶发的两条数据，转换到DataSet/DataTable或DataReader读取时，行数翻倍的情况

