PRINT 'Create spMarkDeletedBlobContents'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spMarkDeletedBlobContents')
  BEGIN
    DROP PROCEDURE spMarkDeletedBlobContents
  END
GO

CREATE PROCEDURE spMarkDeletedBlobContents
(
  @remoteDb NVARCHAR(200) = N''
)
AS

SET NOCOUNT ON;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

BEGIN TRANSACTION;

BEGIN TRY

    CREATE TABLE #UsedBlobContentLocations ([BlobContentId] BIGINT, [PartitionId] INT);
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @Temp INT;

    --Take exclusive lock on the entire blobs table
    SELECT TOP 1 @Temp = 1 FROM BlobContents WITH (XLOCK, TABLOCK);

    SET @SQL =
      'INSERT INTO #UsedBlobContentLocations ' +
      'SELECT BlobContentId, PartitionId FROM ' + @remoteDb + '.dbo.BlobContentLocations ';

    EXEC sp_ExecuteSQL @SQL

    -- Mark deleted blob contents
    UPDATE b SET
      b.IsDeleted = 1,
      b.DeleteDate = GETDATE()
    FROM BlobContents b
      INNER JOIN vwBlobContentPartitions p ON $PARTITION.pfBlobContents(b.PartitionId) = p.partition_number AND p.is_read_only = 0
    WHERE
      b.IsDeleted = 0 AND
      NOT EXISTS (SELECT NULL FROM #UsedBlobContentLocations dbc WHERE b.BlobContentId = dbc.BlobContentId AND b.PartitionId = dbc.PartitionId) AND
      b.CreateDate < DATEADD(day, -2, GETDATE())

    PRINT 'Marked ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' BlobContents as deleted'

    COMMIT;
END TRY
BEGIN CATCH

    DECLARE @error int,
            @message varchar(4000);

    SELECT
        @error = ERROR_NUMBER(),
        @message = ERROR_MESSAGE();

    ROLLBACK;

    RAISERROR ('An error ocurred in spMarkDeletedBlobContents: %d: %s', 16, 1, @error, @message);
END CATCH;

GO
