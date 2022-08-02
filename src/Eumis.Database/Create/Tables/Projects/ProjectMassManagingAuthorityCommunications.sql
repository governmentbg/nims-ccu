PRINT 'ProjectMassManagingAuthorityCommunications'
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
