PRINT 'ContractVersionXmlAmounts'
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

exec spDescTable  N'ContractVersionXmlAmounts', N'Суми на ред от бюджета на версия договор на трето ниво.'
exec spDescColumn N'ContractVersionXmlAmounts', N'ContractVersionXmlAmountId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractVersionXmlAmounts', N'ContractVersionXmlId'           , N'Идентификатор на версия на договор.'
exec spDescColumn N'ContractVersionXmlAmounts', N'ContractId'                     , N'Идентификатор на договор.'
exec spDescColumn N'ContractVersionXmlAmounts', N'ProcedureBudgetLevel2Id'        , N'Идентификатор на ред от бюджета на процедура на второ ниво.'
exec spDescColumn N'ContractVersionXmlAmounts', N'OrderNum'                       , N'Подредба.'
exec spDescColumn N'ContractVersionXmlAmounts', N'Gid'                            , N'Публичен идентификатор на ред от бюджета на процедура на трето ниво.'
exec spDescColumn N'ContractVersionXmlAmounts', N'IsActive'                       , N'Маркер за активност.'

exec spDescColumn N'ContractVersionXmlAmounts', N'Name'                           , N'Наименование на ред от бюджета на договор на трето ниво.'

exec spDescColumn N'ContractVersionXmlAmounts', N'ContractEuAmount'               , N'Първоначално БФП - ЕС.'
exec spDescColumn N'ContractVersionXmlAmounts', N'ContractBgAmount'               , N'Първоначално БФП - НФ.'
exec spDescColumn N'ContractVersionXmlAmounts', N'ContractSelfAmount'             , N'Първоначално Собствено финансиране.'
exec spDescColumn N'ContractVersionXmlAmounts', N'CurrentEuAmount'                , N'Текущо БФП - ЕС.'
exec spDescColumn N'ContractVersionXmlAmounts', N'CurrentBgAmount'                , N'Текущо БФП - НФ.'
exec spDescColumn N'ContractVersionXmlAmounts', N'CurrentSelfAmount'              , N'Текущо Собствено финансиране.'

exec spDescColumn N'ContractVersionXmlAmounts', N'InterventionFieldId'            , N'Област на интервенция.'
exec spDescColumn N'ContractVersionXmlAmounts', N'FormOfFinanceId'                , N'Форма на финансиране.'
exec spDescColumn N'ContractVersionXmlAmounts', N'TerritorialDimensionId'         , N'Вид на територията.'
exec spDescColumn N'ContractVersionXmlAmounts', N'TerritorialDeliveryMechanismId' , N'Механизми за териториално изпълнение.'
exec spDescColumn N'ContractVersionXmlAmounts', N'ThematicObjectiveId'            , N'Тематична цел.'
exec spDescColumn N'ContractVersionXmlAmounts', N'ESFSecondaryThemeId'            , N'Вторична тема на ЕСФ.'
exec spDescColumn N'ContractVersionXmlAmounts', N'EconomicDimensionId'            , N'Икономическа дейност.'
exec spDescColumn N'ContractVersionXmlAmounts', N'NutsCode'                       , N'NUTS код.'
exec spDescColumn N'ContractVersionXmlAmounts', N'NutsName'                       , N'NUTS наименование.'
GO
