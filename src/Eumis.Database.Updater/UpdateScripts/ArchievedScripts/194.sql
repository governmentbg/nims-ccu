GO
ALTER TABLE [dbo].[ContractReportMicros] ADD [Source] INT NOT NULL CONSTRAINT DEFAULT_Source DEFAULT 0;

ALTER TABLE [dbo].[ContractReportMicros] DROP CONSTRAINT DEFAULT_Source;

GO
UPDATE ContractReportMicros 
  SET Source = cr.Source
FROM
  [dbo].[ContractReports] cr
WHERE 
  ContractReportMicros.ContractReportId = cr.ContractReportId;

GO
ALTER TABLE [dbo].[ContractReportMicros] ADD CONSTRAINT [CHK_ContractReportMicros_Source] CHECK ([Source]   IN (1, 2));
GO
