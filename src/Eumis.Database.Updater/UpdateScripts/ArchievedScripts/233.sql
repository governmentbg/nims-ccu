ALTER TABLE [dbo].[ContractVersionXmls] DROP CONSTRAINT [CHK_ContractXmls_VersionType]
GO

ALTER TABLE [dbo].[ContractVersionXmls] WITH CHECK ADD CONSTRAINT [CHK_ContractXmls_VersionType] CHECK ([VersionType] IN (1, 2, 3, 4, 5))
GO
