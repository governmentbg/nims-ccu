GO

ALTER TABLE [dbo].[CheckSheetActionLogs] DROP CONSTRAINT [CHK_CheckSheetActionLogs_Action]
GO
ALTER TABLE [dbo].[CheckSheetActionLogs] WITH CHECK ADD CONSTRAINT [CHK_CheckSheetActionLogs_Action] CHECK ([Action] IN (1, 2, 3, 4, 5, 6, 7))
GO

ALTER TABLE [CheckSheetActionLogs]
ADD [NotifiedUserId] INT NULL,
CONSTRAINT [FK_CheckSheetActionLogs_NotifiedUsers]            FOREIGN KEY ([NotifiedUserId])          REFERENCES [dbo].[Users] ([UserId])

GO
