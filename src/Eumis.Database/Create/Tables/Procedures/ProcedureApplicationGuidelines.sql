PRINT 'ProcedureApplicationGuidelines'
GO

CREATE TABLE [dbo].[ProcedureApplicationGuidelines] (
    [ProcedureApplicationGuidelineId]           INT                 NOT NULL IDENTITY,
    [Gid]                                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureId]                               INT                 NOT NULL,
    [Name]                                      NVARCHAR(200)       NOT NULL,
    [Decription]                                NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL,

    CONSTRAINT [PK_ProcedureApplicationGuidelines]              PRIMARY KEY ([ProcedureApplicationGuidelineId]),
    CONSTRAINT [FK_ProcedureApplicationGuidelines_Procedures]   FOREIGN KEY ([ProcedureId])     REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureApplicationGuidelines_Blobs]        FOREIGN KEY ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ProcedureApplicationGuidelines', N'Документи, които могат да се изтеглят преди кандидатстване.'
exec spDescColumn N'ProcedureApplicationGuidelines', N'ProcedureApplicationGuidelineId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureApplicationGuidelines', N'Gid'                                 , N'Публичен системно генериран идентификатор.'
exec spDescColumn N'ProcedureApplicationGuidelines', N'ProcedureId'                         , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureApplicationGuidelines', N'Name'                                , N'Наименование.'
exec spDescColumn N'ProcedureApplicationGuidelines', N'Decription'                          , N'Описание.'
exec spDescColumn N'ProcedureApplicationGuidelines', N'BlobKey'                             , N'Идентификатор на файл.'

GO
