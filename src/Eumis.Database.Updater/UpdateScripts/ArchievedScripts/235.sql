GO

PRINT 'ContractReportCertAuthorityFinancialCorrections'
GO

CREATE TABLE [dbo].[ContractReportCertAuthorityFinancialCorrections] (
    [ContractReportCertAuthorityFinancialCorrectionId]      INT                 NOT NULL IDENTITY,
    [ContractReportFinancialId]                             INT                 NOT NULL,
    [ContractReportId]                                      INT                 NOT NULL,
    [ContractId]                                            INT                 NOT NULL,
    [Gid]                                                   UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [OrderNum]                                              INT                 NOT NULL,
    [Status]                                                INT                 NOT NULL,
    [CertCorrectionDate]                                    DATETIME2           NULL,
    [BlobKey]                                               UNIQUEIDENTIFIER    NULL,
    [Notes]                                                 NVARCHAR(MAX)       NULL,
    [CheckedByUserId]                                       INT                 NULL,
    [CheckedDate]                                           DATETIME2           NULL,

    [CreateDate]                                            DATETIME2           NOT NULL,
    [ModifyDate]                                            DATETIME2           NOT NULL,
    [Version]                                               ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ContractReportCertAuthorityFinancialCorrections]                             PRIMARY KEY ([ContractReportCertAuthorityFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_ContractReportFinancials]    FOREIGN KEY ([ContractReportFinancialId])   REFERENCES [dbo].[ContractReportFinancials] ([ContractReportFinancialId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_ContractReports]             FOREIGN KEY ([ContractReportId])            REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_Contracts]                   FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_Blobs]                       FOREIGN KEY ([BlobKey])                     REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportCertAuthorityFinancialCorrections_CheckedByUser]               FOREIGN KEY ([CheckedByUserId])             REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportCertAuthorityFinancialCorrections_Status]                     CHECK       ([Status]         IN (1, 2))
);
GO

exec spDescTable  N'ContractReportCertAuthorityFinancialCorrections', N'Корекции от СО на сертифицриани суми на ниво РОД - ПОД.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ContractReportCertAuthorityFinancialCorrectionId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ContractReportFinancialId'                          , N'Идентификатор на финансов отчет към пакет отчетни документи'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ContractReportId'                                   , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ContractId'                                         , N'Идентификатор на договор'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'Gid'                                                , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'OrderNum'                                           , N'Пореден номер.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'Status'                                             , N'Статус: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'CertCorrectionDate'                                 , N'Дата на препотвърждаване.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'BlobKey'                                            , N'Идентификатор на файл.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'Notes'                                              , N'Бележки.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'CheckedByUserId'                                    , N'Проверено от.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'CheckedDate'                                        , N'Дата на проверка.'

exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'CreateDate'                                         , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'ModifyDate'                                         , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportCertAuthorityFinancialCorrections', N'Version'                                            , N'Версия.'
GO

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

PRINT 'ContractReportCertAuthorityCorrections'
GO

