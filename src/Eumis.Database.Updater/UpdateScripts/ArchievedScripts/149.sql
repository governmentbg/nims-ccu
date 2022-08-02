GO

IF EXISTS (SELECT [PrognosisId] FROM [dbo].[Prognoses])
BEGIN
    THROW 50000,'Cannot update database. There must be no Prognoses',1;
END
GO

ALTER TABLE [dbo].[Prognoses]
DROP CONSTRAINT [CHK_Prognoses_Quarter];
GO

DROP INDEX [UQ_Prognoses_LevelProgramme] ON [Prognoses]
DROP INDEX [UQ_Prognoses_LevelProgrammePriority] ON [Prognoses]
DROP INDEX [UQ_Prognoses_LevelProcedure] ON [Prognoses]

ALTER TABLE [dbo].[Prognoses]
DROP COLUMN [Quarter];
GO

ALTER TABLE [dbo].[Prognoses] ADD
    [Month]          INT           NOT NULL CONSTRAINT DEFAULT_Month DEFAULT 0,
    CONSTRAINT [CHK_Prognoses_Month]                  CHECK       ([Month] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
GO

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProgramme]
ON [Prognoses]([ProgrammeId], [Year], [Month], [FinanceSource])
WHERE [Level] = 1;

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProgrammePriority]
ON [Prognoses]([ProgrammePriorityId], [Year], [Month], [FinanceSource])
WHERE [Level] = 2;

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProcedure]
ON [Prognoses]([ProcedureId], [Year], [Month], [FinanceSource])
WHERE [Level] = 3;

ALTER TABLE [dbo].[Prognoses]
DROP CONSTRAINT DEFAULT_Month;
GO