PRINT 'ContractDebtPayments'
GO

CREATE TABLE [dbo].[ContractDebtPayments] (
    [ContractDebtPaymentId]         INT             NOT NULL IDENTITY,
    [ContractDebtId]                INT             NOT NULL,
    [ContractReportPaymentId]       INT             NOT NULL,

    CONSTRAINT [PK_ContractDebtPayments]                         PRIMARY KEY ([ContractDebtPaymentId]),
    CONSTRAINT [FK_ContractDebtPayments_ContractDebts]    FOREIGN KEY ([ContractDebtId])       REFERENCES [dbo].[ContractDebts] ([ContractDebtId]),
    CONSTRAINT [FK_ContractDebtPayments_ContractReportPayments]  FOREIGN KEY ([ContractReportPaymentId])     REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId])
);
GO

exec spDescTable  N'ContractDebtPayments', N'Искания за плащане към дълг.'
exec spDescColumn N'ContractDebtPayments', N'ContractDebtPaymentId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractDebtPayments', N'ContractDebtId'              , N'Идентификатор на дълг.'
exec spDescColumn N'ContractDebtPayments', N'ContractReportPaymentId'     , N'Идентификатор на искане за плащане.'
GO
