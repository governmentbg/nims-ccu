PRINT 'AnnualAccountReportCertFinancialCorrectionCSDs'
GO

CREATE TABLE [dbo].[AnnualAccountReportCertFinancialCorrectionCSDs] (
    [AnnualAccountReportId]                                         INT               NOT NULL,
    [ContractReportCertAuthorityFinancialCorrectionCSDId]           INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertFinancialCorrectionCSDs]                                                      PRIMARY KEY ([AnnualAccountReportId], [ContractReportCertAuthorityFinancialCorrectionCSDId]),
    CONSTRAINT [FK_AnnualAccountReportCertFinancialCorrectionCSDs_AnnualAccountReports]                                 FOREIGN KEY ([AnnualAccountReportId])                               REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertFinancialCorrectionCSDs_ContractReportCertAuthorityFinancialCorrectionCSDs]   FOREIGN KEY ([ContractReportCertAuthorityFinancialCorrectionCSDId]) REFERENCES [dbo].[ContractReportCertAuthorityFinancialCorrectionCSDs] ([ContractReportCertAuthorityFinancialCorrectionCSDId])
);
GO

exec spDescTable  N'AnnualAccountReportCertFinancialCorrectionCSDs', N'Корекции СС на ниво РОД към годишни счетоводни отчети.'
exec spDescColumn N'AnnualAccountReportCertFinancialCorrectionCSDs', N'AnnualAccountReportId'                               , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertFinancialCorrectionCSDs', N'ContractReportCertAuthorityFinancialCorrectionCSDId' , N'Идентификатор на корекция на сертифицирани суми на ниво РОД.'

GO
