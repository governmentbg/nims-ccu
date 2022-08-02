ALTER TABLE ContractReportMicros
ADD CONSTRAINT [FK_ContractReportMicros_ContractRegistrations]  FOREIGN KEY ([SenderContractRegistrationId])   REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]);
GO

ALTER TABLE ContractReportFinancials
ADD CONSTRAINT [FK_ContractReportFinancials_ContractRegistrations]  FOREIGN KEY ([SenderContractRegistrationId])   REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]);
GO

ALTER TABLE ContractReportTechnicals
ADD CONSTRAINT [FK_ContractReportTechnicals_ContractRegistrations]  FOREIGN KEY ([SenderContractRegistrationId])   REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]);
GO

ALTER TABLE ContractReportPayments
ADD CONSTRAINT [FK_ContractReportPayments_ContractRegistrations]    FOREIGN KEY ([SenderContractRegistrationId])   REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]);
GO
