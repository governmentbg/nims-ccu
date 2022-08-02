PRINT 'Create View [UniqueProcedureBudgetSizeTypeKidCodeIndexed]'
GO

IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwUniqueProcedureBudgetSizeTypeKidCodeIndexed'))
DROP VIEW vwUniqueProcedureBudgetSizeTypeKidCodeIndexed
GO

CREATE VIEW vwUniqueProcedureBudgetSizeTypeKidCodeIndexed WITH SCHEMABINDING
AS

SELECT
    pbc.[ProcedureId],
    pbc.[ProcedureBudgetComponentId],
    bkc.[KidCodeId],
    bst.[CompanySizeTypeId],
    pbc.[Status]
FROM [dbo].[ProcedureBudgetComponents] pbc
JOIN [dbo].[ProcedureBudgetKidCodes] bkc on pbc.ProcedureBudgetComponentId = bkc.ProcedureBudgetComponentId
JOIN [dbo].[ProcedureBudgetSizeTypes] bst on pbc.ProcedureBudgetComponentId = bst.ProcedureBudgetComponentId  
WHERE pbc.[Status] < 3
GO

GRANT SELECT ON vwUniqueContractEmailAccessCodeIndexed TO PUBLIC
GO

CREATE UNIQUE CLUSTERED INDEX [vwUniqueProcedureBudgetSizeTypeKidCodeIndexed_PK]
 ON [dbo].vwUniqueProcedureBudgetSizeTypeKidCodeIndexed 
(
    [ProcedureId] ASC,
    [KidCodeId] ASC,
    [CompanySizeTypeId] ASC
)
