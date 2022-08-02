GO

CREATE TABLE [dbo].[ContractProcurementXmlFiles] (
    [ContractProcurementXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractProcurementXmlId]      INT                 NOT NULL,
    [Type]                          INT                 NOT NULL,
    [BlobKey]                       UNIQUEIDENTIFIER    NOT NULL,
    [Name]                          NVARCHAR(200)       NOT NULL,
    [Description]                   NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractProcurementXmlFiles]                           PRIMARY KEY ([ContractProcurementXmlFileId]),
    CONSTRAINT [FK_ContractProcurementXmlFiles_ContractProcurementXmls]   FOREIGN KEY ([ContractProcurementXmlId])    REFERENCES [dbo].[ContractProcurementXmls] ([ContractProcurementXmlId]),
    CONSTRAINT [FK_ContractProcurementXmlFiles_Blobs]                     FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ContractProcurementXmlFiles_Type]                     CHECK ([Type] IN (1, 2))
);
GO