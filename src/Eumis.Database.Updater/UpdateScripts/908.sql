ALTER TABLE [dbo].[ProcedureSpecFields] DROP CONSTRAINT [CHK_ProcedureSpecFields_MaxLength];
GO

ALTER TABLE [dbo].[ProcedureSpecFields] ADD CONSTRAINT [CHK_ProcedureSpecFields_MaxLength] CHECK ([MaxLength] IN (1000, 3000, 5000, 10000, 0));
GO
