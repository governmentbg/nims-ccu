-- 90de06b Add Emails.Status check constraint

GO
ALTER TABLE [dbo].[Emails] ADD CONSTRAINT [CHK_Emails_Status]  CHECK ([Status] IN (1, 2, 3));
