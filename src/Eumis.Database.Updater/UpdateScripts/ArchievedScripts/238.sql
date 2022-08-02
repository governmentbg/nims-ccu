GO

CREATE TABLE [dbo].[AnnualAccountReportCertFinancialCorrectionCSDs] (
    [AnnualAccountReportId]                                     INT               NOT NULL,
    [ContractReportCertAuthorityFinancialCorrectionCSDId]       INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertFinancialCorrectionCSDs]                                                      PRIMARY KEY ([AnnualAccountReportId], [ContractReportCertAuthorityFinancialCorrectionCSDId]),
    CONSTRAINT [FK_AnnualAccountReportCertFinancialCorrectionCSDs_AnnualAccountReports]                                 FOREIGN KEY ([AnnualAccountReportId])                               REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertFinancialCorrectionCSDs_ContractReportCertAuthorityFinancialCorrectionCSDs]   FOREIGN KEY ([ContractReportCertAuthorityFinancialCorrectionCSDId]) REFERENCES [dbo].[ContractReportCertAuthorityFinancialCorrectionCSDs] ([ContractReportCertAuthorityFinancialCorrectionCSDId])
);
GO

GO

CREATE TABLE [dbo].[AnnualAccountReportCertCorrections] (
    [AnnualAccountReportId]                             INT               NOT NULL,
    [ContractReportCertAuthorityCorrectionId]           INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertCorrections]                                              PRIMARY KEY ([AnnualAccountReportId], [ContractReportCertAuthorityCorrectionId]),
    CONSTRAINT [FK_AnnualAccountReportCertCorrections_AnnualAccountReports]                         FOREIGN KEY ([AnnualAccountReportId])                   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertCorrections_ContractReportCertAuthorityCorrectionId]      FOREIGN KEY ([ContractReportCertAuthorityCorrectionId]) REFERENCES [dbo].[ContractReportCertAuthorityCorrections] ([ContractReportCertAuthorityCorrectionId])
);
GO

CREATE TABLE [dbo].[AnnualAccountReportAppendices] (
    [AnnualAccountReportAppendixId]                     INT                 NOT NULL IDENTITY,
    [AnnualAccountReportId]                             INT                 NOT NULL,
    [ProgrammePriorityId]                               INT                 NOT NULL,
    [Type]                                              INT                 NOT NULL,
    [Comment]                                           NVARCHAR (MAX)      NULL,

    CONSTRAINT [PK_AnnualAccountReportAppendices]                               PRIMARY KEY ([AnnualAccountReportAppendixId]),
    CONSTRAINT [FK_AnnualAccountReportAppendices_AnnualAccountReports]          FOREIGN KEY ([AnnualAccountReportId])       REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportAppendices_MapNodes]                      FOREIGN KEY ([ProgrammePriorityId])         REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_AnnualAccountReportAppendices_Type]                         CHECK       ([Type] IN (1, 2)),
);
GO
