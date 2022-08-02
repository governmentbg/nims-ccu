PRINT 'ContractReportMicrosType4Items'
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

exec spDescTable  N'ContractReportMicrosType4Items', N'Микроданни на АСП.'
exec spDescColumn N'ContractReportMicrosType4Items', N'ContractReportMicrosType4ItemId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportMicrosType4Items', N'ContractReportMicroId'          , N'Идентификатор на микроданни.'

exec spDescColumn N'ContractReportMicrosType4Items', N'DistrictId'                     , N'Идентификатор на област.'
exec spDescColumn N'ContractReportMicrosType4Items', N'MunicipalityId'                 , N'Идентификатор на община.'

exec spDescColumn N'ContractReportMicrosType4Items', N'FruitAmounts'                   , N'Плодове (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'VegetableAmounts'               , N'Зеленчуци (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'Group1TotalAmounts'             , N'Общо количество плодове и зеленчуци.'

exec spDescColumn N'ContractReportMicrosType4Items', N'MeatAmounts'                    , N'Месо (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'EggAmounts'                     , N'Яйца (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'FishAmounts'                    , N'Риба и рибни продукти (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'Group2TotalAmounts'             , N'Общо количество месо, яйца, риба и рибни продукти.'

exec spDescColumn N'ContractReportMicrosType4Items', N'FlourAmounts'                   , N'Брашно (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'BreadAmounts'                   , N'Хляб (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'PotatoAmounts'                  , N'Картофи (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'RiceAmounts'                    , N'Ориз (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'StarchProductAmounts'           , N'Други съдържащи скорбяла продукти (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'Group3TotalAmounts'             , N'Общо количество брашно, хляб, картофи, ориз и други съдържащи скорбяла продукти.'

exec spDescColumn N'ContractReportMicrosType4Items', N'SugarAmounts'                   , N'Захар (кг.).'

exec spDescColumn N'ContractReportMicrosType4Items', N'MilkProductAmounts'             , N'Млечни продукти (кг.).'

exec spDescColumn N'ContractReportMicrosType4Items', N'FatsOrOilsAmounts'              , N'Мазнина, масла (кг.).'

exec spDescColumn N'ContractReportMicrosType4Items', N'FastFoodAmounts'                , N'Готови храни (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'OtherFoodAmounts'               , N'Други храни (които не попадат в предходните категории) (кг.).'
exec spDescColumn N'ContractReportMicrosType4Items', N'Group4TotalAmounts'             , N'Общо количество готови храни и други храни.'

exec spDescColumn N'ContractReportMicrosType4Items', N'TotalDishesCount'               , N'Брой ястия.'
exec spDescColumn N'ContractReportMicrosType4Items', N'TotalPackagesCount'             , N'Брой пакети.'
GO
