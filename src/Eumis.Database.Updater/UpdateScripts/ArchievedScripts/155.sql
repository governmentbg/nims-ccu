GO

ALTER TABLE [dbo].[ContractReportIndicators] ADD
    [Name]                                  NVARCHAR(MAX)     NULL,
    [Type]                                  INT               NULL,
    [Kind]                                  INT               NULL,
    [Trend]                                 INT               NULL,
    [AggregatedReport]                      INT               NULL,
    [AggregatedTarget]                      INT               NULL,
    [MeasureName]                           NVARCHAR(MAX)     NULL
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10044' as i,
    N'http://ereg.egov.bg/segment/R-10054' as ci,
    N'http://ereg.egov.bg/segment/R-10038' as si
),
TechnicalReportIndicators as
(
SELECT
    crt.[ContractReportTechnicalId],
    [ContractIndicatorGid] = a.b.value('(./@gid)[1]', 'NVARCHAR(MAX)'),        
    [Name] = a.b.value('(si:SelectedIndicator/si:Name/text())[1]', 'NVARCHAR(MAX)'),
    [Type] = CASE a.b.value('(si:SelectedIndicator/si:TypeName/text())[1]', 'NVARCHAR(MAX)')
                    WHEN N'Специфичен за програмата индикатор' THEN 1
                    WHEN N'Общ индикатор' THEN 2
                    WHEN N'Индивидуален за процедура' THEN 3
                    ELSE NULL
                END,
    [Trend] = CASE a.b.value('(si:SelectedIndicator/si:TrendName/text())[1]', 'NVARCHAR(MAX)')
                    WHEN N'Намаление' THEN 1
                    WHEN N'Увеличение' THEN 2
                    WHEN N'Неприложимо' THEN 3
                    ELSE NULL
                END,
    [Kind] = CASE a.b.value('(si:SelectedIndicator/si:KindName/text())[1]', 'NVARCHAR(MAX)')
                    WHEN N'Финансов' THEN 1
                    WHEN N'Изпълнение' THEN 2
                    WHEN N'Резултат' THEN 3
                    WHEN N'Етап на изпълнение' THEN 4
                    ELSE NULL
                END,
    [MeasureName] = a.b.value('(si:SelectedIndicator/si:MeasureName/text())[1]', 'NVARCHAR(MAX)'),
    [AggregatedReport] = CASE a.b.value('(si:SelectedIndicator/si:AggregatedReport/text())[1]', 'NVARCHAR(MAX)')
                    WHEN N'Не' THEN 1
                    WHEN N'Да' THEN 2
                    WHEN N'Неприложимо' THEN 3
                    ELSE NULL
                END,
    [AggregatedTarget] = CASE a.b.value('(si:SelectedIndicator/si:AggregatedTarget/text())[1]', 'NVARCHAR(MAX)')
                    WHEN N'Не' THEN 1
                    WHEN N'Да' THEN 2
                    WHEN N'Неприложимо' THEN 3
                    ELSE NULL
                END,
    [HasGenderDivision] = CASE a.b.value('(si:SelectedIndicator/si:HasGenderDivision/text())[1]', 'NVARCHAR(MAX)')
                            WHEN 'true' THEN 1
                            WHEN 'false' THEN 0
                            ELSE NULL
                        END
FROM [dbo].[ContractReportTechnicals] crt
CROSS APPLY crt.[Xml].nodes('(//TechnicalReport/i:Indicators/i:Indicator/ci:BFPContractIndicator)') AS a(b)
WHERE crt.[Status] = 3
)
UPDATE cri
SET
    cri.[Name] = tri.[Name],
    cri.[Type] = tri.[Type],
    cri.[Kind] = tri.[Kind],
    cri.[Trend] = tri.[Trend],
    cri.[AggregatedReport] = tri.[AggregatedReport],
    cri.[AggregatedTarget] = tri.[AggregatedTarget],
    cri.[HasGenderDivision] = tri.[HasGenderDivision],
    cri.[MeasureName] = tri.[MeasureName]
FROM [dbo].[ContractReportIndicators] cri
JOIN [dbo].[ContractIndicators] ci ON cri.ContractIndicatorId = ci.ContractIndicatorId
JOIN TechnicalReportIndicators tri ON cri.ContractReportTechnicalId = tri.ContractReportTechnicalId AND ci.Gid = tri.ContractIndicatorGid
GO


ALTER TABLE [dbo].[ContractReportIndicators] ALTER COLUMN [Name]                                  NVARCHAR(MAX)     NOT NULL
ALTER TABLE [dbo].[ContractReportIndicators] ALTER COLUMN [Type]                                  INT               NOT NULL
ALTER TABLE [dbo].[ContractReportIndicators] ALTER COLUMN [Kind]                                  INT               NOT NULL
ALTER TABLE [dbo].[ContractReportIndicators] ALTER COLUMN [Trend]                                 INT               NOT NULL
ALTER TABLE [dbo].[ContractReportIndicators] ALTER COLUMN [AggregatedReport]                      INT               NOT NULL
ALTER TABLE [dbo].[ContractReportIndicators] ALTER COLUMN [AggregatedTarget]                      INT               NOT NULL
ALTER TABLE [dbo].[ContractReportIndicators] ALTER COLUMN [MeasureName]                           NVARCHAR(MAX)     NOT NULL
GO