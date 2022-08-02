GO

ALTER TABLE [dbo].[Contracts] ADD
    [NameEN]                         NVARCHAR(MAX)     NULL;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd
)
UPDATE c SET
    NameEN = x.Xml.value('(/BFPContract/c:BFPContractBasicData/cbd:NameEN)[1]', 'NVARCHAR(MAX)')
FROM ContractVersionXmls x
JOIN Contracts c ON x.ContractId = c.ContractId
WHERE c.ContractStatus = 2 and x.Status = 3
GO
