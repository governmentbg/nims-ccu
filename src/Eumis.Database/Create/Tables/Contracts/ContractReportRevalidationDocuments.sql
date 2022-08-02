PRINT 'ContractReportRevalidationDocuments'
GO

CREATE TABLE [dbo].[ContractReportRevalidationDocuments] (
    [ContractReportRevalidationDocumentId]  INT               NOT NULL IDENTITY,
    [ContractReportRevalidationId]          INT               NOT NULL,
    [Description]                         NVARCHAR(MAX)       NOT NULL,
    [FileName]                            NVARCHAR(200)       NOT NULL,
    [FileKey]                             UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [PK_ContractReportRevalidationDocuments]                              PRIMARY KEY ([ContractReportRevalidationDocumentId]),
    CONSTRAINT [FK_ContractReportRevalidationDocuments_ContractReportRevalidations]  FOREIGN KEY ([ContractReportRevalidationId]) REFERENCES [dbo].[ContractReportRevalidations] ([ContractReportRevalidationId]),
    CONSTRAINT [FK_ContractReportRevalidationDocuments_Blobs]                        FOREIGN KEY ([FileKey])                    REFERENCES [dbo].[Blobs]  ([Key]),
);
GO

exec spDescTable  N'ContractReportRevalidationDocuments', N'Документи към препотвърждаване на верифицирани суми на други нива.'
exec spDescColumn N'ContractReportRevalidationDocuments', N'ContractReportRevalidationDocumentId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportRevalidationDocuments', N'ContractReportRevalidationId'          , N'Идентификатор на изравнителен документ.'
exec spDescColumn N'ContractReportRevalidationDocuments', N'Description'                           , N'Описание.'
exec spDescColumn N'ContractReportRevalidationDocuments', N'FileName'                              , N'Наименование.'
exec spDescColumn N'ContractReportRevalidationDocuments', N'FileKey'                               , N'Идентификатор на файл.'
GO