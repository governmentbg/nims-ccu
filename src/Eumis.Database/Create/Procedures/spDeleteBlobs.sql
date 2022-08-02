PRINT 'Create spDeleteBlobs'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteBlobs')
  BEGIN
    DROP PROCEDURE spDeleteBlobs
  END
GO

CREATE PROCEDURE spDeleteBlobs
AS

SET NOCOUNT ON;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

BEGIN TRANSACTION;

BEGIN TRY

    -- Delete marked blobs older than 30 days
    DELETE b
    FROM Blobs b
    WHERE
      b.IsDeleted = 1 AND b.DeleteDate < DATEADD(day, -30, GETDATE())

    PRINT 'Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' Blobs'

    -- Delete marked blob content locations older than 30 days
    DELETE bcl
    FROM BlobContentLocations bcl
    WHERE
      bcl.IsDeleted = 1 AND bcl.DeleteDate < DATEADD(day, -30, GETDATE())

    PRINT 'Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(20)) + ' BlobContentLocations'
  
    COMMIT;
END TRY
BEGIN CATCH

    DECLARE @error int,
            @message varchar(4000);

    SELECT
        @error = ERROR_NUMBER(),
        @message = ERROR_MESSAGE();

    ROLLBACK;

    RAISERROR ('An error ocurred in spDeleteBlobs: %d: %s', 16, 1, @error, @message);
END CATCH;

GO
