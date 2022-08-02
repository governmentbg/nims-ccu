--executed on 2018/11/02 ~ 17:20

GO
UPDATE [dbo].[Contracts]
SET
    ContractType = 4,
	ModifyDate = GETDATE()
WHERE
	RegNumber = 'BG16RFSM001-1.001-0001-C01'

GO