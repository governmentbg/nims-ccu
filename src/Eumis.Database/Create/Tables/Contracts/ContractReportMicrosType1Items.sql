PRINT 'ContractReportMicrosType1Items'
GO

CREATE TABLE [dbo].[ContractReportMicrosType1Items] (
    [ContractReportMicrosType1ItemId] INT    NOT NULL IDENTITY,
    [ContractReportMicroId]           INT    NOT NULL,

    [DistrictId]                      INT    NULL,
    [MunicipalityId]                  INT    NULL,

    [TotalCount]                      INT    NULL,
    [ChildrensCount]                  INT    NULL,
    [SeniorsCount]                    INT    NULL,
    [FemalesCount]                    INT    NULL,
    [EmigrantsCount]                  INT    NULL,
    [ForeignCitizensCount]            INT    NULL,
    [MinoritiesCount]                 INT    NULL,
    [GypsiesCount]                    INT    NULL,
    [DisabledPersonsCount]            INT    NULL,
    [HomelessCount]                   INT    NULL,

    CONSTRAINT [PK_ContractReportMicrosType1Items]                      PRIMARY KEY ([ContractReportMicrosType1ItemId]),
    CONSTRAINT [FK_ContractReportMicrosType1Items_ContractReportMicros] FOREIGN KEY ([ContractReportMicroId]) REFERENCES [dbo].[ContractReportMicros]               ([ContractReportMicroId]),
    CONSTRAINT [FK_ContractReportMicrosType1Items_Districts]            FOREIGN KEY ([DistrictId])            REFERENCES [dbo].[ContractReportMicrosDistricts]      ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosType1Items_Municipalities]       FOREIGN KEY ([MunicipalityId])        REFERENCES [dbo].[ContractReportMicrosMunicipalities] ([ContractReportMicrosMunicipalityId])
);
GO

exec spDescTable  N'ContractReportMicrosType1Items', N'Микроданни участници (ФЕПНЛ).'
exec spDescColumn N'ContractReportMicrosType1Items', N'ContractReportMicrosType1ItemId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportMicrosType1Items', N'ContractReportMicroId'          , N'Идентификатор на микроданни.'

exec spDescColumn N'ContractReportMicrosType1Items', N'DistrictId'                     , N'Идентификатор на област.'
exec spDescColumn N'ContractReportMicrosType1Items', N'MunicipalityId'                 , N'Идентификатор на община.'

exec spDescColumn N'ContractReportMicrosType1Items', N'TotalCount'                     , N'ОБЩ БРОЙ НА ЛИЦАТА, ПОЛУЧАВАЩИ ХРАНИТЕЛНИ ПРОДУКТИ ЗА ПОДПОМАГАНЕ.'
exec spDescColumn N'ContractReportMicrosType1Items', N'ChildrensCount'                 , N'Деца на възраст 15 години или по-малки.'
exec spDescColumn N'ContractReportMicrosType1Items', N'SeniorsCount'                   , N'Лица на възраст 65 години или по-възрастни.'
exec spDescColumn N'ContractReportMicrosType1Items', N'FemalesCount'                   , N'Жени.'
exec spDescColumn N'ContractReportMicrosType1Items', N'EmigrantsCount'                 , N'Емигранти.'
exec spDescColumn N'ContractReportMicrosType1Items', N'ForeignCitizensCount'           , N'Чужди граждани.'
exec spDescColumn N'ContractReportMicrosType1Items', N'MinoritiesCount'                , N'Малцинства.'
exec spDescColumn N'ContractReportMicrosType1Items', N'GypsiesCount'                   , N'Роми.'
exec spDescColumn N'ContractReportMicrosType1Items', N'DisabledPersonsCount'           , N'Хора с увреждания.'
exec spDescColumn N'ContractReportMicrosType1Items', N'HomelessCount'                  , N'Бездомни лица.'
GO
