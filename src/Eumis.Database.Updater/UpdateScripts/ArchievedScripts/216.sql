WITH
    XMLNAMESPACES (DEFAULT N'http://ereg.egov.bg/segment/R-10078'),
    pta AS
    (
        SELECT p.ProcedureId, (SELECT pt.Alias AS ProcedureTypeAlias FOR XML PATH(''), TYPE) AS Alias
        FROM Procedures p
        JOIN ProcedureTypes pt ON p.ProcedureTypeId = pt.ProcedureTypeId
    )
UPDATE crf
SET
    Xml.modify('insert sql:column("Alias") as last into (/*:FinanceReport/*:BasicData)[1]')
FROM ContractReportFinancials crf
JOIN Contracts c ON crf.ContractId = c.ContractId
JOIN pta ON c.ProcedureId = pta.ProcedureId
