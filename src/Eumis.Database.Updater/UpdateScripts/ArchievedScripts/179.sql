GO

ALTER TABLE [dbo].[Projects] ADD
  [CompanyTypeId]                         INT                 NOT NULL  CONSTRAINT DEFAULT_CompanyType     DEFAULT 1,
  [NutsAddressFullPath]                   NVARCHAR(MAX)       NULL,
  CONSTRAINT [FK_Projects_CompanyType]            FOREIGN KEY ([CompanyTypeId])           REFERENCES [dbo].[CompanyTypes]      ([CompanyTypeId])
GO

EXEC sp_RENAME 'Projects.NutsAddress' , 'NutsAddressFullPathName', 'COLUMN';
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10019' as i2,
    N'http://ereg.egov.bg/segment/R-10004' as ct
)
UPDATE p
SET 
  [CompanyTypeId] = ct.[CompanyTypeId]
FROM [dbo].[Projects] p
JOIN [dbo].[ProjectVersionXmls] pv ON p.[ProjectId] = pv.[ProjectId]
JOIN [dbo].[CompanyTypes] ct ON pv.Xml.value('(/Project/i2:Candidate/ct:CompanyType)[1]', 'uniqueidentifier') = ct.[Gid]
WHERE pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]) AND NOT EXISTS (SELECT pv3.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv3 WHERE pv3.[ProjectId] = p.[ProjectId] AND pv3.[Status] = 2));
GO

ALTER TABLE [dbo].[Projects]
DROP CONSTRAINT DEFAULT_CompanyType;
GO


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10019' as i2,
    N'http://ereg.egov.bg/segment/R-10002' as na,
    N'http://ereg.egov.bg/segment/R-09999' as cont,
    N'http://ereg.egov.bg/segment/R-10000' as nl,
    N'http://ereg.egov.bg/segment/R-09989' as fpn
),
NutsAddresses as
(
    SELECT
        p.ProjectId,
        (CASE pv.Xml.value('(//Project/i2:ProjectBasicData/na:NutsAddress/cont:NutsLevel/nl:Id)[1]', 'NVARCHAR(50)')
            WHEN 'country'       THEN s.value('(cont:Country/fpn:FullPath)[1]'      , 'NVARCHAR(MAX)') + N'; '
            WHEN 'protectedZone' THEN s.value('(cont:ProtectedZone/fpn:FullPath)[1]', 'NVARCHAR(MAX)') + N'; '
            WHEN 'regionNUTS1'   THEN s.value('(cont:Nuts1/fpn:FullPath)[1]'        , 'NVARCHAR(MAX)') + N'; '
            WHEN 'regionNUTS2'   THEN s.value('(cont:Nuts2/fpn:FullPath)[1]'        , 'NVARCHAR(MAX)') + N'; '
            WHEN 'district'      THEN s.value('(cont:District/fpn:FullPath)[1]'     , 'NVARCHAR(MAX)') + N'; '
            WHEN 'municipality'  THEN s.value('(cont:Municipality/fpn:FullPath)[1]' , 'NVARCHAR(MAX)') + N'; '
            WHEN 'settlement'    THEN s.value('(cont:Settlement/fpn:FullPath)[1]'   , 'NVARCHAR(MAX)') + N'; '
        END) as NutsAddr
    FROM [dbo].[Projects] p
    JOIN [dbo].[ProjectVersionXmls] pv ON p.[ProjectId] = pv.[ProjectId]
    OUTER APPLY pv.[Xml].nodes('(/Project/i2:ProjectBasicData/na:NutsAddress/cont:NutsAddressContent)') a(s)
    WHERE pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]) AND NOT EXISTS (SELECT pv3.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv3 WHERE pv3.[ProjectId] = p.[ProjectId] AND pv3.[Status] = 2))
)
UPDATE p
SET
    [NutsAddressFullPath] = STUFF((SELECT NutsAddr FROM NutsAddresses WHERE ProjectId = p.[ProjectId] FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'), 1, 0, N'')
FROM [dbo].[Projects] p
GO


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10019' as i2,
    N'http://ereg.egov.bg/segment/R-10002' as na,
    N'http://ereg.egov.bg/segment/R-09999' as cont,
    N'http://ereg.egov.bg/segment/R-10000' as nl,
    N'http://ereg.egov.bg/segment/R-09989' as fpn
),
NutsAddresses as
(
    SELECT
        p.ProjectId,
        (CASE pv.Xml.value('(//Project/i2:ProjectBasicData/na:NutsAddress/cont:NutsLevel/nl:Id)[1]', 'NVARCHAR(50)')
            WHEN 'country'       THEN s.value('(cont:Country/fpn:FullPathName)[1]'      , 'NVARCHAR(MAX)') + N'; '
            WHEN 'protectedZone' THEN s.value('(cont:ProtectedZone/fpn:FullPathName)[1]', 'NVARCHAR(MAX)') + N'; '
            WHEN 'regionNUTS1'   THEN s.value('(cont:Nuts1/fpn:FullPathName)[1]'        , 'NVARCHAR(MAX)') + N'; '
            WHEN 'regionNUTS2'   THEN s.value('(cont:Nuts2/fpn:FullPathName)[1]'        , 'NVARCHAR(MAX)') + N'; '
            WHEN 'district'      THEN s.value('(cont:District/fpn:FullPathName)[1]'     , 'NVARCHAR(MAX)') + N'; '
            WHEN 'municipality'  THEN s.value('(cont:Municipality/fpn:FullPathName)[1]'     , 'NVARCHAR(MAX)') + N'; '
            WHEN 'settlement'    THEN s.value('(cont:Settlement/fpn:FullPathName)[1]'   , 'NVARCHAR(MAX)') + N'; '
        END) as NutsAddr
    FROM [dbo].[Projects] p
    JOIN [dbo].[ProjectVersionXmls] pv ON p.[ProjectId] = pv.[ProjectId]
    OUTER APPLY pv.[Xml].nodes('(/Project/i2:ProjectBasicData/na:NutsAddress/cont:NutsAddressContent)') a(s)
    WHERE pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]) AND NOT EXISTS (SELECT pv3.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv3 WHERE pv3.[ProjectId] = p.[ProjectId] AND pv3.[Status] = 2))
)
UPDATE p
SET
    [NutsAddressFullPathName] = STUFF((SELECT NutsAddr FROM NutsAddresses WHERE ProjectId = p.[ProjectId] FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'), 1, 0, N'')
FROM [dbo].[Projects] p
GO