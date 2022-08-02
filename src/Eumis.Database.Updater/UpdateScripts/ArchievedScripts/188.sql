GO
ALTER TABLE [dbo].[CertReports] ADD [CertReportNumber] NVARCHAR(30) NOT NULL CONSTRAINT DEFAULT_CertReportNumber DEFAULT '';

ALTER TABLE [dbo].[CertReports] DROP CONSTRAINT DEFAULT_CertReportNumber;