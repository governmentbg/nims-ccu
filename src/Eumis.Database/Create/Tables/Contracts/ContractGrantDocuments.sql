PRINT 'ContractGrantDocuments'
GO

CREATE TABLE [dbo].[ContractGrantDocuments] (
    [ContractGrantDocumentId]                   INT                 NOT NULL IDENTITY,
    [ContractId]                                INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ContractGrantDocuments]                               PRIMARY KEY ([ContractGrantDocumentId]),
    CONSTRAINT [FK_ContractGrantDocuments_Contracts]                     FOREIGN KEY ([ContractId])      REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractGrantDocuments_Blobs]                         FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ContractGrantDocuments', N'Документи за БФП към договор.'
exec spDescColumn N'ContractGrantDocuments', N'ContractGrantDocumentId'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractGrantDocuments', N'ContractId'                          , N'Идентификатор на договор.'
exec spDescColumn N'ContractGrantDocuments', N'Name'                                , N'Наименование.'
exec spDescColumn N'ContractGrantDocuments', N'Description'                         , N'Описание.'
exec spDescColumn N'ContractGrantDocuments', N'BlobKey'                             , N'Идентификатор на файл.'

GO
