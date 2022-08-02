PRINT 'ProcedureInvestmentPriorities'
GO

CREATE TABLE [dbo].[ProcedureInvestmentPriorities] (
    [ProcedureId]           INT             NOT NULL,
    [InvestmentPriorityId]  INT             NOT NULL,
    CONSTRAINT [PK_ProcedureInvestmentPriorities]                           PRIMARY KEY ([ProcedureId], [InvestmentPriorityId]),
    CONSTRAINT [FK_ProcedureInvestmentPriorities_Procedures]                FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureInvestmentPriorities_MapNodes]                  FOREIGN KEY ([InvestmentPriorityId])    REFERENCES [dbo].[MapNodes] ([MapNodeId])
);
GO

exec spDescTable  N'ProcedureInvestmentPriorities', N'Инвестиционни приоритети за процедура.'
exec spDescColumn N'ProcedureInvestmentPriorities', N'ProcedureId'            , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureInvestmentPriorities', N'InvestmentPriorityId'   , N'Идентификатор на инвестиционен приоритет.'

GO

