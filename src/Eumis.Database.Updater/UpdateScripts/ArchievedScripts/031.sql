GO

ALTER TABLE [ExpenseTypes]
ADD [CreateDate]            DATETIME2           NOT NULL DEFAULT GETDATE(),
    [ModifyDate]            DATETIME2           NOT NULL DEFAULT GETDATE(),
    [Version]               ROWVERSION          NOT NULL;
GO
