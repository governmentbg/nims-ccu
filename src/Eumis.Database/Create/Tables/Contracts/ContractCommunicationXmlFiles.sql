PRINT 'ContractCommunicationXmlFiles'
GO

CREATE TABLE [dbo].[ContractCommunicationXmlFiles] (
    [ContractCommunicationXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractCommunicationXmlId]      INT                 NOT NULL,
    [BlobKey]                         UNIQUEIDENTIFIER    NOT NULL,
    [Name]                            NVARCHAR(200)       NOT NULL,
    [Description]                     NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractCommunicationXmlFiles]                             PRIMARY KEY ([ContractCommunicationXmlFileId]),
    CONSTRAINT [FK_ContractCommunicationXmlFiles_ContractCommunicationXmls]   FOREIGN KEY ([ContractCommunicationXmlId])    REFERENCES [dbo].[ContractCommunicationXmls] ([ContractCommunicationXmlId]),
    CONSTRAINT [FK_ContractCommunicationXmlFiles_Blobs]                       FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'ContractCommunicationXmlFiles', N'Файлове към xml за комуникация с бенефициент.'
exec spDescColumn N'ContractCommunicationXmlFiles', N'ContractCommunicationXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractCommunicationXmlFiles', N'ContractCommunicationXmlId'    , N'Идентификатор на xml за комуникация с бенефициент.'
exec spDescColumn N'ContractCommunicationXmlFiles', N'BlobKey'                       , N'Идентификатор на файл.'
exec spDescColumn N'ContractCommunicationXmlFiles', N'Name'                          , N'Име на файл.'
exec spDescColumn N'ContractCommunicationXmlFiles', N'Description'                   , N'Описание.'
GO
