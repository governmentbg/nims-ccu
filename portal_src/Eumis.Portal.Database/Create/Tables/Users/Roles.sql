PRINT 'Roles'
GO

CREATE TABLE [dbo].[Roles] (
    [RoleId]        INT              NOT NULL,
    [Name]          NVARCHAR (200)   NOT NULL,
    [Permissions]   NVARCHAR (MAX)  NULL
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
);
GO

exec spDescTable  N'Roles', N'Роли'
exec spDescColumn N'Roles', N'RoleId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Roles', N'Name'       , N'Име на ролята.'
exec spDescColumn N'Roles', N'Permissions', N'Права.'
GO
