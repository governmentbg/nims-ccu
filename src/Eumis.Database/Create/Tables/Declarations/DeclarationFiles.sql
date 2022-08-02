PRINT 'DeclarationFiles'
GO

CREATE TABLE [dbo].[DeclarationFiles] (
    [DeclarationFileId]    INT                 NOT NULL IDENTITY,
    [DeclarationId]        INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(200)       NOT NULL,

    CONSTRAINT [PK_DeclarationFiles]                PRIMARY KEY ([DeclarationFileId]),
    CONSTRAINT [FK_DeclarationFiles_Declarations]   FOREIGN KEY ([DeclarationId])         REFERENCES [dbo].[Declarations] ([DeclarationId]),
    CONSTRAINT [FK_DeclarationFiles_Blobs]          FOREIGN KEY ([BlobKey])               REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'DeclarationFiles', N'Файлове към декларация.'
exec spDescColumn N'DeclarationFiles', N'DeclarationFileId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'DeclarationFiles', N'DeclarationId'         , N'Идентификатор на декларация.'
exec spDescColumn N'DeclarationFiles', N'BlobKey'               , N'Идентификатор на файл.'
exec spDescColumn N'DeclarationFiles', N'Name'                  , N'Име на файл.'
exec spDescColumn N'DeclarationFiles', N'Description'           , N'Описание.'
GO
