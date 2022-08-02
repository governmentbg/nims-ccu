GO
ALTER TABLE [dbo].[Procedures] ADD CONSTRAINT [CHK_Procedures_ProjectDuration] CHECK ([ProjectDuration] > 0)
