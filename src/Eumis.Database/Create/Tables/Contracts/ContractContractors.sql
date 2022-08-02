PRINT 'ContractContractors'
GO

CREATE TABLE [dbo].[ContractContractors] (
    [ContractContractorId]      INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]                  BIT                 NOT NULL,

    [Uin]                       NVARCHAR(200)       NOT NULL,
    [UinType]                   INT                 NOT NULL,
    [Name]                      NVARCHAR(200)       NOT NULL,
    [NameAlt]                   NVARCHAR(200)       NULL,
    [SeatCountryId]             INT                 NULL,
    [SeatSettlementId]          INT                 NULL,
    [SeatPostCode]              NVARCHAR(50)        NULL,
    [SeatStreet]                NVARCHAR(300)       NULL,
    [SeatAddress]               NVARCHAR(MAX)       NULL,
    [RepresentativeNames]       NVARCHAR(MAX)       NULL,
    [RepresentativeIDNumber]    NVARCHAR(200)       NULL,
    [VATRegistration]           INT                 NOT NULL,
    [CompanyLegalStatus]        INT                 NOT NULL,

    CONSTRAINT [PK_ContractContractors]                         PRIMARY KEY ([ContractContractorId]),
    CONSTRAINT [FK_ContractContractors_Contracts]               FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractContractors_Countries_Seat]          FOREIGN KEY ([SeatCountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_ContractContractors_Settlements_Seat]        FOREIGN KEY ([SeatSettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [CHK_ContractContractors_VATRegistration]        CHECK       ([VATRegistration] IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractContractors_CompanyLegalStatuses]   CHECK       ([CompanyLegalStatus] IN (1, 2))
);
GO

exec spDescTable  N'ContractContractors', N'Изпълнители към договор.'
exec spDescColumn N'ContractContractors', N'ContractContractorId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractContractors', N'ContractId'             , N'Идентификатор на договор.'
exec spDescColumn N'ContractContractors', N'Gid'                    , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractContractors', N'IsActive'               , N'Маркер за активност.'

exec spDescColumn N'ContractContractors', N'Uin'                    , N'Уникален идентификационен номер.'
exec spDescColumn N'ContractContractors', N'UinType'                , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'
exec spDescColumn N'ContractContractors', N'Name'                   , N'Наименование.'
exec spDescColumn N'ContractContractors', N'NameAlt'                , N'Наименование на поддържания език.'
exec spDescColumn N'ContractContractors', N'SeatCountryId'          , N'Седалище : Идентификатор на държава.'
exec spDescColumn N'ContractContractors', N'SeatSettlementId'       , N'Седалище : Идентификатор на териториална единица.'
exec spDescColumn N'ContractContractors', N'SeatPostCode'           , N'Седалище : Пощенски код.'
exec spDescColumn N'ContractContractors', N'SeatStreet'             , N'Седалище : улица, №, бл.'
exec spDescColumn N'ContractContractors', N'SeatAddress'            , N'Седалище : пълен текст на адреса, без държава'
exec spDescColumn N'ContractContractors', N'RepresentativeNames'    , N'Имена на лице, представляващо организацията.'
exec spDescColumn N'ContractContractors', N'RepresentativeIDNumber' , N'ЕГН на лице, представляващо организацията.'
exec spDescColumn N'ContractContractors', N'VATRegistration'        , N'Регистрация по ДДС. 1 - Да, 2 - Не, 3 - Не е приложимо.'
exec spDescColumn N'ContractContractors', N'CompanyLegalStatus'     , N'Правен статут.'
GO
