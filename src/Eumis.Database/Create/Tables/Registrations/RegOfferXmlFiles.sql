PRINT 'RegOfferXmlFiles'
GO

CREATE TABLE [dbo].[RegOfferXmlFiles] (
    [RegOfferXmlFileId]    INT                 NOT NULL IDENTITY,
    [RegOfferXmlId]        INT                 NOT NULL,
    [Type]                 INT                 NOT NULL,
    [BlobKey]              UNIQUEIDENTIFIER    NOT NULL,
    [Name]                 NVARCHAR(200)       NOT NULL,
    [Description]          NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_RegOfferXmlFiles]                  PRIMARY KEY ([RegOfferXmlFileId]),
    CONSTRAINT [FK_RegOfferXmlFiles_RegOfferXmls]     FOREIGN KEY ([RegOfferXmlId])      REFERENCES [dbo].[RegOfferXmls] ([RegOfferXmlId]),
    CONSTRAINT [FK_RegOfferXmlFiles_Blobs]            FOREIGN KEY ([BlobKey])            REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_RegOfferXmlFiles_Type]            CHECK ([Type] IN (1, 2))
);
GO

exec spDescTable  N'RegOfferXmlFiles', N'Файлове към xml за оферта към регистрация.'
exec spDescColumn N'RegOfferXmlFiles', N'RegOfferXmlFileId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RegOfferXmlFiles', N'RegOfferXmlId'      , N'Идентификатор на xml за проектно предложение.'
exec spDescColumn N'RegOfferXmlFiles', N'Type'               , N'Тип на файла: 1 - Прикачен файл; 2 - Signature на прикачен файл.'
exec spDescColumn N'RegOfferXmlFiles', N'BlobKey'            , N'Идентификатор на файл.'
exec spDescColumn N'RegOfferXmlFiles', N'Name'               , N'Име на файл.'
exec spDescColumn N'RegOfferXmlFiles', N'Description'        , N'Описание.'
GO
