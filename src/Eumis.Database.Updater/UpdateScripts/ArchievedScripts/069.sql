GO

ALTER TABLE [dbo].[ProcedureEvalTables] ADD
    [Status] INT NOT NULL CONSTRAINT DEFAULT_Status DEFAULT 2;
GO

ALTER TABLE [dbo].[ProcedureEvalTables] ADD
    CONSTRAINT [CHK_ProcedureEvalTables_Status] CHECK ([Status] IN (1, 2));
GO

ALTER TABLE [dbo].[ProcedureEvalTables]
DROP
  CONSTRAINT DEFAULT_Status
GO

ALTER TABLE [dbo].[ProcedureEvalTableXmls] ADD
    [ProcedureId] INT NOT NULL CONSTRAINT DEFAULT_ProcedureId DEFAULT 0;
GO

UPDATE [dbo].[ProcedureEvalTableXmls] SET [ProcedureId] = 
    (SELECT ProcedureId FROM [dbo].[ProcedureEvalTables]
    WHERE [ProcedureEvalTableXmls].ProcedureEvalTableId = [ProcedureEvalTables].ProcedureEvalTableId)
WHERE [ProcedureId] = 0
GO

ALTER TABLE [dbo].[ProcedureEvalTableXmls] ADD
    CONSTRAINT [FK_ProcedureEvalTableXmls_Procedures] FOREIGN KEY ([ProcedureId]) REFERENCES [dbo].[Procedures] ([ProcedureId]);
GO

ALTER TABLE [dbo].[ProcedureEvalTableXmls]
DROP
  CONSTRAINT DEFAULT_ProcedureId
GO
