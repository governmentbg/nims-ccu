PRINT 'RequestPackageUsers'
GO

CREATE TABLE [dbo].[RequestPackageUsers] (
    [RequestPackageId]           INT                NOT NULL,
    [UserId]                     INT                NOT NULL,
    [Status]                     INT                NOT NULL,
    [RejectionMessage]           NVARCHAR(MAX)      NULL,

    CONSTRAINT [PK_RequestPackageUsers]                   PRIMARY KEY   ([RequestPackageId], [UserId]),
    CONSTRAINT [CHK_RequestPackageUsers_Status]           CHECK         ([Status] IN (1, 2, 3, 4, 5, 6)),
    CONSTRAINT [FK_RequestPackageUsers_RequestPackages]   FOREIGN KEY   ([RequestPackageId])     REFERENCES [dbo].[RequestPackages] ([RequestPackageId]),
    CONSTRAINT [FK_RequestPackageUsers_Users]             FOREIGN KEY   ([UserId])               REFERENCES [dbo].[Users] ([UserId])
);
GO

exec spDescTable  N'RequestPackageUsers', N'Потребители към пакет заявки'
exec spDescColumn N'RequestPackageUsers', N'RequestPackageId'         , N'Идентификатор на пакет заявки.'
exec spDescColumn N'RequestPackageUsers', N'UserId'                   , N'Идентификатор на потребител.'

GO


