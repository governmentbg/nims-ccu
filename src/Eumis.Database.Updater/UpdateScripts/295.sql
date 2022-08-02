GO
ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [CHK_Contracts_ContractType]
GO
ALTER TABLE [dbo].[Contracts] WITH CHECK ADD CONSTRAINT [CHK_Contracts_ContractType] CHECK ([ContractType] IN (1, 2, 3, 4, 5, 6, 7, 8))
GO
