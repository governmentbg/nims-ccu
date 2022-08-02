PRINT 'CertAuthorityCheckProjects'
GO

CREATE TABLE [dbo].[CertAuthorityCheckProjects] (
    [CertAuthorityCheckProjectId]       INT                 NOT NULL IDENTITY,
    [CertAuthorityCheckId]              INT                 NOT NULL,
    [ProjectId]                         INT                 NOT NULL,

    CONSTRAINT [PK_CertAuthorityCheckProjects]                          PRIMARY KEY ([CertAuthorityCheckProjectId]),
    CONSTRAINT [FK_CertAuthorityCheckProjects_CertAuthorityChecks]      FOREIGN KEY ([CertAuthorityCheckId])            REFERENCES [dbo].[CertAuthorityChecks] ([CertAuthorityCheckId]),
    CONSTRAINT [FK_CertAuthorityCheckProjects_Projects]                 FOREIGN KEY ([ProjectId])                       REFERENCES [dbo].[Projects] ([ProjectId]),
);
GO

exec spDescTable  N'CertAuthorityCheckProjects' , N'Проекти към проверката.'
exec spDescColumn N'CertAuthorityCheckProjects' , N'CertAuthorityCheckProjectId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CertAuthorityCheckProjects' , N'CertAuthorityCheckId'           , N'Идентификатор на проверки на СО.'
exec spDescColumn N'CertAuthorityCheckProjects' , N'ProjectId'                      , N'Идентификатор на проектно предложение.'

GO
