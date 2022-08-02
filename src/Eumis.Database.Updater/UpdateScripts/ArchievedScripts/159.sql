GO

DROP INDEX [UQ_IrregularitySignals_Number] ON [dbo].[IrregularitySignals]
GO

UPDATE irs
SET irs.[Number] = calc.[NewNumber],
	irs.[RegNumber] = CAST(calc.[NewNumber] AS NVARCHAR(MAX))
FROM [dbo].[IrregularitySignals] irs
JOIN (SELECT [IrregularitySignalId], [ProgrammeId], [Number], ROW_NUMBER() OVER(PARTITION BY [ProgrammeId] ORDER BY [Number]) AS [NewNumber]
	  FROM [dbo].[IrregularitySignals]
	  WHERE [Number] IS NOT NULL) calc on irs.[IrregularitySignalId] = calc.[IrregularitySignalId]

GO

DELETE FROM [dbo].[Counters]
WHERE [Name] = 'irregularity-signal'
GO

INSERT INTO [dbo].[Counters]
SELECT 'irregularity-signal-for-programme#' + CAST([ProgrammeId] AS NVARCHAR(MAX)), MAX([Number]) FROM [dbo].[IrregularitySignals]
GROUP BY [ProgrammeId]

