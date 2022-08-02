GO

ALTER TABLE [dbo].[Procedures] ADD [AllowConcurrancyContractReports] BIT NOT NULL CONSTRAINT [DEFAULT_ConcurrancyContractReports] DEFAULT 0;
GO

ALTER TABLE [dbo].[Procedures] DROP CONSTRAINT [DEFAULT_ConcurrancyContractReports]
GO
