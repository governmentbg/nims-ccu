GO

ALTER TABLE [dbo].[ActuallyPaidAmounts]
ADD [PaidSelfAmount]          MONEY         NULL,
    [PaidTotalAmount]         MONEY         NULL;
GO

ALTER TABLE [dbo].[Contracts]
ADD [CompanyEmail]     NVARCHAR(200) NULL,
    [Duration]         INT           NULL,
    [ProjectKidCodeId] INT           NULL,
    [TotalEuAmount]    MONEY         NULL,
    [TotalBgAmount]    MONEY         NULL,
    [TotalSelfAmount]  MONEY         NULL,
    CONSTRAINT [FK_Contracts_ProjectKidCodes] FOREIGN KEY ([ProjectKidCodeId]) REFERENCES [dbo].[KidCodes] ([KidCodeId]);
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as i2,
    N'http://ereg.egov.bg/segment/R-10004' as clt,
    N'http://ereg.egov.bg/segment/R-10000' as cltid,
    N'http://ereg.egov.bg/segment/R-10031' as d,
    N'http://ereg.egov.bg/segment/R-10001' as ckc,
    N'http://ereg.egov.bg/segment/R-10036' as am
)
UPDATE c
SET
    [CompanyLegalTypeId] = clt.[CompanyLegalTypeId],
    [ProjectKidCodeId]   = kc.[KidCodeId],
    [CompanyEmail]       = cv.Xml.value('(/BFPContract/i2:Beneficiary/clt:CompanyContactPersonEmail)[1]'                                , 'NVARCHAR(200)'),
    [Duration]           = cv.Xml.value('(/BFPContract/i2:BFPContractBasicData/d:Duration)[1]'                                          , 'INT'          ),
    [TotalEuAmount]      = cv.Xml.value('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/am:EUAmount)[1]'      , 'MONEY'        ),
    [TotalBgAmount]      = cv.Xml.value('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/am:NationalAmount)[1]', 'MONEY'        ),
    [TotalSelfAmount]    = cv.Xml.value('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/am:SelfAmount)[1]'    , 'MONEY'        )
FROM [dbo].[Contracts] c
JOIN [dbo].[ContractVersionXmls] cv ON c.[ContractId] = cv.[ContractId]
JOIN [dbo].[CompanyLegalTypes] clt ON cv.Xml.value('(/BFPContract/i2:Beneficiary/clt:CompanyLegalType/cltid:Id)[1]', 'uniqueidentifier') = clt.[Gid]
LEFT OUTER JOIN [dbo].[KidCodes] kc ON cv.Xml.value('(/BFPContract/i2:Beneficiary/clt:KidCodeProject/ckc:Code)[1]', 'NVARCHAR(50)') = kc.[Code]
WHERE cv.[Status] = 3;
GO
