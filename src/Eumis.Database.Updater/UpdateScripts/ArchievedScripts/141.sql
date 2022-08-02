GO

IF EXISTS (SELECT [ContractReportAdvancePaymentAmountId] FROM [dbo].[ContractReportAdvancePaymentAmounts])
BEGIN
    THROW 50000,'Cannot update database. There must be no ContractReportAdvancePaymentAmounts',1;
END
GO

ALTER TABLE [dbo].[ContractReportAdvancePaymentAmounts] ADD
    [ProgrammePriorityId]                                   INT               NOT NULL CONSTRAINT DEFAULT_ProgrammePriorityId DEFAULT 0,
    CONSTRAINT [FK_ContractReportAdvancePaymentAmounts_ProgrammePriorities]               FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes] ([MapNodeId])
GO

ALTER TABLE [dbo].[ContractReportAdvancePaymentAmounts]
DROP CONSTRAINT DEFAULT_ProgrammePriorityId
GO