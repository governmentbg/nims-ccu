PRINT 'EvalSessionEvaluationSheets'
GO

CREATE TABLE [dbo].[EvalSessionEvaluationSheets] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionEvaluationId]       INT                NOT NULL,
    [EvalSessionSheetId]            INT                NOT NULL,

    CONSTRAINT [PK_EvalSessionEvaluationSheets]                           PRIMARY KEY ([EvalSessionId], [EvalSessionEvaluationId], [EvalSessionSheetId]),
    CONSTRAINT [FK_EvalSessionEvaluationSheets_EvalSessions]              FOREIGN KEY ([EvalSessionId])                               REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionEvaluationSheets_EvalSessionEvaluations]    FOREIGN KEY ([EvalSessionId], [EvalSessionEvaluationId])    REFERENCES [dbo].[EvalSessionEvaluations] ([EvalSessionId], [EvalSessionEvaluationId]),
    CONSTRAINT [FK_EvalSessionEvaluationSheets_EvalSessionSheets]         FOREIGN KEY ([EvalSessionId], [EvalSessionSheetId])         REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId])
);
GO

exec spDescTable  N'EvalSessionEvaluationSheets', N'Оценителни листа към обобщен оценка към оценителна сесия.'
exec spDescColumn N'EvalSessionEvaluationSheets', N'EvalSessionId'                        , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionEvaluationSheets', N'EvalSessionEvaluationId'              , N'Идентификатор на обобщен оценка'
exec spDescColumn N'EvalSessionEvaluationSheets', N'EvalSessionSheetId'                   , N'Идентификатор на оценителен лист'

GO

