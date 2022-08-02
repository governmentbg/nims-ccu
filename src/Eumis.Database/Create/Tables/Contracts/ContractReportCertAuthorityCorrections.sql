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
