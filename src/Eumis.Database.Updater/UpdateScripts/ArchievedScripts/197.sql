ALTER TABLE [dbo].[Procedures] DROP CONSTRAINT [CHK_Procedures_ApplicationFormType];
GO

ALTER TABLE [dbo].[Procedures] ADD CONSTRAINT [CHK_Procedures_ApplicationFormType] CHECK ([ApplicationFormType] IN (1, 2, 3, 4, 5, 6, 7));
GO
