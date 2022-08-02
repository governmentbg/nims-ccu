GO
ALTER TABLE [dbo].[ContractReportCorrections] WITH CHECK ADD CONSTRAINT [CHK_ContractReportCorrections_ClassAdvanceCovered] CHECK ([Type] != 6 OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL AND [ContractReportPaymentId] IS NOT NULL))
GO

ALTER TABLE [dbo].[ContractReportCorrections] DROP CONSTRAINT [CHK_ContractReportCorrections_Type]
GO
ALTER TABLE [dbo].[ContractReportCorrections] WITH CHECK ADD CONSTRAINT [CHK_ContractReportCorrections_Type] CHECK ([Type] IN (1, 2, 3, 4, 5, 6))
GO
