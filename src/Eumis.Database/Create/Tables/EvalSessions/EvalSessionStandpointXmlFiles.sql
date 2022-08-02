PRINT 'EvalSessionStandpointXmlFiles'
GO

CREATE TABLE [dbo].[EvalSessionStandpointXmlFiles] (
    [EvalSessionStandpointXmlFileId]    INT                 NOT NULL IDENTITY,
    [EvalSessionStandpointXmlId]        INT                 NOT NULL,
    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL,
    [Name]                              NVARCHAR(200)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_EvalSessionStandpointXmlFiles]                            PRIMARY KEY ([EvalSessionStandpointXmlFileId]),
    CONSTRAINT [FK_EvalSessionStandpointXmlFiles_EvalSessionStandpointXmls]  FOREIGN KEY ([EvalSessionStandpointXmlId])    REFERENCES [dbo].[EvalSessionStandpointXmls] ([EvalSessionStandpointXmlId]),
    CONSTRAINT [FK_EvalSessionStandpointXmlFiles_Blobs]                      FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key])
);
GO

exec spDescTable  N'EvalSessionStandpointXmlFiles', N'Файлове към xml за становище.'
exec spDescColumn N'EvalSessionStandpointXmlFiles', N'EvalSessionStandpointXmlFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionStandpointXmlFiles', N'EvalSessionStandpointXmlId'    , N'Идентификатор на xml за становище.'
exec spDescColumn N'EvalSessionStandpointXmlFiles', N'BlobKey'                       , N'Идентификатор на файл.'
exec spDescColumn N'EvalSessionStandpointXmlFiles', N'Name'                          , N'Име на файл.'
exec spDescColumn N'EvalSessionStandpointXmlFiles', N'Description'                   , N'Описание.'
GO
