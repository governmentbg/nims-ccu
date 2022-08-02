PRINT 'Irregularities'
GO

CREATE TABLE [dbo].[Irregularities] (
    [IrregularityId]        INT                 NOT NULL IDENTITY,
    [IrregularitySignalId]  INT                 NOT NULL UNIQUE,
    [ProgrammeId]           INT                 NOT NULL,
    [ContractId]            INT                 NULL,
    [Status]                INT                 NOT NULL,
    [FinanceSource]         INT                 NOT NULL,
    [RegNumber]             NVARCHAR(200)       NULL,
    [RegNumberPattern]      NVARCHAR(200)       NULL,
    [IrregularityEndDate]   DATETIME2           NULL,
    [CaseState]             INT                 NULL,

    [FirstReportYear]       INT                 NULL,
    [FirstReportQuarter]    INT                 NULL,
    [LastReportYear]        INT                 NULL,
    [LastReportQuarter]     INT                 NULL,

    [DeleteNote]            NVARCHAR(MAX)       NULL,
    [CreateDate]            DATETIME2           NOT NULL,
    [ModifyDate]            DATETIME2           NOT NULL,
    [Version]               ROWVERSION          NOT NULL

    CONSTRAINT [PK_Irregularities]                      PRIMARY KEY ([IrregularityId]),
    CONSTRAINT [FK_Irregularities_IrregularitySignals]  FOREIGN KEY ([IrregularitySignalId])     REFERENCES [dbo].[IrregularitySignals]      ([IrregularitySignalId]),
    CONSTRAINT [FK_Irregularities_Programmes]           FOREIGN KEY ([ProgrammeId])              REFERENCES [dbo].[MapNodes]      ([MapNodeId]),
    CONSTRAINT [FK_Irregularities_Contracts]            FOREIGN KEY ([ContractId])               REFERENCES [dbo].[Contracts]     ([ContractId]),
    CONSTRAINT [CHK_Irregularities_Status]              CHECK ([Status]              IN (1, 2, 3)),
    CONSTRAINT [CHK_Irregularities_FinanceSource]       CHECK ([FinanceSource]       IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_Irregularities_CaseState]           CHECK ([CaseState]           IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_Irregularities_FirstReportYear]     CHECK ([FirstReportYear]     IN (2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023)),
    CONSTRAINT [CHK_Irregularities_FirstReportQuarter]  CHECK ([FirstReportQuarter]  IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_Irregularities_LastReportYear]      CHECK ([LastReportYear]      IN (2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023)),
    CONSTRAINT [CHK_Irregularities_LastReportQuarter]   CHECK ([LastReportQuarter]   IN (1, 2, 3, 4))
);
GO

CREATE UNIQUE INDEX [UQ_Irregularities_Number]
ON [Irregularities]([RegNumber])
WHERE [Status] = 2;
GO

exec spDescTable  N'Irregularities', N'Нередности.'
exec spDescColumn N'Irregularities', N'IrregularityId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Irregularities', N'IrregularitySignalId' , N'Идентификатор на сигнал за нередност.'
exec spDescColumn N'Irregularities', N'ProgrammeId'          , N'Идентификатор на оперативна програма'
exec spDescColumn N'Irregularities', N'ContractId'           , N'Идентификатор на договор за БФП.'
exec spDescColumn N'Irregularities', N'Status'               , N'Статус: 1 - Нова; 2 - Въведена; 3 - Анулирана.'
exec spDescColumn N'Irregularities', N'FinanceSource'        , N'Фонд: 1 – ЕСФ, 2 – ЕФРР, 3- КФ, 4 – ИМЗ, 5 – ФЕПНЛ.'
exec spDescColumn N'Irregularities', N'RegNumber'            , N'Национален номер на случая.'
exec spDescColumn N'Irregularities', N'RegNumberPattern'     , N'Шаблон за национален номер на случая.'
exec spDescColumn N'Irregularities', N'IrregularityEndDate'  , N'Дата на приключване на нередността.'
exec spDescColumn N'Irregularities', N'CaseState'            , N'Състояние на случая: 1 – активен, 2 – приключен, 3 – прекратен, 4 - отпаднал.'

exec spDescColumn N'Irregularities', N'FirstReportYear'      , N'Година на първоначално докладване.'
exec spDescColumn N'Irregularities', N'FirstReportQuarter'   , N'Тримесечие на първоначално докладване: 1 - Януари - Март; 2 - Април - Юни; 3 - Юли - Септември; 4 - Октомври - Декември.'
exec spDescColumn N'Irregularities', N'LastReportYear'       , N'Година на последващо докладване.'
exec spDescColumn N'Irregularities', N'LastReportQuarter'    , N'Тримесечие на последващо докладване: 1 - Януари - Март; 2 - Април - Юни; 3 - Юли - Септември; 4 - Октомври - Декември.'

exec spDescColumn N'Irregularities', N'DeleteNote'           , N'Причина за анулиране.'
exec spDescColumn N'Irregularities', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'Irregularities', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Irregularities', N'Version'              , N'Версия.'
GO
