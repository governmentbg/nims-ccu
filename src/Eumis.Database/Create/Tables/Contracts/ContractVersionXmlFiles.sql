PRINT 'ContractVersionXmlFiles'
GO

CREATE TABLE [dbo].[ContractVersionXmlFiles] (
    [ContractVersionXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractVersionXmlId]      INT                 NOT NULL,
    [Type]                      INT                 NOT NULL,
    [BlobKey]                   UNIQUEIDENTIFIER    NOT NULL,
    [Name]                      NVARCHAR(200)       NOT NULL,
    [Description]               NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractVersionXmlFiles]                       PRIMARY KEY ([ContractVersionXmlFileId]),
    CONSTRAINT [FK_ContractVersionXmlFiles_ContractVersionXmls]   FOREIGN KEY ([ContractVersionXmlId])    REFERENCES [dbo].[ContractVersionXmls] ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractVersionXmlFiles_Blobs]                 FOREIGN KEY ([BlobKey])                 REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ContractVersionXmlFiles_Type]                 CHECK ([Type] IN (1, 2))
);
GO

exec spDescTable  N'ContractVersionXmlFiles', N'Файлове към xml за договор.'
exec spDescColumn N'ContractVersionXmlFiles', N'ContractVersionXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractVersionXmlFiles', N'ContractVersionXmlId'    , N'Идентификатор на xml за договор.'
exec spDescColumn N'ContractVersionXmlFiles', N'Type'                    , N'Тип на файла: 1 - Прикачен документ; 2 - Ел. подписан договор.'
exec spDescColumn N'ContractVersionXmlFiles', N'BlobKey'                 , N'Идентификатор на файл.'
exec spDescColumn N'ContractVersionXmlFiles', N'Name'                    , N'Име на файл.'
exec spDescColumn N'ContractVersionXmlFiles', N'Description'             , N'Описание.'
GO
