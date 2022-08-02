PRINT 'ContractReportMicrosType2Items'
GO

CREATE TABLE [dbo].[ContractReportMicrosType2Items] (
    [ContractReportMicrosType2ItemId]           INT           NOT NULL IDENTITY,
    [ContractReportMicroId]                     INT           NOT NULL,

    [Number]                                    NVARCHAR(200) NULL,
    [FirstName]                                 NVARCHAR(200) NULL,
    [MiddleName]                                NVARCHAR(200) NULL,
    [LastName]                                  NVARCHAR(200) NULL,
    [Uin]                                       NVARCHAR(200) NULL,
    [Gender]                                    INT           NULL,
    [Age]                                       INT           NULL,
    [Occupation]                                INT           NULL,
    [Education]                                 INT           NULL,
    [AddressDistrictId]                         INT           NULL,
    [AddressSettlementId]                       INT           NULL,
    [Phone]                                     NVARCHAR(200) NULL,
    [Email]                                     NVARCHAR(200) NULL,

    [IsEmigrant]                                BIT           NULL,
    [IsForeigner]                               BIT           NULL,
    [IsMinority]                                BIT           NULL,
    [IsGypsy]                                   BIT           NULL,
    [IsDisabledPerson]                          BIT           NULL,
    [IsHomeless]                                BIT           NULL,
    [DisadvantagedPerson]                       NVARCHAR(MAX) NULL,

    [IsLivingInUnemployedHousehold]             BIT           NULL,
    [IsLivingInUnemployedHouseholdWithChildren] BIT           NULL,
    [IsLivingInFamilyOfOneWithChildren]         BIT           NULL,

    [JoiningDate]                               DATETIME2     NULL,
    [Activity]                                  NVARCHAR(MAX) NULL,
    [ActivityPlaceDistrictId]                   INT           NULL,
    [ActivityPlaceSettlementId]                 INT           NULL,

    [ParticipationState]                        INT           NULL,
    [LeavingDate]                               DATETIME2     NULL,

    [CancelationReason]                         INT           NULL,
    [LeavingState]                              INT           NULL,

    CONSTRAINT [PK_ContractReportMicrosType2Items]                      PRIMARY KEY ([ContractReportMicrosType2ItemId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_ContractReportMicros] FOREIGN KEY ([ContractReportMicroId]) REFERENCES [dbo].[ContractReportMicros]            ([ContractReportMicroId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_AddressDistricts]     FOREIGN KEY ([AddressDistrictId])     REFERENCES [dbo].[ContractReportMicrosDistricts]   ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_AddressSettlements]   FOREIGN KEY ([AddressSettlementId])   REFERENCES [dbo].[ContractReportMicrosSettlements] ([ContractReportMicrosSettlementId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_ActivityDistricts]    FOREIGN KEY ([ActivityPlaceDistrictId])     REFERENCES [dbo].[ContractReportMicrosDistricts]   ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_ActivitySettlements]  FOREIGN KEY ([ActivityPlaceSettlementId])   REFERENCES [dbo].[ContractReportMicrosSettlements] ([ContractReportMicrosSettlementId]),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_Gender]              CHECK ([Gender]             IN (1, 2)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_Occupation]          CHECK ([Occupation]         IN (1, 2, 3, 4, 5, 6, 7, 8)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_Education]           CHECK ([Education]          IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_ParticipationState]  CHECK ([ParticipationState] IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_CancelationReason]   CHECK ([CancelationReason]  IN (1, 2, 3, 4, 5, 6)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_LeavingState]        CHECK ([LeavingState]       IN (1, 2, 3, 4, 5, 6, 7, 8))
);
GO

exec spDescTable  N'ContractReportMicrosType2Items', N'Микроданни участници (ЕСФ).'
exec spDescColumn N'ContractReportMicrosType2Items', N'ContractReportMicrosType2ItemId'          , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportMicrosType2Items', N'ContractReportMicroId'                    , N'Идентификатор на микроданни.'

