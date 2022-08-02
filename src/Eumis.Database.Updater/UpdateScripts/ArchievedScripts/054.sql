-- 6f95047 Add failedAttemptTimeout to EmailJob

ALTER TABLE [dbo].[Emails]
ADD [FailedAttemptsErrors]  NVARCHAR(MAX)     NULL
GO
