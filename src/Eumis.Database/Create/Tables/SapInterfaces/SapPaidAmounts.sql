PRINT 'SapPaidAmounts'
GO

CREATE TABLE [dbo].[SapPaidAmounts] (
    [SapPaidAmountId]           INT              NOT NULL IDENTITY,
    [SapFileId]                 INT              NOT NULL,
    [IsImported]                BIT              NOT NULL,
    [ActuallyPaidAmountId]      INT              NULL,
    [ReimbursedAmountId]        INT              NULL,

    [ProgrammeId]               INT              NULL,
    [ProgrammePriorityId]       INT              NULL,
    [ContractSapNum]            NVARCHAR(50)     NULL,
    [ContractId]                INT              NULL,
    [ContractDebtId]            INT              NULL,
    [Fund]                      INT              NULL,
    [ContractReportPaymentNum]  NVARCHAR(50)     NULL,
    [ContractReportPaymentId]   INT              NULL,
    [ContractReportPaymentDate] DATETIME2        NULL,
    [PaidBfpBgAmount]           MONEY             NOT NULL,
    [PaidBfpEuAmount]           MONEY             NOT NULL,
    [Currency]                  INT              NULL,
    [PaymentType]               INT              NULL,
    [AccDate]                   DATETIME2        NULL,
    [BankDate]                  DATETIME2        NULL,
    [SapDate]                   DATETIME2        NULL,
    [Comment]                   NVARCHAR(MAX)    NULL,
    [StornoCode]                NVARCHAR(50)     NULL,
    [StornoDescr]               NVARCHAR(MAX)    NULL,

    [HasWarning]                BIT              NOT NULL,
    [Warnings]                  NVARCHAR(MAX)    NULL,

    [HasError]                  BIT              NOT NULL,
    [Errors]                    NVARCHAR(MAX)    NULL,

    CONSTRAINT [PK_SapPaidAmounts]                        PRIMARY KEY ([SapPaidAmountId]),
    CONSTRAINT [FK_SapPaidAmounts_SapFiles]               FOREIGN KEY ([SapFileId])               REFERENCES [dbo].[SapFiles]               ([SapFileId]),
    CONSTRAINT [FK_SapPaidAmounts_ActuallyPaidAmounts]    FOREIGN KEY ([ActuallyPaidAmountId])    REFERENCES [dbo].[ActuallyPaidAmounts]    ([ActuallyPaidAmountId]),
    CONSTRAINT [FK_SapPaidAmounts_ReimbursedAmounts]      FOREIGN KEY ([ReimbursedAmountId])      REFERENCES [dbo].[ReimbursedAmounts]      ([ReimbursedAmountId]),
    CONSTRAINT [FK_SapPaidAmounts_Programmes]             FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_SapPaidAmounts_ProgrammePriorities]    FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_SapPaidAmounts_Contracts]              FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]              ([ContractId]),
    CONSTRAINT [FK_SapPaidAmounts_ContractDebts]          FOREIGN KEY ([ContractDebtId])          REFERENCES [dbo].[ContractDebts]          ([ContractDebtId]),
    CONSTRAINT [FK_SapPaidAmounts_ContractReportPayments] FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [CHK_SapPaidAmounts_Fund]                  CHECK       ([Fund]          IN (1, 2, 3, 5)),
    CONSTRAINT [CHK_SapPaidAmounts_Currency]              CHECK       ([Currency]      IN (1)),
    CONSTRAINT [CHK_SapPaidAmounts_PaymentType]           CHECK       ([PaymentType]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

exec spDescTable  N'SapPaidAmounts', N'Файлове от SAP.'
exec spDescColumn N'SapPaidAmounts', N'SapPaidAmountId'          , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SapPaidAmounts', N'SapFileId'                , N'Идентификатор на SAP файл.'
exec spDescColumn N'SapPaidAmounts', N'IsImported'               , N'Маркер дали документът е бил импортиран.'
exec spDescColumn N'SapPaidAmounts', N'ActuallyPaidAmountId'     , N'Идентификатор на реално изплатена сума.'
exec spDescColumn N'SapPaidAmounts', N'ReimbursedAmountId'       , N'Идентификатор на възстановена сума.'

exec spDescColumn N'SapPaidAmounts', N'ProgrammeId'              , N'Идентификатор на програма.'
exec spDescColumn N'SapPaidAmounts', N'ProgrammePriorityId'      , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'SapPaidAmounts', N'ContractSapNum'           , N'Номер на договор, посочен във файла.'
exec spDescColumn N'SapPaidAmounts', N'ContractId'               , N'Идентификатор на договор.'
exec spDescColumn N'SapPaidAmounts', N'Fund'                     , N'Фонд.'
exec spDescColumn N'SapPaidAmounts', N'ContractReportPaymentNum' , N'Номер на искане за плащане.'
exec spDescColumn N'SapPaidAmounts', N'ContractReportPaymentId'  , N'Идентификатор на искане за плащане.'
exec spDescColumn N'SapPaidAmounts', N'ContractReportPaymentDate', N'Дата на искането за плащане.'
exec spDescColumn N'SapPaidAmounts', N'PaidBfpBgAmount'          , N'Изплатена сума Финансиране от НФ.'
exec spDescColumn N'SapPaidAmounts', N'PaidBfpEuAmount'          , N'Изплатена сума Финансиране от ЕС.'
exec spDescColumn N'SapPaidAmounts', N'Currency'                 , N'Валута на плащането.'
exec spDescColumn N'SapPaidAmounts', N'PaymentType'              , N'Вид: 1 - авансово; 2 - междинно; 3 - окончателно; 4 - глоба; 5 - лихва; 6 - възстановяване при доброволно прекратяване; 7 - възстановяване при грешка; 8 - възстановяване при нередност; 9 - банкова гаранция; 10 - касов трансфер.'
exec spDescColumn N'SapPaidAmounts', N'AccDate'                  , N'Дата.'
exec spDescColumn N'SapPaidAmounts', N'BankDate'                 , N'Дата.'
exec spDescColumn N'SapPaidAmounts', N'SapDate'                  , N'Дата.'
exec spDescColumn N'SapPaidAmounts', N'Comment'                  , N'Коментар.'

exec spDescColumn N'SapPaidAmounts', N'HasWarning'               , N'Маркер за възникнало предупреждение при импорта.'
exec spDescColumn N'SapPaidAmounts', N'Warnings'                 , N'Предупреждения.'

exec spDescColumn N'SapPaidAmounts', N'HasError'                 , N'Маркер за възникване на грешка при импорта.'
exec spDescColumn N'SapPaidAmounts', N'Errors'                   , N'Грешки.'
GO
