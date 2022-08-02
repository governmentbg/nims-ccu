PRINT 'HistoricContracts'
GO

CREATE TABLE [dbo].[HistoricContracts] (
    [HistoricContractId]    INT             NOT NULL,
    [ContractIacsRefNum]    NVARCHAR(200)   NOT NULL,
    [ProcedureId]           INT             NOT NULL,
    [ProgrammeId]           INT             NOT NULL,
    [ProgrammePriorityId]   INT             NOT NULL,
    [FinanceSource]         INT             NOT NULL,
    [ModifyDate]            DATETIME2       NOT NULL,
    [RegNumber]             NVARCHAR(200)   NOT NULL,
    [Name]                  NVARCHAR(MAX)   NULL,
    [NameEN]                NVARCHAR(MAX)   NULL,
    [Description]           NVARCHAR(MAX)   NULL,
    [DescriptionEN]         NVARCHAR(MAX)   NULL,
    [CompanyName]           NVARCHAR(MAX)   NULL,
    [CompanyNameEn]         NVARCHAR(MAX)   NULL,
    [CompanyUin]            NVARCHAR(200)   NOT NULL,
    [CompanyUinType]        INT             NOT NULL,
    [CompanyTypeId]         INT             NOT NULL,
    [CompanyLegalTypeId]    INT             NOT NULL,
    [SeatCountryCode]       NVARCHAR(200)   NOT NULL,
    [SeatSettlementCode]    NVARCHAR(10)    NOT NULL,
    [SeatPostCode]          NVARCHAR(50)    NULL,
    [SeatStreet]            NVARCHAR(200)   NULL,
    [SeatAddress]           NVARCHAR(MAX)   NULL,
    [ContractDate]          DATETIME2       NULL,
    [StartDate]             DATETIME2       NULL,
    [CompletionDate]        DATETIME2       NULL,
    [ExecutionStatus]       INT             NULL,
    [NutsLevel]             INT             NULL

    CONSTRAINT [PK_HistoricContracts]                       PRIMARY KEY ([HistoricContractId]),
    CONSTRAINT [FK_HistoricContracts_Programmes]            FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_HistoricContracts_ProgrammePriorities]   FOREIGN KEY ([ProgrammePriorityId]) REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_HistoricContracts_CompanyType]           FOREIGN KEY ([CompanyTypeId])       REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [FK_HistoricContracts_CompanyLegalType]      FOREIGN KEY ([CompanyLegalTypeId])  REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [CHK_HistoricContracts_FinanceSource]        CHECK ([FinanceSource]              IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)),
    CONSTRAINT [CHK_HistoricContracts_CompanyUinType]       CHECK ([CompanyUinType]             IN (0, 1, 2, 3)),
    CONSTRAINT [CHK_HistoricContracts_ExecutionStatus]      CHECK ([ExecutionStatus]            IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_HistoricContracts_NutsLevel]            CHECK ([NutsLevel]                  IN (1, 2, 3, 4, 5, 6, 7))
);
GO

