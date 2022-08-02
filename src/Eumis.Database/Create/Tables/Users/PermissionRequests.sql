PRINT 'PermissionRequests'
GO

CREATE TABLE [dbo].[PermissionRequests] (
    [RequestPackageId]           INT                NOT NULL,
    [UserId]                     INT                NOT NULL,
    [PermissionsString]          NVARCHAR(MAX)      NOT NULL,
    [PermissionTemplate]         NVARCHAR(MAX)      NOT NULL,

    CONSTRAINT [PK_PermissionRequests]                                     PRIMARY KEY   ([RequestPackageId], [UserId]),
    CONSTRAINT [FK_PermissionRequests_RequestPackages]                     FOREIGN KEY   ([RequestPackageId], [UserId])     REFERENCES [dbo].[RequestPackageUsers] ([RequestPackageId], [UserId])
);
GO

exec spDescTable  N'PermissionRequests', N'Заявки за промяна на права'
exec spDescColumn N'PermissionRequests', N'RequestPackageId'         , N'Идентификатор на пакет заявки.'
exec spDescColumn N'PermissionRequests', N'UserId'                   , N'Идентификатор на потребител.'
exec spDescColumn N'PermissionRequests', N'PermissionsString'        , N'Права.'
exec spDescColumn N'PermissionRequests', N'PermissionTemplate'       , N'Шаблон за група.'

GO


