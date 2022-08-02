PRINT 'Create spMarkDeletedBlobs'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spMarkDeletedBlobs')
  BEGIN
    DROP PROCEDURE spMarkDeletedBlobs
  END
GO

CREATE PROCEDURE spMarkDeletedBlobs
AS

SET NOCOUNT ON;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

BEGIN TRANSACTION;

BEGIN TRY

    CREATE TABLE #UsedBlobs ([Key] UNIQUEIDENTIFIER, [SourceTable] NVARCHAR(200))
    DECLARE @SQL NVARCHAR(MAX) = ''

    --get used blobs
    SELECT @SQL = @SQL +
      'INSERT INTO #UsedBlobs ([Key], [SourceTable]) SELECT ' +
      COL_NAME(col.parent_object_id, col.parent_column_id) + ' AS [Key], ' +
      '''' + OBJECT_NAME(fk.parent_object_id) + ''' AS [SourceTable] ' +
      'FROM ' + OBJECT_NAME(fk.parent_object_id) + ' ' +
      'WHERE ' + COL_NAME(col.parent_object_id, col.parent_column_id) + ' IS NOT NULL;'
    FROM sys.foreign_keys AS fk
    INNER JOIN sys.foreign_key_columns AS col
        ON fk.object_id = col.constraint_object_id
    WHERE 
        OBJECT_NAME(fk.referenced_object_id) = 'Blobs'

    EXEC sp_ExecuteSQL @SQL

    -- Mark deleted blobs
    UPDATE b SET
      b.IsDeleted = 1,
      b.DeleteDate = GETDATE()
    FROM Blobs b
    WHERE
      b.IsDeleted = 0 AND
      NOT EXISTS (SELECT NULL FROM #UsedBlobs ub WHERE ub.[Key] = b.[Key]) AND
      b.CreateDate < DATEADD(day, -2, GETDATE())

    PRINT 'Marked ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' Blobs as deleted'

    -- Mark deleted blob content locations
    UPDATE bcl SET
      bcl.IsDeleted = 1,
      bcl.DeleteDate = GETDATE()
    FROM BlobContentLocations bcl
    WHERE
      bcl.IsDeleted = 0 AND
      NOT EXISTS (SELECT NULL FROM Blobs b WHERE b.IsDeleted = 0 AND bcl.BlobContentLocationId = b.BlobContentLocationId)

    PRINT 'Marked ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' BlobContentLocations as deleted'

    DROP TABLE #UsedBlobs;

    COMMIT;
END TRY
BEGIN CATCH

    DECLARE @error int,
            @message varchar(4000);

    SELECT
        @error = ERROR_NUMBER(),
        @message = ERROR_MESSAGE();

    ROLLBACK;

    RAISERROR ('An error ocurred in spMarkDeletedBlobs: %d: %s', 16, 1, @error, @message);
END CATCH;

GO
