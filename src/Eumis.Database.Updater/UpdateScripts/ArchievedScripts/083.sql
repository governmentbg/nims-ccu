GO

-- creating contract tables
DROP TABLE [ContractPayPlans],
           [ContractActivities],
           [ContractErrands],
           [ContractIndicators],
           [ContractInterventionCategories],
           [ContractPartners],
           [ContractShareExpenseBudgets],
           [ContractTeams],
           [Contracts],
           [ContractTypes],
           [ContractStatuses];
GO

CREATE TABLE [dbo].[ContractRegistrations] (
    [ContractRegistrationId]        INT                 NOT NULL IDENTITY,
    [Email]                         NVARCHAR (200)      NOT NULL UNIQUE,
    [Uin]                           NVARCHAR (50)       NOT NULL,
    [FirstName]                     NVARCHAR (100)      NOT NULL,
    [LastName]                      NVARCHAR (100)      NOT NULL,
    [Phone]                         NVARCHAR (50)       NULL,
    [PasswordHash]                  NVARCHAR (200)      NULL,
    [PasswordSalt]                  NVARCHAR (200)      NULL,
    [ActivationCode]                NVARCHAR (50)       NULL,
    [PasswordRecoveryCode]          NVARCHAR (50)       NULL,
    [CreateDate]                    DATETIME2           NOT NULL,
    [ModifyDate]                    DATETIME2           NOT NULL,
    [Version]                       ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractRegistrations]   PRIMARY KEY ([ContractRegistrationId])
);
GO

CREATE UNIQUE INDEX [UQ_ContractRegistrations_ActivationCode]
    ON [dbo].[ContractRegistrations]([ActivationCode]) WHERE [ActivationCode] IS NOT NULL
GO

CREATE UNIQUE INDEX [UQ_ContractRegistrations_PasswordRecoveryCode]
    ON [dbo].[ContractRegistrations]([PasswordRecoveryCode]) WHERE [PasswordRecoveryCode] IS NOT NULL
GO

CREATE TABLE [dbo].[Contracts] (
    [ContractId]                            INT               NOT NULL IDENTITY,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,
    [ProgrammeId]                           INT               NOT NULL,
    [ProcedureId]                           INT               NOT NULL,
    [ProjectId]                             INT               NOT NULL,
    [ContractType]                          INT               NOT NULL,
    [ContractStatus]                        INT               NOT NULL,

    [CompanyId]                             INT               NOT NULL,
    [CompanyName]                           NVARCHAR(200)     NOT NULL,
    [CompanyUin]                            NVARCHAR(200)     NOT NULL,
    [CompanyUinType]                        INT               NOT NULL,
    [CompanyKidCodeId]                      INT               NULL,
    [CompanySizeTypeId]                     INT               NOT NULL,

    [Name]                                  NVARCHAR(MAX)     NULL,
    [ContractDate]                          DATETIME2         NULL,
    [RegNumber]                             NVARCHAR(200)     NOT NULL,

    [ExecutionStatus]                       INT               NULL,
    [StartDate]                             DATETIME2         NULL,
    [StartConditions]                       NVARCHAR(MAX)     NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_Contracts]                    PRIMARY KEY ([ContractId]),
    CONSTRAINT [FK_Contracts_Programmes]         FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_Contracts_Procedures]         FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_Contracts_Projects]           FOREIGN KEY ([ProjectId])           REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_Contracts_Companies]          FOREIGN KEY ([CompanyId])           REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [FK_Contracts_CompanyKidCodes]    FOREIGN KEY ([CompanyKidCodeId])    REFERENCES [dbo].[KidCodes] ([KidCodeId]),
    CONSTRAINT [FK_Contracts_CompanySizeType]    FOREIGN KEY ([CompanySizeTypeId])   REFERENCES [dbo].[CompanySizeTypes] ([CompanySizeTypeId]),
    CONSTRAINT [CHK_Contracts_ContractType]      CHECK ([ContractType]     IN (1, 2, 3)),
    CONSTRAINT [CHK_Contracts_ContractStatus]    CHECK ([ContractStatus]   IN (1, 2)),
    CONSTRAINT [CHK_Contracts_ExecutionStatus]   CHECK ([ExecutionStatus]  IN (1, 2, 3, 4, 5, 6 ,7))
);
GO

CREATE UNIQUE INDEX [UQ_Contracts_RegNumber]
ON [Contracts]([RegNumber])
WHERE [RegNumber] IS NOT NULL;

