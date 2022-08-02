PRINT 'ContractReportMicrosType3Items'
GO

CREATE TABLE [dbo].[ContractReportMicrosType3Items] (
    [ContractReportMicrosType3ItemId] INT           NOT NULL IDENTITY,
    [ContractReportMicroId]           INT           NOT NULL,

    [DistrictId]                      INT           NULL,
    [MunicipalityId]                  INT           NULL,

    [KetchupTargetValue]              DECIMAL(15,3) NULL,
    [KetchupActualValue]              DECIMAL(15,3) NULL,

    [TomatoPasteTargetValue]          DECIMAL(15,3) NULL,
    [TomatoPasteActualValue]          DECIMAL(15,3) NULL,

    [GreenPeasTargetValue]            DECIMAL(15,3) NULL,
    [GreenPeasActualValue]            DECIMAL(15,3) NULL,

    [HotchPotchTargetValue]           DECIMAL(15,3) NULL,
    [HotchPotchActualValue]           DECIMAL(15,3) NULL,

    [NectarTargetValue]               DECIMAL(15,3) NULL,
    [NectarActualValue]               DECIMAL(15,3) NULL,

    [CompoteTargetValue]              DECIMAL(15,3) NULL,
    [CompoteActualValue]              DECIMAL(15,3) NULL,

    [JamTargetValue]                  DECIMAL(15,3) NULL,
    [JamActualValue]                  DECIMAL(15,3) NULL,

    [MeatCanTargetValue]              DECIMAL(15,3) NULL,
    [MeatCanActualValue]              DECIMAL(15,3) NULL,

    [FishCanTargetValue]              DECIMAL(15,3) NULL,
    [FishCanActualValue]              DECIMAL(15,3) NULL,

    [WheatFlourTargetValue]           DECIMAL(15,3) NULL,
    [WheatFlourActualValue]           DECIMAL(15,3) NULL,

    [RiceTargetValue]                 DECIMAL(15,3) NULL,
    [RiceActualValue]                 DECIMAL(15,3) NULL,

    [MacaroniTargetValue]             DECIMAL(15,3) NULL,
    [MacaroniActualValue]             DECIMAL(15,3) NULL,

    [BulgurTargetValue]               DECIMAL(15,3) NULL,
    [BulgurActualValue]               DECIMAL(15,3) NULL,

    [BeansTargetValue]                DECIMAL(15,3) NULL,
    [BeansActualValue]                DECIMAL(15,3) NULL,

    [LentilsTargetValue]              DECIMAL(15,3) NULL,
    [LentilsActualValue]              DECIMAL(15,3) NULL,

    [BiscuitTargetValue]              DECIMAL(15,3) NULL,
    [BiscuitActualValue]              DECIMAL(15,3) NULL,

    [WaffleTargetValue]               DECIMAL(15,3) NULL,
    [WaffleActualValue]               DECIMAL(15,3) NULL,

    [SugarTargetValue]                DECIMAL(15,3) NULL,
    [SugarActualValue]                DECIMAL(15,3) NULL,

    [HoneyTargetValue]                DECIMAL(15,3) NULL,
    [HoneyActualValue]                DECIMAL(15,3) NULL,

    [OilTargetValue]                  DECIMAL(15,3) NULL,
    [OilActualValue]                  DECIMAL(15,3) NULL,

    [LokumTargetValue]                DECIMAL(15,3) NULL,
    [LokumActualValue]                DECIMAL(15,3) NULL,

    CONSTRAINT [PK_ContractReportMicrosType3Items]                      PRIMARY KEY ([ContractReportMicrosType3ItemId]),
    CONSTRAINT [FK_ContractReportMicrosType3Items_ContractReportMicros] FOREIGN KEY ([ContractReportMicroId]) REFERENCES [dbo].[ContractReportMicros]            ([ContractReportMicroId]),
    CONSTRAINT [FK_ContractReportMicrosType3Items_Districts]            FOREIGN KEY ([DistrictId])            REFERENCES [dbo].[ContractReportMicrosDistricts]      ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosType3Items_Municipalities]       FOREIGN KEY ([MunicipalityId])        REFERENCES [dbo].[ContractReportMicrosMunicipalities] ([ContractReportMicrosMunicipalityId])
);
GO

exec spDescTable  N'ContractReportMicrosType3Items', N'Микроданни хранителни продукти.'
exec spDescColumn N'ContractReportMicrosType3Items', N'ContractReportMicrosType3ItemId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportMicrosType3Items', N'ContractReportMicroId'          , N'Идентификатор на микроданни.'

exec spDescColumn N'ContractReportMicrosType3Items', N'DistrictId'                     , N'Идентификатор на област.'
exec spDescColumn N'ContractReportMicrosType3Items', N'MunicipalityId'                 , N'Идентификатор на община.'

exec spDescColumn N'ContractReportMicrosType3Items', N'KetchupTargetValue'             , N'Лютеница - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'KetchupActualValue'             , N'Лютеница - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'TomatoPasteTargetValue'         , N'Доматено пюре - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'TomatoPasteActualValue'         , N'Доматено пюре - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'GreenPeasTargetValue'           , N'Зелен грах - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'GreenPeasActualValue'           , N'Зелен грах - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'HotchPotchTargetValue'          , N'Гювеч - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'HotchPotchActualValue'          , N'Гювеч - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'NectarTargetValue'              , N'Нектар - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'NectarActualValue'              , N'Нектар - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'CompoteTargetValue'             , N'Компот - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'CompoteActualValue'             , N'Компот - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'JamTargetValue'                 , N'Конфитюр - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'JamActualValue'                 , N'Конфитюр - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'MeatCanTargetValue'             , N'Месни консерви - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'MeatCanActualValue'             , N'Месни консерви - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'FishCanTargetValue'             , N'Рибни консерви - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'FishCanActualValue'             , N'Рибни консерви - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'WheatFlourTargetValue'          , N'Пшенично брашно - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'WheatFlourActualValue'          , N'Пшенично брашно - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'RiceTargetValue'                , N'Ориз - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'RiceActualValue'                , N'Ориз - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'MacaroniTargetValue'            , N'Макаронени изделия - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'MacaroniActualValue'            , N'Макаронени изделия - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'BulgurTargetValue'              , N'Булгур - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'BulgurActualValue'              , N'Булгур - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'BeansTargetValue'               , N'Зрял фасул - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'BeansActualValue'               , N'Зрял фасул - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'LentilsTargetValue'             , N'Леща - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'LentilsActualValue'             , N'Леща - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'BiscuitTargetValue'             , N'Обикновени бисквити - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'BiscuitActualValue'             , N'Обикновени бисквити - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'WaffleTargetValue'              , N'Вафли - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'WaffleActualValue'              , N'Вафли - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'SugarTargetValue'               , N'Бяла/кафява захар - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'SugarActualValue'               , N'Бяла/кафява захар - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'HoneyTargetValue'               , N'Пчелен мед - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'HoneyActualValue'               , N'Пчелен мед - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'OilTargetValue'                 , N'Олио - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'OilActualValue'                 , N'Олио - достигната стойност.'

exec spDescColumn N'ContractReportMicrosType3Items', N'LokumTargetValue'               , N'Локум - целева стойност.'
exec spDescColumn N'ContractReportMicrosType3Items', N'LokumActualValue'               , N'Локум - достигната стойност.'
GO
