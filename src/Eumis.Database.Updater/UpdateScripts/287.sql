PRINT 'ContractReportRevalidationCertAuthorityCorrections'
GO

CREATE TABLE [dbo].[ContractReportRevalidationCertAuthorityCorrections] (
    [ContractReportRevalidationCertAuthorityCorrectionId]   INT                 NOT NULL IDENTITY,
    [ProgrammeId]                                           INT                 NOT NULL,
    [ProgrammePriorityId]                                   INT                 NULL,
    [ProcedureId]                                           INT                 NULL,
    [ContractId]                                            INT                 NULL,
    [ContractReportPaymentId]                               INT                 NULL,
    [Type]                                                  INT                 NOT NULL,
    [Status]                                                INT                 NOT NULL,
    [RegNumber]                                             NVARCHAR(200)       NULL,
    [Sign]                                                  INT                 NOT NULL,
    [Date]                                                  DATETIME2           NOT NULL,
    [Description]                                           NVARCHAR(MAX)       NULL,
    [Reason]                                                NVARCHAR(MAX)       NULL,
    [CheckedByUserId]                                       INT                 NULL,
    [CheckedDate]                                           DATETIME2           NULL,

    [FinanceSource]                                         INT                 NOT NULL,
    [CertifiedRevalidatedEuAmount]                          MONEY               NULL,
    [CertifiedRevalidatedBgAmount]                          MONEY               NULL,
    [CertifiedRevalidatedBfpTotalAmount]                    MONEY               NULL,
    [CertifiedRevalidatedCrossAmount]                       MONEY               NULL,
    [CertifiedRevalidatedSelfAmount]                        MONEY               NULL,
    [CertifiedRevalidatedTotalAmount]                       MONEY               NULL,

    [IsActivated]                                           BIT                 NOT NULL,
    [DeleteNote]                                            NVARCHAR(MAX)       NULL,
    [CreateDate]                                            DATETIME2           NOT NULL,
    [ModifyDate]                                            DATETIME2           NOT NULL,
    [Version]                                               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ContractReportRevalidationCertAuthorityCorrections]                          PRIMARY KEY ([ContractReportRevalidationCertAuthorityCorrectionId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrections_Programmes]               FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrections_ProgrammePriorities]      FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrections_Procedures]               FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrections_Contracts]                FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]  ([ContractId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrections_ContractReportPayments]   FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_ClassPayment]            CHECK       ([Type] != 1                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL AND [ContractReportPaymentId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_ClassContract]           CHECK       ([Type] != 2                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_ClassPrgPriority]        CHECK       ([Type] != 4                OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_ClassProcedure]          CHECK       ([Type] != 5                OR [ProcedureId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_Type]                    CHECK       ([Type]                     IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_Status]                  CHECK       ([Status]                   IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_Sign]                    CHECK       ([Sign]                     IN (-1, 1)),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_FinanceSource]           CHECK       ([FinanceSource]            IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractReportRevalidationCertAuthorityCorrections' , N'Корекции от СО на препотвърдени сертифицриани суми на други нива.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'ContractReportRevalidationCertAuthorityCorrectionId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'ProgrammeId'                                         , N'Идентификатор на програма.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'ProgrammePriorityId'                                 , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'ProcedureId'                                         , N'Идентификатор на процедура.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'ContractId'                                          , N'Идентификатор на договор.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'ContractReportPaymentId'                             , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'Type'                                                , N'Вид: 1 - Верифицирани на ниво искане за плащане, 2 - Верифицирани на ниво договор, 3 - Верифицирани на ниво програма; 4 - Верифицирани на ниво приоритетна ос; 5 - Верифицирани на ниво процедура.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'Status'                                              , N'Статус: 1 - Чернова; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'RegNumber'                                           , N'Регистрационен номер.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'Sign'                                                , N'Знак: -1 - -, 1 - +.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'Date'                                                , N'Дата.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'Description'                                         , N'Описание.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'Reason'                                              , N'Основание.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CheckedByUserId'                                     , N'Проверено от.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CheckedDate'                                         , N'Дата на проверка.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'FinanceSource'                                       , N'Фонд: 1 - ЕСФ; 2 - ЕФРР; 3 - КФ; 4 - ИМЗ; 5 - ФЕПНЛ; 6 - ЕФМДР; 7 - ЕЗФРСР; 8 - ФВС; 9 - ФУМИ; 10 - Други; 11 - ФМ на ЕИП; 12 - НФМ'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CertifiedRevalidatedEuAmount'                        , N'Коригирана препотвърдена сертифицирана стойност финансиране от ЕС.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CertifiedRevalidatedBgAmount'                        , N'Коригирана препотвърдена сертифицирана стойност финансиране от НФ.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CertifiedRevalidatedBfpTotalAmount'                  , N'Коригирана препотвърдена сертифицирана стойност общо БФП.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CertifiedRevalidatedCrossAmount'                     , N'Коригирана препотвърдена сертифицирана стойност кръстосано съфинансиране'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CertifiedRevalidatedSelfAmount'                      , N'Коригирана препотвърдена сертифицирана стойност собствено съфинансиране.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CertifiedRevalidatedTotalAmount'                     , N'Коригирана препотвърдена сертифицирана стойност общо финансиране.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'IsActivated'                                         , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'DeleteNote'                                          , N'Причина за изтриване.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'CreateDate'                                          , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'ModifyDate'                                          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrections' , N'Version'                                             , N'Версия.'
GO

PRINT 'ContractReportRevalidationCertAuthorityCorrectionDocuments'
GO

CREATE TABLE [dbo].[ContractReportRevalidationCertAuthorityCorrectionDocuments] (
    [ContractReportRevalidationCertAuthorityCorrectionDocumentId]   INT                 NOT NULL IDENTITY,
    [ContractReportRevalidationCertAuthorityCorrectionId]           INT                 NOT NULL,
    [Description]                                                   NVARCHAR(MAX)       NOT NULL,
    [FileName]                                                      NVARCHAR(200)       NOT NULL,
    [FileKey]                                                       UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [PK_ContractReportRevalidationCertAuthorityCorrectionDocuments]                                                      PRIMARY KEY ([ContractReportRevalidationCertAuthorityCorrectionDocumentId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrectionDocuments_ContractReportRevalidationCertAuthorityCorrections]   FOREIGN KEY ([ContractReportRevalidationCertAuthorityCorrectionId]) REFERENCES [dbo].[ContractReportRevalidationCertAuthorityCorrections] ([ContractReportRevalidationCertAuthorityCorrectionId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityCorrectionDocuments_Blobs]                                                FOREIGN KEY ([FileKey])                                             REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'Документи към корекции от СО на препотвърдени суми.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'ContractReportRevalidationCertAuthorityCorrectionDocumentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'ContractReportRevalidationCertAuthorityCorrectionId'        , N'Идентификатор на изравнителен документ.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'Description'                                                , N'Описание.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'FileName'                                                   , N'Наименование.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityCorrectionDocuments', N'FileKey'                                                    , N'Идентификатор на файл.'
GO

PRINT 'ContractReportRevalidationCertAuthorityFinancialCorrections'
GO

CREATE TABLE [dbo].[ContractReportRevalidationCertAuthorityFinancialCorrections] (
    [ContractReportRevalidationCertAuthorityFinancialCorrectionId]  INT                 NOT NULL IDENTITY,
    [ContractReportFinancialId]                                     INT                 NOT NULL,
    [ContractReportId]                                              INT                 NOT NULL,
    [ContractId]                                                    INT                 NOT NULL,
    [Gid]                                                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [OrderNum]                                                      INT                 NOT NULL,
    [Status]                                                        INT                 NOT NULL,
    [CertCorrectionDate]                                            DATETIME2           NULL,
    [BlobKey]                                                       UNIQUEIDENTIFIER    NULL,
    [Notes]                                                         NVARCHAR(MAX)       NULL,
    [CheckedByUserId]                                               INT                 NULL,
    [CheckedDate]                                                   DATETIME2           NULL,

    [CreateDate]                                                    DATETIME2           NOT NULL,
    [ModifyDate]                                                    DATETIME2           NOT NULL,
    [Version]                                                       ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ContractReportRevalidationCertAuthorityFinancialCorrections]                             PRIMARY KEY ([ContractReportRevalidationCertAuthorityFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_ContractReportFinancials]    FOREIGN KEY ([ContractReportFinancialId])   REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_ContractReports]             FOREIGN KEY ([ContractReportId])            REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_Contracts]                   FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_Blobs]                       FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportRevalidationCertAuthorityFinancialCorrections_CheckedByUser]               FOREIGN KEY ([CheckedByUserId])             REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityFinancialCorrections_Status]                     CHECK       ([Status]         IN (1, 2))
);
GO

