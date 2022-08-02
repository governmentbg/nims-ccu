PRINT 'ContractsContractRegistrations'
GO

CREATE TABLE [dbo].[ContractsContractRegistrations] (
    [ContractsContractRegistrationId]          INT                 NOT NULL IDENTITY,
    [ContractRegistrationId]                   INT                 NOT NULL,
    [ContractId]                               INT                 NOT NULL,
    [CreatedByUserId]                          INT                 NOT NULL,
    [CreateDate]                               DATETIME2           NOT NULL,
    [BlobKey]                                  UNIQUEIDENTIFIER    NOT NULL,
    [IsActive]                                 BIT                 NOT NULL

    CONSTRAINT [PK_ContractsContractRegistrations]                                      PRIMARY KEY ([ContractsContractRegistrationId]),
    CONSTRAINT [FK_ContractsContractRegistrations_ContractRegistrations]                FOREIGN KEY ([ContractRegistrationId])     REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]),
    CONSTRAINT [FK_ContractsContractRegistrations_Contracts]                            FOREIGN KEY ([ContractId])                 REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractsContractRegistrations_Blobs]                                FOREIGN KEY ([BlobKey])                    REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [UQ_ContractsContractRegistrations_ContractRegistrationId_ContractId]    UNIQUE ([ContractRegistrationId], [ContractId])
);
GO

exec spDescTable  N'ContractsContractRegistrations', N'Електронно подписани договори за БФП.'
exec spDescColumn N'ContractsContractRegistrations', N'ContractsContractRegistrationId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractsContractRegistrations', N'ContractId'                       , N'Идентификатор на договор за БФП.'
exec spDescColumn N'ContractsContractRegistrations', N'CreatedByUserId'                  , N'Идентификатор на създаващия потребител.'
exec spDescColumn N'ContractsContractRegistrations', N'CreateDate'                       , N'Дата на създаване.'
exec spDescColumn N'ContractsContractRegistrations', N'BlobKey'                          , N'Идентификатор на файл.'
exec spDescColumn N'ContractsContractRegistrations', N'IsActive'                         , N'Маркер за активност.'

GO
