GO

ALTER TABLE [dbo].[ContractDebts] ADD
    [CertReportId]       INT                NULL,
    CONSTRAINT [FK_ContractDebts_CertReports]          FOREIGN KEY ([CertReportId])            REFERENCES [dbo].[CertReports] ([CertReportId])

GO
