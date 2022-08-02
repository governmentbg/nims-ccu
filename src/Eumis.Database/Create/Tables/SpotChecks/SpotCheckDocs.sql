PRINT 'SpotCheckDocs'
GO

CREATE TABLE [dbo].[SpotCheckDocs] (
    [SpotCheckDocId]  INT               NOT NULL IDENTITY,
    [SpotCheckId]     INT               NOT NULL,
    [Description]     NVARCHAR(MAX)     NOT NULL,
    [FileName]        NVARCHAR(200)     NOT NULL,
    [FileKey]         UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_SpotCheckDocs]             PRIMARY KEY ([SpotCheckDocId]),
    CONSTRAINT [FK_SpotCheckDocs_SpotChecks]  FOREIGN KEY ([SpotCheckId]) REFERENCES [dbo].[SpotChecks] ([SpotCheckId]),
    CONSTRAINT [FK_SpotCheckDocs_Blobs]       FOREIGN KEY ([FileKey])     REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'SpotCheckDocs', N'Документи към проверки на място.'
exec spDescColumn N'SpotCheckDocs', N'SpotCheckDocId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckDocs', N'SpotCheckId'   , N'Идентификатор на проверка на място.'
exec spDescColumn N'SpotCheckDocs', N'Description'   , N'Описание.'
exec spDescColumn N'SpotCheckDocs', N'FileName'      , N'Наименование.'
exec spDescColumn N'SpotCheckDocs', N'FileKey'       , N'Идентификатор на файл.'
GO
