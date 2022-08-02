GO

ALTER TABLE [Procedures]
ADD [LocalActionGroupId] INT NULL,
CONSTRAINT [FK_Procedures_LocalActionGroups]            FOREIGN KEY ([LocalActionGroupId])            REFERENCES [dbo].[Companies] ([CompanyId]);

GO

ALTER TABLE [Companies] ADD [IsLocalActionGroup] BIT NOT NULL DEFAULT(0);
GO

ALTER TABLE [dbo].[EvalSessions] DROP CONSTRAINT [CHK_EvalSessions_Status]
GO
ALTER TABLE [dbo].[EvalSessions] WITH CHECK ADD CONSTRAINT [CHK_EvalSessions_Status] CHECK ([EvalSessionStatus] IN (1, 2, 3, 4, 5))
GO

ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [CHK_Projects_EvalStatus]
GO
ALTER TABLE [dbo].[Projects] WITH CHECK ADD CONSTRAINT [CHK_Projects_EvalStatus] CHECK ([EvalStatus] IN (1, 2, 3, 4))
GO
