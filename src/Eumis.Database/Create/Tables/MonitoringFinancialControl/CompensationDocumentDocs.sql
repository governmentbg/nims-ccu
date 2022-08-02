PRINT 'CompensationDocumentDocs'
GO

CREATE TABLE [dbo].[CompensationDocumentDocs] (
    [CompensationDocumentDocId]  INT               NOT NULL IDENTITY,
    [CompensationDocumentId]     INT               NOT NULL,
    [Description]                NVARCHAR(MAX)     NOT NULL,
    [FileName]                   NVARCHAR(200)     NOT NULL,
    [FileKey]                    UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_CompensationDocumentDocs]                        PRIMARY KEY ([CompensationDocumentDocId]),
    CONSTRAINT [FK_CompensationDocumentDocs_CompensationDocuments]  FOREIGN KEY ([CompensationDocumentId])   REFERENCES [dbo].[CompensationDocuments] ([CompensationDocumentId]),
    CONSTRAINT [FK_CompensationDocumentDocs_Blobs]                  FOREIGN KEY ([FileKey])                  REFERENCES [dbo].[Blobs]  ([Key]),
);
GO

exec spDescTable  N'CompensationDocumentDocs', N'Документи към изравнителни документи.'
exec spDescColumn N'CompensationDocumentDocs', N'CompensationDocumentDocId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CompensationDocumentDocs', N'CompensationDocumentId'   , N'Идентификатор на изравнителен документ.'
exec spDescColumn N'CompensationDocumentDocs', N'Description'              , N'Описание.'
exec spDescColumn N'CompensationDocumentDocs', N'FileName'                 , N'Наименование.'
exec spDescColumn N'CompensationDocumentDocs', N'FileKey'                  , N'Идентификатор на файл.'
GO