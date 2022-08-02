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

CREATE TABLE [dbo].[EvalSessionStandpointXmls] (
    [EvalSessionStandpointXmlId]    INT                 NOT NULL IDENTITY,
    [Gid]                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [EvalSessionId]                 INT                 NOT NULL,
    [EvalSessionStandpointId]       INT                 NOT NULL,
    [Xml]                           XML                 NOT NULL,
    [Hash]                          NVARCHAR(10)        NOT NULL UNIQUE,
    [CreateDate]                    DATETIME2           NOT NULL,
    [ModifyDate]                    DATETIME2           NOT NULL,
    [Version]                       ROWVERSION          NOT NULL,

    CONSTRAINT [PK_EvalSessionStandpointXmls]                          PRIMARY KEY ([EvalSessionStandpointXmlId]),
    CONSTRAINT [FK_EvalSessionStandpointXmls_EvalSessions]             FOREIGN KEY ([EvalSessionId])                             REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionStandpointXmls_EvalSessionStandpoints]   FOREIGN KEY ([EvalSessionId], [EvalSessionStandpointId])  REFERENCES [dbo].[EvalSessionStandpoints] ([EvalSessionId], [EvalSessionStandpointId]),
);
GO
