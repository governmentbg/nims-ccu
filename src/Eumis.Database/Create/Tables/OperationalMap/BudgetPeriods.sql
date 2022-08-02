PRINT 'BudgetPeriods'
GO

CREATE TABLE [dbo].[BudgetPeriods] (
    [BudgetPeriodId]    INT             NOT NULL IDENTITY,
    [Name]              NVARCHAR(MAX)   NOT NULL,
    [Year]              INT             NOT NULL,
    CONSTRAINT [PK_BudgetPeriods] PRIMARY KEY ([BudgetPeriodId])
);
GO

exec spDescTable  N'BudgetPeriods', N'Период на финансиране.'
exec spDescColumn N'BudgetPeriods', N'BudgetPeriodId'     , N'Уникален системно генериран идентификатор.'
