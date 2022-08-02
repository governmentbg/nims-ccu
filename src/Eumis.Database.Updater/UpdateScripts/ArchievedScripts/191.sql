EXEC sp_RENAME 'SapPaidAmounts.PaidBfpBgAmmount' , 'PaidBfpBgAmount', 'COLUMN';
EXEC sp_RENAME 'SapPaidAmounts.PaidBfpEuAmmount' , 'PaidBfpEuAmount', 'COLUMN';

ALTER TABLE [dbo].[SapPaidAmounts]
DROP
    CONSTRAINT [CHK_SapPaidAmounts_FinanceSource],
    COLUMN [FinanceSource];
GO
