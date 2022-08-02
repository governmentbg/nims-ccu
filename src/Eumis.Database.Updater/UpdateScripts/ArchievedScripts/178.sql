GO

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] ALTER COLUMN [NutsCode]    NVARCHAR(MAX)       NOT NULL
GO

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts] ADD
    [NutsFullPath]                      NVARCHAR(MAX)       NOT NULL CONSTRAINT DEFAULT_NutsFullPath     DEFAULT N'',
    [NutsFullPathName]                  NVARCHAR(MAX)       NOT NULL CONSTRAINT DEFAULT_NutsFullPathName DEFAULT N''
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as i2,
    N'http://ereg.egov.bg/segment/R-10036' as pb,
    N'http://ereg.egov.bg/segment/R-10035' as peb,
    N'http://ereg.egov.bg/segment/R-10034' as pdeb,
    N'http://ereg.egov.bg/segment/R-10033' as n,
    N'http://ereg.egov.bg/segment/R-09989' as nfpn
),
BudgetLevel3Nuts as
(
    SELECT
        cv.ContractId,
        s.value('(@gid)[1]'                     , 'UNIQUEIDENTIFIER') as Gid,
        s.value('(n:Nuts/nfpn:Code)[1]'         , 'NVARCHAR(MAX)'   ) as NutsCode,
        s.value('(n:Nuts/nfpn:Name)[1]'         , 'NVARCHAR(MAX)'   ) as NutsName,
        s.value('(n:Nuts/nfpn:FullPath)[1]'     , 'NVARCHAR(MAX)'   ) as NutsFullPath,
        s.value('(n:Nuts/nfpn:FullPathName)[1]' , 'NVARCHAR(MAX)'   ) as NutsFullPathName
    FROM [dbo].[ContractVersionXmls] cv
    OUTER APPLY cv.[Xml].nodes('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/pb:BFPContractProgrammeBudget/peb:BFPContractProgrammeExpenseBudget/pdeb:BFPContractProgrammeDetailsExpenseBudget)') a(s)
    WHERE cv.[Status] = 3
)
UPDATE cba
SET
    [NutsCode]         = bl3n.[NutsCode],
    [NutsName]         = bl3n.[NutsName],
    [NutsFullPath]     = bl3n.[NutsFullPath],
    [NutsFullPathName] = bl3n.[NutsFullPathName]
FROM [dbo].[ContractBudgetLevel3Amounts] cba
JOIN BudgetLevel3Nuts bl3n ON cba.[ContractId] = bl3n.[ContractId] AND cba.[Gid] = bl3n.[Gid]
GO


ALTER TABLE [dbo].[ContractBudgetLevel3Amounts]
DROP CONSTRAINT DEFAULT_NutsFullPath;
GO

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts]
DROP CONSTRAINT DEFAULT_NutsFullPathName;
GO