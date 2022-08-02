USE [master]
GO

print 'Create database $(dbName)'
GO

CREATE DATABASE [$(dbName)] ON
PRIMARY
(
  NAME = [$(dbName)],
  FILENAME = 'G:\Data\$(dbName).mdf'
),
FILEGROUP [FG_Index]
(
  NAME = [$(dbName)_Index],
  FILENAME = 'G:\Data\$(dbName)_Index.ndf'
),
FILEGROUP [FG_Data]
(
  NAME = [$(dbName)_Data],
  FILENAME = 'G:\Data\$(dbName)_Data.ndf'
),
FILEGROUP [FG_Data1]
(
  NAME = [$(dbName)_Data1],
  FILENAME = 'G:\Data\$(dbName)_Data1.ndf'
),
FILEGROUP [FG_Data2]
(
  NAME = [$(dbName)_Data2],
  FILENAME = 'G:\Data\$(dbName)_Data2.ndf'
),
FILEGROUP [FG_Data3]
(
  NAME = [$(dbName)_Data3],
  FILENAME = 'G:\Data\$(dbName)_Data3.ndf'
),
FILEGROUP [FG_Data4]
(
  NAME = [$(dbName)_Data4],
  FILENAME = 'G:\Data\$(dbName)_Data4.ndf'
),
FILEGROUP [FG_Data5]
(
  NAME = [$(dbName)_Data5],
  FILENAME = 'G:\Data\$(dbName)_Data5.ndf'
),
FILEGROUP [FG_Data6]
(
  NAME = [$(dbName)_Data6],
  FILENAME = 'G:\Data\$(dbName)_Data6.ndf'
),
FILEGROUP [FG_Data7]
(
  NAME = [$(dbName)_Data7],
  FILENAME = 'G:\Data\$(dbName)_Data7.ndf'
),
FILEGROUP [FG_Data8]
(
  NAME = [$(dbName)_Data8],
  FILENAME = 'G:\Data\$(dbName)_Data8.ndf'
)
LOG ON
(
  NAME = [$(dbName)_log],
  FILENAME = 'F:\Log\$(dbName)_log.ldf'
)
COLLATE Cyrillic_General_CI_AS;
GO

ALTER DATABASE [$(dbName)]
MODIFY FILEGROUP [FG_Data] DEFAULT
GO

ALTER DATABASE [$(dbName)]
SET ALLOW_SNAPSHOT_ISOLATION ON

ALTER DATABASE [$(dbName)]
SET READ_COMMITTED_SNAPSHOT ON
GO