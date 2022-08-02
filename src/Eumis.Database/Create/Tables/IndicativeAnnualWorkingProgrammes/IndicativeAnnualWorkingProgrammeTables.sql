PRINT 'IndicativeAnnualWorkingProgrammeTables'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammeTables] (
    [IndicativeAnnualWorkingProgrammeTableId]           INT             NOT NULL IDENTITY,
    [IndicativeAnnualWorkingProgrammeId]                INT             NOT NULL,
    [ProcedureId]                                       INT             NOT NULL,

    [ProcedureStatus]                                   INT             NOT NULL,
    [ProgrammePriorityId]                               INT             NOT NULL,
    [OrderNum]                                          INT             NOT NULL,
    [ProcedureName]                                     NVARCHAR(MAX)   NOT NULL,
    [ProcedureNameAlt]                                  NVARCHAR(MAX)   NOT NULL,
    [ProcedureDescription]                              NVARCHAR(MAX)   NOT NULL,
    [ProcedureDescriptionAlt]                           NVARCHAR(MAX)   NOT NULL,
    [IndicativeAnnualWorkingProgrammeTypeConducting]    INT             NOT NULL,
    [WithPreSelection]                                  BIT             NOT NULL,
    [ProcedureTotalAmount]                              MONEY           NOT NULL,

    [EligibleActivities]                                NVARCHAR(MAX)   NOT NULL,
    [EligibleActivitiesAlt]                             NVARCHAR(MAX)   NOT NULL,

    [EligibleCosts]                                     NVARCHAR(MAX)   NOT NULL,
    [EligibleCostsAlt]                                  NVARCHAR(MAX)   NOT NULL,

    [MaxPercentCoFinancing]                             MONEY           NOT NULL,
    [MaxPercentCoFinancingInfo]                         NVARCHAR(MAX)   NOT NULL,
    [MaxPercentCoFinancingInfoAlt]                      NVARCHAR(MAX)   NOT NULL,

    [ListingDate]                                       DATETIME2       NOT NULL,

    [IsStateAssistance]                                 INT             NOT NULL,
    [IsMinimalAssistance]                               INT             NOT NULL,

    [ProjectMinAmount]                                  MONEY           NOT NULL,
    [ProjectMinAmountInfo]                              NVARCHAR(MAX)   NOT NULL,
    [ProjectMinAmountInfoAlt]                           NVARCHAR(MAX)   NOT NULL,

    [ProjectMaxAmount]                                  MONEY           NOT NULL,
    [ProjectMaxAmountInfo]                              NVARCHAR(MAX)   NOT NULL,
    [ProjectMaxAmountInfoAlt]                           NVARCHAR(MAX)   NOT NULL,

    [CreateDate]                                        DATETIME2       NOT NULL,
    [ModifyDate]                                        DATETIME2       NOT NULL,
    [Version]                                           ROWVERSION      NOT NULL,

    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammeTables]                                                  PRIMARY KEY ([IndicativeAnnualWorkingProgrammeTableId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTables_IndicativeAnnualWorkingProgrammes]                FOREIGN KEY ([IndicativeAnnualWorkingProgrammeId])      REFERENCES [dbo].[IndicativeAnnualWorkingProgrammes] ([IndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTables_ProgrammePriorities]                              FOREIGN KEY ([ProgrammePriorityId])                     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTables_Procedures]                                       FOREIGN KEY ([ProcedureId])                             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammeTables_ProcedureStatus]                                 CHECK ([ProcedureStatus] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammeTables_IndicativeAnnualWorkingProgrammeTypeConducting]  CHECK ([IndicativeAnnualWorkingProgrammeTypeConducting] IN (1, 2)),
);
GO

exec spDescTable  N'IndicativeAnnualWorkingProgrammeTables', N'Таблица за индикативна годишна работна програма (ИГРП).'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'IndicativeAnnualWorkingProgrammeTableId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'IndicativeAnnualWorkingProgrammeId'              , N'Идентификатор на индикативна годишна работна програма.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProcedureId'                                     , N'Идентификатор на процедурата.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProcedureStatus'                                 , N'Статус на процедура в момента на генериране на таблицата.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProgrammePriorityId'                             , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'OrderNum'                                        , N'Пореден номер.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProcedureName'                                   , N'Наименование на процедурата.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProcedureNameAlt'                                , N'Наименование на процедурата на английски език.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProcedureDescription'                            , N'Описание/Цели на предоставяната БФП по процедурата.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProcedureDescriptionAlt'                         , N'Описание/Цели на предоставяната БФП по процедурата на английски език.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'IndicativeAnnualWorkingProgrammeTypeConducting'  , N'Начин на провеждане на процедурата съгласно чл. 2 от ПМС № 162/2016 г.: 1 - Процедура на подбор на проекти, 2 - Процедура на директно предоставяне.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'WithPreSelection'                                , N'Извършване на предварителен подбор на концепции за проектни предложения.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProcedureTotalAmount'                            , N'Общ размер на БФП  по процедурата /лв./.'

exec spDescColumn  N'IndicativeAnnualWorkingProgrammeTables', N'EligibleActivities'                              , N'Примерни допустими дейности.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'EligibleActivitiesAlt'                           , N'Примерни допустими дейности на английски език.'

exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'EligibleCosts'                                   , N'Категории допустими разходи.'
exec spDescColumn  N'IndicativeAnnualWorkingProgrammeTables', N'EligibleCostsAlt'                                , N'Категории допустими разходи на английски език.'

exec spDescColumn  N'IndicativeAnnualWorkingProgrammeTables', N'MaxPercentCoFinancing'                           , N'Максимален % на съ-финансиране.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'MaxPercentCoFinancingInfo'                       , N'Информация към максимален % на съ-финансиране.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'MaxPercentCoFinancingInfoAlt'                    , N'Информация на английски език към максимален % на съ-финансиране.'

exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ListingDate'                                     , N'Дата на обявяване на процедурата.'

exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'IsStateAssistance'                               , N'Процедурата(част от нея) представлява държавна помощ.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'IsMinimalAssistance'                             , N'Процедурата(част от нея) представлява минимална  помощ.'

exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProjectMinAmount'                                , N'Минимален размер на БФП за проект /лв./.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProjectMinAmountInfo'                            , N'Информация към минимален размер на БФП за проект /лв./.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProjectMinAmountInfoAlt'                         , N'Информация на английски език към минимален размер на БФП за проект /лв./.'

exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProjectMaxAmount'                                , N'Максимален размер на БФП за проект /лв./.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProjectMaxAmountInfo'                            , N'Информация към максимален размер на БФП за проект /лв./.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ProjectMaxAmountInfoAlt'                         , N'Информация на английски език към максимален размер на БФП за проект /лв./.'

exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'CreateDate'                                      , N'Дата на създаване на записа.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'ModifyDate'                                      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammeTables', N'Version'                                         , N'Версия.'
GO
