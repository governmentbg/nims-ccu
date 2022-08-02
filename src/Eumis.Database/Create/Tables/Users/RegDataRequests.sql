PRINT 'RegDataRequests'
GO

CREATE TABLE [dbo].[RegDataRequests] (
    [RequestPackageId]           INT                NOT NULL,
    [UserId]                     INT                NOT NULL,
    [Uin]                        NVARCHAR (50)      NOT NULL,
    [Fullname]                   NVARCHAR (200)     NOT NULL,
    [Email]                      NVARCHAR (100)     NOT NULL,
    [Phone]                      NVARCHAR (100)     NULL,
    [Address]                    NVARCHAR (300)     NULL,
    [Position]                   NVARCHAR (300)     NULL,
    [UserOrganizationId]         INT                NOT NULL,
    [UserTypeId]                 INT                NOT NULL,

    CONSTRAINT [PK_RegDataRequests]                                     PRIMARY KEY   ([RequestPackageId], [UserId]),
    CONSTRAINT [FK_RegDataRequests_RequestPackages]                     FOREIGN KEY   ([RequestPackageId], [UserId])     REFERENCES [dbo].[RequestPackageUsers] ([RequestPackageId], [UserId]),
    CONSTRAINT [FK_RegDataRequests_UserOrganizations]                   FOREIGN KEY   ([UserOrganizationId])             REFERENCES [dbo].[UserOrganizations] ([UserOrganizationId]),
    CONSTRAINT [FK_RegDataRequests_UserTypes]                           FOREIGN KEY   ([UserTypeId])                     REFERENCES [dbo].[UserTypes] ([UserTypeId])
);
GO

exec spDescTable  N'RegDataRequests', N'Заявки за промяна на регистрационни данни'
exec spDescColumn N'RegDataRequests', N'RequestPackageId'         , N'Идентификатор на пакет заявки.'
exec spDescColumn N'RegDataRequests', N'UserId'                   , N'Идентификатор на потребител.'
exec spDescColumn N'RegDataRequests', N'Uin'                      , N'ЕГН.'
exec spDescColumn N'RegDataRequests', N'Fullname'                 , N'Пълно име'
exec spDescColumn N'RegDataRequests', N'Email'                    , N'E-mail.'
exec spDescColumn N'RegDataRequests', N'Phone'                    , N'Телефон.'
exec spDescColumn N'RegDataRequests', N'Address'                  , N'Адрес.'
exec spDescColumn N'RegDataRequests', N'Position'                 , N'Длъжност.'
exec spDescColumn N'RegDataRequests', N'UserOrganizationId'       , N'Идентификатор на организация.'
exec spDescColumn N'RegDataRequests', N'UserTypeId'               , N'Идентификатор на групи потребители.'

GO


