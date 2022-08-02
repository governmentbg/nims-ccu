WITH
    -- required for the correct default namespace of the element <ProcedureApplicationFormType/>
    XMLNAMESPACES (DEFAULT N'http://ereg.egov.bg/segment/R-10031'),

    ApplicationFormTypes (ContractId, ProcedureApplicationFormType)
AS
(
    SELECT
        c.ContractId,
        -- select the child nodes of the Project's ApplicationFormType and wrap them in a <ProcedureApplicationFormType/>
        (SELECT
            rpx.Xml.query('(/*:Project/*:ProjectBasicData/*:ApplicationFormType)[1]/*')
        FOR XML PATH ('ProcedureApplicationFormType'), TYPE
        ) as ProcedureApplicationFormType
    FROM Contracts c
    JOIN RegProjectXmls rpx ON c.ProjectId = rpx.ProjectId
)
UPDATE cvx
SET
    Xml.modify('insert sql:column("ProcedureApplicationFormType") as last into (/*:BFPContract/*:BFPContractBasicData)[1]')
FROM ContractVersionXmls cvx
JOIN ApplicationFormTypes aft ON cvx.ContractId = aft.ContractId
