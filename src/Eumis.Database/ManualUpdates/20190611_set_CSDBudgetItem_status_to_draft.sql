--executed on 2019/06/11 ~ 09:00

UPDATE ContractReportFinancialCSDBudgetItems SET
    Status = 1,
    CheckedByUserId = NULL,
    CheckedDate = NULL
WHERE 
    ContractReportId = 30004
    AND ContractReportFinancialCSDBudgetItemId IN (863420, 863424)
GO
