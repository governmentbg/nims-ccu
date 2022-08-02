GO

UPDATE [dbo].[ContractReportPaymentCheckAmounts]
SET [PaidBfpTotalAmount] = [ApprovedBfpTotalAmount]
WHERE ([PaidEuAmount] + [PaidBgAmount]) != [PaidBfpTotalAmount]

GO

