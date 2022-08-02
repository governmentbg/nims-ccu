PRINT 'HistoricContractLocations'
GO

CREATE TABLE [dbo].[HistoricContractLocations] (
    [HistoricContractLocationId]    INT             NOT NULL,
    [HistoricContractId]            INT             NOT NULL,
    [CountryCode]                   NVARCHAR(200)   NOT NULL,
    [ProtectedZoneCode]             NVARCHAR(200)   NOT NULL,
    [Nuts1Code]                     NVARCHAR(200)   NOT NULL,
    [Nuts2Code]                     NVARCHAR(200)   NOT NULL,
    [DistrictCode]                  NVARCHAR(200)   NOT NULL,
    [MunicipalityCode]              NVARCHAR(200)   NOT NULL,
    [SettlementCode]                NVARCHAR(10)    NOT NULL,
    [FullPath]                      NVARCHAR(MAX)   NOT NULL,
    [FullPathName]                  NVARCHAR(MAX)   NOT NULL

    CONSTRAINT [PK_HistoricContractLocations]                       PRIMARY KEY ([HistoricContractLocationId]),
    CONSTRAINT [FK_HistoricContractLocations_HistoricContracts]     FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractLocations',     N'Местонахождения.'
exec spDescColumn N'HistoricContractLocations',     N'HistoricContractLocationId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractLocations',     N'HistoricContractId'           , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractLocations',     N'CountryCode'                  , N'Код на държава (ISO 3166-1 alpha-2).'
exec spDescColumn N'HistoricContractLocations',     N'ProtectedZoneCode'            , N'Код на защитена зона.'
exec spDescColumn N'HistoricContractLocations',     N'Nuts1Code'                    , N'NUTS 1 код.'
exec spDescColumn N'HistoricContractLocations',     N'Nuts2Code'                    , N'NUTS 2 код.'
exec spDescColumn N'HistoricContractLocations',     N'DistrictCode'                 , N'Код на област (NUTS 3).'
exec spDescColumn N'HistoricContractLocations',     N'MunicipalityCode'             , N'Код на община (ЕКАТТЕ).'
exec spDescColumn N'HistoricContractLocations',     N'SettlementCode'               , N'Код на населено място (ЕКАТТЕ).'
exec spDescColumn N'HistoricContractLocations',     N'FullPath'                     , N'Пълен път на местонахождение.'
exec spDescColumn N'HistoricContractLocations',     N'FullPathName'                 , N'Пълно наименование на местонахождение.'
GO
