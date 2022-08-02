PRINT 'ProjectMassManagingAuthorityCommunicationDocuments'
GO

CREATE TABLE [dbo].[ProjectMassManagingAuthorityCommunicationDocuments] (
    [ProjectMassManagingAuthorityCommunicationDocumentId]      INT                 NOT NULL IDENTITY,
    [ProjectMassManagingAuthorityCommunicationId]              INT                 NOT NULL,
    [Name]                                                     NVARCHAR(MAX)       NULL,
    [Description]                                              NVARCHAR(MAX)       NULL,
    [FileName]                                                 NVARCHAR(MAX)       NULL,
    [BlobKey]                                                  UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ProjectMassManagingAuthorityCommunicationDocuments]                                             PRIMARY KEY ([ProjectMassManagingAuthorityCommunicationDocumentId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunicationDocuments_ProjectMassManagingAuthorityCommunications]  FOREIGN KEY ([ProjectMassManagingAuthorityCommunicationId])  REFERENCES [dbo].[ProjectMassManagingAuthorityCommunications] ([ProjectMassManagingAuthorityCommunicationId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunicationDocuments_Blobs]                                       FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ProjectMassManagingAuthorityCommunicationDocuments', N'Документи към обща комуникация с УО.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'ProjectMassManagingAuthorityCommunicationDocumentId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'ProjectMassManagingAuthorityCommunicationId'         , N'Идентификатор на обща комуникация.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'Name'                                                , N'Наименование.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'Description'                                         , N'Описание.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'FileName'                                            , N'Наименование на файл.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationDocuments', N'BlobKey'                                             , N'Идентификатор на файл.'
GO
