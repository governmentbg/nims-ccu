GO

--This script will remove the identity constraint from [dbo].[EvalSessionSheets] table (ESS) and
--add a new sequence that will be used to populate ids in [dbo].[EvalSessionSheets] instead of the identity constraint

--Drop constraints from tables that have FK to ESS
----EvalSessionSheetXmls
ALTER TABLE [dbo].[EvalSessionSheetXmls]
DROP
  CONSTRAINT [FK_EvalSessionSheetData_EvalSessionSheets]
GO



----EvalSessionEvaluationSheets
ALTER TABLE [dbo].[EvalSessionEvaluationSheets]
DROP
  CONSTRAINT [PK_EvalSessionEvaluationSheets]
GO

ALTER TABLE [dbo].[EvalSessionEvaluationSheets]
DROP
  CONSTRAINT [FK_EvalSessionEvaluationSheets_EvalSessionSheets]
GO



--Change ESS accordingly - add new column, copy identity columns values to the new column, remove PK constraint,
--remove old identity column, rename new column, add new PK constraint
----EvalSessionSheets
ALTER TABLE [dbo].[EvalSessionSheets] ADD
    [EvalSessionSheetIdNew] INT NOT NULL CONSTRAINT DEFAULT_EvalSessionSheets DEFAULT 0;
GO

ALTER TABLE [dbo].[EvalSessionSheets]
DROP
  CONSTRAINT DEFAULT_EvalSessionSheets
GO

UPDATE [dbo].[EvalSessionSheets] SET [EvalSessionSheetIdNew] = [EvalSessionSheetId]
GO

ALTER TABLE [dbo].[EvalSessionSheets]
DROP
  CONSTRAINT [FK_EvalSessionSheets_ContinuedEvalSessionSheets]
GO

ALTER TABLE [dbo].[EvalSessionSheets]
DROP
  CONSTRAINT [PK_EvalSessionSheets]
GO

ALTER TABLE [dbo].[EvalSessionSheets] DROP COLUMN [EvalSessionSheetId];
GO

EXEC sp_rename '[EvalSessionSheets].EvalSessionSheetIdNew', 'EvalSessionSheetId', 'COLUMN'
GO

ALTER TABLE [dbo].[EvalSessionSheets] ADD
    CONSTRAINT [PK_EvalSessionSheets]                            PRIMARY KEY ([EvalSessionId], [EvalSessionSheetId]);
GO

ALTER TABLE [dbo].[EvalSessionSheets] ADD
    CONSTRAINT [FK_EvalSessionSheets_ContinuedEvalSessionSheets] FOREIGN KEY ([EvalSessionId], [ContinuedEvalSessionSheetId]) REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId]);
GO

DECLARE @max int;
SELECT @max = MAX(EvalSessionSheetId) + 10 FROM EvalSessionSheets;

exec('CREATE SEQUENCE [dbo].[EvalSessionSheetSequence] START WITH ' + @max)


--Add new constraints in tables that have FK to ESS
----EvalSessionSheetXmls
ALTER TABLE [dbo].[EvalSessionSheetXmls] ADD
    CONSTRAINT [FK_EvalSessionSheetData_EvalSessionSheets]          FOREIGN KEY ([EvalSessionId], [EvalSessionSheetId])  REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId]);
GO




----EvalSessionEvaluationSheets
ALTER TABLE [dbo].[EvalSessionEvaluationSheets] ADD
    CONSTRAINT [PK_EvalSessionEvaluationSheets]                     PRIMARY KEY ([EvalSessionId], [EvalSessionEvaluationId], [EvalSessionSheetId]);
GO

ALTER TABLE [dbo].[EvalSessionEvaluationSheets] ADD
    CONSTRAINT [FK_EvalSessionEvaluationSheets_EvalSessionSheets]   FOREIGN KEY ([EvalSessionId], [EvalSessionSheetId])  REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId])
GO