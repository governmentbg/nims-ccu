PRINT 'Create spCleanupBlobContents'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spCleanupBlobContents')
  BEGIN
    DROP PROCEDURE spCleanupBlobContents
  END
GO

CREATE PROCEDURE spCleanupBlobContents
AS

SET NOCOUNT ON;

DELETE b
FROM EumisBlobs.dbo.BlobContents b
WHERE NOT EXISTS(SELECT NULL FROM UniqueUsedBlobContents ubc WHERE ubc.BlobContentId = b.BlobContentId)


SET NOCOUNT OFF;

GO
