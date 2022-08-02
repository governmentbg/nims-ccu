GO

ALTER TABLE [dbo].[CertReports] ADD [ApprovalDate]      DATETIME2      NULL
GO

UPDATE [dbo].[CertReports] SET
  [ApprovalDate] = [ModifyDate]
WHERE [Status] IN (4, 5)
GO