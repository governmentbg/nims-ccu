PRINT 'ProjectCommunications'
GO

CREATE TABLE [dbo].[ProjectCommunications] (
    [ProjectCommunicationId]               INT                NOT NULL IDENTITY,
    [Gid]                                  UNIQUEIDENTIFIER   NOT NULL,
    [ProjectId]                            INT                NOT NULL,
    [EvalSessionId]                        INT                NULL,
    [Status]                               INT                NULL,
    [StatusNote]                           NVARCHAR(MAX)      NULL,
    [RegNumber]                            NVARCHAR(200)      NULL,
    [QuestionEndingDate]                   DATETIME2          NULL,
    [QuestionReadDate]                     DATETIME2          NULL,
    [OrderNum]                             INT                NOT NULL,
    [Discriminator]                        INT                NOT NULL,

    [ManagingAuthorityCommunicationStatus] INT                NULL,
    [Source]                               INT                NULL,
    [Subject]                              INT                NULL,

    [QuestionProjectVersionXmlId]          INT                NOT NULL,
    [QuestionDate]                         DATETIME2          NULL,
    [QuestionContent]                      NVARCHAR(MAX)      NULL,
    [QuestionXml]                          XML                NOT NULL,
    [QuestionHash]                         NVARCHAR(10)       NOT NULL UNIQUE,

    [AnswerProjectVersionXmlId]            INT                NULL,
    [AnswerDate]                           DATETIME2          NULL,
    [AnswerContent]                        NVARCHAR(MAX)      NULL,
    [AnswerXml]                            XML                NULL,
    [AnswerHash]                           NVARCHAR(10)       NULL,

    [CreateDate]                           DATETIME2          NOT NULL,
    [ModifyDate]                           DATETIME2          NOT NULL,
    [Version]                              ROWVERSION         NOT NULL

    CONSTRAINT [PK_ProjectCommunications]                                         PRIMARY KEY ([ProjectCommunicationId]),
    CONSTRAINT [FK_ProjectCommunications_Projects]                                FOREIGN KEY ([ProjectId])                   REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectCommunications_EvalSessions]                            FOREIGN KEY ([EvalSessionId])               REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_ProjectCommunications_QuestionProjectVersionXmls]              FOREIGN KEY ([QuestionProjectVersionXmlId]) REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [FK_ProjectCommunications_AnswerProjectVersionXmls]                FOREIGN KEY ([AnswerProjectVersionXmlId])   REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [CHK_ProjectCommunications_Status]                                 CHECK ([Status] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)),
    CONSTRAINT [CHK_ProjectCommunications_ManagingAuthorityCommunicationStatus]   CHECK ([ManagingAuthorityCommunicationStatus] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ProjectCommunications_Discriminator]                          CHECK ([Discriminator] IN (1, 2)),
    CONSTRAINT [CHK_ProjectCommunications_Source]                                 CHECK ([Source]   IN (1, 2)),
    CONSTRAINT [CHK_ProjectCommunications_Subject]                                CHECK ([Subject]   IN (1, 2, 3))
);
GO

CREATE UNIQUE INDEX [UQ_ProjectCommunications_AnswerHash]
    ON [dbo].[ProjectCommunications]([AnswerHash]) WHERE [AnswerHash] IS NOT NULL
GO

exec spDescTable  N'ProjectCommunications', N'Комуникация с кандидат относно ПП.'
exec spDescColumn N'ProjectCommunications', N'ProjectCommunicationId'                , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectCommunications', N'Gid'                                   , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProjectCommunications', N'ProjectId'                             , N'Идентификатор на проектно предложение.'
exec spDescColumn N'ProjectCommunications', N'Discriminator'                         , N'Вид: 1 - Комуникация; 2 - Комуникация с УО.'
exec spDescColumn N'ProjectCommunications', N'Source'                                , N'Вид: 1 - Кандидат; 2 - УО.'
exec spDescColumn N'ProjectCommunications', N'Subject'                               , N'Вид: 1 - Оттегляне на проектно предложение; 2 - Жалба; 3 - Документи за сключване на Договор за безвъзмездна финансова помощ.'
exec spDescColumn N'ProjectCommunications', N'EvalSessionId'                         , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'ProjectCommunications', N'Status'                                , N'Статус: 1 - Въпрос в чернова; 2 - Изпратен въпрос; 3 - Отговор чернова; 4 - Отговор финализиран; 5 - Изпратен отговор; 6 - Изпратен отговор на хартия; 7 - Приет отговор; 8 - Отказан отговор; 9 - Анулиран; 10 - Изтекъл срок за отговор'
exec spDescColumn N'ProjectCommunications', N'ManagingAuthorityCommunicationStatus'  , N'Статус: 1 - Въпрос в чернова; 2 - Изпратен въпрос; 3 - Анулиран; 4 - Изтекъл срок за отговор'
exec spDescColumn N'ProjectCommunications', N'StatusNote'                            , N'Бележка.'
exec spDescColumn N'ProjectCommunications', N'RegNumber'                             , N'Системно генериран регистрационен номер.'
exec spDescColumn N'ProjectCommunications', N'QuestionEndingDate'                    , N'Крайна дата за отгвор на въпроса.'
exec spDescColumn N'ProjectCommunications', N'QuestionReadDate'                      , N'Дата на първо отваряне от кандидата.'
exec spDescColumn N'ProjectCommunications', N'OrderNum'                              , N'Пореден номер.'
exec spDescColumn N'ProjectCommunications', N'QuestionProjectVersionXmlId'           , N'Идентификатор на версията на ПП, към която е въпроса.'
exec spDescColumn N'ProjectCommunications', N'QuestionDate'                          , N'Дата на изпращане на въпроса.'
exec spDescColumn N'ProjectCommunications', N'QuestionXml'                           , N'Въпрос от администрацията.'
exec spDescColumn N'ProjectCommunications', N'QuestionHash'                          , N'Уникален идентификатор на съдържанието на въпроса.'
exec spDescColumn N'ProjectCommunications', N'AnswerProjectVersionXmlId'             , N'Идентификатор на версията на ПП, асоциирана с отговора.'
exec spDescColumn N'ProjectCommunications', N'AnswerDate'                            , N'Дата на изпращане на отговора.'
exec spDescColumn N'ProjectCommunications', N'AnswerXml'                             , N'Отговор от кандидата.'
exec spDescColumn N'ProjectCommunications', N'AnswerHash'                            , N'Уникален идентификатор на съдържанието на отговора.'
exec spDescColumn N'ProjectCommunications', N'CreateDate'                            , N'Дата на създаване на записа.'
exec spDescColumn N'ProjectCommunications', N'ModifyDate'                            , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProjectCommunications', N'Version'                               , N'Версия.'
GO

