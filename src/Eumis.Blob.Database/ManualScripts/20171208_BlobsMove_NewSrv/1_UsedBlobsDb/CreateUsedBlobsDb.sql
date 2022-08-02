USE [master]
GO

print 'Create database UsedBlobContents'
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'UsedBlobContents')
BEGIN
    ALTER DATABASE [UsedBlobContents] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE [UsedBlobContents]
END
GO

CREATE DATABASE [UsedBlobContents] COLLATE Cyrillic_General_CI_AS
GO

ALTER DATABASE [UsedBlobContents]
SET ALLOW_SNAPSHOT_ISOLATION ON

ALTER DATABASE [UsedBlobContents]
SET READ_COMMITTED_SNAPSHOT ON
GO

USE [UsedBlobContents]
GO

CREATE TABLE UniqueUsedBlobContents ([BlobContentId] INT PRIMARY KEY)
GO
