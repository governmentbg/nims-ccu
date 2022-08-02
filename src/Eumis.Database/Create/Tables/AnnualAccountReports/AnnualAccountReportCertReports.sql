PRINT 'AnnualAccountReportCertReports'
GO

CREATE TABLE [dbo].[AnnualAccountReportCertReports] (
    [AnnualAccountReportId]                             INT               NOT NULL,
    [CertReportId]                                      INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertReports]                          PRIMARY KEY ([AnnualAccountReportId], [CertReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertReports_AnnualAccountReports]     FOREIGN KEY ([AnnualAccountReportId])   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertReports_CertReports]              FOREIGN KEY ([CertReportId])            REFERENCES [dbo].[CertReports] ([CertReportId])
);
GO

exec spDescTable  N'AnnualAccountReportCertReports', N'Доклади за сертификация към годишни счетоводни отчети.'
exec spDescColumn N'AnnualAccountReportCertReports', N'AnnualAccountReportId'   , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertReports', N'CertReportId'            , N'Идентификатор на доклад за сертификация.'

GO
