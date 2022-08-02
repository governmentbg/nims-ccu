GO
ALTER TABLE [dbo].[ProjectCommunications] ADD [Discriminator] INT NOT NULL CONSTRAINT DEFAULT_ProjectCommunicationsDiscriminator DEFAULT 1;

ALTER TABLE [dbo].[ProjectCommunications] DROP CONSTRAINT DEFAULT_ProjectCommunicationsDiscriminator;
GO

GO
ALTER TABLE [dbo].[ProjectCommunications] ADD CONSTRAINT [CHK_ProjectCommunications_Discriminator] CHECK ([Discriminator]   IN (1, 2));
GO

ALTER TABLE [dbo].[ProjectCommunications]
ADD [ManagingAuthorityCommunicationStatus]                INT     NULL,
    [Source]                                              INT     NULL,
    [Subject]                                             INT     NULL,
CONSTRAINT [CHK_ProjectCommunications_ManagingAuthorityCommunicationStatus]  CHECK ([ManagingAuthorityCommunicationStatus]    IN (1, 2, 3, 4)),
CONSTRAINT [CHK_ProjectCommunications_Source]                                CHECK ([Source]    IN (1, 2)),
CONSTRAINT [CHK_ProjectCommunications_Subject]                               CHECK ([Subject]   IN (1, 2, 3));
GO

ALTER TABLE [dbo].[ProjectCommunications] ALTER COLUMN [EvalSessionId] INT NULL
GO
ALTER TABLE [dbo].[ProjectCommunications] ALTER COLUMN [Status] INT NULL
GO

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

ALTER TABLE [ProjectCommunicationMessageFiles]
ADD [ProjectCommunicationAnswerId] INT NULL,
CONSTRAINT [FK_ProjectCommunicationMessageFiles_ProjectCommunicationAnswers]   FOREIGN KEY ([ProjectCommunicationAnswerId])    REFERENCES [dbo].[ProjectCommunicationAnswers] ([ProjectCommunicationAnswerId]);

GO

ALTER TABLE [ProjectCommunicationFiles]
ADD [ProjectCommunicationAnswerId] INT NULL,
CONSTRAINT [FK_ProjectCommunicationFiles_ProjectCommunicationAnswers]   FOREIGN KEY ([ProjectCommunicationAnswerId])   REFERENCES [dbo].[ProjectCommunicationAnswers] ([ProjectCommunicationAnswerId]);
GO

ALTER TABLE [dbo].[ProjectVersionXmls] ALTER COLUMN [CreatedByUserId] INT NULL

GO
INSERT INTO [ProjectCommunicationAnswers] (
    [Gid],
    [ProjectCommunicationId],
    [ProjectVersionXmlId],
    [Status],
    [Source],
    [OrderNum],
    [SendDate],
    [Content],
    [Xml],
    [Hash])
SELECT
    NEWID() AS [Gid],
    [ProjectCommunicationId] AS [ProjectCommunicationId],
    [AnswerProjectVersionXmlId] AS [ProjectVersionXmlId],
    CASE
        WHEN [Status] = 3 THEN 1
        WHEN [Status] = 4 THEN 2
        WHEN [Status] = 5 THEN 3
        WHEN [Status] = 6 THEN 4
        WHEN [Status] = 7 THEN 3
        WHEN [Status] = 8 THEN 5
        WHEN [Status] = 9 THEN 5
        WHEN [Status] = 10 THEN 5
    END AS [Status],
    1 AS [Source],
    1 AS [OrderNum],
    [AnswerDate] AS [AnswerDate],
    [AnswerXml].value('declare namespace R10020 = "http://ereg.egov.bg/segment/R-10020"; (/Message/R10020:Reply)[1]', 'nvarchar(max)') AS [Content],
    [AnswerXml] AS [Xml],
    [AnswerHash] AS [Hash]
FROM [ProjectCommunications]
WHERE [Status] IN (3, 4, 5, 6, 7, 8, 9, 10) AND [AnswerXml] IS NOT NULL
GO

GO
UPDATE pcmf
SET [ProjectCommunicationAnswerId] = pca.ProjectCommunicationAnswerId
FROM
[ProjectCommunicationMessageFiles] AS pcmf
JOIN [ProjectCommunicationAnswers] pca   ON pca.ProjectCommunicationId = pcmf.ProjectCommunicationId
WHERE pcmf.[Type] = 2
GO

GO
UPDATE pcf
SET [ProjectCommunicationAnswerId] = pca.ProjectCommunicationAnswerId
FROM
[ProjectCommunicationFiles] AS pcf
JOIN [ProjectCommunicationAnswers] pca   ON pca.ProjectCommunicationId = pcf.ProjectCommunicationId

GO
