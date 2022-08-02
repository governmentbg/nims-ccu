PRINT 'ContractReportIndicators'
GO

CREATE TABLE [dbo].[ContractReportIndicators] (
    [ContractReportIndicatorId]             INT               NOT NULL IDENTITY,
    [ContractReportTechnicalId]             INT               NOT NULL,
    [ContractIndicatorId]                   INT               NOT NULL,
    [ContractReportId]                      INT               NOT NULL,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [Name]                                  NVARCHAR(MAX)     NOT NULL,
    [Type]                                  INT               NOT NULL,
    [Kind]                                  INT               NOT NULL,
    [Trend]                                 INT               NOT NULL,
    [AggregatedReport]                      INT               NOT NULL,
    [AggregatedTarget]                      INT               NOT NULL,
    [HasGenderDivision]                     BIT               NOT NULL,
    [MeasureName]                           NVARCHAR(MAX)     NOT NULL,

    [Status]                                INT               NOT NULL,
    [Approval]                              INT               NULL,
    [Notes]                                 NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                       INT               NULL,
    [CheckedDate]                           DATETIME2         NULL,

    [PeriodAmountMen]                       DECIMAL(15,3)     NULL,
    [PeriodAmountWomen]                     DECIMAL(15,3)     NULL,
    [PeriodAmountTotal]                     DECIMAL(15,3)     NOT NULL,

    [CumulativeAmountMen]                   DECIMAL(15,3)     NULL,
    [CumulativeAmountWomen]                 DECIMAL(15,3)     NULL,
    [CumulativeAmountTotal]                 DECIMAL(15,3)     NOT NULL,

    [ResidueAmountMen]                      DECIMAL(15,3)     NULL,
    [ResidueAmountWomen]                    DECIMAL(15,3)     NULL,
    [ResidueAmountTotal]                    DECIMAL(15,3)     NOT NULL,

    [LastReportCumulativeAmountMen]         DECIMAL(15,3)     NULL,
    [LastReportCumulativeAmountWomen]       DECIMAL(15,3)     NULL,
    [LastReportCumulativeAmountTotal]       DECIMAL(15,3)     NOT NULL,

    [Comment]                               NVARCHAR(MAX)     NULL,

    [ApprovedPeriodAmountMen]               DECIMAL(15,3)     NULL,
    [ApprovedPeriodAmountWomen]             DECIMAL(15,3)     NULL,
    [ApprovedPeriodAmountTotal]             DECIMAL(15,3)     NULL,

    [ApprovedCumulativeAmountMen]           DECIMAL(15,3)     NULL,
    [ApprovedCumulativeAmountWomen]         DECIMAL(15,3)     NULL,
    [ApprovedCumulativeAmountTotal]         DECIMAL(15,3)     NULL,

    [ApprovedResidueAmountMen]              DECIMAL(15,3)     NULL,
    [ApprovedResidueAmountWomen]            DECIMAL(15,3)     NULL,
    [ApprovedResidueAmountTotal]            DECIMAL(15,3)     NULL,

    [CreateDate]                            DATETIME2         NOT NULL,
    [ModifyDate]                            DATETIME2         NOT NULL,
    [Version]                               ROWVERSION        NOT NULL,

    CONSTRAINT [ContractReportIndicatorId]                               PRIMARY KEY ([ContractReportIndicatorId]),
    CONSTRAINT [FK_ContractReportIndicators_ContractReportTechnicals]    FOREIGN KEY ([ContractReportTechnicalId])         REFERENCES [dbo].[ContractReportTechnicals] ([ContractReportTechnicalId]),
    CONSTRAINT [FK_ContractReportIndicators_ContractIndicators]          FOREIGN KEY ([ContractIndicatorId])               REFERENCES [dbo].[ContractIndicators] ([ContractIndicatorId]),
    CONSTRAINT [FK_ContractReportIndicators_ContractReports]             FOREIGN KEY ([ContractReportId])                  REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportIndicators_Contracts]                   FOREIGN KEY ([ContractId])                        REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportIndicators_CheckedByUser]               FOREIGN KEY ([CheckedByUserId])                   REFERENCES [dbo].[Users] ([UserId])
);
GO

