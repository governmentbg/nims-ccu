PRINT 'EvalSessionDocuments'
GO

CREATE TABLE [dbo].[EvalSessionDocuments] (
    [EvalSessionId]                           INT                 NOT NULL,
    [EvalSessionDocumentId]                   INT                 NOT NULL IDENTITY,
    [Name]                                    NVARCHAR(200)       NOT NULL,
    [Description]                             NVARCHAR(MAX)       NULL,
    [BlobKey]                                 UNIQUEIDENTIFIER    NOT NULL,
    [IsDeleted]                               BIT                 NOT NULL,
    [IsDeletedNote]                           NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_EvalSessionDocuments]              PRIMARY KEY       ([EvalSessionDocumentId]),
    CONSTRAINT [FK_EvalSessionDocuments_EvalSessions] FOREIGN KEY       ([EvalSessionId])       REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionDocuments_Blobs]        FOREIGN KEY       ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'EvalSessionDocuments', N'Документи към елемент на оперативна карта.'
exec spDescColumn N'EvalSessionDocuments', N'EvalSessionDocumentId'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionDocuments', N'EvalSessionId'                     , N'Идентификатор на елемент на оперативна карта.'
exec spDescColumn N'EvalSessionDocuments', N'Name'                              , N'Наименование.'
exec spDescColumn N'EvalSessionDocuments', N'Description'                       , N'Описание.'
exec spDescColumn N'EvalSessionDocuments', N'BlobKey'                           , N'Идентификатор на файл.'
exec spDescColumn N'EvalSessionDocuments', N'IsDeleted'                         , N'Маркер, дали обобщената оценка е изтрита.'
exec spDescColumn N'EvalSessionDocuments', N'IsDeletedNote'                     , N'Причина за изтриване.'

GO


