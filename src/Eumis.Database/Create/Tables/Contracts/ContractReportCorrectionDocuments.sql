PRINT 'ContractReportCorrectionDocuments'
GO

CREATE TABLE [dbo].[ContractReportCorrectionDocuments] (
    [ContractReportCorrectionDocumentId]  INT               NOT NULL IDENTITY,
    [ContractReportCorrectionId]          INT               NOT NULL,
    [Description]                         NVARCHAR(MAX)     NOT NULL,
    [FileName]                            NVARCHAR(200)     NOT NULL,
    [FileKey]                             UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_ContractReportCorrectionDocuments]                            PRIMARY KEY ([ContractReportCorrectionDocumentId]),
    CONSTRAINT [FK_ContractReportCorrectionDocuments_ContractReportCorrections]  FOREIGN KEY ([ContractReportCorrectionId]) REFERENCES [dbo].[ContractReportCorrections] ([ContractReportCorrectionId]),
    CONSTRAINT [FK_ContractReportCorrectionDocuments_Blobs]                      FOREIGN KEY ([FileKey])                    REFERENCES [dbo].[Blobs]  ([Key]),
);
GO

exec spDescTable  N'ContractReportCorrectionDocuments', N'Документи към корекции на верифицирани суми.'
exec spDescColumn N'ContractReportCorrectionDocuments', N'ContractReportCorrectionDocumentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCorrectionDocuments', N'ContractReportCorrectionId'   , N'Идентификатор на изравнителен документ.'
exec spDescColumn N'ContractReportCorrectionDocuments', N'Description'              , N'Описание.'
exec spDescColumn N'ContractReportCorrectionDocuments', N'FileName'                 , N'Наименование.'
exec spDescColumn N'ContractReportCorrectionDocuments', N'FileKey'                  , N'Идентификатор на файл.'
GO