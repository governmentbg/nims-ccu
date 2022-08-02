PRINT 'IrregularityDocs'
GO

CREATE TABLE [dbo].[IrregularityDocs] (
    [IrregularityDocId]    INT               NOT NULL IDENTITY,
    [IrregularityId]       INT               NOT NULL,
    [Description]          NVARCHAR(MAX)     NOT NULL,
    [FileName]             NVARCHAR(200)     NOT NULL,
    [FileKey]              UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_IrregularityDocs]                 PRIMARY KEY ([IrregularityDocId]),
    CONSTRAINT [FK_IrregularityDocs_Irregularities]  FOREIGN KEY ([IrregularityId])   REFERENCES [dbo].[Irregularities] ([IrregularityId]),
    CONSTRAINT [FK_IrregularityDocs_Blobs]           FOREIGN KEY ([FileKey])          REFERENCES [dbo].[Blobs]          ([Key]),
);
GO

exec spDescTable  N'IrregularityDocs', N'Документи към нередност.'
exec spDescColumn N'IrregularityDocs', N'IrregularityDocId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularityDocs', N'IrregularityId'   , N'Идентификатор на нередност.'
exec spDescColumn N'IrregularityDocs', N'Description'      , N'Описание.'
exec spDescColumn N'IrregularityDocs', N'FileName'         , N'Наименование.'
exec spDescColumn N'IrregularityDocs', N'FileKey'          , N'Идентификатор на файл.'
GO