PRINT 'UserOrganizations'
GO

CREATE TABLE [dbo].[UserOrganizations] (
    [UserOrganizationId]         INT                NOT NULL IDENTITY,
    [Name]                       NVARCHAR(200)      NOT NULL,
    [CreateDate]                 DATETIME2          NOT NULL,
    [ModifyDate]                 DATETIME2          NOT NULL,
    [Version]                    ROWVERSION         NOT NULL,

    CONSTRAINT [PK_UserOrganizations]                        PRIMARY KEY    ([UserOrganizationId])
);
GO

exec spDescTable  N'UserOrganizations', N'Организации за групи потребители'
exec spDescColumn N'UserOrganizations', N'UserOrganizationId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'UserOrganizations', N'Name'                   , N'Наименование.'
exec spDescColumn N'UserOrganizations', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'UserOrganizations', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'UserOrganizations', N'Version'                , N'Версия.'

GO


