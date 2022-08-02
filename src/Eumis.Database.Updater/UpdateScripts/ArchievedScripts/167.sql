GO

UPDATE pbl3
SET pbl3.[OrderNum] = calc.[NewOrderNum]
FROM [dbo].[ProcedureBudgetLevel3] pbl3
JOIN (SELECT [ProcedureBudgetLevel3Id], ROW_NUMBER() OVER(PARTITION BY [ProcedureBudgetLevel2Id] ORDER BY [ProcedureBudgetLevel3Id]) AS [NewOrderNum]
      FROM [dbo].[ProcedureBudgetLevel3]) calc on pbl3.[ProcedureBudgetLevel3Id] = calc.[ProcedureBudgetLevel3Id]

UPDATE pbl2
SET pbl2.[OrderNum] = calc.[NewOrderNum]
FROM [dbo].[ProcedureBudgetLevel2] pbl2
JOIN (SELECT [ProcedureBudgetLevel2Id], ROW_NUMBER() OVER(PARTITION BY [ProcedureBudgetLevel1Id] ORDER BY [ProcedureBudgetLevel2Id]) AS [NewOrderNum]
      FROM [dbo].[ProcedureBudgetLevel2]) calc on pbl2.[ProcedureBudgetLevel2Id] = calc.[ProcedureBudgetLevel2Id]

UPDATE pbl1
SET pbl1.[OrderNum] = calc.[NewOrderNum]
FROM [dbo].[ProcedureBudgetLevel1] pbl1
JOIN (SELECT [ProcedureBudgetLevel1Id], ROW_NUMBER() OVER(PARTITION BY [ProcedureId] ORDER BY [ProcedureBudgetLevel1Id]) AS [NewOrderNum]
      FROM [dbo].[ProcedureBudgetLevel1]) calc on pbl1.[ProcedureBudgetLevel1Id] = calc.[ProcedureBudgetLevel1Id]

GO