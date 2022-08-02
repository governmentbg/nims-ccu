GO

ALTER TABLE [CompanyLegalTypes]
ADD [Alias]             NVARCHAR(200)       NULL;
GO

UPDATE [CompanyLegalTypes]
SET [Alias] = N'person'
WHERE [CompanyLegalTypeId] = 31;
GO