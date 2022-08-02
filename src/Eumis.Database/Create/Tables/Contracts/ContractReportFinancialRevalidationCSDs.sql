PRINT 'ContractReportFinancialRevalidationCSDs'
GO

CREATE TABLE [dbo].[ContractReportFinancialRevalidationCSDs] (
    [ContractReportFinancialRevalidationCSDId]       INT               NOT NULL IDENTITY,
    [ContractReportFinancialRevalidationId]          INT               NOT NULL,
    [ContractReportFinancialCSDBudgetItemId]         INT               NOT NULL,
    [ContractReportFinancialId]                      INT               NOT NULL,
    [ContractReportId]                               INT               NOT NULL,
    [ContractId]                                     INT               NOT NULL,
    [Gid]                                            UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [Sign]                                           INT               NULL,
    [Status]                                         INT               NOT NULL,
    [Notes]                                          NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                                INT               NULL,
    [CheckedDate]                                    DATETIME2         NULL,

    [RevalidatedEuAmount]                            MONEY             NULL,
    [RevalidatedBgAmount]                            MONEY             NULL,
    [RevalidatedBfpTotalAmount]                      MONEY             NULL,
    [RevalidatedSelfAmount]                          MONEY             NULL,
    [RevalidatedTotalAmount]                         MONEY             NULL,

    [CertReportId]                                   INT               NULL,

    [CertStatus]                                     INT               NULL,
    [CertCheckedByUserId]                            INT               NULL,
    [CertCheckedDate]                                DATETIME2         NULL,
    [UncertifiedRevalidatedEuAmount]                 MONEY             NULL,
    [UncertifiedRevalidatedBgAmount]                 MONEY             NULL,
    [UncertifiedRevalidatedBfpTotalAmount]           MONEY             NULL,
    [UncertifiedRevalidatedSelfAmount]               MONEY             NULL,
    [UncertifiedRevalidatedTotalAmount]              MONEY             NULL,

    [CertifiedRevalidatedEuAmount]                   MONEY             NULL,
    [CertifiedRevalidatedBgAmount]                   MONEY             NULL,
    [CertifiedRevalidatedBfpTotalAmount]             MONEY             NULL,
    [CertifiedRevalidatedSelfAmount]                 MONEY             NULL,
    [CertifiedRevalidatedTotalAmount]                MONEY             NULL,

    [CreateDate]                                     DATETIME2         NOT NULL,
    [ModifyDate]                                     DATETIME2         NOT NULL,
    [Version]                                        ROWVERSION        NOT NULL,
    
    CONSTRAINT [PK_ContractReportFinancialRevalidationCSDs]                                       PRIMARY KEY ([ContractReportFinancialRevalidationCSDId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidationCSDs_ContractReportFinancialRevalidations]  FOREIGN KEY ([ContractReportFinancialRevalidationId])    REFERENCES [dbo].[ContractReportFinancialRevalidations] ([ContractReportFinancialRevalidationId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidationCSDs_ContractReportFinancialCSDBudgetItems] FOREIGN KEY ([ContractReportFinancialCSDBudgetItemId])   REFERENCES [dbo].[ContractReportFinancialCSDBudgetItems] ([ContractReportFinancialCSDBudgetItemId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidationCSDs_ContractReportFinancials] FOREIGN KEY ([ContractReportFinancialId]) REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidationCSDs_ContractReports]          FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidationCSDs_Contracts]                FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidationCSDs_CheckedByUser]            FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidationCSDs_CertCheckedByUser]        FOREIGN KEY ([CertCheckedByUserId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportFinancialRevalidationCSDs_CertReports]              FOREIGN KEY ([CertReportId])              REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_ContractReportFinancialRevalidationCSDs_Status]                  CHECK       ([Status]         IN (1, 2)),
    CONSTRAINT [CHK_ContractReportFinancialRevalidationCSDs_CertStatus]              CHECK       ([CertStatus]     IN (1, 2)),
    CONSTRAINT [CHK_ContractReportFinancialRevalidationCSDs_Sign]                    CHECK       ([Sign]           IN (-1, 1))
);
GO

exec spDescTable  N'ContractReportFinancialRevalidationCSDs', N'Препотвърждаване на верифицирани суми на ниво РОД - РОД.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'ContractReportFinancialRevalidationId'      , N'Идентификатор на корекция на финансов отчет.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'ContractReportFinancialCSDBudgetItemId'     , N'Идентификатор на суми на разходооправдателни документи за конкретни бюджетен ред и дейност.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'ContractReportFinancialId'                  , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'ContractReportId'                           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'ContractId'                                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'Gid'                                        , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'Sign'                                       , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'Status'                                     , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'Notes'                                      , N'Бележки.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'CheckedByUserId'                            , N'Проверено от.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'CheckedDate'                                , N'Дата на проверка.'

exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'RevalidatedEuAmount'                        , N'Коригирана одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'RevalidatedBgAmount'                        , N'Коригирана одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'RevalidatedSelfAmount'                      , N'Коригирана одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'RevalidatedTotalAmount'                     , N'Коригирана одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'CertStatus'                                 , N'Статус за сертификация: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'UncertifiedRevalidatedEuAmount'             , N'Несертифицирана коригирана одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'UncertifiedRevalidatedBgAmount'             , N'Несертифицирана коригирана одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'UncertifiedRevalidatedSelfAmount'           , N'Несертифицирана коригирана одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'UncertifiedRevalidatedTotalAmount'          , N'Несертифицирана коригирана одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'CertifiedRevalidatedEuAmount'               , N'Сертифицирана коригирана одобрена стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'CertifiedRevalidatedBgAmount'               , N'Сертифицирана коригирана одобрена стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'CertifiedRevalidatedSelfAmount'             , N'Сертифицирана коригирана одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'CertifiedRevalidatedTotalAmount'            , N'Сертифицирана коригирана одобрена обща сума.'

exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'CreateDate'                                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'ModifyDate'                                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialRevalidationCSDs', N'Version'                                    , N'Версия.'
GO
