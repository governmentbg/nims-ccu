GO
ALTER TABLE [dbo].[Contracts] ADD 
	[RegistrationType] INT NOT NULL,
CONSTRAINT [DFLT_Contracts_RegistrationType] DEFAULT 3 FOR [RegistrationType];
GO
ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT DFLT_Contracts_RegistrationType;
GO
ALTER TABLE [dbo].[Contracts] ADD CONSTRAINT [CHK_Contracts_RegistrationType] CHECK ([RegistrationType] IN (1, 2, 3));
GO