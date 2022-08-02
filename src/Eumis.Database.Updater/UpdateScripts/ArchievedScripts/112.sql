GO

ALTER TABLE [dbo].[Contracts]
ADD [TotalBfpAmount] MONEY  NULL,
    [TotalAmount]    MONEY  NULL;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as cdb,
    N'http://ereg.egov.bg/segment/R-10036' as cb
)
UPDATE c
SET
    [TotalBfpAmount] = x.Xml.value('(/BFPContract/cdb:BFPContractDimensionBudgetContract/cdb:BFPContractBudget/cb:GrandAmount)[1]', 'MONEY'),
    [TotalAmount] = x.Xml.value('(/BFPContract/cdb:BFPContractDimensionBudgetContract/cdb:BFPContractBudget/cb:TotalAmount)[1]', 'MONEY')
FROM ContractVersionXmls x
JOIN Contracts c ON x.ContractId = c.ContractId
WHERE c.ContractStatus = 2 and x.Status = 3
GO
