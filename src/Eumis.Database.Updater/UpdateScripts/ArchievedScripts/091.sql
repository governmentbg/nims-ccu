GO

ALTER TABLE [dbo].[Contracts]
ADD [CompletionDate]              DATETIME2         NULL,
    [NutsLevel]                   INT               NULL,
    [NutsCodes]                   NVARCHAR(MAX)     NULL,
    [BeneficiarySeatCountryId]    INT               NULL,
    [BeneficiarySeatSettlementId] INT               NULL,
    [BeneficiarySeatPostCode]     NVARCHAR(50)      NULL,
    [BeneficiarySeatStreet]       NVARCHAR(200)     NULL,
    [BeneficiarySeatAddress]      NVARCHAR(MAX)     NULL;
GO

ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [CHK_Contracts_NutsLevel]         CHECK       ([NutsLevel] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [FK_Contracts_Countries_Seat]     FOREIGN KEY ([BeneficiarySeatCountryId])    REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_Contracts_Settlements_Seat]   FOREIGN KEY ([BeneficiarySeatSettlementId]) REFERENCES [dbo].[Settlements] ([SettlementId]);
GO