exec spDescColumn N'ContractReportMicrosType2Items', N'Number'                                   , N'Номер.'
exec spDescColumn N'ContractReportMicrosType2Items', N'FirstName'                                , N'Име.'
exec spDescColumn N'ContractReportMicrosType2Items', N'MiddleName'                               , N'Презиме.'
exec spDescColumn N'ContractReportMicrosType2Items', N'LastName'                                 , N'Фамилия.'
exec spDescColumn N'ContractReportMicrosType2Items', N'Uin'                                      , N'ЕГН.'
exec spDescColumn N'ContractReportMicrosType2Items', N'Gender'                                   , N'Пол: 1 - мъж; 2 - жена.'
exec spDescColumn N'ContractReportMicrosType2Items', N'Age'                                      , N'Възраст.'
exec spDescColumn N'ContractReportMicrosType2Items', N'Occupation'                               , N'Статут на пазара на труда: 1 - заето лице; 2 - самонаето лице; 3 - безработно лице до 6 месеца; 4 - безработно лице над 6 месеца; 5 - безработно лице над 12 месеца; 6 - неактивно лице; 7 - неактивно лице извън образование или обучение.'
exec spDescColumn N'ContractReportMicrosType2Items', N'Education'                                , N'Степен на завършено образование: 1 - Без образование; 2 - Основно образование (начално  или прогимназиално); 3 - Средно образование (гимназия, техникум и т.н.); 4 - Следгимназиално (професионално обучение след средно образование – ІV степен); 5 - Висше образование.'
exec spDescColumn N'ContractReportMicrosType2Items', N'AddressDistrictId'                        , N'Настоящ адрес - област.'
exec spDescColumn N'ContractReportMicrosType2Items', N'AddressSettlementId'                      , N'Настоящ адрес - населено място.'
exec spDescColumn N'ContractReportMicrosType2Items', N'Phone'                                    , N'Телефон за контакт.'
exec spDescColumn N'ContractReportMicrosType2Items', N'Email'                                    , N'E-mail.'

exec spDescColumn N'ContractReportMicrosType2Items', N'IsEmigrant'                               , N'Емигрант.'
exec spDescColumn N'ContractReportMicrosType2Items', N'IsForeigner'                              , N'Участник с произход от друга държава.'
exec spDescColumn N'ContractReportMicrosType2Items', N'IsMinority'                               , N'Малцинства.'
exec spDescColumn N'ContractReportMicrosType2Items', N'IsGypsy'                                  , N'Роми.'
exec spDescColumn N'ContractReportMicrosType2Items', N'IsDisabledPerson'                         , N'Хора с увреждания.'
exec spDescColumn N'ContractReportMicrosType2Items', N'IsHomeless'                               , N'Бездомни или засегнати от жилищно изключване.'
exec spDescColumn N'ContractReportMicrosType2Items', N'DisadvantagedPerson'                      , N'Други хора в неравностойно положение.'

exec spDescColumn N'ContractReportMicrosType2Items', N'IsLivingInUnemployedHousehold'            , N'Участници, които живеят в безработни домакинства.'
exec spDescColumn N'ContractReportMicrosType2Items', N'IsLivingInUnemployedHouseholdWithChildren', N'Участници, които живеят в безработни домакинства с деца на издръжка.'
exec spDescColumn N'ContractReportMicrosType2Items', N'IsLivingInFamilyOfOneWithChildren'        , N'Участници, които живеят в едночленно домакинство с деца на издръжка.'

exec spDescColumn N'ContractReportMicrosType2Items', N'JoiningDate'                              , N'Дата на включване в дейности.'
exec spDescColumn N'ContractReportMicrosType2Items', N'Activity'                                 , N'Дейност: 1 - Потребител на услуги; 2 - Личен асистент.'
exec spDescColumn N'ContractReportMicrosType2Items', N'ActivityPlaceDistrictId'                  , N'Място на изпълнение на дейността - област.'
exec spDescColumn N'ContractReportMicrosType2Items', N'ActivityPlaceSettlementId'                , N'Място на изпълнение на дейността - населено място.'

exec spDescColumn N'ContractReportMicrosType2Items', N'ParticipationState'                       , N'Статут на участие: 1 - лицето е напуснало операцията преди планирания край на дейностите; 2 - лицето е участвало до планирания край на дейностите.'
exec spDescColumn N'ContractReportMicrosType2Items', N'LeavingDate'                              , N'Дата на напускане на дейности.'

exec spDescColumn N'ContractReportMicrosType2Items', N'CancelationReason'                        , N'Причини за отпадане: 1 - Получих предложение за друга работа; 2 - Нямах възможност да продължа участие поради лични причини; 3 - Получих предложение за продължаване на образование/обучение; 4 - Друго; 5 - Промених своето местоживеене; 6 - Преместих се в друга образователна институция'
exec spDescColumn N'ContractReportMicrosType2Items', N'LeavingState'                             , N'Статут на участника при излизане от операцията: 1 - което не е получило предложение за работа, обучение или образование; 2 - ангажирано с образование/обучение; 3 - получило предложение за работа, обучение или образование; 4 - което е заето при същия работодател; 5 - което е заето при друг работодател; 6 - което е самонаето; 7 - което е отпаднало от образователната система; 8 - Друго'
GO
