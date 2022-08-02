ALTER TABLE [dbo].[IndicativeAnnualWorkingProgrammeTables] ADD
[ProcedureId] INT NULL
GO

UPDATE [dbo].[IndicativeAnnualWorkingProgrammeTables]
SET [ProcedureId] = p.ProcedureId
FROM [dbo].[IndicativeAnnualWorkingProgrammeTables] iawpt
INNER JOIN [dbo].[Procedures] p ON iawpt.ProcedureName = p.[Name]
INNER JOIN [dbo].[ProcedureIndicativeAnnualWorkingProgrammes] piawpt on p.ProcedureId = piawpt.ProcedureId;
GO


ALTER TABLE [IndicativeAnnualWorkingProgrammeTables]
ALTER COLUMN [ProcedureId] INT NOT NULL;
GO

UPDATE iawp
SET iawp.[OrderNum] = calc.[NewOrderNum]
FROM [dbo].[IndicativeAnnualWorkingProgrammeTables] iawp
JOIN(SELECT [IndicativeAnnualWorkingProgrammeId], ROW_NUMBER() OVER(PARTITION BY [IndicativeAnnualWorkingProgrammeId] ORDER BY [IndicativeAnnualWorkingProgrammeTableId]) AS [NewOrderNum]
FROM [dbo].[IndicativeAnnualWorkingProgrammeTables]) calc ON iawp.[IndicativeAnnualWorkingProgrammeId] = calc.[IndicativeAnnualWorkingProgrammeId]
GO
