PRINT 'IrregularityVersions'
GO

CREATE TABLE [dbo].[IrregularityVersions] (
    [IrregularityVersionId]               INT                 NOT NULL IDENTITY,
    [IrregularityId]                      INT                 NOT NULL,
    [OrderNum]                            INT                 NOT NULL,
    [Status]                              INT                 NOT NULL,
    [RegNumber]                           NVARCHAR(200)       NULL,

    [IrregularityCategoryId]              INT                 NULL,
    [IrregularityTypeId]                  INT                 NULL,
    [IrregularityClassification]          INT                 NULL,
    [IrregularityDateFrom]                DATETIME2           NOT NULL,
    [IrregularityDateTo]                  DATETIME2           NULL,

    [EndingActRegNum]                     NVARCHAR(50)        NULL,
    [EndingActDate]                       DATETIME2           NULL,
    [CaseState]                           INT                 NULL,
    [IrregularityEndDate]                 DATETIME2           NULL,

    [ReportYear]                          INT                 NOT NULL,
    [ReportQuarter]                       INT                 NOT NULL,
    [Rapporteur]                          INT                 NULL,
    [RapporteurComments]                  NVARCHAR(MAX)       NULL,
    [IsNewUnlawfulPractice]               BIT                 NULL,
    [ShouldInformOther]                   BIT                 NULL,
    [ProcedureStatus]                     INT                 NULL,
    [FinancialStatusId]                   INT                 NULL,

    [ImpairedRegulationAct]               INT                 NULL,
    [ImpairedRegulationNum]               NVARCHAR(50)        NULL,
    [ImpairedRegulationYear]              INT                 NULL,
    [ImpairedRegulation]                  NVARCHAR(100)       NULL,
    [ImpairedNationalRegulation]          NVARCHAR(MAX)       NULL,

    [AppliedPractices]                    NVARCHAR(MAX)       NULL,
    [BeneficiaryData]                     NVARCHAR(MAX)       NULL,
    [AdminAscertainments]                 NVARCHAR(MAX)       NULL,
    [IrregularityDetectedBy]              NVARCHAR(500)       NULL,

    [EUCoFinancingPercent]                DECIMAL(15,3)       NULL,

    [ExpensesBfpEuAmountLv]               MONEY               NULL,
    [ExpensesBfpBgAmountLv]               MONEY               NULL,
    [ExpensesBfpTotalAmountLv]            MONEY               NULL,
    [ExpensesSelfAmountLv]                MONEY               NULL,
    [ExpensesTotalAmountLv]               MONEY               NULL,

    [ExpensesBfpEuAmountEuro]             MONEY               NULL,
    [ExpensesBfpBgAmountEuro]             MONEY               NULL,
    [ExpensesBfpTotalAmountEuro]          MONEY               NULL,
    [ExpensesSelfAmountEuro]              MONEY               NULL,
    [ExpensesTotalAmountEuro]             MONEY               NULL,

    [IrregularExpensesBfpEuAmountLv]      MONEY               NULL,
    [IrregularExpensesBfpBgAmountLv]      MONEY               NULL,
    [IrregularExpensesBfpTotalAmountLv]   MONEY               NULL,

    [IrregularExpensesBfpEuAmountEuro]    MONEY               NULL,
    [IrregularExpensesBfpBgAmountEuro]    MONEY               NULL,
    [IrregularExpensesBfpTotalAmountEuro] MONEY               NULL,

    [CertifiedExpensesBfpEuAmountLv]      MONEY               NULL,
    [CertifiedExpensesBfpBgAmountLv]      MONEY               NULL,
    [CertifiedExpensesBfpTotalAmountLv]   MONEY               NULL,

    [CertifiedExpensesBfpEuAmountEuro]    MONEY               NULL,
    [CertifiedExpensesBfpBgAmountEuro]    MONEY               NULL,
    [CertifiedExpensesBfpTotalAmountEuro] MONEY               NULL,

    [PaidBfpEuAmountLv]                   MONEY               NULL,
    [PaidBfpBgAmountLv]                   MONEY               NULL,
    [PaidBfpTotalAmountLv]                MONEY               NULL,

    [PaidBfpEuAmountEuro]                 MONEY               NULL,
    [PaidBfpBgAmountEuro]                 MONEY               NULL,
    [PaidBfpTotalAmountEuro]              MONEY               NULL,

    [ContractDebtStatus]                  INT                 NULL,
    [ShouldDecertifyIrregularExpenses]    BIT                 NULL,
    [DecertificationComments]             NVARCHAR(MAX)       NULL,

    [SanctionProcedureType]               INT                 NOT NULL,
    [SanctionProcedureKind]               INT                 NULL,
    [SanctionProcedureStartDate]          DATETIME2           NULL,
    [SanctionProcedureExpectedEndDate]    DATETIME2           NULL,
    [SanctionProcedureEndDate]            DATETIME2           NULL,
    [SanctionProcedureStatus]             INT                 NULL,
    [SanctionCategoryId]                  INT                 NULL,
    [SanctionTypeId]                      INT                 NULL,
    [SanctionFines]                       NVARCHAR(MAX)       NULL,

    [AdminProcedures]                     NVARCHAR(500)       NULL,
    [PenaltyProcedures]                   NVARCHAR(500)       NULL,

    [ShouldReportToOlaf]                  BIT                 NOT NULL,
    [ReasonNotReportingToOlaf]            INT                 NULL,
    [CheckTime]                           INT                 NULL,

    [CreateDate]                          DATETIME2           NOT NULL,
    [ModifyDate]                          DATETIME2           NOT NULL,
    [Version]                             ROWVERSION          NOT NULL

    CONSTRAINT [PK_IrregularityVersions]                        PRIMARY KEY ([IrregularityVersionId]),
    CONSTRAINT [FK_IrregularityVersions_Irregularities]         FOREIGN KEY ([IrregularityId])          REFERENCES [dbo].[Irregularities]                 ([IrregularityId]),
    CONSTRAINT [FK_IrregularityVersions_IrregularityCategories] FOREIGN KEY ([IrregularityCategoryId])  REFERENCES [dbo].[IrregularityCategories]         ([IrregularityCategoryId]),
    CONSTRAINT [FK_IrregularityVersions_IrregularityTypes]      FOREIGN KEY ([IrregularityTypeId])      REFERENCES [dbo].[IrregularityTypes]              ([IrregularityTypeId]),
    CONSTRAINT [FK_IrregularityVersions_FinancialStatuses]      FOREIGN KEY ([FinancialStatusId])       REFERENCES [dbo].[IrregularityFinancialStatuses]  ([IrregularityFinancialStatusId]),
    CONSTRAINT [FK_IrregularityVersions_SanctionCategories]     FOREIGN KEY ([SanctionCategoryId])      REFERENCES [dbo].[IrregularitySanctionCategories] ([IrregularitySanctionCategoryId]),
    CONSTRAINT [FK_IrregularityVersions_SanctionTypes]          FOREIGN KEY ([SanctionTypeId])          REFERENCES [dbo].[IrregularitySanctionTypes]      ([IrregularitySanctionTypeId]),
    CONSTRAINT [CHK_IrregularityVersions_IrregularityClassification]  CHECK ([IrregularityClassification] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_IrregularityVersions_Status]                      CHECK ([Status]                     IN (1, 2, 3)),
    CONSTRAINT [CHK_IrregularityVersions_CaseState]                   CHECK ([CaseState]                  IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_IrregularityVersions_ReportYear]                  CHECK ([ReportYear]                 IN (2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023)),
    CONSTRAINT [CHK_IrregularityVersions_ReportQuarter]               CHECK ([ReportQuarter]              IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_IrregularityVersions_Rapporteur]                  CHECK ([Rapporteur]                 IN (1, 2, 3, 4, 5, 6, 7, 8, 9)),
    CONSTRAINT [CHK_IrregularityVersions_ProcedureStatus]             CHECK ([ProcedureStatus]            IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_IrregularityVersions_ImpairedRegulationAct]       CHECK ([ImpairedRegulationAct]      IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_IrregularityVersions_SanctionProcedureType]       CHECK ([SanctionProcedureType]      IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_IrregularityVersions_SanctionProcedureKind]       CHECK ([SanctionProcedureKind]      IN (1, 2, 3)),
    CONSTRAINT [CHK_IrregularityVersions_SanctionProcedureStatus]     CHECK ([SanctionProcedureStatus]    IN (1, 2, 3)),
    CONSTRAINT [CHK_IrregularityVersions_ReasonNotReportingToOlaf]    CHECK ([ReasonNotReportingToOlaf]   IN (1, 2)),
    CONSTRAINT [CHK_IrregularityVersions_CheckTime]                   CHECK ([CheckTime]                  IN (1, 2, 3)),
    CONSTRAINT [CHK_IrregularityVersions_ContractDebtStatus]          CHECK ([ContractDebtStatus]         IN (1, 2, 3, 4, 5, 6, 7, 8))
);
GO

