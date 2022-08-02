GO

ALTER TABLE [dbo].[ContractCommunicationXmls]
    ADD
    [Type]   INT     NOT NULL CONSTRAINT DEFAULT_Type DEFAULT 1,
    CONSTRAINT [CHK_ContractCommunicationXmls_Type]  CHECK ([Type]   IN (1, 2, 3));
GO

ALTER TABLE [dbo].[ContractCommunicationXmls]
DROP
  CONSTRAINT DEFAULT_Type
GO