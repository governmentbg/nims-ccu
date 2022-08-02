--executed on 2015/11/20 ~ 11:30
DECLARE @uinUinType xml;
SET @uinUinType = N'
    <Uin xmlns="http://ereg.egov.bg/segment/R-10004">121015056</Uin>
    <UinType xmlns="http://ereg.egov.bg/segment/R-10004">
      <Id xmlns="http://ereg.egov.bg/segment/R-10000">bulstat</Id>
      <Name xmlns="http://ereg.egov.bg/segment/R-10000">Булстат</Name>
    </UinType>
    <Email xmlns="http://ereg.egov.bg/segment/R-10004">secretary@asp.government.bg</Email>';

UPDATE [dbo].[ProjectVersionXmls]
SET [Xml].modify('
declare namespace c="http://ereg.egov.bg/segment/R-10019";
insert sql:variable("@uinUinType") as first
into (//Project/c:Candidate)[1] ')
WHERE [ProjectVersionXmlId] = 2182 AND
      [Gid] = 'CEEDD82E-0AF7-4F71-B5D9-0F172B9290BF' AND
      [ProjectId] IN
            (SELECT p.[ProjectId]
             FROM [dbo].[Projects] p
             WHERE p.[CompanyUin] = '121015056' AND p.[CompanyUinType] = 1);
GO

DECLARE @uinUinType xml;
SET @uinUinType = N'
    <Uin xmlns="http://ereg.egov.bg/segment/R-10004">121604974</Uin>
    <UinType xmlns="http://ereg.egov.bg/segment/R-10004">
      <Id xmlns="http://ereg.egov.bg/segment/R-10000">bulstat</Id>
      <Name xmlns="http://ereg.egov.bg/segment/R-10000">Булстат</Name>
    </UinType>
    <Email xmlns="http://ereg.egov.bg/segment/R-10004">VGeorgieva@az.government.bg</Email>';

UPDATE [dbo].[ProjectVersionXmls]
SET [Xml].modify('
declare namespace c="http://ereg.egov.bg/segment/R-10019";
insert sql:variable("@uinUinType") as first
into (//Project/c:Candidate)[1] ')
WHERE [ProjectVersionXmlId] = 2186 AND
      [Gid] = '7DC998C9-8D8C-4737-B0DB-CC4FF758AFFF' AND
      [ProjectId] IN
            (SELECT p.[ProjectId]
             FROM [dbo].[Projects] p
             WHERE p.[CompanyUin] = '121604974' AND p.[CompanyUinType] = 1);
GO

DECLARE @uinUinType xml;
SET @uinUinType = N'
    <Uin xmlns="http://ereg.egov.bg/segment/R-10004">000695025</Uin>
    <UinType xmlns="http://ereg.egov.bg/segment/R-10004">
      <Id xmlns="http://ereg.egov.bg/segment/R-10000">bulstat</Id>
      <Name xmlns="http://ereg.egov.bg/segment/R-10000">Булстат</Name>
    </UinType>
    <Email xmlns="http://ereg.egov.bg/segment/R-10004">Rositsa.Ivanova@government.bg</Email>';

UPDATE [dbo].[ProjectVersionXmls]
SET [Xml].modify('
declare namespace c="http://ereg.egov.bg/segment/R-10019";
insert sql:variable("@uinUinType") as first
into (//Project/c:Candidate)[1] ')
WHERE [ProjectVersionXmlId] = 2253 AND
      [Gid] = '6726F39E-C72C-47EF-B30D-4FB2EA11113F' AND
      [ProjectId] IN
            (SELECT p.[ProjectId]
             FROM [dbo].[Projects] p
             WHERE p.[CompanyUin] = '000695025' AND p.[CompanyUinType] = 1);
GO