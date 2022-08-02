PRINT 'Prognoses'
GO

CREATE TABLE [dbo].[Prognoses] (
    [PrognosisId]                  INT           NOT NULL IDENTITY,
    [Level]                        INT           NOT NULL,
    [ProgrammeId]                  INT           NULL,
    [ProgrammePriorityId]          INT           NULL,
    [ProcedureId]                  INT           NULL,
    [Status]                       INT           NOT NULL,

    [Year]                         INT           NOT NULL,
    [Month]                        INT           NOT NULL,
    [FinanceSource]                INT           NOT NULL,

    [ContractedEuAmount]           MONEY         NULL,
    [ContractedBgAmount]           MONEY         NULL,
    [ContractedBfpAmount]          MONEY         NULL,

    [PaymentEuAmount]              MONEY         NULL,
    [PaymentBgAmount]              MONEY         NULL,
    [PaymentBfpAmount]             MONEY         NULL,

    [AdvancePaymentEuAmount]       MONEY         NULL,
    [AdvancePaymentBgAmount]       MONEY         NULL,
    [AdvancePaymentBfpAmount]      MONEY         NULL,

    [AdvanceVerPaymentEuAmount]    MONEY         NULL,
    [AdvanceVerPaymentBgAmount]    MONEY         NULL,
    [AdvanceVerPaymentBfpAmount]   MONEY         NULL,

    [IntermediatePaymentEuAmount]  MONEY         NULL,
    [IntermediatePaymentBgAmount]  MONEY         NULL,
    [IntermediatePaymentBfpAmount] MONEY         NULL,

    [FinalPaymentEuAmount]         MONEY         NULL,
    [FinalPaymentBgAmount]         MONEY         NULL,
    [FinalPaymentBfpAmount]        MONEY         NULL,

    [ApprovedEuAmount]             MONEY         NULL,
    [ApprovedBgAmount]             MONEY         NULL,
    [ApprovedBfpAmount]            MONEY         NULL,

    [CertifiedEuAmount]            MONEY         NULL,
    [CertifiedBgAmount]            MONEY         NULL,
    [CertifiedBfpAmount]           MONEY         NULL,

    [IsActivated]                  BIT           NOT NULL,
    [DeleteNote]                   NVARCHAR(MAX) NULL,
    [CreateDate]                   DATETIME2     NOT NULL,
    [ModifyDate]                   DATETIME2     NOT NULL,
    [Version]                      ROWVERSION    NOT NULL,

    CONSTRAINT [PK_Prognoses]                         PRIMARY KEY ([PrognosisId]),
    CONSTRAINT [FK_Prognoses_Programmes]              FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_Prognoses_ProgrammePriorities]     FOREIGN KEY ([ProgrammePriorityId]) REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_Prognoses_Procedures]              FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_Prognoses_Level]                  CHECK       ([Level] IN (1, 2, 3)),
    CONSTRAINT [CHK_Prognoses_LevelProgramme]         CHECK       ([Level] != 1 OR [ProgrammeId] IS NOT NULL),
    CONSTRAINT [CHK_Prognoses_LevelProgrammePriority] CHECK       ([Level] != 2 OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_Prognoses_LevelProcedure]         CHECK       ([Level] != 3 OR [ProcedureId] IS NOT NULL),
    CONSTRAINT [CHK_Prognoses_Status]                 CHECK       ([Status] IN (1, 2, 3)),
    CONSTRAINT [CHK_Prognoses_Year]                   CHECK       ([Year] IN (2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023)),
    CONSTRAINT [CHK_Prognoses_Month]                  CHECK       ([Month] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_Prognoses_FinanceSource]          CHECK       ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProgramme]
ON [Prognoses]([ProgrammeId], [Year], [Month], [FinanceSource])
WHERE [Level] = 1;

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProgrammePriority]
ON [Prognoses]([ProgrammePriorityId], [Year], [Month], [FinanceSource])
WHERE [Level] = 2;

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProcedure]
ON [Prognoses]([ProcedureId], [Year], [Month], [FinanceSource])
WHERE [Level] = 3;

exec spDescTable  N'Prognoses', N'Прогнози.'
exec spDescColumn N'Prognoses', N'PrognosisId'                 , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Prognoses', N'Level'                       , N'Ниво: 1 - Програма, 2 - Приоритетна ос, 3 - Процедура.'
exec spDescColumn N'Prognoses', N'ProgrammeId'                 , N'Идентификатор на програма.'
exec spDescColumn N'Prognoses', N'ProgrammePriorityId'         , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'Prognoses', N'ProcedureId'                 , N'Идентификатор на процедура.'
exec spDescColumn N'Prognoses', N'Status'                      , N'Статус: 1 - Чернова;  2 - Въведена; 3 - Анулирана.'

