PRINT 'Contracts'
GO

CREATE TABLE [dbo].[Contracts] (
    [ContractId]                            INT               NOT NULL IDENTITY,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,
    [ProgrammeId]                           INT               NOT NULL,
    [ProcedureId]                           INT               NOT NULL,
    [ProjectId]                             INT               NOT NULL,
    [ContractType]                          INT               NOT NULL,
    [ContractStatus]                        INT               NOT NULL,

    [AttachedContractId]                    INT               NULL,

    [CompanyId]                             INT               NOT NULL,
    [CompanyName]                           NVARCHAR(200)     NOT NULL,
    [CompanyNameAlt]                        NVARCHAR(200)     NULL,
    [CompanyUin]                            NVARCHAR(200)     NOT NULL,
    [CompanyUinType]                        INT               NOT NULL,
    [CompanyKidCodeId]                      INT               NULL,
    [CompanySizeTypeId]                     INT               NOT NULL,
    [CompanyTypeId]                         INT               NULL,
    [CompanyLegalStatus]                    INT               NULL,
    [CompanyLegalTypeId]                    INT               NULL,
    [CompanyEmail]                          NVARCHAR(200)     NULL,

    [Name]                                  NVARCHAR(MAX)     NULL,
    [NameEN]                                NVARCHAR(MAX)     NULL,
    [Description]                           NVARCHAR(MAX)     NULL,
    [DescriptionEN]                         NVARCHAR(MAX)     NULL,
    [RegNumber]                             NVARCHAR(200)     NOT NULL,
    [ExecutionStatus]                       INT               NULL,
    [ContractDate]                          DATETIME2         NULL,
    [StartDate]                             DATETIME2         NULL,
    [StartConditions]                       NVARCHAR(MAX)     NULL,
    [CompletionDate]                        DATETIME2         NULL,
    [TerminationDate]                       DATETIME2         NULL,
    [TerminationReason]                     NVARCHAR(MAX)     NULL,
    [NutsLevel]                             INT               NULL,
    [Duration]                              INT               NULL,
    [ProjectKidCodeId]                      INT               NULL,

    [BeneficiarySeatCountryId]              INT               NULL,
    [BeneficiarySeatSettlementId]           INT               NULL,
    [BeneficiarySeatPostCode]               NVARCHAR(50)      NULL,
    [BeneficiarySeatStreet]                 NVARCHAR(200)     NULL,
    [BeneficiarySeatAddress]                NVARCHAR(MAX)     NULL,

    [BeneficiaryCorrespondenceCountryId]    INT               NULL,
    [BeneficiaryCorrespondenceSettlementId] INT               NULL,
    [BeneficiaryCorrespondencePostCode]     NVARCHAR(50)      NULL,
    [BeneficiaryCorrespondenceStreet]       NVARCHAR(200)     NULL,
    [BeneficiaryCorrespondenceAddress]      NVARCHAR(MAX)     NULL,

    [TotalEuAmount]                         MONEY             NULL,
    [TotalBgAmount]                         MONEY             NULL,
    [TotalBfpAmount]                        MONEY             NULL,
    [TotalSelfAmount]                       MONEY             NULL,
    [TotalAmount]                           MONEY             NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_Contracts]                            PRIMARY KEY ([ContractId]),
    CONSTRAINT [FK_Contracts_Programmes]                 FOREIGN KEY ([ProgrammeId])                           REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_Contracts_Procedures]                 FOREIGN KEY ([ProcedureId])                           REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_Contracts_Projects]                   FOREIGN KEY ([ProjectId])                             REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_Contracts_Companies]                  FOREIGN KEY ([CompanyId])                             REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [FK_Contracts_CompanyKidCodes]            FOREIGN KEY ([CompanyKidCodeId])                      REFERENCES [dbo].[KidCodes] ([KidCodeId]),
    CONSTRAINT [FK_Contracts_CompanySizeType]            FOREIGN KEY ([CompanySizeTypeId])                     REFERENCES [dbo].[CompanySizeTypes] ([CompanySizeTypeId]),
    CONSTRAINT [FK_Contracts_CompanyLegalType]           FOREIGN KEY ([CompanyLegalTypeId])                    REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [FK_Contracts_CompanyType]                FOREIGN KEY ([CompanyTypeId])                         REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [FK_Contracts_ProjectKidCodes]            FOREIGN KEY ([ProjectKidCodeId])                      REFERENCES [dbo].[KidCodes] ([KidCodeId]),
    CONSTRAINT [FK_Contracts_Countries_Seat]             FOREIGN KEY ([BeneficiarySeatCountryId])              REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_Contracts_Settlements_Seat]           FOREIGN KEY ([BeneficiarySeatSettlementId])           REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_Contracts_Countries_Correspondence]   FOREIGN KEY ([BeneficiaryCorrespondenceCountryId])    REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_Contracts_Settlements_Correspondence] FOREIGN KEY ([BeneficiaryCorrespondenceSettlementId]) REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_Contracts_AttachedContracts]          FOREIGN KEY ([AttachedContractId])                    REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_Contracts_ContractType]              CHECK ([ContractType]     IN (1, 2, 3, 4, 5, 6, 7, 8)),
    CONSTRAINT [CHK_Contracts_ContractStatus]            CHECK ([ContractStatus]   IN (1, 2)),
    CONSTRAINT [CHK_Contracts_ExecutionStatus]           CHECK ([ExecutionStatus]  IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_Contracts_NutsLevel]                 CHECK ([NutsLevel] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_Contracts_CompanyLegalStatuses]      CHECK ([CompanyLegalStatus] IN (1, 2))
);
GO

