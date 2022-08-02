PRINT 'CertReportSnapshotFiles'
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

exec spDescTable  N'CertReportSnapshotFiles', N'Файлове към версия на доклад по сертификация.'
exec spDescColumn N'CertReportSnapshotFiles', N'CertReportSnapshotFileId'    , N'Идентификатор на файл към версия на доклад по сертификация.'
exec spDescColumn N'CertReportSnapshotFiles', N'CertReportSnapshotId'        , N'Идентификатор на версия на доклад по сертификация.'
exec spDescColumn N'CertReportSnapshotFiles', N'BlobKey'                     , N'Идентификатор на файл.'
GO
