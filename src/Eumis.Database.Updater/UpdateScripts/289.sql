
GO
ALTER TABLE [dbo].[EvalSessionProjectStandings] ADD [ManualOrderNum] INT NULL;
ALTER TABLE [dbo].[EvalSessionProjectStandings] ADD [ManualStatus]   INT NOT NULL CONSTRAINT DEFAULT_ManualStatus DEFAULT 1;
ALTER TABLE [dbo].[EvalSessionProjectStandings] DROP CONSTRAINT DEFAULT_ManualStatus;

GO
ALTER TABLE [dbo].[EvalSessionStandings] DROP CONSTRAINT [CHK_EvalSessionStandings_Status];
ALTER TABLE [dbo].[EvalSessionStandings] WITH CHECK ADD CONSTRAINT [CHK_EvalSessionStandings_Status] CHECK ([Status] IN (1, 2, 3))

GO
UPDATE [dbo].[EvalSessionProjectStandings] Set [ManualOrderNum] = [OrderNum]
UPDATE [dbo].[EvalSessionProjectStandings] Set [ManualStatus] = [Status]
GO
