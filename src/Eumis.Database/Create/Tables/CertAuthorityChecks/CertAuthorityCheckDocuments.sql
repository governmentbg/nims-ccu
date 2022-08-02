PRINT 'CertAuthorityCheckDocuments'
GO

CREATE TABLE [dbo].[CertAuthorityCheckDocuments] (
    [CertAuthorityCheckDocumentId]              INT                 NOT NULL IDENTITY,
    [CertAuthorityCheckId]                      INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_CertAuthorityCheckDocuments]                          PRIMARY KEY ([CertAuthorityCheckDocumentId]),
    CONSTRAINT [FK_CertAuthorityCheckDocuments_CertAuthorityChecks]      FOREIGN KEY ([CertAuthorityCheckId])  REFERENCES [dbo].[CertAuthorityChecks] ([CertAuthorityCheckId]),
    CONSTRAINT [FK_CertAuthorityCheckDocuments_Blobs]                    FOREIGN KEY ([BlobKey])               REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'CertAuthorityCheckDocuments', N'Документи към проверка на СО.'
exec spDescColumn N'CertAuthorityCheckDocuments', N'CertAuthorityCheckDocumentId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CertAuthorityCheckDocuments', N'CertAuthorityCheckId'                , N'Идентификатор на проверка на СО.'
exec spDescColumn N'CertAuthorityCheckDocuments', N'Name'                                , N'Наименование.'
exec spDescColumn N'CertAuthorityCheckDocuments', N'Description'                         , N'Описание.'
exec spDescColumn N'CertAuthorityCheckDocuments', N'BlobKey'                             , N'Идентификатор на файл.'

GO
