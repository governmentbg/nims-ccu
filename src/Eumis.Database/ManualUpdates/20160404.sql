--executed on 2016/04/06 ~ 15:20

UPDATE updatepcha
SET
    updatepcha.[ApprovedEuAmount] = amcalc.[ApprovedEuAmount],
    updatepcha.[ApprovedBgAmount] = amcalc.[ApprovedBgAmount],
    updatepcha.[ApprovedBfpTotalAmount] = amcalc.[ApprovedBfpTotalAmount],
    updatepcha.[ApprovedCrossAmount] = amcalc.[ApprovedCrossAmount],
    updatepcha.[ApprovedTotalAmount] = amcalc.[ApprovedTotalAmount]
FROM [dbo].[ContractReportPaymentCheckAmounts] updatepcha
JOIN
(
SELECT
    pcha.[ContractReportPaymentCheckAmountId],
    cr.[ContractReportId],
    crp.[ContractReportPaymentId],
    pcha.[ProgrammePriorityId],
    pcha.[FinanceSource],
    pcha.[ApprovedEuAmount],
    pcha.[ApprovedBgAmount],
    pcha.[ApprovedBfpTotalAmount],
    pcha.[ApprovedCrossAmount],
    pcha.[ApprovedSelfAmount],
    pcha.[ApprovedTotalAmount]
FROM dbo.[ContractReportPaymentChecks] pch
    JOIN [dbo].[ContractReports] cr on pch.[ContractReportId] = cr.[ContractReportId]
    JOIN [dbo].[ContractReportPayments] crp on pch.[ContractReportPaymentId] = crp.[ContractReportPaymentId]
    JOIN [dbo].[ContractReportPaymentCheckAmounts] pcha on pch.[ContractReportPaymentCheckId] = pcha.[ContractReportPaymentCheckId]
WHERE
    crp.[PaymentType] in (3,4) and pch.[Status] in (1,2)
) am ON updatepcha.[ContractReportPaymentCheckAmountId] = am.[ContractReportPaymentCheckAmountId]
JOIN
(
SELECT
    bi.[ContractReportId],
    crp.[ContractReportPaymentId],
    ps.[ProgrammePriorityId],
    ps.[FinanceSource],
    SUM(CASE WHEN bi.[AdvancePayment] = 2 THEN bi.[ApprovedEuAmount] ELSE 0 END) AS [ApprovedEuAmount],
    SUM(CASE WHEN bi.[AdvancePayment] = 2 THEN bi.[ApprovedBgAmount] ELSE 0 END) AS [ApprovedBgAmount],
    SUM(CASE WHEN bi.[AdvancePayment] = 2 THEN bi.[ApprovedBfpTotalAmount] ELSE 0 END) AS [ApprovedBfpTotalAmount],
    SUM(CASE WHEN bi.[AdvancePayment] = 2 AND bi.[CrossFinancing] = 1 THEN [ApprovedBfpTotalAmount] ELSE 0 END) AS [ApprovedCrossAmount],
    SUM(CASE WHEN bi.[AdvancePayment] = 2 THEN bi.[ApprovedSelfAmount] ELSE 0 END) AS [ApprovedSelfAmount],
    SUM(CASE WHEN bi.[AdvancePayment] = 2 THEN bi.[ApprovedTotalAmount] ELSE 0 END) AS [ApprovedTotalAmount]
FROM [dbo].[ContractReportFinancialCSDBudgetItems] bi                           
    JOIN [dbo].[ContractReportFinancialCSDs] csd ON bi.[ContractReportFinancialCSDId] = csd.[ContractReportFinancialCSDId]
    JOIN [dbo].[ContractBudgetLevel3Amounts] cbl3a ON bi.[ContractBudgetLevel3AmountId] = cbl3a.[ContractBudgetLevel3AmountId]
    JOIN [dbo].[ProcedureBudgetLevel2] pl2 ON cbl3a.[ProcedureBudgetLevel2Id] = pl2.[ProcedureBudgetLevel2Id]
    JOIN [dbo].[ProcedureShares] ps ON pl2.[ProcedureShareId] = ps.[ProcedureShareId]
    JOIN [dbo].[ContractReports] cr ON bi.[ContractReportId] = cr.[ContractReportId]
    JOIN [dbo].[ContractReportPayments] crp ON bi.[ContractReportId] = crp.[ContractReportId]
    JOIN [dbo].[ContractReportPaymentChecks] pch ON pch.[ContractReportPaymentId] = crp.[ContractReportPaymentId]
WHERE
    crp.[PaymentType] in (3,4) and pch.[Status] in (1,2)
GROUP BY
    bi.[ContractReportId],
    crp.[ContractReportPaymentId],
    ps.[ProgrammePriorityId],
    ps.[FinanceSource]
) amcalc ON
    amcalc.[ContractReportId] = am.[ContractReportId] AND
    amcalc.[ContractReportPaymentId] = am.[ContractReportPaymentId] AND
    amcalc.[ProgrammePriorityId] = am.[ProgrammePriorityId] AND
    amcalc.[FinanceSource] = am.[FinanceSource]
WHERE
    am.[ApprovedEuAmount] <> amcalc.[ApprovedEuAmount] OR
    am.[ApprovedBgAmount] <> amcalc.[ApprovedBgAmount] OR
    am.[ApprovedBfpTotalAmount] <> amcalc.[ApprovedBfpTotalAmount] OR
    am.[ApprovedCrossAmount]<> amcalc.[ApprovedCrossAmount] OR
    am.[ApprovedSelfAmount]<> amcalc.[ApprovedSelfAmount] OR
    am.[ApprovedTotalAmount] <> amcalc.[ApprovedTotalAmount]

GO