CREATE UNIQUE INDEX [UQ_Contracts_RegNumber]
ON [Contracts]([RegNumber])
WHERE [RegNumber] IS NOT NULL;

exec spDescTable  N'Contracts', N'Договор за БФП по проектно предложение по процедура.'
exec spDescColumn N'Contracts', N'ContractId'                           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Contracts', N'Gid'                                  , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'Contracts', N'ProgrammeId'                          , N'Идентификатор на оперативна програма'
exec spDescColumn N'Contracts', N'ProcedureId'                          , N'Идентификатор на процедура.'
exec spDescColumn N'Contracts', N'ProjectId'                            , N'Идентификатор на проектно предложение.'
exec spDescColumn N'Contracts', N'ContractType'                         , N'Тип на договор: 1 - Решение; 2 - Заповед; 3 - Договор, 8 - Служебен договор.'
exec spDescColumn N'Contracts', N'ContractStatus'                       , N'Статус на договор: 1 - Чернова; 2 - Въведен.'

exec spDescColumn N'Contracts', N'CompanyId'                            , N'Идентификатор на компанията бенефициент.'
exec spDescColumn N'Contracts', N'CompanyName'                          , N'Наименование на компанията бенефициент.'
exec spDescColumn N'Contracts', N'CompanyNameAlt'                       , N'Наименование на компанията бенефициент на поддържан език.'
exec spDescColumn N'Contracts', N'CompanyUin'                           , N'Уникален идентификационен номер на бенефициент.'
exec spDescColumn N'Contracts', N'CompanyUinType'                       , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'
exec spDescColumn N'Contracts', N'CompanyKidCodeId'                     , N'Код на бенефициента по КИД 2008.'
exec spDescColumn N'Contracts', N'CompanySizeTypeId'                    , N'Класификация на бенефициента спрямо „Закона на малки и средни предприятия“.'
exec spDescColumn N'Contracts', N'CompanyEmail'                         , N'Е-mail за контакт на бенефициента.'

