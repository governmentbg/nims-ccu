--executed on 2016/11/03 ~ 19.00

DECLARE @currentStateNode xml;
SET @currentStateNode = N'<CurrentState xmlns="http://ereg.egov.bg/segment/R-10032">0</CurrentState>';

UPDATE [dbo].[ContractVersionXmls]
SET [Xml].modify('
declare namespace i2="http://ereg.egov.bg/segment/R-10040";
declare namespace pb="http://ereg.egov.bg/segment/R-10036";
declare namespace peb="http://ereg.egov.bg/segment/R-10035";
declare namespace pdeb="http://ereg.egov.bg/segment/R-10034";
declare namespace n="http://ereg.egov.bg/segment/R-10033";
declare namespace a="http://ereg.egov.bg/segment/R-10032";
insert sql:variable("@currentStateNode") as last
into (/BFPContract/i2:BFPContractDimensionBudgetContract[@id = "3f8a776a-9ba6-46cd-96d9-1bd155e3910b"]/i2:BFPContractBudget/pb:BFPContractProgrammeBudget[@gid = "c739fcbf-bc32-4c66-bca9-d5dc48b2acae"]/peb:BFPContractProgrammeExpenseBudget[@gid = "93576e0b-b667-4d0f-9a23-2b032f2f004f"]/pdeb:BFPContractProgrammeDetailsExpenseBudget[@gid = "090e0332-7d09-4ced-a9bb-8e7fe5ec2bff"]/n:SelfAmounts)[1]')
WHERE ContractVersionXmlId = 3313
GO