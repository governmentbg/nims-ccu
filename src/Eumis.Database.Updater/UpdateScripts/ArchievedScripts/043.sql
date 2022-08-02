GO

ALTER TABLE [dbo].[ProcedureEvalTables]
ADD
    [EvalType] INT NOT NULL DEFAULT(1),
    CONSTRAINT [CHK_ProcedureEvalTables_EvalType] CHECK ([EvalType] IN (1, 2))

GO

UPDATE [dbo].[ProcedureEvalTables]
SET [EvalType] = [Type]


GO