exec spDescTable  N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Корекции от СО на препотвърдени сертифицриани суми на ниво РОД - ПОД.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ContractReportRevalidationCertAuthorityFinancialCorrectionId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ContractReportFinancialId'                                   , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ContractReportId'                                            , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ContractId'                                                  , N'Идентификатор на договор'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Gid'                                                         , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'OrderNum'                                                    , N'Пореден номер.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Status'                                                      , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'CertCorrectionDate'                                          , N'Дата на препотвърждаване.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'BlobKey'                                                     , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Notes'                                                       , N'Бележки.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'CheckedByUserId'                                             , N'Проверено от.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'CheckedDate'                                                 , N'Дата на проверка.'

exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'CreateDate'                                                  , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'ModifyDate'                                                  , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportRevalidationCertAuthorityFinancialCorrections', N'Version'                                                     , N'Версия.'
GO

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

PRINT 'AnnualAccountReportCertRevalidationCorrections'
GO

CREATE TABLE [dbo].[AnnualAccountReportCertRevalidationCorrections] (
    [AnnualAccountReportId]                                 INT               NOT NULL,
    [ContractReportRevalidationCertAuthorityCorrectionId]   INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertRevalidationCorrections]                                                      PRIMARY KEY ([AnnualAccountReportId], [ContractReportRevalidationCertAuthorityCorrectionId]),
    CONSTRAINT [FK_AnnualAccountReportCertRevalidationCorrections_AnnualAccountReports]                                 FOREIGN KEY ([AnnualAccountReportId])                               REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertRevalidationCorrections_ContractReportRevalidationCertAuthorityCorrections]   FOREIGN KEY ([ContractReportRevalidationCertAuthorityCorrectionId]) REFERENCES [dbo].[ContractReportRevalidationCertAuthorityCorrections] ([ContractReportRevalidationCertAuthorityCorrectionId])
);
GO

