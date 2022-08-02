PRINT 'ProjectShareDetailExpenseBudgets'
GO

CREATE TABLE [dbo].[ProjectShareDetailExpenseBudgets] (
    [ProjectShareDetailExpenseBudgetId]     INT                 NOT NULL IDENTITY,
    [ProcedureBudgetLevel2Id]               INT                 NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProjectId]                             INT                 NOT NULL,
    [Note]                                  NVARCHAR(MAX)       NOT NULL,
    [Amount]                                MONEY               NOT NULL,
    [OrderNum]                              INT                 NOT NULL,
    CONSTRAINT [PK_ProjectShareDetailExpenseBudgets]                               PRIMARY KEY ([ProjectShareDetailExpenseBudgetId]),
    CONSTRAINT [FK_ProjectShareDetailExpenseBudgets_Projects]                      FOREIGN KEY ([ProjectId])                                REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectShareDetailExpenseBudgets_ProcedureBudgetLevel2Id]       FOREIGN KEY ([ProcedureBudgetLevel2Id])     REFERENCES [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id])
);
GO

exec spDescTable  N'ProjectShareDetailExpenseBudgets', N'Бюджет на проектно предложение - Ред от бюджета на процедура на трето ниво.'
exec spDescColumn N'ProjectShareDetailExpenseBudgets', N'ProjectShareDetailExpenseBudgetId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectShareDetailExpenseBudgets', N'ProcedureBudgetLevel2Id'                  , N'Идентификатор на подтип разход по елемент от бюджета.'
exec spDescColumn N'ProjectShareDetailExpenseBudgets', N'Gid'                                       , N'Глобален уникален идентификатор.'
exec spDescColumn N'ProjectShareDetailExpenseBudgets', N'ProjectId'                                 , N'Идентификатор на проектно предложние'
exec spDescColumn N'ProjectShareDetailExpenseBudgets', N'Note'                                      , N'Описание на конкретния разход.'
exec spDescColumn N'ProjectShareDetailExpenseBudgets', N'Amount'                                    , N'Стойност на разходите.'
exec spDescColumn N'ProjectShareDetailExpenseBudgets', N'OrderNum'                                  , N'Пореден номер в бюджета за сортиране на разходите в бюджета.'
GO
