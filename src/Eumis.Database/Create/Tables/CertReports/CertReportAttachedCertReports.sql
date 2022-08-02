PRINT 'CertReportAttachedCertReports'
GO

CREATE TABLE [dbo].[CertReportAttachedCertReports] (
    [CertReportId]                          INT                    NOT NULL,
    [AttachedCertReportId]                  INT                    NOT NULL,

    CONSTRAINT [PK_CertReportAttachedCertReports]                          PRIMARY KEY ([CertReportId], [AttachedCertReportId])
);
GO

exec spDescTable  N'CertReportAttachedCertReports', N'Свързани доклади по сертификация.'
exec spDescColumn N'CertReportAttachedCertReports', N'CertReportId'                        , N'Идентификатор на доклад по сертификация.'
exec spDescColumn N'CertReportAttachedCertReports', N'AttachedCertReportId'                , N'Идентификатор на свързан доклад по сертификация.'

GO