CREATE TABLE [dbo].[ContractVersionXmls] (
    [ContractVersionXmlId]            INT                 NOT NULL IDENTITY,
    [Gid]                             UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ContractId]                      INT                 NOT NULL,
    [Xml]                             XML                 NOT NULL,
    [Hash]                            NVARCHAR(10)        NOT NULL UNIQUE,
    [OrderNum]                        INT                 NOT NULL,

    [VersionType]                     INT                 NOT NULL,
    [VersionNum]                      INT                 NOT NULL,
    [VersionSubNum]                   INT                 NOT NULL,

    [Name]                            NVARCHAR(MAX)       NULL,
    [ContractDate]                    DATETIME2           NULL,
    [RegNumber]                       NVARCHAR(200)       NOT NULL,
    [ExecutionStatus]                 INT                 NULL,
    [StartDate]                       DATETIME2           NULL,
    [StartConditions]                 NVARCHAR(MAX)       NULL,

    [Status]                          INT                 NOT NULL,
    [CreatedByUserId]                 INT                 NOT NULL,
    [CreateNote]                      NVARCHAR(MAX)       NOT NULL,
    [CreateDate]                      DATETIME2           NOT NULL,
    [ModifyDate]                      DATETIME2           NOT NULL,
    [Version]                         ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractVersionXmls]                 PRIMARY KEY ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractVersionXmls_Contracts]       FOREIGN KEY ([ContractId])                   REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractVersionXmls_Users]           FOREIGN KEY ([CreatedByUserId])              REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractXmls_VersionType]           CHECK ([VersionType] IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractXmls_Status]                CHECK ([Status]      IN (1, 2, 3, 4))
);
GO

CREATE TABLE [dbo].[ContractProcurementXmls] (
    [ContractProcurementXmlId]  INT                 NOT NULL IDENTITY,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ContractId]                INT                 NOT NULL,
    [ContractVersionXmlId]      INT                 NOT NULL,
    [Source]                    INT                 NOT NULL,
    [Xml]                       XML                 NOT NULL,
    [Hash]                      NVARCHAR(10)        NOT NULL UNIQUE,
    [OrderNum]                  INT                 NOT NULL,
    [Status]                    INT                 NOT NULL,
    [CreatedByUserId]           INT                 NOT NULL,
    [CreateNote]                NVARCHAR(MAX)       NOT NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractProcurementXmls]                 PRIMARY KEY ([ContractProcurementXmlId]),
    CONSTRAINT [FK_ContractProcurementXmls_Contracts]       FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractProcurementXmls_VersionsXml]     FOREIGN KEY ([ContractVersionXmlId])    REFERENCES [dbo].[ContractVersionXmls] ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractProcurementXmls_Users]           FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractProcurementXmls_Source]         CHECK ([Source]      IN (1, 2)),
    CONSTRAINT [CHK_ContractProcurementXmls_Status]         CHECK ([Status]      IN (1, 2, 3, 4))
);
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


CREATE TABLE [dbo].[ContractCommunicationXmls] (
    [ContractCommunicationXmlId]    INT                NOT NULL IDENTITY,
    [Gid]                           UNIQUEIDENTIFIER   NOT NULL,
    [ContractId]                    INT                NOT NULL,
    [Status]                        INT                NOT NULL,
    [StatusNote]                    NVARCHAR(MAX)      NULL,
    [Source]                        INT                NOT NULL,
    [RegNumber]                     NVARCHAR(200)      NULL,
    [ReadDate]                      DATETIME2          NULL,

    [SendDate]                      DATETIME2          NULL,
    [Subject]                       NVARCHAR(MAX)      NULL,
    [Content]                       NVARCHAR(MAX)      NULL,
    [Xml]                           XML                NOT NULL,
    [Hash]                          NVARCHAR(10)       NOT NULL UNIQUE,

    [CreateDate]                    DATETIME2          NOT NULL,
    [ModifyDate]                    DATETIME2          NOT NULL,
    [Version]                       ROWVERSION         NOT NULL

    CONSTRAINT [PK_ContractCommunicationXmls]                              PRIMARY KEY ([ContractCommunicationXmlId]),
    CONSTRAINT [FK_ContractCommunicationXmls_Contracts]                    FOREIGN KEY ([ContractId])                   REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_ContractCommunicationXmls_Source]                      CHECK ([Source] IN (1, 2)),
    CONSTRAINT [CHK_ContractCommunicationXmls_Status]                      CHECK ([Status] IN (1, 2))
);
GO

