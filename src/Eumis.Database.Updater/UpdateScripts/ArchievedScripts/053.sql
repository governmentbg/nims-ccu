-- 37511f7 Add login failed attempts

ALTER TABLE [dbo].[Users]
ADD [FailedAttempts]        INT                 NOT NULL DEFAULT 0
GO
