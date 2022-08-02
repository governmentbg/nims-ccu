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

GO
INSERT INTO [ProcedureLocations] (
	[ProcedureId],
	[NutsLevel],
	[CountryId], 
	[Nuts1Id],
	[Nuts2Id],
	[DistrictId],
	[MunicipalityId],
	[SettlementId],
	[ProtectedZoneId])
SELECT
    [ProcedureId],
	[NutsLevel],
	[CountryId], 
	[Nuts1Id],
	[Nuts2Id],
	[DistrictId],
	[MunicipalityId],
	[SettlementId],
	[ProtectedZoneId]
FROM [Procedures]
GO

GO
ALTER TABLE [Procedures] DROP CONSTRAINT [CHK_Procedures_NutsLevel];
ALTER TABLE [Procedures] ALTER COLUMN [NutsLevel] INT NULL;
GO
