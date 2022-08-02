PRINT 'EvalSessionDistributions'
GO

CREATE TABLE [dbo].[EvalSessionDistributions] (
    [EvalSessionId]                 INT             NOT NULL,
    [EvalSessionDistributionId]     INT             NOT NULL IDENTITY,
    [EvalTableType]                 INT             NOT NULL,
    [Status]                        INT             NOT NULL,
    [Code]                          NVARCHAR(50)    NOT NULL,
    [CreateDate]                    DATETIME2       NOT NULL,
    [StatusNote]                    NVARCHAR(MAX)   NULL,
    [AssessorsPerProject]           INT             NOT NULL,

    CONSTRAINT [PK_EvalSessionDistributions]                  PRIMARY KEY ([EvalSessionId], [EvalSessionDistributionId]),
    CONSTRAINT [CHK_EvalSessionDistributions_EvalTableType]   CHECK       ([EvalTableType] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_EvalSessionDistributions_Status]          CHECK       ([Status] IN (1, 2, 3)),
    CONSTRAINT [FK_EvalSessionDistributions_EvalSessions]     FOREIGN KEY ([EvalSessionId])       REFERENCES [dbo].[EvalSessions] ([EvalSessionId])
);
GO

exec spDescTable  N'EvalSessionDistributions', N'Разпределения към оценителна сесия.'
exec spDescColumn N'EvalSessionDistributions', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionDistributions', N'EvalSessionDistributionId'          , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionDistributions', N'EvalTableType'                      , N'Тип на етап на оценка: 1 - Оценка на административното съответствие и допустимостта, 2 - Техническа и финансова оценка, 3 - Комплексна оценка, 4 - Предварителна оценка.'
exec spDescColumn N'EvalSessionDistributions', N'Status'                             , N'Статус на разпределение: 1 - Чернова , 2 - Приложено, 3 - Отказано.'
exec spDescColumn N'EvalSessionDistributions', N'StatusNote'                         , N'Причина за промяна на статуса'
exec spDescColumn N'EvalSessionDistributions', N'AssessorsPerProject'                , N'Брой оценители за проектно предложение.'

GO

