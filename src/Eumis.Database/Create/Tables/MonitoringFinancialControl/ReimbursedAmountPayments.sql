PRINT 'ReimbursedAmountPayments'
GO

CREATE TABLE [dbo].[ReimbursedAmountPayments] (
    [ReimbursedAmountPaymentId]         INT             NOT NULL IDENTITY,
    [ReimbursedAmountId]                INT             NOT NULL,
    [ContractReportPaymentId]           INT             NOT NULL,

    CONSTRAINT [PK_ReimbursedAmountPayments]                         PRIMARY KEY ([ReimbursedAmountPaymentId]),
    CONSTRAINT [FK_ReimbursedAmountPayments_ReimbursedAmounts]       FOREIGN KEY ([ReimbursedAmountId])          REFERENCES [dbo].[ReimbursedAmounts] ([ReimbursedAmountId]),
    CONSTRAINT [FK_ReimbursedAmountPayments_ContractReportPayments]  FOREIGN KEY ([ContractReportPaymentId])     REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId])
);
GO

exec spDescTable  N'ReimbursedAmountPayments', N'Искания за плащане към възстановена сума.'
exec spDescColumn N'ReimbursedAmountPayments', N'ReimbursedAmountPaymentId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ReimbursedAmountPayments', N'ReimbursedAmountId'              , N'Идентификатор на възстановена сума.'
exec spDescColumn N'ReimbursedAmountPayments', N'ContractReportPaymentId'         , N'Идентификатор на искане за плащане.'
GO
