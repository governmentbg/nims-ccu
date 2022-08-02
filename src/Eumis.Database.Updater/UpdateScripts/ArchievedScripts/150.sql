GO

ALTER TABLE [RegProjectXmls]
    ALTER COLUMN [ProjectName] NVARCHAR(MAX) NULL;
GO

UPDATE [dbo].[RegProjectXmls]
SET [ProjectName] = [Xml].value('declare namespace p="http://ereg.egov.bg/segment/R-10019";
                                 declare namespace bd="http://ereg.egov.bg/segment/R-10002";
                                 (//Project/p:ProjectBasicData/bd:Name)[1]', 'NVARCHAR(MAX)')
WHERE LEN([Xml].value('declare namespace p="http://ereg.egov.bg/segment/R-10019";
                       declare namespace bd="http://ereg.egov.bg/segment/R-10002";
                       (//Project/p:ProjectBasicData/bd:Name)[1]', 'NVARCHAR(MAX)')) > 190
GO

UPDATE p
SET p.[Name] = pv.[Xml].value('declare namespace p="http://ereg.egov.bg/segment/R-10019";
                               declare namespace bd="http://ereg.egov.bg/segment/R-10002";
                               (//Project/p:ProjectBasicData/bd:Name)[1]', 'NVARCHAR(MAX)')
  FROM [dbo].[Projects] p
  JOIN [dbo].[ProjectVersionXmls] pv on p.ProjectId = pv.ProjectId
  WHERE (pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]) AND NOT EXISTS (SELECT pv3.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv3 WHERE pv3.[ProjectId] = p.[ProjectId] AND pv3.[Status] = 2))) AND
        LEN(pv.[Xml].value('declare namespace p="http://ereg.egov.bg/segment/R-10019";
                            declare namespace bd="http://ereg.egov.bg/segment/R-10002";
                            (//Project/p:ProjectBasicData/bd:Name)[1]', 'NVARCHAR(MAX)')) > 190 AND
        pv.[Xml].value('declare namespace p="http://ereg.egov.bg/segment/R-10019";
                        declare namespace bd="http://ereg.egov.bg/segment/R-10002";
                        (//Project/p:ProjectBasicData/bd:Name)[1]', 'NVARCHAR(MAX)') != p.[Name]
GO