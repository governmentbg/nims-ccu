GO

ALTER TABLE [dbo].[ProcedureBudgetLevel2]
    ADD
    [IsStandardTablesExpense]           BIT                 NOT NULL DEFAULT 0,
    [IsOneTimeExpense]                  BIT                 NOT NULL DEFAULT 0,
    [IsFlatRateExpense]                 BIT                 NOT NULL DEFAULT 0,
    [IsLandExpense]                     BIT                 NOT NULL DEFAULT 0;

GO
