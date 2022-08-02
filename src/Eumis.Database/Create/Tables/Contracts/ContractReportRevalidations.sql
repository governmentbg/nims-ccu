PRINT 'ContractReportRevalidations'
GO

CREATE TABLE [dbo].[ContractReportRevalidations] (
    [ContractReportRevalidationId]           INT                     NOT NULL IDENTITY,
    [ProgrammeId]                            INT                     NOT NULL,
    [ProgrammePriorityId]                    INT                     NULL,
    [ProcedureId]                            INT                     NULL,
    [ContractId]                             INT                     NULL,
    [ContractReportPaymentId]                INT                     NULL,
    [Type]                                   INT                     NOT NULL,
    [Status]                                 INT                     NOT NULL,
    [RegNumber]                              NVARCHAR(200)           NULL,
    [Sign]                                   INT                     NOT NULL,
    [Date]                                   DATETIME2               NOT NULL,
    [Description]                            NVARCHAR(MAX)           NULL,
    [Reason]                                 NVARCHAR(MAX)           NULL,
    [CheckedByUserId]                        INT                     NULL,
    [CheckedDate]                            DATETIME2               NULL,

    [FinanceSource]                          INT                     NOT NULL,
    [RevalidatedEuAmount]                    MONEY                   NULL,
    [RevalidatedBgAmount]                    MONEY                   NULL,
    [RevalidatedBfpTotalAmount]              MONEY                   NULL,
    [RevalidatedCrossAmount]                 MONEY                   NULL,
    [RevalidatedSelfAmount]                  MONEY                   NULL,
    [RevalidatedTotalAmount]                 MONEY                   NULL,

    [CertReportId]                           INT                     NULL,

    [CertStatus]                             INT                     NULL,
    [CertCheckedByUserId]                    INT                     NULL,
    [CertCheckedDate]                        DATETIME2               NULL,
    [UncertifiedRevalidatedEuAmount]         MONEY                   NULL,
    [UncertifiedRevalidatedBgAmount]         MONEY                   NULL,
    [UncertifiedRevalidatedBfpTotalAmount]   MONEY                   NULL,
    [UncertifiedRevalidatedCrossAmount]      MONEY                   NULL,
    [UncertifiedRevalidatedSelfAmount]       MONEY                   NULL,
    [UncertifiedRevalidatedTotalAmount]      MONEY                   NULL,

    [CertifiedRevalidatedEuAmount]           MONEY                   NULL,
    [CertifiedRevalidatedBgAmount]           MONEY                   NULL,
    [CertifiedRevalidatedBfpTotalAmount]     MONEY                   NULL,
    [CertifiedRevalidatedCrossAmount]        MONEY                   NULL,
    [CertifiedRevalidatedSelfAmount]         MONEY                   NULL,
    [CertifiedRevalidatedTotalAmount]        MONEY                   NULL,

    [IsActivated]                            BIT                     NOT NULL,
    [DeleteNote]                             NVARCHAR(MAX)           NULL,
    [CreateDate]                             DATETIME2               NOT NULL,
    [ModifyDate]                             DATETIME2               NOT NULL,
    [Version]                                ROWVERSION              NOT NULL,

    CONSTRAINT [PK_ContractReportRevalidations]                             PRIMARY KEY ([ContractReportRevalidationId]),
    CONSTRAINT [FK_ContractReportRevalidations_Programmes]                  FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportRevalidations_ProgrammePriorities]         FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportRevalidations_Procedures]                  FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ContractReportRevalidations_Contracts]                   FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]  ([ContractId]),
    CONSTRAINT [FK_ContractReportRevalidations_ContractReportPayments]      FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportRevalidations_CertReports]                 FOREIGN KEY ([CertReportId])            REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_ContractReportRevalidations_ClassPayment]               CHECK       ([Type] != 1                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL AND [ContractReportPaymentId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportRevalidations_ClassContract]              CHECK       ([Type] != 2                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportRevalidations_ClassPrgPriority]           CHECK       ([Type] != 4                OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportRevalidations_ClassProcedure]             CHECK       ([Type] != 5                OR [ProcedureId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportRevalidations_Type]                       CHECK       ([Type]              IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractReportRevalidations_Status]                     CHECK       ([Status]            IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportRevalidations_Sign]                       CHECK       ([Sign]              IN (-1, 1)),
    CONSTRAINT [CHK_ContractReportRevalidations_FinanceSource]              CHECK       ([FinanceSource]     IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractReportRevalidations', N'Препотвърждаване на верифицирани суми на други нива.'
exec spDescColumn N'ContractReportRevalidations', N'ContractReportRevalidationId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportRevalidations', N'ProgrammeId'                     , N'Идентификатор на програма.'
exec spDescColumn N'ContractReportRevalidations', N'ProgrammePriorityId'             , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ContractReportRevalidations', N'ProcedureId'                     , N'Идентификатор на процедура.'
exec spDescColumn N'ContractReportRevalidations', N'ContractId'                      , N'Идентификатор на договор.'
exec spDescColumn N'ContractReportRevalidations', N'ContractReportPaymentId'         , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportRevalidations', N'Type'                            , N'Вид: 1 - Верифицирани на ниво искане за плащане, 2 - Верифицирани на ниво договор, 3 - Верифицирани на ниво програма; 4 - Верифицирани на ниво приоритетна ос; 5 - Верифицирани на ниво процедура.'
exec spDescColumn N'ContractReportRevalidations', N'Status'                          , N'Статус: 1 - Чернова; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'ContractReportRevalidations', N'RegNumber'                       , N'Регистрационен номер.'
exec spDescColumn N'ContractReportRevalidations', N'Sign'                            , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportRevalidations', N'Date'                            , N'Дата.'
exec spDescColumn N'ContractReportRevalidations', N'Description'                     , N'Описание.'
exec spDescColumn N'ContractReportRevalidations', N'Reason'                          , N'Основание.'

exec spDescColumn N'ContractReportRevalidations', N'FinanceSource'                   , N'Фонд: 1 - ЕСФ; 2 - ЕФРР; 3 - КФ; 4 - ИМЗ; 5 - ФЕПНЛ.'
exec spDescColumn N'ContractReportRevalidations', N'RevalidatedEuAmount'             , N'Финансиране от ЕС.'
exec spDescColumn N'ContractReportRevalidations', N'RevalidatedBgAmount'             , N'Финансиране от НФ.'
exec spDescColumn N'ContractReportRevalidations', N'RevalidatedBfpTotalAmount'       , N'Общо БФП.'
exec spDescColumn N'ContractReportRevalidations', N'RevalidatedSelfAmount'           , N'Собствено съфинансиране.'
exec spDescColumn N'ContractReportRevalidations', N'RevalidatedTotalAmount'          , N'Общо финансиране.'

exec spDescColumn N'ContractReportRevalidations', N'IsActivated'                     , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'ContractReportRevalidations', N'DeleteNote'                      , N'Причина за изтриване.'
exec spDescColumn N'ContractReportRevalidations', N'CreateDate'                      , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportRevalidations', N'ModifyDate'                      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportRevalidations', N'Version'                         , N'Версия.'
GO