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
    CONSTRAINT [CHK_ContractReportAdvanceNVPaymentAmounts_FinanceSource]                    CHECK       ([FinanceSource]       IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

INSERT INTO [dbo].[ContractReportAdvanceNVPaymentAmounts] (
    [ContractReportPaymentId],
    [ContractReportId],
    [ContractId],
    [Gid],
    [Status],
    [Approval],
    [Notes],
    [CheckedByUserId],
    [CheckedDate],
    [ProgrammePriorityId],
    [FinanceSource],
    [EuAmount],
    [BgAmount],
    [BfpTotalAmount],
    [CreateDate],
    [ModifyDate])
SELECT
    crp.[ContractReportPaymentId], 
    cr.[ContractReportId],
    cr.[ContractId],
    NEWID(),
    2,
    1,
    NULL,
    crpc.[CheckedByUserId],
    crpc.[CheckedDate],
    ps.[ProgrammePriorityId],
    ps.[FinanceSource],
    crpc.[PaidEuAmount],
    crpc.[PaidBgAmount],
    crpc.[PaidBfpTotalAmount],
    GETDATE(),
    GETDATE()
  FROM [dbo].[Contracts] c
  JOIN [dbo].[ProcedureShares] ps on c.[ProcedureId] = ps.[ProcedureId]
  JOIN [dbo].[ContractReports] cr on c.[ContractId] = cr.[ContractId]
  JOIN [dbo].[ContractReportPayments] crp on cr.[ContractReportId] = crp.[ContractReportId]
  JOIN [dbo].[ContractReportPaymentChecks]  crpc on crp.[ContractReportPaymentId]  = crpc.[ContractReportPaymentId]
  WHERE cr.[ReportType] = 1 and
        cr.[Status] = 5 and
        crp.[PaymentType] = 1 and
        crp.[Status] = 3 and 
        crpc.[Status] = 2 and 
        c.[ProcedureId] in (
            SELECT [ProcedureId]
              FROM [dbo].[ProcedureShares]
              GROUP BY [ProcedureId]
              HAVING COUNT(*) = 1)
GO

INSERT INTO [dbo].[ContractReportAdvanceNVPaymentAmounts] (
    [ContractReportPaymentId],
    [ContractReportId],
    [ContractId],
    [Gid],
    [Status],
    [Approval],
    [Notes],
    [CheckedByUserId],
    [CheckedDate],
    [ProgrammePriorityId],
    [FinanceSource],
    [EuAmount],
    [BgAmount],
    [BfpTotalAmount],
    [CreateDate],
    [ModifyDate])
SELECT
    crp.[ContractReportPaymentId],
    cr.[ContractReportId],
    cr.[ContractId],
    NEWID(),
    1,
    NULL,
    NULL,
    NULL,
    NULL,
    ps.[ProgrammePriorityId],
    ps.[FinanceSource],
    0,
    0,
    0,
    GETDATE(),
    GETDATE()
  FROM [dbo].[Contracts] c
  JOIN [dbo].[ProcedureShares] ps on c.[ProcedureId] = ps.[ProcedureId]
  JOIN [dbo].[ContractReports] cr on c.[ContractId] = cr.[ContractId]
  JOIN [dbo].[ContractReportPayments] crp on cr.[ContractReportId] = crp.[ContractReportId]
  WHERE cr.[ReportType] = 1 and
        cr.[Status] not in (1,2,3,5) and
        crp.[PaymentType] = 1 and
        crp.[Status] = 3
GO

DELETE crpc
  FROM [dbo].[Contracts] c
  JOIN [dbo].[ContractReports] cr on c.[ContractId] = cr.[ContractId]
  JOIN [dbo].[ContractReportPayments] crp on cr.[ContractReportId] = crp.[ContractReportId]
  JOIN  [dbo].[ContractReportPaymentChecks]  crpc on crp.[ContractReportPaymentId] = crpc.[ContractReportPaymentId]
  WHERE cr.[ReportType] = 1 and
        cr.[Status] not in (1,2,3,5) and
        crp.[PaymentType] = 1 and
        crp.[Status] = 3
GO