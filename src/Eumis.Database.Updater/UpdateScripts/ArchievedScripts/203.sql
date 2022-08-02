PRINT 'CertReportCertificationDocuments'
GO

CREATE TABLE [dbo].[CertReportCertificationDocuments] (
    [CertReportCertificationDocumentId]         INT                 NOT NULL IDENTITY,
    [CertReportId]                              INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_CertReportCertificationDocuments]              PRIMARY KEY ([CertReportCertificationDocumentId]),
    CONSTRAINT [FK_CertReportCertificationDocuments_CertReports]  FOREIGN KEY ([CertReportId])  REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [FK_CertReportCertificationDocuments_Blobs]        FOREIGN KEY ([BlobKey])       REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'CertReportCertificationDocuments', N'Документи СО към доклад по сертификация.'
exec spDescColumn N'CertReportCertificationDocuments', N'CertReportCertificationDocumentId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CertReportCertificationDocuments', N'CertReportId'                      , N'Идентификатор на доклад по сертификация.'
exec spDescColumn N'CertReportCertificationDocuments', N'Name'                              , N'Наименование.'
exec spDescColumn N'CertReportCertificationDocuments', N'Description'                       , N'Описание.'
exec spDescColumn N'CertReportCertificationDocuments', N'BlobKey'                           , N'Идентификатор на файл.'

GO
