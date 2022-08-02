PRINT 'ProjectCommunicationFileSignatures'
GO

CREATE TABLE [dbo].[ProjectCommunicationFileSignatures] (
    [ProjectCommunicationFileSignatureId]    INT             NOT NULL IDENTITY,
    [ProjectCommunicationFileId]             INT             NOT NULL,
    [Signature]                              VARBINARY(MAX)  NOT NULL,
    [FileName]                               NVARCHAR(200)   NOT NULL,

    CONSTRAINT [PK_ProjectCommunicationFileSignatures]                            PRIMARY KEY ([ProjectCommunicationFileSignatureId]),
    CONSTRAINT [FK_ProjectCommunicationFileSignatures_ProjectCommunicationFiles]  FOREIGN KEY ([ProjectCommunicationFileId])   REFERENCES [dbo].[ProjectCommunicationFiles] ([ProjectCommunicationFileId])
);
GO

exec spDescTable  N'ProjectCommunicationFileSignatures', N'Подпис на файл на комуникация към проектно предложение.'
exec spDescColumn N'ProjectCommunicationFileSignatures', N'ProjectCommunicationFileSignatureId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectCommunicationFileSignatures', N'ProjectCommunicationFileId'            , N'Идентификатор на файл проектно предложние.'
exec spDescColumn N'ProjectCommunicationFileSignatures', N'Signature'                             , N'Подпис.'
exec spDescColumn N'ProjectCommunicationFileSignatures', N'FileName'                              , N'Име на подпис.'

GO