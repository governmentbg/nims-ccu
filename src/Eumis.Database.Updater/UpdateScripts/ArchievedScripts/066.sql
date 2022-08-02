GO

ALTER TABLE [dbo].[EvalSessionSheets] ADD 
    [CreateDate]                    DATETIME2       NOT NULL DEFAULT GETDATE(),
    [Notes]                         NVARCHAR(MAX)   NULL
GO
