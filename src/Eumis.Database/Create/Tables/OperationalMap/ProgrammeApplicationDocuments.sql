PRINT 'ProgrammeApplicationDocuments'
GO

CREATE TABLE [dbo].[ProgrammeApplicationDocuments] (

    [ProgrammeApplicationDocumentId]          INT                 NOT NULL IDENTITY,
    [ProgrammeId]                             INT                 NOT NULL,
    [Name]                                    NVARCHAR(500)       NOT NULL,
    [Extension]                               NVARCHAR(100)       NULL,
    [IsSignatureRequired]                     BIT                 NOT NULL,
    [IsActive]                                BIT                 NOT NULL DEFAULT(1),

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
exec spDescColumn N'ProgrammeApplicationDocuments', N'IsActive'                          , N'Маркер за активност.'

GO
