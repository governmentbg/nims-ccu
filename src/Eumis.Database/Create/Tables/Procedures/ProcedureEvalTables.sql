PRINT 'ProcedureEvalTables'
GO

CREATE TABLE [dbo].[ProcedureEvalTables] (
    [ProcedureEvalTableId]                      INT                 NOT NULL IDENTITY,
    [ProcedureId]                               INT                 NOT NULL,
    [Name]                                      NVARCHAR(200)       NOT NULL,
    [Type]                                      INT                 NOT NULL,
    [EvalType]                                  INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [IsActivated]                               BIT                 NOT NULL,
    [IsActive]                                  BIT                 NOT NULL,

    CONSTRAINT [PK_ProcedureEvalTables]                          PRIMARY KEY ([ProcedureEvalTableId]),
    CONSTRAINT [CHK_ProcedureEvalTables_Type]                    CHECK       ([Type] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ProcedureEvalTables_EvalType]                CHECK       ([EvalType] IN (1, 2)),
    CONSTRAINT [CHK_ProcedureEvalTables_Status]                  CHECK       ([Status] IN (1, 2)),
    CONSTRAINT [FK_ProcedureEvalTables_Procedures]               FOREIGN KEY ([ProcedureId])     REFERENCES [dbo].[Procedures] ([ProcedureId]),
);
GO

exec spDescTable  N'ProcedureEvalTables', N'Оценителна таблица.'
exec spDescColumn N'ProcedureEvalTables', N'ProcedureEvalTableId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureEvalTables', N'ProcedureId'                     , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureEvalTables', N'Name'                            , N'Наименование.'
exec spDescColumn N'ProcedureEvalTables', N'Type'                            , N'Етап на оценка: 1 - Оценка на административното съответствие и допустимостта , 2 - Техническа и финансова оценка, 3 - Комплексна оценка, 4 - Предварителна оценка.'
exec spDescColumn N'ProcedureEvalTables', N'EvalType'                        , N'Тип на оценителния лист: 1 - Да/Не , 2 - Точки.'
exec spDescColumn N'ProcedureEvalTables', N'Status'                          , N'Статус: 1 - Чернова , 2 - Приключен.'

GO
