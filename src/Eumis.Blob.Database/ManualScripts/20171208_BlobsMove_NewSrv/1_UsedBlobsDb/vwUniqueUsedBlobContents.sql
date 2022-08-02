PRINT 'Create vwUniqueUsedBlobContents'
GO

USE [UsedBlobContents]
GO

IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwUniqueUsedBlobContents'))
  DROP VIEW vwUniqueUsedBlobContents
GO

CREATE VIEW vwUniqueUsedBlobContents
AS

SELECT
  ubc.[BlobContentId],
  CAST((100 + ubc.[BlobContentId] % 16) AS INT) as [PartitionId]
FROM
  UniqueUsedBlobContents ubc

GO
