PRINT 'RegProjectXmls'
GO

CREATE TABLE [dbo].[RegProjectXmls] (
    [RegProjectXmlId]           INT                 NOT NULL IDENTITY,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [RegistrationId]            INT                 NOT NULL,
    [ProcedureId]               INT                 NOT NULL,
    [Status]                    INT                 NOT NULL,
    [ProjectName]               NVARCHAR(MAX)       NULL,
    [ProjectNameAlt]            NVARCHAR(MAX)       NULL,
    [CompanyName]               NVARCHAR(200)       NULL,
    [CompanyNameAlt]            NVARCHAR(200)       NULL,
    [RegistrationType]          INT                 NULL,
    [ProjectId]                 INT                 NULL,
    [Xml]                       XML                 NOT NULL,
    [Hash]                      NVARCHAR(10)        NOT NULL UNIQUE,

    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_RegProjectXmls]                      PRIMARY KEY ([RegProjectXmlId]),
    CONSTRAINT [FK_RegProjectXmls_Registrations]        FOREIGN KEY ([RegistrationId])      REFERENCES [dbo].[Registrations] ([RegistrationId]),
    CONSTRAINT [FK_RegProjectXmls_Procedures]           FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_RegProjectXmls_Projects]             FOREIGN KEY ([ProjectId])           REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [CHK_RegProjectXmls_Status]              CHECK ([Status] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_RegProjectXmls_RegistrationType]    CHECK ([RegistrationType] IN (NULL, 1, 2))
);
GO

CREATE UNIQUE INDEX [UQ_RegProjectXmls_ProjectId]
    ON [dbo].[RegProjectXmls]([ProjectId]) WHERE [ProjectId] IS NOT NULL
GO

exec spDescTable  N'RegProjectXmls', N'Xml за проектно предложение към регистрация.'
exec spDescColumn N'RegProjectXmls', N'RegProjectXmlId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RegProjectXmls', N'Gid'                 , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'RegProjectXmls', N'RegistrationId'      , N'Идентификатор на регистрация.'
exec spDescColumn N'RegProjectXmls', N'ProcedureId'         , N'Идентификатор на процедура.'
exec spDescColumn N'RegProjectXmls', N'Status'              , N'Статус на формуляра. 1 - Чернова, 2 - Приключен. 3 - Подаден, 4 - Регистриран.'
exec spDescColumn N'RegProjectXmls', N'ProjectName'         , N'Наименование на проекта.'
exec spDescColumn N'RegProjectXmls', N'ProjectNameAlt'      , N'Наименование на проекта на друг език.'
exec spDescColumn N'RegProjectXmls', N'CompanyName'         , N'Наименование на кандидат.'
exec spDescColumn N'RegProjectXmls', N'CompanyNameAlt'      , N'Наименование на кандидат на друг език.'
exec spDescColumn N'RegProjectXmls', N'RegistrationType'    , N'Тип на регистрация. 1 - Електронно, 2 - На хартия.'
exec spDescColumn N'RegProjectXmls', N'ProjectId'           , N'Идентификатор на ПП.'
exec spDescColumn N'RegProjectXmls', N'Xml'                 , N'Xml съдържание.'
exec spDescColumn N'RegProjectXmls', N'Hash'                , N'Уникален идентификатор на съдържанието на Xml-а.'

exec spDescColumn N'RegProjectXmls', N'CreateDate'          , N'Дата на създаване на записа.'
exec spDescColumn N'RegProjectXmls', N'ModifyDate'          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'RegProjectXmls', N'Version'             , N'Версия.'

GO
