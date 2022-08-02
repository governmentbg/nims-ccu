PRINT 'ContractReportCorrections'
GO

CREATE TABLE [dbo].[ContractReportCorrections] (
    [ContractReportCorrectionId]                   INT               NOT NULL IDENTITY,
    [ProgrammeId]                                  INT               NOT NULL,
    [ProgrammePriorityId]                          INT               NULL,
    [ProcedureId]                                  INT               NULL,
    [ContractId]                                   INT               NULL,
    [ContractReportPaymentId]                      INT               NULL,
    [Type]                                         INT               NOT NULL,
    [Status]                                       INT               NOT NULL,
    [RegNumber]                                    NVARCHAR(200)     NULL,
    [Sign]                                         INT               NOT NULL,
    [Date]                                         DATETIME2         NOT NULL,
    [Description]                                  NVARCHAR(MAX)     NULL,
    [Reason]                                       NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                              INT               NULL,
    [CheckedDate]                                  DATETIME2         NULL,

    [CorrectionType]                               INT               NULL,
    [FinancialCorrectionId]                        INT               NULL,
    [IrregularityId]                               INT               NULL,
    [FlatFinancialCorrectionId]                    INT               NULL,

    [FinanceSource]                                INT               NOT NULL,
    [CorrectedApprovedEuAmount]                    MONEY             NULL,
    [CorrectedApprovedBgAmount]                    MONEY             NULL,
    [CorrectedApprovedBfpTotalAmount]              MONEY             NULL,
    [CorrectedApprovedCrossAmount]                 MONEY             NULL,
    [CorrectedApprovedSelfAmount]                  MONEY             NULL,
    [CorrectedApprovedTotalAmount]                 MONEY             NULL,

    [CertReportId]                                 INT               NULL,

    [CertStatus]                                   INT               NULL,
    [CertCheckedByUserId]                          INT               NULL,
    [CertCheckedDate]                              DATETIME2         NULL,
    [UncertifiedCorrectedApprovedEuAmount]         MONEY             NULL,
    [UncertifiedCorrectedApprovedBgAmount]         MONEY             NULL,
    [UncertifiedCorrectedApprovedBfpTotalAmount]   MONEY             NULL,
    [UncertifiedCorrectedApprovedCrossAmount]      MONEY             NULL,
    [UncertifiedCorrectedApprovedSelfAmount]       MONEY             NULL,
    [UncertifiedCorrectedApprovedTotalAmount]      MONEY             NULL,

    [CertifiedCorrectedApprovedEuAmount]           MONEY             NULL,
    [CertifiedCorrectedApprovedBgAmount]           MONEY             NULL,
    [CertifiedCorrectedApprovedBfpTotalAmount]     MONEY             NULL,
    [CertifiedCorrectedApprovedCrossAmount]        MONEY             NULL,
    [CertifiedCorrectedApprovedSelfAmount]         MONEY             NULL,
    [CertifiedCorrectedApprovedTotalAmount]        MONEY             NULL,

    [IsActivated]                                  BIT               NOT NULL,
    [DeleteNote]                                   NVARCHAR(MAX)     NULL,
    [CreateDate]                                   DATETIME2         NOT NULL,
    [ModifyDate]                                   DATETIME2         NOT NULL,
    [Version]                                      ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportCorrections]                             PRIMARY KEY ([ContractReportCorrectionId]),
    CONSTRAINT [FK_ContractReportCorrections_Programmes]                  FOREIGN KEY ([ProgrammeId])               REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportCorrections_ProgrammePriorities]         FOREIGN KEY ([ProgrammePriorityId])       REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_ContractReportCorrections_Procedures]                  FOREIGN KEY ([ProcedureId])               REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ContractReportCorrections_Contracts]                   FOREIGN KEY ([ContractId])                REFERENCES [dbo].[Contracts]  ([ContractId]),
    CONSTRAINT [FK_ContractReportCorrections_ContractReportPayments]      FOREIGN KEY ([ContractReportPaymentId])   REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportCorrections_FinancialCorrections]        FOREIGN KEY ([FinancialCorrectionId])     REFERENCES [dbo].[FinancialCorrections] ([FinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportCorrections_Irregularities]              FOREIGN KEY ([IrregularityId])            REFERENCES [dbo].[Irregularities] ([IrregularityId]),
    CONSTRAINT [FK_ContractReportCorrections_FlatFinancialCorrections]    FOREIGN KEY ([FlatFinancialCorrectionId]) REFERENCES [dbo].[FlatFinancialCorrections] ([FlatFinancialCorrectionId]),
    CONSTRAINT [FK_ContractReportCorrections_CheckedByUser]               FOREIGN KEY ([CheckedByUserId])           REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportCorrections_CertCheckedByUser]           FOREIGN KEY ([CertCheckedByUserId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportCorrections_CertReports]                 FOREIGN KEY ([CertReportId])              REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_ContractReportCorrections_ClassPayment]               CHECK       ([Type] != 1                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL AND [ContractReportPaymentId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportCorrections_ClassContract]              CHECK       ([Type] != 2                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportCorrections_ClassPrgPriority]           CHECK       ([Type] != 4                OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportCorrections_ClassProcedure]             CHECK       ([Type] != 5                OR [ProcedureId] IS NOT NULL),
    CONSTRAINT [CHK_ContractReportCorrections_ClassAdvanceCovered]        CHECK       ([Type] != 6                OR ([ProcedureId] IS NOT NULL AND [ContractId] IS NOT NULL AND [ContractReportPaymentId] IS NOT NULL)),
    CONSTRAINT [CHK_ContractReportCorrections_Type]                       CHECK       ([Type]              IN (1, 2, 3, 4, 5, 6)),
    CONSTRAINT [CHK_ContractReportCorrections_CorrectionType]             CHECK       ([CorrectionType]    IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ContractReportCorrections_Status]                     CHECK       ([Status]            IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractReportCorrections_CertStatus]                 CHECK       ([CertStatus]        IN (1, 2)),
    CONSTRAINT [CHK_ContractReportCorrections_Sign]                       CHECK       ([Sign]              IN (-1, 1)),
    CONSTRAINT [CHK_ContractReportCorrections_FinanceSource]              CHECK       ([FinanceSource]     IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractReportCorrections', N'Корекции на верифицирани суми на други нива.'
exec spDescColumn N'ContractReportCorrections', N'ContractReportCorrectionId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportCorrections', N'ProgrammeId'                     , N'Идентификатор на програма.'
exec spDescColumn N'ContractReportCorrections', N'ProgrammePriorityId'             , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ContractReportCorrections', N'ProcedureId'                     , N'Идентификатор на процедура.'
exec spDescColumn N'ContractReportCorrections', N'ContractId'                      , N'Идентификатор на договор.'
exec spDescColumn N'ContractReportCorrections', N'ContractReportPaymentId'         , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportCorrections', N'Type'                            , N'Вид: 1 - Верифицирани на ниво искане за плащане, 2 - Верифицирани на ниво договор, 3 - Верифицирани на ниво програма; 4 - Верифицирани на ниво приоритетна ос; 5 - Верифицирани на ниво процедура; 6 - Покриване на аванс по чл. 131.'
exec spDescColumn N'ContractReportCorrections', N'Status'                          , N'Статус: 1 - Чернова; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'ContractReportCorrections', N'RegNumber'                       , N'Регистрационен номер.'
exec spDescColumn N'ContractReportCorrections', N'Sign'                            , N'Знак: 1 - -, 2 - +.'
exec spDescColumn N'ContractReportCorrections', N'Date'                            , N'Дата.'
exec spDescColumn N'ContractReportCorrections', N'Description'                     , N'Описание.'
exec spDescColumn N'ContractReportCorrections', N'Reason'                          , N'Основание.'
exec spDescColumn N'ContractReportCorrections', N'CorrectionType'                  , N'Тип на корекцията.'
exec spDescColumn N'ContractReportCorrections', N'FinancialCorrectionId'           , N'Идентификатор на финансова корекция.'
exec spDescColumn N'ContractReportCorrections', N'IrregularityId'                  , N'Идентификатор на нередност.'
exec spDescColumn N'ContractReportCorrections', N'FlatFinancialCorrectionId'       , N'Идентификатор на ФКСП.'

exec spDescColumn N'ContractReportCorrections', N'FinanceSource'                   , N'Фонд: 1 - ЕСФ; 2 - ЕФРР; 3 - КФ; 4 - ИМЗ; 5 - ФЕПНЛ.'
exec spDescColumn N'ContractReportCorrections', N'CorrectedApprovedEuAmount'       , N'Финансиране от ЕС.'
exec spDescColumn N'ContractReportCorrections', N'CorrectedApprovedBgAmount'       , N'Финансиране от НФ.'
exec spDescColumn N'ContractReportCorrections', N'CorrectedApprovedBfpTotalAmount' , N'Общо БФП.'
exec spDescColumn N'ContractReportCorrections', N'CorrectedApprovedSelfAmount'     , N'Собствено съфинансиране.'
exec spDescColumn N'ContractReportCorrections', N'CorrectedApprovedTotalAmount'    , N'Общо финансиране.'

exec spDescColumn N'ContractReportCorrections', N'IsActivated'                     , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'ContractReportCorrections', N'DeleteNote'                      , N'Причина за изтриване.'
exec spDescColumn N'ContractReportCorrections', N'CreateDate'                      , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportCorrections', N'ModifyDate'                      , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportCorrections', N'Version'                         , N'Версия.'
GO