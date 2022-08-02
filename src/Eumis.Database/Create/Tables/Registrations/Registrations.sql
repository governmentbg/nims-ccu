PRINT 'Registrations'
GO

CREATE TABLE [dbo].[Registrations] (
    [RegistrationId]        INT                 NOT NULL IDENTITY,
    [Email]                 NVARCHAR (200)      NOT NULL UNIQUE,
    [FirstName]             NVARCHAR (100)      NOT NULL,
    [LastName]              NVARCHAR (100)      NOT NULL,
    [Phone]                 NVARCHAR (50)       NULL,
    [PasswordHash]          NVARCHAR (200)      NULL,
    [PasswordSalt]          NVARCHAR (200)      NULL,
    [ActivationCode]        NVARCHAR (50)       NULL,
    [PasswordRecoveryCode]  NVARCHAR (50)       NULL,
    [CreateDate]            DATETIME2           NOT NULL,
    [ModifyDate]            DATETIME2           NOT NULL,
    [Version]               ROWVERSION          NOT NULL

    CONSTRAINT [PK_Registrations]   PRIMARY KEY ([RegistrationId])
);
GO

CREATE UNIQUE INDEX [UQ_Registrations_ActivationCode]
    ON [dbo].[Registrations]([ActivationCode]) WHERE [ActivationCode] IS NOT NULL
GO

CREATE UNIQUE INDEX [UQ_Registrations_PasswordRecoveryCode]
    ON [dbo].[Registrations]([PasswordRecoveryCode]) WHERE [PasswordRecoveryCode] IS NOT NULL
GO

exec spDescTable  N'Registrations', N'Организации.'
exec spDescColumn N'Registrations', N'RegistrationId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Registrations', N'Email'                , N'Ел. адрес.'
exec spDescColumn N'Registrations', N'FirstName'            , N'Собствено име.'
exec spDescColumn N'Registrations', N'LastName'             , N'Фамилно име.'
exec spDescColumn N'Registrations', N'Phone'                , N'Телефон.'
exec spDescColumn N'Registrations', N'PasswordHash'         , N'Криптирана парола.'
exec spDescColumn N'Registrations', N'PasswordSalt'         , N'SALT за криптираната парола.'
exec spDescColumn N'Registrations', N'ActivationCode'       , N'Код за активиране.'
exec spDescColumn N'Registrations', N'PasswordRecoveryCode' , N'Код за смяна на паролата.'
exec spDescColumn N'Registrations', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'Registrations', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Registrations', N'Version'              , N'Версия.'

GO
