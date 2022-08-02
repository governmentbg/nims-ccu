PRINT 'Countries'
GO

CREATE TABLE [dbo].[Countries] (
    [CountryId]     INT   NOT NULL IDENTITY,
    [NutsCode]      NVARCHAR(200)   NOT NULL,
    [Name]          NVARCHAR(MAX)   NOT NULL,
	[NameAlt]       NVARCHAR(200)   NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([CountryId])
);
GO

exec spDescTable  N'Countries', N'Номенклатура държави NUTS0.'
exec spDescColumn N'Countries', N'CountryId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Countries', N'NutsCode'         , N'Код по NUTS0.'
exec spDescColumn N'Countries', N'Name'             , N'Наименование.'
exec spDescColumn N'Countries', N'NameAlt'          , N'Наименование на английски.'
