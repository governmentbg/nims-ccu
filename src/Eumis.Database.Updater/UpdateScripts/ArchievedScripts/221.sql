
GO

CREATE TABLE [dbo].[ContractProcurementPlanAdditionalDocuments] (
    [ContractProcurementPlanAdditionalDocumentId]   INT                 NOT NULL IDENTITY,
    [ContractProcurementPlanId]                     INT                 NOT NULL,

    [BlobKey]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                                          NVARCHAR(MAX)       NOT NULL,
    [Description]                                   NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractProcurementPlanAdditionalDocuments]                                  PRIMARY KEY ([ContractProcurementPlanAdditionalDocumentId]),
    CONSTRAINT [FK_ContractProcurementPlanAdditionalDocuments_ContractProcurementPlans]         FOREIGN KEY ([ContractProcurementPlanId])   REFERENCES [dbo].[ContractProcurementPlans] ([ContractProcurementPlanId]),
    CONSTRAINT [FK_ContractProcurementPlanAdditionalDocuments_Blobs]                            FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
);
GO