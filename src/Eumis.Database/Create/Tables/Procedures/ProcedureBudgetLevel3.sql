PRINT 'ProcedureBudgetLevel3'
GO

CREATE TABLE [dbo].[ProcedureBudgetLevel3] (
    [ProcedureBudgetLevel3Id]                   INT                 NOT NULL IDENTITY,
    [ProcedureBudgetLevel2Id]                   INT                 NOT NULL,
    [Gid]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Note]                                      NVARCHAR(MAX)       NOT NULL,
    [OrderNum]                                  INT                 NOT NULL,
    CONSTRAINT [PK_ProcedureBudgetLevel3]                           PRIMARY KEY ([ProcedureBudgetLevel3Id]),
    CONSTRAINT [FK_ProcedureBudgetLevel3_ProcedureBudgetLevel2]     FOREIGN KEY ([ProcedureBudgetLevel2Id])    REFERENCES [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id])
);
GO

exec spDescTable  N'ProcedureBudgetLevel3', N'Ред от бюджета на процедура на трето ниво.'
exec spDescColumn N'ProcedureBudgetLevel3', N'ProcedureBudgetLevel3Id'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureBudgetLevel3', N'ProcedureBudgetLevel2Id'     , N'Идентификатор на ред от бюджета на процедура на второ ниво.'
exec spDescColumn N'ProcedureBudgetLevel3', N'Gid'                                     , N'Глобален уникален идентификатор.'
exec spDescColumn N'ProcedureBudgetLevel3', N'Note'                                    , N'Описание на конкретния разход'
exec spDescColumn N'ProcedureBudgetLevel3', N'OrderNum'                                , N'Пореден номер в бюджета за сортиране на разходите в бюджета.'
GO