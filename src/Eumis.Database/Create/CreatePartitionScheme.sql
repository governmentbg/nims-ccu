PRINT 'Create partition scheme'
GO

USE [$(dbName)]
GO

CREATE PARTITION SCHEME psActionLogs
AS PARTITION pfActionLogs ALL TO ([FG_ActionLogData]);
GO
