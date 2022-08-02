PRINT 'ProjectCommunicationMessageFiles'
GO

CREATE TABLE [dbo].[ProjectCommunicationMessageFiles] (
    [ProjectCommunicationMessageFileId]  INT                 NOT NULL IDENTITY,
    [ProjectCommunicationId]             INT                 NOT NULL,
    [ProjectCommunicationAnswerId]       INT                 NULL,
    [MessageType]                        INT                 NOT NULL,
    [Type]                               INT                 NOT NULL,
    [BlobKey]                            UNIQUEIDENTIFIER    NOT NULL,
    [Name]                               NVARCHAR(200)       NOT NULL,
    [Description]                        NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ProjectCommunicationMessageFiles]                               PRIMARY KEY ([ProjectCommunicationMessageFileId]),
    CONSTRAINT [FK_ProjectCommunicationMessageFiles_ProjectCommunications]         FOREIGN KEY ([ProjectCommunicationId])          REFERENCES [dbo].[ProjectCommunications] ([ProjectCommunicationId]),
    CONSTRAINT [FK_ProjectCommunicationMessageFiles_ProjectCommunicationAnswers]   FOREIGN KEY ([ProjectCommunicationAnswerId])    REFERENCES [dbo].[ProjectCommunicationAnswers] ([ProjectCommunicationAnswerId]),
    CONSTRAINT [FK_ProjectCommunicationMessageFiles_Blobs]                         FOREIGN KEY ([BlobKey])                         REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ProjectCommunicationMessageFiles_MessageType]                  CHECK ([MessageType] IN (1, 2)),
    CONSTRAINT [CHK_ProjectCommunicationMessageFiles_Type]                         CHECK ([Type] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'ProjectCommunicationMessageFiles', N'Файлове към xml за комуникация.'
exec spDescColumn N'ProjectCommunicationMessageFiles', N'ProjectCommunicationMessageFileId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectCommunicationMessageFiles', N'ProjectCommunicationId'           , N'Идентификатор на комуникация.'
exec spDescColumn N'ProjectCommunicationMessageFiles', N'ProjectCommunicationAnswerId'     , N'Идентификатор на отговор при комуникация с УО.'
exec spDescColumn N'ProjectCommunicationMessageFiles', N'MessageType'                      , N'Тип на съобщението: 1 - Въпрос; 2 - Отговор.'
exec spDescColumn N'ProjectCommunicationMessageFiles', N'Type'                             , N'Тип на файла: 1 - Прикачен файл към въпроса; 2 - Прикачен файл към отговора; 3 - Прикачен файл към проектното предложение в комуникацията; 4 - Прикачен signature на файл към проектното предложение в комуникацията.'
exec spDescColumn N'ProjectCommunicationMessageFiles', N'BlobKey'                          , N'Идентификатор на файл.'
exec spDescColumn N'ProjectCommunicationMessageFiles', N'Name'                             , N'Име на файл.'
exec spDescColumn N'ProjectCommunicationMessageFiles', N'Description'                      , N'Описание.'
GO
