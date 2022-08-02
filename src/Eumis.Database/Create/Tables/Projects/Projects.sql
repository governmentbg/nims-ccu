PRINT 'Projects'
GO

CREATE TABLE [dbo].[Projects] (
    [ProjectId]                             INT                 NOT NULL IDENTITY,
    [Gid]                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [ProcedureId]                           INT                 NOT NULL,
    [ProjectTypeId]                         INT                 NOT NULL,

    [CompanyId]                             INT                 NOT NULL,
    [CompanyName]                           NVARCHAR(200)       NOT NULL,
    [CompanyNameAlt]                        NVARCHAR(200)       NULL,
    [CompanyUin]                            NVARCHAR(200)       NOT NULL,
    [CompanyUinType]                        INT                 NOT NULL,
    [CompanyKidCodeId]                      INT                 NULL,
    [CompanySizeTypeId]                     INT                 NOT NULL,
    [CompanyTypeId]                         INT                 NOT NULL,
    [CompanyLegalTypeId]                    INT                 NOT NULL,
    [CompanyEmail]                          NVARCHAR(200)       NULL,
    [CompanySeatCountryId]                  INT                 NULL,
    [CompanySeatSettlementId]               INT                 NULL,
    [CompanySeatPostCode]                   NVARCHAR(50)        NULL,
    [CompanySeatStreet]                     NVARCHAR(300)       NULL,
    [CompanySeatAddress]                    NVARCHAR(MAX)       NULL,
    [CompanyCorrespondenceCountryId]        INT                 NULL,
    [CompanyCorrespondenceSettlementId]     INT                 NULL,
    [CompanyCorrespondencePostCode]         NVARCHAR(50)        NULL,
    [CompanyCorrespondenceStreet]           NVARCHAR(300)       NULL,
    [CompanyCorrespondenceAddress]          NVARCHAR(MAX)       NULL,

    [Name]                                  NVARCHAR(MAX)       NOT NULL,
    [NameAlt]                               NVARCHAR(MAX)       NULL,
    [KidCodeId]                             INT                 NULL,

    [RegistrationStatus]                    INT                 NOT NULL,
    [RegNumber]                             NVARCHAR(200)       NOT NULL,
    [RegDate]                               DATETIME2           NOT NULL,
    [RecieveType]                           INT                 NOT NULL,
    [RecieveDate]                           DATETIME2           NOT NULL,
    [SubmitDate]                            DATETIME2           NOT NULL,
    [StoragePlace]                          NVARCHAR(MAX)       NULL,
    [Originals]                             INT                 NULL,
    [Copies]                                INT                 NULL,
    [Notes]                                 NVARCHAR(MAX)       NULL,

    [EvalStatus]                            INT                 NOT NULL,

    [Duration]                              INT                 NULL,
    [NutsAddressFullPath]                   NVARCHAR(MAX)       NULL,
    [NutsAddressFullPathName]               NVARCHAR(MAX)       NULL,
    [TotalBfpAmount]                        MONEY               NULL,
    [CoFinancingAmount]                     MONEY               NULL,

    [VatEligible]                           INT                 NULL,
    [AmountType]                            INT                 NULL,
    [IsJointActionPlan]                     BIT                 NULL,
    [IsUsesFinancialInstruments]            BIT                 NULL,
    [IsIncludesSupportFromIYE]              BIT                 NULL,
    [IsSubjectToStateAidRegime]             BIT                 NULL,
    [IsIncludesPublicPrivatePartnership]    BIT                 NULL,

    [Description]                           NVARCHAR(MAX)       NULL,
    [Purpose]                               NVARCHAR(MAX)       NULL,

    [RequestedFundingAmount]                        MONEY       NULL,
    [RequestedFundingPartOfOtherPublicFundsAmount]  MONEY       NULL,
    [RequestedFundingPartOfCrossFinancingAmount]    MONEY       NULL,
    [CoFinancingPublicAmount]                       MONEY       NULL,
    [CoFinancingPrivateAmount]                      MONEY       NULL,
    [EIBAmount]                                     MONEY       NULL,
    [EBRDAmount]                                    MONEY       NULL,
    [WBAmount]                                      MONEY       NULL,
    [OtherAmount]                                   MONEY       NULL,

    [ExpectedRevenue]                               MONEY       NULL,
    [IneligibleCosts]                               MONEY       NULL,

    [CreateDate]                                    DATETIME2   NOT NULL,
    [ModifyDate]                                    DATETIME2   NOT NULL,
    [Version]                                       ROWVERSION  NOT NULL,

    CONSTRAINT [PK_Projects]                        PRIMARY KEY ([ProjectId]),
    CONSTRAINT [CHK_Projects_RegistrationStatus]    CHECK ([RegistrationStatus] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_Projects_RecieveType]           CHECK ([RecieveType] IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_Projects_EvalStatus]            CHECK ([EvalStatus] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_Projects_VatEligible]           CHECK ([VatEligible] IN (1, 2, 3)),
    CONSTRAINT [CHK_Projects_AmountType]            CHECK ([AmountType] IN (1, 2, 3)),
    CONSTRAINT [CHK_Projects_CompanyUinType]        CHECK ([CompanyUinType] IN (0, 1, 2, 3)),

    CONSTRAINT [FK_Projects_Procedures]                       FOREIGN KEY ([ProcedureId])                       REFERENCES [dbo].[Procedures]        ([ProcedureId]),
    CONSTRAINT [FK_Projects_ProjectTypes]                     FOREIGN KEY ([ProjectTypeId])                     REFERENCES [dbo].[ProjectTypes]      ([ProjectTypeId]),
    CONSTRAINT [FK_Projects_Companies]                        FOREIGN KEY ([CompanyId])                         REFERENCES [dbo].[Companies]         ([CompanyId]),
    CONSTRAINT [FK_Projects_CompanyKidCodes]                  FOREIGN KEY ([CompanyKidCodeId])                  REFERENCES [dbo].[KidCodes]          ([KidCodeId]),
    CONSTRAINT [FK_Projects_CompanySizeType]                  FOREIGN KEY ([CompanySizeTypeId])                 REFERENCES [dbo].[CompanySizeTypes]  ([CompanySizeTypeId]),
    CONSTRAINT [FK_Projects_CompanyType]                      FOREIGN KEY ([CompanyTypeId])                     REFERENCES [dbo].[CompanyTypes]      ([CompanyTypeId]),
    CONSTRAINT [FK_Projects_CompanyLegalType]                 FOREIGN KEY ([CompanyLegalTypeId])                REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [FK_Projects_CompanySeatCountries]             FOREIGN KEY ([CompanySeatCountryId])              REFERENCES [dbo].[Countries]         ([CountryId]),
    CONSTRAINT [FK_Projects_CompanySeatSettlements]           FOREIGN KEY ([CompanySeatSettlementId])           REFERENCES [dbo].[Settlements]       (SettlementId),
    CONSTRAINT [FK_Projects_CompanyCorrespondenceCountries]   FOREIGN KEY ([CompanyCorrespondenceCountryId])    REFERENCES [dbo].[Countries]         ([CountryId]),
    CONSTRAINT [FK_Projects_CompanyCorrespondenceSettlements] FOREIGN KEY ([CompanyCorrespondenceSettlementId]) REFERENCES [dbo].[Settlements]       (SettlementId),

    CONSTRAINT [FK_Projects_ProjectKidCodes]        FOREIGN KEY ([KidCodeId])           REFERENCES [dbo].[KidCodes] ([KidCodeId])
);
GO

