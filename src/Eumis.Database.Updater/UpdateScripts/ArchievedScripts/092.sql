GO

ALTER TABLE [dbo].[ContractContracts]
DROP COLUMN [TotalAmount],
     COLUMN [AmountFinancedProject];
GO

ALTER TABLE [dbo].[ContractContracts]
ADD [TotalAmountExcludingVAT]   MONEY               NOT NULL,
    [ContractAmountWithoutVAT]  MONEY               NOT NULL,
    [VATAmountIfEligible]       MONEY               NOT NULL,
    [TotalFundedValue]          MONEY               NOT NULL;
GO
