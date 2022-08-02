GO

ALTER TABLE [dbo].[CheckSheets] DROP CONSTRAINT [CHK_CheckSheets_Status]
GO
ALTER TABLE [dbo].[CheckSheets] WITH CHECK ADD CONSTRAINT [CHK_CheckSheets_Status] CHECK ([Status] IN (1, 2, 3, 4, 5))
GO

ALTER TABLE [dbo].[CheckSheetActionLogs] DROP CONSTRAINT [CHK_CheckSheetActionLogs_Action]
GO
ALTER TABLE [dbo].[CheckSheetActionLogs] WITH CHECK ADD CONSTRAINT [CHK_CheckSheetActionLogs_Action] CHECK ([Action] IN (1, 2, 3, 4, 5, 6))
GO

ALTER TABLE [dbo].[CheckSheetActionLogs] ADD [Reason] NVARCHAR(MAX);
GO
