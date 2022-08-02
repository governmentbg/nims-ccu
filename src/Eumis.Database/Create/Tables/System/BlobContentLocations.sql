PRINT 'BlobContentLocations'
GO

CREATE TABLE [dbo].[BlobContentLocations] (
    [BlobContentLocationId] BIGINT          NOT NULL IDENTITY,
    [BlobContentId]         BIGINT          NOT NULL,
    [PartitionId]           INT             NOT NULL,
    [ContentDbCSName]       NVARCHAR(100)   NOT NULL,
    [Hash]                  NVARCHAR(64)    NOT NULL,
    [Size]                  BIGINT          NOT NULL,
    [IsDeleted]             BIT             NOT NULL,
    [CreateDate]            DATETIME2       NOT NULL,
    [DeleteDate]            DATETIME2       NULL,
    CONSTRAINT [PK_BlobContentLocations]            PRIMARY KEY CLUSTERED   ([BlobContentLocationId] ASC)
);
GO

CREATE UNIQUE INDEX [UQ_BlobContentLocations_Hash_Size]
    ON [dbo].[BlobContentLocations]([Hash], [Size]) WHERE [IsDeleted] = 0
GO

exec spDescTable  N'BlobContentLocations', N'Каталог на файлово съдържание.'
exec spDescColumn N'BlobContentLocations', N'BlobContentLocationId', N'Уникален идентификатор на файловото съдържание.'
exec spDescColumn N'BlobContentLocations', N'BlobContentId', N'Уникален идентификатор за извличане на файловото съдържание.'
exec spDescColumn N'BlobContentLocations', N'PartitionId', N'Номер на партишън.'
exec spDescColumn N'BlobContentLocations', N'ContentDbCSName', N'Местоположение на файловото съдържание.'
exec spDescColumn N'BlobContentLocations', N'Hash', N'Уникален идентификатор на съдържанието на файла.'
exec spDescColumn N'BlobContentLocations', N'Size', N'Размер на съдържанието.'
exec spDescColumn N'BlobContentLocations', N'IsDeleted', N'Маркер за изтриване.'
exec spDescColumn N'BlobContentLocations', N'CreateDate', N'Дата на създаване на записа.'
exec spDescColumn N'BlobContentLocations', N'DeleteDate', N'Дата на изтриване на записа.'
GO
