PRINT 'EvalSessionProjects'
GO

CREATE TABLE [dbo].[EvalSessionProjects] (
    [EvalSessionId]     INT             NOT NULL,
    [ProjectId]         INT             NOT NULL,
    [IsDeleted]         BIT             NOT NULL,
    [IsDeletedNote]     NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_EvalSessionProjects]                  PRIMARY KEY ([EvalSessionId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionProjects_EvalSessions]     FOREIGN KEY ([EvalSessionId])       REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionProjects_Projects]         FOREIGN KEY ([ProjectId])           REFERENCES [dbo].[Projects] ([ProjectId])
);

exec spDescTable  N'EvalSessionProjects', N'Проектни предложения към оценителна сесия.'
exec spDescColumn N'EvalSessionProjects', N'EvalSessionId'          , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionProjects', N'ProjectId'              , N'Идентификатор на проектно предложние'
exec spDescColumn N'EvalSessionProjects', N'IsDeleted'              , N'Маркер, дали проектното предложение е премахнато от сесията.'

GO

