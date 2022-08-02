GO

CREATE TABLE [dbo].[ContractBudgetLevel3Amounts] (
    [ContractBudgetLevel3AmountId]      INT                 NOT NULL IDENTITY,
    [ContractId]                        INT                 NOT NULL,
    [ProcedureBudgetLevel2Id]           INT                 NOT NULL,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]                          BIT                 NOT NULL,

    [Name]                              NVARCHAR(MAX)       NOT NULL,
    [ContractEuAmount]                  MONEY               NOT NULL,
    [ContractBgAmount]                  MONEY               NOT NULL,
    [ContractSelfAmount]                MONEY               NOT NULL,
    [CurrentEuAmount]                   MONEY               NOT NULL,
    [CurrentBgAmount]                   MONEY               NOT NULL,
    [CurrentSelfAmount]                 MONEY               NOT NULL,
    [NutsCode]                          NVARCHAR(50)        NOT NULL,
    [OrderNum]                          INT                 NOT NULL,

    CONSTRAINT [PK_ContractBudgetLevel3Amounts]                        PRIMARY KEY ([ContractBudgetLevel3AmountId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_Contracts]              FOREIGN KEY ([ContractId])               REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_ProcedureBudgetLevel2]  FOREIGN KEY ([ProcedureBudgetLevel2Id])  REFERENCES [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id])
);
GO

CREATE TABLE [dbo].[ContractBudgetLevel3ICs] (
    [ContractBudgetLevel3AmountId]  INT NOT NULL,
    [InterventionCategoryId]        INT NOT NULL,

    CONSTRAINT [PK_ContractBudgetLevel3ICs]                             PRIMARY KEY ([ContractBudgetLevel3AmountId], [InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3ICs_ContractBudgetLevel3Amounts] FOREIGN KEY ([ContractBudgetLevel3AmountId])    REFERENCES [dbo].[ContractBudgetLevel3Amounts] ([ContractBudgetLevel3AmountId]),
    CONSTRAINT [FK_ContractBudgetLevel3ICs_InterventionCategories]      FOREIGN KEY ([InterventionCategoryId])          REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId])
);
GO

CREATE TABLE [dbo].[ContractActivities] (
    [ContractActivityId]        INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]                  BIT                 NOT NULL,

    [Code]                      NVARCHAR(MAX)       NOT NULL,
    [Name]                      NVARCHAR(MAX)       NOT NULL,
    [ExecutionMethod]           NVARCHAR(MAX)       NOT NULL,
    [Result]                    NVARCHAR(MAX)       NOT NULL,
    [StartMonth]                INT                 NOT NULL,
    [Duration]                  INT                 NOT NULL,
    [Amount]                    MONEY               NOT NULL,

    CONSTRAINT [PK_ContractActivities]                        PRIMARY KEY ([ContractActivityId]),
    CONSTRAINT [FK_ContractActivities_Contracts]              FOREIGN KEY ([ContractId])               REFERENCES [dbo].[Contracts] ([ContractId])
);
GO

CREATE TABLE [dbo].[ContractActivityCompanies] (
    [ContractActivityCompanyId]     INT             NOT NULL IDENTITY,
    [ContractActivityId]            INT             NOT NULL,

    [CompanyUin]                    NVARCHAR(200)   NOT NULL,
    [CompanyUinType]                INT             NOT NULL,
    [CompanyName]                   NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_ContractActivityCompanies]                       PRIMARY KEY ([ContractActivityCompanyId]),
    CONSTRAINT [FK_ContractActivityCompanies_ContractActivities]    FOREIGN KEY ([ContractActivityId])            REFERENCES [dbo].[ContractActivities] ([ContractActivityId])
);
GO