exec spDescColumn N'Contracts', N'Name'                                 , N'Нименование.'
exec spDescColumn N'Contracts', N'NameEN'                               , N'Нименование на английски.'
exec spDescColumn N'Contracts', N'Description'                          , N'Описание.'
exec spDescColumn N'Contracts', N'DescriptionEN'                        , N'Описание на английски.'
exec spDescColumn N'Contracts', N'RegNumber'                            , N'Системно генериран регистрационен номер.'
exec spDescColumn N'Contracts', N'ExecutionStatus'                      , N'Статус на изпълнение на договора: 1 - В изпълнение (от дата на стартиране); 2 - В изпълнение (временно спрян); 3 - В изпълнение (под наблюдение); 4 - Прекратен (към дата на прекратяване); 5 - Приключен (към датата на приключване).'
exec spDescColumn N'Contracts', N'ContractDate'                         , N'Дата на сключване на договор.'
exec spDescColumn N'Contracts', N'StartDate'                            , N'Дата на стартиране.'
exec spDescColumn N'Contracts', N'StartConditions'                      , N'Условие за стартиране - задължително трябва да е попълнена или дата на стартиране или условие за стартиране.'
exec spDescColumn N'Contracts', N'CompletionDate'                       , N'Дата на завършване на договора.'
exec spDescColumn N'Contracts', N'TerminationDate'                      , N'Дата на прекратяване.'
exec spDescColumn N'Contracts', N'TerminationReason'                    , N'Основание за прекратяване.'

exec spDescColumn N'Contracts', N'NutsLevel'                            , N'1 - Country, 2 - RegionNUTS1, 3 - RegionNUTS2, 4 - RegionNUTS3, 5 - Municipality, 6 - Settlement; 7 - Protected zone.'
exec spDescColumn N'Contracts', N'Duration'                             , N'Срок на изпълнение, месеци.'
exec spDescColumn N'Contracts', N'ProjectKidCodeId'                     , N'Код на проекта по КИД 2008.'

exec spDescColumn N'Contracts', N'BeneficiarySeatCountryId'             , N'Седалище - Държава.'
exec spDescColumn N'Contracts', N'BeneficiarySeatSettlementId'          , N'Седалище - Населено място.'
exec spDescColumn N'Contracts', N'BeneficiarySeatPostCode'              , N'Седалище - ПК.'
exec spDescColumn N'Contracts', N'BeneficiarySeatStreet'                , N'Седалище - Улица.'
exec spDescColumn N'Contracts', N'BeneficiarySeatAddress'               , N'Седалище - Адрес.'

exec spDescColumn N'Contracts', N'BeneficiaryCorrespondenceCountryId'   , N'Кореспонденция - Държава.'
exec spDescColumn N'Contracts', N'BeneficiaryCorrespondenceSettlementId', N'Кореспонденция - Населено място.'
exec spDescColumn N'Contracts', N'BeneficiaryCorrespondencePostCode'    , N'Кореспонденция - ПК.'
exec spDescColumn N'Contracts', N'BeneficiaryCorrespondenceStreet'      , N'Кореспонденция - Улица.'
exec spDescColumn N'Contracts', N'BeneficiaryCorrespondenceAddress'     , N'Кореспонденция - Адрес.'

exec spDescColumn N'Contracts', N'TotalEuAmount'                        , N'Общо Финансиране от ЕС за договора.'
exec spDescColumn N'Contracts', N'TotalBgAmount'                        , N'Общо Финансиране от НФ за договора.'
exec spDescColumn N'Contracts', N'TotalBfpAmount'                       , N'Общо БФП за договора.'
exec spDescColumn N'Contracts', N'TotalSelfAmount'                      , N'Общо собствено финансиране за договора.'
exec spDescColumn N'Contracts', N'TotalAmount'                          , N'Обща стойност на договора.'

exec spDescColumn N'Contracts', N'CreateDate'                           , N'Дата на създаване на записа.'
exec spDescColumn N'Contracts', N'ModifyDate'                           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Contracts', N'Version'                              , N'Версия.'
GO
