PRINT 'EvalSessionEvaluations'
GO

CREATE TABLE [dbo].[EvalSessionEvaluations] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionEvaluationId]       INT                NOT NULL IDENTITY,
    [ProjectId]                     INT                NOT NULL,
    [CalculationType]               INT                NOT NULL,
    [EvalType]                      INT                NOT NULL,
    [EvalTableType]                 INT                NOT NULL,
    [EvalIsPassed]                  BIT                NOT NULL,
    [EvalPoints]                    DECIMAL(15,3)      NULL,
    [EvalNote]                      NVARCHAR(MAX)      NULL,
    [IsDeleted]                     BIT                NOT NULL,
    [IsDeletedNote]                 NVARCHAR(MAX)      NULL,
    [CreateDate]                    DATETIME2          NOT NULL,

    CONSTRAINT [PK_EvalSessionEvaluations]                      PRIMARY KEY ([EvalSessionId], [EvalSessionEvaluationId]),
    CONSTRAINT [CHK_EvalSessionEvaluations_EvalTableType]       CHECK       ([EvalTableType] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_EvalSessionEvaluations_CalculationType]     CHECK       ([CalculationType] IN (1, 2)),
    CONSTRAINT [FK_EvalSessionEvaluations_EvalSessions]         FOREIGN KEY ([EvalSessionId])                    REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionEvaluations_EvalSessionProjects]  FOREIGN KEY ([EvalSessionId], [ProjectId])       REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId])
);
GO

CREATE UNIQUE INDEX [UQ_EvalSessionEvaluations_Project_IsDeleted]
    ON [dbo].[EvalSessionEvaluations]([EvalSessionId], [ProjectId], [EvalTableType], [IsDeleted]) WHERE [IsDeleted] = 0
GO

exec spDescTable  N'EvalSessionEvaluations', N'Обобщени оценки към оценителна сесия.'
exec spDescColumn N'EvalSessionEvaluations', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionEvaluations', N'EvalSessionEvaluationId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionEvaluations', N'ProjectId'                          , N'Идентификатор на проектно предложение.'
exec spDescColumn N'EvalSessionEvaluations', N'CalculationType'                    , N'Тип на обобщаването: 1 - Автоматично , 2 - Ръчно.'
exec spDescColumn N'EvalSessionEvaluations', N'EvalType'                           , N'Тип на оценителния лист: 1 - Да/Не , 2 - Точки.'
exec spDescColumn N'EvalSessionEvaluations', N'EvalTableType'                      , N'Тип на етап на оценка: 1 - Оценка на административното съответствие и допустимостта, 2 - Техническа и финансова оценка, 3 - Комплексна оценка, 4 - Предварителна оценка.'
exec spDescColumn N'EvalSessionEvaluations', N'EvalIsPassed'                       , N'Преминава оценка.'
exec spDescColumn N'EvalSessionEvaluations', N'EvalPoints'                         , N'Точки.'
exec spDescColumn N'EvalSessionEvaluations', N'EvalNote'                           , N'Коментар към оценката.'
exec spDescColumn N'EvalSessionEvaluations', N'IsDeleted'                          , N'Маркер, дали обобщената оценка е изтрита.'
exec spDescColumn N'EvalSessionEvaluations', N'IsDeletedNote'                      , N'Причина за изтриване.'
exec spDescColumn N'EvalSessionEvaluations', N'CreateDate'                         , N'Дата на създаване.'

GO

