GO

ALTER TABLE [dbo].[Contracts]
ADD
    [BeneficiaryCorrespondenceCountryId]    INT               NULL,
    [BeneficiaryCorrespondenceSettlementId] INT               NULL,
    [BeneficiaryCorrespondencePostCode]     NVARCHAR(50)      NULL,
    [BeneficiaryCorrespondenceStreet]       NVARCHAR(200)     NULL,
    [BeneficiaryCorrespondenceAddress]      NVARCHAR(MAX)     NULL;
GO

ALTER TABLE [dbo].[Contracts] ADD
    CONSTRAINT [FK_Contracts_Countries_Correspondence]     FOREIGN KEY ([BeneficiaryCorrespondenceCountryId])    REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_Contracts_Settlements_Correspondence]   FOREIGN KEY ([BeneficiaryCorrespondenceSettlementId]) REFERENCES [dbo].[Settlements] ([SettlementId]);
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as b,
    N'http://ereg.egov.bg/segment/R-10004' as c,
    N'http://ereg.egov.bg/segment/R-10003' as a,
    N'http://ereg.egov.bg/segment/R-10001' as co
)
UPDATE contr
SET
    [BeneficiaryCorrespondenceCountryId]    = c.[CountryId],
    [BeneficiaryCorrespondenceSettlementId] = (CASE c.[NutsCode]
                                                  WHEN N'BG' THEN s.[SettlementId]
                                                  ELSE NULL
                                              END),
    [BeneficiaryCorrespondencePostCode]     = (CASE c.[NutsCode]
                                                  WHEN N'BG' THEN cv.Xml.value('(//BFPContract/b:Beneficiary/c:Correspondence/a:PostCode)[1]', 'NVARCHAR(50)')
                                                  ELSE NULL
                                              END),
    [BeneficiaryCorrespondenceStreet]       = (CASE c.[NutsCode]
                                                  WHEN N'BG' THEN cv.Xml.value('(//BFPContract/b:Beneficiary/c:Correspondence/a:Street)[1]', 'NVARCHAR(200)')
                                                  ELSE NULL
                                              END),
    [BeneficiaryCorrespondenceAddress]     = (CASE c.[NutsCode]
                                                  WHEN 'BG' THEN NULL
                                                  ELSE cv.Xml.value('(//BFPContract/b:Beneficiary/c:Correspondence/a:FullAddress)[1]', 'NVARCHAR(MAX)')
                                              END)
FROM [dbo].[ContractVersionXmls] cv
JOIN [dbo].[Contracts] contr on cv.[ContractId] = contr.[ContractId]
JOIN [dbo].[Countries] c ON cv.Xml.value('(//BFPContract/b:Beneficiary/c:Correspondence/a:Country/co:Code)[1]', 'NVARCHAR(50)') = c.[NutsCode]
LEFT OUTER JOIN [dbo].[Settlements] s ON cv.Xml.value('(//BFPContract/b:Beneficiary/c:Correspondence/a:Settlement/co:Code)[1]', 'NVARCHAR(50)') = s.[LauCode]
WHERE cv.[Status] = 3
GO