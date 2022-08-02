-- bf46fe2 Add upper bound to the number of times an email can be sent

GO
ALTER TABLE [dbo].[Emails] ADD [FailedAttempts] INT NOT NULL DEFAULT(0);
