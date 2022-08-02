--executed on 2019/01/23 ~ 17:45

--Current values
--TargetTotalValue=14.060;
--ApprovedResidueAmountTotal=14.060;

BEGIN TRANSACTION;
GO

UPDATE dbo.ContractIndicators
SET 
    [TargetTotalValue] =  14.07
WHERE 
    ContractIndicatorId = 5155
GO

UPDATE dbo.ContractReportIndicators
SET 
    [ApprovedResidueAmountTotal] = 14.07
WHERE 
    ContractIndicatorId = 5155
GO

COMMIT TRANSACTION;
GO
