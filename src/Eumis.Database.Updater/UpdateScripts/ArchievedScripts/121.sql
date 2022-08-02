GO

ALTER TABLE [dbo].[Projects]
DROP
CONSTRAINT [CHK_Projects_NutsLevel], [FK_Projects_Countries], [FK_Projects_Nuts1s], [FK_Projects_Nuts2s], [FK_Projects_Districts], [FK_Projects_Municipalities], [FK_Projects_Settlements],
COLUMN [NutsLevel], [CountryId], [Nuts1Id], [Nuts2Id], [DistrictId], [MunicipalityId], [SettlementId];

ALTER TABLE [dbo].[Projects]
ADD [CompanyLegalTypeId]      INT            NOT NULL CONSTRAINT DEFAULT_CompanyLegalType DEFAULT 1,
    [NutsAddress]             NVARCHAR(MAX)  NULL,
    [CompanyEmail]            NVARCHAR(200)  NULL,
    [CompanySeatCountryId]    INT            NULL,
    [CompanySeatSettlementId] INT            NULL,
    [CompanySeatPostCode]     NVARCHAR(50)   NULL,
    [CompanySeatStreet]       NVARCHAR(200)  NULL,
    [CompanySeatAddress]      NVARCHAR(MAX)  NULL,
    [TotalBfpAmount]          MONEY          NULL,
    [CoFinancingAmount]       MONEY          NULL,
    CONSTRAINT [FK_Projects_CompanyLegalType]       FOREIGN KEY ([CompanyLegalTypeId])      REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [FK_Projects_CompanySeatCountries]   FOREIGN KEY ([CompanySeatCountryId])    REFERENCES [dbo].[Countries]         ([CountryId]),
    CONSTRAINT [FK_Projects_CompanySeatSettlements] FOREIGN KEY ([CompanySeatSettlementId]) REFERENCES [dbo].[Settlements]       ([SettlementId]);
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
    [CompanyLegalTypeId]      = clt.[CompanyLegalTypeId],
    [Duration]                = pv.Xml.value('(/Project/i2:ProjectBasicData/d:Duration)[1]'            , 'INT'          ),
    [CompanyEmail]            = pv.Xml.value('(/Project/i2:Candidate/clt:CompanyContactPersonEmail)[1]', 'NVARCHAR(200)'),
    [CompanySeatCountryId]    = c.[CountryId],
    [CompanySeatSettlementId] = (CASE c.[NutsCode]
                                    WHEN N'BG' THEN s.[SettlementId]
                                    ELSE NULL
                                END),
    [CompanySeatPostCode]     = (CASE c.[NutsCode]
                                    WHEN N'BG' THEN pv.Xml.value('(//Project/i2:Candidate/clt:Seat/cont:PostCode)[1]', 'NVARCHAR(50)')
                                    ELSE NULL
                                END),
    [CompanySeatStreet]       = (CASE c.[NutsCode]
                                    WHEN N'BG' THEN pv.Xml.value('(//Project/i2:Candidate/clt:Seat/cont:Street)[1]', 'NVARCHAR(200)')
                                    ELSE NULL
                                END),
    [CompanySeatAddress]     = (CASE c.[NutsCode]
                                WHEN 'BG' THEN NULL
                                ELSE pv.Xml.value('(//Project/i2:Candidate/clt:Seat/cont:FullAddress)[1]', 'NVARCHAR(MAX)')
                            END)
FROM [dbo].[Projects] p
JOIN [dbo].[ProjectVersionXmls] pv ON p.[ProjectId] = pv.[ProjectId]
JOIN [dbo].[CompanyLegalTypes] clt ON pv.Xml.value('(/Project/i2:Candidate/clt:CompanyLegalType)[1]', 'uniqueidentifier') = clt.[Gid]
JOIN [dbo].[Countries] c ON pv.Xml.value('(//Project/i2:Candidate/clt:Seat/cont:Country/sitm:Code)[1]', 'NVARCHAR(50)') = c.[NutsCode]
LEFT OUTER JOIN [dbo].[Settlements] s ON pv.Xml.value('(//Project/i2:Candidate/clt:Seat/cont:Settlement/sitm:Code)[1]', 'NVARCHAR(50)') = s.[LauCode]
WHERE pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]));
GO

UPDATE p
SET
    [CompanyLegalTypeId]      = c.[CompanyLegalTypeId],
    [CompanySeatCountryId]    = c.[SeatCountryId],
    [CompanySeatSettlementId] = c.[SeatSettlementId],
    [CompanySeatPostCode]     = c.[SeatPostCode],
    [CompanySeatStreet]       = c.[SeatStreet],
    [CompanySeatAddress]      = c.[SeatAddress]
FROM [dbo].[Projects] p
JOIN [dbo].[Companies] c ON p.[CompanyId] = c.[CompanyId]
WHERE NOT EXISTS(SELECT pv.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv WHERE pv.[ProjectId] = p.[ProjectId])
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
            WHEN 'municipality'  THEN s.value('(cont:District/fpn:Municipality)[1]'     , 'NVARCHAR(MAX)') + N'; '
            WHEN 'settlement'    THEN s.value('(cont:Settlement/fpn:FullPathName)[1]'   , 'NVARCHAR(MAX)') + N'; '
        END) as NutsAddr
    FROM [dbo].[Projects] p
    JOIN [dbo].[ProjectVersionXmls] pv ON p.[ProjectId] = pv.[ProjectId]
    OUTER APPLY pv.[Xml].nodes('(/Project/i2:ProjectBasicData/na:NutsAddress/cont:NutsAddressContent)') a(s)
    WHERE pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]))
)
UPDATE p
SET
    [NutsAddress] = STUFF((SELECT NutsAddr FROM NutsAddresses WHERE ProjectId = p.[ProjectId] FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'), 1, 0, N'')
FROM [dbo].[Projects] p
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10019' as i2,
    N'http://ereg.egov.bg/segment/R-09998' as b,
    N'http://ereg.egov.bg/segment/R-10010' as pb,
    N'http://ereg.egov.bg/segment/R-10009' as peb,
    N'http://ereg.egov.bg/segment/R-10008' as pdeb,
    N'http://ereg.egov.bg/segment/R-10007' as am
),
BudgetAmounts as
(
    SELECT
        p.ProjectId,
        COALESCE(s.value('(pdeb:ProgrammeDetailsExpenseBudget/am:SelfAmount)[1]'      , 'MONEY'), 0) as SelfAmount,
        COALESCE(s.value('(pdeb:ProgrammeDetailsExpenseBudget/am:TotalAmount)[1]'     , 'MONEY'), 0) as TotalAmount
    FROM [dbo].[Projects] p
    JOIN [dbo].[ProjectVersionXmls] pv ON p.[ProjectId] = pv.[ProjectId]
    OUTER APPLY pv.[Xml].nodes('(/Project/i2:DimensionsBudgetContract/b:Budget/pb:ProgrammeBudget/peb:ProgrammeExpenseBudget)') a(s)
    WHERE pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]))
)
UPDATE p
SET
    [TotalBfpAmount]    = (SELECT SUM(ba.[TotalAmount]) FROM BudgetAmounts ba WHERE ba.[ProjectId] = p.[ProjectId] GROUP BY ba.[ProjectId]),
    [CoFinancingAmount] = (SELECT SUM(ba.[SelfAmount]) FROM BudgetAmounts ba WHERE ba.[ProjectId] = p.[ProjectId] GROUP BY ba.[ProjectId])
FROM [dbo].[Projects] p
GO

ALTER TABLE [dbo].[Projects]
DROP CONSTRAINT DEFAULT_CompanyLegalType;
GO
