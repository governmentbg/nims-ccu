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
