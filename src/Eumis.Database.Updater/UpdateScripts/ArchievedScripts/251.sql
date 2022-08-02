GO

ALTER TABLE [dbo].[FinancialCorrectionVersions] DROP CONSTRAINT [CHK_FinancialCorrectionVersions_AmendmentReason]
GO

ALTER TABLE [dbo].[FinancialCorrectionVersions] WITH CHECK ADD CONSTRAINT [CHK_FinancialCorrectionVersions_AmendmentReason] CHECK ([AmendmentReason] IN (1, 2, 3, 4, 5, 6, 7))
GO
