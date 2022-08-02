--executed on 2018/07/19 ~ 14:50

DECLARE @input TABLE (ContractRegNum NVARCHAR(MAX), PaymentNum INT);
INSERT INTO @input
    (ContractRegNum, PaymentNum)
VALUES
    ('BG05FMOP001-3.002-0008-C03',	2),
	('BG05FMOP001-3.002-0008-C03',	3),
	('BG05FMOP001-3.002-0008-C03',	4),
	('BG05FMOP001-3.002-0008-C03',	5),
	('BG05FMOP001-3.002-0008-C03',	7),
	('BG05FMOP001-3.002-0008-C03',	8),
	('BG05FMOP001-3.002-0087-C05',	2),
	('BG05FMOP001-3.002-0155-C03',	2),
	('BG05FMOP001-3.002-0155-C03',	4),
	('BG05M9OP001-1.019-0067-C01',	1),
	('BG05M9OP001-1.019-0079-C01',	1),
	('BG05M9OP001-2.005-0029-C01',	3),
	('BG05M9OP001-1.019-0020-C01',	1),
	('BG05M9OP001-1.019-0045-C01',	1),
	('BG05M9OP001-1.019-0047-C01',	1),
	('BG05M9OP001-1.019-0057-C02',	1),
	('BG16M1OP001-4.001-0001-C01',	3),
	('BG16M1OP001-4.001-0001-C01',	4),
	('BG16M1OP001-4.001-0001-C01',	5),
	('BG16M1OP001-5.001-0004-C01',	2);

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
