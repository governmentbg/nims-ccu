ALTER TABLE [dbo].[CertReports] ADD
    [OrderVersionNum]                       INT                    NOT NULL CONSTRAINT DEFAULT_OrderVersionNum DEFAULT 1,
    [StatusNote]                            NVARCHAR(MAX)          NULL,
    [CertReportOriginId]                    INT                    NULL
GO

ALTER TABLE [dbo].[CertReports]
DROP
  CONSTRAINT DEFAULT_OrderVersionNum
GO

ALTER TABLE [dbo].[CertReports] ADD
    CONSTRAINT [FK_CertReports_CertReportOrigins]           FOREIGN KEY ([CertReportOriginId])           REFERENCES [dbo].[CertReports] ([CertReportId])
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


CREATE TABLE [dbo].[CertReportSnapshotFiles] (
    [CertReportSnapshotFileId]          INT                 NOT NULL IDENTITY,
    [CertReportSnapshotId]              INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [PK_CertReportSnapshotFiles]                            PRIMARY KEY ([CertReportSnapshotFileId]),
    CONSTRAINT [FK_CertReportSnapshotFiles_CertReports]                FOREIGN KEY ([CertReportSnapshotId])  REFERENCES [dbo].[CertReportSnapshots] ([CertReportSnapshotId]),
    CONSTRAINT [FK_CertReportSnapshotFiles_Blobs]                      FOREIGN KEY ([BlobKey])               REFERENCES [dbo].[Blobs] ([Key])
);
GO

UPDATE [dbo].[CertReports]
SET [Status] = 3
WHERE [Status] = 7
GO