PRINT 'ContractProcurementDocuments'
GO

CREATE TABLE [dbo].[ContractProcurementDocuments] (
    [ContractProcurementDocumentId]             INT                 NOT NULL IDENTITY,
    [ContractId]                                INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ContractProcurementDocuments]                               PRIMARY KEY ([ContractProcurementDocumentId]),
    CONSTRAINT [FK_ContractProcurementDocuments_Contracts]                     FOREIGN KEY ([ContractId])      REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractProcurementDocuments_Blobs]                         FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ContractProcurementDocuments', N'Документи към договор.'
exec spDescColumn N'ContractProcurementDocuments', N'ContractProcurementDocumentId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractProcurementDocuments', N'ContractId'                        , N'Идентификатор на договор.'
exec spDescColumn N'ContractProcurementDocuments', N'Name'                              , N'Наименование.'
exec spDescColumn N'ContractProcurementDocuments', N'Description'                       , N'Описание.'
exec spDescColumn N'ContractProcurementDocuments', N'BlobKey'                           , N'Идентификатор на файл.'

GO
