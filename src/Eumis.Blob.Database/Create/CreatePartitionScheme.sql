PRINT 'Create partition scheme'
GO

USE [$(dbName)]
GO

CREATE PARTITION SCHEME psBlobContents
AS PARTITION pfBlobContents ALL TO ([FG_Data]);
GO
