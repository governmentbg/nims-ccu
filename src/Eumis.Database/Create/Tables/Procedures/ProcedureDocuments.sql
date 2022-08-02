PRINT 'ProcedureDocuments'
GO

CREATE TABLE [dbo].[ProcedureDocuments] (
    [ProcedureDocumentId]                       INT                 NOT NULL IDENTITY,
    [ProcedureId]                               INT                 NOT NULL,
    [Name]                                      NVARCHAR(200)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ProcedureDocuments]                          PRIMARY KEY ([ProcedureDocumentId]),
    CONSTRAINT [FK_ProcedureDocuments_Procedures]               FOREIGN KEY ([ProcedureId])     REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureDocuments_Blobs]                    FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ProcedureDocuments', N'Документи към процедура.'
exec spDescColumn N'ProcedureDocuments', N'ProcedureDocumentId'                 , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureDocuments', N'ProcedureId'                         , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureDocuments', N'Name'                                , N'Наименование.'
exec spDescColumn N'ProcedureDocuments', N'Description'                         , N'Описание.'
exec spDescColumn N'ProcedureDocuments', N'BlobKey'                             , N'Идентификатор на файл.'

GO
