PRINT 'AnnualAccountReportCertCorrections'
GO

CREATE TABLE [dbo].[AnnualAccountReportCertCorrections] (
    [AnnualAccountReportId]                         INT               NOT NULL,
    [ContractReportCertAuthorityCorrectionId]       INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertCorrections]                                          PRIMARY KEY ([AnnualAccountReportId], [ContractReportCertAuthorityCorrectionId]),
    CONSTRAINT [FK_AnnualAccountReportCertCorrections_AnnualAccountReports]                     FOREIGN KEY ([AnnualAccountReportId])                   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertCorrections_ContractReportCertAuthorityCorrectionId]  FOREIGN KEY ([ContractReportCertAuthorityCorrectionId]) REFERENCES [dbo].[ContractReportCertAuthorityCorrections] ([ContractReportCertAuthorityCorrectionId])
);
GO

exec spDescTable  N'AnnualAccountReportCertCorrections', N'Корекции СС на други нива към годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertCorrections', N'AnnualAccountReportId'                       , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertCorrections', N'ContractReportCertAuthorityCorrectionId'     , N'Идентификатор на корекция от СС на други нива.'

GO
