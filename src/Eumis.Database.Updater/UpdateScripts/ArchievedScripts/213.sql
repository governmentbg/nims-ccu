ALTER TABLE [dbo].[ProcedureEvalTables] DROP CONSTRAINT [CHK_ProcedureEvalTables_Type]
GO

ALTER TABLE [dbo].[ProcedureEvalTables] ADD CONSTRAINT [CHK_ProcedureEvalTables_Type] CHECK ([Type] IN (1, 2, 3, 4))
GO


ALTER TABLE [dbo].[EvalSessionSheets] DROP CONSTRAINT [CHK_EvalSessionSheets_EvalTableType]
GO

ALTER TABLE [dbo].[EvalSessionSheets] ADD CONSTRAINT [CHK_EvalSessionSheets_EvalTableType] CHECK ([EvalTableType] IN (1, 2, 3, 4))
GO


ALTER TABLE [dbo].[EvalSessionEvaluations] DROP CONSTRAINT [CHK_EvalSessionEvaluations_EvalTableType]
GO

ALTER TABLE [dbo].[EvalSessionEvaluations] ADD CONSTRAINT [CHK_EvalSessionEvaluations_EvalTableType] CHECK ([EvalTableType] IN (1, 2, 3, 4))
GO


ALTER TABLE [dbo].[EvalSessionProjectStandings] DROP CONSTRAINT [CHK_EvalSessionProjectStanding_Status]
GO

ALTER TABLE [dbo].[EvalSessionProjectStandings] ADD CONSTRAINT [CHK_EvalSessionProjectStanding_Status] CHECK ([Status] IN (1, 2, 3, 4, 5, 6))
GO

ALTER TABLE [dbo].[EvalSessionProjectStandings] ADD
    [IsPreliminary] BIT NOT NULL CONSTRAINT DEFAULT_EvalSessionProjectStandings_IsPreliminary DEFAULT 0;
GO

ALTER TABLE [dbo].[EvalSessionProjectStandings]
DROP
  CONSTRAINT DEFAULT_EvalSessionProjectStandings_IsPreliminary
GO

DROP INDEX [UQ_EvalSessionProjectStandings_EvalSessionId_OrderNum] ON [EvalSessionProjectStandings]

CREATE UNIQUE NONCLUSTERED INDEX [UQ_EvalSessionProjectStandings_EvalSessionId_IsPreliminary_OrderNum]
ON [EvalSessionProjectStandings]([EvalSessionId], [IsPreliminary], [OrderNum])
WHERE [OrderNum] IS NOT NULL AND [IsDeleted] = 0;


ALTER TABLE [dbo].[EvalSessionStandings] ADD
    [IsPreliminary]                 BIT NOT NULL CONSTRAINT DEFAULT_EvalSessionStandings_IsPreliminary DEFAULT 0,
    [PreliminaryBudgetPercentage]   INT NULL;

ALTER TABLE [dbo].[EvalSessionStandings]
DROP
  CONSTRAINT DEFAULT_EvalSessionStandings_IsPreliminary
GO
