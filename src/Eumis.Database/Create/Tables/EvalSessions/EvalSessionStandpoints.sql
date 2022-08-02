PRINT 'EvalSessionStandpoints'
GO

CREATE TABLE [dbo].[EvalSessionStandpoints] (
    [EvalSessionId]             INT           NOT NULL,
    [EvalSessionStandpointId]   INT           NOT NULL IDENTITY,
    [EvalSessionUserId]         INT           NOT NULL,
    [ProjectId]                 INT           NOT NULL,
    [Note]                      NVARCHAR(MAX) NOT NULL,

    [CreateDate]                DATETIME2     NOT NULL,
    [Status]                    INT           NOT NULL,
    [StatusDate]                DATETIME2     NOT NULL,
    [DeleteNote]                NVARCHAR(MAX) NULL,

    CONSTRAINT [PK_EvalSessionStandpoints]                 PRIMARY KEY ([EvalSessionId], [EvalSessionStandpointId]),
    CONSTRAINT [FK_EvalSessionStandpoints_EvalSessions]    FOREIGN KEY ([EvalSessionId])                     REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionStandpoints_Users]           FOREIGN KEY ([EvalSessionUserId])                 REFERENCES [dbo].[EvalSessionUsers] ([EvalSessionUserId]),
    CONSTRAINT [FK_EvalSessionStandpoints_Projects]        FOREIGN KEY ([EvalSessionId], [ProjectId])        REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId]),
    CONSTRAINT [CHK_EvalSessionStandpoints_Status]         CHECK       ([Status] IN (1, 2, 3)),
);
GO

exec spDescTable  N'EvalSessionStandpoints', N'Становища към оценителна сесия.'
exec spDescColumn N'EvalSessionStandpoints', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionStandpoints', N'EvalSessionStandpointId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionStandpoints', N'EvalSessionUserId'                  , N'Идентификатор на потребител към становище.'
exec spDescColumn N'EvalSessionStandpoints', N'ProjectId'                          , N'Идентификатор на проектно предложние към оценителен лист.'
exec spDescColumn N'EvalSessionStandpoints', N'Note'                               , N'Коментар.'

exec spDescColumn N'EvalSessionStandpoints', N'CreateDate'                         , N'Дата на създаване.'
exec spDescColumn N'EvalSessionStandpoints', N'Status'                             , N'Статус на оценителен лист: 1 - Чернова, 2 - Приключен, 3 - Анулиран'
exec spDescColumn N'EvalSessionStandpoints', N'StatusDate'                         , N'Дата на промяна на статуса.'
exec spDescColumn N'EvalSessionStandpoints', N'DeleteNote'                         , N'Причина за анулиране.'
GO
