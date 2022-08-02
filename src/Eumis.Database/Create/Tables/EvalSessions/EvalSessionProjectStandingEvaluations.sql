PRINT 'EvalSessionProjectStandingEvaluations'
GO

CREATE TABLE [dbo].[EvalSessionProjectStandingEvaluations] (
    [EvalSessionId]                    INT                NOT NULL,
    [EvalSessionProjectStandingId]     INT                NOT NULL,
    [EvalSessionEvaluationId]          INT                NOT NULL,

    CONSTRAINT [PK_EvalSessionProjectStandingEvaluations]                                PRIMARY KEY ([EvalSessionId], [EvalSessionProjectStandingId], [EvalSessionEvaluationId]),
    CONSTRAINT [FK_EvalSessionProjectStandingEvaluations_EvalSessions]                   FOREIGN KEY ([EvalSessionId])                                  REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionProjectStandingEvaluations_EvalSessionProjectStandings]    FOREIGN KEY ([EvalSessionId], [EvalSessionProjectStandingId])  REFERENCES [dbo].[EvalSessionProjectStandings] ([EvalSessionId], [EvalSessionProjectStandingId]),
    CONSTRAINT [FK_EvalSessionProjectStandingEvaluations_EvalSessionEvaluations]         FOREIGN KEY ([EvalSessionId], [EvalSessionEvaluationId])       REFERENCES [dbo].[EvalSessionEvaluations] ([EvalSessionId], [EvalSessionEvaluationId])
);
GO

exec spDescTable  N'EvalSessionProjectStandingEvaluations', N'Оценителни листа към обобщен оценка към оценителна сесия.'
exec spDescColumn N'EvalSessionProjectStandingEvaluations', N'EvalSessionId'                        , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionProjectStandingEvaluations', N'EvalSessionProjectStandingId'         , N'Идентификатор на класиране'
exec spDescColumn N'EvalSessionProjectStandingEvaluations', N'EvalSessionEvaluationId'              , N'Идентификатор на обобщен оценка'

GO

