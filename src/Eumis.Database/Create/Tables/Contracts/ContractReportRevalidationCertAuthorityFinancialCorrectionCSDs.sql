PRINT 'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs'
GO

CREATE TABLE [dbo].[ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs] (
    [ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId] INT                 NOT NULL IDENTITY,
    [ContractReportRevalidationCertAuthorityFinancialCorrectionId]    INT                 NOT NULL,
    [ContractReportFinancialCSDBudgetItemId]                          INT                 NOT NULL,
    [ContractReportFinancialId]                                       INT                 NOT NULL,
    [ContractReportId]                                                INT                 NOT NULL,
    [ContractId]                                                      INT                 NOT NULL,
    [Gid]                                                             UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [Sign]                                                            INT                 NULL,
    [Status]                                                          INT                 NOT NULL,
    [Notes]                                                           NVARCHAR(MAX)       NULL,
    [CheckedByUserId]                                                 INT                 NULL,
    [CheckedDate]                                                     DATETIME2           NULL,

    [RevalidatedEuAmount]                                             MONEY               NULL,
    [RevalidatedBgAmount]                                             MONEY               NULL,
    [RevalidatedBfpTotalAmount]                                       MONEY               NULL,
    [RevalidatedSelfAmount]                                           MONEY               NULL,
    [RevalidatedTotalAmount]                                          MONEY               NULL,

    [CreateDate]                                                      DATETIME2           NOT NULL,
    [ModifyDate]                                                      DATETIME2           NOT NULL,
    [Version]                                                         ROWVERSION          NOT NULL,
    
    CONSTRAINT [PK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs]                                                              PRIMARY KEY ([ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs_ContractReportRevalidationCertAuthorityFinancialCorrections]  FOREIGN KEY ([ContractReportRevalidationCertAuthorityFinancialCorrectionId])    REFERENCES [dbo].[ContractReportRevalidationCertAuthorityFinancialCorrections] ([ContractReportRevalidationCertAuthorityFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs_ContractReportFinancialCSDBudgetItems]                        FOREIGN KEY ([ContractReportFinancialCSDBudgetItemId])                          REFERENCES [dbo].[ContractReportFinancialCSDBudgetItems] ([ContractReportFinancialCSDBudgetItemId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs_ContractReportFinancials]                                     FOREIGN KEY ([ContractReportFinancialId])                                       REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs_ContractReports]                                              FOREIGN KEY ([ContractReportId])                                                REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs_Contracts]                                                    FOREIGN KEY ([ContractId])                                                      REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs_CheckedByUser]                                                FOREIGN KEY ([CheckedByUserId])                                                 REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs_Status]                                                      CHECK       ([Status]         IN (1, 2)),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs_Sign]                                                        CHECK       ([Sign]           IN (-1, 1))
);
GO

exec spDescTable  N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'Корекции от СО на препотвърдени сертифицриани суми на ниво РОД - РОД.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'ContractReportRevalidationCertAuthorityFinancialCorrectionId'       , N'Идентификатор на корекция на финансов отчет.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'ContractReportFinancialCSDBudgetItemId'                             , N'Идентификатор на суми на разходооправдателни документи за конкретни бюджетен ред и дейност.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'ContractReportFinancialId'                                          , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'ContractReportId'                                                   , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'ContractId'                                                         , N'Идентификатор на договор'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'Gid'                                                                , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'Sign'                                                               , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'Status'                                                             , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'Notes'                                                              , N'Бележки.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'CheckedByUserId'                                                    , N'Проверено от.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'CheckedDate'                                                        , N'Дата на проверка.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'RevalidatedEuAmount'                                                , N'Коригирана препотвърдена сертифицирана стойност финансиране ЕС.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'RevalidatedBgAmount'                                                , N'Коригирана препотвърдена сертифицирана стойност национално финансиране.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'RevalidatedBfpTotalAmount'                                          , N'Коригирана препотвърдена сертифицирана стойност общо БФП.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'RevalidatedSelfAmount'                                              , N'Коригирана препотвърдена сертифицирана собствено съфинансиране.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'RevalidatedTotalAmount'                                             , N'Коригирана препотвърдена сертифицирана обща сума.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'CreateDate'                                                         , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'ModifyDate'                                                         , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs' , N'Version'                                                            , N'Версия.'
GO
