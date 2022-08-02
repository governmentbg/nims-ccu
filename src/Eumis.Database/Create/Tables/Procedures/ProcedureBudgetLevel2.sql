PRINT 'ProcedureBudgetLevel2'
GO

CREATE TABLE [dbo].[ProcedureBudgetLevel2] (
    [ProcedureBudgetLevel2Id]           INT                 NOT NULL IDENTITY,
    [ProcedureShareId]                  INT                 NOT NULL,
    [ProcedureBudgetLevel1Id]           INT                 NOT NULL,
    [Name]                              NVARCHAR(MAX)       NOT NULL,
    [NameAlt]                           NVARCHAR(MAX)       NULL,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [OrderNum]                          INT                 NOT NULL,
    [AidMode]                           INT                 NOT NULL,
    [IsEligibleCost]                    BIT                 NOT NULL,
    [IsStandardTablesExpense]           BIT                 NOT NULL DEFAULT 0,
    [IsOneTimeExpense]                  BIT                 NOT NULL DEFAULT 0,
    [IsFlatRateExpense]                 BIT                 NOT NULL DEFAULT 0,
    [IsLandExpense]                     BIT                 NOT NULL DEFAULT 0,
    [IsEuApprovedStandardTablesExpense] BIT                 NOT NULL DEFAULT 0,
    [IsEuApprovedOneTimeExpense]        BIT                 NOT NULL DEFAULT 0,
    [IsActivated]                       BIT                 NOT NULL,
    [IsActive]                          BIT                 NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetLevel2]                        PRIMARY KEY ([ProcedureBudgetLevel2Id]),
    CONSTRAINT [CHK_ProcedureBudgetLevel2_AidMode]               CHECK ([AidMode] IN (1, 2, 3)),
    CONSTRAINT [FK_ProcedureBudgetLevel2_ProcedureShares]        FOREIGN KEY ([ProcedureShareId])           REFERENCES [dbo].[ProcedureShares] ([ProcedureShareId]),
    CONSTRAINT [FK_ProcedureBudgetLevel2_ProcedureBudgetLevel1]  FOREIGN KEY ([ProcedureBudgetLevel1Id])    REFERENCES [dbo].[ProcedureBudgetLevel1] ([ProcedureBudgetLevel1Id])
);
GO

exec spDescTable  N'ProcedureBudgetLevel2', N'Ред от бюджета на процедура на второ ниво.'
exec spDescColumn N'ProcedureBudgetLevel2', N'ProcedureBudgetLevel2Id'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureBudgetLevel2', N'ProcedureShareId'                 , N'Идентификатор на дял на процедура.'
exec spDescColumn N'ProcedureBudgetLevel2', N'ProcedureBudgetLevel1Id'          , N'Идентификатор на ред от бюджета на първо ниво.'
exec spDescColumn N'ProcedureBudgetLevel2', N'Name'                             , N'Описание на конкретния разход.'
exec spDescColumn N'ProcedureBudgetLevel2', N'NameAlt'                          , N'Описание на конкретния разход на друг език.'
exec spDescColumn N'ProcedureBudgetLevel2', N'Gid'                              , N'Глобален уникален идентификатор.'
exec spDescColumn N'ProcedureBudgetLevel2', N'OrderNum'                         , N'Пореден номер в бюджета за сортиране на разходите в бюджета.'
exec spDescColumn N'ProcedureBudgetLevel2', N'AidMode'                          , N'Режим на помощта : 1 - deminimis, 2 - Държавна помощ, 3 - Неприложимо'
exec spDescColumn N'ProcedureBudgetLevel2', N'IsEligibleCost'                   , N'0 - Недопустим, 1 - Допустим'
exec spDescColumn N'ProcedureBudgetLevel2', N'IsStandardTablesExpense'          , N'Разход въз основа на стандартни таблици на единични разходи'
exec spDescColumn N'ProcedureBudgetLevel2', N'IsOneTimeExpense'                 , N'Разход въз основа на еднократни суми'
exec spDescColumn N'ProcedureBudgetLevel2', N'IsFlatRateExpense'                , N'Разход въз основа на единни ставки'
exec spDescColumn N'ProcedureBudgetLevel2', N'IsLandExpense'                    , N'Разходът е за закупуване на земя'
exec spDescColumn N'ProcedureBudgetLevel2', N'IsEuApprovedStandardTablesExpense', N'Разход въз основа на стандартни таблици на единичните разходи, одобрени от ЕК'
exec spDescColumn N'ProcedureBudgetLevel2', N'IsEuApprovedOneTimeExpense'       , N'Разход въз основа на еднократни общи суми, одобрени от ЕК'

GO