exec spDescTable  N'ContractReportIndicators', N'Верифицирани индикатори.'
exec spDescColumn N'ContractReportIndicators', N'ContractReportIndicatorId'        , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportIndicators', N'ContractReportTechnicalId'        , N'Идентификатор на технически отчет'
exec spDescColumn N'ContractReportIndicators', N'ContractReportId'                 , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportIndicators', N'ContractId'                       , N'Идентификатор на договор'
exec spDescColumn N'ContractReportIndicators', N'Gid'                              , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportIndicators', N'Status'                           , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportIndicators', N'HasGenderDivision'                , N'Разпределение по полове.'
exec spDescColumn N'ContractReportIndicators', N'Approval'                         , N'Одобрение: 1- Одобрен; 2 - Неодобрен.'
exec spDescColumn N'ContractReportIndicators', N'Notes'                            , N'Бележки.'
exec spDescColumn N'ContractReportIndicators', N'CheckedByUserId'                  , N'Проверено от.'
exec spDescColumn N'ContractReportIndicators', N'CheckedDate'                      , N'Дата на проверка.'

exec spDescColumn N'ContractReportIndicators', N'PeriodAmountMen'                  , N'Отчетена стойност за периода (мъже).'
exec spDescColumn N'ContractReportIndicators', N'PeriodAmountWomen'                , N'Отчетена стойност за периода (жени).'
exec spDescColumn N'ContractReportIndicators', N'PeriodAmountTotal'                , N'Отчетена стойност за периода.'

exec spDescColumn N'ContractReportIndicators', N'CumulativeAmountMen'              , N'Отчетена стойност с натрупване (мъже).'
exec spDescColumn N'ContractReportIndicators', N'CumulativeAmountWomen'            , N'Отчетена стойност с натрупване (жени).'
exec spDescColumn N'ContractReportIndicators', N'CumulativeAmountTotal'            , N'Отчетена стойност с натрупване.'

exec spDescColumn N'ContractReportIndicators', N'ResidueAmountMen'                 , N'Остатък/отклонение спрямо зададеното в договора (мъже).'
exec spDescColumn N'ContractReportIndicators', N'ResidueAmountWomen'               , N'Остатък/отклонение спрямо зададеното в договора (жени).'
exec spDescColumn N'ContractReportIndicators', N'ResidueAmountTotal'               , N'Остатък/отклонение спрямо зададеното в договора.'

exec spDescColumn N'ContractReportIndicators', N'LastReportCumulativeAmountMen'    , N'Отчетена стойност от последния отчет (мъже).'
exec spDescColumn N'ContractReportIndicators', N'LastReportCumulativeAmountWomen'  , N'Отчетена стойност от последния отчет (жени).'
exec spDescColumn N'ContractReportIndicators', N'LastReportCumulativeAmountTotal'  , N'Отчетена стойност от последния отчет.'

exec spDescColumn N'ContractReportIndicators', N'Comment'                          , N'Дата на проверка.'

exec spDescColumn N'ContractReportIndicators', N'ApprovedPeriodAmountMen'          , N'Одобрена отчетена стойност за периода.'
exec spDescColumn N'ContractReportIndicators', N'ApprovedPeriodAmountWomen'        , N'Одобрена отчетена стойност за периода.'
exec spDescColumn N'ContractReportIndicators', N'ApprovedPeriodAmountTotal'        , N'Одобрена отчетена стойност за периода.'

exec spDescColumn N'ContractReportIndicators', N'ApprovedCumulativeAmountMen'      , N'Одобрена отчетена стойност с натрупване (мъже).'
exec spDescColumn N'ContractReportIndicators', N'ApprovedCumulativeAmountWomen'    , N'Одобрена отчетена стойност с натрупване (жени).'
exec spDescColumn N'ContractReportIndicators', N'ApprovedCumulativeAmountTotal'    , N'Одобрена отчетена стойност с натрупване.'

exec spDescColumn N'ContractReportIndicators', N'ApprovedResidueAmountMen'         , N'Одобрен остатък/отклонение спрямо зададеното в договора (мъже).'
exec spDescColumn N'ContractReportIndicators', N'ApprovedResidueAmountWomen'       , N'Одобрен остатък/отклонение спрямо зададеното в договора (жени).'
exec spDescColumn N'ContractReportIndicators', N'ApprovedResidueAmountTotal'       , N'Одобрен остатък/отклонение спрямо зададеното в договора.'

exec spDescColumn N'ContractReportIndicators', N'CreateDate'                       , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportIndicators', N'ModifyDate'                       , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportIndicators', N'Version'                          , N'Версия.'
GO
