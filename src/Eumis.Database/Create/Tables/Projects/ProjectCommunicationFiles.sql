PRINT 'ProjectCommunicationFiles'
GO

CREATE TABLE [dbo].[ProjectCommunicationFiles] (
    [ProjectCommunicationFileId]     INT             NOT NULL IDENTITY,
    [ProjectCommunicationId]         INT             NOT NULL,
    [ProjectCommunicationAnswerId]   INT             NULL,
    [File]                           VARBINARY(MAX)  NOT NULL,
    [FileName]                       NVARCHAR(200)   NOT NULL,
    [CreateDate]                     DATETIME2       NOT NULL,
    [ModifyDate]                     DATETIME2       NOT NULL,
    [Version]                        ROWVERSION      NOT NULL

    CONSTRAINT [PK_ProjectCommunicationFiles]                             PRIMARY KEY ([ProjectCommunicationFileId]),
    CONSTRAINT [FK_ProjectCommunicationFiles_ProjectCommunications]       FOREIGN KEY ([ProjectCommunicationId])         REFERENCES [dbo].[ProjectCommunications] ([ProjectCommunicationId]),
    CONSTRAINT [FK_ProjectCommunicationFiles_ProjectCommunicationAnswers] FOREIGN KEY ([ProjectCommunicationAnswerId])   REFERENCES [dbo].[ProjectCommunicationAnswers] ([ProjectCommunicationAnswerId])
);
GO

exec spDescTable  N'ProjectCommunicationFiles', N'Файл на проектно предложение.'
exec spDescColumn N'ProjectCommunicationFiles', N'ProjectCommunicationFileId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectCommunicationFiles', N'ProjectCommunicationId'       , N'Идентификатор на комуникация към проектно предложние.'
exec spDescColumn N'ProjectCommunicationFiles', N'ProjectCommunicationAnswerId' , N'Идентификатор на отговор при комуникация с УО.'
exec spDescColumn N'ProjectCommunicationFiles', N'File'                         , N'Файл.'
exec spDescColumn N'ProjectCommunicationFiles', N'FileName'                     , N'Име на файл.'
exec spDescColumn N'ProjectCommunicationFiles', N'CreateDate'                   , N'Дата на създаване на записа.'
exec spDescColumn N'ProjectCommunicationFiles', N'ModifyDate'                   , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProjectCommunicationFiles', N'Version'                      , N'Версия.'

GO
