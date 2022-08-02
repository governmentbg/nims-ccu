PRINT 'UserTypes'
GO

CREATE TABLE [dbo].[UserTypes] (
    [UserTypeId]                 INT                NOT NULL IDENTITY,
    [Name]                       NVARCHAR(100)      NOT NULL,
    [IsSuperUser]                BIT                NOT NULL,
    [PermissionTemplateId]       INT                NOT NULL,
    [UserOrganizationId]         INT                NOT NULL,
    [CreateDate]                 DATETIME2          NOT NULL,
    [ModifyDate]                 DATETIME2          NOT NULL,
    [Version]                    ROWVERSION         NOT NULL,

    CONSTRAINT [PK_UserTypes]                        PRIMARY KEY    ([UserTypeId]),
    CONSTRAINT [FK_UserTypes_PermissionTemplate]     FOREIGN KEY    ([PermissionTemplateId]) REFERENCES [dbo].[PermissionTemplates] ([PermissionTemplateId]),
    CONSTRAINT [FK_UserTypes_UserOrganization]       FOREIGN KEY    ([UserOrganizationId])   REFERENCES [dbo].[UserOrganizations] ([UserOrganizationId])
);
GO

exec spDescTable  N'UserTypes', N'Групи потребители'
exec spDescColumn N'UserTypes', N'UserTypeId'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'UserTypes', N'Name'                   , N'Наименование.'
exec spDescColumn N'UserTypes', N'IsSuperUser'            , N'Маркер за суперпотребител.'
exec spDescColumn N'UserTypes', N'PermissionTemplateId'   , N'Идентификатор на шаблон.'
exec spDescColumn N'UserTypes', N'UserOrganizationId'     , N'Идентификатор на организация.'
exec spDescColumn N'UserTypes', N'Version'                , N'Версия.'

GO