exec spDescTable  N'HistoricContracts', N'Основни данни за договори.'
exec spDescColumn N'HistoricContracts', N'HistoricContractId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContracts', N'ContractIacsRefNum'   , N'Идентификатор на договор в ИСАК.'
exec spDescColumn N'HistoricContracts', N'ProcedureId'          , N'Идентификатор на процедура.'
exec spDescColumn N'HistoricContracts', N'ProgrammeId'          , N'Идентификатор на оперативна програма.'
exec spDescColumn N'HistoricContracts', N'ProgrammePriorityId'  , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'HistoricContracts', N'FinanceSource'        , N'Източник на финансиране: 1 - ЕСФ; 2 - ЕФРР; 3 - КФ; 4 - ИМЗ; 5 - ФЕПНЛ; 6 - ЕФМДР; 7 - ЕЗФРСР; 8 - ФВС; 9 - ФУМИ; 10 - Други.'
exec spDescColumn N'HistoricContracts', N'ModifyDate'           , N'Дата на последна промяна.'
exec spDescColumn N'HistoricContracts', N'RegNumber'            , N'Регистрационен номер.'
exec spDescColumn N'HistoricContracts', N'Name'                 , N'Наименование на проекта.'
exec spDescColumn N'HistoricContracts', N'NameEN'               , N'Наименование на проекта на английски.'
exec spDescColumn N'HistoricContracts', N'Description'          , N'Кратко описание на проекта.'
exec spDescColumn N'HistoricContracts', N'DescriptionEN'        , N'Кратко описание на проекта на английски.'
exec spDescColumn N'HistoricContracts', N'CompanyName'          , N'Наименование на бенефициента.'
exec spDescColumn N'HistoricContracts', N'CompanyNameEn'        , N'Наименование на бенефициента на английски.'
exec spDescColumn N'HistoricContracts', N'CompanyUin'           , N'Булстат/ЕИК на бенефициента.'
exec spDescColumn N'HistoricContracts', N'CompanyUinType'       , N'Вид Булстат/ЕИК на бенефициента: 0 - ЕИК; 1 - БУЛСТАТ; 2 - БУЛСТАТ за свободни професии (ЕГН); 3 - Чуждестранни фирми.'
exec spDescColumn N'HistoricContracts', N'CompanyTypeId'        , N'Идентификатор на тип органицазия.'
exec spDescColumn N'HistoricContracts', N'CompanyLegalTypeId'   , N'Идентификатор на правен статут на организация.'
exec spDescColumn N'HistoricContracts', N'SeatCountryCode'      , N'Седалище на бенефициента - Код на държава (ISO 3166-1 alpha-2).'
exec spDescColumn N'HistoricContracts', N'SeatSettlementCode'   , N'Седалище на бенефициента - Код на населено място (ЕКАТТЕ).'
exec spDescColumn N'HistoricContracts', N'SeatPostCode'         , N'Седалище на бенефициента - Пощенски код.'
exec spDescColumn N'HistoricContracts', N'SeatStreet'           , N'Седалище на бенефициента - Улица (ж.к., кв., №, бл., вх., ет., ап.).'
exec spDescColumn N'HistoricContracts', N'SeatAddress'          , N'Седалище на бенефициента - Адрес в чужбина.'
exec spDescColumn N'HistoricContracts', N'ContractDate'         , N'Дата на сключване на договора/заповедта.'
exec spDescColumn N'HistoricContracts', N'StartDate'            , N'Дата на стартиране.'
exec spDescColumn N'HistoricContracts', N'CompletionDate'       , N'Дата на приключване.'
exec spDescColumn N'HistoricContracts', N'ExecutionStatus'      , N'Статус на изпълнение на договора/заповедта за БФП:  1 - В изпълнение (от дата на стартиране); 2 - В изпълнение (временно спрян); 3 - В изпълнение (под наблюдение); 4 - Прекратен (към дата на прекратяване); 5 - Приключен (към датата на приключване); 6 - Сключен; 7 - Развален.'
exec spDescColumn N'HistoricContracts', N'NutsLevel'            , N'Ниво на местонахожденията: 1 - Държава; 2 - NUTS ниво 1; 3 - NUTS ниво 2; 4 - Област; 5 - Община; 6 - Населено място; 7 - Защитена зона.'
GO

PRINT 'Create HistoricContractSequence'

CREATE SEQUENCE [dbo].[HistoricContractSequence] START WITH 10
GO

PRINT 'HistoricContractActivities'
GO

