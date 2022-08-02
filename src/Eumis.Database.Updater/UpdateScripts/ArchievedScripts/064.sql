GO

ALTER TABLE [dbo].[ProcedureShares] ADD 
    [IsActivated]               BIT             NOT NULL CONSTRAINT DEFAULT_IsActivated DEFAULT 1
GO

ALTER TABLE [dbo].[ProcedureShares] DROP 
    CONSTRAINT DEFAULT_IsActivated;
GO

UPDATE ps SET IsActivated = 0
FROM [dbo].[ProcedureShares] ps
JOIN [dbo].[Procedures] p on ps.ProcedureId = p.ProcedureId
WHERE ProcedureStatus = 1 --draft
GO
