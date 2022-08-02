PRINT 'RegOfferXmls'
GO

CREATE TABLE [dbo].[RegOfferXmls] (
    [RegOfferXmlId]                         INT                 NOT NULL IDENTITY,
    [Gid]                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [RegistrationId]                        INT                 NOT NULL,
    [ContractDifferentiatedPositionId]      INT                 NOT NULL,
    [Xml]                                   XML                 NOT NULL,
    [Tenderer]                              NVARCHAR(200)       NULL,
    [Uin]                                   NVARCHAR(200)       NULL,
    [UinType]                               INT                 NULL,
    [Email]                                 NVARCHAR(200)       NULL,
    [Hash]                                  NVARCHAR(10)        NOT NULL UNIQUE,
    [Status]                                INT                 NOT NULL,
    [SubmitDate]                            DATETIME2           NULL,
    [CreateDate]                            DATETIME2           NOT NULL,
    [ModifyDate]                            DATETIME2           NOT NULL,
    [Version]                               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_RegOfferXmls]                            PRIMARY KEY ([RegOfferXmlId]),
    CONSTRAINT [FK_RegOfferXmls_Registrations]              FOREIGN KEY ([RegistrationId])                   REFERENCES [dbo].[Registrations] ([RegistrationId]),
    CONSTRAINT [FK_RegOfferXmls_DifferentiatedPositions]    FOREIGN KEY ([ContractDifferentiatedPositionId]) REFERENCES [dbo].[ContractDifferentiatedPositions] ([ContractDifferentiatedPositionId]),
    CONSTRAINT [CHK_RegOfferXmls_UinType]                   CHECK       ([Status] = 1 OR ([UinType] IS NOT NULL AND [UinType] IN (0, 1, 2, 3))),
    CONSTRAINT [CHK_RegOfferXmls_Tenderer]                  CHECK       ([Status] = 1 OR [Tenderer] IS NOT NULL),
    CONSTRAINT [CHK_RegOfferXmls_Email]                     CHECK       ([Status] = 1 OR [Email] IS NOT NULL),
    CONSTRAINT [CHK_RegOfferXmls_SubmitDate]                CHECK       ([Status] = 1 OR [SubmitDate] IS NOT NULL),
    CONSTRAINT [CHK_RegOfferXmls_Status]                    CHECK       ([Status] IN (1, 2, 3))
);
GO

exec spDescTable  N'RegOfferXmls', N'Xml за оферта към регистрация.'
exec spDescColumn N'RegOfferXmls', N'RegOfferXmlId'                         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RegOfferXmls', N'Gid'                                   , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'RegOfferXmls', N'RegistrationId'                        , N'Идентификатор на регистрация.'
exec spDescColumn N'RegOfferXmls', N'ContractDifferentiatedPositionId'      , N'Идентификатор на Обособена позиция към договор.'
exec spDescColumn N'RegOfferXmls', N'Xml'                                   , N'Xml съдържание.'
exec spDescColumn N'RegOfferXmls', N'Tenderer'                              , N'Оферент.'
exec spDescColumn N'RegOfferXmls', N'Uin'                                   , N'Уникален идентификационен номер на оферент.'
exec spDescColumn N'RegOfferXmls', N'UinType'                               , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'
exec spDescColumn N'RegOfferXmls', N'Email'                                 , N'Ел. адрес на оферент.'
exec spDescColumn N'RegOfferXmls', N'Hash'                                  , N'Уникален идентификатор на съдържанието на Xml-а.'
exec spDescColumn N'RegOfferXmls', N'Status'                                , N'Статус: 1 - Чернова, 2 - Въведена, 3 - Оттеглена'
exec spDescColumn N'RegOfferXmls', N'SubmitDate'                            , N'Дата на подаване'

exec spDescColumn N'RegOfferXmls', N'CreateDate'                            , N'Дата на създаване на записа.'
exec spDescColumn N'RegOfferXmls', N'ModifyDate'                            , N'Дата на последно редактиране на записа.'
exec spDescColumn N'RegOfferXmls', N'Version'                               , N'Версия.'

GO
