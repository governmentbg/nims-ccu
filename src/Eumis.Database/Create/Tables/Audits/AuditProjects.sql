PRINT 'AuditProjects'
GO

CREATE TABLE [dbo].[AuditProjects] (
    [AuditProjectId]        INT                 NOT NULL IDENTITY,
    [AuditId]               INT                 NOT NULL,
    [ProjectId]             INT                 NOT NULL,

    CONSTRAINT [PK_AuditProjects]               PRIMARY KEY ([AuditProjectId]),
    CONSTRAINT [FK_AuditProjects_Audits]        FOREIGN KEY ([AuditId])             REFERENCES [dbo].[Audits] ([AuditId]),
    CONSTRAINT [FK_AuditProjects_Projects]      FOREIGN KEY ([ProjectId])           REFERENCES [dbo].[Projects] ([ProjectId]),
);
GO

exec spDescTable  N'AuditProjects'  , N'Проекти към проверката.'
exec spDescColumn N'AuditProjects'  , N'AuditProjectId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AuditProjects'  , N'AuditId'            , N'Идентификатор на проверки на СО.'
exec spDescColumn N'AuditProjects'  , N'ProjectId'          , N'Идентификатор на проектно предложение.'

GO
