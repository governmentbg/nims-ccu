PRINT 'Create spUpdateUsedBlobContents'

USE [UsedBlobContents]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateUsedBlobContents')
  BEGIN
    DROP PROCEDURE spUpdateUsedBlobContents
  END
GO

CREATE PROCEDURE spUpdateUsedBlobContents
  (
    @lastBlobContentId INT = 0,
    @top INT = 10
  )
AS

SET NOCOUNT ON;

BEGIN TRANSACTION

CREATE TABLE #UsedBlobs ([Key] UNIQUEIDENTIFIER)
DECLARE @SQL NVARCHAR(MAX) = ''

--get used blobs
SELECT @SQL = @SQL + 'INSERT INTO #UsedBlobs ([Key])
                      SELECT ' + col.name + ' AS [Key]
                      FROM ' + '[Eumis].dbo.' + col_obj.name + 
                      ' WHERE ' + col.name + ' IS NOT NULL;'
FROM [Eumis].sys.foreign_keys AS fk
  INNER JOIN [Eumis].sys.foreign_key_columns AS fk_col ON fk.object_id = fk_col.constraint_object_id
  INNER JOIN [Eumis].sys.columns AS col ON fk_col.parent_object_id = col.object_id AND fk_col.parent_column_id = col.column_id
  INNER JOIN [Eumis].sys.objects AS col_obj ON col.object_id = col_obj.object_id
  INNER JOIN [Eumis].sys.objects AS fk_obj ON fk.referenced_object_id = fk_obj.object_id
WHERE 
    fk_obj.name = 'Blobs'

EXEC sp_ExecuteSQL @SQL

TRUNCATE TABLE UniqueUsedBlobContents

--get unused blobs
INSERT INTO UniqueUsedBlobContents([BlobContentId])
SELECT DISTINCT bcl.BlobContentId
from [Eumis].dbo.BlobContentLocations bcl
  inner join [Eumis].dbo.Blobs b on bcl.BlobContentLocationId = b.BlobContentLocationId
  inner join #UsedBlobs ub on b.[Key] = ub.[Key]

DROP TABLE #UsedBlobs

COMMIT

GO
