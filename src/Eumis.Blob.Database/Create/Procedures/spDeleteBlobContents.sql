PRINT 'Create spDeleteBlobContents'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteBlobContents')
  BEGIN
    DROP PROCEDURE spDeleteBlobContents
  END
GO

CREATE PROCEDURE spDeleteBlobContents
AS

-- Delete marked blob contents older than 30 days
DELETE b
FROM BlobContents b
  INNER JOIN vwBlobContentPartitions p ON $PARTITION.pfBlobContents(b.PartitionId) = p.partition_number AND p.is_read_only = 0
WHERE
  b.IsDeleted = 1 AND b.DeleteDate < DATEADD(day, -30, GETDATE())

PRINT 'Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' BlobContents'

GO
