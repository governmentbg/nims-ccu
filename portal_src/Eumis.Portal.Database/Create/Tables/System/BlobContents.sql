PRINT 'BlobContents'
GO

CREATE TABLE [dbo].[BlobContents] (
    [Key]           UNIQUEIDENTIFIER            NOT NULL,
    [Hash]          NVARCHAR(40)                NULL,
    [Size]          INT                         NULL,
    [Content]       VARBINARY(MAX)              NULL,
    [IsDeleted]     BIT                         NOT NULL,
    CONSTRAINT [PK_BlobContents] PRIMARY KEY CLUSTERED ([Key] ASC)
);
GO

exec spDescTable  N'BlobContents', N'Файлово съдържание.'
exec spDescColumn N'BlobContents', N'Key', N'Уникален идентификатор за извличане на файловото съдържание.'
exec spDescColumn N'BlobContents', N'Hash', N'Уникален идентификатор на съдържанието на файла.'
exec spDescColumn N'BlobContents', N'Size', N'Размер на съдържанието.'
exec spDescColumn N'BlobContents', N'Content', N'Съдържание.'
exec spDescColumn N'BlobContents', N'IsDeleted', N'Маркер за изтриване.'
GO
