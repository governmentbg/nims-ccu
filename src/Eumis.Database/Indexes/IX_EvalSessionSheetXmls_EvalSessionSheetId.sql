CREATE NONCLUSTERED INDEX [IX_EvalSessionSheetXmls_EvalSessionSheetId]
ON [dbo].[EvalSessionSheetXmls] ([EvalSessionSheetId])
INCLUDE ([Gid],[EvalType],[EvalIsPassed],[EvalPoints],[EvalNote])

GO
