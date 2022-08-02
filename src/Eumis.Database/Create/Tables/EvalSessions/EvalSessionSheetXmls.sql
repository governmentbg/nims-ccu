PRINT 'EvalSessionSheetXmls'
GO

CREATE TABLE [dbo].[EvalSessionSheetXmls] (
    [EvalSessionSheetXmlId]         INT                 NOT NULL IDENTITY,
    [Gid]                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [EvalSessionId]                 INT                 NOT NULL,
    [EvalSessionSheetId]            INT                 NOT NULL,
    [Xml]                           XML                 NOT NULL,
    [Hash]                          NVARCHAR(10)        NOT NULL UNIQUE,
    [EvalType]                      INT                 NOT NULL,
    [EvalTableType]                 INT                 NOT NULL,
    [EvalIsPassed]                  BIT                 NULL,
    [EvalPoints]                    DECIMAL(15,3)       NULL,
    [EvalNote]                      NVARCHAR(MAX)       NULL,
    [CreateDate]                    DATETIME2           NOT NULL,
    [ModifyDate]                    DATETIME2           NOT NULL,
    [Version]                       ROWVERSION          NOT NULL,

    CONSTRAINT [PK_EvalSessionSheetData]                        PRIMARY KEY ([EvalSessionSheetXmlId]),
    CONSTRAINT [FK_EvalSessionSheetData_EvalSessions]           FOREIGN KEY ([EvalSessionId])                         REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionSheetData_EvalSessionSheets]      FOREIGN KEY ([EvalSessionId], [EvalSessionSheetId])   REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId]),
);
GO

exec spDescTable  N'EvalSessionSheetXmls', N'Данни за оценителен лист към оценителна сесия.'
exec spDescColumn N'EvalSessionSheetXmls', N'EvalSessionSheetXmlId'              , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionSheetXmls', N'Gid'                                , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'EvalSessionSheetXmls', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionSheetXmls', N'EvalSessionSheetId'                 , N'Идентификатор на оценителен лист към оценителна сесия.'
exec spDescColumn N'EvalSessionSheetXmls', N'Xml'                                , N'Xml съдържание на оценителния лист.'
exec spDescColumn N'EvalSessionSheetXmls', N'Hash'                               , N'Hash.'
exec spDescColumn N'EvalSessionSheetXmls', N'EvalType'                           , N'Тип на оценителния лист: 1 - Да/Не , 2 - Точки.'
exec spDescColumn N'EvalSessionSheetXmls', N'EvalTableType'                      , N'Етап на оценка: 1 - Оценка на административното съответствие и допустимостта , 2 - Техническа и финансова оценка, 3 - Комплексна оценка, 4 - Предварителна оценка.'
exec spDescColumn N'EvalSessionSheetXmls', N'EvalIsPassed'                       , N'Преминава оценка.'
exec spDescColumn N'EvalSessionSheetXmls', N'EvalPoints'                         , N'Точки.'
exec spDescColumn N'EvalSessionSheetXmls', N'EvalNote'                           , N'Коментар към оценката.'
exec spDescColumn N'EvalSessionSheetXmls', N'CreateDate'                         , N'Дата на създаване на записа.'
exec spDescColumn N'EvalSessionSheetXmls', N'ModifyDate'                         , N'Дата на последно редактиране на записа.'
exec spDescColumn N'EvalSessionSheetXmls', N'Version'                            , N'Версия.'

GO

