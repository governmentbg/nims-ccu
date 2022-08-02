PRINT 'Create vwUsedBlobContents'
GO

USE [UsedBlobContents]
GO

IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwUsedBlobContents'))
  DROP VIEW vwUsedBlobContents
GO

CREATE VIEW vwUsedBlobContents
AS

SELECT
  bc.[BlobContentId],
  CAST((100 + bc.[BlobContentId] % 16) AS INT) as [PartitionId],
  bc.[Hash],
  bc.[Size],
  bc.[Content],
  bc.[IsDeleted],
  CAST(N'2017-12-01 00:00:00' AS DATETIME2(7)) as [CreateDate],
  CAST(NULL AS DATETIME2(7)) as [DeleteDate]
FROM
  EumisBlobs1.dbo.BlobContents bc
  inner join UniqueUsedBlobContents ubc on bc.BlobContentId = ubc.BlobContentId

GO
