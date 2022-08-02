PRINT 'ProcedureDiscussions'
GO

CREATE TABLE [dbo].[ProcedureDiscussions] (
    [ProcedureDiscussionId]        INT              NOT NULL IDENTITY,
    [Gid]                          UNIQUEIDENTIFIER NOT NULL UNIQUE,
    [ProcedureId]                  INT              NOT NULL,
    [Status]                       INT              NOT NULL,
    [Type]                         INT              NOT NULL,
    [Question]                     NVARCHAR(4000)   NOT NULL,
    [UserId]                       INT              NULL,
    [RegistrationId]               INT              NULL,
    [SenderEmail]                  NVARCHAR (100)   NOT NULL,
    [EditQuestion]                 NVARCHAR(4000)   NULL,
    [Answer]                       NVARCHAR(4000)   NULL,
    [AnswerUserId]                 INT              NULL,
    [AnswerDate]                   DATETIME2        NULL,
    [PublicationDate]              DATETIME2        NULL,

    [CreateDate]                   DATETIME2        NOT NULL,
    [ModifyDate]                   DATETIME2        NOT NULL,
    [Version]                      ROWVERSION       NOT NULL,

    CONSTRAINT [PK_ProcedureDiscussions]                        PRIMARY KEY ([ProcedureDiscussionId]),
    CONSTRAINT [FK_ProcedureDiscussions_Procedures]             FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureDiscussions_RegistrationId]         FOREIGN KEY ([RegistrationId])      REFERENCES [dbo].[Registrations] ([RegistrationId]),
    CONSTRAINT [FK_ProcedureDiscussions_UserId]                 FOREIGN KEY ([UserId])              REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ProcedureDiscussions_AnswerUserId]           FOREIGN KEY ([AnswerUserId])        REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ProcedureDiscussions_Status]                CHECK       ([Status] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ProcedureDiscussions_Type]                  CHECK       ([Type] IN (1, 2)),
);
GO

exec spDescTable  N'ProcedureDiscussions', N'Разяснения по обявени процедури.'
exec spDescColumn N'ProcedureDiscussions', N'ProcedureDiscussionId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureDiscussions', N'Gid'                         , N'Публичен системно генериран идентификатор.'
exec spDescColumn N'ProcedureDiscussions', N'ProcedureId'                 , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureDiscussions', N'Status'                      , N'Статус: 1 - Чернова, 2 - Изпратен, 3 - Публикуван, 4 - Анулиран.'
exec spDescColumn N'ProcedureDiscussions', N'Type'                        , N'Тип: 1 - Кандидат, 2 - УО/МИГ.'
exec spDescColumn N'ProcedureDiscussions', N'Question'                    , N'Въпрос.'
exec spDescColumn N'ProcedureDiscussions', N'UserId'                      , N'Идентификатор на потребител (подател).'
exec spDescColumn N'ProcedureDiscussions', N'RegistrationId'              , N'Идентификатор на регистрация (подател).'
exec spDescColumn N'ProcedureDiscussions', N'SenderEmail'                 , N'E-mail на подател.'
exec spDescColumn N'ProcedureDiscussions', N'EditQuestion'                , N'Коригиран въпрос.'
exec spDescColumn N'ProcedureDiscussions', N'Answer'                      , N'Отговор на УО/МИГ.'
exec spDescColumn N'ProcedureDiscussions', N'AnswerUserId'                , N'Идентификатор на потребител (отговор).'
exec spDescColumn N'ProcedureDiscussions', N'AnswerDate'                  , N'Дата на отговор.'
exec spDescColumn N'ProcedureDiscussions', N'PublicationDate'             , N'Дата на публикуване.'
exec spDescColumn N'ProcedureDiscussions', N'CreateDate'                  , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureDiscussions', N'ModifyDate'                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureDiscussions', N'Version'                     , N'Версия.'

GO