CREATE TABLE [dbo].[ContractReportCertAuthorityCorrections] (
    [ContractReportCertAuthorityCorrectionId]   INT                 NOT NULL IDENTITY,
    [ProgrammeId]                               INT                 NOT NULL,
    [ProgrammePriorityId]                       INT                 NULL,
    [ProcedureId]                               INT                 NULL,
    [ContractId]                                INT                 NULL,
    [ContractReportPaymentId]                   INT                 NULL,
    [Type]                                      INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [RegNumber]                                 NVARCHAR(200)       NULL,
    [Sign]                                      INT                 NOT NULL,
    [Date]                                      DATETIME2           NOT NULL,
    [Description]                               NVARCHAR(MAX)       NULL,
    [Reason]                                    NVARCHAR(MAX)       NULL,
    [CheckedByUserId]                           INT                 NULL,
    [CheckedDate]                               DATETIME2           NULL,

    [FinanceSource]                             INT                 NOT NULL,
    [CertifiedEuAmount]                         MONEY               NULL,
    [CertifiedBgAmount]                         MONEY               NULL,
    [CertifiedBfpTotalAmount]                   MONEY               NULL,
    [CertifiedCrossAmount]                      MONEY               NULL,
    [CertifiedSelfAmount]                       MONEY               NULL,
    [CertifiedTotalAmount]                      MONEY               NULL,

    [IsActivated]                               BIT                 NOT NULL,
    [DeleteNote]                                NVARCHAR(MAX)       NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ContractReportCertAuthorityCorrections]                              PRIMARY KEY ([ContractReportCertAuthorityCorrectionId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrections_Programmes]                   FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrections_ProgrammePriorities]          FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrections_Procedures]                   FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrections_Contracts]                    FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]  ([ContractId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrections_ContractReportPayments]       FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_ClassPayment]                CHECK       ([Type] != 1                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL AND [ContractReportPaymentId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_ClassContract]               CHECK       ([Type] != 2                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_ClassPrgPriority]            CHECK       ([Type] != 4                OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_ClassProcedure]              CHECK       ([Type] != 5                OR [ProcedureId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_Type]                        CHECK       ([Type]                     IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_Status]                      CHECK       ([Status]                   IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_Sign]                        CHECK       ([Sign]                     IN (-1, 1)),
    CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_FinanceSource]               CHECK       ([FinanceSource]            IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractReportCertAuthorityCorrections' , N'Корекции от СО на сертифицриани суми на други нива.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'ContractReportCertAuthorityCorrectionId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'ProgrammeId'                                , N'Идентификатор на програма.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'ProgrammePriorityId'                        , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'ProcedureId'                                , N'Идентификатор на процедура.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'ContractId'                                 , N'Идентификатор на договор.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'ContractReportPaymentId'                    , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'Type'                                       , N'Вид: 1 - Верифицирани на ниво искане за плащане, 2 - Верифицирани на ниво договор, 3 - Верифицирани на ниво програма; 4 - Верифицирани на ниво приоритетна ос; 5 - Верифицирани на ниво процедура.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'Status'                                     , N'Статус: 1 - Чернова; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'RegNumber'                                  , N'Регистрационен номер.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'Sign'                                       , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'Date'                                       , N'Дата.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'Description'                                , N'Описание.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'Reason'                                     , N'Основание.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CheckedByUserId'                            , N'Проверено от.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CheckedDate'                                , N'Дата на проверка.'

exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'FinanceSource'                              , N'Фонд: 1 - ЕСФ; 2 - ЕФРР; 3 - КФ; 4 - ИМЗ; 5 - ФЕПНЛ.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CertifiedEuAmount'                          , N'Коригирана сертифицирана стойност финансиране от ЕС.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CertifiedBgAmount'                          , N'Коригирана сертифицирана стойност финансиране от НФ.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CertifiedBfpTotalAmount'                    , N'Коригирана сертифицирана стойност общо БФП.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CertifiedCrossAmount'                       , N'Коригирана сертифицирана стойност кръстосано съфинансиране'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CertifiedSelfAmount'                        , N'Коригирана сертифицирана стойност собствено съфинансиране.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CertifiedTotalAmount'                       , N'Коригирана сертифицирана стойност общо финансиране.'

exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'IsActivated'                                , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'DeleteNote'                                 , N'Причина за изтриване.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'CreateDate'                                 , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'ModifyDate'                                 , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportCertAuthorityCorrections' , N'Version'                                    , N'Версия.'
GO

PRINT 'ContractReportCertAuthorityCorrectionDocuments'
GO

CREATE TABLE [dbo].[ContractReportCertAuthorityCorrectionDocuments] (
    [ContractReportCertAuthorityCorrectionDocumentId]   INT                 NOT NULL IDENTITY,
    [ContractReportCertAuthorityCorrectionId]           INT                 NOT NULL,
    [Description]                                       NVARCHAR(MAX)       NOT NULL,
    [FileName]                                          NVARCHAR(200)       NOT NULL,
    [FileKey]                                           UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [PK_ContractReportCertAuthorityCorrectionDocuments]                                          PRIMARY KEY ([ContractReportCertAuthorityCorrectionDocumentId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrectionDocuments_ContractReportCertAuthorityCorrections]   FOREIGN KEY ([ContractReportCertAuthorityCorrectionId]) REFERENCES [dbo].[ContractReportCertAuthorityCorrections] ([ContractReportCertAuthorityCorrectionId]),
    CONSTRAINT [FK_ContractReportCertAuthorityCorrectionDocuments_Blobs]                                    FOREIGN KEY ([FileKey])                    REFERENCES [dbo].[Blobs]  ([Key]),
);
GO

exec spDescTable  N'ContractReportCertAuthorityCorrectionDocuments' , N'Документи към корекции от СО на верифицирани суми.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'ContractReportCertAuthorityCorrectionDocumentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'ContractReportCertAuthorityCorrectionId'        , N'Идентификатор на изравнителен документ.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'Description'                                    , N'Описание.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'FileName'                                       , N'Наименование.'
exec spDescColumn N'ContractReportCertAuthorityCorrectionDocuments' , N'FileKey'                                        , N'Идентификатор на файл.'
GO
