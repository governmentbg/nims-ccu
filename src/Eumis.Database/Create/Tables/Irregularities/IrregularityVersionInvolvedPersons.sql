PRINT 'IrregularityVersionInvolvedPersons'
GO

CREATE TABLE [dbo].[IrregularityVersionInvolvedPersons] (
    [IrregularityVersionInvolvedPersonId] INT               NOT NULL IDENTITY,
    [IrregularityVersionId]               INT               NOT NULL,
    [LegalType]                           INT               NOT NULL,
    [Uin]                                 NVARCHAR(200)     NOT NULL,
    [UinType]                             INT               NOT NULL,
    [UndisclosureMotives]                 NVARCHAR(MAX)     NULL,

    [CompanyName]                         NVARCHAR(200)     NULL,
    [TradeName]                           NVARCHAR(200)     NULL,
    [HoldingName]                         NVARCHAR(200)     NULL,
    [FirstName]                           NVARCHAR(100)     NULL,
    [MiddleName]                          NVARCHAR(100)     NULL,
    [LastName]                            NVARCHAR(100)     NULL,

    [CountryId]                           INT               NULL,
    [SettlementId]                        INT               NULL,
    [PostCode]                            NVARCHAR(50)      NULL,
    [Street]                              NVARCHAR(200)     NULL,
    [Address]                             NVARCHAR(MAX)     NULL,

    CONSTRAINT [PK_IrregularityVersionInvolvedPersons]                       PRIMARY KEY ([IrregularityVersionInvolvedPersonId]),
    CONSTRAINT [FK_IrregularityVersionInvolvedPersons_IrregularityVersions]  FOREIGN KEY ([IrregularityVersionId])   REFERENCES [dbo].[IrregularityVersions] ([IrregularityVersionId]),
    CONSTRAINT [FK_IrregularityVersionInvolvedPersons_Countries]             FOREIGN KEY ([CountryId])               REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_IrregularityVersionInvolvedPersons_Settlements]           FOREIGN KEY ([SettlementId])            REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [CHK_IrregularityVersionInvolvedPersons_LegalType]            CHECK       ([LegalType] IN (1, 2)),
    CONSTRAINT [CHK_IrregularityVersionInvolvedPersons_UinType]              CHECK       ([UinType] IN (0, 1, 2, 3))
);
GO

exec spDescTable  N'IrregularityVersionInvolvedPersons', N'Замесени лица към версия на нередност.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'IrregularityVersionInvolvedPersonId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'IrregularityVersionId'              , N'Идентификатор на версия на нередност.'

exec spDescColumn N'IrregularityVersionInvolvedPersons', N'LegalType'                          , N'Правен статут: 1 – ФЛ, 2 – ЮЛ.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'Uin'                                , N'Уникален идентификационен номер.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'UinType'                            , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'UndisclosureMotives'                , N'Основание за неразкриване.'

exec spDescColumn N'IrregularityVersionInvolvedPersons', N'CompanyName'                        , N'Наименование на фирмата.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'TradeName'                          , N'Търговско име.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'HoldingName'                        , N'Име на холдинга.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'FirstName'                          , N'Име.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'MiddleName'                         , N'Презиме.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'LastName'                           , N'Фамилно име.'

exec spDescColumn N'IrregularityVersionInvolvedPersons', N'CountryId'                          , N'Идентификатор на държава.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'SettlementId'                       , N'Идентификатор на териториална единица.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'PostCode'                           , N'Пощенски код.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'Street'                             , N'Улица, №, бл.'
exec spDescColumn N'IrregularityVersionInvolvedPersons', N'Address'                            , N'Пълен текст на адреса, без държава'
GO