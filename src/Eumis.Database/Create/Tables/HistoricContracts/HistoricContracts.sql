PRINT 'HistoricContracts'
GO

CREATE TABLE [dbo].[HistoricContracts] (
    [HistoricContractId]    INT             NOT NULL,
    [ProcedureId]           INT             NOT NULL,
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
    CONSTRAINT [FK_HistoricContracts_CompanyType]           FOREIGN KEY ([CompanyTypeId])       REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [FK_HistoricContracts_CompanyLegalType]      FOREIGN KEY ([CompanyLegalTypeId])  REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [CHK_HistoricContracts_CompanyUinType]       CHECK ([CompanyUinType]             IN (0, 1, 2, 3)),
    CONSTRAINT [CHK_HistoricContracts_ExecutionStatus]      CHECK ([ExecutionStatus]            IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_HistoricContracts_NutsLevel]            CHECK ([NutsLevel]                  IN (1, 2, 3, 4, 5, 6, 7))
);
GO

exec spDescTable  N'HistoricContracts', N'Основни данни за договори.'
exec spDescColumn N'HistoricContracts', N'HistoricContractId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContracts', N'ProcedureId'          , N'Идентификатор на процедура.'
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
