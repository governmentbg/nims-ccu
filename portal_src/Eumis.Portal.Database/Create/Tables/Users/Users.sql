PRINT 'Users'
GO

CREATE TABLE [dbo].[Users] (
    [UserId]                INT                 NOT NULL IDENTITY,
    [Username]              NVARCHAR (200)      NOT NULL UNIQUE,
    [PasswordHash]          NVARCHAR (200)      NULL,
    [PasswordSalt]          NVARCHAR (200)      NULL,
    [Fullname]              NVARCHAR (200)      NULL,
    [Notes]                 NVARCHAR (MAX)      NULL,
    [Email]                 NVARCHAR (100)      NULL,
    [IsActive]              BIT                 NOT NULL,
    [CreateDate]            DATETIME2           NOT NULL,
    [ModifyDate]            DATETIME2           NOT NULL,
    [Version]               ROWVERSION          NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
);
GO

exec spDescTable  N'Users', N'Потребители.'
exec spDescColumn N'Users', N'UserId'               , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Users', N'Username'             , N'Потребителско име.'
exec spDescColumn N'Users', N'PasswordHash'         , N'Криптирана парола.'
exec spDescColumn N'Users', N'PasswordSalt'         , N'SALT за криптираната парола.'
exec spDescColumn N'Users', N'Fullname'             , N'Пълно име.'
exec spDescColumn N'Users', N'Notes'                , N'Бележки.'
exec spDescColumn N'Users', N'IsActive'             , N'Маркер за активност.'
exec spDescColumn N'Users', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'Users', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Users', N'Version'              , N'Версия.'
GO
