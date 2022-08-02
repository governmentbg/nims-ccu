PRINT 'EvalSessionSheets'
GO

CREATE TABLE [dbo].[EvalSessionSheets] (
    [EvalSessionId]                 INT             NOT NULL,
    [EvalSessionSheetId]            INT             NOT NULL,
    [EvalSessionUserId]             INT             NOT NULL,
    [ProjectId]                     INT             NOT NULL,
    [EvalTableType]                 INT             NOT NULL,
    [Status]                        INT             NOT NULL,
    [StatusNote]                    NVARCHAR(MAX)   NULL,
    [StatusDate]                    DATETIME2       NOT NULL,
    [CreateDate]                    DATETIME2       NOT NULL,
    [Notes]                         NVARCHAR(MAX)   NULL,
    [EvalSessionDistributionId]     INT             NULL,
    [DistributionType]              INT             NOT NULL,
    [ContinuedEvalSessionSheetId]   INT             NULL,

    CONSTRAINT [PK_EvalSessionSheets]                            PRIMARY KEY ([EvalSessionId], [EvalSessionSheetId]),
    CONSTRAINT [CHK_EvalSessionSheets_EvalTableType]             CHECK       ([EvalTableType] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_EvalSessionSheets_Status]                    CHECK       ([Status] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_EvalSessionSheets_DistributionType]          CHECK       ([DistributionType] IN (1, 2, 3)),
    CONSTRAINT [FK_EvalSessionSheets_EvalSessions]               FOREIGN KEY ([EvalSessionId])                                REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionSheets_Users]                      FOREIGN KEY ([EvalSessionUserId])                            REFERENCES [dbo].[EvalSessionUsers] ([EvalSessionUserId]),
    CONSTRAINT [FK_EvalSessionSheets_Projects]                   FOREIGN KEY ([EvalSessionId], [ProjectId])                   REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionSheets_EvalSessionDistributions]   FOREIGN KEY ([EvalSessionId], [EvalSessionDistributionId])   REFERENCES [dbo].[EvalSessionDistributions] ([EvalSessionId], [EvalSessionDistributionId]),
    CONSTRAINT [FK_EvalSessionSheets_ContinuedEvalSessionSheets] FOREIGN KEY ([EvalSessionId], [ContinuedEvalSessionSheetId]) REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId])
);
GO

exec spDescTable  N'EvalSessionSheets', N'Оценителен лист към оценителна сесия.'
exec spDescColumn N'EvalSessionSheets', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionSheets', N'EvalSessionSheetId'                 , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionSheets', N'EvalSessionUserId'                  , N'Идентификатор на потребител към оценителен лист.'
exec spDescColumn N'EvalSessionSheets', N'ProjectId'                          , N'Идентификатор на проектно предложние към оценителен лист.'
exec spDescColumn N'EvalSessionSheets', N'EvalTableType'                      , N'Тип на етап на оценка: 1 - Оценка на административното съответствие и допустимостта, 2 - Техническа и финансова оценка, 3 - Комплексна оценка, 4 - Предварителна оценка.'
exec spDescColumn N'EvalSessionSheets', N'Status'                             , N'Статус на оценителен лист: 1 - Чернова, 2 - Прекъснат, 3 - Приключен, 4 - Анулиран'
exec spDescColumn N'EvalSessionSheets', N'StatusNote'                         , N'Причина за промяна на статуса.'
exec spDescColumn N'EvalSessionSheets', N'StatusDate'                         , N'Дата на промяна на статуса.'
exec spDescColumn N'EvalSessionSheets', N'CreateDate'                         , N'Дата на създаване.'
exec spDescColumn N'EvalSessionSheets', N'Notes'                              , N'Коментар.'
exec spDescColumn N'EvalSessionSheets', N'EvalSessionDistributionId'          , N'Идентификатор на разпределение към оценителна сесия'
exec spDescColumn N'EvalSessionSheets', N'DistributionType'                   , N'Тип на разпределение: 1 - Автоматично , 2 - Ръчно., 3 - Продължена оценка'
exec spDescColumn N'EvalSessionSheets', N'ContinuedEvalSessionSheetId'        , N'Идентификатор на продължен оценителен лист'
GO