CREATE TABLE [dbo].[HistoricContractActivities] (
    [HistoricContractActivityId]    INT             NOT NULL,
    [HistoricContractId]            INT             NOT NULL,
    [Activity]                      NVARCHAR(MAX)   NOT NULL

    CONSTRAINT [PK_HistoricContractActivities]                      PRIMARY KEY ([HistoricContractActivityId]),
    CONSTRAINT [FK_HistoricContractActivities_HistoricContracts]    FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractActivities',    N'Дейности по основни данни за договорите.'
exec spDescColumn N'HistoricContractActivities',    N'HistoricContractActivityId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractActivities',    N'HistoricContractId'           , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractActivities',    N'Activity'                     , N'Наименование.'
GO

PRINT 'Create HistoricContractActivitySequence'

CREATE SEQUENCE [dbo].[HistoricContractActivitySequence] START WITH 10
GO

PRINT 'HistoricContractLocations'
GO

CREATE TABLE [dbo].[HistoricContractLocations] (
    [HistoricContractLocationId]    INT             NOT NULL,
    [HistoricContractId]            INT             NOT NULL,
    [CountryCode]                   NVARCHAR(200)   NOT NULL,
    [ProtectedZoneCode]             NVARCHAR(200)   NOT NULL,
    [Nuts1Code]                     NVARCHAR(200)   NOT NULL,
    [Nuts2Code]                     NVARCHAR(200)   NOT NULL,
    [DistrictCode]                  NVARCHAR(200)   NOT NULL,
    [MunicipalityCode]              NVARCHAR(200)   NOT NULL,
    [SettlementCode]                NVARCHAR(10)    NOT NULL,
    [FullPath]                      NVARCHAR(MAX)   NOT NULL,
    [FullPathName]                  NVARCHAR(MAX)   NOT NULL

    CONSTRAINT [PK_HistoricContractLocations]                       PRIMARY KEY ([HistoricContractLocationId]),
    CONSTRAINT [FK_HistoricContractLocations_HistoricContracts]     FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractLocations',     N'Местонахождения.'
exec spDescColumn N'HistoricContractLocations',     N'HistoricContractLocationId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractLocations',     N'HistoricContractId'           , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractLocations',     N'CountryCode'                  , N'Код на държава (ISO 3166-1 alpha-2).'
exec spDescColumn N'HistoricContractLocations',     N'ProtectedZoneCode'            , N'Код на защитена зона.'
exec spDescColumn N'HistoricContractLocations',     N'Nuts1Code'                    , N'NUTS 1 код.'
exec spDescColumn N'HistoricContractLocations',     N'Nuts2Code'                    , N'NUTS 2 код.'
exec spDescColumn N'HistoricContractLocations',     N'DistrictCode'                 , N'Код на област (NUTS 3).'
exec spDescColumn N'HistoricContractLocations',     N'MunicipalityCode'             , N'Код на община (ЕКАТТЕ).'
exec spDescColumn N'HistoricContractLocations',     N'SettlementCode'               , N'Код на населено място (ЕКАТТЕ).'
exec spDescColumn N'HistoricContractLocations',     N'FullPath'                     , N'Пълен път на местонахождение.'
exec spDescColumn N'HistoricContractLocations',     N'FullPathName'                 , N'Пълно наименование на местонахождение.'
GO

PRINT 'Create HistoricContractLocationSequence'

CREATE SEQUENCE [dbo].[HistoricContractLocationSequence] START WITH 10
GO

PRINT 'HistoricContractPartners'
GO

CREATE TABLE [dbo].[HistoricContractPartners] (
    [HistoricContractPartnerId] INT                 NOT NULL,
    [HistoricContractId]        INT                 NOT NULL,
    [PartnerType]               INT                 NOT NULL,
    [PartnerName]               NVARCHAR(200)       NOT NULL,
    [PartnerNameEn]             NVARCHAR(200)       NOT NULL,
    [PartnerUin]                NVARCHAR(200)       NOT NULL,
    [PartnerUinType]            INT                 NOT NULL,
    [PartnerTypeId]             INT                 NOT NULL,
    [PartnerLegalTypeId]        INT                 NOT NULL,
    [SeatCountryCode]           NVARCHAR(200)       NOT NULL,
    [SeatSettlementCode]        NVARCHAR(10)        NOT NULL,
    [SeatPostCode]              NVARCHAR(50)        NULL,
    [SeatStreet]                NVARCHAR(200)       NULL,
    [SeatAddress]               NVARCHAR(MAX)       NULL

    CONSTRAINT [PK_HistoricContractPartners]                    PRIMARY KEY ([HistoricContractPartnerId]),
    CONSTRAINT [FK_HistoricContractPartners_HistoricContracts]  FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId]),
    CONSTRAINT [FK_HistoricContractPartners_CompanyType]        FOREIGN KEY ([PartnerTypeId])               REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [FK_HistoricContractPartners_CompanyLegalTypes]  FOREIGN KEY ([PartnerLegalTypeId])          REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [CHK_HistoricContractPartners_PartnerType]       CHECK ([PartnerType]                        IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_HistoricContractPartners_PartnerUinType]    CHECK ([PartnerUinType]                     IN (0, 1, 2, 3))
);
GO

