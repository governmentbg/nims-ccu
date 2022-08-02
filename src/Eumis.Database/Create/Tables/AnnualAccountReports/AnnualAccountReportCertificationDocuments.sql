PRINT 'AnnualAccountReportCertificationDocuments'
GO

CREATE TABLE [dbo].[AnnualAccountReportCertificationDocuments] (
    [AnnualAccountReportCertificationDocumentId]        INT                 NOT NULL IDENTITY,
    [AnnualAccountReportId]                             INT                 NOT NULL,
    [Name]                                              NVARCHAR(200)       NOT NULL,
    [Description]                                       NVARCHAR(MAX)       NULL,
    [BlobKey]                                           UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_AnnualAccountReportCertificationDocuments]                           PRIMARY KEY ([AnnualAccountReportCertificationDocumentId]),
    CONSTRAINT [FK_AnnualAccountReportCertificationDocuments_AnnualAccountReportId]     FOREIGN KEY ([AnnualAccountReportId])   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertificationDocuments_Blobs]                     FOREIGN KEY ([BlobKey])                 REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'AnnualAccountReportCertificationDocuments', N'Документи на сертифициращ орган към годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertificationDocuments', N'AnnualAccountReportCertificationDocumentId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AnnualAccountReportCertificationDocuments', N'AnnualAccountReportId'                        , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertificationDocuments', N'Name'                                         , N'Наименование.'
exec spDescColumn N'AnnualAccountReportCertificationDocuments', N'Description'                                  , N'Описание.'
exec spDescColumn N'AnnualAccountReportCertificationDocuments', N'BlobKey'                                      , N'Идентификатор на файл.'

GO
