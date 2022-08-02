GO

ALTER TABLE ContractReportMicros
ADD SenderContractRegistrationId INT NULL;
GO

ALTER TABLE ContractReportFinancials
ADD SenderContractRegistrationId INT NULL;
GO

ALTER TABLE ContractReportTechnicals
ADD SenderContractRegistrationId INT NULL;
GO

ALTER TABLE ContractReportPayments
ADD SenderContractRegistrationId INT NULL;
GO