exec spDescColumn N'Prognoses', N'Year'                        , N'Година.'
exec spDescColumn N'Prognoses', N'Month'                       , N'Месец.'
exec spDescColumn N'Prognoses', N'FinanceSource'               , N'Фонд.'

exec spDescColumn N'Prognoses', N'ContractedEuAmount'          , N'Прогноза за договаряне - Финансиране от ЕС.'
exec spDescColumn N'Prognoses', N'ContractedBgAmount'          , N'Прогноза за договаряне - Финансиране от НФ.'
exec spDescColumn N'Prognoses', N'ContractedBfpAmount'         , N'Прогноза за договаряне - БФП.'

exec spDescColumn N'Prognoses', N'PaymentEuAmount'             , N'Прогноза за плащане - Финансиране от ЕС.'
exec spDescColumn N'Prognoses', N'PaymentBgAmount'             , N'Прогноза за плащане - Финансиране от НФ.'
exec spDescColumn N'Prognoses', N'PaymentBfpAmount'            , N'Прогноза за плащане - БФП.'

exec spDescColumn N'Prognoses', N'AdvancePaymentEuAmount'      , N'Прогноза за авансово плащане - Финансиране от ЕС.'
exec spDescColumn N'Prognoses', N'AdvancePaymentBgAmount'      , N'Прогноза за авансово плащане - Финансиране от НФ.'
exec spDescColumn N'Prognoses', N'AdvancePaymentBfpAmount'     , N'Прогноза за авансово плащане - БФП.'

exec spDescColumn N'Prognoses', N'AdvanceVerPaymentEuAmount'   , N'Прогноза за авансово плащане - Финансиране от ЕС.'
exec spDescColumn N'Prognoses', N'AdvanceVerPaymentBgAmount'   , N'Прогноза за авансово плащане - Финансиране от НФ.'
exec spDescColumn N'Prognoses', N'AdvanceVerPaymentBfpAmount'  , N'Прогноза за авансово плащане - БФП.'

exec spDescColumn N'Prognoses', N'IntermediatePaymentEuAmount' , N'Прогноза за междинни плащания - Финансиране от ЕС.'
exec spDescColumn N'Prognoses', N'IntermediatePaymentBgAmount' , N'Прогноза за междинни плащания - Финансиране от НФ.'
exec spDescColumn N'Prognoses', N'IntermediatePaymentBfpAmount', N'Прогноза за междинни плащания - БФП.'

exec spDescColumn N'Prognoses', N'FinalPaymentEuAmount'        , N'Прогноза за окончателни плащания - Финансиране от ЕС.'
exec spDescColumn N'Prognoses', N'FinalPaymentBgAmount'        , N'Прогноза за окончателни плащания - Финансиране от НФ.'
exec spDescColumn N'Prognoses', N'FinalPaymentBfpAmount'       , N'Прогноза за окончателни плащания - БФП.'

exec spDescColumn N'Prognoses', N'ApprovedEuAmount'            , N'Прогноза за верифициране - Финансиране от ЕС.'
exec spDescColumn N'Prognoses', N'ApprovedBgAmount'            , N'Прогноза за верифициране - Финансиране от НФ.'
exec spDescColumn N'Prognoses', N'ApprovedBfpAmount'           , N'Прогноза за верифициране - БФП.'

exec spDescColumn N'Prognoses', N'CertifiedEuAmount'           , N'Прогноза за сертифициране - Финансиране от ЕС.'
exec spDescColumn N'Prognoses', N'CertifiedBgAmount'           , N'Прогноза за сертифициране - Финансиране от НФ.'
exec spDescColumn N'Prognoses', N'CertifiedBfpAmount'          , N'Прогноза за сертифициране - БФП.'

exec spDescColumn N'Prognoses', N'IsActivated'                 , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'Prognoses', N'DeleteNote'                  , N'Причина за изтриване.'
exec spDescColumn N'Prognoses', N'CreateDate'                  , N'Дата на създаване на записа.'
exec spDescColumn N'Prognoses', N'ModifyDate'                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Prognoses', N'Version'                     , N'Версия.'
GO