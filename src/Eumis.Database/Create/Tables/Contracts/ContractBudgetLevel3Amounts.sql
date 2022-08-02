PRINT 'ContractBudgetLevel3Amounts'
GO

CREATE TABLE [dbo].[ContractBudgetLevel3Amounts] (
    [ContractBudgetLevel3AmountId]      INT                 NOT NULL IDENTITY,
    [ContractId]                        INT                 NOT NULL,
    [ProcedureBudgetLevel2Id]           INT                 NOT NULL,
    [OrderNum]                          INT                 NOT NULL,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL,
    [IsActive]                          BIT                 NOT NULL,

    [Code]                              NVARCHAR(MAX)       NOT NULL,
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

    CONSTRAINT [PK_ContractBudgetLevel3Amounts]                              PRIMARY KEY ([ContractBudgetLevel3AmountId]),
    CONSTRAINT [UQ_ContractBudgetLevel3Amounts_ContractId_Gid]               UNIQUE ([ContractId], [Gid]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_Contracts]                    FOREIGN KEY ([ContractId])                     REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_ProcedureBudgetLevel2]        FOREIGN KEY ([ProcedureBudgetLevel2Id])        REFERENCES [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_InterventionField]            FOREIGN KEY ([InterventionFieldId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_FormOfFinance]                FOREIGN KEY ([FormOfFinanceId])                REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_TerritorialDimension]         FOREIGN KEY ([TerritorialDimensionId])         REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_TerritorialDeliveryMechanism] FOREIGN KEY ([TerritorialDeliveryMechanismId]) REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_ThematicObjective]            FOREIGN KEY ([ThematicObjectiveId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_ESFSecondaryTheme]            FOREIGN KEY ([ESFSecondaryThemeId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId]),
    CONSTRAINT [FK_ContractBudgetLevel3Amounts_EconomicDimension]            FOREIGN KEY ([EconomicDimensionId])            REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId])
);
GO

exec spDescTable  N'ContractBudgetLevel3Amounts', N'Суми на ред от бюджета на договор на трето ниво.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'ContractBudgetLevel3AmountId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'ContractId'                     , N'Идентификатор на договор.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'ProcedureBudgetLevel2Id'        , N'Идентификатор на ред от бюджета на процедура на второ ниво.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'OrderNum'                       , N'Подредба.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'Gid'                            , N'Публичен идентификатор на ред от бюджета на процедура на трето ниво.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'IsActive'                       , N'Маркер за активност.'

exec spDescColumn N'ContractBudgetLevel3Amounts', N'Code'                           , N'Код на ред от бюджета на договор на трето ниво.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'Name'                           , N'Наименование на ред от бюджета на договор на трето ниво.'

exec spDescColumn N'ContractBudgetLevel3Amounts', N'ContractEuAmount'               , N'Първоначално БФП - ЕС.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'ContractBgAmount'               , N'Първоначално БФП - НФ.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'ContractSelfAmount'             , N'Първоначално Собствено финансиране.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'CurrentEuAmount'                , N'Текущо БФП - ЕС.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'CurrentBgAmount'                , N'Текущо БФП - НФ.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'CurrentSelfAmount'              , N'Текущо Собствено финансиране.'

exec spDescColumn N'ContractBudgetLevel3Amounts', N'InterventionFieldId'            , N'Област на интервенция.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'FormOfFinanceId'                , N'Форма на финансиране.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'TerritorialDimensionId'         , N'Вид на територията.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'TerritorialDeliveryMechanismId' , N'Механизми за териториално изпълнение.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'ThematicObjectiveId'            , N'Тематична цел.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'ESFSecondaryThemeId'            , N'Вторична тема на ЕСФ.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'EconomicDimensionId'            , N'Икономическа дейност.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'NutsCode'                       , N'NUTS код.'
exec spDescColumn N'ContractBudgetLevel3Amounts', N'NutsName'                       , N'NUTS наименование.'
GO
