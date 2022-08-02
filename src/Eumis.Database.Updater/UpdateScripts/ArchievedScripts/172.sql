CREATE TABLE [dbo].[ReimbursedAmountPayments] (
    [ReimbursedAmountPaymentId]         INT             NOT NULL IDENTITY,
    [ReimbursedAmountId]                INT             NOT NULL,
    [ContractReportPaymentId]           INT             NOT NULL,

    CONSTRAINT [PK_ReimbursedAmountPayments]                         PRIMARY KEY ([ReimbursedAmountPaymentId]),
    CONSTRAINT [FK_ReimbursedAmountPayments_ReimbursedAmounts]       FOREIGN KEY ([ReimbursedAmountId])          REFERENCES [dbo].[ReimbursedAmounts] ([ReimbursedAmountId]),
    CONSTRAINT [FK_ReimbursedAmountPayments_ContractReportPayments]  FOREIGN KEY ([ContractReportPaymentId])     REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId])
);
GO