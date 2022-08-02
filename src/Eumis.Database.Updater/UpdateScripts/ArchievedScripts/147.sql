GO

UPDATE fbi
    SET [BudgetDetailName] = [BudgetDetailName] + N' (фонд: ' +
        (CASE ps.FinanceSource
            WHEN 1 THEN 'ЕСФ'
            WHEN 2 THEN 'ЕФРР'
            WHEN 3 THEN 'КФ'
            WHEN 4 THEN 'ИМЗ'
            WHEN 5 THEN 'ФЕПНЛ'
            WHEN 6 THEN 'ЕФМДР'
            WHEN 7 THEN 'ЕЗФРСР'
            WHEN 8 THEN 'ФВС'
            WHEN 9 THEN 'ФУМИ'
            WHEN 10 THEN 'Други'
        END) +
        ', режим на финансиране: ' +
        (CASE pb2.AidMode
            WHEN 1 THEN 'de minimis'
            WHEN 2 THEN 'Държавна помощ'
            WHEN 3 THEN 'Неприложимо'
        END) +
        ', ' +
        (CASE pb2.IsEligibleCost
            WHEN 1 THEN 'допустим'
            ELSE 'недопустим'
        END) + ')'
    FROM [dbo].[ContractReportFinancialCSDBudgetItems] fbi
    JOIN [dbo].[ContractBudgetLevel3Amounts] cba ON fbi.[ContractBudgetLevel3AmountId] = cba.[ContractBudgetLevel3AmountId]
    JOIN [dbo].[ProcedureBudgetLevel2] pb2 ON cba.[ProcedureBudgetLevel2Id] = pb2.[ProcedureBudgetLevel2Id]
    JOIN [dbo].[ProcedureShares] ps ON pb2.[ProcedureShareId] = ps.[ProcedureShareId];
GO