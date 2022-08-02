ALTER TABLE [dbo].[ContractReports]
    ALTER COLUMN [DateFrom] DATETIME2 NULL
GO

ALTER TABLE [dbo].[ContractReports]
    ALTER COLUMN [DateTo] DATETIME2 NULL
GO

ALTER TABLE [dbo].[ContractReportFinancials]
    ADD [SubmitDate] DATETIME2 NULL
GO

UPDATE f
SET    f.SubmitDate = cr.SubmitDate
FROM   [dbo].[ContractReportFinancials] AS f
       INNER JOIN [dbo].[ContractReports] AS cr
               ON f.ContractReportId = cr.ContractReportId
WHERE  f.VersionSubNum = 1
GO
