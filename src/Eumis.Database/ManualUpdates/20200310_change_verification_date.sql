--executed on 2020/03/10 ~ 15:00

GO
UPDATE [dbo].[ContractReports] SET
    [CheckedDate] = '2020-01-15'
WHERE
    [ContractReportId] = 36761
    AND [ContractId] = 6896
GO

UPDATE [dbo].[ContractReportPaymentChecks] SET
    [CheckedDate] = '2020-01-15'
WHERE
    [ContractReportPaymentCheckId] = 42051
    AND [ContractId] = 6896
GO
