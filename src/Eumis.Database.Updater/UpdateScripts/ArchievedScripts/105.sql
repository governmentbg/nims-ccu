GO

ALTER TABLE [dbo].[Contracts] ADD
    [CompanyNameAlt] NVARCHAR(200) NULL;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10004' as b
)
UPDATE c SET
    CompanyNameAlt = x.Xml.value('(/BFPContract/c:Beneficiary/b:NameEN)[1]', 'NVARCHAR(200)')
FROM ContractVersionXmls x
JOIN Contracts c ON x.ContractId = c.ContractId
WHERE c.ContractStatus = 2 and x.Status = 3
GO
