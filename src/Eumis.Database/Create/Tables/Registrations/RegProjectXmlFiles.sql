PRINT 'RegProjectXmlFiles'
GO

CREATE TABLE [dbo].[RegProjectXmlFiles] (
    [RegProjectXmlFileId]  INT                 NOT NULL IDENTITY,
    [RegProjectXmlId]      INT                 NOT NULL,
    [Type]                 INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_RegProjectXmlFiles]                  PRIMARY KEY ([RegProjectXmlFileId]),
    CONSTRAINT [FK_RegProjectXmlFiles_RegProjectXmls]   FOREIGN KEY ([RegProjectXmlId])    REFERENCES [dbo].[RegProjectXmls] ([RegProjectXmlId]),
    CONSTRAINT [FK_RegProjectXmlFiles_Blobs]            FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_RegProjectXmlFiles_Type]            CHECK ([Type] IN (1, 2))
);
GO

exec spDescTable  N'RegProjectXmlFiles', N'Файлове към xml за проектно предложение.'
exec spDescColumn N'RegProjectXmlFiles', N'RegProjectXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RegProjectXmlFiles', N'RegProjectXmlId'    , N'Идентификатор на xml за проектно предложение.'
exec spDescColumn N'RegProjectXmlFiles', N'Type'               , N'Тип на файла: 1 - Прикачен файл; 2 - Signature на прикачен файл.'
exec spDescColumn N'RegProjectXmlFiles', N'BlobKey'            , N'Идентификатор на файл.'
exec spDescColumn N'RegProjectXmlFiles', N'Name'               , N'Име на файл.'
exec spDescColumn N'RegProjectXmlFiles', N'Description'        , N'Описание.'
GO
