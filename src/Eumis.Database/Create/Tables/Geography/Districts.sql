PRINT 'Districts'
GO

CREATE TABLE [dbo].[Districts] (
    [DistrictId]            INT             NOT NULL IDENTITY,
    [Nuts2Id]               INT             NOT NULL,
    [NutsCode]              NVARCHAR(200)   NOT NULL,
    [Name]                  NVARCHAR(200)   NOT NULL,
	[FullPathName]          NVARCHAR(1000)  NOT NULL,
	[NameAlt]               NVARCHAR(200)   NULL,
	[FullPathNameAlt]       NVARCHAR(1000)  NULL,
	[FullPath]              NVARCHAR(500)   NOT NULL,
    CONSTRAINT [PK_Districts]        PRIMARY KEY ([DistrictId]),
    CONSTRAINT [FK_Districts_Nuts2s] FOREIGN KEY ([Nuts2Id]) REFERENCES [dbo].[Nuts2s] ([Nuts2Id]),
)
GO

exec spDescTable  N'Districts', N'Номенклатура области NUTS3 (Nomenclature of Territorial Units for Statistics) код на регион (ниво 3)'
exec spDescColumn N'Districts', N'DistrictId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Districts', N'Nuts2Id'              , N'Идентификатор на NUTS2.'
exec spDescColumn N'Districts', N'NutsCode'             , N'Код по NUTS3.'
exec spDescColumn N'Districts', N'Name'                 , N'Наименование.'
exec spDescColumn N'Districts', N'FullPathName'         , N'Пълно наименование.'
exec spDescColumn N'Districts', N'NameAlt'              , N'Наименование на друг език.'
exec spDescColumn N'Districts', N'FullPathNameAlt'      , N'Пълно наименование на друг език.'
exec spDescColumn N'Districts', N'FullPath'             , N'Всички кодове.'
GO
