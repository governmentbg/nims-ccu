PRINT 'ContractProcurementPlanPublicDocuments'
GO

CREATE TABLE [dbo].[ContractProcurementPlanPublicDocuments] (
    [ContractProcurementPlanPublicDocumentId]  INT          NOT NULL IDENTITY,
    [ContractProcurementPlanId]         INT                 NOT NULL,

    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                              NVARCHAR(MAX)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractProcurementPlanPublicDocuments]                              PRIMARY KEY ([ContractProcurementPlanPublicDocumentId]),
    CONSTRAINT [FK_ContractProcurementPlanPublicDocuments_ContractProcurementPlans]     FOREIGN KEY ([ContractProcurementPlanId])   REFERENCES [dbo].[ContractProcurementPlans] ([ContractProcurementPlanId]),
    CONSTRAINT [FK_ContractProcurementPlanPublicDocuments_Blobs]                        FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ContractProcurementPlanPublicDocuments', N'Публични документи към процедура за избор на изпълнител.'
exec spDescColumn N'ContractProcurementPlanPublicDocuments', N'ContractProcurementPlanPublicDocumentId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractProcurementPlanPublicDocuments', N'ContractProcurementPlanId'                   , N'Идентификатор на процедура за избор на изпълнител.'

exec spDescColumn N'ContractProcurementPlanPublicDocuments', N'BlobKey'                                     , N'Идентификатор на файл.'
exec spDescColumn N'ContractProcurementPlanPublicDocuments', N'Name'                                        , N'Име на файл.'
exec spDescColumn N'ContractProcurementPlanPublicDocuments', N'Description'                                 , N'Описание.'
GO
