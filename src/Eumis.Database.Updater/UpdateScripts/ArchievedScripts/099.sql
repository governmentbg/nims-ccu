GO

ALTER TABLE [dbo].[Contracts] ADD
    [Description]                           NVARCHAR(MAX)     NULL,
    [DescriptionEN]                         NVARCHAR(MAX)     NULL;
GO

ALTER TABLE [dbo].[Contracts] DROP COLUMN [NutsCodes];
GO

ALTER TABLE [dbo].[ContractProcurementPlans] ADD [OffersDeadlineDate] DATETIME2 NULL;
GO

CREATE TABLE [dbo].[ContractLocations] (
    [ContractLocationId]    INT             NOT NULL IDENTITY,
    [ContractId]            INT             NOT NULL,

    [NutsCode]              NVARCHAR(MAX)   NOT NULL,
    [Name]                  NVARCHAR(MAX)   NOT NULL,
    [FullPath]              NVARCHAR(MAX)   NOT NULL,
    [FullPathName]          NVARCHAR(MAX)   NOT NULL,

    CONSTRAINT [PK_ContractLocations]               PRIMARY KEY ([ContractLocationId]),
    CONSTRAINT [FK_ContractLocations_Contracts]     FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId])
);
GO

CREATE TABLE [dbo].[ContractProcurementPlanPublicDocuments] (
    [ContractProcurementPlanPublicDocumentId]  INT          NOT NULL IDENTITY,
    [ContractProcurementPlanId]         INT                 NOT NULL,

    [BlobKey]                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [Name]                              NVARCHAR(MAX)       NOT NULL,
    [Description]                       NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractProcurementPlanPublicDocuments]                              PRIMARY KEY ([ContractProcurementPlanPublicDocumentId]),
    CONSTRAINT [FK_ContractProcurementPlanPublicDocuments_ContractProcurementPlans]     FOREIGN KEY ([ContractProcurementPlanId])   REFERENCES [dbo].[ContractProcurementPlans] ([ContractProcurementPlanId]),
    CONSTRAINT [FK_ContractProcurementPlanPublicDocuments_Blobs]                        FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
);
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd
)
UPDATE c SET
	Description = x.Xml.value('(/BFPContract/c:BFPContractBasicData/cbd:Description)[1]', 'NVARCHAR(MAX)'),
    DescriptionEN = x.Xml.value('(/BFPContract/c:BFPContractBasicData/cbd:DescriptionEN)[1]', 'NVARCHAR(MAX)')
FROM ContractVersionXmls x
JOIN Contracts c ON x.ContractId = c.ContractId
WHERE c.ContractStatus = 2 and x.Status = 3
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10041' as p,
    N'http://ereg.egov.bg/segment/R-10048' as pp
),
ContractProcurementPlanValues as
(
    SELECT
        s.value('(@gid)[1]', 'NVARCHAR(MAX)') as ContractProcurementPlanGid,
		s.value('(pp:OffersDeadlineDate)[1]', 'DATETIME2') as OffersDeadlineDate
    FROM ContractProcurementXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    OUTER APPLY x.Xml.nodes('(/Procurements/p:ProcurementPlans/p:ProcurementPlan)') a(s)
    WHERE c.ContractStatus = 2 and x.Status = 3
)
UPDATE pp SET
	OffersDeadlineDate = ppv.OffersDeadlineDate
