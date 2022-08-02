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

    CONSTRAINT [PK_ContractReportPaymentCheckAmounts]                             PRIMARY KEY ([ContractReportPaymentCheckAmountId]),
    CONSTRAINT [FK_ContractReportPaymentCheckAmounts_ContractReportPaymentChecks] FOREIGN KEY ([ContractReportPaymentCheckId]) REFERENCES [dbo].[ContractReportPaymentChecks] ([ContractReportPaymentCheckId]),
    CONSTRAINT [FK_ContractReportPaymentCheckAmounts_ContractReportPayments]      FOREIGN KEY ([ContractReportPaymentId])      REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [FK_ContractReportPaymentCheckAmounts_ContractReports]             FOREIGN KEY ([ContractReportId])             REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportPaymentCheckAmounts_Contracts]                   FOREIGN KEY ([ContractId])                   REFERENCES [dbo].[Contracts] ([ContractId])
);
GO

INSERT INTO [dbo].[ContractReportPaymentCheckAmounts] (
    [ContractReportPaymentCheckId],
    [ContractReportPaymentId],
    [ContractReportId],
    [ContractId],
    [Gid],
    [ProgrammePriorityId],
    [FinanceSource],
    [ApprovedEuAmount],
    [ApprovedBgAmount],
    [ApprovedBfpTotalAmount],
    [ApprovedCrossAmount],
    [ApprovedSelfAmount],
    [ApprovedTotalAmount],
    [PaidEuAmount],
    [PaidBgAmount],
    [PaidBfpTotalAmount],
    [PaidCrossAmount])
SELECT
    crpc.[ContractReportPaymentCheckId],
    crp.[ContractReportPaymentId],
    cr.[ContractReportId],
    cr.[ContractId],
    NEWID(),
    ps.[ProgrammePriorityId],
    ps.[FinanceSource],
    calc.[ApprovedEuAmount],
    calc.[ApprovedBgAmount],
    calc.[ApprovedBfpTotalAmount],
    calc.[ApprovedCrossAmount],
    calc.[ApprovedSelfAmount],
    calc.[ApprovedTotalAmount],
    crpc.[PaidEuAmount],
    crpc.[PaidBgAmount],
    crpc.[PaidBfpTotalAmount],
    crpc.[PaidCrossAmount]
  FROM [dbo].[Contracts] c
  JOIN [dbo].[ProcedureShares] ps on c.[ProcedureId] = ps.[ProcedureId]
  JOIN [dbo].[ContractReports] cr on c.[ContractId] = cr.[ContractId]
  JOIN [dbo].[ContractReportPayments] crp on cr.[ContractReportId] = crp.[ContractReportId]
  JOIN [dbo].[ContractReportPaymentChecks] crpc on crp.[ContractReportPaymentId] = crpc.[ContractReportPaymentId]
  JOIN (
    select crbi.ContractReportId      
      ,SUM(case when [AdvancePayment] = 2 then [ApprovedEuAmount] else 0 end) as [ApprovedEuAmount]
      ,SUM(case when [AdvancePayment] = 2 then [ApprovedBgAmount] else 0 end) as [ApprovedBgAmount]
      ,SUM(case when [AdvancePayment] = 2 then [ApprovedBfpTotalAmount] else 0 end) as [ApprovedBfpTotalAmount]
      ,SUM(case when [AdvancePayment] = 2 and [CrossFinancing] = 1 then [ApprovedBfpTotalAmount] else 0 end) as [ApprovedCrossAmount]
      ,SUM(case when [AdvancePayment] = 2 then [ApprovedSelfAmount] else 0 end) as [ApprovedSelfAmount]
      ,SUM(case when [AdvancePayment] = 2 then [ApprovedTotalAmount] else 0 end) as [ApprovedTotalAmount]
    from [dbo].[ContractReportFinancialCSDBudgetItems] crbi
    group by crbi.[ContractReportId]
    ) calc on calc.[ContractReportId] = crp.[ContractReportId]
  WHERE cr.[ReportType] != 1 and
        cr.[Status] = 5 and
        crp.[PaymentType] != 1 and
        c.[ProcedureId] in (
            SELECT [ProcedureId]
              FROM [dbo].[ProcedureShares]
              GROUP BY [ProcedureId]
              HAVING COUNT(*) = 1)
GO

INSERT INTO [dbo].[ContractReportPaymentCheckAmounts] (
    [ContractReportPaymentCheckId],
    [ContractReportPaymentId],
    [ContractReportId],
    [ContractId],
    [Gid],
    [ProgrammePriorityId],
    [FinanceSource],
    [ApprovedEuAmount],
    [ApprovedBgAmount],
    [ApprovedBfpTotalAmount],
    [ApprovedCrossAmount],
    [ApprovedSelfAmount],
    [ApprovedTotalAmount],
    [PaidEuAmount],
    [PaidBgAmount],
    [PaidBfpTotalAmount],
    [PaidCrossAmount])
SELECT
    crpc.[ContractReportPaymentCheckId],
    crp.[ContractReportPaymentId],
    cr.[ContractReportId],
    cr.[ContractId],
    NEWID(),
    ps.[ProgrammePriorityId],
    ps.[FinanceSource],
    0,
    0,
    0,
    0,
    0,
    0,
    crpc.[PaidEuAmount],
    crpc.[PaidBgAmount],
    crpc.[PaidBfpTotalAmount],
    crpc.[PaidCrossAmount]
  FROM [dbo].[Contracts] c
  JOIN [dbo].[ProcedureShares] ps on c.[ProcedureId] = ps.[ProcedureId]
  JOIN [dbo].[ContractReports] cr on c.[ContractId] = cr.[ContractId]
  JOIN [dbo].[ContractReportPayments] crp on cr.[ContractReportId] = crp.[ContractReportId]
  JOIN [dbo].[ContractReportPaymentChecks]  crpc on crp.[ContractReportPaymentId]  = crpc.[ContractReportPaymentId]
  WHERE cr.[ReportType] = 1 and
        cr.[Status] not in (1,2,3) and
        crp.[PaymentType] = 1 and
        crp.[Status] = 3 and
        c.[ProcedureId] in (
            SELECT [ProcedureId]
              FROM [dbo].[ProcedureShares]
              GROUP BY [ProcedureId]
              HAVING COUNT(*) = 1)
GO

ALTER TABLE [dbo].[ContractReportPaymentChecks] DROP COLUMN
    [ApprovedEuAmount],
    [ApprovedBgAmount],
    [ApprovedBfpTotalAmount],
    [ApprovedCrossAmount],
    [ApprovedSelfAmount],
    [ApprovedTotalAmount],

    [PaidEuAmount],
    [PaidBgAmount],
    [PaidBfpTotalAmount],
    [PaidCrossAmount]
GO