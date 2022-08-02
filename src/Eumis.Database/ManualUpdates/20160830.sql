--executed on 2016/08/30 ~ 14:30

UPDATE [Eumis].[dbo].[ContractVersionXmls]
SET [Xml].modify('
	declare namespace c="http://ereg.egov.bg/segment/R-10040";
	declare namespace p="http://ereg.egov.bg/segment/R-10004";
	declare namespace s="http://ereg.egov.bg/segment/R-10003";
	declare namespace sa="http://ereg.egov.bg/segment/R-10001";
	replace value of (//BFPContract/c:Partners/c:Partner/p:Seat/s:Settlement/sa:Code/text())[1] with "07079"')
WHERE [ContractId] = 2096

UPDATE [Eumis].[dbo].[ContractVersionXmls]
SET [Xml].modify('
	declare namespace c="http://ereg.egov.bg/segment/R-10040";
	declare namespace p="http://ereg.egov.bg/segment/R-10004";
	declare namespace s="http://ereg.egov.bg/segment/R-10003";
	declare namespace sa="http://ereg.egov.bg/segment/R-10001";
	replace value of (//BFPContract/c:Partners/c:Partner/p:Seat/s:Settlement/sa:Name/text())[1] with "гр.Бургас"')
WHERE [ContractId] = 2096

UPDATE [Eumis].[dbo].[ContractVersionXmls]
SET [Xml].modify('
	declare namespace c="http://ereg.egov.bg/segment/R-10040";
	declare namespace p="http://ereg.egov.bg/segment/R-10004";
	declare namespace s="http://ereg.egov.bg/segment/R-10003";
	declare namespace sa="http://ereg.egov.bg/segment/R-10001";
	replace value of (//BFPContract/c:Partners/c:Partner/p:Correspondence/s:Settlement/sa:Code/text())[1] with "07079"')
WHERE [ContractId] = 2096

UPDATE [Eumis].[dbo].[ContractVersionXmls]
SET [Xml].modify('
	declare namespace c="http://ereg.egov.bg/segment/R-10040";
	declare namespace p="http://ereg.egov.bg/segment/R-10004";
	declare namespace s="http://ereg.egov.bg/segment/R-10003";
	declare namespace sa="http://ereg.egov.bg/segment/R-10001";
	replace value of (//BFPContract/c:Partners/c:Partner/p:Correspondence/s:Settlement/sa:Name/text())[1] with "гр.Бургас"')
WHERE [ContractId] = 2096
GO