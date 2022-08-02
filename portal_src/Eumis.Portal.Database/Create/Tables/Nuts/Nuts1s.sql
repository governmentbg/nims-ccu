PRINT 'Nuts1s'
GO

CREATE TABLE [dbo].[Nuts1s] (
    [Nuts1Id]               INT             NOT NULL IDENTITY,
    [CountryId]             INT             NOT NULL,
    [NutsCode]              NVARCHAR(200)   NOT NULL,
    [Name]                  NVARCHAR(MAX)   NOT NULL,
	[FullPathName]          NVARCHAR(1000)  NOT NULL,
	[FullPath]              NVARCHAR(500)   NOT NULL,
    [NameAlt]               NVARCHAR(200)   NULL,
    [FullPathNameAlt]       NVARCHAR(1000)  NULL,
    CONSTRAINT [PK_Nuts1s]           PRIMARY KEY ([Nuts1Id]),
    CONSTRAINT [FK_Nuts1s_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId]),
);
GO

exec spDescTable  N'Nuts1s', N'Номенклатура NUTS1 (Nomenclature of Territorial Units for Statistics) код на регион (ниво 1).'
exec spDescColumn N'Nuts1s', N'Nuts1Id'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Nuts1s', N'CountryId'           , N'Идентификатор на държава.'
exec spDescColumn N'Nuts1s', N'NutsCode'            , N'Код по NUTS1.'
exec spDescColumn N'Nuts1s', N'Name'                , N'Наименование.'
exec spDescColumn N'Nuts1s', N'FullPathName'        , N'Пълно наименование.'
exec spDescColumn N'Nuts1s', N'FullPath'            , N'Всички кодове.'
exec spDescColumn N'Nuts1s', N'NameAlt'             , N'Наименование на английски.'
exec spDescColumn N'Nuts1s', N'FullPathNameAlt'     , N'Пълно наименование на английски.'
GO
