GO

ALTER TABLE [dbo].[ProjectCommunications] DROP CONSTRAINT [CHK_ProjectCommunications_Subject]
GO
ALTER TABLE [dbo].[ProjectCommunications] WITH CHECK ADD CONSTRAINT [CHK_ProjectCommunications_Subject] CHECK ([Subject]   IN (1, 2, 3, 4, 5, 6, 7, 8))
GO
