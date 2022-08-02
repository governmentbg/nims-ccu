PRINT 'ProcedureLocations'
GO

CREATE TABLE [dbo].[ProcedureLocations] (
    [ProcedureLocationId]                                   INT                 NOT NULL IDENTITY,
    [ProcedureId]                                           INT                 NOT NULL,

    [NutsLevel]                                             INT                 NOT NULL,
    [CountryId]                                             INT                 NULL,
    [Nuts1Id]                                               INT                 NULL,
    [Nuts2Id]                                               INT                 NULL,
    [DistrictId]                                            INT                 NULL,
    [MunicipalityId]                                        INT                 NULL,
    [SettlementId]                                          INT                 NULL,
    [ProtectedZoneId]                                       INT                 NULL,

    CONSTRAINT [PK_ProcedureLocations]                                                PRIMARY KEY ([ProcedureLocationId]),
    CONSTRAINT [FK_ProcedureLocations_Procedures]                                     FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureLocations_Countries]                                      FOREIGN KEY ([CountryId])           REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_ProcedureLocations_Nuts1s]                                         FOREIGN KEY ([Nuts1Id])             REFERENCES [dbo].[Nuts1s] ([Nuts1Id]),
    CONSTRAINT [FK_ProcedureLocations_Nuts2s]                                         FOREIGN KEY ([Nuts2Id])             REFERENCES [dbo].[Nuts2s] ([Nuts2Id]),
    CONSTRAINT [FK_ProcedureLocations_Districts]                                      FOREIGN KEY ([DistrictId])          REFERENCES [dbo].[Districts] ([DistrictId]),
    CONSTRAINT [FK_ProcedureLocations_Municipalities]                                 FOREIGN KEY ([MunicipalityId])      REFERENCES [dbo].[Municipalities] (MunicipalityId),
    CONSTRAINT [FK_ProcedureLocations_Settlements]                                    FOREIGN KEY ([SettlementId])        REFERENCES [dbo].[Settlements] (SettlementId),
    CONSTRAINT [FK_ProcedureLocations_ProtectedZones]                                 FOREIGN KEY ([ProtectedZoneId])     REFERENCES [dbo].[ProtectedZones] (ProtectedZoneId),
    CONSTRAINT [CHK_ProcedureLocations_NutsLevel]                                     CHECK       ([NutsLevel] IN (1, 2, 3, 4, 5, 6, 7))
);
GO

exec spDescTable  N'ProcedureLocations', N'Местоположение за процедура.'
exec spDescColumn N'ProcedureLocations', N'ProcedureLocationId'                            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureLocations', N'ProcedureId'                                    , N'Идентификатор на процедура.'

exec spDescColumn N'ProcedureLocations', N'NutsLevel'                                      , N'1 - Държава, 2 - Регион 1, 3 - Регион 2, 4 - Област, 5 - Община, 6 - Населено място, 7 - Защитена зона.'
exec spDescColumn N'ProcedureLocations', N'CountryId'                                      , N'Идентификатор на държава.'
exec spDescColumn N'ProcedureLocations', N'Nuts1Id'                                        , N'Идентификатор на NUTS ниво 1.'
exec spDescColumn N'ProcedureLocations', N'Nuts2Id'                                        , N'Идентификатор на NUTS ниво 2.'
exec spDescColumn N'ProcedureLocations', N'DistrictId'                                     , N'Идентификатор на област.'
exec spDescColumn N'ProcedureLocations', N'MunicipalityId'                                 , N'Идентификатор на община.'
exec spDescColumn N'ProcedureLocations', N'SettlementId'                                   , N'Идентификатор на териториална единица.'
exec spDescColumn N'ProcedureLocations', N'ProtectedZoneId'                                , N'Идентификатор на защитена зона.'

GO
