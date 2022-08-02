PRINT 'ProgrammeApplicationDocuments'
GO

CREATE TABLE [dbo].[ProgrammeApplicationDocuments] (

    [ProgrammeApplicationDocumentId]          INT                 NOT NULL IDENTITY,
    [ProgrammeId]                             INT                 NOT NULL,
    [Name]                                    NVARCHAR(200)       NOT NULL,
    [Extension]                               NVARCHAR(50)        NULL,
    [IsSignatureRequired]                     BIT                 NOT NULL,

    CONSTRAINT [PK_ProgrammeApplicationDocuments]              PRIMARY KEY       ([ProgrammeApplicationDocumentId]),
    CONSTRAINT [FK_ProgrammeApplicationDocuments_MapNodes]     FOREIGN KEY       ([ProgrammeId])       REFERENCES [dbo].[MapNodes] ([MapNodeId])
);
GO

exec spDescTable  N'ProgrammeApplicationDocuments', N'Документи от кандидатите.'
exec spDescColumn N'ProgrammeApplicationDocuments', N'ProgrammeApplicationDocumentId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeApplicationDocuments', N'ProgrammeId'                       , N'Идентификатор на оперативна програма.'
exec spDescColumn N'ProgrammeApplicationDocuments', N'Name'                              , N'Наименование.'
exec spDescColumn N'ProgrammeApplicationDocuments', N'Extension'                         , N'Разширение на документа(файла).'
exec spDescColumn N'ProgrammeApplicationDocuments', N'IsSignatureRequired'               , N'Маркер, дали е задължителен подпис.'

GO

ALTER TABLE [dbo].[ProcedureApplicationDocs] ADD [ProgrammeApplicationDocumentId] INT NULL

ALTER TABLE [dbo].[ProcedureApplicationDocs] ADD CONSTRAINT [FK_ProcedureApplicationDocs_ProgrammeApplicationDocuments]
FOREIGN KEY ([ProgrammeApplicationDocumentId]) REFERENCES [dbo].[ProgrammeApplicationDocuments] ([ProgrammeApplicationDocumentId]);

GO
