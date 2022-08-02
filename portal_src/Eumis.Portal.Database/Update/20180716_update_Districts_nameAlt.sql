GO
ALTER TABLE [dbo].[Districts] ADD [NameAlt] NVARCHAR(200) NULL;
ALTER TABLE [dbo].[Districts] ADD [FullPathNameAlt] NVARCHAR(1000) NULL;
GO

UPDATE [Districts] SET  
    NameAlt = ListData.NameAlt
FROM (VALUES
	('BG331', 'Varna'), 
	('BG321', 'Veliko Tarnovo'),
	('BG332', 'Dobrich'),
	('BG315', 'Lovech'),
	('BG411', 'Sofia cap.'),
	('BG412', 'Sofia'),
	('BG413', 'Blagoevgrad'),
	('BG341', 'Burgas'),
	('BGZZZ', 'Extra-Regio NUTS 3'),
	('BG322', 'Gabrovo'),
	('BG422', 'Haskovo'),
	('BG425', 'Kardzhali'),
	('BG415', 'Kyustendil'),
	('BG312', 'Montana'),
	('BG423', 'Pazardzhik'),
	('BG414', 'Pernik'),
	('BG314', 'Pleven'),
	('BG421', 'Plovdiv'),
	('BG324', 'Razgrad'),
	('BG323', 'Ruse'),
	('BG333', 'Shumen'),
	('BG325', 'Silistra'),
	('BG342', 'Sliven'),
	('BG424', 'Smolyan'),
	('BG344', 'Stara Zagora'),
	('BG334', 'Targovishte'),
	('BG311', 'Vidin'),
	('BG313', 'Vratsa'),
	('BG343', 'Yambol')) AS ListData(NutsCode, NameAlt) 
WHERE 
    ListData.NutsCode = Districts.NutsCode
GO
