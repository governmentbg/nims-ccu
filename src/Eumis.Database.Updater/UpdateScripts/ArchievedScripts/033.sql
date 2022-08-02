ALTER TABLE [Indicators] ALTER COLUMN [Code] NVARCHAR (20) NOT NULL
GO

ALTER TABLE [Indicators] DROP CONSTRAINT [UQ_Indicators_Code_Type_Kind]
GO

ALTER TABLE [Indicators] ADD CONSTRAINT [UQ_Indicators_Code_Type_Kind] UNIQUE ([Code], [Type], [Kind], [ProgrammeId])
GO

