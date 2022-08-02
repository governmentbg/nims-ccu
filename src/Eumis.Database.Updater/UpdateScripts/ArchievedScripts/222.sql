GO
ALTER TABLE [dbo].[RegOfferXmls] ADD
    [Tenderer]                              NVARCHAR(200)       NOT NULL CONSTRAINT DEFAULT_TendererName    DEFAULT '',
    [Uin]                                   NVARCHAR(200)       NULL,
    [UinType]                               INT                 NOT NULL CONSTRAINT DEFAULT_TendererUinType DEFAULT 1,
    [Email]                                 NVARCHAR(200)       NOT NULL CONSTRAINT DEFAULT_TendererEmail   DEFAULT '',
    CONSTRAINT [CHK_RegOfferXmls_UinType]   CHECK               ([UinType] IN (0, 1, 2, 3))
GO

WITH XMLNAMESPACES (
	N'http://ereg.egov.bg/segment/R-10080' as ns1,
	N'http://ereg.egov.bg/segment/R-10004' as ns2,
	N'http://ereg.egov.bg/segment/R-10000' as ns3
),
 xmlData as(

SELECT
    s.value('(ns2:Name)[1]', 'NVARCHAR(200)') as Tenderer,
    s.value('(ns2:Email)[1]', 'NVARCHAR(200)') as Email,
    s.value('(ns2:Uin)[1]', 'NVARCHAR(200)') as Uin,
    Case s.value('(ns2:UinType/ns3:Id)[1]', 'NVARCHAR(15)') 
	    WHEN 'eik' THEN 0
	    WHEN 'bulstat' THEN 1
	    WHEN 'personalBulstat' THEN 2
	    WHEN 'foreign' THEN 3
    End as UinType,
    rox.Gid as Gid
FROM [dbo].[RegOfferXmls] rox
 OUTER APPLY rox.[Xml].nodes('(/Offer/ns1:Candidate)') a(s)
)
 UPDATE rox SET
	[Uin]			= xmlData.[Uin],
	[Email]			= xmlData.[Email],
	[UinType]		= xmlData.[UinType],
	[Tenderer]		= xmlData.[Tenderer]
FROM [dbo].[RegOfferXmls] rox
JOIN xmlData on rox.Gid = xmlData.Gid


ALTER TABLE [dbo].[RegOfferXmls] DROP CONSTRAINT
    DEFAULT_TendererName,
    DEFAULT_TendererUinType,
    DEFAULT_TendererEmail
