PRINT 'ContractReportCertCorrections'
GO

CREATE TABLE [dbo].[ContractReportCertCorrections] (
    [ContractReportCertCorrectionId]         INT                NOT NULL IDENTITY,
    [ProgrammeId]                            INT                NOT NULL,
    [ProgrammePriorityId]                    INT                NULL,
    [ProcedureId]                            INT                NULL,
    [ContractId]                             INT                NULL,
    [ContractReportPaymentId]                INT                NULL,
    [Type]                                   INT                NOT NULL,
    [Status]                                 INT                NOT NULL,
    [RegNumber]                              NVARCHAR(200)      NULL,
    [Sign]                                   INT                NOT NULL,
    [Date]                                   DATETIME2          NOT NULL,
    [Description]                            NVARCHAR(MAX)      NULL,
    [Reason]                                 NVARCHAR(MAX)      NULL,
    [CheckedByUserId]                        INT                NULL,
    [CheckedDate]                            DATETIME2          NULL,

    [FinanceSource]                          INT                NOT NULL,
    [CertifiedEuAmount]                      MONEY              NULL,
    [CertifiedBgAmount]                      MONEY              NULL,
    [CertifiedBfpTotalAmount]                MONEY              NULL,
    [CertifiedCrossAmount]                   MONEY              NULL,
    [CertifiedSelfAmount]                    MONEY              NULL,
    [CertifiedTotalAmount]                   MONEY              NULL,

    [CertReportId]                           INT                NULL,

    [IsActivated]                            BIT                NOT NULL,
    [DeleteNote]                             NVARCHAR(MAX)      NULL,
    [CreateDate]                             DATETIME2          NOT NULL,
    [ModifyDate]                             DATETIME2          NOT NULL,
    [Version]                                ROWVERSION         NOT NULL,

    CONSTRAINT [PK_ContractReportCertCorrections]                             PRIMARY KEY ([ContractReportCertCorrectionId]),
    CONSTRAINT [FK_ContractReportCertCorrections_Programmes]                  FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportCertCorrections_ProgrammePriorities]         FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportCertCorrections_Procedures]                  FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ContractReportCertCorrections_Contracts]                   FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]  ([ContractId]),
    CONSTRAINT [FK_ContractReportCertCorrections_ContractReportPayments]      FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportCertCorrections_CertReports]                 FOREIGN KEY ([CertReportId])            REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_ContractReportCertCorrections_ClassPayment]               CHECK       ([Type] != 1                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL AND [ContractReportPaymentId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportCertCorrections_ClassContract]              CHECK       ([Type] != 2                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportCertCorrections_ClassPrgPriority]           CHECK       ([Type] != 4                OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportCertCorrections_ClassProcedure]             CHECK       ([Type] != 5                OR [ProcedureId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportCertCorrections_Type]                       CHECK       ([Type]              IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractReportCertCorrections_Status]                     CHECK       ([Status]            IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportCertCorrections_Sign]                       CHECK       ([Sign]              IN (-1, 1)),
    CONSTRAINT [CHK_ContractReportCertCorrections_FinanceSource]              CHECK       ([FinanceSource]     IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractReportCertCorrections', N'Корекции на сертифицриани суми на други нива.'
exec spDescColumn N'ContractReportCertCorrections', N'ContractReportCertCorrectionId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCertCorrections', N'ProgrammeId'                     , N'Идентификатор на програма.'
exec spDescColumn N'ContractReportCertCorrections', N'ProgrammePriorityId'             , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ContractReportCertCorrections', N'ProcedureId'                     , N'Идентификатор на процедура.'
exec spDescColumn N'ContractReportCertCorrections', N'ContractId'                      , N'Идентификатор на договор.'
exec spDescColumn N'ContractReportCertCorrections', N'ContractReportPaymentId'         , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportCertCorrections', N'Type'                            , N'Вид: 1 - Верифицирани на ниво искане за плащане, 2 - Верифицирани на ниво договор, 3 - Верифицирани на ниво програма; 4 - Верифицирани на ниво приоритетна ос; 5 - Верифицирани на ниво процедура.'
exec spDescColumn N'ContractReportCertCorrections', N'Status'                          , N'Статус: 1 - Чернова; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'ContractReportCertCorrections', N'RegNumber'                       , N'Регистрационен номер.'
exec spDescColumn N'ContractReportCertCorrections', N'Sign'                            , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportCertCorrections', N'Date'                            , N'Дата.'
exec spDescColumn N'ContractReportCertCorrections', N'Description'                     , N'Описание.'
exec spDescColumn N'ContractReportCertCorrections', N'Reason'                          , N'Основание.'

exec spDescColumn N'ContractReportCertCorrections', N'FinanceSource'                   , N'Фонд: 1 - ЕСФ; 2 - ЕФРР; 3 - КФ; 4 - ИМЗ; 5 - ФЕПНЛ.'
exec spDescColumn N'ContractReportCertCorrections', N'CertifiedEuAmount'               , N'Коригирана сертифицирана стойност финансиране от ЕС.'
exec spDescColumn N'ContractReportCertCorrections', N'CertifiedBgAmount'               , N'Коригирана сертифицирана стойност финансиране от НФ.'
exec spDescColumn N'ContractReportCertCorrections', N'CertifiedBfpTotalAmount'         , N'Коригирана сертифицирана стойност общо БФП.'
exec spDescColumn N'ContractReportCertCorrections', N'CertifiedSelfAmount'             , N'Коригирана сертифицирана стойност собствено съфинансиране.'
exec spDescColumn N'ContractReportCertCorrections', N'CertifiedTotalAmount'            , N'Коригирана сертифицирана стойност общо финансиране.'

exec spDescColumn N'ContractReportCertCorrections', N'IsActivated'                     , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'ContractReportCertCorrections', N'DeleteNote'                      , N'Причина за изтриване.'
exec spDescColumn N'ContractReportCertCorrections', N'CreateDate'                      , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportCertCorrections', N'ModifyDate'                      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportCertCorrections', N'Version'                         , N'Версия.'
GO