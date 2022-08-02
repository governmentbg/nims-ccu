PRINT 'CertReportSnapshots'
GO

CREATE TABLE [dbo].[CertReportSnapshots] (
    [CertReportSnapshotId]                  INT                 NOT NULL IDENTITY,
    [CertReportId]                          INT                 NOT NULL,
    [CertReportText]                        NVARCHAR(MAX)       NOT NULL,

    [ApprovedEuAmount]                      MONEY             NULL,
    [ApprovedBgAmount]                      MONEY             NULL,
    [ApprovedBfpTotalAmount]                MONEY             NULL,
    [ApprovedSelfAmount]                    MONEY             NULL,
    [ApprovedTotalAmount]                   MONEY             NULL,
    [CertifiedEuAmount]                     MONEY             NULL,
    [CertifiedBgAmount]                     MONEY             NULL,
    [CertifiedBfpTotalAmount]               MONEY             NULL,
    [CertifiedSelfAmount]                   MONEY             NULL,
    [CertifiedTotalAmount]                  MONEY             NULL,

    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_CertReportSnapshots]                            PRIMARY KEY ([CertReportSnapshotId]),
    CONSTRAINT [FK_CertReportSnapshots_CertReports]                FOREIGN KEY ([CertReportId])    REFERENCES [dbo].[CertReports] ([CertReportId])
);
GO

exec spDescTable  N'CertReportSnapshots', N'Версия на доклад по сертификация.'
exec spDescColumn N'CertReportSnapshots', N'CertReportSnapshotId'    , N'Идентификатор на версия на доклад по сертификация.'
exec spDescColumn N'CertReportSnapshots', N'CertReportId'            , N'Идентификатор на процедура.'
exec spDescColumn N'CertReportSnapshots', N'CertReportText'          , N'Json на процедурата.'
exec spDescColumn N'CertReportSnapshots', N'CreateDate'              , N'Дата на създаване на записа.'
exec spDescColumn N'CertReportSnapshots', N'ModifyDate'              , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CertReportSnapshots', N'Version'                 , N'Версия.'
GO
