-- cc3b3fc Remove ReleaseYear from ProcedureNumbers table.

GO
ALTER TABLE [dbo].[ProcedureNumbers] DROP CONSTRAINT [UQ_ProcedureNumbers_ProgrammePriorityId_Number];
ALTER TABLE [dbo].[ProcedureNumbers] DROP COLUMN [ReleaseYear];
ALTER TABLE [dbo].[ProcedureNumbers] ADD CONSTRAINT [UQ_ProcedureNumbers_ProgrammePriorityId_Number] UNIQUE ([ProgrammePriorityId], [Number]);
