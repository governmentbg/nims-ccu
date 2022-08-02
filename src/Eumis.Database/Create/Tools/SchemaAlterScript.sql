DECLARE @schemaName NVARCHAR(50) = 'RioSchemaCollection'

DECLARE @xmlCols TABLE (
	ID	INTEGER IDENTITY(1,1),
	TBL NVARCHAR(1024),
	COL NVARCHAR(1024)
);

INSERT INTO @xmlCols (TBL,COL)
SELECT DISTINCT
	OBJECT_NAME(cols.object_id) AS 'TABLE',
	cols.NAME AS 'COLUMN'
FROM SYS.COLUMNS cols
INNER JOIN SYS.XML_SCHEMA_COLLECTIONS sch ON cols.XML_COLLECTION_ID = sch.XML_COLLECTION_ID
WHERE sch.NAME = @schemaName
ORDER BY OBJECT_NAME(cols.OBJECT_ID), cols.NAME

DECLARE @lastRow INT = @@ROWCOUNT
DECLARE @currentRow INT = @lastRow
DECLARE @tableName NVARCHAR(1024)
DECLARE @colName NVARCHAR(1024)
DECLARE @alterSql NVARCHAR(1024) = N''

WHILE @currentRow <> 0
BEGIN
    SELECT @tableName=TBL, @colName=COL from @xmlCols WHERE ID = @currentRow

    SET @alterSql = @alterSql + N'ALTER TABLE [dbo].[' + @tableName + N'] ALTER COLUMN ['+ @colName + N'] XML' + CHAR(13)

    SET @currentRow = @currentRow -1
END

SET @alterSql = @alterSql + N'DROP XML SCHEMA COLLECTION [dbo].[' + @schemaName + N']' + CHAR(13)

SET @currentRow = @lastRow
WHILE @currentRow <> 0
BEGIN
    SELECT @tableName=TBL, @colName=COL from @xmlCols WHERE ID = @currentRow

    SET @alterSql = @alterSql +  N'DBCC CLEANTABLE (''' + db_name() + N''', ''' + @tableName + N''')' + CHAR(13)

    set @currentRow = @currentRow -1
END

SET @alterSql = @alterSql + N'--------------------------------------------' + CHAR(13)
SET @alterSql = @alterSql + N'-------ADD SCHEMA CREATION HERE-------------' + CHAR(13)
SET @alterSql = @alterSql + N'--------------------------------------------' + CHAR(13)

SET @currentRow = @lastRow
WHILE @currentRow <> 0
BEGIN
    SELECT @tableName=TBL, @colName=COL from @xmlCols WHERE ID = @currentRow

    SET @alterSql = @alterSql + N'ALTER TABLE [dbo].[' + @tableName + N'] ALTER COLUMN ['+ @colName + N'] XML (DOCUMENT [dbo].[' + @schemaName + N'])' + CHAR(13)

    SET @currentRow = @currentRow -1
END

PRINT @alterSql

