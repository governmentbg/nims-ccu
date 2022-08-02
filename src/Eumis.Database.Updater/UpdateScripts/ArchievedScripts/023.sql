GO

ALTER TABLE [dbo].[Procedures]
DROP CONSTRAINT [CHK_Procedures_NutsLevel];
GO

ALTER TABLE [dbo].[Procedures]
ADD [ProtectedZoneId]           INT                 NULL,
    CONSTRAINT [FK_Procedures_ProtectedZones]       FOREIGN KEY ([ProtectedZoneId])    REFERENCES [dbo].[ProtectedZones] (ProtectedZoneId),
    CONSTRAINT [CHK_Procedures_NutsLevel]           CHECK       ([NutsLevel] IN (1, 2, 3, 4, 5, 6, 7));