ALTER TABLE [dbo].[BlobContents] ADD
    [PartitionId]   INT         NOT NULL CONSTRAINT [DEFAULT_PartitionId] DEFAULT 0,
    [CreateDate]    DATETIME2   NOT NULL CONSTRAINT [DEFAULT_CreateDate]  DEFAULT GETDATE(),
    [DeleteDate]    DATETIME2                   NULL
GO

ALTER TABLE [dbo].[BlobContents] DROP CONSTRAINT [DEFAULT_PartitionId]
GO

ALTER TABLE [dbo].[BlobContents] DROP CONSTRAINT [DEFAULT_CreateDate]
GO

UPDATE [dbo].[BlobContents]
SET [PartitionId] = 100 + [BlobContentId] % 16
WHERE [PartitionId] = 0
GO

DROP INDEX [UQ_BlobContents_Hash_Size] ON [dbo].[BlobContents]
GO

ALTER TABLE [dbo].[BlobContents] ADD
    CONSTRAINT [CHK_BlobContents_PartitionId] CHECK ([PartitionId] BETWEEN 100 AND 147)
GO

CREATE UNIQUE INDEX [UQ_BlobContents_Hash_Size]
    ON [dbo].[BlobContents]([Hash], [Size]) WHERE [IsDeleted] = 0 AND [Hash] IS NOT NULL AND [Size] IS NOT NULL
GO
