PRINT 'AnnualAccountReportCertRevalidationCorrections'
GO

CREATE TABLE [dbo].[AnnualAccountReportCertRevalidationCorrections] (
    [AnnualAccountReportId]                                 INT               NOT NULL,
    [ContractReportRevalidationCertAuthorityCorrectionId]   INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertRevalidationCorrections]                                                      PRIMARY KEY ([AnnualAccountReportId], [ContractReportRevalidationCertAuthorityCorrectionId]),
    CONSTRAINT [FK_AnnualAccountReportCertRevalidationCorrections_AnnualAccountReports]                                 FOREIGN KEY ([AnnualAccountReportId])                               REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertRevalidationCorrections_ContractReportRevalidationCertAuthorityCorrections]   FOREIGN KEY ([ContractReportRevalidationCertAuthorityCorrectionId]) REFERENCES [dbo].[ContractReportRevalidationCertAuthorityCorrections] ([ContractReportRevalidationCertAuthorityCorrectionId])
);
GO

exec spDescTable  N'AnnualAccountReportCertRevalidationCorrections', N'Корекции СС на препотвърдени суми на други нива към годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertRevalidationCorrections', N'AnnualAccountReportId'                               , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertRevalidationCorrections', N'ContractReportRevalidationCertAuthorityCorrectionId' , N'Идентификатор на корекция от СС на препотвърдени суми на други нива.'
GO
