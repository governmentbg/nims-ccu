PRINT 'CheckSheetVersionXmlFiles'
GO

CREATE TABLE [dbo].[CheckSheetVersionXmlFiles] (
    [CheckSheetVersionXmlFileId]       INT                 NOT NULL IDENTITY,
    [CheckSheetVersionXmlId]           INT                 NOT NULL,
    [BlobKey]                          UNIQUEIDENTIFIER    NOT NULL,
    [Name]                             NVARCHAR(200)       NOT NULL,
    [Description]                      NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_CheckSheetVersionXmlFiles]                                 PRIMARY KEY ([CheckSheetVersionXmlFileId]),
    CONSTRAINT [FK_CheckSheetVersionXmlFiles_CheckSheetVersionXmls]           FOREIGN KEY ([CheckSheetVersionXmlId])    REFERENCES [dbo].[CheckSheetVersionXmls] ([CheckSheetVersionXmlId]),
    CONSTRAINT [FK_CheckSheetVersionXmlFiles_Blobs]                           FOREIGN KEY ([BlobKey])                   REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'CheckSheetVersionXmlFiles', N'Файлове към xml за контролен лист.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'CheckSheetVersionXmlFileId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'CheckSheetVersionXmlId'           , N'Идентификатор на xml за оценителна таблица.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'BlobKey'                          , N'Идентификатор на файл.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'Name'                             , N'Име на файл.'
exec spDescColumn N'CheckSheetVersionXmlFiles', N'Description'                      , N'Описание.'
GO
