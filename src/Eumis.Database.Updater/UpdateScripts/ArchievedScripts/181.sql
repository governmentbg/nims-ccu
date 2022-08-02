GO

ALTER TABLE [dbo].[Projects]
ADD 
    [CompanyCorrespondenceCountryId]        INT                 NULL,
    [CompanyCorrespondenceSettlementId]     INT                 NULL,
    [CompanyCorrespondencePostCode]         NVARCHAR(50)        NULL,
    [CompanyCorrespondenceStreet]           NVARCHAR(300)       NULL,
    [CompanyCorrespondenceAddress]          NVARCHAR(MAX)       NULL,
    CONSTRAINT [FK_Projects_CompanyCorrespondenceCountries]   FOREIGN KEY ([CompanyCorrespondenceCountryId])    REFERENCES [dbo].[Countries]         ([CountryId]),
    CONSTRAINT [FK_Projects_CompanyCorrespondenceSettlements] FOREIGN KEY ([CompanyCorrespondenceSettlementId]) REFERENCES [dbo].[Settlements]       (SettlementId);
GO


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10019' as i2,
    N'http://ereg.egov.bg/segment/R-10004' as clt,
    N'http://ereg.egov.bg/segment/R-10002' as d,
    N'http://ereg.egov.bg/segment/R-10003' as cont,
    N'http://ereg.egov.bg/segment/R-10001' as sitm
)
UPDATE p
SET
    [CompanyCorrespondenceCountryId]    = c.[CountryId],
    [CompanyCorrespondenceSettlementId] = (CASE c.[NutsCode]
                                              WHEN N'BG' THEN s.[SettlementId]
                                              ELSE NULL
                                          END),
    [CompanyCorrespondencePostCode]     = (CASE c.[NutsCode]
                                              WHEN N'BG' THEN pv.Xml.value('(//Project/i2:Candidate/clt:Correspondence/cont:PostCode)[1]', 'NVARCHAR(50)')
                                              ELSE NULL
                                          END),
    [CompanyCorrespondenceStreet]       = (CASE c.[NutsCode]
                                              WHEN N'BG' THEN pv.Xml.value('(//Project/i2:Candidate/clt:Correspondence/cont:Street)[1]', 'NVARCHAR(200)')
                                              ELSE NULL
                                          END),
    [CompanyCorrespondenceAddress]     = (CASE c.[NutsCode]
                                              WHEN 'BG' THEN NULL
                                              ELSE pv.Xml.value('(//Project/i2:Candidate/clt:Correspondence/cont:FullAddress)[1]', 'NVARCHAR(MAX)')
                                          END)
FROM [dbo].[Projects] p
JOIN [dbo].[ProjectVersionXmls] pv ON p.[ProjectId] = pv.[ProjectId]
JOIN [dbo].[Countries] c ON pv.Xml.value('(//Project/i2:Candidate/clt:Correspondence/cont:Country/sitm:Code)[1]', 'NVARCHAR(50)') = c.[NutsCode]
LEFT OUTER JOIN [dbo].[Settlements] s ON pv.Xml.value('(//Project/i2:Candidate/clt:Correspondence/cont:Settlement/sitm:Code)[1]', 'NVARCHAR(50)') = s.[LauCode]
WHERE pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]) AND NOT EXISTS (SELECT pv3.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv3 WHERE pv3.[ProjectId] = p.[ProjectId] AND pv3.[Status] = 2));
GO