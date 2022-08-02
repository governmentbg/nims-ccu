PRINT 'ContractReportRevalidationCertAuthorityCorrectionDocuments'
GO

CREATE TABLE [dbo].[ContractReportRevalidationCertAuthorityCorrectionDocuments] (
    [ContractReportRevalidationCertAuthorityCorrectionDocumentId]   INT                 NOT NULL IDENTITY,
    [ContractReportRevalidationCertAuthorityCorrectionId]           INT                 NOT NULL,
    [Description]                                                   NVARCHAR(MAX)       NOT NULL,
    [FileName]                                                      NVARCHAR(200)       NOT NULL,
    [FileKey]                                                       UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [PK_ContractReportRevalidationCertAuthorityCorrectionDocuments]                                                      PRIMARY KEY ([ContractReportRevalidationCertAuthorityCorrectionDocumentId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrectionDocuments_ContractReportRevalidationCertAuthorityCorrections]   FOREIGN KEY ([ContractReportRevalidationCertAuthorityCorrectionId]) REFERENCES [dbo].[ContractReportRevalidationCertAuthorityCorrections] ([ContractReportRevalidationCertAuthorityCorrectionId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrectionDocuments_Blobs]                                                FOREIGN KEY ([FileKey])                                             REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'Документи към корекции от СО на препотвърдени суми.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'ContractReportRevalidationCertAuthorityCorrectionDocumentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'ContractReportRevalidationCertAuthorityCorrectionId'        , N'Идентификатор на изравнителен документ.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'Description'                                                , N'Описание.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'FileName'                                                   , N'Наименование.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'FileKey'                                                    , N'Идентификатор на файл.'
GO
