USE [$(dbName)]
GO

PRINT 'Create switch table BlobContents_$(partitionId)'
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BlobContents_$(partitionId)')
  BEGIN
    CREATE TABLE [dbo].[BlobContents_$(partitionId)] (
        [BlobContentId] BIGINT                      NOT NULL,
        [PartitionId]   INT                         NOT NULL,
        [Hash]          NVARCHAR(64)                NULL,
        [Size]          BIGINT                      NULL,
        [Content]       VARBINARY(MAX)              NULL,
        [IsDeleted]     BIT                         NOT NULL,
        [CreateDate]    DATETIME2                   NOT NULL,
        [DeleteDate]    DATETIME2                   NULL,
        CONSTRAINT [PK_BlobContents_$(partitionId)] PRIMARY KEY CLUSTERED ([BlobContentId] ASC, [PartitionId] ASC),
        CONSTRAINT [CHK_BlobContents_PartitionId_$(partitionId)] CHECK ([PartitionId] = $(partitionId))
    )
    ON [$(fileGroupName)]
  END
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'UQ_BlobContents_Hash_Size')
  BEGIN
    PRINT 'IMPORTANT!!! Dropping [UQ_BlobContents_Hash_Size]. Recreate it manually after copying all partitions.'
    DROP INDEX [UQ_BlobContents_Hash_Size] ON [dbo].[BlobContents]
  END
GO
