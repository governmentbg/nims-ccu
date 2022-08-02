ALTER TABLE [dbo].[EvalSessionDistributionProjects]
ALTER COLUMN [IsDeletedNote]          NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionDistributions]
ALTER COLUMN [StatusNote]             NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionDistributionUsers]
ALTER COLUMN [IsDeletedNote]          NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionEvaluations]
ALTER COLUMN [EvalNote]               NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionEvaluations]
ALTER COLUMN [IsDeletedNote]          NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionProjects]
ALTER COLUMN [IsDeletedNote]          NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionProjectStandings]
ALTER COLUMN [IsDeletedNote]          NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionProjectStandings]
ALTER COLUMN [Notes]                  NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionReports]
ALTER COLUMN [IsDeletedNote]          NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionSheets]
ALTER COLUMN [StatusNote]             NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionSheetXmls]
ALTER COLUMN [EvalNote]               NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[EvalSessionStandings]
ALTER COLUMN [StatusNote]             NVARCHAR(MAX)   NULL
GO

ALTER TABLE [dbo].[Procedures]
ALTER COLUMN [Name]                   NVARCHAR(MAX)   NOT NULL
GO

ALTER TABLE [dbo].[Procedures]
ALTER COLUMN [NameAlt]                NVARCHAR(MAX)   NULL
GO