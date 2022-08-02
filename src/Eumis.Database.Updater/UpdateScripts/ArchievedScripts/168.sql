GO

ALTER TABLE [dbo].[IrregularityVersions]
DROP CONSTRAINT [CHK_IrregularityVersions_ContractDebtStatus];
GO

ALTER TABLE [dbo].[IrregularityVersions]
ADD CONSTRAINT [CHK_IrregularityVersions_ContractDebtStatus] CHECK ([ContractDebtStatus] IN (1, 2, 3, 4, 5, 6, 7, 8));
GO