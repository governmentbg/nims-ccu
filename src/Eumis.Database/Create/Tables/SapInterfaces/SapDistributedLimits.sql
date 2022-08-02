PRINT 'SapDistributedLimits'
GO

CREATE TABLE [dbo].[SapDistributedLimits] (
    [SapDistributedLimitId]     INT              NOT NULL IDENTITY,
    [SapFileId]                 INT              NOT NULL,
    [IsImported]                BIT              NOT NULL,
    [ActuallyPaidAmountId]      INT              NULL,

    [ProgrammeId]               INT              NULL,
    [ProgrammePriorityId]       INT              NULL,
    [ContractSapNum]            NVARCHAR(50)     NULL,
    [ContractId]                INT              NULL,
    [Fund]                      INT              NULL,
    [ContractReportPaymentNum]  NVARCHAR(50)     NULL,
    [ContractReportPaymentId]   INT              NULL,
    [ContractReportPaymentDate] DATETIME2        NULL,
    [PaidBfpBgAmount]           MONEY            NOT NULL,
    [PaidBfpEuAmount]           MONEY            NOT NULL,
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

    CONSTRAINT [PK_SapDistributedLimits]                        PRIMARY KEY ([SapDistributedLimitId]),
    CONSTRAINT [FK_SapDistributedLimits_SapFiles]               FOREIGN KEY ([SapFileId])               REFERENCES [dbo].[SapFiles]               ([SapFileId]),
    CONSTRAINT [FK_SapDistributedLimits_ActuallyPaidAmounts]    FOREIGN KEY ([ActuallyPaidAmountId])    REFERENCES [dbo].[ActuallyPaidAmounts]    ([ActuallyPaidAmountId]),
    CONSTRAINT [FK_SapDistributedLimits_Programmes]             FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_SapDistributedLimits_ProgrammePriorities]    FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_SapDistributedLimits_Contracts]              FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]              ([ContractId]),
    CONSTRAINT [FK_SapDistributedLimits_ContractReportPayments] FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [CHK_SapDistributedLimits_Fund]                  CHECK       ([Fund]          IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_SapDistributedLimits_Currency]              CHECK       ([Currency]      IN (1)),
    CONSTRAINT [CHK_SapDistributedLimits_PaymentType]           CHECK       ([PaymentType]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

exec spDescTable  N'SapDistributedLimits', N'Разпределени лимити от SAP.'
exec spDescColumn N'SapDistributedLimits', N'SapDistributedLimitId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SapDistributedLimits', N'SapFileId'                , N'Идентификатор на SAP файл.'
exec spDescColumn N'SapDistributedLimits', N'IsImported'               , N'Маркер дали документът е бил импортиран.'
exec spDescColumn N'SapDistributedLimits', N'ActuallyPaidAmountId'     , N'Идентификатор на реално изплатена сума.'

exec spDescColumn N'SapDistributedLimits', N'ProgrammeId'              , N'Идентификатор на програма.'
exec spDescColumn N'SapDistributedLimits', N'ProgrammePriorityId'      , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'SapDistributedLimits', N'ContractSapNum'           , N'Номер на договор, посочен във файла.'
exec spDescColumn N'SapDistributedLimits', N'ContractId'               , N'Идентификатор на договор.'
exec spDescColumn N'SapDistributedLimits', N'Fund'                     , N'Фонд.'
exec spDescColumn N'SapDistributedLimits', N'ContractReportPaymentNum' , N'Номер на искане за плащане.'
exec spDescColumn N'SapDistributedLimits', N'ContractReportPaymentId'  , N'Идентификатор на искане за плащане.'
exec spDescColumn N'SapDistributedLimits', N'ContractReportPaymentDate', N'Дата на искането за плащане.'
exec spDescColumn N'SapDistributedLimits', N'PaidBfpBgAmount'          , N'Изплатена сума Финансиране от НФ.'
exec spDescColumn N'SapDistributedLimits', N'PaidBfpEuAmount'          , N'Изплатена сума Финансиране от ЕС.'
exec spDescColumn N'SapDistributedLimits', N'Currency'                 , N'Валута на плащането.'
exec spDescColumn N'SapDistributedLimits', N'PaymentType'              , N'Вид: 1 - авансово; 2 - междинно; 3 - окончателно; 4 - глоба; 5 - лихва; 6 - възстановяване при доброволно прекратяване; 7 - възстановяване при грешка; 8 - възстановяване при нередност; 9 - банкова гаранция; 10 - касов трансфер.'
exec spDescColumn N'SapDistributedLimits', N'AccDate'                  , N'Дата.'
exec spDescColumn N'SapDistributedLimits', N'BankDate'                 , N'Дата.'
exec spDescColumn N'SapDistributedLimits', N'SapDate'                  , N'Дата.'
exec spDescColumn N'SapDistributedLimits', N'Comment'                  , N'Коментар.'

exec spDescColumn N'SapDistributedLimits', N'HasWarning'               , N'Маркер за възникнало предупреждение при импорта.'
exec spDescColumn N'SapDistributedLimits', N'Warnings'                 , N'Предупреждения.'

exec spDescColumn N'SapDistributedLimits', N'HasError'                 , N'Маркер за възникване на грешка при импорта.'
exec spDescColumn N'SapDistributedLimits', N'Errors'                   , N'Грешки.'
GO
