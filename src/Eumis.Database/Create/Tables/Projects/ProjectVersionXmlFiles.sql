PRINT 'ProjectVersionXmlFiles'
GO

CREATE TABLE [dbo].[ProjectVersionXmlFiles] (
    [ProjectVersionXmlFileId]  INT                 NOT NULL IDENTITY,
    [ProjectVersionXmlId]      INT                 NOT NULL,
    [Type]                     INT                 NOT NULL,
    [BlobKey]                  UNIQUEIDENTIFIER    NOT NULL,
    [Name]                     NVARCHAR(200)       NOT NULL,
    [Description]              NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ProjectVersionXmlFiles]                      PRIMARY KEY ([ProjectVersionXmlFileId]),
    CONSTRAINT [FK_ProjectVersionXmlFiles_ProjectVersionXmls]   FOREIGN KEY ([ProjectVersionXmlId])    REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [FK_ProjectVersionXmlFiles_Blobs]                FOREIGN KEY ([BlobKey])                REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ProjectVersionXmlFiles_Type]                CHECK ([Type] IN (1, 2))
);
GO

exec spDescTable  N'ProjectVersionXmlFiles', N'Файлове към xml за проектно предложение.'
exec spDescColumn N'ProjectVersionXmlFiles', N'ProjectVersionXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectVersionXmlFiles', N'ProjectVersionXmlId'    , N'Идентификатор на xml за проектно предложение.'
exec spDescColumn N'ProjectVersionXmlFiles', N'Type'                   , N'Тип на файла: 1 - Прикачен файл; 2 - Signature на прикачен файл.'
exec spDescColumn N'ProjectVersionXmlFiles', N'BlobKey'                , N'Идентификатор на файл.'
exec spDescColumn N'ProjectVersionXmlFiles', N'Name'                   , N'Име на файл.'
exec spDescColumn N'ProjectVersionXmlFiles', N'Description'            , N'Описание.'
GO
