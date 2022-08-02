PRINT 'BlobContents'
GO

CREATE TABLE [dbo].[BlobContents] (
    [BlobContentId] BIGINT                      NOT NULL,
    [PartitionId]   INT                         NOT NULL,
    [Hash]          NVARCHAR(64)                NULL,
    [Size]          BIGINT                      NULL,
    [Content]       VARBINARY(MAX)              NULL,
    [IsDeleted]     BIT                         NOT NULL,
    [CreateDate]    DATETIME2                   NOT NULL,
    [DeleteDate]    DATETIME2                   NULL,
    CONSTRAINT [PK_BlobContents] PRIMARY KEY CLUSTERED ([BlobContentId] ASC, [PartitionId] ASC),
    CONSTRAINT [CHK_BlobContents_PartitionId] CHECK ([PartitionId] BETWEEN 100 AND 147)
)
ON psBlobContents([PartitionId]);
GO

CREATE UNIQUE INDEX [UQ_BlobContents_Hash_Size]
    ON [dbo].[BlobContents]([Hash], [Size]) WHERE [IsDeleted] = 0 AND [Hash] IS NOT NULL AND [Size] IS NOT NULL
ON [FG_Index]
GO

exec spDescTable  N'BlobContents', N'Файлово съдържание.'
exec spDescColumn N'BlobContents', N'BlobContentId', N'Уникален идентификатор за извличане на файловото съдържание.'
exec spDescColumn N'BlobContents', N'PartitionId', N'Номер на партишън.'
exec spDescColumn N'BlobContents', N'Hash', N'Уникален идентификатор на съдържанието на файла.'
exec spDescColumn N'BlobContents', N'Size', N'Размер на съдържанието.'
exec spDescColumn N'BlobContents', N'Content', N'Съдържание.'
exec spDescColumn N'BlobContents', N'IsDeleted', N'Маркер за изтриване.'
exec spDescColumn N'BlobContents', N'CreateDate', N'Дата на създаване на записа.'
exec spDescColumn N'BlobContents', N'DeleteDate', N'Дата на изтриване на записа.'
GO
