GO

ALTER TABLE [dbo].[ContractDebtVersions] ADD [ExecutionStatus]      INT      NULL
GO

ALTER TABLE [dbo].[ContractDebtVersions]
ADD CONSTRAINT [CHK_ContractDebtVersions_ExecutionStatus] CHECK ([ExecutionStatus] IN (1, 2, 3, 4, 5, 6, 7, 8));
GO

UPDATE cdv
SET cdv.[ExecutionStatus] = cd.[ExecutionStatus]
FROM [dbo].[ContractDebtVersions] cdv
JOIN [dbo].[ContractDebts] cd on cdv.[ContractDebtId] = cd.[ContractDebtId]
GO

ALTER TABLE [ContractDebts] ALTER COLUMN [ExecutionStatus] INT NULL
GO