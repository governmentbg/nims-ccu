USE [master]
GO

print 'Create database $(dbName)'
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'$(dbName)')
BEGIN
    ALTER DATABASE [$(dbName)] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE [$(dbName)]
END
GO

DECLARE @SQL NVARCHAR(MAX) = ''

SELECT @SQL = @SQL + '
  CREATE DATABASE [$(dbName)] ON
  PRIMARY
  (
    NAME = [$(dbName)],
    FILENAME = ''' + CAST(SERVERPROPERTY('INSTANCEDEFAULTDATAPATH') AS NVARCHAR(MAX)) + '$(dbName).mdf''
  ),
  FILEGROUP [FG_ActionLogData]
  (
    NAME = [$(dbName)_ActionLogData],
    FILENAME = ''' + CAST(SERVERPROPERTY('INSTANCEDEFAULTDATAPATH') AS NVARCHAR(MAX)) + '$(dbName)_ActionLogData.ndf''
  )
  LOG ON
  (
    NAME = [$(dbName)_log],
    FILENAME = '''  + CAST(SERVERPROPERTY('INSTANCEDEFAULTLOGPATH') AS NVARCHAR(MAX)) + '$(dbName)_log.ldf''
  )
  COLLATE Cyrillic_General_CI_AS';

EXEC sp_executesql @SQL
GO

ALTER DATABASE [$(dbName)]
SET ALLOW_SNAPSHOT_ISOLATION ON

ALTER DATABASE [$(dbName)]
SET READ_COMMITTED_SNAPSHOT ON
GO