exec spDescTable  N'Projects', N'Проектно предложение по процедура.'
exec spDescColumn N'Projects', N'ProjectId'                             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Projects', N'ProcedureId'                           , N'Идентификатор на процедура.'
exec spDescColumn N'Projects', N'ProjectTypeId'                         , N'Идентификатор на тип на предложението.'

exec spDescColumn N'Projects', N'CompanyId'                             , N'Идентификатор на компанията кандидат.'
exec spDescColumn N'Projects', N'CompanyName'                           , N'Наименование на компанията кандидат.'
exec spDescColumn N'Projects', N'CompanyNameAlt'                        , N'Наименование на компанията кандидат на друг език.'
exec spDescColumn N'Projects', N'CompanyUin'                            , N'Уникален идентификационен номер на кандидат.'
exec spDescColumn N'Projects', N'CompanyUinType'                        , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'
exec spDescColumn N'Projects', N'CompanyKidCodeId'                      , N'Код на кандидата по КИД 2008.'
exec spDescColumn N'Projects', N'CompanySizeTypeId'                     , N'Класификация на кандидата спрямо „Закона на малки и средни предприятия“.'
exec spDescColumn N'Projects', N'CompanyTypeId'                         , N'Тип организация.'
exec spDescColumn N'Projects', N'CompanyLegalTypeId'                    , N'Вид организация.'
exec spDescColumn N'Projects', N'CompanyEmail'                          , N'Е-mail за контакт.'
exec spDescColumn N'Projects', N'CompanySeatCountryId'                  , N'Адрес на организацията - идентификатор на държава.'
exec spDescColumn N'Projects', N'CompanySeatSettlementId'               , N'Адрес на организацията - идентификатор на населено място.'
exec spDescColumn N'Projects', N'CompanySeatPostCode'                   , N'Адрес на организацията - пощенски код.'
exec spDescColumn N'Projects', N'CompanySeatStreet'                     , N'Адрес на организацията - улица.'
exec spDescColumn N'Projects', N'CompanySeatAddress'                    , N'Адрес на организацията - пълен адрес.'
exec spDescColumn N'Projects', N'CompanyCorrespondenceCountryId'        , N'Адрес за кореспонденция на организацията - идентификатор на държава.'
exec spDescColumn N'Projects', N'CompanyCorrespondenceSettlementId'     , N'Адрес за кореспонденция на организацията - идентификатор на населено място.'
exec spDescColumn N'Projects', N'CompanyCorrespondencePostCode'         , N'Адрес за кореспонденция на организацията - пощенски код.'
exec spDescColumn N'Projects', N'CompanyCorrespondenceStreet'           , N'Адрес за кореспонденция на организацията - улица.'
exec spDescColumn N'Projects', N'CompanyCorrespondenceAddress'          , N'Адрес за кореспонденция на организацията - пълен адрес.'

