PRINT 'AuditDocs'
GO

CREATE TABLE [dbo].[AuditDocs] (
    [AuditDocId]    INT               NOT NULL IDENTITY,
    [AuditId]       INT               NOT NULL,
    [Description]   NVARCHAR(MAX)     NOT NULL,
    [FileName]      NVARCHAR(200)     NOT NULL,
    [FileKey]       UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_AuditDocs]         PRIMARY KEY ([AuditDocId]),
    CONSTRAINT [FK_AuditDocs_Audits]  FOREIGN KEY ([AuditId])   REFERENCES [dbo].[Audits] ([AuditId]),
    CONSTRAINT [FK_AuditDocs_Blobs]   FOREIGN KEY ([FileKey])   REFERENCES [dbo].[Blobs]  ([Key]),
);
GO

exec spDescTable  N'AuditDocs', N'Документи към одити.'
exec spDescColumn N'AuditDocs', N'AuditDocId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AuditDocs', N'AuditId'           , N'Идентификатор на одит.'
exec spDescColumn N'AuditDocs', N'Description'       , N'Описание.'
exec spDescColumn N'AuditDocs', N'FileName'          , N'Наименование.'
exec spDescColumn N'AuditDocs', N'FileKey'           , N'Идентификатор на файл.'
GO