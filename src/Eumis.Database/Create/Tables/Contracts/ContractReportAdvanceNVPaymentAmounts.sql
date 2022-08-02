PRINT 'ContractReportAdvanceNVPaymentAmounts'
GO

CREATE TABLE [dbo].[ContractReportAdvanceNVPaymentAmounts] (
    [ContractReportAdvanceNVPaymentAmountId]                INT               NOT NULL IDENTITY,
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
    [EuAmount]                                              MONEY             NULL,
    [BgAmount]                                              MONEY             NULL,
    [BfpTotalAmount]                                        MONEY             NULL,

    [CreateDate]                                            DATETIME2         NOT NULL,
    [ModifyDate]                                            DATETIME2         NOT NULL,
    [Version]                                               ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportAdvanceNVPaymentAmounts]                                   PRIMARY KEY ([ContractReportAdvanceNVPaymentAmountId]),
    CONSTRAINT [FK_ContractReportAdvanceNVPaymentAmounts_ContractReportPayments]            FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportAdvanceNVPaymentAmounts_ContractReports]                   FOREIGN KEY ([ContractReportId])        REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportAdvanceNVPaymentAmounts_Contracts]                         FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportAdvanceNVPaymentAmounts_ProgrammePriorities]               FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ContractReportAdvanceNVPaymentAmounts_CheckedByUser]                     FOREIGN KEY ([CheckedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractReportAdvanceNVPaymentAmounts_Status]                           CHECK       ([Status]              IN (1, 2)),
    CONSTRAINT [CHK_ContractReportAdvanceNVPaymentAmounts_FinanceSource]                    CHECK       ([FinanceSource]       IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'ContractReportAdvanceNVPaymentAmounts', N'Суми на авансово искане за плащане.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'ContractReportAdvanceNVPaymentAmountId'    , N'Уникален системно генериран идентификатор'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'ContractReportPaymentId'                   , N'Идентификатор на искане за плащане'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'ContractReportId'                          , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'ContractId'                                , N'Идентификатор на договор'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'Gid'                                       , N'Уникален системно генериран публичен идентификатор.'

exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'Status'                                    , N'Статус: 1 - Чернова, 2 - Въведен.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'Notes'                                     , N'Бележки.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'CheckedByUserId'                           , N'Проверено от.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'CheckedDate'                               , N'Дата на проверка.'

exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'FinanceSource'                             , N'Идентификатор на източник на финансиране.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'EuAmount'                                  , N'Одобрена сума БФП - ЕС.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'BgAmount'                                  , N'Одобрена сума БФП - НФ.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'BfpTotalAmount'                            , N'Одобрена обща сума БФП.'

exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'CreateDate'                                , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'ModifyDate'                                , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportAdvanceNVPaymentAmounts', N'Version'                                   , N'Версия.'
GO
