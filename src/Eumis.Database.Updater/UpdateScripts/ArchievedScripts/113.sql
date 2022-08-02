GO

CREATE TABLE [dbo].[CertReportAttachedCertReports] (
    [CertReportId]                          INT                    NOT NULL,
    [AttachedCertReportId]                  INT                    NOT NULL,

    CONSTRAINT [PK_CertReportAttachedCertReports]                          PRIMARY KEY ([CertReportId], [AttachedCertReportId])
);
GO