CREATE UNIQUE INDEX [UQ_IrregularityVersions_Number]
ON [IrregularityVersions]([RegNumber])
WHERE [Status] <> 1;
GO

exec spDescTable  N'IrregularityVersions', N'Версии на нередности.'
exec spDescColumn N'IrregularityVersions', N'IrregularityVersionId'              , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularityVersions', N'IrregularityId'                     , N'Идентификатор на нередност.'
exec spDescColumn N'IrregularityVersions', N'IrregularityCategoryId'             , N'Идентификатор на категория на нередност.'
exec spDescColumn N'IrregularityVersions', N'IrregularityTypeId'                 , N'Идентификатор на тип нередност.'
exec spDescColumn N'IrregularityVersions', N'IrregularityClassification'         , N'Квалификация на нередността: 1 - установена липса на нередност, 2 - нередност, 3 - подозрение за измама, 4 - установена измама.'
exec spDescColumn N'IrregularityVersions', N'IrregularityDateFrom'               , N'Период на извършване на нередността от дата.'
exec spDescColumn N'IrregularityVersions', N'IrregularityDateTo'                 , N'Период на извършване на нередността до дата.'
exec spDescColumn N'IrregularityVersions', N'OrderNum'                           , N'Пореден номер.'
exec spDescColumn N'IrregularityVersions', N'Status'                             , N'Статус: 1 - Чернова, 2 - Актуална, 3 - Архивирана.'
exec spDescColumn N'IrregularityVersions', N'RegNumber'                          , N'Регистрационен номер.'

