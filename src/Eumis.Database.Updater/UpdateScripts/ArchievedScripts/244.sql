GO
ALTER TABLE [dbo].[News] ADD
[Type]          INT             NULL,
[TitleAlt]      NVARCHAR(200)   NULL,
[ContentAlt]    NVARCHAR(MAX)   NULL
GO

GO
UPDATE [dbo].[News] SET [Type] = 1 WHERE [Type] IS NULL
GO

GO
ALTER TABLE [dbo].[News] ALTER COLUMN [Type] INT NOT NULL
GO

GO
ALTER TABLE [dbo].[News] WITH CHECK ADD CONSTRAINT [CHK_News_Type] CHECK ([Type] IN (1, 2))
GO