FROM ContractProcurementPlans pp
JOIN ContractProcurementPlanValues ppv ON pp.Gid = ppv.ContractProcurementPlanGid
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd,
    N'http://ereg.egov.bg/segment/R-09999' as na,
    N'http://ereg.egov.bg/segment/R-09989' as nuts
),
ContractLocationValues as
(
    SELECT
        c.ContractId,
        s.value('(nuts:Code)[1]', 'NVARCHAR(MAX)') as NutsCode,
        s.value('(nuts:Name)[1]', 'NVARCHAR(MAX)') as Name,
        s.value('(nuts:FullPath)[1]', 'NVARCHAR(MAX)') as FullPath,
        s.value('(nuts:FullPathName)[1]', 'NVARCHAR(MAX)') as FullPathName
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    CROSS APPLY x.Xml.nodes('(/BFPContract/c:BFPContractBasicData/cbd:NutsAddress/na:NutsAddressContent/na:Country)') a(s)
    WHERE c.ContractStatus = 2 and
        c.NutsLevel = 1 and --country
        x.Status = 3
)
INSERT INTO ContractLocations
SELECT * FROM ContractLocationValues;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd,
    N'http://ereg.egov.bg/segment/R-09999' as na,
    N'http://ereg.egov.bg/segment/R-09989' as nuts
),
ContractLocationValues as
(
    SELECT
        c.ContractId,
        s.value('(nuts:Code)[1]', 'NVARCHAR(MAX)') as NutsCode,
        s.value('(nuts:Name)[1]', 'NVARCHAR(MAX)') as Name,
        s.value('(nuts:FullPath)[1]', 'NVARCHAR(MAX)') as FullPath,
        s.value('(nuts:FullPathName)[1]', 'NVARCHAR(MAX)') as FullPathName
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    CROSS APPLY x.Xml.nodes('(/BFPContract/c:BFPContractBasicData/cbd:NutsAddress/na:NutsAddressContent/na:Nuts1)') a(s)
    WHERE c.ContractStatus = 2 and
        c.NutsLevel = 2 and --nuts1
        x.Status = 3
)
INSERT INTO ContractLocations
SELECT * FROM ContractLocationValues;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd,
    N'http://ereg.egov.bg/segment/R-09999' as na,
    N'http://ereg.egov.bg/segment/R-09989' as nuts
),
ContractLocationValues as
(
    SELECT
        c.ContractId,
        s.value('(nuts:Code)[1]', 'NVARCHAR(MAX)') as NutsCode,
        s.value('(nuts:Name)[1]', 'NVARCHAR(MAX)') as Name,
        s.value('(nuts:FullPath)[1]', 'NVARCHAR(MAX)') as FullPath,
        s.value('(nuts:FullPathName)[1]', 'NVARCHAR(MAX)') as FullPathName
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    CROSS APPLY x.Xml.nodes('(/BFPContract/c:BFPContractBasicData/cbd:NutsAddress/na:NutsAddressContent/na:Nuts2)') a(s)
    WHERE c.ContractStatus = 2 and
        c.NutsLevel = 3 and --nuts2
        x.Status = 3
)
INSERT INTO ContractLocations
SELECT * FROM ContractLocationValues;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd,
    N'http://ereg.egov.bg/segment/R-09999' as na,
    N'http://ereg.egov.bg/segment/R-09989' as nuts
),
ContractLocationValues as
(
    SELECT
        c.ContractId,
        s.value('(nuts:Code)[1]', 'NVARCHAR(MAX)') as NutsCode,
        s.value('(nuts:Name)[1]', 'NVARCHAR(MAX)') as Name,
        s.value('(nuts:FullPath)[1]', 'NVARCHAR(MAX)') as FullPath,
        s.value('(nuts:FullPathName)[1]', 'NVARCHAR(MAX)') as FullPathName
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    CROSS APPLY x.Xml.nodes('(/BFPContract/c:BFPContractBasicData/cbd:NutsAddress/na:NutsAddressContent/na:District)') a(s)
    WHERE c.ContractStatus = 2 and
        c.NutsLevel = 4 and --district
        x.Status = 3
)
INSERT INTO ContractLocations
SELECT * FROM ContractLocationValues;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd,
    N'http://ereg.egov.bg/segment/R-09999' as na,
    N'http://ereg.egov.bg/segment/R-09989' as nuts
),
ContractLocationValues as
(
    SELECT
        c.ContractId,
        s.value('(nuts:Code)[1]', 'NVARCHAR(MAX)') as NutsCode,
        s.value('(nuts:Name)[1]', 'NVARCHAR(MAX)') as Name,
        s.value('(nuts:FullPath)[1]', 'NVARCHAR(MAX)') as FullPath,
        s.value('(nuts:FullPathName)[1]', 'NVARCHAR(MAX)') as FullPathName
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    CROSS APPLY x.Xml.nodes('(/BFPContract/c:BFPContractBasicData/cbd:NutsAddress/na:NutsAddressContent/na:Municipality)') a(s)
    WHERE c.ContractStatus = 2 and
        c.NutsLevel = 5 and --municipality
        x.Status = 3
)
INSERT INTO ContractLocations
SELECT * FROM ContractLocationValues;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd,
    N'http://ereg.egov.bg/segment/R-09999' as na,
    N'http://ereg.egov.bg/segment/R-09989' as nuts
),
ContractLocationValues as
(
    SELECT
        c.ContractId,
        s.value('(nuts:Code)[1]', 'NVARCHAR(MAX)') as NutsCode,
        s.value('(nuts:Name)[1]', 'NVARCHAR(MAX)') as Name,
        s.value('(nuts:FullPath)[1]', 'NVARCHAR(MAX)') as FullPath,
        s.value('(nuts:FullPathName)[1]', 'NVARCHAR(MAX)') as FullPathName
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    CROSS APPLY x.Xml.nodes('(/BFPContract/c:BFPContractBasicData/cbd:NutsAddress/na:NutsAddressContent/na:Settlement)') a(s)
    WHERE c.ContractStatus = 2 and
        c.NutsLevel = 6 and --settlement
        x.Status = 3
)
INSERT INTO ContractLocations
SELECT * FROM ContractLocationValues;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as c,
    N'http://ereg.egov.bg/segment/R-10031' as cbd,
    N'http://ereg.egov.bg/segment/R-09999' as na,
    N'http://ereg.egov.bg/segment/R-09989' as nuts
),
ContractLocationValues as
(
    SELECT
        c.ContractId,
        s.value('(nuts:Code)[1]', 'NVARCHAR(MAX)') as NutsCode,
        s.value('(nuts:Name)[1]', 'NVARCHAR(MAX)') as Name,
        s.value('(nuts:FullPath)[1]', 'NVARCHAR(MAX)') as FullPath,
        s.value('(nuts:FullPathName)[1]', 'NVARCHAR(MAX)') as FullPathName
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    CROSS APPLY x.Xml.nodes('(/BFPContract/c:BFPContractBasicData/cbd:NutsAddress/na:NutsAddressContent/na:ProtectedZone)') a(s)
    WHERE c.ContractStatus = 2 and
        c.NutsLevel = 7 and --protectedZone
        x.Status = 3
)
INSERT INTO ContractLocations
SELECT * FROM ContractLocationValues;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10041' as p,
    N'http://ereg.egov.bg/segment/R-10048' as pp,
    N'http://ereg.egov.bg/segment/R-10018' as pad,
    N'http://ereg.egov.bg/segment/R-09992' as adc
),
ContractProcurementPlanPublicDocumentValues as
(
    SELECT
        s.value('(@gid)[1]', 'NVARCHAR(MAX)') as ContractProcurementPlanGid,
        s1.value('(pad:Description)[1]', 'NVARCHAR(MAX)') as Description,
        s1.value('(pad:AttachedDocumentContent/adc:FileName)[1]', 'NVARCHAR(MAX)') as Name,
        s1.value('(pad:AttachedDocumentContent/adc:BlobContentId)[1]', 'NVARCHAR(MAX)') as BlobKey
    FROM ContractProcurementXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    OUTER APPLY x.Xml.nodes('(/Procurements/p:ProcurementPlans/p:ProcurementPlan)') a(s)
    OUTER APPLY a.s.nodes('(pp:PublicAttachedDocument)') a1(s1)
    WHERE c.ContractStatus = 2 and x.Status = 3
)
INSERT INTO ContractProcurementPlanPublicDocuments
SELECT
    cpp.ContractProcurementPlanId,
    CONVERT(UNIQUEIDENTIFIER, v.BlobKey) as BlobKey,
    v.Name,
    v.Description
FROM ContractProcurementPlanPublicDocumentValues v 
JOIN ContractProcurementPlans cpp on v.ContractProcurementPlanGid = cpp.Gid
WHERE v.BlobKey IS NOT NULL
GO
