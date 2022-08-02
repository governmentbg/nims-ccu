PRINT 'ContractPartners'
GO

CREATE TABLE [dbo].[ContractPartners] (
    [ContractPartnerId]     INT                 NOT NULL IDENTITY,
    [ContractId]            INT                 NOT NULL,
    [Gid]                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]              BIT                 NOT NULL,

    [FinancialContribution] MONEY               NOT NULL,

    --company fields
    [Uin]                   NVARCHAR(200)       NOT NULL,
    [UinType]               INT                 NOT NULL,

    [CompanyTypeId]         INT                 NOT NULL,
    [CompanyLegalStatus]    INT                 NOT NULL,
    [CompanyLegalTypeId]    INT                 NOT NULL,
    [Name]                  NVARCHAR(200)       NOT NULL,
    [NameAlt]               NVARCHAR(200)       NULL,
    [KidCodeId]             INT                 NULL,
    [CompanySizeTypeId]     INT                 NOT NULL,

    [SeatCountryId]         INT                 NULL,
    [SeatSettlementId]      INT                 NULL,
    [SeatPostCode]          NVARCHAR(50)        NULL,
    [SeatStreet]            NVARCHAR(200)       NULL,
    [SeatAddress]           NVARCHAR(MAX)       NULL,

    [CorrCountryId]         INT                 NULL,
    [CorrSettlementId]      INT                 NULL,
    [CorrPostCode]          NVARCHAR(50)        NULL,
    [CorrStreet]            NVARCHAR(200)       NULL,
    [CorrAddress]           NVARCHAR(MAX)       NULL,

    [Representative]        NVARCHAR(200)       NULL,
    [Phone1]                NVARCHAR(100)       NOT NULL,
    [Phone2]                NVARCHAR(100)       NULL,
    [Fax]                   NVARCHAR(100)       NULL,
    [Email]                 NVARCHAR(200)       NOT NULL,

    [ContactName]           NVARCHAR(200)       NULL,
    [ContactPhone]          NVARCHAR(100)       NULL,
    [ContactEmail]          NVARCHAR(100)       NULL

    CONSTRAINT [PK_ContractPartners]                        PRIMARY KEY ([ContractPartnerId]),
    CONSTRAINT [FK_ContractPartners_Contracts]              FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractPartners_CompanyLegalTypes]      FOREIGN KEY ([CompanyLegalTypeId])          REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [FK_ContractPartners_Countries_Seat]         FOREIGN KEY ([SeatCountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_ContractPartners_Settlements_Seat]       FOREIGN KEY ([SeatSettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_ContractPartners_Countries_Corr]         FOREIGN KEY ([CorrCountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_ContractPartners_Settlements_Corr]       FOREIGN KEY ([CorrSettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_ContractPartners_KidCodes]               FOREIGN KEY ([KidCodeId])                   REFERENCES [dbo].[KidCodes] ([KidCodeId]),
    CONSTRAINT [FK_ContractPartners_CompanySizeType]        FOREIGN KEY ([CompanySizeTypeId])           REFERENCES [dbo].[CompanySizeTypes] ([CompanySizeTypeId]),
    CONSTRAINT [CHK_ContractPartners_CompanyLegalStatuses]  CHECK       ([CompanyLegalStatus] IN (1, 2))
);
GO

exec spDescTable  N'ContractPartners', N'Партньори към договор.'
exec spDescColumn N'ContractPartners', N'ContractPartnerId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractPartners', N'ContractId'            , N'Идентификатор на договор.'
exec spDescColumn N'ContractPartners', N'Gid'                   , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractPartners', N'IsActive'              , N'Маркер за активност.'

exec spDescColumn N'ContractPartners', N'FinancialContribution' , N'Финансово участие.'

exec spDescColumn N'ContractPartners', N'Uin'                   , N'Уникален идентификационен номер.'
exec spDescColumn N'ContractPartners', N'UinType'               , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'

exec spDescColumn N'ContractPartners', N'CompanyTypeId'         , N'Идентификатор на тип органицазия.'
exec spDescColumn N'ContractPartners', N'CompanyLegalStatus'    , N'Правен статут.'
exec spDescColumn N'ContractPartners', N'CompanyLegalTypeId'    , N'Идентификатор на правен статут на организация.'
exec spDescColumn N'ContractPartners', N'Name'                  , N'Наименование.'
exec spDescColumn N'ContractPartners', N'NameAlt'               , N'Наименование на поддържания език.'
exec spDescColumn N'ContractPartners', N'KidCodeId'             , N'Класификация на икономическите дейности (КИД 2008).'
exec spDescColumn N'ContractPartners', N'CompanySizeTypeId'     , N'Класификация на предприятията спрямо „Закона на малки и средни предприятия“.'

exec spDescColumn N'ContractPartners', N'SeatCountryId'         , N'Седалище : Идентификатор на държава.'
exec spDescColumn N'ContractPartners', N'SeatSettlementId'      , N'Седалище : Идентификатор на териториална единица.'
exec spDescColumn N'ContractPartners', N'SeatPostCode'          , N'Седалище : Пощенски код.'
exec spDescColumn N'ContractPartners', N'SeatStreet'            , N'Седалище : улица, №, бл.'
exec spDescColumn N'ContractPartners', N'SeatAddress'           , N'Седалище : пълен текст на адреса, без държава'
exec spDescColumn N'ContractPartners', N'CorrCountryId'         , N'Кореспонденция : Идентификатор на държава.'
exec spDescColumn N'ContractPartners', N'CorrSettlementId'      , N'Кореспонденция : Идентификатор на териториална единица.'
exec spDescColumn N'ContractPartners', N'CorrPostCode'          , N'Кореспонденция : Пощенски код.'
exec spDescColumn N'ContractPartners', N'CorrStreet'            , N'Кореспонденция : улица, №, бл.'
exec spDescColumn N'ContractPartners', N'CorrAddress'           , N'Кореспонденция : пълен текст на адреса, без държава'
exec spDescColumn N'ContractPartners', N'Representative'        , N'Имена на лицето, представляващо организацията.'
exec spDescColumn N'ContractPartners', N'Phone1'                , N'Телефон 1.'
exec spDescColumn N'ContractPartners', N'Phone2'                , N'Телефон 2.'
exec spDescColumn N'ContractPartners', N'Fax'                   , N'Факс.'
exec spDescColumn N'ContractPartners', N'Email'                 , N'Ел. адрес.'

exec spDescColumn N'ContractPartners', N'ContactName'           , N'Име на лицето към организацията.'
exec spDescColumn N'ContractPartners', N'ContactPhone'          , N'Телефон на лицето към организацията.'
exec spDescColumn N'ContractPartners', N'ContactEmail'          , N'Ел. адрес на лицето към организацията.'
GO
