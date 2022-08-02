PRINT 'AnnualAccountReportContractReportCorrections'
GO

CREATE TABLE [dbo].[AnnualAccountReportContractReportCorrections] (
    [AnnualAccountReportId]                         INT               NOT NULL,
    [ContractReportCorrectionId]                    INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportContractReportCorrections]                            PRIMARY KEY ([AnnualAccountReportId], [ContractReportCorrectionId]),
    CONSTRAINT [FK_AnnualAccountReportContractReportCorrections_AnnualAccountReports]       FOREIGN KEY ([AnnualAccountReportId])               REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportContractReportCorrections_ContractReportCorrections]  FOREIGN KEY ([ContractReportCorrectionId])          REFERENCES [dbo].[ContractReportCorrections] ([ContractReportCorrectionId])
);
GO

exec spDescTable  N'AnnualAccountReportContractReportCorrections', N'Корекции ВС на ниво РОД към годишни счетоводни отчети.'
exec spDescColumn N'AnnualAccountReportContractReportCorrections', N'AnnualAccountReportId'         , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportContractReportCorrections', N'ContractReportCorrectionId'    , N'Идентификатор на корекция.'

GO
