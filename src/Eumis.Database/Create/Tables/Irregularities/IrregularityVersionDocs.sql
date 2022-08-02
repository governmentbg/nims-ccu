PRINT 'IrregularityVersionDocs'
GO

CREATE TABLE [dbo].[IrregularityVersionDocs] (
    [IrregularityVersionDocId]    INT               NOT NULL IDENTITY,
    [IrregularityVersionId]       INT               NOT NULL,
    [Description]                 NVARCHAR(MAX)     NOT NULL,
    [FileName]                    NVARCHAR(200)     NOT NULL,
    [FileKey]                     UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_IrregularityVersionDocs]                       PRIMARY KEY ([IrregularityVersionDocId]),
    CONSTRAINT [FK_IrregularityVersionDocs_IrregularityVersions]  FOREIGN KEY ([IrregularityVersionId])   REFERENCES [dbo].[IrregularityVersions] ([IrregularityVersionId]),
    CONSTRAINT [FK_IrregularityVersionDocs_Blobs]                 FOREIGN KEY ([FileKey])                 REFERENCES [dbo].[Blobs]                ([Key]),
);
GO

exec spDescTable  N'IrregularityVersionDocs', N'Документи към версия на нередност.'
exec spDescColumn N'IrregularityVersionDocs', N'IrregularityVersionDocId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularityVersionDocs', N'IrregularityVersionId'   , N'Идентификатор на версия на нередност.'
exec spDescColumn N'IrregularityVersionDocs', N'Description'             , N'Описание.'
exec spDescColumn N'IrregularityVersionDocs', N'FileName'                , N'Наименование.'
exec spDescColumn N'IrregularityVersionDocs', N'FileKey'                 , N'Идентификатор на файл.'
GO