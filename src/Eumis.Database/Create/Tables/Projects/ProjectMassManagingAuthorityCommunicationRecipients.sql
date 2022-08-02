PRINT 'ProjectMassManagingAuthorityCommunicationRecipients'
GO

CREATE TABLE [dbo].[ProjectMassManagingAuthorityCommunicationRecipients] (
    [ProjectMassManagingAuthorityCommunicationRecipientId]     INT                 NOT NULL IDENTITY,
    [ProjectMassManagingAuthorityCommunicationId]              INT                 NOT NULL,
    [ProjectId]                                                INT                 NOT NULL,

    CONSTRAINT [PK_ProjectMassManagingAuthorityCommunicationRecipients]                                             PRIMARY KEY ([ProjectMassManagingAuthorityCommunicationRecipientId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunicationRecipients_ProjectMassManagingAuthorityCommunications]  FOREIGN KEY ([ProjectMassManagingAuthorityCommunicationId])  REFERENCES [dbo].[ProjectMassManagingAuthorityCommunications] ([ProjectMassManagingAuthorityCommunicationId]),
    CONSTRAINT [FK_ProjectMassManagingAuthorityCommunicationRecipients_Projects]                                    FOREIGN KEY ([ProjectId])                    REFERENCES [dbo].[Projects] ([ProjectId]),
);
GO

exec spDescTable  N'ProjectMassManagingAuthorityCommunicationRecipients', N'Получатели на обща комуникация с УО.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationRecipients', N'ProjectMassManagingAuthorityCommunicationRecipientId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationRecipients', N'ProjectMassManagingAuthorityCommunicationId'          , N'Идентификатор на обща комуникация с УО.'
exec spDescColumn N'ProjectMassManagingAuthorityCommunicationRecipients', N'ProjectId'                                            , N'Идентификатор на проектно предложение.'
GO
