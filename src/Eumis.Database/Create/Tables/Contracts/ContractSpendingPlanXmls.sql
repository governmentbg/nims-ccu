PRINT 'ContractSpendingPlanXmls'
GO

CREATE TABLE [dbo].[ContractSpendingPlanXmls] (
    [ContractSpendingPlanXmlId] INT                 NOT NULL IDENTITY,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ContractId]                INT                 NOT NULL,
    [ContractVersionXmlId]      INT                 NOT NULL,
    [Source]                    INT                 NOT NULL,
    [Xml]                       XML                 NOT NULL,
    [Hash]                      NVARCHAR(10)        NOT NULL UNIQUE,
    [OrderNum]                  INT                 NOT NULL,
    [Status]                    INT                 NOT NULL,
    [CreatedByUserId]           INT                 NOT NULL,
    [CreateNote]                NVARCHAR(MAX)       NOT NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractSpendingPlanXmls]                 PRIMARY KEY ([ContractSpendingPlanXmlId]),
    CONSTRAINT [FK_ContractSpendingPlanXmls_Contracts]       FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractSpendingPlanXmls_VersionsXml]     FOREIGN KEY ([ContractVersionXmlId])    REFERENCES [dbo].[ContractVersionXmls] ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractSpendingPlanXmls_Users]           FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractSpendingPlanXmls_Source]         CHECK ([Source]      IN (1, 2)),
    CONSTRAINT [CHK_ContractSpendingPlanXmls_Status]         CHECK ([Status]      IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'ContractSpendingPlanXmls', N'Xml на план за разходване на средствата на договор за БФП.'
exec spDescColumn N'ContractSpendingPlanXmls', N'ContractSpendingPlanXmlId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractSpendingPlanXmls', N'Gid'                         , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractSpendingPlanXmls', N'ContractId'                  , N'Идентификатор на договор.'
exec spDescColumn N'ContractSpendingPlanXmls', N'ContractVersionXmlId'        , N'Идентификатор на версия на договор.'
exec spDescColumn N'ContractSpendingPlanXmls', N'Source'                      , N'Източник: 1 - Бенефициент, 2 – УО.'
exec spDescColumn N'ContractSpendingPlanXmls', N'Xml'                         , N'Xml съдържание.'
exec spDescColumn N'ContractSpendingPlanXmls', N'Hash'                        , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ContractSpendingPlanXmls', N'OrderNum'                    , N'Пореден номер.'
exec spDescColumn N'ContractSpendingPlanXmls', N'Status'                      , N'Статус: 1 - Чернова, 2 - Въведен, 3 - Актуален, 4 - Архивиран.'
exec spDescColumn N'ContractSpendingPlanXmls', N'CreatedByUserId'             , N'Създадено от.'
exec spDescColumn N'ContractSpendingPlanXmls', N'CreateNote'                  , N'Бележка.'
exec spDescColumn N'ContractSpendingPlanXmls', N'CreateDate'                  , N'Дата на създаване на записа.'
exec spDescColumn N'ContractSpendingPlanXmls', N'ModifyDate'                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractSpendingPlanXmls', N'Version'                     , N'Версия.'
GO

