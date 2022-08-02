PRINT 'EvalSessionStandpointXmls'
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

exec spDescTable  N'EvalSessionStandpointXmls', N'Данни за становище към оценителна сесия.'
exec spDescColumn N'EvalSessionStandpointXmls', N'EvalSessionStandpointXmlId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionStandpointXmls', N'Gid'                       , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'EvalSessionStandpointXmls', N'EvalSessionId'             , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionStandpointXmls', N'EvalSessionStandpointId'   , N'Идентификатор на становище към оценителна сесия.'
exec spDescColumn N'EvalSessionStandpointXmls', N'Xml'                       , N'Xml съдържание на оценителния лист.'
exec spDescColumn N'EvalSessionStandpointXmls', N'Hash'                      , N'Hash.'
exec spDescColumn N'EvalSessionStandpointXmls', N'CreateDate'                , N'Дата на създаване на записа.'
exec spDescColumn N'EvalSessionStandpointXmls', N'ModifyDate'                , N'Дата на последно редактиране на записа.'
exec spDescColumn N'EvalSessionStandpointXmls', N'Version'                   , N'Версия.'
GO
