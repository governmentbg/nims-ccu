update ContractReportFinancialCorrectionCSDs
set Sign = Sign * -1
where CertReportId = 14

update ContractReportFinancialCorrectionCSDs
set Sign = Sign * -1
where CertReportId = 45

update ContractReportFinancialCorrectionCSDs
set Sign = Sign * -1
where CertReportId = 101 and ContractReportFinancialCorrectionId in (167, 168, 169, 170)
