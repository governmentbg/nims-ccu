PRINT 'ProcedureBudgetLevel1'
GO

CREATE TABLE [dbo].[ProcedureBudgetLevel1] (
    [ProcedureBudgetLevel1Id]           INT                 NOT NULL IDENTITY,
    [ProcedureId]                       INT                 NOT NULL,
    [ProgrammeId]                       INT                 NOT NULL,
    [ExpenseTypeId]                     INT                 NOT NULL,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [OrderNum]                          INT                 NOT NULL,
    [IsActivated]                       BIT                 NOT NULL,
    [IsActive]                          BIT                 NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetLevel1]                       PRIMARY KEY ([ProcedureBudgetLevel1Id]),
    CONSTRAINT [FK_ProcedureBudgetLevel1_Procedures]            FOREIGN KEY ([ProcedureId])                  REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureBudgetLevel1_MapNodes]              FOREIGN KEY ([ProgrammeId])                  REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProcedureBudgetLevel1_ProcedureProgrammes]   FOREIGN KEY ([ProcedureId], [ProgrammeId])   REFERENCES [dbo].[ProcedureProgrammes] ([ProcedureId], [ProgrammeId]),
    CONSTRAINT [FK_ProcedureBudgetLevel1_ExpenseTypes]          FOREIGN KEY ([ExpenseTypeId])                REFERENCES [dbo].[ExpenseTypes] ([ExpenseTypeId])
);
GO

exec spDescTable  N'ProcedureBudgetLevel1', N'Ред от бюджета на процедура на първо ниво.'
exec spDescColumn N'ProcedureBudgetLevel1', N'ProcedureBudgetLevel1Id'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureBudgetLevel1', N'ProcedureId'                , N'Идентификатор на процедура'
exec spDescColumn N'ProcedureBudgetLevel1', N'ProgrammeId'                , N'Идентификатор на програма'
exec spDescColumn N'ProcedureBudgetLevel1', N'ExpenseTypeId'              , N'Идентификатор на тип разход по елемент от бюджета.'
exec spDescColumn N'ProcedureBudgetLevel1', N'Gid'                        , N'Глобален уникален идентификатор.'
exec spDescColumn N'ProcedureBudgetLevel1', N'OrderNum'                   , N'Пореден номер в бюджета за сортиране на разходите в бюджета.'

GO
