PRINT 'AnnualAccountReportAuditDocuments'
GO

CREATE TABLE [dbo].[AnnualAccountReportAuditDocuments] (
    [AnnualAccountReportAuditDocumentId]        INT                 NOT NULL IDENTITY,
    [AnnualAccountReportId]                     INT                 NOT NULL,
    [Name]                                      NVARCHAR(200)       NOT NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_AnnualAccountReportAuditDocuments]                           PRIMARY KEY ([AnnualAccountReportAuditDocumentId]),
    CONSTRAINT [FK_AnnualAccountReportAuditDocuments_AnnualAccountReportId]     FOREIGN KEY ([AnnualAccountReportId])   REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportAuditDocuments_Blobs]                     FOREIGN KEY ([BlobKey])                 REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'AnnualAccountReportAuditDocuments', N'Документи на одитиращ орган към годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportAuditDocuments', N'AnnualAccountReportAuditDocumentId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AnnualAccountReportAuditDocuments', N'AnnualAccountReportId'                , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportAuditDocuments', N'Name'                                 , N'Наименование.'
exec spDescColumn N'AnnualAccountReportAuditDocuments', N'Description'                          , N'Описание.'
exec spDescColumn N'AnnualAccountReportAuditDocuments', N'BlobKey'                              , N'Идентификатор на файл.'

GO
