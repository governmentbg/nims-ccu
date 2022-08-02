PRINT 'ContractReportCertAuthorityCorrectionDocuments'
GO

CREATE TABLE [dbo].[ContractReportCertAuthorityCorrectionDocuments] (
    [ContractReportCertAuthorityCorrectionDocumentId]   INT                 NOT NULL IDENTITY,
    [ContractReportCertAuthorityCorrectionId]           INT                 NOT NULL,
    [Description]                                       NVARCHAR(MAX)       NOT NULL,
    [FileName]                                          NVARCHAR(200)       NOT NULL,
    [FileKey]                                           UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [PK_ContractReportCertAuthorityCorrectionDocuments]                                          PRIMARY KEY ([ContractReportCertAuthorityCorrectionDocumentId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrectionDocuments_ContractReportCertAuthorityCorrections]   FOREIGN KEY ([ContractReportCertAuthorityCorrectionId]) REFERENCES [dbo].[ContractReportCertAuthorityCorrections] ([ContractReportCertAuthorityCorrectionId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrectionDocuments_Blobs]                                    FOREIGN KEY ([FileKey])                    REFERENCES [dbo].[Blobs]  ([Key]),
);
GO

exec spDescTable  N'ContractReportCertAuthorityCorrectionDocuments' , N'Документи към корекции от СО на верифицирани суми.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'ContractReportCertAuthorityCorrectionDocumentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'ContractReportCertAuthorityCorrectionId'        , N'Идентификатор на изравнителен документ.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'Description'                                    , N'Описание.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'FileName'                                       , N'Наименование.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'FileKey'                                        , N'Идентификатор на файл.'
GO
