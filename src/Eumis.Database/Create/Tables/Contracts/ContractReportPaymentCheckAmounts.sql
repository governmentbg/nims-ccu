PRINT 'ContractReportPaymentCheckAmounts'
GO

CREATE TABLE [dbo].[ContractReportPaymentCheckAmounts] (
    [ContractReportPaymentCheckAmountId]    INT               NOT NULL IDENTITY,
    [ContractReportPaymentCheckId]          INT               NOT NULL,
    [ContractReportPaymentId]               INT               NOT NULL,
    [ContractReportId]                      INT               NOT NULL,
    [ContractId]                            INT               NOT NULL,
    [Gid]                                   UNIQUEIDENTIFIER  NOT NULL UNIQUE,

    [ProgrammePriorityId]                   INT               NOT NULL,
    [FinanceSource]                         INT               NOT NULL,
    [ApprovedEuAmount]                      MONEY             NULL,
    [ApprovedBgAmount]                      MONEY             NULL,
    [ApprovedBfpTotalAmount]                MONEY             NULL,
    [ApprovedCrossAmount]                   MONEY             NULL,
    [ApprovedSelfAmount]                    MONEY             NULL,
    [ApprovedTotalAmount]                   MONEY             NULL,

    [PaidEuAmount]                          MONEY             NULL,
    [PaidBgAmount]                          MONEY             NULL,
    [PaidBfpTotalAmount]                    MONEY             NULL,
    [PaidCrossAmount]                       MONEY             NULL,

    CONSTRAINT [PK_ContractReportPaymentCheckAmounts]                               PRIMARY KEY ([ContractReportPaymentCheckAmountId]),
    CONSTRAINT [FK_ContractReportPaymentCheckAmounts_ContractReportPaymentChecks]   FOREIGN KEY ([ContractReportPaymentCheckId]) REFERENCES [dbo].[ContractReportPaymentChecks] ([ContractReportPaymentCheckId]),
    CONSTRAINT [FK_ContractReportPaymentCheckAmounts_ContractReportPayments]        FOREIGN KEY ([ContractReportPaymentId])      REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportPaymentCheckAmounts_ContractReports]               FOREIGN KEY ([ContractReportId])             REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportPaymentCheckAmounts_Contracts]                     FOREIGN KEY ([ContractId])                   REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_ContractReportPaymentCheckAmounts_FinanceSource]                CHECK       ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractReportPaymentCheckAmounts', N'Проверка на искане за плащане.'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ContractReportPaymentCheckAmountId'     , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ContractReportPaymentId'          , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ContractReportId'                 , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ContractId'                       , N'Идентификатор на договор'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'Gid'                              , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ApprovedEuAmount'                 , N'Одобрена сума БФП - ЕС.'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ApprovedBgAmount'                 , N'Одобрена сума БФП - НФ.'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ApprovedBfpTotalAmount'           , N'Одобрена обща сума БФП.'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ApprovedSelfAmount'               , N'Одобрено собствено съфинансиране.'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'ApprovedTotalAmount'              , N'Одобрена обща сума.'

exec spDescColumn N'ContractReportPaymentCheckAmounts', N'PaidEuAmount'                     , N'Стойност финансиране ЕС за плащане.'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'PaidBgAmount'                     , N'Стойност национално финансиране за плащане.'
exec spDescColumn N'ContractReportPaymentCheckAmounts', N'PaidBfpTotalAmount'               , N'Обща сума БФП за плащане.'
GO