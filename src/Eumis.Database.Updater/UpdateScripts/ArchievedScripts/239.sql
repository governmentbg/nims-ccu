GO
ALTER TABLE [dbo].[ContractReportTechnicalCorrectionIndicators]  ADD
[LastReportCorrectedCumulativeAmountMen]    DECIMAL(15,3)   NULL,
[LastReportCorrectedCumulativeAmountWomen]  DECIMAL(15,3)   NULL,
[LastReportCorrectedCumulativeAmountTotal]  DECIMAL(15,3)   NULL
GO

UPDATE crtci
SET crtci.[LastReportCorrectedCumulativeAmountTotal] = cri.[LastReportCumulativeAmountTotal],
    crtci.[LastReportCorrectedCumulativeAmountMen] = cri.[LastReportCumulativeAmountMen],
    crtci.[LastReportCorrectedCumulativeAmountWomen] = cri.[LastReportCumulativeAmountWomen]
FROM [dbo].[ContractReportTechnicalCorrectionIndicators] AS crtci
INNER JOIN [dbo].[ContractReportIndicators] AS cri on crtci.[ContractReportIndicatorId] = cri.[ContractReportIndicatorId]
GO

ALTER TABLE ContractReportTechnicalCorrectionIndicators ALTER COLUMN
    [LastReportCorrectedCumulativeAmountTotal]  DECIMAL(15,3)   NOT NULL;
GO
