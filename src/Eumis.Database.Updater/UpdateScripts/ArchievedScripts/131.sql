GO

ALTER TABLE [dbo].[SapPaidAmounts]
ADD [ProgrammePriorityId]       INT              NULL,
CONSTRAINT [FK_SapPaidAmounts_ProgrammePriorities] FOREIGN KEY ([ProgrammePriorityId]) REFERENCES [dbo].[MapNodes] ([MapNodeId]);
GO

ALTER TABLE [dbo].[ActuallyPaidAmounts]
ADD [ProgrammePriorityId]     INT           NOT NULL CONSTRAINT DEFAULT_ProgrammePriority DEFAULT 1,
    [FinanceSource]           INT           NOT NULL CONSTRAINT DEFAULT_FinanceSource     DEFAULT 1
CONSTRAINT [FK_ActuallyPaidAmounts_ProgrammePriorities] FOREIGN KEY ([ProgrammePriorityId]) REFERENCES [dbo].[MapNodes] ([MapNodeId]),
CONSTRAINT [CHK_ActuallyPaidAmounts_FinanceSource]      CHECK       ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
GO

UPDATE a
SET [ProgrammePriorityId] = ps.[ProgrammePriorityId],
    [FinanceSource] = ps.[FinanceSource]
FROM [dbo].[ActuallyPaidAmounts] a
JOIN [dbo].[Contracts] c ON a.[ContractId] = c.[ContractId]
JOIN [dbo].[ProcedureShares] ps ON c.[ProcedureId] = ps.[ProcedureId]
WHERE ps.[IsPrimary] = 1 AND ps.[ProgrammeId] = a.[ProgrammeId]

ALTER TABLE [dbo].[ActuallyPaidAmounts]
DROP CONSTRAINT DEFAULT_ProgrammePriority,
     CONSTRAINT DEFAULT_FinanceSource
GO