exec spDescTable  N'AnnualAccountReportCertRevalidationCorrections', N'Корекции СС на препотвърдени суми на други нива към годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertRevalidationCorrections', N'AnnualAccountReportId'                               , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertRevalidationCorrections', N'ContractReportRevalidationCertAuthorityCorrectionId' , N'Идентификатор на корекция от СС на препотвърдени суми на други нива.'
GO

PRINT 'AnnualAccountReportCertRevalidationFinancialCorrectionCSDs'
GO

CREATE TABLE [dbo].[AnnualAccountReportCertRevalidationFinancialCorrectionCSDs] (
    [AnnualAccountReportId]                                             INT               NOT NULL,
    [ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId]   INT               NOT NULL

    CONSTRAINT [PK_AnnualAccountReportCertRevalidationFinancialCorrectionCSDs]                                                                  PRIMARY KEY ([AnnualAccountReportId], [ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId]),
    CONSTRAINT [FK_AnnualAccountReportCertRevalidationFinancialCorrectionCSDs_AnnualAccountReports]                                             FOREIGN KEY ([AnnualAccountReportId])                                           REFERENCES [dbo].[AnnualAccountReports] ([AnnualAccountReportId]),
    CONSTRAINT [FK_AnnualAccountReportCertRevalidationFinancialCorrectionCSDs_ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs]   FOREIGN KEY ([ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId]) REFERENCES [dbo].[ContractReportRevalidationCertAuthorityFinancialCorrectionCSDs] ([ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId])
);
GO

exec spDescTable  N'AnnualAccountReportCertRevalidationFinancialCorrectionCSDs', N'Корекции СС на препотвърдени суми на ниво РОД към годишни счетоводни отчети.'
exec spDescColumn N'AnnualAccountReportCertRevalidationFinancialCorrectionCSDs', N'AnnualAccountReportId'                                           , N'Идентификатор на годишен счетоводен отчет.'
exec spDescColumn N'AnnualAccountReportCertRevalidationFinancialCorrectionCSDs', N'ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId' , N'Идентификатор на корекция на сертифицирани препотвърдени суми на ниво РОД.'
GO
