PRINT 'EvalSessionProjectStandings'
GO

CREATE TABLE [dbo].[EvalSessionProjectStandings] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionProjectStandingId]  INT                NOT NULL IDENTITY,
    [ProjectId]                     INT                NOT NULL,
    [IsPreliminary]                 BIT                NOT NULL,
    [OrderNum]                      INT                NULL,
    [ManualOrderNum]                INT                NULL,
    [Status]                        INT                NOT NULL,
    [ManualStatus]                  INT                NOT NULL,
    [GrandAmount]                   MONEY              NULL,
    [IsDeleted]                     BIT                NOT NULL,
    [IsDeletedNote]                 NVARCHAR(MAX)      NULL,
    [Notes]                         NVARCHAR(MAX)      NULL,
    [EvalSessionStandingId]         INT                NULL,
    [ProcedureBudgetComponentId]    INT                NULL,
    [CreateDate]                    DATETIME2          NOT NULL,
    [ProjectVersionXmlId]           INT                NOT NULL,
    [RejectionReasonId]             INT                NULL,

    CONSTRAINT [PK_EvalSessionProjectStanding]                                 PRIMARY KEY ([EvalSessionId], [EvalSessionProjectStandingId]),
    CONSTRAINT [CHK_EvalSessionProjectStanding_Status]                         CHECK       ([Status] IN (1, 2, 3, 4, 5, 6)),
    CONSTRAINT [CHK_EvalSessionProjectStanding_ManualStatus]                   CHECK       ([ManualStatus] IN (1, 2, 3, 4, 5, 6)),
    CONSTRAINT [FK_EvalSessionProjectStanding_EvalSessions]                    FOREIGN KEY ([EvalSessionId])                               REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionProjectStandings_EvalSessionProjects]            FOREIGN KEY ([EvalSessionId], [ProjectId])                  REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionProjectStandings_EvalSessionStandings]           FOREIGN KEY ([EvalSessionId], [EvalSessionStandingId])      REFERENCES [dbo].[EvalSessionStandings] ([EvalSessionId], [EvalSessionStandingId]),
    CONSTRAINT [FK_EvalSessionProjectStandings_ProjectVersions]                FOREIGN KEY ([ProjectVersionXmlId])                         REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [FK_EvalSessionProjectStandings_RejectionReasons]               FOREIGN KEY ([RejectionReasonId])                           REFERENCES [dbo].[EvalSessionProjectStandingRejectionReasons] ([EvalSessionProjectStandingRejectionReasonId]),
    CONSTRAINT [FK_EvalSessionProjectStandings_ProcedureBudgetComponents]      FOREIGN KEY ([ProcedureBudgetComponentId])                  REFERENCES [dbo].[ProcedureBudgetComponents] ([ProcedureBudgetComponentId])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_EvalSessionProjectStandings_EvalSessionId_OrderNum]
ON [EvalSessionProjectStandings]([EvalSessionId], [IsPreliminary], [ProcedureBudgetComponentId], [OrderNum])
WHERE [OrderNum] IS NOT NULL AND [IsDeleted] = 0;

exec spDescTable  N'EvalSessionProjectStandings', N'Проектни предложения към класиране към оценителна сесия.'
exec spDescColumn N'EvalSessionProjectStandings', N'EvalSessionId'                  , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionProjectStandings', N'EvalSessionProjectStandingId'   , N'Уникален системно генериран идентификатор'
exec spDescColumn N'EvalSessionProjectStandings', N'ProjectId'                      , N'Идентификатор на проектно предложение'
exec spDescColumn N'EvalSessionProjectStandings', N'IsPreliminary'                  , N'Предварително класиране - Да/Не'
exec spDescColumn N'EvalSessionProjectStandings', N'OrderNum'                       , N'Пореден номер.'
exec spDescColumn N'EvalSessionProjectStandings', N'ManualOrderNum'                 , N'Пореден номер в ръчно подреждане.'
exec spDescColumn N'EvalSessionProjectStandings', N'Status'                         , N'Статус: 1 - Одобрено, 2 - Резерва, 3 - Отхвърлено, 4 - Отхвърлено на ОАСД, 5 - Отхвърлено на ТФО, 6 - Отхвърлено на ПО'
exec spDescColumn N'EvalSessionProjectStandings', N'ManualStatus'                   , N'Статус ръчно подреждане: 1 - Одобрено, 2 - Резерва, 3 - Отхвърлено, 4 - Отхвърлено на ОАСД, 5 - Отхвърлено на ТФО, 6 - Отхвърлено на ПО'
exec spDescColumn N'EvalSessionProjectStandings', N'GrandAmount'                    , N'Одобрено БФП.'
exec spDescColumn N'EvalSessionProjectStandings', N'IsDeleted'                      , N'Маркер, дали класирането е изтрито.'
exec spDescColumn N'EvalSessionProjectStandings', N'IsDeletedNote'                  , N'Причина за изтриване.'
exec spDescColumn N'EvalSessionProjectStandings', N'Notes'                          , N'Бележки.'
exec spDescColumn N'EvalSessionProjectStandings', N'EvalSessionStandingId'          , N'Идентификатор на класиране'
exec spDescColumn N'EvalSessionProjectStandings', N'ProcedureBudgetComponentId'     , N'Идентификатор на бюджетен компонент'
exec spDescColumn N'EvalSessionProjectStandings', N'CreateDate'                     , N'Дата на създаване.'
exec spDescColumn N'EvalSessionProjectStandings', N'ProjectVersionXmlId'            , N'Идентификатор на версия на проектно предложение.'
exec spDescColumn N'EvalSessionProjectStandings', N'RejectionReasonId'              , N'Причини за отхвърляне/отпадане/ниска оценка на проекти.'

GO

