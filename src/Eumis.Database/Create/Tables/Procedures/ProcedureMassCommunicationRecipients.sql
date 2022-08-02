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
