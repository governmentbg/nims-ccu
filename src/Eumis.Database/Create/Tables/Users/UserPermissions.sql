PRINT 'UserPermissions'
GO

CREATE TABLE [dbo].[UserPermissions] (
    [UserPermissionId]   INT                NOT NULL IDENTITY,
    [UserId]             INT                NOT NULL,
    [Permission]         NVARCHAR (100)     NOT NULL,
    [PermissionType]     NVARCHAR (100)     NOT NULL,
    [ProgrammeId]        INT                NULL
    CONSTRAINT [PK_UserPermissions]                PRIMARY KEY    ([UserPermissionId]),
    CONSTRAINT [UQ_UserPermissions]                UNIQUE         ([UserId], [PermissionType], [Permission], [ProgrammeId]),
    CONSTRAINT [FK_UserPermissions_Users]          FOREIGN KEY    ([UserId])        REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_UserPermissions_MapNodes]       FOREIGN KEY    ([ProgrammeId])   REFERENCES [dbo].[MapNodes] ([MapNodeId])
);
GO

exec spDescTable  N'UserPermissions', N'Потребителски права'
exec spDescColumn N'UserPermissions', N'UserPermissionId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'UserPermissions', N'UserId'                 , N'Идентификатор на потребител.'
exec spDescColumn N'UserPermissions', N'PermissionType'         , N'Вид право.'
exec spDescColumn N'UserPermissions', N'Permission'             , N'Право.'
exec spDescColumn N'UserPermissions', N'ProgrammeId'            , N'Идентификатор на програма.'
GO
