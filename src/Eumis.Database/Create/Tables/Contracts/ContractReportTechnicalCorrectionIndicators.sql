PRINT 'ContractReportTechnicalCorrectionIndicators'
GO

CREATE TABLE [dbo].[ContractReportTechnicalCorrectionIndicators] (
    [ContractReportTechnicalCorrectionIndicatorId]  INT               NOT NULL IDENTITY,
    [ContractReportTechnicalCorrectionId]           INT               NOT NULL,
    [ContractReportIndicatorId]                     INT               NOT NULL,
    [ContractReportTechnicalId]                     INT               NOT NULL,
    [ContractReportId]                              INT               NOT NULL,
    [ContractId]                                    INT               NOT NULL,
    [Gid]                                           UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [Status]                                        INT               NOT NULL,
    [Notes]                                         NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                               INT               NULL,
    [CheckedDate]                                   DATETIME2         NULL,

    [CorrectedApprovedPeriodAmountMen]              DECIMAL(15,3)     NULL,
    [CorrectedApprovedPeriodAmountWomen]            DECIMAL(15,3)     NULL,
    [CorrectedApprovedPeriodAmountTotal]            DECIMAL(15,3)     NULL,

    [CorrectedApprovedCumulativeAmountMen]          DECIMAL(15,3)     NULL,
    [CorrectedApprovedCumulativeAmountWomen]        DECIMAL(15,3)     NULL,
    [CorrectedApprovedCumulativeAmountTotal]        DECIMAL(15,3)     NULL,

    [CorrectedApprovedResidueAmountMen]             DECIMAL(15,3)     NULL,
    [CorrectedApprovedResidueAmountWomen]           DECIMAL(15,3)     NULL,
    [CorrectedApprovedResidueAmountTotal]           DECIMAL(15,3)     NULL,

    [LastReportCorrectedCumulativeAmountMen]        DECIMAL(15,3)     NULL,
    [LastReportCorrectedCumulativeAmountWomen]      DECIMAL(15,3)     NULL,
    [LastReportCorrectedCumulativeAmountTotal]      DECIMAL(15,3)     NOT NULL,

    [CreateDate]                                    DATETIME2         NOT NULL,
    [ModifyDate]                                    DATETIME2         NOT NULL,
    [Version]                                       ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportTechnicalCorrectionIndicators]                                       PRIMARY KEY ([ContractReportTechnicalCorrectionIndicatorId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrectionIndicators_ContractReportTechnicalCorrections]    FOREIGN KEY ([ContractReportTechnicalCorrectionId]) REFERENCES [dbo].[ContractReportTechnicalCorrections] ([ContractReportTechnicalCorrectionId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrectionIndicators_ContractReportIndicators]  FOREIGN KEY ([ContractReportIndicatorId]) REFERENCES [dbo].[ContractReportIndicators] ([ContractReportIndicatorId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrectionIndicators_ContractReportTechnicals]  FOREIGN KEY ([ContractReportTechnicalId]) REFERENCES [dbo].[ContractReportTechnicals] ([ContractReportTechnicalId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrectionIndicators_ContractReports]           FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrectionIndicators_Contracts]                 FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportTechnicalCorrectionIndicators_CheckedByUser]             FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportTechnicalCorrectionIndicators_Status]                   CHECK       ([Status]         IN (1, 2))
);
GO

exec spDescTable  N'ContractReportTechnicalCorrectionIndicators', N'Корекции на верифицирани индикатори - индикатори.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'ContractReportTechnicalCorrectionIndicatorId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'ContractReportTechnicalCorrectionId'          , N'Идентификатор на корекция на технически отчет.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'ContractReportIndicatorId'                    , N'Идентификатор на индикатор към технически отчет.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'ContractReportTechnicalId'                    , N'Идентификатор на технически отчет към пакет отчетни документи.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'ContractReportId'                             , N'Идентификатор на пакет отчетни документи.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'ContractId'                                   , N'Идентификатор на договор.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'Gid'                                          , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'Status'                                       , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'Notes'                                        , N'Бележки.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CheckedByUserId'                              , N'Проверено от.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CheckedDate'                                  , N'Дата на проверка.'

exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedPeriodAmountMen'             , N'Коригирана одобрена отчетена стойност за периода (мъже).'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedPeriodAmountWomen'           , N'Коригирана одобрена отчетена стойност за периода (жени).'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedPeriodAmountTotal'           , N'Коригирана одобрена отчетена стойност за периода.'

exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedCumulativeAmountMen'         , N'Коригирана одобрена отчетена стойност с натрупване (мъже).'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedCumulativeAmountWomen'       , N'Коригирана одобрена отчетена стойност с натрупване (жени).'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedCumulativeAmountTotal'       , N'Коригирана одобрена отчетена стойност с натрупване.'

exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedResidueAmountMen'            , N'Коригиран одобрен остатък/отклонение спрямо зададеното в договора (мъже).'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedResidueAmountWomen'          , N'Коригиран одобрен остатък/отклонение спрямо зададеното в договора (жени).'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CorrectedApprovedResidueAmountTotal'          , N'Коригиран одобрен остатък/отклонение спрямо зададеното в договора.'

exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'LastReportCorrectedCumulativeAmountMen'       , N'Коригирана одобрена стойност от предходен отчет (мъже).'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'LastReportCorrectedCumulativeAmountWomen'     , N'Коригирана одобрена стойност от предходен отчет (жени).'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'LastReportCorrectedCumulativeAmountTotal'     , N'Коригирана одобрена стойност от предходен отчет.'

exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'CreateDate'                                   , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'ModifyDate'                                   , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportTechnicalCorrectionIndicators', N'Version'                                      , N'Версия.'
GO
