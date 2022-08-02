-- 2096c69 Fix texts in user interface; Fix small bugs
GO

UPDATE [dbo].[InstitutionTypes]
SET
    Name = N'Одитен орган'
WHERE InstitutionTypeId = 2

GO
