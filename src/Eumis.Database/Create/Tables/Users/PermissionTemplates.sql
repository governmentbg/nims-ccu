PRINT 'PermissionTemplates'
GO

CREATE TABLE [dbo].[PermissionTemplates] (
    [PermissionTemplateId]  INT                 NOT NULL IDENTITY,
    [Name]                  NVARCHAR(100)       NOT NULL,
    [PermissionsString]     NVARCHAR(MAX)       NOT NULL,
    [CreateDate]            DATETIME2           NOT NULL,
    [ModifyDate]            DATETIME2           NOT NULL,
    [Version]               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_PermissionTemplates]         PRIMARY KEY    ([PermissionTemplateId])
);
GO

exec spDescTable  N'PermissionTemplates', N'Шаблон за група'
exec spDescColumn N'PermissionTemplates', N'PermissionTemplateId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'PermissionTemplates', N'Name'                   , N'Име на шаблона.'
exec spDescColumn N'PermissionTemplates', N'PermissionsString'      , N'Шаблон.'
exec spDescColumn N'PermissionTemplates', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'PermissionTemplates', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'PermissionTemplates', N'Version'                , N'Версия.'
GO


