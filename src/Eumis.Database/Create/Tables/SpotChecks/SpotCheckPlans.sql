PRINT 'SpotCheckPlans'
GO

CREATE TABLE [dbo].[SpotCheckPlans] (
    [SpotCheckPlanId]     INT              NOT NULL IDENTITY,
    [ProgrammeId]         INT              NOT NULL,
    [ContractId]          INT              NULL,
    [Level]               INT              NOT NULL,

    [Year]                INT              NOT NULL,
    [Month]               INT              NOT NULL,

    [PlaceCountryId]      INT              NOT NULL,
    [PlaceDistrictId]     INT              NULL,
    [PlaceMunicipalityId] INT              NULL,
    [PlaceSettlementId]   INT              NULL,
    [PlaceAddress]        NVARCHAR(MAX)    NULL,

    [IsConfirmed]         BIT              NOT NULL,
    [ConfirmationDate]    DATETIME2        NULL,
    [ConfirmationOrder]   NVARCHAR(200)    NULL,

    [CreateDate]          DATETIME2        NOT NULL,
    [ModifyDate]          DATETIME2        NOT NULL,
    [Version]             ROWVERSION       NOT NULL

    CONSTRAINT [PK_SpotCheckPlans]                   PRIMARY KEY ([SpotCheckPlanId]),
    CONSTRAINT [FK_SpotCheckPlans_Programmes]        FOREIGN KEY ([ProgrammeId])          REFERENCES [dbo].[MapNodes]       ([MapNodeId]),
    CONSTRAINT [FK_SpotCheckPlans_Contracts]         FOREIGN KEY ([ContractId])           REFERENCES [dbo].[Contracts]      ([ContractId]),
    CONSTRAINT [FK_SpotCheckPlans_Countries]         FOREIGN KEY ([PlaceCountryId])       REFERENCES [dbo].[Countries]      ([CountryId]),
    CONSTRAINT [FK_SpotCheckPlans_Districts]         FOREIGN KEY ([PlaceDistrictId])      REFERENCES [dbo].[Districts]      ([DistrictId]),
    CONSTRAINT [FK_SpotCheckPlans_Municipalities]    FOREIGN KEY ([PlaceMunicipalityId])  REFERENCES [dbo].[Municipalities] (MunicipalityId),
    CONSTRAINT [FK_SpotCheckPlans_Settlements]       FOREIGN KEY ([PlaceSettlementId])    REFERENCES [dbo].[Settlements]    ([SettlementId]),
    CONSTRAINT [CHK_SpotCheckPlans_Level]            CHECK ([Level]       IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_SpotCheckPlans_Year]             CHECK ([Year]        IN (2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023)),
    CONSTRAINT [CHK_SpotCheckPlans_Month]            CHECK ([Month]       IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

CREATE UNIQUE INDEX [UQ_SpotCheckPlans]
ON [SpotCheckPlans]([ProgrammeId], [Year], [Month]);

exec spDescTable  N'SpotCheckPlans', N'Годишни планове за проверки на място.'
exec spDescColumn N'SpotCheckPlans', N'SpotCheckPlanId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckPlans', N'ProgrammeId'         , N'Идентификатор на оперативна програма.'
exec spDescColumn N'SpotCheckPlans', N'ContractId'          , N'Идентификатор на договор.'
exec spDescColumn N'SpotCheckPlans', N'Level'               , N'Ниво: 1 - Програма, 2 – Приоритетна ос, 3 – Процедура, 4 - Договор за БФП, 5 - Договор с изпълнител.'

exec spDescColumn N'SpotCheckPlans', N'Year'                , N'Година.'
exec spDescColumn N'SpotCheckPlans', N'Month'               , N'Месец.'

exec spDescColumn N'SpotCheckPlans', N'PlaceCountryId'      , N'Място на провеждане на проверката - държава.'
exec spDescColumn N'SpotCheckPlans', N'PlaceDistrictId'     , N'Място на провеждане на проверката - област.'
exec spDescColumn N'SpotCheckPlans', N'PlaceMunicipalityId' , N'Място на провеждане на проверката - община.'
exec spDescColumn N'SpotCheckPlans', N'PlaceSettlementId'   , N'Място на провеждане на проверката - населено място.'
exec spDescColumn N'SpotCheckPlans', N'PlaceAddress'        , N'Място на провеждане на проверката - адрес за чуждестранни държави.'

exec spDescColumn N'SpotCheckPlans', N'IsConfirmed'         , N'Маркер за утвърждаване на ГП.'
exec spDescColumn N'SpotCheckPlans', N'ConfirmationDate'    , N'Дата на утвърждаване.'
exec spDescColumn N'SpotCheckPlans', N'ConfirmationOrder'   , N'Заповед за утвърждаване.'

exec spDescColumn N'SpotCheckPlans', N'CreateDate'          , N'Дата на създаване на записа.'
exec spDescColumn N'SpotCheckPlans', N'ModifyDate'          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'SpotCheckPlans', N'Version'             , N'Версия.'
GO
