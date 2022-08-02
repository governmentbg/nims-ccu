GO
ALTER TABLE [dbo].[Nuts1s] ADD [NameAlt] NVARCHAR(200) NULL;
ALTER TABLE [dbo].[Nuts1s] ADD [FullPathNameAlt] NVARCHAR(1000) NULL;
GO

UPDATE [Nuts1s] SET  
    NameAlt = ListData.NameAlt
FROM (VALUES
	('BGZ', 'Extra-Regio NUTS 1'),
	('BG3', 'North and South East Bulgaria'),
	('BG4', 'South-West and South Central Bulgaria')) AS ListData(NutsCode, NameAlt) 
WHERE 
    ListData.NutsCode = Nuts1s.NutsCode
GO
