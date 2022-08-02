GO

ALTER TABLE [dbo].[ProjectVersionXmls] ADD 
    [TotalBfpAmount]        MONEY               NULL,
    [CoFinancingAmount]     MONEY               NULL
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
        pvx.[ProjectVersionXmlId],
        COALESCE(s.value('(am:SelfAmount)[1]'      , 'MONEY'), 0) as [SelfAmount],
        COALESCE(s.value('(am:GrandAmount)[1]'     , 'MONEY'), 0) as [GrandAmount]
    FROM [dbo].[ProjectVersionXmls] pvx
    OUTER APPLY pvx.[Xml].nodes('(/Project/i2:DimensionsBudgetContract/b:Budget/pb:ProgrammeBudget/peb:ProgrammeExpenseBudget/pdeb:ProgrammeDetailsExpenseBudget)') a(s)
)
UPDATE pvx
SET
    [CoFinancingAmount] = (SELECT SUM(ba.[SelfAmount]) FROM BudgetAmounts ba WHERE ba.[ProjectVersionXmlId] = pvx.[ProjectVersionXmlId] GROUP BY ba.[ProjectVersionXmlId]),
    [TotalBfpAmount] = (SELECT SUM(ba.[GrandAmount]) FROM BudgetAmounts ba WHERE ba.[ProjectVersionXmlId] = pvx.[ProjectVersionXmlId] GROUP BY ba.[ProjectVersionXmlId])
FROM [dbo].[ProjectVersionXmls] pvx
GO
