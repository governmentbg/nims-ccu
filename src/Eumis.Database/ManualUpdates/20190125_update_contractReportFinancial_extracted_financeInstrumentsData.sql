--executed on 2019/01/25 ~ 11:50

DECLARE @input TABLE (ContractRegNum NVARCHAR(MAX), PaymentNum INT);
INSERT INTO @input
    (ContractRegNum, PaymentNum)
VALUES
    ('BG05M2OP001-3.001-0031-C03', 3),
	('BG05M2OP001-3.001-0064-C02', 3),
	('BG05M2OP001-3.001-0087-C03', 3),
	('BG05M2OP001-3.001-0087-C03', 4),
	('BG05M2OP001-3.002-0340-C02', 2);


WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET
    PaymentsFinalRecipientsAmount = COALESCE(crf.Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:PaymentsFinalRecipientsAmount/text())[1]', 'MONEY'), 0),
    CommitmentsGuaranteeAmount = COALESCE(crf.Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:CommitmentsGuaranteeAmount/text())[1]', 'MONEY'), 0),
    ExpenseManagementAmount = COALESCE(crf.Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ExpenseManagementAmount/text())[1]', 'MONEY'), 0),
    ManagementFeesAmount = COALESCE(crf.Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ManagementFeesAmount/text())[1]', 'MONEY'), 0),
    CorrespondingPublicSpendingAmount = COALESCE(crf.Xml.value('(/FinanceReport/R10043:FinanceInstrumentsReport/R10082:CorrespondingPublicSpendingAmount/text())[1]', 'MONEY'), 0)
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;
GO
