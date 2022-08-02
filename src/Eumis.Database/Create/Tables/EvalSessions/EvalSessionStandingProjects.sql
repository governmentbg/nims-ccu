PRINT 'EvalSessionStandingProjects'
GO

CREATE TABLE [dbo].[EvalSessionStandingProjects] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionStandingId]         INT                NOT NULL,
    [ProjectId]                     INT                NOT NULL,

    CONSTRAINT [PK_EvalSessionStandingProjects]                           PRIMARY KEY ([EvalSessionId], [EvalSessionStandingId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionStandingProjects_EvalSessions]              FOREIGN KEY ([EvalSessionId])                             REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionStandingProjects_EvalSessionStandings]      FOREIGN KEY ([EvalSessionId], [EvalSessionStandingId])    REFERENCES [dbo].[EvalSessionStandings] ([EvalSessionId], [EvalSessionStandingId]),
    CONSTRAINT [FK_EvalSessionStandingProjects_EvalSessionProjects]       FOREIGN KEY ([EvalSessionId], [ProjectId])                REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId])
);
GO

exec spDescTable  N'EvalSessionStandingProjects', N'Оценителни листа към обобщен оценка към оценителна сесия.'
exec spDescColumn N'EvalSessionStandingProjects', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionStandingProjects', N'EvalSessionStandingId'              , N'Идентификатор на класиране.'
exec spDescColumn N'EvalSessionStandingProjects', N'ProjectId'                          , N'Идентификатор на проектно предложение.'

GO