exec spDescTable  N'HistoricContractPartners',  N'Партньори.'
exec spDescColumn N'HistoricContractPartners',  N'HistoricContractPartnerId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractPartners',  N'HistoricContractId'       , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractPartners',  N'PartnerType'              , N'Вид на партньора: 1 - Партньор; 2 - Изпълнител; 3 - Подизпълнител; 4 - Членове на обединение.'
exec spDescColumn N'HistoricContractPartners',  N'PartnerName'              , N'Наименование.'
exec spDescColumn N'HistoricContractPartners',  N'PartnerNameEn'            , N'Наименование на английски.'
exec spDescColumn N'HistoricContractPartners',  N'PartnerUin'               , N'Булстат/ЕИК.'
exec spDescColumn N'HistoricContractPartners',  N'PartnerUinType'           , N'Вид Булстат/ЕИК: 0 - ЕИК; 1 - БУЛСТАТ; 2 - БУЛСТАТ за свободни професии (ЕГН); 3 - Чуждестранни фирми.'
exec spDescColumn N'HistoricContractPartners',  N'PartnerTypeId'            , N'Идентификатор на тип органицазия.'
exec spDescColumn N'HistoricContractPartners',  N'PartnerLegalTypeId'       , N'Идентификатор на правен статут на организация.'
exec spDescColumn N'HistoricContractPartners',  N'SeatCountryCode'          , N'Седалище - Код на държава (ISO 3166-1 alpha-2).'
exec spDescColumn N'HistoricContractPartners',  N'SeatSettlementCode'       , N'Седалище - Код на населено място (ЕКАТТЕ).'
exec spDescColumn N'HistoricContractPartners',  N'SeatPostCode'             , N'Седалище - Пощенски код.'
exec spDescColumn N'HistoricContractPartners',  N'SeatStreet'               , N'Седалище - Улица (ж.к., кв., №, бл., вх., ет., ап.).'
exec spDescColumn N'HistoricContractPartners',  N'SeatAddress'              , N'Седалище - Адрес в чужбина.'
GO

PRINT 'Create HistoricContractPartnerSequence'

CREATE SEQUENCE [dbo].[HistoricContractPartnerSequence] START WITH 10
GO

PRINT 'HistoricContractProcurementPlans'
GO

