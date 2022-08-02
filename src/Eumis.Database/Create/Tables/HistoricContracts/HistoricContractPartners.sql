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
