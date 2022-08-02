PRINT 'AnnualAccountReportFinancialCorrectionCSDs'
GO

CREATE TABLE [dbo].[AnnualAccountReportFinancialCorrectionCSDs] (
    [AnnualAccountReportId]                         INT               NOT NULL,
    [ContractReportFinancialCorrectionCSDId]        INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportFinancialCorrectionCSDs]                                          PRIMARY KEY ([AnnualAccountReportId], [ContractReportFinancialCorrectionCSDId]),
    CONSTRAINT [FK_AnnualAccountReportFinancialCorrectionCSDs_AnnualAccountReports]                     FOREIGN KEY ([AnnualAccountReportId])                   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportFinancialCorrectionCSDs_ContractReportFinancialCorrectionCSDs]    FOREIGN KEY ([ContractReportFinancialCorrectionCSDId])  REFERENCES [dbo].[ContractReportFinancialCorrectionCSDs] ([ContractReportFinancialCorrectionCSDId]),
);
GO

exec spDescTable  N'AnnualAccountReportFinancialCorrectionCSDs', N'Корекции ВС на ниво РОД към годишни счетоводни отчети.'
exec spDescColumn N'AnnualAccountReportFinancialCorrectionCSDs', N'AnnualAccountReportId'                   , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportFinancialCorrectionCSDs', N'ContractReportFinancialCorrectionCSDId'  , N'Идентификатор на корекция на верифицирани суми на ниво РОД.'

GO
