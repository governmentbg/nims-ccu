GO

EXEC sp_rename '[dbo].[Regions]', 'ProtectedZones';
EXEC sp_rename '[dbo].[ProtectedZones].[RegionId]'            , 'ProtectedZoneId'            , 'COLUMN';
