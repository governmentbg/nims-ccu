PRINT 'ContractProcurementXmls'
GO

CREATE TABLE [dbo].[ContractProcurementXmls] (
    [ContractProcurementXmlId]  INT                 NOT NULL IDENTITY,
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

    CONSTRAINT [PK_ContractProcurementXmls]                 PRIMARY KEY ([ContractProcurementXmlId]),
    CONSTRAINT [FK_ContractProcurementXmls_Contracts]       FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractProcurementXmls_VersionsXml]     FOREIGN KEY ([ContractVersionXmlId])    REFERENCES [dbo].[ContractVersionXmls] ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractProcurementXmls_Users]           FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractProcurementXmls_Source]         CHECK ([Source]      IN (1, 2)),
    CONSTRAINT [CHK_ContractProcurementXmls_Status]         CHECK ([Status]      IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'ContractProcurementXmls', N'Xml на процедурa за избор на изпълнител и сключени договори на договор за БФП.'
exec spDescColumn N'ContractProcurementXmls', N'ContractProcurementXmlId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractProcurementXmls', N'Gid'                         , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractProcurementXmls', N'ContractId'                  , N'Идентификатор на договор.'
exec spDescColumn N'ContractProcurementXmls', N'ContractVersionXmlId'        , N'Идентификатор на версия на договор.'
exec spDescColumn N'ContractProcurementXmls', N'Source'                      , N'Източник: 1 - Бенефициент, 2 – УО.'
exec spDescColumn N'ContractProcurementXmls', N'Xml'                         , N'Xml съдържание.'
exec spDescColumn N'ContractProcurementXmls', N'Hash'                        , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'ContractProcurementXmls', N'OrderNum'                    , N'Пореден номер.'
exec spDescColumn N'ContractProcurementXmls', N'Status'                      , N'Статус: 1 - Чернова, 2 - Въведен, 3 - Актуален, 4 - Архивиран.'
exec spDescColumn N'ContractProcurementXmls', N'CreatedByUserId'             , N'Създадено от.'
exec spDescColumn N'ContractProcurementXmls', N'CreateNote'                  , N'Бележка.'
exec spDescColumn N'ContractProcurementXmls', N'CreateDate'                  , N'Дата на създаване на записа.'
exec spDescColumn N'ContractProcurementXmls', N'ModifyDate'                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractProcurementXmls', N'Version'                     , N'Версия.'
GO

