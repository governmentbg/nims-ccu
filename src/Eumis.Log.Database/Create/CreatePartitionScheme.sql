PRINT 'Create partition scheme'
GO

USE [$(dbName)]
GO

CREATE PARTITION SCHEME psLogs
AS PARTITION pfLogs ALL TO ([FG_Data]);
GO
