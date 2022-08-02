PRINT 'EvalSessions'
GO

CREATE TABLE [dbo].[EvalSessions] (
    [EvalSessionId]                             INT             NOT NULL IDENTITY,
    [ProcedureId]                               INT             NOT NULL,
    [EvalSessionStatus]                         INT             NOT NULL,
    [EvalSessionType]                           INT             NOT NULL,
    [SessionNum]                                NVARCHAR(50)    NOT NULL,
    [SessionDate]                               DATETIME2       NOT NULL,
    [OrderNum]                                  NVARCHAR(50)    NULL,
    [OrderDate]                                 DATETIME2       NULL,
    [CreateDate]                                DATETIME2       NOT NULL,
    [ModifyDate]                                DATETIME2       NOT NULL,
    [Version]                                   ROWVERSION      NOT NULL,

    CONSTRAINT [PK_EvalSessions]                        PRIMARY KEY ([EvalSessionId]),
    CONSTRAINT [CHK_EvalSessions_Status]                CHECK       ([EvalSessionStatus] IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_EvalSessions_Type]                  CHECK       ([EvalSessionType] IN (1, 2, 3)),
    CONSTRAINT [FK_EvalSessions_Procedures]             FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId])
);
GO

exec spDescTable  N'EvalSessions', N'Оценителна сесия по процедура.'
exec spDescColumn N'EvalSessions', N'EvalSessionId'                             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessions', N'ProcedureId'                               , N'Идентификатор на процедура.'
exec spDescColumn N'EvalSessions', N'EvalSessionStatus'                         , N'Статус на сесията: 1 - Чернова, 2 - Активна, 3 - Приключена, 4 - Анулирана, 5 - Приключена от МИГ.'
exec spDescColumn N'EvalSessions', N'EvalSessionType'                           , N'Тип на сесията: 1 - За предварителен подбор, 2 - За оценка на проектни предложения, 3 - За оценка на проектни фишове.'
exec spDescColumn N'EvalSessions', N'SessionNum'                                , N'Номер на сесия.'
exec spDescColumn N'EvalSessions', N'SessionDate'                               , N'Дата на сесия.'
exec spDescColumn N'EvalSessions', N'OrderNum'                                  , N'Номер на заповед.'
exec spDescColumn N'EvalSessions', N'OrderDate'                                 , N'Дата на заповед.'
exec spDescColumn N'EvalSessions', N'CreateDate'                                , N'Дата на създаване на записа.'
exec spDescColumn N'EvalSessions', N'ModifyDate'                                , N'Дата на последно редактиране на записа.'
exec spDescColumn N'EvalSessions', N'Version'                                   , N'Версия.'

GO


