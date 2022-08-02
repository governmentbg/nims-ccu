DECLARE @input TABLE (ContractRegNum NVARCHAR(MAX), PaymentNum INT);
INSERT INTO @input
    (ContractRegNum, PaymentNum)
VALUES
    ('BG16RFOP002-2.001-1035-C01', 1), 
    ('BG16RFOP002-2.001-1235-C01', 1), 
    ('BG16RFOP002-3.001-0122-C01', 1);

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ExpectedLeverageLoanAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ProgressLeverageYearProductAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:InvestmentParticipationPreviousYearsAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:PaymentsFinalRecipientsAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:CommitmentsGuaranteeAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ExpenseManagementAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ManagementFeesAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:CorrespondingPublicSpendingAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:OtherContributionsOutsideESIPublic/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:OtherContributionsOutsideESIPrivate/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:OtherContributionsOutsideESITotal/text())[1] with "0.00"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:OtherContributionsOutsideESIMobilizedFinalRecipientsPublic/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:OtherContributionsOutsideESIMobilizedFinalRecipientsPrivate/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:OtherContributionsOutsideESIMobilizedFinalRecipientsTotal/text())[1] with "0.00"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10043' as R10043,
    N'http://ereg.egov.bg/segment/R-10082' as R10082
)
UPDATE crf
    SET Xml.modify('replace value of (/FinanceReport/R10043:FinanceInstrumentsReport/R10082:ReusableResourcesSizeAmount/text())[1] with "0"')
FROM ContractReports cr
JOIN ContractReportPayments crp ON cr.ContractReportId = crp.ContractReportId
JOIN ContractReportFinancials crf ON cr.ContractReportId = crf.ContractReportId
JOIN Contracts c ON cr.ContractId = c.ContractId
JOIN @input v ON c.RegNumber = v.ContractRegNum AND crp.VersionNum = v.PaymentNum
WHERE crp.Status = 3 AND crf.Status = 3;

