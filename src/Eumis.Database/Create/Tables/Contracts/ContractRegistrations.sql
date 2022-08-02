PRINT 'ContractRegistrations'
GO

CREATE TABLE [dbo].[ContractRegistrations] (
    [ContractRegistrationId]        INT                 NOT NULL IDENTITY,
    [Email]                         NVARCHAR (200)      NOT NULL UNIQUE,
    [UinType]                       INT                 NOT NULL,
    [Uin]                           NVARCHAR (50)       NOT NULL,
    [FirstName]                     NVARCHAR (100)      NOT NULL,
    [LastName]                      NVARCHAR (100)      NOT NULL,
    [Phone]                         NVARCHAR (50)       NULL,
    [PasswordHash]                  NVARCHAR (200)      NULL,
    [PasswordSalt]                  NVARCHAR (200)      NULL,
    [ActivationCode]                NVARCHAR (50)       NULL,
    [PasswordRecoveryCode]          NVARCHAR (50)       NULL,
    [CreateDate]                    DATETIME2           NOT NULL,
    [ModifyDate]                    DATETIME2           NOT NULL,
    [Version]                       ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractRegistrations]           PRIMARY KEY ([ContractRegistrationId]),
    CONSTRAINT [CHK_ContractRegistrations_UinType]  CHECK       ([UinType] IN (1, 2))
);
GO

CREATE UNIQUE INDEX [UQ_ContractRegistrations_ActivationCode]
    ON [dbo].[ContractRegistrations]([ActivationCode]) WHERE [ActivationCode] IS NOT NULL
GO

CREATE UNIQUE INDEX [UQ_ContractRegistrations_PasswordRecoveryCode]
    ON [dbo].[ContractRegistrations]([PasswordRecoveryCode]) WHERE [PasswordRecoveryCode] IS NOT NULL
GO

exec spDescTable  N'ContractRegistrations', N'Профили за договор за БФП.'
exec spDescColumn N'ContractRegistrations', N'ContractRegistrationId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractRegistrations', N'Email'                 , N'Ел. адрес.'
exec spDescColumn N'ContractRegistrations', N'UinType'               , N'1 - ЕГН, 2 - ЛНЧ.'
exec spDescColumn N'ContractRegistrations', N'Uin'                   , N'ЕГН/ЛНЧ.'
exec spDescColumn N'ContractRegistrations', N'FirstName'             , N'Собствено име.'
exec spDescColumn N'ContractRegistrations', N'LastName'              , N'Фамилно име.'
exec spDescColumn N'ContractRegistrations', N'Phone'                 , N'Телефон.'
exec spDescColumn N'ContractRegistrations', N'PasswordHash'          , N'Криптирана парола.'
exec spDescColumn N'ContractRegistrations', N'PasswordSalt'          , N'SALT за криптираната парола.'
exec spDescColumn N'ContractRegistrations', N'ActivationCode'        , N'Код за активиране.'
exec spDescColumn N'ContractRegistrations', N'PasswordRecoveryCode'  , N'Код за смяна на паролата.'
exec spDescColumn N'ContractRegistrations', N'CreateDate'            , N'Дата на създаване на записа.'
exec spDescColumn N'ContractRegistrations', N'ModifyDate'            , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractRegistrations', N'Version'               , N'Версия.'
GO
