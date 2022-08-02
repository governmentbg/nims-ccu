GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [CHK_Projects_RegistrationStatus]
GO
ALTER TABLE [dbo].[Projects] WITH CHECK ADD CONSTRAINT [CHK_Projects_RegistrationStatus] CHECK ([RegistrationStatus] IN (1, 2, 3, 4))
GO
