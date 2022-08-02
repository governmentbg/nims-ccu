PRINT 'NewsFiles'
GO

CREATE TABLE [dbo].[NewsFiles] (
    [NewsFileId]           INT                 NOT NULL IDENTITY,
    [NewsId]               INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(200)       NOT NULL,

    CONSTRAINT [PK_NewsFiles]        PRIMARY KEY ([NewsFileId]),
    CONSTRAINT [FK_NewsFiles_News]   FOREIGN KEY ([NewsId])         REFERENCES [dbo].[News] ([NewsId]),
    CONSTRAINT [FK_NewsFiles_Blobs]  FOREIGN KEY ([BlobKey])        REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'NewsFiles', N'Файлове към новина.'
exec spDescColumn N'NewsFiles', N'NewsFileId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NewsFiles', N'NewsId'         , N'Идентификатор на новина.'
exec spDescColumn N'NewsFiles', N'BlobKey'        , N'Идентификатор на файл.'
exec spDescColumn N'NewsFiles', N'Name'           , N'Име на файл.'
exec spDescColumn N'NewsFiles', N'Description'    , N'Описание.'
GO
