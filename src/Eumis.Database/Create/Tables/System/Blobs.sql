PRINT 'Blobs'
GO

CREATE TABLE [dbo].[Blobs] (
    [Key]                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [FileName]              NVARCHAR(200)       NOT NULL,
    [BlobContentLocationId] BIGINT              NOT NULL,
    [IsDeleted]             BIT                 NOT NULL,
    [CreateDate]            DATETIME2           NOT NULL,
    [DeleteDate]            DATETIME2           NULL,
    CONSTRAINT [PK_Blobs]                       PRIMARY KEY CLUSTERED ([Key] ASC),
    CONSTRAINT [FK_Blobs_BlobContentLocations]  FOREIGN KEY ([BlobContentLocationId])  REFERENCES [dbo].[BlobContentLocations] ([BlobContentLocationId])
);
GO

exec spDescTable  N'Blobs', N'Каталог на файлове.'
exec spDescColumn N'Blobs', N'Key', N'Уникален идентификатор за извличане на файла.'
exec spDescColumn N'Blobs', N'FileName', N'Име на файл.'
exec spDescColumn N'Blobs', N'BlobContentLocationId', N'Уникален идентификатор на файловото съдържание.'
exec spDescColumn N'Blobs', N'IsDeleted', N'Маркер за изтриване.'
exec spDescColumn N'Blobs', N'CreateDate', N'Дата на създаване на записа.'
exec spDescColumn N'Blobs', N'DeleteDate', N'Дата на изтриване на записа.'
GO
