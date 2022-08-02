GO

ALTER TABLE [dbo].[ContractRegistrations]
ADD [UinType]                 INT                 NOT NULL CONSTRAINT DEFAULT_UinType DEFAULT 1,
CONSTRAINT [CHK_ContractRegistrations_UinType]             CHECK      ([UinType] IN (1, 2));
GO

ALTER TABLE [dbo].[ContractRegistrations]
DROP CONSTRAINT DEFAULT_UinType
GO