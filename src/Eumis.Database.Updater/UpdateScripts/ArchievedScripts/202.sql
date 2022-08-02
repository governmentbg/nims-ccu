PRINT 'Create spExistsBlobReference'

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spExistsBlobReference')
  BEGIN
    DROP PROCEDURE spExistsBlobReference
  END
GO

CREATE PROCEDURE spExistsBlobReference
(
    @blobKey UNIQUEIDENTIFIER
)
AS
    DECLARE @SQL NVARCHAR(MAX) = ''

    --get used blobs
    SELECT @SQL = @SQL +
        (CASE WHEN ROW_NUMBER() OVER(ORDER BY col.parent_object_id, col.parent_column_id) = 1 THEN '' ELSE ' UNION ALL ' END) +
        'SELECT ' + COL_NAME(col.parent_object_id, col.parent_column_id) + ' AS [Key] ' +
        'FROM ' + OBJECT_NAME(fk.parent_object_id) + ' ' +
        'WHERE ' + COL_NAME(col.parent_object_id, col.parent_column_id) + ' IS NOT NULL'
    FROM sys.foreign_keys AS fk
    INNER JOIN sys.foreign_key_columns AS col
        ON fk.object_id = col.constraint_object_id
    WHERE
        OBJECT_NAME(fk.referenced_object_id) = 'Blobs'
    ORDER BY
        col.parent_object_id, col.parent_column_id

    SET @SQL =
        'SELECT TOP(1) * FROM (' +
        @SQL +
        ') s WHERE [Key] = ''' + CAST(@blobKey AS NVARCHAR(MAX)) + ''''

    EXEC sp_ExecuteSQL @SQL
GO
