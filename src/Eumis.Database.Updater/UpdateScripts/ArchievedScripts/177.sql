GO

ALTER TABLE [dbo].[Contracts] ADD
    [TerminationDate]                       DATETIME2         NULL,
    [TerminationReason]                     NVARCHAR(MAX)     NULL
GO

ALTER TABLE [dbo].[ContractVersionXmls] ADD
    [CompletionDate]                  DATETIME2           NULL,
    [TerminationDate]                 DATETIME2           NULL,
    [TerminationReason]               NVARCHAR(MAX)       NULL,

    [TotalEuAmount]                   MONEY               NULL,
    [TotalBgAmount]                   MONEY               NULL,
    [TotalBfpAmount]                  MONEY               NULL,
    [TotalSelfAmount]                 MONEY               NULL,
    [TotalAmount]                     MONEY               NULL
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as i2,
    N'http://ereg.egov.bg/segment/R-10031' as d,
    N'http://ereg.egov.bg/segment/R-10036' as am
)
UPDATE cv
SET
  [CompletionDate]     = cv.Xml.value('(/BFPContract/i2:BFPContractBasicData/d:CompletionDate)[1]'                                     , 'DATETIME2'    ),
  [TerminationDate]    = cv.Xml.value('(/BFPContract/i2:BFPContractBasicData/d:TerminationDate)[1]'                                    , 'DATETIME2'    ),
  [TerminationReason]  = cv.Xml.value('(/BFPContract/i2:BFPContractBasicData/d:TerminationReason)[1]'                                  , 'NVARCHAR(MAX)'),

  [TotalEuAmount]      = cv.Xml.value('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/am:EUAmount)[1]'       , 'MONEY'        ),
  [TotalBgAmount]      = cv.Xml.value('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/am:NationalAmount)[1]' , 'MONEY'        ),
  [TotalBfpAmount]     = cv.Xml.value('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/am:GrandAmount)[1]'    , 'MONEY'        ),
  [TotalSelfAmount]    = cv.Xml.value('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/am:SelfAmount)[1]'     , 'MONEY'        ),
  [TotalAmount]        = cv.Xml.value('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/am:TotalAmount)[1]'    , 'MONEY'        )
FROM [dbo].[ContractVersionXmls] cv
GO


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as i2,
    N'http://ereg.egov.bg/segment/R-10031' as d,
    N'http://ereg.egov.bg/segment/R-10036' as am
)
UPDATE c
SET
  [TerminationDate]    = cv.Xml.value('(/BFPContract/i2:BFPContractBasicData/d:TerminationDate)[1]'                                    , 'DATETIME2'    ),
  [TerminationReason]  = cv.Xml.value('(/BFPContract/i2:BFPContractBasicData/d:TerminationReason)[1]'                                  , 'NVARCHAR(MAX)')
FROM [dbo].[ContractVersionXmls] cv
JOIN [dbo].[Contracts] c on cv.[ContractId] = c.[ContractId]
WHERE cv.[Status] = 3
GO