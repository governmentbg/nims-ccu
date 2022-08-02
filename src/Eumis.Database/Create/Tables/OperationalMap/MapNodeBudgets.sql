PRINT 'MapNodeBudgets'
GO

CREATE TABLE [dbo].[MapNodeBudgets](
    [MapNodeId]                INT             NOT NULL,
    [BudgetPeriodId]           INT             NOT NULL,
    [ProgrammeId]              INT             NOT NULL,
    [FinanceSource]            INT             NOT NULL,
    [EuAmount]                 MONEY           NOT NULL,
    [BgAmount]                 MONEY           NOT NULL,
    [EuReservedAmount]         MONEY           NOT NULL,
    [BgReservedAmount]         MONEY           NOT NULL,
    [NextThreeWithAdvances]    MONEY           NOT NULL,
    [NextThreeWithoutAdvances] MONEY           NOT NULL,

    CONSTRAINT [PK_MapNodeBudgets]                       PRIMARY KEY     ([MapNodeId], [BudgetPeriodId], [FinanceSource]),
    CONSTRAINT [FK_MapNodeBudgets_MapNodes]              FOREIGN KEY     ([MapNodeId])                     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeBudgets_BudgetPeriods]         FOREIGN KEY     ([BudgetPeriodId])                REFERENCES [dbo].[BudgetPeriods]         ([BudgetPeriodId]),
    CONSTRAINT [FK_MapNodeBudgets_Programmes]            FOREIGN KEY     ([ProgrammeId])                   REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeBudgets_MapNodeFinanceSources] FOREIGN KEY     ([ProgrammeId], [FinanceSource])  REFERENCES [dbo].[MapNodeFinanceSources] ([MapNodeId], [FinanceSource]),
    CONSTRAINT [CHK_MapNodeBudgets_FinanceSource]        CHECK           ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
);
GO

exec spDescTable  N'MapNodeBudgets', N'Бюджет на елемент на оперативна карта.'
exec spDescColumn N'MapNodeBudgets', N'MapNodeId'                 , N'Идентификатор на елемент на оперативна карта.'
exec spDescColumn N'MapNodeBudgets', N'BudgetPeriodId'            , N'Идентификатор на бюджетен период.'
exec spDescColumn N'MapNodeBudgets', N'ProgrammeId'               , N'Идентификатор на програма.'
exec spDescColumn N'MapNodeBudgets', N'FinanceSource'             , N'Идентификатор на източник на финансиране.'
exec spDescColumn N'MapNodeBudgets', N'EuAmount'                  , N'Размер на финансиране от ЕС.'
exec spDescColumn N'MapNodeBudgets', N'BgAmount'                  , N'Размер на финансиране от Нац.фонд'
exec spDescColumn N'MapNodeBudgets', N'EuReservedAmount'          , N'Размер на резервен план от ЕС.'
exec spDescColumn N'MapNodeBudgets', N'BgReservedAmount'          , N'Размер на резервен план от Нац.фонд.'
exec spDescColumn N'MapNodeBudgets', N'NextThreeWithAdvances'     , N'n+3 с аванс.'
exec spDescColumn N'MapNodeBudgets', N'NextThreeWithoutAdvances'  , N'n+3 без аванс.'
GO
