PRINT 'EvalSessionStandings'
GO

CREATE TABLE [dbo].[EvalSessionStandings] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionStandingId]         INT                NOT NULL IDENTITY,
    [Code]                          NVARCHAR(50)       NOT NULL,
    [IsPreliminary]                 BIT                NOT NULL,
    [PreliminaryBudgetPercentage]   INT                NULL,
    [Status]                        INT                NOT NULL,
    [StatusNote]                    NVARCHAR(MAX)      NULL,
    [StatusDate]                    DATETIME2          NOT NULL,

    CONSTRAINT [PK_EvalSessionStandings]                      PRIMARY KEY ([EvalSessionId], [EvalSessionStandingId]),
    CONSTRAINT [CHK_EvalSessionStandings_Status]              CHECK       ([Status] IN (1, 2, 3)),
    CONSTRAINT [FK_EvalSessionStandings_EvalSessions]         FOREIGN KEY ([EvalSessionId])                    REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
);
GO

exec spDescTable  N'EvalSessionStandings', N'Класиране на проектни предложения към оценителна сесия.'
exec spDescColumn N'EvalSessionStandings', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionStandings', N'EvalSessionStandingId'              , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionStandings', N'Code'                               , N'Код.'
exec spDescColumn N'EvalSessionStandings', N'IsPreliminary'                      , N'Предварително класиране - Да/Не'
exec spDescColumn N'EvalSessionStandings', N'PreliminaryBudgetPercentage'        , N'Бюджет на предварителното класиране в проценти'
exec spDescColumn N'EvalSessionStandings', N'Status'                             , N'Статус на разпределение: 1 - Приложено, 2 - Отказано, 3 - Автоматично с промени'
exec spDescColumn N'EvalSessionStandings', N'StatusNote'                         , N'Причина за промяна на статуса.'
exec spDescColumn N'EvalSessionStandings', N'StatusDate'                         , N'Дата на промяна на статуса / на създаване.'

GO

