PRINT 'ProcedureShares'
GO

CREATE TABLE [dbo].[ProcedureShares] (
    [ProcedureShareId]          INT             NOT NULL IDENTITY,
    [ProcedureId]               INT             NOT NULL,
    [ProgrammeId]               INT             NOT NULL,
    [ProgrammePriorityId]       INT             NOT NULL,
    [FinanceSource]             INT             NULL,
    [EuAmount]                  MONEY           NOT NULL,
    [BgAmount]                  MONEY           NOT NULL,
    [IsPrimary]                 BIT             NOT NULL,
    [IsActivated]               BIT             NOT NULL,

    CONSTRAINT [PK_ProcedureShares]                         PRIMARY KEY ([ProcedureShareId]),
    CONSTRAINT [FK_ProcedureShares_Procedures]              FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureShares_ProgrammePriorities]     FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProcedureShares_Programmes]              FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes] ([MapNodeId]),

    CONSTRAINT [CHK_ProcedureShares_FinanceSource]          CHECK ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
);
GO

exec spDescTable  N'ProcedureShares', N'Дял на процедура.'
exec spDescColumn N'ProcedureShares', N'ProcedureShareId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureShares', N'ProcedureId'            , N'Идентификатор на процедура'
exec spDescColumn N'ProcedureShares', N'ProgrammeId'            , N'Идентификатор на оперативна програма'
exec spDescColumn N'ProcedureShares', N'ProgrammePriorityId'    , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ProcedureShares', N'FinanceSource'          , N'Източник на финансиране(ограничава се от Бюджети на приоритетна ос).'
exec spDescColumn N'ProcedureShares', N'EuAmount'               , N'Стойност финансиране ЕС.'
exec spDescColumn N'ProcedureShares', N'BgAmount'               , N'Стойност национално финансиране.'
exec spDescColumn N'ProcedureShares', N'IsPrimary'              , N'Маркер, за водеща програма.'
GO
