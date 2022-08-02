ALTER TABLE [dbo].[PublicDiscussionGuidelines]
    ADD [StatusDate] DATETIME2 NOT NULL CONSTRAINT [DEFAULT_StatusDate] DEFAULT GETDATE()
GO

ALTER TABLE [dbo].[PublicDiscussionGuidelines] DROP CONSTRAINT [DEFAULT_StatusDate]
GO

ALTER TABLE [dbo].[Procedures]
    ADD [ActivationDate] DATETIME2 NULL
GO

UPDATE  p
SET     p.[ActivationDate] = pv.[CreateDate]
FROM    [dbo].Procedures AS p
        JOIN [dbo].[ProcedureVersions] AS pv
        ON p.[ProcedureId] = pv.[ProcedureId]
WHERE  pv.ProcedureVersionId = 1
GO
