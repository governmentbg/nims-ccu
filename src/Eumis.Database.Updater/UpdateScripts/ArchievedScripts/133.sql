GO

IF EXISTS (SELECT [ContractReportRevalidationId] FROM [dbo].[ContractReportRevalidations])
BEGIN
    THROW 50000,'Cannot update database. There must be no ContractReportRevalidations',1;
END
GO

IF EXISTS (SELECT [ContractReportCorrectionId] FROM [dbo].[ContractReportCorrections])
BEGIN
    THROW 50000,'Cannot update database. There must be no ContractReportCorrections',1;
END
GO

IF EXISTS (SELECT [ContractReportCertCorrectionId] FROM [dbo].[ContractReportCertCorrections])
BEGIN
    THROW 50000,'Cannot update database. There must be no ContractReportCertCorrections',1;
END
GO

ALTER TABLE [dbo].[ContractReportRevalidations] ALTER COLUMN [FinanceSource] INT NOT NULL;
ALTER TABLE [dbo].[ContractReportCorrections] ALTER COLUMN [FinanceSource] INT NOT NULL;
ALTER TABLE [dbo].[ContractReportCertCorrections] ALTER COLUMN [FinanceSource] INT NOT NULL;
GO
