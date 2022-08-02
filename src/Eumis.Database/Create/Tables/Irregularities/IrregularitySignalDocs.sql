PRINT 'IrregularitySignalDocs'
GO

CREATE TABLE [dbo].[IrregularitySignalDocs] (
    [IrregularitySignalDocId] INT               NOT NULL IDENTITY,
    [IrregularitySignalId]    INT               NOT NULL,
    [Description]             NVARCHAR(MAX)     NOT NULL,
    [FileName]                NVARCHAR(200)     NOT NULL,
    [FileKey]                 UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_IrregularitySignalDocs]                      PRIMARY KEY ([IrregularitySignalDocId]),
    CONSTRAINT [FK_IrregularitySignalDocs_IrregularitySignals]  FOREIGN KEY ([IrregularitySignalId])   REFERENCES [dbo].[IrregularitySignals] ([IrregularitySignalId]),
    CONSTRAINT [FK_IrregularitySignalDocs_Blobs]                FOREIGN KEY ([FileKey])                REFERENCES [dbo].[Blobs]  ([Key]),
);
GO

exec spDescTable  N'IrregularitySignalDocs', N'Документи към сигнали за нередности.'
exec spDescColumn N'IrregularitySignalDocs', N'IrregularitySignalDocId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularitySignalDocs', N'IrregularitySignalId'   , N'Идентификатор на сигнал за нередност.'
exec spDescColumn N'IrregularitySignalDocs', N'Description'            , N'Описание.'
exec spDescColumn N'IrregularitySignalDocs', N'FileName'               , N'Наименование.'
exec spDescColumn N'IrregularitySignalDocs', N'FileKey'                , N'Идентификатор на файл.'
GO