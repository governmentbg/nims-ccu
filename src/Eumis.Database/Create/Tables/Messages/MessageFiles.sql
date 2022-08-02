PRINT 'MessageFiles'
GO

CREATE TABLE [dbo].[MessageFiles] (
    [MessageFileId]        INT                 NOT NULL IDENTITY,
    [MessageId]            INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(200)       NOT NULL,

    CONSTRAINT [PK_MessageFiles]            PRIMARY KEY ([MessageFileId]),
    CONSTRAINT [FK_MessageFiles_Messages]   FOREIGN KEY ([MessageId])      REFERENCES [dbo].[Messages] ([MessageId]),
    CONSTRAINT [FK_MessageFiles_Blobs]      FOREIGN KEY ([BlobKey])        REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'MessageFiles', N'Файлове към съобщение.'
exec spDescColumn N'MessageFiles', N'MessageFileId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MessageFiles', N'MessageId'      , N'Идентификатор на съобщение.'
exec spDescColumn N'MessageFiles', N'BlobKey'        , N'Идентификатор на файл.'
exec spDescColumn N'MessageFiles', N'Name'           , N'Име на файл.'
exec spDescColumn N'MessageFiles', N'Description'    , N'Описание.'
GO
