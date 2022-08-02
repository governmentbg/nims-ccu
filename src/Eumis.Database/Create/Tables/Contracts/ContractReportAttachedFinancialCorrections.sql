PRINT 'ContractReportAttachedFinancialCorrections'
GO

CREATE TABLE [dbo].[ContractReportAttachedFinancialCorrections] (
    [ContractReportId]                            INT               NOT NULL,
    [ContractReportFinancialCorrectionId]         INT               NOT NULL UNIQUE,
    [ContractId]                                  INT               NOT NULL,

    CONSTRAINT [PK_ContractReportAttachedFinancialCorrections]          PRIMARY KEY ([ContractReportId], [ContractReportFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportAttachedFinancialCorrections_ContractReports]                     FOREIGN KEY ([ContractReportId])                     REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportAttachedFinancialCorrections_ContractReportFinancialCorrections]  FOREIGN KEY ([ContractReportFinancialCorrectionId])  REFERENCES [dbo].[ContractReportFinancialCorrections] ([ContractReportFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportAttachedFinancialCorrections_Contracts]                           FOREIGN KEY ([ContractId])                           REFERENCES [dbo].[Contracts] ([ContractId])
);
GO

exec spDescTable  N'ContractReportAttachedFinancialCorrections', N'Свързани документи за коригиране на верифицирани суми на ниво РОД.'
exec spDescColumn N'ContractReportAttachedFinancialCorrections', N'ContractReportId'                              , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportAttachedFinancialCorrections', N'ContractReportFinancialCorrectionId'           , N'Идентификатор на корекция на финансов отчет'
exec spDescColumn N'ContractReportAttachedFinancialCorrections', N'ContractId'                                    , N'Идентификатор на договор'
GO
