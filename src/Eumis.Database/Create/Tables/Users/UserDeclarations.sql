PRINT 'UserDeclarations'
GO

CREATE TABLE [dbo].[UserDeclarations] (
    [UserId]                     INT                 NOT NULL,
    [DeclarationId]              INT                 NOT NULL,
    [AcceptDate]                 DATETIME2           NOT NULL,

    CONSTRAINT [PK_UserDeclarations]                    PRIMARY KEY ([UserId], [DeclarationId]),
    CONSTRAINT [FK_UserDeclarations_Users]              FOREIGN KEY ([UserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_UserDeclarations_Declartions]        FOREIGN KEY ([DeclarationId])    REFERENCES [dbo].[Declarations] ([DeclarationId])
);
GO

exec spDescTable  N'UserDeclarations', N'Декларации които потребителят е приел.'
exec spDescColumn N'UserDeclarations', N'UserId'           , N'Идентификатор на потребител.'
exec spDescColumn N'UserDeclarations', N'DeclarationId'    , N'Идентификатор на декларация.'
exec spDescColumn N'UserDeclarations', N'AcceptDate'       , N'Дата на приемане на декларацията.'
GO