exec spDescColumn N'IrregularityVersions', N'EndingActRegNum'                    , N'Регистрационен № на акта по чл. 30 за приключване на нередността.'
exec spDescColumn N'IrregularityVersions', N'EndingActDate'                      , N'Дата на акта по чл. 30 за приключване на нередността.'
exec spDescColumn N'IrregularityVersions', N'CaseState'                          , N'Състояние на случая: 1 – активен, 2 – приключен, 3 – прекратен, 4 - отпаднал.'
exec spDescColumn N'IrregularityVersions', N'IrregularityEndDate'                , N'Дата на приключване на нередността.'

exec spDescColumn N'IrregularityVersions', N'ReportYear'                         , N'Година на докладване.'
exec spDescColumn N'IrregularityVersions', N'ReportQuarter'                      , N'Тримесечие на докладване: 1 - Януари - Март; 2 - Април - Юни; 3 - Юли - Септември; 4 - Октомври - Декември.'
exec spDescColumn N'IrregularityVersions', N'Rapporteur'                         , N'Докладващ орган: 1 - MTITC, 2 - МRDPW, 3 - МЕ, 4 - MEW, 5 - MLSP, 6 – MES, 7 – CM, 8 - MAFF, 9 - FEADOP.'
exec spDescColumn N'IrregularityVersions', N'RapporteurComments'                 , N'Коментари от Докладващият орган.'
exec spDescColumn N'IrregularityVersions', N'IsNewUnlawfulPractice'              , N'Нова неправомерна практика.'
exec spDescColumn N'IrregularityVersions', N'ShouldInformOther'                  , N'Hеобходимост да се информират други страни.'
exec spDescColumn N'IrregularityVersions', N'ProcedureStatus'                    , N'Статус на процедурите: 1 - Административни действия; 2 - Съдебно производство; 3 - Наказателно производство; 4 - Приключило производство.'
exec spDescColumn N'IrregularityVersions', N'FinancialStatusId'                  , N'Финансов статус.'

exec spDescColumn N'IrregularityVersions', N'ImpairedRegulationAct'              , N'Акт: 1 - Решение; 2 - Директива; 3 - Регламент; 4 - Споразумение.'
exec spDescColumn N'IrregularityVersions', N'ImpairedRegulationNum'              , N'Номер на разпоредбата.'
exec spDescColumn N'IrregularityVersions', N'ImpairedRegulationYear'             , N'Година на разпоредбата.'
exec spDescColumn N'IrregularityVersions', N'ImpairedRegulation'                 , N'Нарушена разпоредба.'
exec spDescColumn N'IrregularityVersions', N'ImpairedNationalRegulation'         , N'Нарушена национална разпоредба.'

