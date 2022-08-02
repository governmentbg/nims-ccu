PRINT 'AnnualAccountReportCertRevalidationFinancialCorrectionCSDs'
GO

CREATE TABLE [dbo].[AnnualAccountReportCertRevalidationFinancialCorrectionCSDs] (
    [AnnualAccountReportId]                                             INT               NOT NULL,
    [ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId]   INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertRevalidationFinancialCorrectionCSDs]                                                                  PRIMARY KEY ([AnnualAccountReportId], [ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId]),
    CONSTRAINT [FK_AnnualAccountReportCertRevalidationFinancialCorrectionCSDs_AnnualAccountReports]                                             FOREIGN KEY ([AnnualAccountReportId])                                           REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertRevalidationFinancialCorrectionCSDs_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs]   FOREIGN KEY ([ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId]) REFERENCES [dbo].[ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs] ([ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId])
);
GO

exec spDescTable  N'AnnualAccountReportCertRevalidationFinancialCorrectionCSDs', N'Корекции СС на препотвърдени суми на ниво РОД към годишни счетоводни отчети.'
exec spDescColumn N'AnnualAccountReportCertRevalidationFinancialCorrectionCSDs', N'AnnualAccountReportId'                                           , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertRevalidationFinancialCorrectionCSDs', N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId' , N'Идентификатор на корекция на сертифицирани препотвърдени суми на ниво РОД.'
GO
