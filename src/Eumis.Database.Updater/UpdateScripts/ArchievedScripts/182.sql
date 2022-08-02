GO

CREATE TABLE [dbo].[ContractVersionXmlAmounts] (
    [ContractVersionXmlAmountId]        INT                 NOT NULL IDENTITY,
    [ContractVersionXmlId]              INT                 NOT NULL,
    [ContractId]                        INT                 NOT NULL,
    [ProcedureBudgetLevel2Id]           INT                 NOT NULL,
    [OrderNum]                          INT                 NOT NULL,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL,
    [IsActive]                          BIT                 NOT NULL,

    [Name]                              NVARCHAR(MAX)       NOT NULL,

    [ContractEuAmount]                  MONEY               NOT NULL,
    [ContractBgAmount]                  MONEY               NOT NULL,
    [ContractSelfAmount]                MONEY               NOT NULL,
    [CurrentEuAmount]                   MONEY               NOT NULL,
    [CurrentBgAmount]                   MONEY               NOT NULL,
    [CurrentSelfAmount]                 MONEY               NOT NULL,

    [InterventionFieldId]               INT                 NOT NULL,
    [FormOfFinanceId]                   INT                 NOT NULL,
    [TerritorialDimensionId]            INT                 NOT NULL,
    [TerritorialDeliveryMechanismId]    INT                 NOT NULL,
    [ThematicObjectiveId]               INT                 NOT NULL,
    [ESFSecondaryThemeId]               INT                 NOT NULL,
    [EconomicDimensionId]               INT                 NOT NULL,

    [NutsCode]                          NVARCHAR(MAX)       NOT NULL,
    [NutsName]                          NVARCHAR(MAX)       NOT NULL,
    [NutsFullPath]                      NVARCHAR(MAX)       NOT NULL,
    [NutsFullPathName]                  NVARCHAR(MAX)       NOT NULL,

    CONSTRAINT [PK_ContractVersionXmlAmounts]                              PRIMARY KEY ([ContractVersionXmlAmountId]),
    CONSTRAINT [UQ_ContractVersionXmlAmounts_ContractVersionXmlId_Gid]     UNIQUE      ([ContractVersionXmlId], [Gid]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_ContractVersionXmls]          FOREIGN KEY ([ContractVersionXmlId])           REFERENCES [dbo].[ContractVersionXmls] ([ContractVersionXmlId]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_Contracts]                    FOREIGN KEY ([ContractId])                     REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_ProcedureBudgetLevel2]        FOREIGN KEY ([ProcedureBudgetLevel2Id])        REFERENCES [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_InterventionField]            FOREIGN KEY ([InterventionFieldId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_FormOfFinance]                FOREIGN KEY ([FormOfFinanceId])                REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_TerritorialDimension]         FOREIGN KEY ([TerritorialDimensionId])         REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_TerritorialDeliveryMechanism] FOREIGN KEY ([TerritorialDeliveryMechanismId]) REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_ThematicObjective]            FOREIGN KEY ([ThematicObjectiveId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_ESFSecondaryTheme]            FOREIGN KEY ([ESFSecondaryThemeId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractVersionXmlAmounts_EconomicDimension]            FOREIGN KEY ([EconomicDimensionId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId])
);
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as i2,
    N'http://ereg.egov.bg/segment/R-10036' as pb,
    N'http://ereg.egov.bg/segment/R-10035' as peb,
    N'http://ereg.egov.bg/segment/R-10034' as pdeb,
    N'http://ereg.egov.bg/segment/R-10033' as n,
    N'http://ereg.egov.bg/segment/R-10032' as a,
    N'http://ereg.egov.bg/segment/R-09989' as nfpn
),
BudgetLevel3s as
(
    SELECT
        cv.ContractVersionXmlId                                                               as ContractVersionXmlId,
        cv.ContractId                                                                         as ContractId,
        s.value('(../@gid)[1]'                                          , 'UNIQUEIDENTIFIER') as ProcedureBudgetLevel2Gid,
        s.value('(n:OrderNum)[1]'                                       , 'INT'             ) as OrderNum,
        s.value('(@gid)[1]'                                             , 'UNIQUEIDENTIFIER') as Gid,
        s.value('(@isActive)[1]'                                        , 'BIT'             ) as IsActive,
        s.value('(n:Name)[1]'                                           , 'NVARCHAR(MAX)'   ) as Name,
        s.value('(n:GrandAmounts/n:EUAmounts/a:ContractValue)[1]'       , 'MONEY'           ) as ContractEuAmount,
        s.value('(n:GrandAmounts/n:NationalAmounts/a:ContractValue)[1]' , 'MONEY'           ) as ContractBgAmount,
        s.value('(n:SelfAmounts/a:ContractValue)[1]'                    , 'MONEY'           ) as ContractSelfAmount,
        s.value('(n:GrandAmounts/n:EUAmounts/a:CurrentState)[1]'        , 'MONEY'           ) as CurrentEuAmount,
        s.value('(n:GrandAmounts/n:NationalAmounts/a:CurrentState)[1]'  , 'MONEY'           ) as CurrentBgAmount,
        s.value('(n:SelfAmounts/a:CurrentState)[1]'                     , 'MONEY'           ) as CurrentSelfAmount,
        s.value('(n:InterventionFieldCode)[1]'                          , 'NVARCHAR(MAX)'   ) as InterventionFieldCode,
        s.value('(n:FormOfFinanceCode)[1]'                              , 'NVARCHAR(MAX)'   ) as FormOfFinanceCode,
        s.value('(n:TerritorialDimensionCode)[1]'                       , 'NVARCHAR(MAX)'   ) as TerritorialDimensionCode,
        s.value('(n:TerritorialDeliveryMechanismCode)[1]'               , 'NVARCHAR(MAX)'   ) as TerritorialDeliveryMechanismCode,
        s.value('(n:ThematicObjectiveCode)[1]'                          , 'NVARCHAR(MAX)'   ) as ThematicObjectiveCode,
        s.value('(n:ESFSecondaryThemeCode)[1]'                          , 'NVARCHAR(MAX)'   ) as ESFSecondaryThemeCode,
        s.value('(n:EconomicDimensionCode)[1]'                          , 'NVARCHAR(MAX)'   ) as EconomicDimensionCode,
        s.value('(n:Nuts/nfpn:Code)[1]'                                 , 'NVARCHAR(MAX)'   ) as NutsCode,
        s.value('(n:Nuts/nfpn:Name)[1]'                                 , 'NVARCHAR(MAX)'   ) as NutsName,
        s.value('(n:Nuts/nfpn:FullPath)[1]'                             , 'NVARCHAR(MAX)'   ) as NutsFullPath,
        s.value('(n:Nuts/nfpn:FullPathName)[1]'                         , 'NVARCHAR(MAX)'   ) as NutsFullPathName
    FROM [dbo].[ContractVersionXmls] cv
    OUTER APPLY cv.[Xml].nodes('(/BFPContract/i2:BFPContractDimensionBudgetContract/i2:BFPContractBudget/pb:BFPContractProgrammeBudget/peb:BFPContractProgrammeExpenseBudget/pdeb:BFPContractProgrammeDetailsExpenseBudget)') a(s)
    WHERE cv.[Status] != 1
)
INSERT INTO [dbo].[ContractVersionXmlAmounts]
SELECT
    ContractVersionXmlId = bl3.ContractVersionXmlId,
    ContractId = bl3.ContractId,
    ProcedureBudgetLevel2Id = pbl2.ProcedureBudgetLevel2Id,
    OrderNum = bl3.OrderNum,
    Gid = bl3.Gid,
    IsActive = bl3.IsActive,
    Name = bl3.Name,
    ContractEuAmount = bl3.ContractEuAmount,
    ContractBgAmount = bl3.ContractBgAmount,
    ContractSelfAmount = bl3.ContractSelfAmount,
    CurrentEuAmount = bl3.CurrentEuAmount,
    CurrentBgAmount = bl3.CurrentBgAmount,
    CurrentSelfAmount = bl3.CurrentSelfAmount,
    InterventionFieldId = ics1.[InterventionCategoryId],
    FormOfFinanceId = ics2.[InterventionCategoryId],
    TerritorialDimensionId = ics3.[InterventionCategoryId],
    TerritorialDeliveryMechanismId = ics4.[InterventionCategoryId],
    ThematicObjectiveId = ics5.[InterventionCategoryId],
    ESFSecondaryThemeId = ics6.[InterventionCategoryId],
    EconomicDimensionId = ics7.[InterventionCategoryId],
    NutsCode = bl3.NutsCode,
    NutsName = bl3.NutsName,
    NutsFullPath = bl3.NutsFullPath,
    NutsFullPathName = bl3.NutsFullPathName
