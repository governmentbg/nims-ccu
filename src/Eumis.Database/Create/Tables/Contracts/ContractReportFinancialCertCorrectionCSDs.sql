PRINT 'ContractReportFinancialCertCorrectionCSDs'
GO

CREATE TABLE [dbo].[ContractReportFinancialCertCorrectionCSDs] (
    [ContractReportFinancialCertCorrectionCSDId]     INT               NOT NULL IDENTITY,
    [ContractReportFinancialCertCorrectionId]        INT               NOT NULL,
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

    [CertifiedEuAmount]                              MONEY             NULL,
    [CertifiedBgAmount]                              MONEY             NULL,
    [CertifiedBfpTotalAmount]                        MONEY             NULL,
    [CertifiedSelfAmount]                            MONEY             NULL,
    [CertifiedTotalAmount]                           MONEY             NULL,

    [CertReportId]                                   INT               NULL,

    [CreateDate]                                     DATETIME2         NOT NULL,
    [ModifyDate]                                     DATETIME2         NOT NULL,
    [Version]                                        ROWVERSION        NOT NULL,
    
    CONSTRAINT [PK_ContractReportFinancialCertCorrectionCSDs]                                        PRIMARY KEY ([ContractReportFinancialCertCorrectionCSDId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrectionCSDs_ContractReportFinancialCertCorrections] FOREIGN KEY ([ContractReportFinancialCertCorrectionId])   REFERENCES [dbo].[ContractReportFinancialCertCorrections] ([ContractReportFinancialCertCorrectionId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrectionCSDs_ContractReportFinancialCSDBudgetItems]  FOREIGN KEY ([ContractReportFinancialCSDBudgetItemId])    REFERENCES [dbo].[ContractReportFinancialCSDBudgetItems] ([ContractReportFinancialCSDBudgetItemId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrectionCSDs_ContractReportFinancials]  FOREIGN KEY ([ContractReportFinancialId]) REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrectionCSDs_ContractReports]           FOREIGN KEY ([ContractReportId])          REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrectionCSDs_Contracts]                 FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrectionCSDs_CheckedByUser]             FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportFinancialCertCorrectionCSDs_CertReports]               FOREIGN KEY ([CertReportId])              REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_ContractReportFinancialCertCorrectionCSDs_Status]                   CHECK       ([Status]         IN (1, 2)),
    CONSTRAINT [CHK_ContractReportFinancialCertCorrectionCSDs_Sign]                     CHECK       ([Sign]           IN (-1, 1))
);
GO

exec spDescTable  N'ContractReportFinancialCertCorrectionCSDs', N'Корекции на сертифицриани суми на ниво РОД - РОД.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'ContractReportFinancialCertCorrectionId'    , N'Идентификатор на корекция на финансов отчет.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'ContractReportFinancialCSDBudgetItemId'     , N'Идентификатор на суми на разходооправдателни документи за конкретни бюджетен ред и дейност.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'ContractReportFinancialId'                  , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'ContractReportId'                           , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'ContractId'                                 , N'Идентификатор на договор'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'Gid'                                        , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'Sign'                                       , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'Status'                                     , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'Notes'                                      , N'Бележки.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'CheckedByUserId'                            , N'Проверено от.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'CheckedDate'                                , N'Дата на проверка.'

exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'CertifiedEuAmount'                          , N'Коригирана сертифицирана стойност финансиране ЕС.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'CertifiedBgAmount'                          , N'Коригирана сертифицирана стойност национално финансиране.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'CertifiedSelfAmount'                        , N'Коригирана сертифицирана собствено съфинансиране.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'CertifiedTotalAmount'                       , N'Коригирана сертифицирана обща сума.'

exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'CreateDate'                                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'ModifyDate'                                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportFinancialCertCorrectionCSDs', N'Version'                                    , N'Версия.'
GO
