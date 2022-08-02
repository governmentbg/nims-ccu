PRINT 'ProcedureMassCommunications'
GO

CREATE TABLE [dbo].[ProcedureMassCommunications] (
    [ProcedureMassCommunicationId]                  INT             NOT NULL IDENTITY,
    [ProgrammeId]                                   INT             NOT NULL,
    [ProcedureId]                                   INT             NOT NULL,
    [Status]                                        INT             NOT NULL,
    [Subject]                                       NVARCHAR(MAX)   NULL,
    [Body]                                          NVARCHAR(MAX)   NULL,

    [CreateDate]                                    DATETIME2       NOT NULL,
    [ModifyDate]                                    DATETIME2       NOT NULL,
    [Version]                                       ROWVERSION      NOT NULL,

    CONSTRAINT [PK_ProcedureMassCommunications]                     PRIMARY KEY ([ProcedureMassCommunicationId]),
    CONSTRAINT [FK_ProcedureMassCommunications_Procedures]          FOREIGN KEY ([ProcedureId])           REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureMassCommunications_MapNodes]            FOREIGN KEY ([ProgrammeId])           REFERENCES [dbo].[MapNodes]    ([MapNodeId]),
    CONSTRAINT [CHK_ProcedureMassCommunications_Status]             CHECK       ([Status] IN (1, 2)),
);
GO

PRINT 'ProcedureMassCommunicationDocuments'
GO

CREATE TABLE [dbo].[ProcedureMassCommunicationDocuments] (
    [ProcedureMassCommunicationDocumentId]      INT                 NOT NULL IDENTITY,
    [ProcedureMassCommunicationId]              INT                 NOT NULL,
    [Name]                                      NVARCHAR(MAX)       NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [FileName]                                  NVARCHAR(MAX)       NULL,
    [BlobKey]                                   UNIQUEIDENTIFIER    NULL

    CONSTRAINT [PK_ProcedureMassCommunicationDocuments]                              PRIMARY KEY ([ProcedureMassCommunicationDocumentId]),
    CONSTRAINT [FK_ProcedureMassCommunicationDocuments_ProcedureMassCommunications]  FOREIGN KEY ([ProcedureMassCommunicationId])  REFERENCES [dbo].[ProcedureMassCommunications] ([ProcedureMassCommunicationId]),
    CONSTRAINT [FK_ProcedureMassCommunicationDocuments_Blobs]                        FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ProcedureMassCommunicationDocuments', N'Документи към обща кореспонденция.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'ProcedureMassCommunicationDocumentId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'ProcedureMassCommunicationId'         , N'Идентификатор на обща комуникация.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'Name'                                 , N'Наименование.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'Description'                          , N'Описание.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'FileName'                             , N'Наименование на файл.'
exec spDescColumn N'ProcedureMassCommunicationDocuments', N'BlobKey'                              , N'Идентификатор на файл.'
GO

PRINT 'ProcedureMassCommunicationRecipients'
GO

CREATE TABLE [dbo].[ProcedureMassCommunicationRecipients] (
    [ProcedureMassCommunicationRecipientId]     INT                 NOT NULL IDENTITY,
    [ProcedureMassCommunicationId]              INT                 NOT NULL,
    [ContractId]                                INT                 NOT NULL,
    
    CONSTRAINT [PK_ProcedureMassCommunicationRecipients]                              PRIMARY KEY ([ProcedureMassCommunicationRecipientId]),
    CONSTRAINT [FK_ProcedureMassCommunicationRecipients_ProcedureMassCommunications]  FOREIGN KEY ([ProcedureMassCommunicationId])  REFERENCES [dbo].[ProcedureMassCommunications] ([ProcedureMassCommunicationId]),
    CONSTRAINT [FK_ProcedureMassCommunicationRecipients_Contracts]                    FOREIGN KEY ([ContractId])                    REFERENCES [dbo].[Contracts] ([ContractId]),
);
GO

exec spDescTable  N'ProcedureMassCommunicationRecipients', N'Получатели на обща кореспонденция.'
exec spDescColumn N'ProcedureMassCommunicationRecipients', N'ProcedureMassCommunicationRecipientId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureMassCommunicationRecipients', N'ProcedureMassCommunicationId'          , N'Идентификатор на обща комуникация.'
exec spDescColumn N'ProcedureMassCommunicationRecipients', N'ContractId'                            , N'Идентификатор на договор.'
GO