exec spDescColumn N'IrregularityVersions', N'AppliedPractices'                   , N'Приложени практики при извършване на нередността.'
exec spDescColumn N'IrregularityVersions', N'BeneficiaryData'                    , N'Данни, декларирани от бенефициент.'
exec spDescColumn N'IrregularityVersions', N'AdminAscertainments'                , N'Констатации на администрацията.'
exec spDescColumn N'IrregularityVersions', N'IrregularityDetectedBy'             , N'Компетентен орган, установил нередността.'

exec spDescColumn N'IrregularityVersions', N'EUCoFinancingPercent'               , N'Процент на съ-финансиране от ЕС.'

exec spDescColumn N'IrregularityVersions', N'ExpensesBfpEuAmountLv'              , N'Сума на разходите - Финансиране от ЕС (лева).'
exec spDescColumn N'IrregularityVersions', N'ExpensesBfpBgAmountLv'              , N'Сума на разходите - Финансиране от НФ (лева).'
exec spDescColumn N'IrregularityVersions', N'ExpensesBfpTotalAmountLv'           , N'Сума на разходите - БФП (лева).'
exec spDescColumn N'IrregularityVersions', N'ExpensesSelfAmountLv'               , N'Сума на разходите - собствено съфинансиране от бенефициента (лева).'
exec spDescColumn N'IrregularityVersions', N'ExpensesTotalAmountLv'              , N'Обща сума на разходите (лева).'

exec spDescColumn N'IrregularityVersions', N'ExpensesBfpEuAmountEuro'            , N'Сума на разходите – Финансиране от ЕС (евро).'
exec spDescColumn N'IrregularityVersions', N'ExpensesBfpBgAmountEuro'            , N'Сума на разходите - Финансиране от НФ(евро).'
exec spDescColumn N'IrregularityVersions', N'ExpensesBfpTotalAmountEuro'         , N'Сума на разходите - БФП (евро).'
exec spDescColumn N'IrregularityVersions', N'ExpensesSelfAmountEuro'             , N'Сума на разходите - собствено съфинансиране от бенефициента (евро).'
exec spDescColumn N'IrregularityVersions', N'ExpensesTotalAmountEuro'            , N'Обща сума на разходите (евро).'

exec spDescColumn N'IrregularityVersions', N'IrregularExpensesBfpEuAmountLv'     , N'Сума на нередния разход - финансиране от ЕС (лева).'
exec spDescColumn N'IrregularityVersions', N'IrregularExpensesBfpBgAmountLv'     , N'Сума на нередния разход - Финансиране от НФ (лева).'
exec spDescColumn N'IrregularityVersions', N'IrregularExpensesBfpTotalAmountLv'  , N'Обща сума на нередния разход - БФП(лева).'

exec spDescColumn N'IrregularityVersions', N'IrregularExpensesBfpEuAmountEuro'   , N'Сума на нередния разход - финансиране от ЕС (евро).'
exec spDescColumn N'IrregularityVersions', N'IrregularExpensesBfpBgAmountEuro'   , N'Сума на нередния разход - Финансиране от НФ (евро).'
exec spDescColumn N'IrregularityVersions', N'IrregularExpensesBfpTotalAmountEuro', N'Обща сума на нередния разход – БФП (евро).'

exec spDescColumn N'IrregularityVersions', N'CertifiedExpensesBfpEuAmountLv'     , N'Сума на сертифицираните разходи, засегнати от нередността - финансиране от ЕС (лева).'
exec spDescColumn N'IrregularityVersions', N'CertifiedExpensesBfpBgAmountLv'     , N'Сума на сертифицираните разходи, засегнати от нередността - Финансиране от НФ (лева).'
exec spDescColumn N'IrregularityVersions', N'CertifiedExpensesBfpTotalAmountLv'  , N'Обща сума на сертифицираните разходи, засегнати от нередността – БФП - БФП(лева).'

exec spDescColumn N'IrregularityVersions', N'CertifiedExpensesBfpEuAmountEuro'   , N'Сума на сертифицираните разходи, засегнати от нередността - финансиране от ЕС (евро).'
exec spDescColumn N'IrregularityVersions', N'CertifiedExpensesBfpBgAmountEuro'   , N'Сума на сертифицираните разходи, засегнати от нередността - Финансиране от НФ (евро).'
exec spDescColumn N'IrregularityVersions', N'CertifiedExpensesBfpTotalAmountEuro', N'Обща сума на сертифицираните разходи, засегнати от нередността – БФП - БФП(евро).'