exec spDescColumn N'Projects', N'Name'                                  , N'Наименование на проектно предложение.'
exec spDescColumn N'Projects', N'NameAlt'                               , N'Наименование на проектно предложение на друг език.'
exec spDescColumn N'Projects', N'KidCodeId'                             , N'Код на проекта по КИД 2008.'

exec spDescColumn N'Projects', N'RegistrationStatus'                    , N'Статус при регистрацията. 1. Регистрирано, 2. Регистрирано извън срок, 3.Оттеглено, 4. Регистрирано служебно'
exec spDescColumn N'Projects', N'RegNumber'                             , N'Системно генериран регистрационен номер .'
exec spDescColumn N'Projects', N'RegDate'                               , N'Дата на регистрация.'
exec spDescColumn N'Projects', N'RecieveType'                           , N'Начин на получване. 1. На ръка, 2. Поща, 3. Куриер, 4. Факс, 5. Електронен път'
exec spDescColumn N'Projects', N'RecieveDate'                           , N'Дата на получаване .'
exec spDescColumn N'Projects', N'SubmitDate'                            , N'Дата на подаване.'
exec spDescColumn N'Projects', N'StoragePlace'                          , N'Място на съхранение.'
exec spDescColumn N'Projects', N'Originals'                             , N'Брой оригинали.'
exec spDescColumn N'Projects', N'Copies'                                , N'Брой копия.'
exec spDescColumn N'Projects', N'Notes'                                 , N'Коментар, описание.'

