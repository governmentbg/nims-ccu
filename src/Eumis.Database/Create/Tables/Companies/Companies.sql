PRINT 'Companies'
GO

CREATE TABLE [dbo].[Companies] (
    [CompanyId]             INT                 NOT NULL,
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
    [SeatStreet]            NVARCHAR(300)       NULL,
    [SeatAddress]           NVARCHAR(MAX)       NULL,

    [CorrCountryId]         INT                 NULL,
    [CorrSettlementId]      INT                 NULL,
    [CorrPostCode]          NVARCHAR(50)        NULL,
    [CorrStreet]            NVARCHAR(300)       NULL,
    [CorrAddress]           NVARCHAR(MAX)       NULL,

    [Representative]        NVARCHAR(200)       NULL,
    [Phone1]                NVARCHAR(100)       NOT NULL,
    [Phone2]                NVARCHAR(100)       NULL,
    [Fax]                   NVARCHAR(100)       NULL,
    [Email]                 NVARCHAR(200)       NOT NULL,

    [ContactName]           NVARCHAR(200)       NULL,
    [ContactPhone]          NVARCHAR(100)       NULL,
    [ContactEmail]          NVARCHAR(100)       NULL,

    [IsLocalActionGroup]    BIT                 NOT NULL DEFAULT 0,

    [CreateDate]            DATETIME2           NOT NULL,
    [ModifyDate]            DATETIME2           NOT NULL,
    [Version]               ROWVERSION          NOT NULL

    CONSTRAINT [PK_Companies]                           PRIMARY KEY ([CompanyId]),
    CONSTRAINT [UQ_Companies_Uin_UinType]               UNIQUE      ([Uin], [UinType]),
    CONSTRAINT [FK_Companies_CompanyLegalTypes]         FOREIGN KEY ([CompanyLegalTypeId])          REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
    CONSTRAINT [FK_Companies_Countries_Seat]            FOREIGN KEY ([SeatCountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_Companies_Settlements_Seat]          FOREIGN KEY ([SeatSettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_Companies_Countries_Corr]            FOREIGN KEY ([CorrCountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_Companies_Settlements_Corr]          FOREIGN KEY ([CorrSettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_Companies_KidCodes]                  FOREIGN KEY ([KidCodeId])                   REFERENCES [dbo].[KidCodes] ([KidCodeId]),
    CONSTRAINT [FK_Companies_CompanySizeType]           FOREIGN KEY ([CompanySizeTypeId])           REFERENCES [dbo].[CompanySizeTypes] ([CompanySizeTypeId]),
    CONSTRAINT [CHK_Companies_CompanyLegalStatuses]     CHECK       ([CompanyLegalStatus] IN (1, 2))
);
GO

exec spDescTable  N'Companies', N'Организации.'
exec spDescColumn N'Companies', N'CompanyId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Companies', N'Uin'                  , N'Уникален идентификационен номер.'
exec spDescColumn N'Companies', N'UinType'              , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'

exec spDescColumn N'Companies', N'CompanyTypeId'        , N'Идентификатор на тип органицазия.'
exec spDescColumn N'Companies', N'CompanyLegalStatus'   , N'Правен статут.'
exec spDescColumn N'Companies', N'CompanyLegalTypeId'   , N'Идентификатор на правен статут на организация.'
exec spDescColumn N'Companies', N'Name'                 , N'Наименование.'
exec spDescColumn N'Companies', N'NameAlt'              , N'Наименование на поддържания език.'
exec spDescColumn N'Companies', N'KidCodeId'            , N'Класификация на икономическите дейности (КИД 2008).'
exec spDescColumn N'Companies', N'CompanySizeTypeId'    , N'Класификация на предприятията спрямо „Закона на малки и средни предприятия“.'

exec spDescColumn N'Companies', N'SeatCountryId'        , N'Седалище : Идентификатор на държава.'
exec spDescColumn N'Companies', N'SeatSettlementId'     , N'Седалище : Идентификатор на териториална единица.'
exec spDescColumn N'Companies', N'SeatPostCode'         , N'Седалище : Пощенски код.'
exec spDescColumn N'Companies', N'SeatStreet'           , N'Седалище : улица, №, бл.'
exec spDescColumn N'Companies', N'SeatAddress'          , N'Седалище : пълен текст на адреса, без държава'
exec spDescColumn N'Companies', N'CorrCountryId'        , N'Кореспонденция : Идентификатор на държава.'
exec spDescColumn N'Companies', N'CorrSettlementId'     , N'Кореспонденция : Идентификатор на териториална единица.'
exec spDescColumn N'Companies', N'CorrPostCode'         , N'Кореспонденция : Пощенски код.'
exec spDescColumn N'Companies', N'CorrStreet'           , N'Кореспонденция : улица, №, бл.'
exec spDescColumn N'Companies', N'CorrAddress'          , N'Кореспонденция : пълен текст на адреса, без държава'
exec spDescColumn N'Companies', N'Representative'       , N'Имена на лицето, представляващо организацията.'
exec spDescColumn N'Companies', N'Phone1'               , N'Телефон 1.'
exec spDescColumn N'Companies', N'Phone2'               , N'Телефон 2.'
exec spDescColumn N'Companies', N'Fax'                  , N'Факс.'
exec spDescColumn N'Companies', N'Email'                , N'Ел. адрес.'

exec spDescColumn N'Companies', N'ContactName'          , N'Име на лицето към организацията.'
exec spDescColumn N'Companies', N'ContactPhone'         , N'Телефон на лицето към организацията.'
exec spDescColumn N'Companies', N'ContactEmail'         , N'Ел. адрес на лицето към организацията.'

exec spDescColumn N'Companies', N'IsLocalActionGroup'   , N'Маркер дали организацията е МИГ/МИРГ.'

exec spDescColumn N'Companies', N'CreateDate'          , N'Дата на създаване на записа.'
exec spDescColumn N'Companies', N'ModifyDate'          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Companies', N'Version'             , N'Версия.'

GO
