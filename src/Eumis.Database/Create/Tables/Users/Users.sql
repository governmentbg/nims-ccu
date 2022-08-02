PRINT 'Users'
GO

CREATE TABLE [dbo].[Users] (
    [UserId]                    INT                 NOT NULL IDENTITY,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Username]                  NVARCHAR (200)      NOT NULL UNIQUE,
    [Uin]                       NVARCHAR (50)       NOT NULL,
    [UserTypeId]                INT                 NOT NULL,
    [UserOrganizationId]        INT                 NOT NULL,
    [PasswordHash]              NVARCHAR (200)      NULL,
    [PasswordSalt]              NVARCHAR (200)      NULL,
    [Fullname]                  NVARCHAR (200)      NOT NULL,
    [Email]                     NVARCHAR (100)      NOT NULL,
    [Phone]                     NVARCHAR (100)      NULL,
    [Address]                   NVARCHAR (300)      NULL,
    [Position]                  NVARCHAR (300)      NULL,
    [IsActive]                  BIT                 NOT NULL,
    [IsDeleted]                 BIT                 NOT NULL,
    [IsLocked]                  BIT                 NOT NULL,
    [IsSystem]                  BIT                 NOT NULL,
    [PermissionTemplate]        NVARCHAR(MAX)       NOT NULL,
    [PasswordRecoveryCode]      NVARCHAR (50)       NULL,
    [NewPasswordCode]           NVARCHAR (50)       NULL,
    [FailedAttempts]            INT                 NOT NULL,
    [GDPRDeclarationAcceptDate] DATETIME2           NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_UserTypes] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserTypes] ([UserTypeId]),
    CONSTRAINT [FK_Users_UserOrganizations] FOREIGN KEY ([UserOrganizationId]) REFERENCES [dbo].[UserOrganizations] ([UserOrganizationId])
);
GO

exec spDescTable  N'Users', N'Потребители.'
exec spDescColumn N'Users', N'UserId'               , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Users', N'Username'             , N'Потребителско име.'
exec spDescColumn N'Users', N'Uin'                  , N'ЕГН.'
exec spDescColumn N'Users', N'UserTypeId'           , N'Идентификатор на група потребители.'
exec spDescColumn N'Users', N'UserOrganizationId'   , N'Идентификатор на организация.'
exec spDescColumn N'Users', N'PasswordHash'         , N'Криптирана парола.'
exec spDescColumn N'Users', N'PasswordSalt'         , N'SALT за криптираната парола.'
exec spDescColumn N'Users', N'Fullname'             , N'Пълно име(трите имена).'
exec spDescColumn N'Users', N'Email'                , N'E-mail.'
exec spDescColumn N'Users', N'Phone'                , N'Телефон.'
exec spDescColumn N'Users', N'Address'              , N'Адрес.'
exec spDescColumn N'Users', N'Position'             , N'Длъжност.'
exec spDescColumn N'Users', N'IsActive'             , N'Маркер за активност.'
exec spDescColumn N'Users', N'IsDeleted'            , N'Маркер дали потребителят е изтрит.'
exec spDescColumn N'Users', N'IsLocked'             , N'Маркер дали потребителят е закллючен.'
exec spDescColumn N'Users', N'PermissionTemplate'   , N'Шаблон за група на потребител.'
exec spDescColumn N'Users', N'PasswordRecoveryCode' , N'Код за смяна на паролата.'
exec spDescColumn N'Users', N'NewPasswordCode'      , N'Код за първоначално въвеждане на парола.'
exec spDescColumn N'Users', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'Users', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Users', N'Version'              , N'Версия.'
GO
