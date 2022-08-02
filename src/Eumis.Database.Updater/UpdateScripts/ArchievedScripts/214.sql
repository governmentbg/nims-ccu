ALTER TABLE [dbo].[EvalSessionDistributions] DROP CONSTRAINT [CHK_EvalSessionDistributions_EvalTableType]
GO

ALTER TABLE [dbo].[EvalSessionDistributions] ADD CONSTRAINT [CHK_EvalSessionDistributions_EvalTableType] CHECK ([EvalTableType] IN (1, 2, 3, 4))
GO