CREATE TABLE [dbo].[ContractAccessCodes] (
    [ContractAccessCodeId]      INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Code]                      NVARCHAR (50)       NOT NULL,
    [FirstName]                 NVARCHAR (100)      NOT NULL,
    [LastName]                  NVARCHAR (100)      NOT NULL,
    [Position]                  NVARCHAR (100)      NULL,
    [Email]                     NVARCHAR (200)      NOT NULL,
    [IsActive]                  BIT                 NOT NULL,
    [CanReadContracts]          BIT                 NOT NULL,
    [CanReadProcurements]       BIT                 NOT NULL,
    [CanWriteProcurements]      BIT                 NOT NULL,
    [CanReadTechnicalPlan]      BIT                 NOT NULL,
    [CanWriteTechnicalPlan]     BIT                 NOT NULL,
    [CanReadFinancialPlan]      BIT                 NOT NULL,
    [CanWriteFinancialPlan]     BIT                 NOT NULL,
    [CanReadPaymentRequest]     BIT                 NOT NULL,
    [CanWritePaymentRequest]    BIT                 NOT NULL,
    [CanReadCommunication]      BIT                 NOT NULL,
    [CanReadSpendingPlan]       BIT                 NOT NULL,
    [CanWriteSpendingPlan]      BIT                 NOT NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractAccessCodes]                 PRIMARY KEY ([ContractAccessCodeId]),
    CONSTRAINT [FK_ContractAccessCodes_Contracts]       FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts] ([ContractId]),
);
GO

CREATE VIEW vwUniqueContractEmailAccessCodeIndexed WITH SCHEMABINDING
AS

SELECT
    ac.[Code],
    ac.[Email],
    ac.[ContractAccessCodeId],
    ac.[IsActive],
    c.[ContractId],
    c.[Gid] AS ContractGid
FROM [dbo].[ContractAccessCodes] ac
JOIN [dbo].[Contracts] c ON ac.[ContractId] = c.[ContractId]

GO

GRANT SELECT ON vwUniqueContractEmailAccessCodeIndexed TO PUBLIC
GO

CREATE UNIQUE CLUSTERED INDEX [vwUniqueContractEmailAccessCodeIndexed_PK]
 ON [dbo].[vwUniqueContractEmailAccessCodeIndexed] 
(
    [Email] ASC,
    [ContractId] ASC
)
GO

CREATE TABLE [dbo].[ContractCommunicationXmlFiles] (
    [ContractCommunicationXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractCommunicationXmlId]      INT                 NOT NULL,
    [BlobKey]                         UNIQUEIDENTIFIER    NOT NULL,
    [Name]                            NVARCHAR(200)       NOT NULL,
    [Description]                     NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractCommunicationXmlFiles]                             PRIMARY KEY ([ContractCommunicationXmlFileId]),
    CONSTRAINT [FK_ContractCommunicationXmlFiles_ContractCommunicationXmls]   FOREIGN KEY ([ContractCommunicationXmlId])    REFERENCES [dbo].[ContractCommunicationXmls] ([ContractCommunicationXmlId]),
    CONSTRAINT [FK_ContractCommunicationXmlFiles_Blobs]                       FOREIGN KEY ([BlobKey])                       REFERENCES [dbo].[Blobs] ([Key])
);
GO

CREATE TABLE [dbo].[ContractVersionXmlFiles] (
    [ContractVersionXmlFileId]  INT                 NOT NULL IDENTITY,
    [ContractVersionXmlId]      INT                 NOT NULL,
    [Type]                      INT                 NOT NULL,
    [BlobKey]                   UNIQUEIDENTIFIER    NOT NULL,
    [Name]                      NVARCHAR(200)       NOT NULL,
    [Description]               NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractVersionXmlFiles]                       PRIMARY KEY ([ContractVersionXmlFileId]),
    CONSTRAINT [FK_ContractVersionXmlFiles_ContractVersionXmls]   FOREIGN KEY ([ContractVersionXmlId])    REFERENCES [dbo].[ContractVersionXmls] ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractVersionXmlFiles_Blobs]                 FOREIGN KEY ([BlobKey])                 REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [CHK_ContractVersionXmlFiles_Type]                 CHECK ([Type] IN (1, 2))
);
GO

CREATE TABLE [dbo].[ContractSpendingPlanXmls] (
    [ContractSpendingPlanXmlId] INT                 NOT NULL IDENTITY,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ContractId]                INT                 NOT NULL,
    [ContractVersionXmlId]      INT                 NOT NULL,
    [Source]                    INT                 NOT NULL,
    [Xml]                       XML                 NOT NULL,
    [Hash]                      NVARCHAR(10)        NOT NULL UNIQUE,
    [OrderNum]                  INT                 NOT NULL,
    [Status]                    INT                 NOT NULL,
    [CreatedByUserId]           INT                 NOT NULL,
    [CreateNote]                NVARCHAR(MAX)       NOT NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_ContractSpendingPlanXmls]                 PRIMARY KEY ([ContractSpendingPlanXmlId]),
    CONSTRAINT [FK_ContractSpendingPlanXmls_Contracts]       FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractSpendingPlanXmls_VersionsXml]     FOREIGN KEY ([ContractVersionXmlId])    REFERENCES [dbo].[ContractVersionXmls] ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractSpendingPlanXmls_Users]           FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractSpendingPlanXmls_Source]         CHECK ([Source]      IN (1, 2)),
    CONSTRAINT [CHK_ContractSpendingPlanXmls_Status]         CHECK ([Status]      IN (1, 2, 3, 4))
);
GO