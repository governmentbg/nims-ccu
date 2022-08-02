PRINT 'ProjectCommunicationAnswers'
GO

CREATE TABLE [dbo].[ProjectCommunicationAnswers] (
    [ProjectCommunicationAnswerId]   INT                NOT NULL IDENTITY,
    [Gid]                            UNIQUEIDENTIFIER   NOT NULL,
    [ProjectCommunicationId]         INT                NOT NULL,
    [ProjectVersionXmlId]            INT                NULL,
    [ReadDate]                       DATETIME2          NULL,
    [Status]                         INT                NOT NULL,
    [Source]                         INT                NOT NULL,
    [OrderNum]                       INT                NOT NULL,

    [SendDate]                       DATETIME2          NULL,
    [Content]                        NVARCHAR(MAX)      NULL,
    [Xml]                            XML                NOT NULL,
    [Hash]                           NVARCHAR(10)       NOT NULL,

    CONSTRAINT [PK_ProjectCommunicationAnswers]                              PRIMARY KEY ([ProjectCommunicationAnswerId]),
    CONSTRAINT [FK_ProjectCommunicationAnswers_ProjectCommunications]        FOREIGN KEY ([ProjectCommunicationId])      REFERENCES [dbo].[ProjectCommunications] ([ProjectCommunicationId]),
    CONSTRAINT [FK_ProjectCommunicationAnswers_AnswerProjectVersionXmls]     FOREIGN KEY ([ProjectVersionXmlId])         REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [CHK_ProjectCommunicationAnswers_Status]                      CHECK ([Status] IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ProjectCommunicationAnswers_Source]                      CHECK ([Source] IN (1, 2))
);
GO

exec spDescTable  N'ProjectCommunicationAnswers', N'Отговор при комуникация между кандидат/УО относно ПП.'
exec spDescColumn N'ProjectCommunicationAnswers', N'ProjectCommunicationAnswerId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectCommunicationAnswers', N'ProjectCommunicationId'           , N'Идентификатор на комуникация с кандидат относно ПП.'
exec spDescColumn N'ProjectCommunicationAnswers', N'Gid'                              , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ProjectCommunicationAnswers', N'ProjectVersionXmlId'              , N'Идентификатор на версията на ПП, асоциирана с отговора.'
exec spDescColumn N'ProjectCommunicationAnswers', N'ReadDate'                         , N'Дата на прочитане.'
exec spDescColumn N'ProjectCommunicationAnswers', N'Status'                           , N'Статус: 1 - Чернова, 2 - Отговор финализиран 3 - Изпратен, 4 - Изпратен отговор на хартия, 5 - Анулиран.'
exec spDescColumn N'ProjectCommunicationAnswers', N'Source'                           , N'Подател: 1 - Бенефициент, 2 - УО.'
exec spDescColumn N'ProjectCommunicationAnswers', N'OrderNum'                         , N'Пореден номер'
exec spDescColumn N'ProjectCommunicationAnswers', N'SendDate'                         , N'Дата на изпращане на отговора.'
exec spDescColumn N'ProjectCommunicationAnswers', N'Xml'                              , N'Отговор от кандидат/УО.'
exec spDescColumn N'ProjectCommunicationAnswers', N'Hash'                             , N'Уникален идентификатор на съдържанието на отговора.'
exec spDescColumn N'ProjectCommunicationAnswers', N'Content'                          , N'Съдържание на отговора.'
GO
