PRINT 'ContractCommunicationXmls'
GO

CREATE TABLE [dbo].[ContractCommunicationXmls] (
    [ContractCommunicationXmlId]    INT                NOT NULL IDENTITY,
    [Gid]                           UNIQUEIDENTIFIER   NOT NULL,
    [ContractId]                    INT                NOT NULL,
    [Type]                          INT                NOT NULL,
    [Status]                        INT                NOT NULL,
    [StatusNote]                    NVARCHAR(MAX)      NULL,
    [Source]                        INT                NOT NULL,
    [RegNumber]                     NVARCHAR(200)      NULL,
    [ReadDate]                      DATETIME2          NULL,

    [SendDate]                      DATETIME2          NULL,
    [Subject]                       NVARCHAR(MAX)      NULL,
    [Content]                       NVARCHAR(MAX)      NULL,
    [Xml]                           XML                NOT NULL,
    [Hash]                          NVARCHAR(10)       NOT NULL UNIQUE,

    [CreateDate]                    DATETIME2          NOT NULL,
    [ModifyDate]                    DATETIME2          NOT NULL,
    [Version]                       ROWVERSION         NOT NULL

    CONSTRAINT [PK_ContractCommunicationXmls]                              PRIMARY KEY ([ContractCommunicationXmlId]),
    CONSTRAINT [FK_ContractCommunicationXmls_Contracts]                    FOREIGN KEY ([ContractId])                   REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_ContractCommunicationXmls_Source]                      CHECK ([Source] IN (1, 2)),
    CONSTRAINT [CHK_ContractCommunicationXmls_Status]                      CHECK ([Status] IN (1, 2)),
    CONSTRAINT [CHK_ContractCommunicationXmls_Type]                        CHECK ([Type]   IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractCommunicationXmls', N'Комуникация с бенефициент отсносно договор за БФП.'
exec spDescColumn N'ContractCommunicationXmls', N'ContractCommunicationXmlId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractCommunicationXmls', N'Gid'                             , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractCommunicationXmls', N'ContractId'                      , N'Идентификатор на договор за БФП.'
exec spDescColumn N'ContractCommunicationXmls', N'Type'                            , N'Тип: 1 - Administrative, 2 - Cert, 3 - Audit.'
exec spDescColumn N'ContractCommunicationXmls', N'Status'                          , N'Статус: 1 - Чернова; 2 - Изпратено съобщение;'
exec spDescColumn N'ContractCommunicationXmls', N'StatusNote'                      , N'Бележка.'
exec spDescColumn N'ContractCommunicationXmls', N'Source'                          , N'Източник: 1 - Бенефициент, 2 – Вътрешен орган.'
exec spDescColumn N'ContractCommunicationXmls', N'RegNumber'                       , N'Системно генериран регистрационен номер.'
exec spDescColumn N'ContractCommunicationXmls', N'ReadDate'                        , N'Дата на първо отваряне.'
exec spDescColumn N'ContractCommunicationXmls', N'SendDate'                        , N'Дата на изпращане на съобщението.'
exec spDescColumn N'ContractCommunicationXmls', N'Subject'                         , N'Заглавие на съобщението.'
exec spDescColumn N'ContractCommunicationXmls', N'Content'                         , N'Съдържание на съобщението.'
exec spDescColumn N'ContractCommunicationXmls', N'Xml'                             , N'Съобщение.'
exec spDescColumn N'ContractCommunicationXmls', N'Hash'                            , N'Уникален идентификатор на съдържанието на съобщението.'
exec spDescColumn N'ContractCommunicationXmls', N'CreateDate'                      , N'Дата на създаване на записа.'
exec spDescColumn N'ContractCommunicationXmls', N'ModifyDate'                      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractCommunicationXmls', N'Version'                         , N'Версия.'
GO

