GO

IF EXISTS (SELECT [ContractDebtId] FROM [dbo].[ContractDebts])
BEGIN
    THROW 50000,'Cannot update database. There must be no ContractDebts',1;
END
GO

IF EXISTS (SELECT [ReimbursedAmountId] FROM [dbo].[ReimbursedAmounts] WHERE [Discriminator] = 1)
BEGIN
    THROW 50000,'Cannot update database. There must be no ReimbursedAmounts',1;
END
GO


ALTER TABLE [dbo].[ContractDebts]
ADD [ProgrammePriorityId]     INT           NOT NULL,
    [FinanceSource]           INT           NOT NULL
CONSTRAINT [FK_ContractDebts_ProgrammePriorities]  FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]             ([MapNodeId]),
CONSTRAINT [CHK_ContractDebts_FinanceSource]       CHECK ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
GO

ALTER TABLE [dbo].[ReimbursedAmounts]
ADD [ProgrammePriorityId]     INT           NOT NULL,
    [FinanceSource]           INT           NOT NULL
CONSTRAINT [FK_ReimbursedAmounts_ProgrammePriorities]    FOREIGN KEY ([ProgrammePriorityId]) REFERENCES [dbo].[MapNodes]      ([MapNodeId]),
CONSTRAINT [CHK_ReimbursedAmounts_FinanceSource]         CHECK       ([FinanceSource]  IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
GO
