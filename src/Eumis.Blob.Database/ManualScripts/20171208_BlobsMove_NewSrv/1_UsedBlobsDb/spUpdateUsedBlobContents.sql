PRINT 'Create spUpdateUsedBlobContents'

USE [UsedBlobContents]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateUsedBlobContents')
  BEGIN
    DROP PROCEDURE spUpdateUsedBlobContents
  END
GO

CREATE PROCEDURE spUpdateUsedBlobContents
AS

BEGIN TRANSACTION

TRUNCATE TABLE UniqueUsedBlobContents

--get unused blobs
INSERT INTO UniqueUsedBlobContents([BlobContentId])
SELECT [BlobContentId]
FROM [UsedBlobsSrv].[UsedBlobContents].dbo.UniqueUsedBlobContents

COMMIT

GO
