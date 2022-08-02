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
        COALESCE(s.value('(am:GrandAmount)[1]'     , 'MONEY'), 0) as GrandAmount
    FROM [dbo].[Projects] p
    JOIN [dbo].[ProjectVersionXmls] pv ON p.[ProjectId] = pv.[ProjectId]
    OUTER APPLY pv.[Xml].nodes('(/Project/i2:DimensionsBudgetContract/b:Budget/pb:ProgrammeBudget/peb:ProgrammeExpenseBudget/pdeb:ProgrammeDetailsExpenseBudget)') a(s)
    WHERE pv.[Status] = 2 OR (pv.[Status] = 3 AND NOT EXISTS(SELECT pv2.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv2 WHERE pv2.[ProjectId] = p.[ProjectId] AND pv2.[Status] = 3 AND pv2.[OrderNum] > pv.[OrderNum]) AND NOT EXISTS (SELECT pv3.[ProjectVersionXmlId] FROM [dbo].[ProjectVersionXmls] pv3 WHERE pv3.[ProjectId] = p.[ProjectId] AND pv3.[Status] = 2))
)
UPDATE p
SET
    [TotalBfpAmount]    = (SELECT SUM(ba.[GrandAmount]) FROM BudgetAmounts ba WHERE ba.[ProjectId] = p.[ProjectId] GROUP BY ba.[ProjectId])
FROM [dbo].[Projects] p
GO
