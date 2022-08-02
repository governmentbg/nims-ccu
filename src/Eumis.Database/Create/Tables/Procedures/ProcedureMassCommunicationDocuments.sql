PRINT 'ProcedureMassCommunicationDocuments'
GO

CREATE TABLE [dbo].[ProcedureMassCommunicationDocuments] (
    [ProcedureMassCommunicationDocumentId]      INT                 NOT NULL IDENTITY,
    [ProcedureMassCommunicationId]              INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [FileName]                                  NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ProcedureMassCommunicationDocuments]                              PRIMARY KEY ([ProcedureMassCommunicationDocumentId]),
    CONSTRAINT [FK_ProcedureMassCommunicationDocuments_ProcedureMassCommunications]  FOREIGN KEY ([ProcedureMassCommunicationId])  REFERENCES [dbo].[ProcedureMassCommunications] ([ProcedureMassCommunicationId]),
    CONSTRAINT [FK_ProcedureMassCommunicationDocuments_Blobs]                        FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ProcedureMassCommunicationDocuments', N'Документи към обща кореспонденция.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'ProcedureMassCommunicationDocumentId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'ProcedureMassCommunicationId'         , N'Идентификатор на обща комуникация.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'Name'                                 , N'Наименование.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'Description'                          , N'Описание.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'FileName'                             , N'Наименование на файл.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'BlobKey'                              , N'Идентификатор на файл.'
GO
