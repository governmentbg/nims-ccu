PRINT 'ProcedureApplicationDocs'
GO

CREATE TABLE [dbo].[ProcedureApplicationDocs] (
    [ProcedureApplicationDocId]         INT                 NOT NULL IDENTITY,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureId]                       INT                 NOT NULL,
    [ProgrammeApplicationDocumentId]    INT                 NULL,
    [Name]                              NVARCHAR(500)       NOT NULL,
    [Extension]                         NVARCHAR(100)        NULL,
    [IsRequired]                        BIT                 NOT NULL,
    [IsSignatureRequired]               BIT                 NOT NULL,
    [IsActivated]                       BIT                 NOT NULL,
    [IsActive]                          BIT                 NOT NULL,

    CONSTRAINT [PK_ProcedureApplicationDocs]                                   PRIMARY KEY ([ProcedureApplicationDocId]),
    CONSTRAINT [FK_ProcedureApplicationDocs_Procedures]                        FOREIGN KEY ([ProcedureId])                     REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureApplicationDocs_ProgrammeApplicationDocuments]     FOREIGN KEY ([ProgrammeApplicationDocumentId])  REFERENCES [dbo].[ProgrammeApplicationDocuments] ([ProgrammeApplicationDocumentId]),
);
GO

exec spDescTable  N'ProcedureApplicationDocs', N'Документи, който се подават при кандидатстване по процедура.'
exec spDescColumn N'ProcedureApplicationDocs', N'ProcedureApplicationDocId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureApplicationDocs', N'Gid'                           , N'Публичен системно генериран идентификатор.'
exec spDescColumn N'ProcedureApplicationDocs', N'ProcedureId'                   , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureApplicationDocs', N'Name'                          , N'Тип документ при електронно кандидастване.'
exec spDescColumn N'ProcedureApplicationDocs', N'Extension'                     , N'Разширение на документа(файла).'
exec spDescColumn N'ProcedureApplicationDocs', N'IsRequired'                    , N'Маркер, дали е задължителен.'
exec spDescColumn N'ProcedureApplicationDocs', N'IsSignatureRequired'           , N'Маркер, дали е задължителен подпис.'
GO
