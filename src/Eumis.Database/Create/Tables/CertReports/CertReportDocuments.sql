PRINT 'CertReportDocuments'
GO

CREATE TABLE [dbo].[CertReportDocuments] (
    [CertReportDocumentId]                      INT                 NOT NULL IDENTITY,
    [CertReportId]                              INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_CertReportDocuments]                          PRIMARY KEY ([CertReportDocumentId]),
    CONSTRAINT [FK_CertReportDocuments_CertReports]              FOREIGN KEY ([CertReportId])     REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [FK_CertReportDocuments_Blobs]                    FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'CertReportDocuments', N'Документи към доклад по сертификация.'
exec spDescColumn N'CertReportDocuments', N'CertReportDocumentId'                , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CertReportDocuments', N'CertReportId'                        , N'Идентификатор на доклад по сертификация.'
exec spDescColumn N'CertReportDocuments', N'Name'                                , N'Наименование.'
exec spDescColumn N'CertReportDocuments', N'Description'                         , N'Описание.'
exec spDescColumn N'CertReportDocuments', N'BlobKey'                             , N'Идентификатор на файл.'

GO