CREATE TABLE [dbo].[ContractContractors] (
    [ContractContractorId]      INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]                  BIT                 NOT NULL,

    [Uin]                       NVARCHAR(200)       NOT NULL,
    [UinType]                   INT                 NOT NULL,
    [Name]                      NVARCHAR(200)       NOT NULL,
    [NameAlt]                   NVARCHAR(200)       NOT NULL,
    [SeatCountryId]             INT                 NULL,
    [SeatSettlementId]          INT                 NULL,
    [SeatPostCode]              NVARCHAR(50)        NULL,
    [SeatStreet]                NVARCHAR(200)       NULL,
    [SeatAddress]               NVARCHAR(MAX)       NULL,
    [RepresentativeNames]       NVARCHAR(MAX)       NOT NULL,
    [RepresentativeIDNumber]    NVARCHAR(200)       NOT NULL,
    [VATRegistration]           INT                 NOT NULL,
    [CompanyLegalStatus]        INT                 NOT NULL,

    CONSTRAINT [PK_ContractContractors]                         PRIMARY KEY ([ContractContractorId]),
    CONSTRAINT [FK_ContractContractors_Contracts]               FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractContractors_Countries_Seat]          FOREIGN KEY ([SeatCountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_ContractContractors_Settlements_Seat]        FOREIGN KEY ([SeatSettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [CHK_ContractContractors_VATRegistration]        CHECK       ([VATRegistration] IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractContractors_CompanyLegalStatuses]   CHECK       ([CompanyLegalStatus] IN (1, 2))
);
GO

CREATE TABLE [dbo].[ContractContracts] (
    [ContractContractId]        INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]                  BIT                 NOT NULL,
    [ContractContractorId]      INT                 NOT NULL,

    [SignDate]                  DATETIME2           NOT NULL,
    [Number]                    NVARCHAR(MAX)       NOT NULL,
    [TotalAmount]               MONEY               NOT NULL,
    [AmountFinancedProject]     MONEY               NOT NULL,
    [BudgetDifference]          MONEY               NOT NULL,
    [NumberAnnexes]             INT                 NOT NULL,
    [CurrentAnnexTotalAmount]   MONEY               NOT NULL,
    [Comment]                   NVARCHAR(MAX)       NULL,
    [StartDate]                 DATETIME2           NOT NULL,
    [EndDate]                   DATETIME2           NOT NULL,
    [HasSubcontractorMember]    BIT                 NOT NULL,

    CONSTRAINT [PK_ContractContracts]                           PRIMARY KEY ([ContractContractId]),
    CONSTRAINT [FK_ContractContracts_Contracts]                 FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractContracts_ContractContractors]       FOREIGN KEY ([ContractContractorId])        REFERENCES [dbo].[ContractContractors] ([ContractContractorId])
);
GO

CREATE TABLE [dbo].[ContractSubcontracts] (
    [ContractSubcontractId]     INT                 NOT NULL IDENTITY,
    [ContractContractId]        INT                 NOT NULL,
    [ContractContractorId]      INT                 NOT NULL,

    [Type]                      INT                 NOT NULL,
    [Date]                      DATETIME2           NOT NULL,
    [Number]                    NVARCHAR(MAX)       NOT NULL,
    [Amount]                    MONEY               NOT NULL,

    CONSTRAINT [PK_ContractSubcontracts]                        PRIMARY KEY ([ContractSubcontractId]),
    CONSTRAINT [CHK_ContractSubcontracts_Type]                  CHECK ([Type] IN (1,2)),
    CONSTRAINT [FK_ContractSubcontracts_ContractContracts]      FOREIGN KEY ([ContractContractId])          REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [FK_ContractSubcontracts_ContractContractors]    FOREIGN KEY ([ContractContractorId])        REFERENCES [dbo].[ContractContractors] ([ContractContractorId])
);
GO

CREATE TABLE [dbo].[ContractContractActivities] (
    [ContractContractActivityId]    INT                 NOT NULL IDENTITY,
    [ContractContractId]            INT                 NOT NULL,
    [ContractActivityId]            INT                 NOT NULL,
    [ContractBudgetLevel3AmountId]  INT                 NOT NULL,

    CONSTRAINT [PK_ContractContractActivities]                              PRIMARY KEY ([ContractContractActivityId]),
    CONSTRAINT [FK_ContractContractActivities_ContractContracts]            FOREIGN KEY ([ContractContractId])              REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [FK_ContractContractActivities_ContractActivities]           FOREIGN KEY ([ContractActivityId])              REFERENCES [dbo].[ContractActivities] ([ContractActivityId]),
    CONSTRAINT [FK_ContractContractActivities_ContractBudgetLevel3Amounts]  FOREIGN KEY ([ContractBudgetLevel3AmountId])    REFERENCES [dbo].[ContractBudgetLevel3Amounts] ([ContractBudgetLevel3AmountId])
);
GO
