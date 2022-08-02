PRINT 'ContractReportCertAuthorityFinancialCorrectionCSDs'
GO

CREATE TABLE [dbo].[ContractReportCertAuthorityFinancialCorrectionCSDs] (
    [ContractReportCertAuthorityFinancialCorrectionCSDId]   INT                 NOT NULL IDENTITY,
    [ContractReportCertAuthorityFinancialCorrectionId]      INT                 NOT NULL,
    [ContractReportFinancialCSDBudgetItemId]                INT                 NOT NULL,
    [ContractReportFinancialId]                             INT                 NOT NULL,
    [ContractReportId]                                      INT                 NOT NULL,
    [ContractId]                                            INT                 NOT NULL,
    [Gid]                                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [Sign]                                                  INT                 NULL,
    [Status]                                                INT                 NOT NULL,
    [Notes]                                                 NVARCHAR(MAX)       NULL,
    [CheckedByUserId]                                       INT                 NULL,
    [CheckedDate]                                           DATETIME2           NULL,

    [CertifiedEuAmount]                                     MONEY               NULL,
    [CertifiedBgAmount]                                     MONEY               NULL,
    [CertifiedBfpTotalAmount]                               MONEY               NULL,
    [CertifiedSelfAmount]                                   MONEY               NULL,
    [CertifiedTotalAmount]                                  MONEY               NULL,

    [CreateDate]                                            DATETIME2           NOT NULL,
    [ModifyDate]                                            DATETIME2           NOT NULL,
    [Version]                                               ROWVERSION          NOT NULL,
    
    CONSTRAINT [PK_ContractReportCertAuthorityFinancialCorrectionCSDs]                                          PRIMARY KEY ([ContractReportCertAuthorityFinancialCorrectionCSDId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrectionCSDs_ContractReportFinancialCertCorrections]   FOREIGN KEY ([ContractReportCertAuthorityFinancialCorrectionId])    REFERENCES [dbo].[ContractReportCertAuthorityFinancialCorrections] ([ContractReportCertAuthorityFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrectionCSDs_ContractReportFinancialCSDBudgetItems]    FOREIGN KEY ([ContractReportFinancialCSDBudgetItemId])              REFERENCES [dbo].[ContractReportFinancialCSDBudgetItems] ([ContractReportFinancialCSDBudgetItemId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrectionCSDs_ContractReportFinancials]                 FOREIGN KEY ([ContractReportFinancialId])                           REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrectionCSDs_ContractReports]                          FOREIGN KEY ([ContractReportId])                                    REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrectionCSDs_Contracts]                                FOREIGN KEY ([ContractId])                                          REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrectionCSDs_CheckedByUser]                            FOREIGN KEY ([CheckedByUserId])                                     REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportCertAuthorityFinancialCorrectionCSDs_Status]                                  CHECK       ([Status]         IN (1, 2)),
    CONSTRAINT [CHK_ContractReportCertAuthorityFinancialCorrectionCSDs_Sign]                                    CHECK       ([Sign]           IN (-1, 1))
);
GO

exec spDescTable  N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'Корекции от СО на сертифицриани суми на ниво РОД - РОД.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'ContractReportCertAuthorityFinancialCorrectionCSDId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'ContractReportCertAuthorityFinancialCorrectionId'       , N'Идентификатор на корекция на финансов отчет.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'ContractReportFinancialCSDBudgetItemId'                 , N'Идентификатор на суми на разходооправдателни документи за конкретни бюджетен ред и дейност.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'ContractReportFinancialId'                              , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'ContractReportId'                                       , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'ContractId'                                             , N'Идентификатор на договор'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'Gid'                                                    , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'Sign'                                                   , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'Status'                                                 , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'Notes'                                                  , N'Бележки.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'CheckedByUserId'                                        , N'Проверено от.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'CheckedDate'                                            , N'Дата на проверка.'

exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'CertifiedEuAmount'                                      , N'Коригирана сертифицирана стойност финансиране ЕС.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'CertifiedBgAmount'                                      , N'Коригирана сертифицирана стойност национално финансиране.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'CertifiedBfpTotalAmount'                                , N'Коригирана сертифицирана стойност общо БФП.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'CertifiedSelfAmount'                                    , N'Коригирана сертифицирана собствено съфинансиране.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'CertifiedTotalAmount'                                   , N'Коригирана сертифицирана обща сума.'

exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'CreateDate'                                             , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'ModifyDate'                                             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrectionCSDs' , N'Version'                                                , N'Версия.'
GO
