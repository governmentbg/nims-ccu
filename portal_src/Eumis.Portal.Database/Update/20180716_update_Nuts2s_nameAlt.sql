GO
ALTER TABLE [dbo].[Nuts2s] ADD [NameAlt] NVARCHAR(200) NULL;
ALTER TABLE [dbo].[Nuts2s] ADD [FullPathNameAlt] NVARCHAR(1000) NULL;
GO

UPDATE [Nuts2s] SET  
    NameAlt = ListData.NameAlt
FROM (VALUES
	('BGZZ', 'Extra-Regio NUTS 2'),
	('BG32', 'North Central'),
	('BG33', 'North-East'),
	('BG31', 'North-West'),
	('BG42', 'South Central'),
	('BG34', 'South-East'),
	('BG41', 'South-West')) AS ListData(NutsCode, NameAlt) 
WHERE 
    ListData.NutsCode = Nuts2s.NutsCode
GO
