PRINT 'ProcedureEvalTableXmlFiles'
GO

CREATE TABLE [dbo].[ProcedureEvalTableXmlFiles] (
    [ProcedureEvalTableXmlFileId]       INT                 NOT NULL IDENTITY,
    [ProcedureEvalTableXmlId]           INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ProcedureEvalTableXmlFiles]                          PRIMARY KEY ([ProcedureEvalTableXmlFileId]),
    CONSTRAINT [FK_ProcedureEvalTableXmlFiles_ProcedureEvalTableXmls]   FOREIGN KEY ([ProcedureEvalTableXmlId])    REFERENCES [dbo].[ProcedureEvalTableXmls] ([ProcedureEvalTableXmlId]),
    CONSTRAINT [FK_ProcedureEvalTableXmlFiles_Blobs]                    FOREIGN KEY ([BlobKey])                    REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'ProcedureEvalTableXmlFiles', N'Файлове към xml за оценителна таблица.'
exec spDescColumn N'ProcedureEvalTableXmlFiles', N'ProcedureEvalTableXmlFileId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureEvalTableXmlFiles', N'ProcedureEvalTableXmlId'     , N'Идентификатор на xml за оценителна таблица.'
exec spDescColumn N'ProcedureEvalTableXmlFiles', N'BlobKey'                     , N'Идентификатор на файл.'
exec spDescColumn N'ProcedureEvalTableXmlFiles', N'Name'                        , N'Име на файл.'
exec spDescColumn N'ProcedureEvalTableXmlFiles', N'Description'                 , N'Описание.'
GO
