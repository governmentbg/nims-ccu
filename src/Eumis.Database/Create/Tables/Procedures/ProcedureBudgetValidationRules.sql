PRINT 'ProcedureBudgetValidationRules'
GO

CREATE TABLE [dbo].[ProcedureBudgetValidationRules] (
    [ProcedureBudgetValidationRuleId]       INT                 NOT NULL IDENTITY,
    [ProcedureId]                           INT                 NOT NULL,
    [ProgrammeId]                           INT                 NOT NULL,
    [Message]                               NVARCHAR(MAX)       NOT NULL,
    [Condition]                             NVARCHAR(MAX)       NULL,
    [Rule]                                  NVARCHAR(MAX)       NOT NULL,

    CONSTRAINT [PK_ProcedureBudgetValidationRules]                       PRIMARY KEY ([ProcedureBudgetValidationRuleId]),
    CONSTRAINT [FK_ProcedureBudgetValidationRules_Procedures]            FOREIGN KEY ([ProcedureId])                  REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureBudgetValidationRules_MapNodes]              FOREIGN KEY ([ProgrammeId])                  REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProcedureBudgetValidationRules_ProcedureProgrammes]   FOREIGN KEY ([ProcedureId], [ProgrammeId])   REFERENCES [dbo].[ProcedureProgrammes] ([ProcedureId], [ProgrammeId])
);
GO

exec spDescTable  N'ProcedureBudgetValidationRules', N'Ред от валидационните правила по процедура и програма.'
exec spDescColumn N'ProcedureBudgetValidationRules', N'ProcedureBudgetValidationRuleId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureBudgetValidationRules', N'ProcedureId'                        , N'Идентификатор на процедура'
exec spDescColumn N'ProcedureBudgetValidationRules', N'ProgrammeId'                        , N'Идентификатор на програма'
exec spDescColumn N'ProcedureBudgetValidationRules', N'Message'                            , N'Валидационно съобщение.'
exec spDescColumn N'ProcedureBudgetValidationRules', N'Condition'                          , N'Валидационно условие.'
exec spDescColumn N'ProcedureBudgetValidationRules', N'Rule'                               , N'Валидационно правило.'

GO