FROM
BudgetLevel3s bl3
JOIN [dbo].[ProcedureBudgetLevel2]  pbl2 on bl3.ProcedureBudgetLevel2Gid = pbl2.Gid
JOIN [dbo].[InterventionCategories] ics1 on bl3.InterventionFieldCode = ics1.[Code]
JOIN [dbo].[InterventionCategories] ics2 on bl3.FormOfFinanceCode = ics2.[Code]
JOIN [dbo].[InterventionCategories] ics3 on bl3.TerritorialDimensionCode = ics3.[Code]
JOIN [dbo].[InterventionCategories] ics4 on bl3.TerritorialDeliveryMechanismCode = ics4.[Code]
JOIN [dbo].[InterventionCategories] ics5 on bl3.ThematicObjectiveCode = ics5.[Code]
JOIN [dbo].[InterventionCategories] ics6 on bl3.ESFSecondaryThemeCode = ics6.[Code]
JOIN [dbo].[InterventionCategories] ics7 on bl3.EconomicDimensionCode = ics7.[Code]
WHERE ics1.[Dimension] = 1 and
      ics2.[Dimension] = 2 and
      ics3.[Dimension] = 3 and
      ics4.[Dimension] = 4 and
      ics5.[Dimension] = 5 and
      ics6.[Dimension] = 6 and
      ics7.[Dimension] = 7

