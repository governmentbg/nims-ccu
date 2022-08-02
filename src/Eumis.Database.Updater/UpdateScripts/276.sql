GO

ALTER TABLE [dbo].[ProgrammeApplicationDocuments] ALTER COLUMN [Name] NVARCHAR(500) NOT NULL;
ALTER TABLE [dbo].[ProgrammeApplicationDocuments] ALTER COLUMN [Extension] NVARCHAR(100) NULL;
GO

ALTER TABLE [dbo].[ProcedureApplicationDocs] ALTER COLUMN [Name] NVARCHAR(500) NOT NULL;
ALTER TABLE [dbo].[ProcedureApplicationDocs] ALTER COLUMN [Extension] NVARCHAR(100) NULL;
GO
