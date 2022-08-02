GO

CREATE VIEW vwMonitoringMapNodeIndicators WITH SCHEMABINDING
AS

SELECT mni.[MapNodeId],
       mni.[IndicatorId],
       mnr.[ProgrammeId],
       i.[Type],
       i.[Name],
       mni.[BaseTotalValue],
       mni.[TargetTotalValue]
FROM [dbo].[MapNodeIndicators] mni
JOIN [dbo].[MapNodeRelations] mnr ON mni.[MapNodeId] = mnr.[MapNodeId]
JOIN [dbo].[Indicators] i ON mni.[IndicatorId] = i.[IndicatorId]
GO

GRANT SELECT ON vwMonitoringMapNodeIndicators TO PUBLIC
GO

CREATE UNIQUE CLUSTERED INDEX [vwMonitoringMapNodeIndicators_PK]
 ON [dbo].[vwMonitoringMapNodeIndicators] ([MapNodeId], [IndicatorId])