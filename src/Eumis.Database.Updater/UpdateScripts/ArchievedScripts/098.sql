GO

ALTER TABLE [dbo].[ProcedureBudgetLevel2]
    ADD
    [IsEuApprovedStandardTablesExpense] BIT                 NOT NULL DEFAULT 0,
    [IsEuApprovedOneTimeExpense]        BIT                 NOT NULL DEFAULT 0
GO
