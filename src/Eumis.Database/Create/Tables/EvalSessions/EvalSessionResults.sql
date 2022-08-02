PRINT 'EvalSessionResults'
GO

CREATE TABLE [dbo].[EvalSessionResults] (
    [EvalSessionResultId]                           INT             NOT NULL IDENTITY,
    [EvalSessionId]                                 INT             NOT NULL,
    [Status]                                        INT             NOT NULL,
    [StatusNote]                                    NVARCHAR(MAX)   NULL,
    [Type]                                          INT             NOT NULL,
    [OrderNum]                                      INT             NOT NULL,
    [ProcedureId]                                   INT             NOT NULL,

    [PublicationDate]                               DATETIME2       NULL,
    [PublicationUserId]                             INT             NULL,

    [CreateDate]                                    DATETIME2       NOT NULL,
    [ModifyDate]                                    DATETIME2       NOT NULL,

    CONSTRAINT [PK_EvalSessionResults]                               PRIMARY KEY ([EvalSessionResultId]),
    CONSTRAINT [FK_EvalSessionResults_EvalSessions]                  FOREIGN KEY ([EvalSessionId])           REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionResults_Users]                         FOREIGN KEY ([PublicationUserId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_EvalSessionResults_Procedures]                    FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_EvalSessionResults_Status]                       CHECK       ([Status] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_EvalSessionResults_Type]                         CHECK       ([Type]   IN (1, 2, 3))
);

exec spDescTable  N'EvalSessionResults', N'Резултати към оценителна сесия.'
exec spDescColumn N'EvalSessionResults', N'EvalSessionResultId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionResults', N'EvalSessionId'            , N'Идентификатор на оценителена сесия.'
exec spDescColumn N'EvalSessionResults', N'Status'                   , N'Статус на резултата: 1 - Чернова; 2 - Публикуван; 3 - Архивиран; 4 - Анулиран'
exec spDescColumn N'EvalSessionResults', N'StatusNote'               , N'Причина за промяна на статуса.'
exec spDescColumn N'EvalSessionResults', N'Type'                     , N'Вид на резултата: 1 - Предварително класиране, 2 - Оценка АСД, 3 - Класиране'
exec spDescColumn N'EvalSessionResults', N'OrderNum'                 , N'Пореден номер.'
exec spDescColumn N'EvalSessionResults', N'ProcedureId'              , N'Идентификатор на процедура.'
exec spDescColumn N'EvalSessionResults', N'PublicationDate'          , N'Дата на публикуване.'
exec spDescColumn N'EvalSessionResults', N'PublicationUserId'        , N'Идентификатор на потребител.'
exec spDescColumn N'EvalSessionResults', N'CreateDate'               , N'Дата на създаване.'
exec spDescColumn N'EvalSessionResults', N'ModifyDate'               , N'Дата на модифициране.'
GO
