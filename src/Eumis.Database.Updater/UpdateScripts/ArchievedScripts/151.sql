GO

IF EXISTS (SELECT [ContractDebtId] FROM [dbo].[ContractDebts])
BEGIN
    THROW 50000,'Cannot update database. There must be no ContractDebts',1;
END
GO

DROP TABLE [dbo].[ContractDebtVersionPayments]
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
