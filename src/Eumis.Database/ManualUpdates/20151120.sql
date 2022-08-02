--executed on 2015/11/20 ~ 12:30
UPDATE [Eumis].[dbo].[RegProjectXmls]
SET [RegistrationId] = 9751
WHERE [RegProjectXmlId] = 6448 and [Gid] = 'CB75204D-ED61-43B9-A810-0D77A271D9AE'


UPDATE [Eumis].[dbo].[ProjectVersionXmls]
SET [Xml].modify('
declare namespace c="http://ereg.egov.bg/segment/R-10019";
declare namespace e="http://ereg.egov.bg/segment/R-10004";
replace value of (//Project/c:Candidate/e:Email/text())[1]
    with "space.design.bg@gmail.com"')
WHERE [ProjectVersionXmlId] = 6223 AND
      [Gid] = '83501BB7-4C62-44BC-AD84-4DEC0C14D8EA' AND
      [ProjectId] IN
            (SELECT p.[ProjectId]
             FROM [Eumis].[dbo].[Projects] p
             WHERE p.[CompanyUin] = '200936485' AND p.[CompanyUinType] = 0);