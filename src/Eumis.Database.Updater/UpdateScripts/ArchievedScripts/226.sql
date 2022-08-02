GO
ALTER TABLE [dbo].[ContractIndicators] DROP CONSTRAINT [CHK_ContractIndicators_FinanceSource];
GO
ALTER TABLE [dbo].[ContractIndicators] ADD CONSTRAINT [CHK_ContractIndicators_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ContractReportAdvanceNVPaymentAmounts] DROP CONSTRAINT [CHK_ContractReportAdvanceNVPaymentAmounts_FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportAdvanceNVPaymentAmounts] ADD CONSTRAINT [CHK_ContractReportAdvanceNVPaymentAmounts_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ContractReportAdvancePaymentAmounts] DROP CONSTRAINT [CHK_ContractReportAdvancePaymentAmounts_FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportAdvancePaymentAmounts] ADD CONSTRAINT [CHK_ContractReportAdvancePaymentAmounts_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ContractReportCertCorrections] DROP CONSTRAINT [CHK_ContractReportCertCorrections_FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportCertCorrections] ADD CONSTRAINT [CHK_ContractReportCertCorrections_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ContractReportCorrections] DROP CONSTRAINT [CHK_ContractReportCorrections_FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportCorrections] ADD CONSTRAINT [CHK_ContractReportCorrections_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

--Check constraint for FinanceSource is misssing in ContractReportPaymentCheckAmounts
ALTER TABLE [dbo].[ContractReportPaymentCheckAmounts] ADD CONSTRAINT [CHK_ContractReportPaymentCheckAmounts_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ContractReportRevalidations] DROP CONSTRAINT [CHK_ContractReportRevalidations_FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportRevalidations] ADD CONSTRAINT [CHK_ContractReportRevalidations_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ContractDebts] DROP CONSTRAINT [CHK_ContractDebts_FinanceSource];
GO
ALTER TABLE [dbo].[ContractDebts] ADD CONSTRAINT [CHK_ContractDebts_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[EuReimbursedAmounts] DROP CONSTRAINT [CHK_EuReimbursedAmounts_FinanceSource];
GO
ALTER TABLE [dbo].[EuReimbursedAmounts] ADD CONSTRAINT [CHK_EuReimbursedAmounts_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

--Check constraint for FinanceSource is misssing in FIReimbursedAmounts
ALTER TABLE [dbo].[FIReimbursedAmounts] ADD CONSTRAINT [CHK_FIReimbursedAmounts_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[Irregularities] DROP CONSTRAINT [CHK_Irregularities_FinanceSource];
GO
ALTER TABLE [dbo].[Irregularities] ADD CONSTRAINT [CHK_Irregularities_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ActuallyPaidAmounts] DROP CONSTRAINT [CHK_ActuallyPaidAmounts_FinanceSource];
GO
ALTER TABLE [dbo].[ActuallyPaidAmounts] ADD CONSTRAINT [CHK_ActuallyPaidAmounts_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[CompensationDocuments] DROP CONSTRAINT [CHK_CompensationDocuments_FinanceSource];
GO
ALTER TABLE [dbo].[CompensationDocuments] ADD CONSTRAINT [CHK_CompensationDocuments_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[Prognoses] DROP CONSTRAINT [CHK_Prognoses_FinanceSource];
GO
ALTER TABLE [dbo].[Prognoses] ADD CONSTRAINT [CHK_Prognoses_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ReimbursedAmounts] DROP CONSTRAINT [CHK_ReimbursedAmounts_FinanceSource];
GO
ALTER TABLE [dbo].[ReimbursedAmounts] ADD CONSTRAINT [CHK_ReimbursedAmounts_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[MapNodeBudgets] DROP CONSTRAINT [CHK_MapNodeBudgets_FinanceSource];
GO
ALTER TABLE [dbo].[MapNodeBudgets] ADD CONSTRAINT [CHK_MapNodeBudgets_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[MapNodeFinanceSources] DROP CONSTRAINT [CHK_MapNodeFinanceSources_FinanceSource];
GO
ALTER TABLE [dbo].[MapNodeFinanceSources] ADD CONSTRAINT [CHK_MapNodeFinanceSources_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[MapNodeIndicators] DROP CONSTRAINT [CHK_MapNodeIndicators_FinanceSource];
GO
ALTER TABLE [dbo].[MapNodeIndicators] ADD CONSTRAINT [CHK_MapNodeIndicators_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

ALTER TABLE [dbo].[ProcedureShares] DROP CONSTRAINT [CHK_ProcedureShares_FinanceSource];
GO
ALTER TABLE [dbo].[ProcedureShares] ADD CONSTRAINT [CHK_ProcedureShares_FinanceSource] CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO
