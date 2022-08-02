PRINT 'ContractReportCertCorrectionDocuments'
GO

CREATE TABLE [dbo].[ContractReportCertCorrectionDocuments] (
    [ContractReportCertCorrectionDocumentId]  INT               NOT NULL IDENTITY,
    [ContractReportCertCorrectionId]          INT               NOT NULL,
    [Description]                         NVARCHAR(MAX)     NOT NULL,
    [FileName]                            NVARCHAR(200)     NOT NULL,
    [FileKey]                             UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_ContractReportCertCorrectionDocuments]                            PRIMARY KEY ([ContractReportCertCorrectionDocumentId]),
    CONSTRAINT [FK_ContractReportCertCorrectionDocuments_ContractReportCertCorrections]  FOREIGN KEY ([ContractReportCertCorrectionId]) REFERENCES [dbo].[ContractReportCertCorrections] ([ContractReportCertCorrectionId]),
    CONSTRAINT [FK_ContractReportCertCorrectionDocuments_Blobs]                      FOREIGN KEY ([FileKey])                    REFERENCES [dbo].[Blobs]  ([Key]),
);
GO

exec spDescTable  N'ContractReportCertCorrectionDocuments', N'Документи към корекции на верифицирани суми.'
exec spDescColumn N'ContractReportCertCorrectionDocuments', N'ContractReportCertCorrectionDocumentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCertCorrectionDocuments', N'ContractReportCertCorrectionId'   , N'Идентификатор на изравнителен документ.'
exec spDescColumn N'ContractReportCertCorrectionDocuments', N'Description'              , N'Описание.'
exec spDescColumn N'ContractReportCertCorrectionDocuments', N'FileName'                 , N'Наименование.'
exec spDescColumn N'ContractReportCertCorrectionDocuments', N'FileKey'                  , N'Идентификатор на файл.'
GO