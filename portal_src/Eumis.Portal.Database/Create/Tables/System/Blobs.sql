PRINT 'Blobs'
GO

CREATE TABLE [dbo].[Blobs] (
    [Key]           UNIQUEIDENTIFIER            NOT NULL UNIQUE,
    [Hash]          NVARCHAR(40)                NULL,
    [Size]          INT                         NULL,
    CONSTRAINT [PK_Blobs] PRIMARY KEY CLUSTERED ([Key] ASC)
);
GO

CREATE UNIQUE INDEX [UQ_Blobs_Hash_Size]
    ON [dbo].[Blobs]([Hash], [Size]) WHERE [Hash] IS NOT NULL AND [Size] IS NOT NULL
GO

exec spDescTable  N'Blobs', N'Каталог на файлово съдържание.'
exec spDescColumn N'Blobs', N'Key', N'Уникален идентификатор за извличане на файловото съдържание.'
exec spDescColumn N'Blobs', N'Hash', N'Уникален идентификатор на съдържанието на файла.'
exec spDescColumn N'Blobs', N'Size', N'Размер на съдържанието.'
GO
