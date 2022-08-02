PRINT 'Create vwBlobContentPartitions'
GO

IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwBlobContentPartitions'))
  DROP VIEW vwBlobContentPartitions
GO

CREATE VIEW vwBlobContentPartitions
AS

SELECT
    p.partition_number
  , rv.value
  , fg.name as filegroup
  , fg.is_read_only
  , p.rows
FROM sys.partitions p
  INNER JOIN sys.indexes i ON p.object_id = i.object_id AND p.index_id = i.index_id
  INNER JOIN sys.partition_schemes ps ON ps.data_space_id = i.data_space_id
  INNER JOIN sys.partition_functions f ON f.function_id = ps.function_id
  INNER JOIN sys.destination_data_spaces dds ON dds.partition_scheme_id = ps.data_space_id AND dds.destination_id = p.partition_number
  INNER JOIN sys.filegroups fg ON dds.data_space_id = fg.data_space_id
  LEFT OUTER JOIN sys.partition_range_values rv ON f.function_id = rv.function_id AND p.partition_number = rv.boundary_id
WHERE i.index_id = 1 AND i.object_id = OBJECT_ID('BlobContents')

GO
