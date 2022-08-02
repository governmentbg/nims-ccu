PRINT 'ContractReportFinancialCorrectionCSDs'
GO

CREATE TABLE [dbo].[ContractReportFinancialCorrectionCSDs] (
    [ContractReportFinancialCorrectionCSDId]        INT               NOT NULL IDENTITY,
    [ContractReportFinancialCorrectionId]           INT               NOT NULL,
    [ContractReportFinancialCSDBudgetItemId]        INT               NOT NULL,
    [ContractReportFinancialId]                     INT               NOT NULL,
    [ContractReportId]                              INT               NOT NULL,
    [ContractId]                                    INT               NOT NULL,
    [Gid]                                           UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [Sign]                                          INT               NULL,
    [Status]                                        INT               NOT NULL,
    [Notes]                                         NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                               INT               NULL,
    [CheckedDate]                                   DATETIME2         NULL,

    [CorrectedUnapprovedEuAmount]                   MONEY             NULL,
    [CorrectedUnapprovedBgAmount]                   MONEY             NULL,
    [CorrectedUnapprovedBfpTotalAmount]             MONEY             NULL,
    [CorrectedUnapprovedSelfAmount]                 MONEY             NULL,
    [CorrectedUnapprovedTotalAmount]                MONEY             NULL,

    [CorrectedUnapprovedByCorrectionEuAmount]       MONEY             NULL,
    [CorrectedUnapprovedByCorrectionBgAmount]       MONEY             NULL,
    [CorrectedUnapprovedByCorrectionBfpTotalAmount] MONEY             NULL,
    [CorrectedUnapprovedByCorrectionSelfAmount]     MONEY             NULL,
    [CorrectedUnapprovedByCorrectionTotalAmount]    MONEY             NULL,

    [CorrectionType]                                INT               NULL,
    [FinancialCorrectionId]                         INT               NULL,
    [IrregularityId]                                INT               NULL,

    [CorrectedApprovedEuAmount]                     MONEY             NULL,
    [CorrectedApprovedBgAmount]                     MONEY             NULL,
    [CorrectedApprovedBfpTotalAmount]               MONEY             NULL,
    [CorrectedApprovedSelfAmount]                   MONEY             NULL,
    [CorrectedApprovedTotalAmount]                  MONEY             NULL,
    
    [CertReportId]                                  INT               NULL,

    [CertStatus]                                    INT               NULL,
    [CertCheckedByUserId]                           INT               NULL,
    [CertCheckedDate]                               DATETIME2         NULL,
    [UncertifiedCorrectedApprovedEuAmount]          MONEY             NULL,
    [UncertifiedCorrectedApprovedBgAmount]          MONEY             NULL,
    [UncertifiedCorrectedApprovedBfpTotalAmount]    MONEY             NULL,
    [UncertifiedCorrectedApprovedSelfAmount]        MONEY             NULL,
    [UncertifiedCorrectedApprovedTotalAmount]       MONEY             NULL,

    [CertifiedCorrectedApprovedEuAmount]            MONEY             NULL,
    [CertifiedCorrectedApprovedBgAmount]            MONEY             NULL,
    [CertifiedCorrectedApprovedBfpTotalAmount]      MONEY             NULL,
    [CertifiedCorrectedApprovedSelfAmount]          MONEY             NULL,
    [CertifiedCorrectedApprovedTotalAmount]         MONEY             NULL,

    [CreateDate]                                    DATETIME2         NOT NULL,
    [ModifyDate]                                    DATETIME2         NOT NULL,
    [Version]                                       ROWVERSION        NOT NULL,
    
    CONSTRAINT [PK_ContractReportFinancialCorrectionCSDs]                                       PRIMARY KEY ([ContractReportFinancialCorrectionCSDId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_ContractReportFinancialCorrections]    FOREIGN KEY ([ContractReportFinancialCorrectionId])    REFERENCES [dbo].[ContractReportFinancialCorrections] ([ContractReportFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_ContractReportFinancialCSDBudgetItems] FOREIGN KEY ([ContractReportFinancialCSDBudgetItemId]) REFERENCES [dbo].[ContractReportFinancialCSDBudgetItems] ([ContractReportFinancialCSDBudgetItemId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_ContractReportFinancials]  FOREIGN KEY ([ContractReportFinancialId]) REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_ContractReports]           FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_Contracts]                 FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_FinancialCorrections]      FOREIGN KEY ([FinancialCorrectionId])     REFERENCES [dbo].[FinancialCorrections] ([FinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_Irregularities]            FOREIGN KEY ([IrregularityId])            REFERENCES [dbo].[Irregularities] ([IrregularityId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_CheckedByUser]             FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_CertCheckedByUser]         FOREIGN KEY ([CertCheckedByUserId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportFinancialCorrectionCSDs_CertReports]               FOREIGN KEY ([CertReportId])              REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_ContractReportFinancialCorrectionCSDs_CorrectionType]           CHECK       ([CorrectionType] IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportFinancialCorrectionCSDs_Status]                   CHECK       ([Status]         IN (1, 2)),
    CONSTRAINT [CHK_ContractReportFinancialCorrectionCSDs_CertStatus]               CHECK       ([CertStatus]     IN (1, 2)),
    CONSTRAINT [CHK_ContractReportFinancialCorrectionCSDs_Sign]                     CHECK       ([Sign]           IN (-1, 1))
);
GO

exec spDescTable  N'ContractReportFinancialCorrectionCSDs', N'Корекции на верифицирани суми на ниво РОД - РОД.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'ContractReportFinancialCorrectionId'        , N'Идентификатор на корекция на финансов отчет.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'ContractReportFinancialCSDBudgetItemId'     , N'Идентификатор на суми на разходооправдателни документи за конкретни бюджетен ред и дейност.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'ContractReportFinancialId'                  , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'ContractReportId'                           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'ContractId'                                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'Gid'                                        , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'Sign'                                       , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'Status'                                     , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'Notes'                                      , N'Бележки.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CheckedByUserId'                            , N'Проверено от.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CheckedDate'                                , N'Дата на проверка.'

exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedUnapprovedEuAmount'                , N'Коригирана неверифицирана стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedUnapprovedBgAmount'                , N'Коригирана неверифицирана стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedUnapprovedSelfAmount'              , N'Коригирана неверифицирано собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedUnapprovedTotalAmount'             , N'Коригирана неверифицирана обща сума.'

exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedUnapprovedByCorrectionEuAmount'    , N'Коригирана неверифицирана стойност финансиране ЕС по наложена финансова корекция.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedUnapprovedByCorrectionBgAmount'    , N'Коригирана неверифицирана стойност национално финансиране по наложена финансова корекция.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedUnapprovedByCorrectionSelfAmount'  , N'Коригирана неверифицирано собствено съфинансиране по наложена финансова корекция.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedUnapprovedByCorrectionTotalAmount' , N'Коригирана неверифицирана обща сума по наложена финансова корекция.'

exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectionType'                             , N'Тип на корекцията: 1 - Финансова корекция, 2 - Нередност, 3 - Финансова корекция и нередност.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'FinancialCorrectionId'                      , N'Идентификатор на финансова корекция.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'IrregularityId'                             , N'Идентификатор на нередност.'

exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedApprovedEuAmount'                  , N'Коригирана одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedApprovedBgAmount'                  , N'Коригирана одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedApprovedSelfAmount'                , N'Коригирана одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CorrectedApprovedTotalAmount'               , N'Коригирана одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CertStatus'                                 , N'Статус за сертификация: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'UncertifiedCorrectedApprovedEuAmount'       , N'Несертифицирана коригирана одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'UncertifiedCorrectedApprovedBgAmount'       , N'Несертифицирана коригирана одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'UncertifiedCorrectedApprovedSelfAmount'     , N'Несертифицирана коригирана одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'UncertifiedCorrectedApprovedTotalAmount'    , N'Несертифицирана коригирана одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CertifiedCorrectedApprovedEuAmount'         , N'Сертифицирана коригирана одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CertifiedCorrectedApprovedBgAmount'         , N'Сертифицирана коригирана одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CertifiedCorrectedApprovedSelfAmount'       , N'Сертифицирана коригирана одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CertifiedCorrectedApprovedTotalAmount'      , N'Сертифицирана коригирана одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'CreateDate'                                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'ModifyDate'                                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialCorrectionCSDs', N'Version'                                    , N'Версия.'
GO
