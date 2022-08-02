GO

CREATE TABLE [dbo].[ProjectMassManagingAuthorityCommunications] (
    [ProjectMassManagingAuthorityCommunicationId]   INT             NOT NULL IDENTITY,
    [ProgrammeId]                                   INT             NOT NULL,
    [ProcedureId]                                   INT             NOT NULL,
    [OrderNum]                                      INT             NOT NULL,
    [Status]                                        INT             NOT NULL,
    [Subject]                                       INT             NULL,
    [Message]                                       NVARCHAR(MAX)   NULL,
    [EndingDate]                                    DATETIME2       NULL,

    [CreateDate]                                    DATETIME2       NOT NULL,
    [ModifyDate]                                    DATETIME2       NOT NULL,
    [Version]                                       ROWVERSION      NOT NULL,

    CONSTRAINT [PK_ProjectMassManagingAuthorityCommunications]                     PRIMARY KEY ([ProjectMassManagingAuthorityCommunicationId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunications_Procedures]          FOREIGN KEY ([ProcedureId])           REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunications_MapNodes]            FOREIGN KEY ([ProgrammeId])           REFERENCES [dbo].[MapNodes]    ([MapNodeId]),
    CONSTRAINT [CHK_ProjectMassManagingAuthorityCommunications_Status]             CHECK       ([Status] IN (1, 2)),
    CONSTRAINT [CHK_ProjectMassManagingAuthorityCommunications_Subject]            CHECK       ([Subject] IN (1, 2, 3, 4, 5, 6, 7))
);
GO

exec spDescTable  N'ProjectMassManagingAuthorityCommunications', N'Обща комуникация с УО по Проектно предложение.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'ProjectMassManagingAuthorityCommunicationId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'ProgrammeId'                                 , N'Идентификатор на оперативна програма.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'ProcedureId'                                 , N'Идентификатор на процедура.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'OrderNum'                                    , N'Пореден номер спрямо ОП.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'Status'                                      , N'Статус: 1 - Чернова, 2 - Изпратена.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'Subject'                                     , N'Тема на комуникацията: 1 - Оттегляне на проектно предложение; 2 - Жалба; 3 - Документи за сключване на Договор за безвъзмездна финансова помощ, 4 - Промени и обстоятелства, 5 - Съобщение, 6 - Отчитане пред Министерството на туризма, 7 - Връчване на актове.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'Message'                                     , N'Съдържание.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'EndingDate'                                  , N'Краен срок за отговор.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'CreateDate'                                  , N'Дата на създаване на записа.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'ModifyDate'                                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunications', N'Version'                                     , N'Версия.'
GO

CREATE TABLE [dbo].[ProjectMassManagingAuthorityCommunicationRecipients] (
    [ProjectMassManagingAuthorityCommunicationRecipientId]     INT                 NOT NULL IDENTITY,
    [ProjectMassManagingAuthorityCommunicationId]              INT                 NOT NULL,
    [ProjectId]                                                INT                 NOT NULL,

    CONSTRAINT [PK_ProjectMassManagingAuthorityCommunicationRecipients]                                             PRIMARY KEY ([ProjectMassManagingAuthorityCommunicationRecipientId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunicationRecipients_ProjectMassManagingAuthorityCommunications]  FOREIGN KEY ([ProjectMassManagingAuthorityCommunicationId])  REFERENCES [dbo].[ProjectMassManagingAuthorityCommunications] ([ProjectMassManagingAuthorityCommunicationId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunicationRecipients_Projects]                                    FOREIGN KEY ([ProjectId])                    REFERENCES [dbo].[Projects] ([ProjectId]),
);
GO

exec spDescTable  N'ProjectMassManagingAuthorityCommunicationRecipients', N'Получатели на обща комуникация с УО.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationRecipients', N'ProjectMassManagingAuthorityCommunicationRecipientId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationRecipients', N'ProjectMassManagingAuthorityCommunicationId'          , N'Идентификатор на обща комуникация с УО.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationRecipients', N'ProjectId'                                            , N'Идентификатор на проектно предложение.'
GO

CREATE TABLE [dbo].[ProjectMassManagingAuthorityCommunicationDocuments] (
    [ProjectMassManagingAuthorityCommunicationDocumentId]      INT                 NOT NULL IDENTITY,
    [ProjectMassManagingAuthorityCommunicationId]              INT                 NOT NULL,
    [Name]                                                     NVARCHAR(MAX)       NULL,
    [Description]                                              NVARCHAR(MAX)       NULL,
    [FileName]                                                 NVARCHAR(MAX)       NULL,
    [BlobKey]                                                  UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ProjectMassManagingAuthorityCommunicationDocuments]                                             PRIMARY KEY ([ProjectMassManagingAuthorityCommunicationDocumentId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunicationDocuments_ProjectMassManagingAuthorityCommunications]  FOREIGN KEY ([ProjectMassManagingAuthorityCommunicationId])  REFERENCES [dbo].[ProjectMassManagingAuthorityCommunications] ([ProjectMassManagingAuthorityCommunicationId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunicationDocuments_Blobs]                                       FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ProjectMassManagingAuthorityCommunicationDocuments', N'Документи към обща комуникация с УО.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'ProjectMassManagingAuthorityCommunicationDocumentId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'ProjectMassManagingAuthorityCommunicationId'         , N'Идентификатор на обща комуникация.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'Name'                                                , N'Наименование.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'Description'                                         , N'Описание.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'FileName'                                            , N'Наименование на файл.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'BlobKey'                                             , N'Идентификатор на файл.'
GO

ALTER TABLE [dbo].[ProjectCommunications] DROP CONSTRAINT [CHK_ProjectCommunications_Subject]
GO
ALTER TABLE [dbo].[ProjectCommunications] WITH CHECK ADD CONSTRAINT [CHK_ProjectCommunications_Subject] CHECK ([Subject]   IN (1, 2, 3, 4, 5, 6, 7))
GO
