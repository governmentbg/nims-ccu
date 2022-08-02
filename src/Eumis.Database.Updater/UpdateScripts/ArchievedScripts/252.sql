ALTER TABLE [dbo].[ProjectCommunications] DROP CONSTRAINT [CHK_ProjectCommunications_Status]
GO

ALTER TABLE [dbo].[ProjectCommunications] WITH CHECK ADD CONSTRAINT [CHK_ProjectCommunications_Status] CHECK ([Status] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO
