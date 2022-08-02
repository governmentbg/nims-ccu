GO

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts]
ADD [NutsName] NVARCHAR(MAX)  NOT NULL CONSTRAINT DEFAULT_NutsName DEFAULT N'';
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as i2,
    N'http://ereg.egov.bg/segment/R-10036' as pb,
    N'http://ereg.egov.bg/segment/R-10035' as peb,
    N'http://ereg.egov.bg/segment/R-10034' as pdeb,
    N'http://ereg.egov.bg/segment/R-10033' as n,
    N'http://ereg.egov.bg/segment/R-09989' as nfpn
),
NutsNames as
(
    SELECT
        cv.ContractId,
        s.value('(@gid)[1]'                     , 'UNIQUEIDENTIFIER') as Gid,
        s.value('(n:Nuts/nfpn:FullPathName)[1]' , 'NVARCHAR(MAX)'   ) as NutsName
    FROM [dbo].[ContractVersionXmls] cv
    OUTER APPLY cv.[Xml].nodes('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/pb:BFPContractProgrammeBudget/peb:BFPContractProgrammeExpenseBudget/pdeb:BFPContractProgrammeDetailsExpenseBudget)') a(s)
    WHERE cv.[Status] = 3
)
UPDATE cba
SET [NutsName] = nn.NutsName
FROM [dbo].[ContractBudgetLevel3Amounts] cba
JOIN NutsNames nn ON cba.[ContractId] = nn.ContractId AND cba.[Gid] = nn.Gid
GO

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts]
DROP CONSTRAINT DEFAULT_NutsName;
GO