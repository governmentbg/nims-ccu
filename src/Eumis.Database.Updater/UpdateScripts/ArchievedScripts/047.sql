GO

ALTER TABLE [Indicators] DROP COLUMN [Order];
GO

ALTER TABLE [Indicators] ADD [ReportingType] INT NULL;
GO

UPDATE [Indicators] SET [ReportingType]=1;
GO

ALTER TABLE [Indicators] ALTER COLUMN [ReportingType] INT NOT NULL;
GO
