GO

ALTER TABLE [dbo].[CheckSheets]
ADD [ContractReportFinancialRevalidationId]      INT     NULL,
    [ContractReportFinancialCorrectionId]        INT     NULL,
    [ContractReportCorrectionId]                 INT     NULL,
    [ContractReportRevalidationId]               INT     NULL,

CONSTRAINT [FK_CheckSheets_ContractReportFinancialRevalidations]
FOREIGN KEY ([ContractReportFinancialRevalidationId])
REFERENCES [dbo].[ContractReportFinancialRevalidations] ([ContractReportFinancialRevalidationId]),

CONSTRAINT [FK_CheckSheets_ContractReportFinancialCorrections]
FOREIGN KEY ([ContractReportFinancialCorrectionId])
REFERENCES [dbo].[ContractReportFinancialCorrections] ([ContractReportFinancialCorrectionId]),

CONSTRAINT [FK_CheckSheets_ContractReportCorrections]
FOREIGN KEY ([ContractReportCorrectionId])
REFERENCES [dbo].[ContractReportCorrections] ([ContractReportCorrectionId]),

CONSTRAINT [FK_CheckSheets_ContractReportRevalidations]
FOREIGN KEY ([ContractReportRevalidationId])
REFERENCES [dbo].[ContractReportRevalidations] ([ContractReportRevalidationId]);
GO

ALTER TABLE [dbo].[CheckSheets] DROP CONSTRAINT [CHK_CheckSheets_Type]
GO
ALTER TABLE [dbo].[CheckSheets] WITH CHECK ADD CONSTRAINT [CHK_CheckSheets_Type] CHECK ([Type] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
GO

ALTER TABLE [dbo].[ProgrammeCheckLists] DROP CONSTRAINT [CHK_ProgrammeCheckLists_Type]
GO
ALTER TABLE [dbo].[ProgrammeCheckLists] WITH CHECK ADD CONSTRAINT [CHK_ProgrammeCheckLists_Type] CHECK ([Type] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
GO
