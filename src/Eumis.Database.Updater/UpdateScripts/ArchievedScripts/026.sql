GO

ALTER TABLE [Users]
ADD [Uin]                   NVARCHAR (50)       NOT NULL DEFAULT N'empty';
GO

ALTER TABLE [RegDataRequests]
ADD [Uin]                   NVARCHAR (50)       NOT NULL DEFAULT N'empty';
GO
