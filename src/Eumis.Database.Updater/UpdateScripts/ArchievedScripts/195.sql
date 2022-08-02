GO

ALTER TABLE [dbo].[ContractReportFinancials] ADD
    [PaymentsFinalRecipientsAmount]         MONEY NOT NULL CONSTRAINT DEFAULT_PaymentsFinalRecipientsAmount     DEFAULT 0,
    [CommitmentsGuaranteeAmount]            MONEY NOT NULL CONSTRAINT DEFAULT_CommitmentsGuaranteeAmount        DEFAULT 0,
    [ExpenseManagementAmount]               MONEY NOT NULL CONSTRAINT DEFAULT_ExpenseManagementAmount           DEFAULT 0,
    [ManagementFeesAmount]                  MONEY NOT NULL CONSTRAINT DEFAULT_ManagementFeesAmount              DEFAULT 0,
    [CorrespondingPublicSpendingAmount]     MONEY NOT NULL CONSTRAINT DEFAULT_CorrespondingPublicSpendingAmount DEFAULT 0;
GO

ALTER TABLE [dbo].[ContractReportFinancials]
DROP
  CONSTRAINT DEFAULT_PaymentsFinalRecipientsAmount,
  CONSTRAINT DEFAULT_CommitmentsGuaranteeAmount,
  CONSTRAINT DEFAULT_ExpenseManagementAmount,
  CONSTRAINT DEFAULT_ManagementFeesAmount,
  CONSTRAINT DEFAULT_CorrespondingPublicSpendingAmount;
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE ContractReportFinancials
SET
    PaymentsFinalRecipientsAmount = COALESCE(Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:PaymentsFinalRecipientsAmount/text())[1]', 'MONEY'), 0),
    CommitmentsGuaranteeAmount = COALESCE(Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:CommitmentsGuaranteeAmount/text())[1]', 'MONEY'), 0),
    ExpenseManagementAmount = COALESCE(Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ExpenseManagementAmount/text())[1]', 'MONEY'), 0),
    ManagementFeesAmount = COALESCE(Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ManagementFeesAmount/text())[1]', 'MONEY'), 0),
    CorrespondingPublicSpendingAmount = COALESCE(Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:CorrespondingPublicSpendingAmount/text())[1]', 'MONEY'), 0);
GO
