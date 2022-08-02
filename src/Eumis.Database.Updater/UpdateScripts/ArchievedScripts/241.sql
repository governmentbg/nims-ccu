GO

ALTER TABLE [dbo].[Indicators] DROP CONSTRAINT IF EXISTS [CHK_Indicators_ReportingType]
GO

ALTER TABLE [dbo].[Indicators] WITH CHECK ADD CONSTRAINT [CHK_Indicators_ReportingType] CHECK ([ReportingType] IN (1, 2, 3))
GO
