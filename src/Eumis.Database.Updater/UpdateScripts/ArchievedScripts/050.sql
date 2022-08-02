-- ac56a90 Fix CompanyLegalTypes insert script
GO

UPDATE [CompanyLegalTypes] 
SET [Name] = N'Изпълнителна агенция / административна структура, създадена с нормативен акт'
WHERE [CompanyTypeId] = 3 and [CompanyLegalTypeId] = 4
GO
