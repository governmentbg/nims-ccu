--executed on 2018/10/09 ~ 10:40

GO
UPDATE [dbo].[FlatFinancialCorrections]
SET
    Status = 1,
	ModifyDate = GETDATE()
WHERE
	OrderNum in (5,4)

GO