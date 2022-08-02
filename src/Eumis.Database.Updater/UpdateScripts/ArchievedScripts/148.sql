GO

CREATE TABLE [dbo].[ContractReportMicrosType4Items] (
    [ContractReportMicrosType4ItemId] INT           NOT NULL IDENTITY,
    [ContractReportMicroId]           INT           NOT NULL,

    [DistrictId]                      INT           NULL,
    [MunicipalityId]                  INT           NULL,

    [FruitAmounts]                    DECIMAL(15,3) NULL,
    [VegetableAmounts]                DECIMAL(15,3) NULL,
    [Group1TotalAmounts]              DECIMAL(15,3) NULL,

    [MeatAmounts]                     DECIMAL(15,3) NULL,
    [EggAmounts]                      DECIMAL(15,3) NULL,
    [FishAmounts]                     DECIMAL(15,3) NULL,
    [Group2TotalAmounts]              DECIMAL(15,3) NULL,

    [FlourAmounts]                    DECIMAL(15,3) NULL,
    [BreadAmounts]                    DECIMAL(15,3) NULL,
    [PotatoAmounts]                   DECIMAL(15,3) NULL,
    [RiceAmounts]                     DECIMAL(15,3) NULL,
    [StarchProductAmounts]            DECIMAL(15,3) NULL,
    [Group3TotalAmounts]              DECIMAL(15,3) NULL,

    [SugarAmounts]                    DECIMAL(15,3) NULL,

    [MilkProductAmounts]              DECIMAL(15,3) NULL,

    [FatsOrOilsAmounts]               DECIMAL(15,3) NULL,

    [FastFoodAmounts]                 DECIMAL(15,3) NULL,
    [OtherFoodAmounts]                DECIMAL(15,3) NULL,
    [Group4TotalAmounts]              DECIMAL(15,3) NULL,

    [TotalDishesCount]                INT           NULL,
    [TotalPackagesCount]              INT           NULL,

    CONSTRAINT [PK_ContractReportMicrosType4Items]                      PRIMARY KEY ([ContractReportMicrosType4ItemId]),
    CONSTRAINT [FK_ContractReportMicrosType4Items_ContractReportMicros] FOREIGN KEY ([ContractReportMicroId]) REFERENCES [dbo].[ContractReportMicros]            ([ContractReportMicroId]),
    CONSTRAINT [FK_ContractReportMicrosType4Items_Districts]            FOREIGN KEY ([DistrictId])            REFERENCES [dbo].[ContractReportMicrosDistricts]      ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosType4Items_Municipalities]       FOREIGN KEY ([MunicipalityId])        REFERENCES [dbo].[ContractReportMicrosMunicipalities] ([ContractReportMicrosMunicipalityId])
);
GO

ALTER TABLE [dbo].[ContractReportMicros]
DROP CONSTRAINT [CHK_ContractReportMicros_Type];
GO

ALTER TABLE [dbo].[ContractReportMicros]
ADD CONSTRAINT [CHK_ContractReportMicros_Type] CHECK ([Type] IN (1, 2, 3, 4));
GO