exec spDescColumn N'Projects', N'EvalStatus'                            , N'Статус при оценката. 1. Оценяване, 2. Приключила оценка, 3. Договориран, 4. Чакащо потвърждение от УО/ДФЗ.'

exec spDescColumn N'Projects', N'Duration'                              , N'Срок на изпълнение, месеци.'
exec spDescColumn N'Projects', N'NutsAddressFullPath'                   , N'Място на изпълнение на проекта - пълен път.'
exec spDescColumn N'Projects', N'NutsAddressFullPathName'               , N'Място на изпълнение на проекта - пълно наименование.'
exec spDescColumn N'Projects', N'TotalBfpAmount'                        , N'Общ размер на безвъзмездната финансова помощ.'
exec spDescColumn N'Projects', N'CoFinancingAmount'                     , N'Общ размер на съфинансиране.'

exec spDescColumn N'Projects', N'VatEligible'                           , N'ДДС е допустим разход по проекта.1-НЕ, 2-ДА, 3-Друго'
exec spDescColumn N'Projects', N'AmountType'                            , N'Вид на проекта : 1- Проектът е голям проект съгласно чл. 100 от Регламент (ЕС) № 1303/ 2013 г; 2-Инфраструктурен проект на стойност над 5 000 000 лв.; 3-Друг.'
exec spDescColumn N'Projects', N'IsJointActionPlan'                     , N'Проектът е Съвместен план за действие.'
exec spDescColumn N'Projects', N'IsUsesFinancialInstruments'            , N'Проектът използва  финансови инструменти.'
exec spDescColumn N'Projects', N'IsIncludesSupportFromIYE'              , N'Проектът включва подкрепа от Инициатива за младежка заетост.'
exec spDescColumn N'Projects', N'IsSubjectToStateAidRegime'             , N'Проектът подлежи на режим на държавна помощ.'
exec spDescColumn N'Projects', N'IsIncludesPublicPrivatePartnership'    , N'Проектът включва публично-частно партньорство.'

exec spDescColumn N'Projects', N'Description'                           , N'Кратко описание на проектното предложение.'
exec spDescColumn N'Projects', N'Purpose'                               , N'Цели на проектното предложение.'

exec spDescColumn N'Projects', N'RequestedFundingAmount'                        , N'Искано финансиране (Безвъзмездна финансова помощ).'
exec spDescColumn N'Projects', N'RequestedFundingPartOfOtherPublicFundsAmount'  , N'в т.ч. други публични средства.'
exec spDescColumn N'Projects', N'RequestedFundingPartOfCrossFinancingAmount'    , N'в т.ч. кръстосано финансиране.'
exec spDescColumn N'Projects', N'CoFinancingPublicAmount'                       , N'Съ-финансиране от бенефициента/партньорите (публични средства).'
exec spDescColumn N'Projects', N'CoFinancingPrivateAmount'                      , N'Съ-финансиране от бенефициента/партньорите (частни средства).'
exec spDescColumn N'Projects', N'EIBAmount'                                     , N'Финансиране от Европейска инвестиционна банка.'
exec spDescColumn N'Projects', N'EBRDAmount'                                    , N'Финансиране от Европейската банка за възстановяване и развитие.'
exec spDescColumn N'Projects', N'WBAmount'                                      , N'Финансиране от Световна банка.'
exec spDescColumn N'Projects', N'OtherAmount'                                   , N'Финансиране от други МФИ.'

exec spDescColumn N'Projects', N'ExpectedRevenue'                               , N'Очаквани приходи от проекта.'
exec spDescColumn N'Projects', N'IneligibleCosts'                               , N'Недопустими разходи, необходими за изпълнението на проекта.'
GO
