CREATE TABLE [dbo].[MapRegions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MapId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL,
	[NameAlt] NVARCHAR(50) NOT NULL,
	[NameAltEnglish] NVARCHAR(50) NOT NULL,    
    [Path] VARCHAR(MAX) NULL, 
	[NutsLevel] INT NOT NULL,
	[RegionId] INT NULL
)
