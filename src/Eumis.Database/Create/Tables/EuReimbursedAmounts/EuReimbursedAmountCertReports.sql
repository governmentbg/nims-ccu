PRINT 'EuReimbursedAmountCertReports'
GO

CREATE TABLE [dbo].[EuReimbursedAmountCertReports] (
    [EuReimbursedAmountCertReportId]   INT   NOT NULL IDENTITY,
    [EuReimbursedAmountId]             INT   NOT NULL,
    [CertReportId]                     INT   NOT NULL,

    CONSTRAINT [PK_EuReimbursedAmountCertReports]                     PRIMARY KEY ([EuReimbursedAmountCertReportId]),
    CONSTRAINT [FK_EuReimbursedAmountCertReports_EuReimbursedAmounts] FOREIGN KEY ([EuReimbursedAmountId])       REFERENCES [dbo].[EuReimbursedAmounts] ([EuReimbursedAmountId]),
    CONSTRAINT [FK_EuReimbursedAmountCertReports_CertReports]         FOREIGN KEY ([CertReportId])               REFERENCES [dbo].[CertReports]         ([CertReportId]),
);
GO

exec spDescTable  N'EuReimbursedAmountCertReports', N'Доклади по сертификация към възстановени от ЕК суми.'
exec spDescColumn N'EuReimbursedAmountCertReports', N'EuReimbursedAmountCertReportId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EuReimbursedAmountCertReports', N'EuReimbursedAmountId'          , N'Идентификатор на възстановена от ЕК сума.'
exec spDescColumn N'EuReimbursedAmountCertReports', N'CertReportId'                  , N'Идентификатор на доклад по сертификация.'
GO
