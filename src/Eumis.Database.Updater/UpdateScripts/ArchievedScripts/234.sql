GO

CREATE TABLE [dbo].[AnnualAccountReports] (
    [AnnualAccountReportId]             INT                 NOT NULL IDENTITY,
    [ProgrammeId]                       INT                 NOT NULL,
    [OrderNum]                          INT                 NOT NULL,
    [OrderVersionNum]                   INT                 NOT NULL,
    [RegDate]                           DATETIME2           NOT NULL,
    [Status]                            INT                 NOT NULL,
    [StatusNote]                        NVARCHAR(MAX)       NULL,
    [ApprovalDate]                      DATETIME2           NULL,
    [AccountPeriod]                     INT                 NOT NULL,
    [CreateDate]                        DATETIME2           NOT NULL,
    [ModifyDate]                        DATETIME2           NOT NULL,
    [Version]                           ROWVERSION          NOT NULL,

    CONSTRAINT [PK_AnnualAccountReports]                    PRIMARY KEY ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReports_Programmes]         FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_AnnualAccountReports_Status]            CHECK       ([Status]                   IN (1, 2, 3)),
    CONSTRAINT [CHK_AnnualAccountReports_AccountPeriod]     CHECK       ([AccountPeriod]            IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

CREATE TABLE [dbo].[AnnualAccountReportAuditDocuments] (
    [AnnualAccountReportAuditDocumentId]        INT                 NOT NULL IDENTITY,
    [AnnualAccountReportId]                     INT                 NOT NULL,
    [Name]                                      NVARCHAR(200)       NOT NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_AnnualAccountReportAuditDocuments]                           PRIMARY KEY ([AnnualAccountReportAuditDocumentId]),
    CONSTRAINT [FK_AnnualAccountReportAuditDocuments_AnnualAccountReportId]     FOREIGN KEY ([AnnualAccountReportId])       REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportAuditDocuments_Blobs]                     FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
);
GO

CREATE TABLE [dbo].[AnnualAccountReportCertificationDocuments] (
    [AnnualAccountReportCertificationDocumentId]        INT                 NOT NULL IDENTITY,
    [AnnualAccountReportId]                             INT                 NOT NULL,
    [Name]                                              NVARCHAR(200)       NOT NULL,
    [Description]                                       NVARCHAR(MAX)       NULL,
    [BlobKey]                                           UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_AnnualAccountReportCertificationDocuments]                           PRIMARY KEY ([AnnualAccountReportCertificationDocumentId]),
    CONSTRAINT [FK_AnnualAccountReportCertificationDocuments_AnnualAccountReportId]     FOREIGN KEY ([AnnualAccountReportId])       REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertificationDocuments_Blobs]                     FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
);
GO

CREATE TABLE [dbo].[AnnualAccountReportCertReports] (
    [AnnualAccountReportId]                             INT               NOT NULL,
    [CertReportId]                                      INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertReports]                              PRIMARY KEY ([AnnualAccountReportId], [CertReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertReports_AnnualAccountReports]         FOREIGN KEY ([AnnualAccountReportId])                   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertReports_CertReports]                  FOREIGN KEY ([CertReportId])                            REFERENCES [dbo].[CertReports] ([CertReportId])
);
GO

CREATE TABLE [dbo].[AnnualAccountReportContractReportCorrections] (
    [AnnualAccountReportId]                                         INT               NOT NULL,
    [ContractReportCorrectionId]                                    INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportContractReportCorrections]                            PRIMARY KEY ([AnnualAccountReportId], [ContractReportCorrectionId]),
    CONSTRAINT [FK_AnnualAccountReportContractReportCorrections_AnnualAccountReports]       FOREIGN KEY ([AnnualAccountReportId])					REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportContractReportCorrections_ContractReportCorrections]  FOREIGN KEY ([ContractReportCorrectionId])		        REFERENCES [dbo].[ContractReportCorrections] ([ContractReportCorrectionId])
);
GO

CREATE TABLE [dbo].[AnnualAccountReportFinancialCorrectionCSDs] (
    [AnnualAccountReportId]                         INT                 NOT NULL,
    [ContractReportFinancialCorrectionCSDId]        INT                 NOT NULL

    CONSTRAINT [PK_AnnualAccountReportFinancialCorrectionCSDs]                                          PRIMARY KEY ([AnnualAccountReportId], [ContractReportFinancialCorrectionCSDId]),
    CONSTRAINT [FK_AnnualAccountReportFinancialCorrectionCSDs_AnnualAccountReports]                     FOREIGN KEY ([AnnualAccountReportId])                   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportFinancialCorrectionCSDs_ContractReportFinancialCorrectionCSDs]    FOREIGN KEY ([ContractReportFinancialCorrectionCSDId])  REFERENCES [dbo].[ContractReportFinancialCorrectionCSDs] ([ContractReportFinancialCorrectionCSDId]),
);
GO
