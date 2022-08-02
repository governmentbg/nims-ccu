GO

CREATE TABLE [dbo].[ContractPartners] (
    [ContractPartnerId]     INT                 NOT NULL IDENTITY,
    [ContractId]            INT                 NOT NULL,

    [FinancialContribution] MONEY               NOT NULL,

    --company fields
    [Uin]                   NVARCHAR(200)       NOT NULL,
    [UinType]               INT                 NOT NULL,

    [CompanyTypeId]         INT                 NOT NULL,
    [CompanyLegalStatus]    INT                 NOT NULL,
    [CompanyLegalTypeId]    INT                 NOT NULL,
    [Name]                  NVARCHAR(200)       NOT NULL,
    [NameAlt]               NVARCHAR(200)       NULL,
    [KidCodeId]             INT                 NULL,
    [CompanySizeTypeId]     INT                 NOT NULL,

    [SeatCountryId]         INT                 NULL,
    [SeatSettlementId]      INT                 NULL,
    [SeatPostCode]          NVARCHAR(50)        NULL,
    [SeatStreet]            NVARCHAR(200)       NULL,
    [SeatAddress]           NVARCHAR(MAX)       NULL,

    [CorrCountryId]         INT                 NULL,
    [CorrSettlementId]      INT                 NULL,
    [CorrPostCode]          NVARCHAR(50)        NULL,
    [CorrStreet]            NVARCHAR(200)       NULL,
    [CorrAddress]           NVARCHAR(MAX)       NULL,

    [Representative]        NVARCHAR(200)       NULL,
    [Phone1]                NVARCHAR(100)       NOT NULL,
    [Phone2]                NVARCHAR(100)       NULL,
    [Fax]                   NVARCHAR(100)       NULL,
    [Email]                 NVARCHAR(200)       NOT NULL,

    [ContactName]           NVARCHAR(200)       NULL,
    [ContactPhone]          NVARCHAR(100)       NULL,
    [ContactEmail]          NVARCHAR(100)       NULL

    CONSTRAINT [PK_ContractPartners]                        PRIMARY KEY ([ContractPartnerId]),
    CONSTRAINT [FK_ContractPartners_Contracts]              FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractPartners_CompanyLegalTypes]      FOREIGN KEY ([CompanyLegalTypeId])          REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [FK_ContractPartners_Countries_Seat]         FOREIGN KEY ([SeatCountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_ContractPartners_Settlements_Seat]       FOREIGN KEY ([SeatSettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_ContractPartners_Countries_Corr]         FOREIGN KEY ([CorrCountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_ContractPartners_Settlements_Corr]       FOREIGN KEY ([CorrSettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_ContractPartners_KidCodes]               FOREIGN KEY ([KidCodeId])                   REFERENCES [dbo].[KidCodes] ([KidCodeId]),
    CONSTRAINT [FK_ContractPartners_CompanySizeType]        FOREIGN KEY ([CompanySizeTypeId])           REFERENCES [dbo].[CompanySizeTypes] ([CompanySizeTypeId]),
    CONSTRAINT [CHK_ContractPartners_CompanyLegalStatuses]  CHECK       ([CompanyLegalStatus] IN (1, 2))
);
GO
