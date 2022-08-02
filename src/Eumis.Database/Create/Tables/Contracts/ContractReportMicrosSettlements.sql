PRINT 'ContractReportMicrosSettlements'
GO 

CREATE TABLE [dbo].[ContractReportMicrosSettlements] (
    [ContractReportMicrosSettlementId]   INT             NOT NULL IDENTITY,
    [ContractReportMicrosMunicipalityId] INT             NOT NULL,
    [SettlementId]                       INT             NOT NULL,
    [Name]                               NVARCHAR(200)   NOT NULL,
    CONSTRAINT [PK_ContractReportMicrosSettlements]                                    PRIMARY KEY ([ContractReportMicrosSettlementId]),
    CONSTRAINT [FK_ContractReportMicrosSettlements_ContractReportMicrosMunicipalities] FOREIGN KEY ([ContractReportMicrosMunicipalityId]) REFERENCES [dbo].[ContractReportMicrosMunicipalities] ([ContractReportMicrosMunicipalityId]),
    CONSTRAINT [FK_ContractReportMicrosSettlements_Settlements]                        FOREIGN KEY ([SettlementId])                       REFERENCES [dbo].[Settlements]                        ([SettlementId])
)
GO

exec spDescTable  N'ContractReportMicrosSettlements', N'Населени места към микроданните.'
exec spDescColumn N'ContractReportMicrosSettlements', N'ContractReportMicrosSettlementId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportMicrosSettlements', N'ContractReportMicrosMunicipalityId', N'Идентификатор на община.'
exec spDescColumn N'ContractReportMicrosSettlements', N'SettlementId'                      , N'Идентификатор на населено място.'
exec spDescColumn N'ContractReportMicrosSettlements', N'Name'                              , N'Наименование.'
GO
