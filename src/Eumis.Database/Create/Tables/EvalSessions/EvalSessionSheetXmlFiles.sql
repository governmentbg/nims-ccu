PRINT 'EvalSessionSheetXmlFiles'
GO

CREATE TABLE [dbo].[EvalSessionSheetXmlFiles] (
    [EvalSessionSheetXmlFileId]         INT                 NOT NULL IDENTITY,
    [EvalSessionSheetXmlId]             INT                 NOT NULL,
    [Type]                              INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_EvalSessionSheetXmlFiles]                          PRIMARY KEY ([EvalSessionSheetXmlFileId]),
    CONSTRAINT [FK_EvalSessionSheetXmlFiles_EvalSessionSheetXmls]     FOREIGN KEY ([EvalSessionSheetXmlId])    REFERENCES [dbo].[EvalSessionSheetXmls] ([EvalSessionSheetXmlId]),
    CONSTRAINT [FK_EvalSessionSheetXmlFiles_Blobs]                    FOREIGN KEY ([BlobKey])                  REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_EvalSessionSheetXmlFiles_Type]                    CHECK ([Type] IN (1, 2))
);
GO

exec spDescTable  N'EvalSessionSheetXmlFiles', N'Файлове към xml за оценителен лист.'
exec spDescColumn N'EvalSessionSheetXmlFiles', N'EvalSessionSheetXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionSheetXmlFiles', N'EvalSessionSheetXmlId'    , N'Идентификатор на xml за оценителен лист.'
exec spDescColumn N'EvalSessionSheetXmlFiles', N'Type'                     , N'Тип на файла: 1 - Прикачен файл; 2 - Прикачен файл към инструкции за оценка.'
exec spDescColumn N'EvalSessionSheetXmlFiles', N'BlobKey'                  , N'Идентификатор на файл.'
exec spDescColumn N'EvalSessionSheetXmlFiles', N'Name'                     , N'Име на файл.'
exec spDescColumn N'EvalSessionSheetXmlFiles', N'Description'              , N'Описание.'
GO
