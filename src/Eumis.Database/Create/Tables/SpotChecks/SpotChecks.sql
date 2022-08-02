PRINT 'SpotChecks'
GO

CREATE TABLE [dbo].[SpotChecks] (
    [SpotCheckId]         INT               NOT NULL IDENTITY,
    [ProgrammeId]         INT               NOT NULL,
    [Level]               INT               NOT NULL,
    [ContractId]          INT               NULL,
    [SpotCheckPlanId]     INT               NULL,
    [Status]              INT               NOT NULL,
    [Type]                INT               NOT NULL,
    [RegNumber]           NVARCHAR(200)     NULL,
    [CheckNum]            INT               NULL,

    [PlaceCountryId]      INT               NOT NULL,
    [PlaceDistrictId]     INT               NULL,
    [PlaceMunicipalityId] INT               NULL,
    [PlaceSettlementId]   INT               NULL,
    [PlaceAddress]        NVARCHAR(MAX)     NULL,

    [Goal]                NVARCHAR(MAX)     NULL,
    [Team]                NVARCHAR(500)     NULL,
    [DateFrom]            DATETIME2         NULL,
    [DateTo]              DATETIME2         NULL,

    [OrderNum]            NVARCHAR(200)     NULL,
    [OrderDate]           DATETIME2         NULL,

    [ReportNum]           NVARCHAR(200)     NULL,
    [ReportDate]          DATETIME2         NULL,
    [ReportRecieveDate]   DATETIME2         NULL,

    [CreateDate]          DATETIME2         NOT NULL,
    [ModifyDate]          DATETIME2         NOT NULL,
    [Version]             ROWVERSION        NOT NULL

    CONSTRAINT [PK_SpotChecks]                   PRIMARY KEY ([SpotCheckId]),
    CONSTRAINT [FK_SpotChecks_Programmes]        FOREIGN KEY ([ProgrammeId])          REFERENCES [dbo].[MapNodes]       ([MapNodeId]),
    CONSTRAINT [FK_SpotChecks_Contracts]         FOREIGN KEY ([ContractId])           REFERENCES [dbo].[Contracts]      ([ContractId]),
    CONSTRAINT [FK_SpotChecks_SpotCheckPlans]    FOREIGN KEY ([SpotCheckPlanId])      REFERENCES [dbo].[SpotCheckPlans] ([SpotCheckPlanId]),

    CONSTRAINT [FK_SpotChecks_Countries]         FOREIGN KEY ([PlaceCountryId])       REFERENCES [dbo].[Countries]      ([CountryId]),
    CONSTRAINT [FK_SpotChecks_Districts]         FOREIGN KEY ([PlaceDistrictId])      REFERENCES [dbo].[Districts]      ([DistrictId]),
    CONSTRAINT [FK_SpotChecks_Municipalities]    FOREIGN KEY ([PlaceMunicipalityId])  REFERENCES [dbo].[Municipalities] (MunicipalityId),
    CONSTRAINT [FK_SpotChecks_Settlements]       FOREIGN KEY ([PlaceSettlementId])    REFERENCES [dbo].[Settlements]    ([SettlementId]),
    CONSTRAINT [CHK_SpotChecks_Level]            CHECK ([Level]       IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_SpotChecks_Type]             CHECK ([Type]        IN (1, 2)),
    CONSTRAINT [CHK_SpotChecks_Status]           CHECK ([Status]      IN (1, 2))
);
GO

exec spDescTable  N'SpotChecks', N'Проверки на място.'
exec spDescColumn N'SpotChecks', N'SpotCheckId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotChecks', N'ProgrammeId'        , N'Идентификатор на оперативна програма.'
exec spDescColumn N'SpotChecks', N'Level'              , N'Ниво: 1 - Програма, 2 – Приоритетна ос, 3 – Процедура, 4 - Договор за БФП, 5 - Договор с изпълнител.'
exec spDescColumn N'SpotChecks', N'ContractId'         , N'Идентификатор на договор.'
exec spDescColumn N'SpotChecks', N'Status'             , N'Статус на проверката: 1 - Чернова, 2 - Въведена.'
exec spDescColumn N'SpotChecks', N'Type'               , N'Тип на проверката: 1 - Планирана; 2 - Непланирана.'
exec spDescColumn N'SpotChecks', N'RegNumber'          , N'Регистрационен номер на проверката.'
exec spDescColumn N'SpotChecks', N'CheckNum'           , N'Пореден номер на проверката.'

exec spDescColumn N'SpotChecks', N'PlaceCountryId'     , N'Място на провеждане на проверката - държава.'
exec spDescColumn N'SpotChecks', N'PlaceDistrictId'    , N'Място на провеждане на проверката - област.'
exec spDescColumn N'SpotChecks', N'PlaceMunicipalityId', N'Място на провеждане на проверката - община.'
exec spDescColumn N'SpotChecks', N'PlaceSettlementId'  , N'Място на провеждане на проверката - населено място.'
exec spDescColumn N'SpotChecks', N'PlaceAddress'       , N'Място на провеждане на проверката - адрес за чуждестранни държави.'

exec spDescColumn N'SpotChecks', N'Goal'               , N'Цел на проверката.'
exec spDescColumn N'SpotChecks', N'Team'               , N'Екип, извършил проверката'
exec spDescColumn N'SpotChecks', N'DateFrom'           , N'Период на проверката - От'
exec spDescColumn N'SpotChecks', N'DateTo'             , N'Период на проверката - До'

exec spDescColumn N'SpotChecks', N'OrderNum'           , N'№ на заповед за проверка'
exec spDescColumn N'SpotChecks', N'OrderDate'          , N'Дата на заповед за проверка'

exec spDescColumn N'SpotChecks', N'ReportNum'          , N'№ на доклад за извършена проверка/лист за проверка'
exec spDescColumn N'SpotChecks', N'ReportDate'         , N'Дата на доклад за извършена проверка/лист за проверка'
exec spDescColumn N'SpotChecks', N'ReportRecieveDate'  , N'Дата на получаване на доклада/листа за проверка от бенефициента'

exec spDescColumn N'SpotChecks', N'CreateDate'         , N'Дата на създаване на записа.'
exec spDescColumn N'SpotChecks', N'ModifyDate'         , N'Дата на последно редактиране на записа.'
exec spDescColumn N'SpotChecks', N'Version'            , N'Версия.'
GO
