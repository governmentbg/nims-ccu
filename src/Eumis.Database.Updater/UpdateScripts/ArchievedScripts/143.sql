GO

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts]
ADD [InterventionFieldId]               INT                 NOT NULL CONSTRAINT DEFAULT_InterventionFieldId            DEFAULT 1,
    [FormOfFinanceId]                   INT                 NOT NULL CONSTRAINT DEFAULT_FormOfFinanceId                DEFAULT 1,
    [TerritorialDimensionId]            INT                 NOT NULL CONSTRAINT DEFAULT_TerritorialDimensionId         DEFAULT 1,
    [TerritorialDeliveryMechanismId]    INT                 NOT NULL CONSTRAINT DEFAULT_TerritorialDeliveryMechanismId DEFAULT 1,
    [ThematicObjectiveId]               INT                 NOT NULL CONSTRAINT DEFAULT_ThematicObjectiveId            DEFAULT 1,
    [ESFSecondaryThemeId]               INT                 NOT NULL CONSTRAINT DEFAULT_ESFSecondaryThemeId            DEFAULT 1,
    [EconomicDimensionId]               INT                 NOT NULL CONSTRAINT DEFAULT_EconomicDimensionId            DEFAULT 1,
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_InterventionField]            FOREIGN KEY ([InterventionFieldId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_FormOfFinance]                FOREIGN KEY ([FormOfFinanceId])                REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_TerritorialDimension]         FOREIGN KEY ([TerritorialDimensionId])         REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_TerritorialDeliveryMechanism] FOREIGN KEY ([TerritorialDeliveryMechanismId]) REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_ThematicObjective]            FOREIGN KEY ([ThematicObjectiveId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_ESFSecondaryTheme]            FOREIGN KEY ([ESFSecondaryThemeId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_EconomicDimension]            FOREIGN KEY ([EconomicDimensionId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]);
GO

UPDATE cba
SET [InterventionFieldId] = ific.[InterventionCategoryId],
    [FormOfFinanceId] = ffic.[InterventionCategoryId],
    [TerritorialDimensionId] = tdic.[InterventionCategoryId],
    [TerritorialDeliveryMechanismId] = tdmic.[InterventionCategoryId],
    [ThematicObjectiveId] = toic.[InterventionCategoryId],
    [ESFSecondaryThemeId] = esfic.[InterventionCategoryId],
    [EconomicDimensionId] = edic.[InterventionCategoryId]
FROM [dbo].[ContractBudgetLevel3Amounts] cba

JOIN [dbo].[ContractBudgetLevel3ICs] ifbic ON cba.[ContractBudgetLevel3AmountId] = ifbic.[ContractBudgetLevel3AmountId]
JOIN [dbo].[InterventionCategories] ific ON ifbic.[InterventionCategoryId] = ific.[InterventionCategoryId]

JOIN [dbo].[ContractBudgetLevel3ICs] ffbic ON cba.[ContractBudgetLevel3AmountId] = ffbic.[ContractBudgetLevel3AmountId]
JOIN [dbo].[InterventionCategories] ffic ON ffbic.[InterventionCategoryId] = ffic.[InterventionCategoryId]

JOIN [dbo].[ContractBudgetLevel3ICs] tdbic ON cba.[ContractBudgetLevel3AmountId] = tdbic.[ContractBudgetLevel3AmountId]
JOIN [dbo].[InterventionCategories] tdic ON tdbic.[InterventionCategoryId] = tdic.[InterventionCategoryId]

JOIN [dbo].[ContractBudgetLevel3ICs] tdmbic ON cba.[ContractBudgetLevel3AmountId] = tdmbic.[ContractBudgetLevel3AmountId]
JOIN [dbo].[InterventionCategories] tdmic ON tdmbic.[InterventionCategoryId] = tdmic.[InterventionCategoryId]

JOIN [dbo].[ContractBudgetLevel3ICs] tobic ON cba.[ContractBudgetLevel3AmountId] = tobic.[ContractBudgetLevel3AmountId]
JOIN [dbo].[InterventionCategories] toic ON tobic.[InterventionCategoryId] = toic.[InterventionCategoryId]

JOIN [dbo].[ContractBudgetLevel3ICs] esfbic ON cba.[ContractBudgetLevel3AmountId] = esfbic.[ContractBudgetLevel3AmountId]
JOIN [dbo].[InterventionCategories] esfic ON esfbic.[InterventionCategoryId] = esfic.[InterventionCategoryId]

JOIN [dbo].[ContractBudgetLevel3ICs] edbic ON cba.[ContractBudgetLevel3AmountId] = edbic.[ContractBudgetLevel3AmountId]
JOIN [dbo].[InterventionCategories] edic ON edbic.[InterventionCategoryId] = edic.[InterventionCategoryId]


WHERE ific.[Dimension] = 1 AND ffic.[Dimension] = 2 AND tdic.[Dimension] = 3 AND tdmic.[Dimension] = 4 AND toic.[Dimension] = 5 AND esfic.[Dimension] = 6 AND edic.[Dimension] = 7;
GO

DROP TABLE [ContractBudgetLevel3ICs];
GO

ALTER TABLE [dbo].[ContractBudgetLevel3Amounts]
DROP CONSTRAINT DEFAULT_InterventionFieldId,
     CONSTRAINT DEFAULT_FormOfFinanceId,
     CONSTRAINT DEFAULT_TerritorialDimensionId,
     CONSTRAINT DEFAULT_TerritorialDeliveryMechanismId,
     CONSTRAINT DEFAULT_ThematicObjectiveId,
     CONSTRAINT DEFAULT_ESFSecondaryThemeId,
     CONSTRAINT DEFAULT_EconomicDimensionId;
GO