exec spDescColumn N'IrregularityVersions', N'PaidBfpEuAmountLv'                  , N'Платена част от нередния разход - финансиране от ЕС (лева).'
exec spDescColumn N'IrregularityVersions', N'PaidBfpBgAmountLv'                  , N'Платена част от нередния разход - Финансиране от НФ (лева).'
exec spDescColumn N'IrregularityVersions', N'PaidBfpTotalAmountLv'               , N'Обща сума на платената част от нередния разход - БФП(лева).'

exec spDescColumn N'IrregularityVersions', N'PaidBfpEuAmountEuro'                , N'Платена част от нередния разход - финансиране от ЕС (евро).'
exec spDescColumn N'IrregularityVersions', N'PaidBfpBgAmountEuro'                , N'Платена част от нередния разход - Финансиране от НФ (евро).'
exec spDescColumn N'IrregularityVersions', N'PaidBfpTotalAmountEuro'             , N'Обща сума на платената част от нередния разход – БФП (евро).'

exec spDescColumn N'IrregularityVersions', N'ContractDebtStatus'                 , N'Статус на дълга.'
exec spDescColumn N'IrregularityVersions', N'ShouldDecertifyIrregularExpenses'   , N'Десертифициране на нередния разход.'
exec spDescColumn N'IrregularityVersions', N'DecertificationComments'            , N'Коментари за десертифицирането и по финансовата част.'

exec spDescColumn N'IrregularityVersions', N'SanctionProcedureType'              , N'Процедури за налагане на санкциите: 1 - Не е взето решение; 2 - Няма да се налага санкция; 3 - Предстои определянето на санкция; 4 - Наложена санкция.'
exec spDescColumn N'IrregularityVersions', N'SanctionProcedureKind'              , N'Вид процедура: 1 - Административни; 2 - Наказателни; 3 - Административни и наказателни.'
exec spDescColumn N'IrregularityVersions', N'SanctionProcedureStartDate'         , N'Дата, на която процедурата е започнала.'
exec spDescColumn N'IrregularityVersions', N'SanctionProcedureExpectedEndDate'   , N'Дата, на която се очаква процедурата да приключи.'
exec spDescColumn N'IrregularityVersions', N'SanctionProcedureEndDate'           , N'Дата на приключване на процедурата по налагане на санкции.'
exec spDescColumn N'IrregularityVersions', N'SanctionProcedureStatus'            , N'Статус на процедурата: 1 - Initiated; 2 - Completed; 3 - Abandoned.'
exec spDescColumn N'IrregularityVersions', N'SanctionCategoryId'                 , N'Идентификатор на категория санкции.'
exec spDescColumn N'IrregularityVersions', N'SanctionTypeId'                     , N'Идентификатор на вид санкции.'
exec spDescColumn N'IrregularityVersions', N'SanctionFines'                      , N'Наложени глоби.'

exec spDescColumn N'IrregularityVersions', N'AdminProcedures'                    , N'Кратко описание на административните процедури по случая.'
exec spDescColumn N'IrregularityVersions', N'PenaltyProcedures'                  , N'Кратко описание на наказателните процедури.'

exec spDescColumn N'IrregularityVersions', N'ShouldReportToOlaf'                 , N'Маркер дали нередността подлежи на докладване до ОЛАФ.'
exec spDescColumn N'IrregularityVersions', N'ReasonNotReportingToOlaf'           , N'Класификация на нередностите, неподлежащи на докладване до ОЛАФ: 1 - Под прага за докладване до ОЛАФ; 2: Попада в изключенията за докладване до ОЛАФ.'
exec spDescColumn N'IrregularityVersions', N'CheckTime'                          , N'Време на проверката: 1 - Преди плащане; 2 - След плащане;  3 – Преди, както и след плащане.'

exec spDescColumn N'IrregularityVersions', N'CreateDate'                         , N'Дата на създаване на записа.'
exec spDescColumn N'IrregularityVersions', N'ModifyDate'                         , N'Дата на последно редактиране на записа.'
exec spDescColumn N'IrregularityVersions', N'Version'                            , N'Версия.'
GO
