--executed on 2017/11/13 ~ 17:20

update ContractReportPayments
set Status = 2, SubmitDate = NULL
where
    ContractReportPaymentId = 13298

delete from ContractReportAdvanceNVPaymentAmounts
where
    ContractReportPaymentId = 13298

GO
