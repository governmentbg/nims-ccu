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

CREATE TABLE [dbo].[ContractProcurementDocuments] (
    [ContractProcurementDocumentId]                        INT                 NOT NULL IDENTITY,
    [ContractId]                                INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ContractProcurementDocuments]                               PRIMARY KEY ([ContractProcurementDocumentId]),
    CONSTRAINT [FK_ContractProcurementDocuments_Contracts]                     FOREIGN KEY ([ContractId])      REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractProcurementDocuments_Blobs]                         FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO
