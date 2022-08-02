GO

ALTER TABLE [dbo].[MapNodeBudgets]
ADD [NextThreeWithAdvances]    MONEY NOT NULL CONSTRAINT DEFAULT_WithAdvances    DEFAULT 0,
    [NextThreeWithoutAdvances] MONEY NOT NULL CONSTRAINT DEFAULT_WithoutAdvances DEFAULT 0;
GO

ALTER TABLE [dbo].[MapNodeBudgets]
DROP CONSTRAINT DEFAULT_WithAdvances,
     CONSTRAINT DEFAULT_WithoutAdvances;
GO