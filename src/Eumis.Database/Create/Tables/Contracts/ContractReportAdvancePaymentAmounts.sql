PRINT 'ContractReportAdvancePaymentAmounts'
GO

CREATE TABLE [dbo].[ContractReportAdvancePaymentAmounts] (
    [ContractReportAdvancePaymentAmountId]                  INT               NOT NULL IDENTITY,
    [ContractReportPaymentId]                               INT               NOT NULL,
    [ContractReportId]                                      INT               NOT NULL,
    [ContractId]                                            INT               NOT NULL,
    [Gid]                                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [Status]                                                INT               NOT NULL,
    [Approval]                                              INT               NULL,
    [Notes]                                                 NVARCHAR(MAX)     NULL,
    [CheckedByUserId]                                       INT               NULL,
    [CheckedDate]                                           DATETIME2         NULL,

    [ProgrammePriorityId]                                   INT               NOT NULL,
    [FinanceSource]                                         INT               NOT NULL,
    [ApprovedEuAmount]                                      MONEY             NULL,
    [ApprovedBgAmount]                                      MONEY             NULL,
    [ApprovedBfpTotalAmount]                                MONEY             NULL,

    [CertReportId]                                          INT               NULL,

    [CertStatus]                                            INT               NULL,
    [CertCheckedByUserId]                                   INT               NULL,
    [CertCheckedDate]                                       DATETIME2         NULL,
    [UncertifiedApprovedEuAmount]                           MONEY             NULL,
    [UncertifiedApprovedBgAmount]                           MONEY             NULL,
    [UncertifiedApprovedBfpTotalAmount]                     MONEY             NULL,

    [CertifiedApprovedEuAmount]                             MONEY             NULL,
    [CertifiedApprovedBgAmount]                             MONEY             NULL,
    [CertifiedApprovedBfpTotalAmount]                       MONEY             NULL,

    [CreateDate]                                            DATETIME2         NOT NULL,
    [ModifyDate]                                            DATETIME2         NOT NULL,
    [Version]                                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportAdvancePaymentAmounts]                                   PRIMARY KEY ([ContractReportAdvancePaymentAmountId]),
    CONSTRAINT [FK_ContractReportAdvancePaymentAmounts_ContractReportPayments]            FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportAdvancePaymentAmounts_ContractReports]                   FOREIGN KEY ([ContractReportId])        REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportAdvancePaymentAmounts_Contracts]                         FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportAdvancePaymentAmounts_ProgrammePriorities]               FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ContractReportAdvancePaymentAmounts_CheckedByUser]                     FOREIGN KEY ([CheckedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportAdvancePaymentAmounts_CertCheckedByUser]                 FOREIGN KEY ([CertCheckedByUserId])     REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_ContractReportAdvancePaymentAmounts_CertReports]                       FOREIGN KEY ([CertReportId])            REFERENCES [dbo].[CertReports] ([CertReportId]),
    CONSTRAINT [CHK_ContractReportAdvancePaymentAmounts_Status]                           CHECK       ([Status]              IN (1, 2)),
    CONSTRAINT [CHK_ContractReportAdvancePaymentAmounts_CertStatus]                       CHECK       ([CertStatus]          IN (1, 2)),
    CONSTRAINT [CHK_ContractReportAdvancePaymentAmounts_FinanceSource]                    CHECK       ([FinanceSource]       IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractReportAdvancePaymentAmounts', N'Суми на авансово искане за плащане, подлежащо на верификация.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'ContractReportAdvancePaymentAmountId'      , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'ContractReportPaymentId'                   , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'ContractReportId'                          , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'ContractId'                                , N'Идентификатор на договор'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'Gid'                                       , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'Status'                                    , N'Статус: 1 - Чернова, 2 - Въведен.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'Notes'                                     , N'Бележки.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'CheckedByUserId'                           , N'Проверено от.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'CheckedDate'                               , N'Дата на проверка.'

exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'FinanceSource'                             , N'Идентификатор на източник на финансиране.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'ApprovedEuAmount'                          , N'Одобрена сума БФП - ЕС.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'ApprovedBgAmount'                          , N'Одобрена сума БФП - НФ.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'ApprovedBfpTotalAmount'                    , N'Одобрена обща сума БФП.'

exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'CertStatus'                                , N'Статус за сертификация: 1- Чернова; 2 - Приключен.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'CertifiedApprovedEuAmount'                 , N'Сертифицирана одобрена сума БФП - ЕС.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'CertifiedApprovedBgAmount'                 , N'Сертифицирана одобрена сума БФП - НФ.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'CertifiedApprovedBfpTotalAmount'           , N'Сертифицирана одобрена обща сума БФП.'

exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'CreateDate'                                , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'ModifyDate'                                , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportAdvancePaymentAmounts', N'Version'                                   , N'Версия.'
GO