CREATE TABLE [dbo].[HistoricContractProcurementPlans] (
    [HistoricContractProcurementPlanId]     INT             NOT NULL,
    [HistoricContractId]                    INT             NOT NULL,
    [ProcurementPlanName]                   NVARCHAR(MAX)   NOT NULL,
    [Amount]                                MONEY           NOT NULL

    CONSTRAINT [PK_HistoricContractProcurementPlans]                    PRIMARY KEY ([HistoricContractProcurementPlanId]),
    CONSTRAINT [FK_HistoricContractProcurementPlans_HistoricContracts]  FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractProcurementPlans',  N'Тръжни процедури.'
exec spDescColumn N'HistoricContractProcurementPlans',  N'HistoricContractProcurementPlanId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractProcurementPlans',  N'HistoricContractId'               , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractProcurementPlans',  N'ProcurementPlanName'              , N'Предмет на предвидената процедура.'
exec spDescColumn N'HistoricContractProcurementPlans',  N'Amount'                           , N'Прогнозна стойност.'
GO

PRINT 'Create HistoricContractProcurementPlanSequence'

CREATE SEQUENCE [dbo].[HistoricContractProcurementPlanSequence] START WITH 10
GO

PRINT 'HistoricContractProcurementPlanPositions'
GO

CREATE TABLE [dbo].[HistoricContractProcurementPlanPositions] (
    [HistoricContractProcurementPlanPositionId]     INT             NOT NULL,
    [HistoricContractProcurementPlanId]             INT             NOT NULL,
    [PositionName]                                  NVARCHAR(200)   NOT NULL

    CONSTRAINT [PK_HistoricContractProcurementPlanPositions]                                    PRIMARY KEY ([HistoricContractProcurementPlanPositionId]),
    CONSTRAINT [FK_HistoricContractProcurementPlanPositions_HistoricContractProcurementPlans]   FOREIGN KEY ([HistoricContractProcurementPlanId])           REFERENCES [dbo].[HistoricContractProcurementPlans] ([HistoricContractProcurementPlanId])
);
GO

exec spDescTable  N'HistoricContractProcurementPlanPositions',  N'Обособени позиции на тръжни процедури.'
exec spDescColumn N'HistoricContractProcurementPlanPositions',  N'HistoricContractProcurementPlanPositionId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractProcurementPlanPositions',  N'HistoricContractProcurementPlanId'            , N'Идентификатор на тръжна процедура.'
exec spDescColumn N'HistoricContractProcurementPlanPositions',  N'PositionName'                                 , N'Наименование.'
GO

PRINT 'Create HistoricContractProcurementPlanPositionSequence'

CREATE SEQUENCE [dbo].[HistoricContractProcurementPlanPositionSequence] START WITH 10
GO

PRINT 'HistoricContractContractedAmounts'
GO

CREATE TABLE [dbo].[HistoricContractContractedAmounts] (
    [HistoricContractContractedAmountId]    INT         NOT NULL,
    [HistoricContractId]                    INT         NOT NULL,
    [ContractedDate]                        DATETIME2   NOT NULL,
    [ContractedEuAmount]                    MONEY       NULL,
    [ContractedBgAmount]                    MONEY       NULL,
    [ContractedSeftAmount]                  MONEY       NULL,
    [IsLast]                                BIT         NOT NULL

    CONSTRAINT [PK_HistoricContractContractedAmounts]                       PRIMARY KEY ([HistoricContractContractedAmountId]),
    CONSTRAINT [FK_HistoricContractContractedAmounts_HistoricContracts]     FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractContractedAmounts', N'Договорени суми.'
exec spDescColumn N'HistoricContractContractedAmounts', N'HistoricContractContractedAmountId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractContractedAmounts', N'HistoricContractId'                   , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractContractedAmounts', N'ContractedDate'                       , N'Дата на договориране/промяна.'
exec spDescColumn N'HistoricContractContractedAmounts', N'ContractedEuAmount'                   , N'Безвъзмездна финансова помощ - Европейски съюз.'
exec spDescColumn N'HistoricContractContractedAmounts', N'ContractedBgAmount'                   , N'Безвъзмездна финансова помощ - Национално финансиране.'
exec spDescColumn N'HistoricContractContractedAmounts', N'ContractedSeftAmount'                 , N'Собствено финансиране.'
exec spDescColumn N'HistoricContractContractedAmounts', N'IsLast'                               , N'Маркер за последен запис.'
GO

PRINT 'Create HistoricContractContractedAmountSequence'

CREATE SEQUENCE [dbo].[HistoricContractContractedAmountSequence] START WITH 10
GO

PRINT 'HistoricContractActuallyPaidAmounts'
GO

CREATE TABLE [dbo].[HistoricContractActuallyPaidAmounts] (
    [HistoricContractActuallyPaidAmountId]  INT             NOT NULL,
    [HistoricContractId]                    INT             NOT NULL,
    [PaymentDate]                           DATETIME2       NOT NULL,
    [PaidEuAmount]                          MONEY           NULL,
    [PaidBgAmount]                          MONEY           NULL

    CONSTRAINT [PK_HistoricContractActuallyPaidAmounts]                     PRIMARY KEY ([HistoricContractActuallyPaidAmountId]),
    CONSTRAINT [FK_HistoricContractActuallyPaidAmounts_HistoricContracts]   FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractActuallyPaidAmounts'    , N'Реално изплатени суми.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'HistoricContractActuallyPaidAmountId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'HistoricContractId'                     , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'PaymentDate'                            , N'Дата.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'PaidEuAmount'                           , N'Безвъзмездна финансова помощ - Европейски съюз.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'PaidBgAmount'                           , N'Безвъзмездна финансова помощ - Национално финансиране.'
GO

PRINT 'Create HistoricContractActuallyPaidAmountSequence'

CREATE SEQUENCE [dbo].[HistoricContractActuallyPaidAmountSequence] START WITH 10
GO

PRINT 'HistoricContractReimbursedAmounts'
GO

CREATE TABLE [dbo].[HistoricContractReimbursedAmounts] (
    [HistoricContractReimbursedAmountId]    INT         NOT NULL,
    [HistoricContractId]                    INT         NOT NULL,
    [ReimbursementDate]                     DATETIME2   NOT NULL,
    [ReimbursedPrincipalEuAmount]           MONEY       NULL,
    [ReimbursedPrincipalBgAmount]           MONEY       NULL

    CONSTRAINT [PK_HistoricContractReimbursedAmounts]                       PRIMARY KEY ([HistoricContractReimbursedAmountId]),
    CONSTRAINT [FK_HistoricContractReimbursedAmounts_HistoricContracts]     FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractReimbursedAmounts', N'Възстановени суми.'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'HistoricContractReimbursedAmountId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'HistoricContractId'                   , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'ReimbursementDate'                    , N'Дата на възстановяване.'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'ReimbursedPrincipalEuAmount'          , N'Възстановена БПФ - ЕС (Главница).'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'ReimbursedPrincipalBgAmount'          , N'Възстановена БПФ - НФ (Главница).'
GO

PRINT 'Create HistoricContractReimbursedAmountSequence'

CREATE SEQUENCE [dbo].[HistoricContractReimbursedAmountSequence] START WITH 10
GO
