ALTER TABLE [dbo].[SapPaidAmounts] ADD
    [PaidBfpBgAmmount]          MONEY            NOT NULL CONSTRAINT [DEFAULT_PaidBfpBgAmmount] DEFAULT 0,
    [PaidBfpEuAmmount]          MONEY            NOT NULL CONSTRAINT [DEFAULT_PaidBfpEuAmmount] DEFAULT 0,
    [HasWarning]                BIT             NOT NULL CONSTRAINT [DEFAULT_HasWarning] DEFAULT 0,
    [Warnings]                  NVARCHAR(MAX)    NULL;
GO

ALTER TABLE [dbo].[SapPaidAmounts]
DROP
    COLUMN     [PaidAmount],
    CONSTRAINT [DEFAULT_PaidBfpBgAmmount],
    CONSTRAINT [DEFAULT_PaidBfpEuAmmount],
    CONSTRAINT [DEFAULT_HasWarning];
GO

GO
