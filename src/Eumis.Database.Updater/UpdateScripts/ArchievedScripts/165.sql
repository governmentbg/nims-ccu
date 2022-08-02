GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10041' as p,
    N'http://ereg.egov.bg/segment/R-10048' as pp
),
ContractProcurementPlanValues as
(
    SELECT
        s.value('(@gid)[1]', 'NVARCHAR(MAX)') as ContractProcurementPlanGid,
        s.value('(pp:OffersDeadlineDate)[1]', 'DATETIME2') as OffersDeadlineDate
    FROM ContractProcurementXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    OUTER APPLY x.Xml.nodes('(/Procurements/p:ProcurementPlans/p:ProcurementPlan)') a(s)
    WHERE c.ContractStatus = 2 and x.Status = 3
)
UPDATE pp SET
    OffersDeadlineDate = ppv.OffersDeadlineDate
FROM ContractProcurementPlans pp
JOIN ContractProcurementPlanValues ppv ON pp.Gid = ppv.ContractProcurementPlanGid
GO