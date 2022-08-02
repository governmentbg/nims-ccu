PRINT 'IrregularitySignalInvolvedPersons'
GO

CREATE TABLE [dbo].[IrregularitySignalInvolvedPersons] (
    [IrregularitySignalInvolvedPersonId] INT               NOT NULL IDENTITY,
    [IrregularitySignalId]               INT               NOT NULL,
    [LegalType]                          INT               NOT NULL,
    [Uin]                                NVARCHAR(200)     NOT NULL,
    [UinType]                            INT               NOT NULL,

    [CompanyName]                        NVARCHAR(200)     NULL,
    [TradeName]                          NVARCHAR(200)     NULL,
    [HoldingName]                        NVARCHAR(200)     NULL,
    [FirstName]                          NVARCHAR(100)     NULL,
    [MiddleName]                         NVARCHAR(100)     NULL,
    [LastName]                           NVARCHAR(100)     NULL,

    [CountryId]                          INT               NULL,
    [SettlementId]                       INT               NULL,
    [PostCode]                           NVARCHAR(50)      NULL,
    [Street]                             NVARCHAR(200)     NULL,
    [Address]                            NVARCHAR(MAX)     NULL,

    CONSTRAINT [PK_IrregularitySignalInvolvedPersons]                       PRIMARY KEY ([IrregularitySignalInvolvedPersonId]),
    CONSTRAINT [FK_IrregularitySignalInvolvedPersons_IrregularitySignalId]  FOREIGN KEY ([IrregularitySignalId])   REFERENCES [dbo].[IrregularitySignals] ([IrregularitySignalId]),
    CONSTRAINT [FK_IrregularitySignalInvolvedPersons_Countries]             FOREIGN KEY ([CountryId])              REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_IrregularitySignalInvolvedPersons_Settlements]           FOREIGN KEY ([SettlementId])           REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [CHK_IrregularitySignalInvolvedPersonss_LegalType]           CHECK       ([LegalType] IN (1, 2)),
    CONSTRAINT [CHK_IrregularitySignalInvolvedPersonss_UinType]             CHECK       ([UinType] IN (0, 1, 2, 3))
);
GO

exec spDescTable  N'IrregularitySignalInvolvedPersons', N'Замесени лица към сигнал за нередност.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'IrregularitySignalInvolvedPersonId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'IrregularitySignalId'              , N'Идентификатор на сигнал за нередност.'

exec spDescColumn N'IrregularitySignalInvolvedPersons', N'LegalType'                         , N'Правен статут: 1 – ФЛ, 2 – ЮЛ.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'Uin'                               , N'Уникален идентификационен номер.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'UinType'                           , N'0-ЕИК, 1-булстат, 2 - булстат за свободни професии (ЕГН), 3 - Чуждестранни фирми.'

exec spDescColumn N'IrregularitySignalInvolvedPersons', N'CompanyName'                       , N'Наименование на фирмата.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'TradeName'                         , N'Търговско име.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'HoldingName'                       , N'Име на холдинга.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'FirstName'                         , N'Име.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'MiddleName'                        , N'Презиме.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'LastName'                          , N'Фамилно име.'

exec spDescColumn N'IrregularitySignalInvolvedPersons', N'CountryId'                         , N'Идентификатор на държава.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'SettlementId'                      , N'Идентификатор на териториална единица.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'PostCode'                          , N'Пощенски код.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'Street'                            , N'Улица, №, бл.'
exec spDescColumn N'IrregularitySignalInvolvedPersons', N'Address'                           , N'Пълен текст на адреса, без държава'
GO