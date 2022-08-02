PRINT 'Nuts2s'
GO

CREATE TABLE [dbo].[Nuts2s] (
    [Nuts2Id]           INT             NOT NULL IDENTITY,
    [Nuts1Id]           INT             NOT NULL,
    [NutsCode]          NVARCHAR(200)   NOT NULL,
    [Name]              NVARCHAR(MAX)   NOT NULL,
	[FullPathName]      NVARCHAR(1000)  NOT NULL,
	[NameAlt]           NVARCHAR(200)   NULL,
	[FullPathNameAlt]   NVARCHAR(1000)  NULL,
	[FullPath]          NVARCHAR(500)   NOT NULL,
    CONSTRAINT [PK_Nuts2s]        PRIMARY KEY ([Nuts2Id]),
    CONSTRAINT [FK_Nuts2s_Nuts1s] FOREIGN KEY ([Nuts1Id]) REFERENCES [dbo].[Nuts1s] ([Nuts1Id]),
);
GO

exec spDescTable  N'Nuts2s', N'Номенклатура NUTS2 (Nomenclature of Territorial Units for Statistics) код на регион (ниво 2).'
exec spDescColumn N'Nuts2s', N'Nuts2Id'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Nuts2s', N'Nuts1Id'         , N'Идентификатор на NUTS1.'
exec spDescColumn N'Nuts2s', N'NutsCode'        , N'Код по NUTS2.'
exec spDescColumn N'Nuts2s', N'Name'            , N'Наименование.'
exec spDescColumn N'Nuts2s', N'FullPathName'    , N'Пълно наименование.'
exec spDescColumn N'Nuts2s', N'NameAlt'         , N'Наименование на друг език.'
exec spDescColumn N'Nuts2s', N'FullPathNameAlt' , N'Пълно наименование на друг език.'
exec spDescColumn N'Nuts2s', N'FullPath'        , N'Всички кодове.'
GO
