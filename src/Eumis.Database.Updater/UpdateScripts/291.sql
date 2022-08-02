GO

ALTER TABLE [dbo].[EvalSessionDocuments] ADD [IsDeleted] BIT NOT NULL DEFAULT(0);
ALTER TABLE [dbo].[EvalSessionDocuments] ADD [IsDeletedNote] NVARCHAR(MAX) NULL;
GO
