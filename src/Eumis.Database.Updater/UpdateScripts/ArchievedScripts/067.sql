GO

CREATE TABLE [dbo].[ProjectCommunicationFiles] (
    [ProjectCommunicationFileId]     INT             NOT NULL IDENTITY,
    [ProjectCommunicationId]         INT             NOT NULL,
    [File]                           VARBINARY(MAX)  NOT NULL,
    [FileName]                       NVARCHAR(200)   NOT NULL,
    [CreateDate]                     DATETIME2       NOT NULL,
    [ModifyDate]                     DATETIME2       NOT NULL,
    [Version]                        ROWVERSION      NOT NULL

    CONSTRAINT [PK_ProjectCommunicationFiles]                             PRIMARY KEY ([ProjectCommunicationFileId]),
    CONSTRAINT [FK_ProjectCommunicationFiles_ProjectCommunications]       FOREIGN KEY ([ProjectCommunicationId])   REFERENCES [dbo].[ProjectCommunications] ([ProjectCommunicationId])
);
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
