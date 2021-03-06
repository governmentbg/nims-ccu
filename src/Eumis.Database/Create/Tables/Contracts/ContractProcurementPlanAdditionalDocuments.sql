PRINT 'ContractProcurementPlanAdditionalDocuments'

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

exec spDescTable  N'ContractProcurementPlanAdditionalDocuments', N'Допълнително документи към процедура за избор на изпълнител.'
exec spDescColumn N'ContractProcurementPlanAdditionalDocuments', N'ContractProcurementPlanAdditionalDocumentId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractProcurementPlanAdditionalDocuments', N'ContractProcurementPlanId'                       , N'Идентификатор на процедура за избор на изпълнител.'

exec spDescColumn N'ContractProcurementPlanAdditionalDocuments', N'BlobKey'                                         , N'Идентификатор на файл.'
exec spDescColumn N'ContractProcurementPlanAdditionalDocuments', N'Name'                                            , N'Име на файл.'
exec spDescColumn N'ContractProcurementPlanAdditionalDocuments', N'Description'                                     , N'Описание.'
GO
