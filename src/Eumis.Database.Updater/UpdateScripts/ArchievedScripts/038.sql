-- 4574001 Add newPassword page; Fix user activated and updated events;
GO

ALTER TABLE [Users]
ADD [NewPasswordCode]       NVARCHAR (50)            NULL;
GO
