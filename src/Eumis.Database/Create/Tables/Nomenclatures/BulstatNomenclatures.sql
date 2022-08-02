PRINT 'BulstatNomenclatures'
GO
CREATE TABLE [dbo].[BulstatNomenclatures](
	[Id]                   INT           NOT NULL IDENTITY,
	[Code1]                DECIMAL(5, 0) NOT NULL,
	[Description]          VARCHAR(80)   NULL,
	[Code2]                DECIMAL(5, 0) NULL,
	[Name]                 VARCHAR(80)   NULL,
	[NomenclatureDate]     DATE          NULL,
 CONSTRAINT [PK_SystemNomenclatures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO

exec spDescTable  N'BulstatNomenclatures', N'Булстат номенклатури.'
exec spDescColumn N'BulstatNomenclatures', N'Id'                     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'BulstatNomenclatures', N'Code1'                  , N'Код на номенклатура.'
exec spDescColumn N'BulstatNomenclatures', N'Description'            , N'Описание на номенклатура.'
exec spDescColumn N'BulstatNomenclatures', N'Code2'                  , N'Код на категория.'
exec spDescColumn N'BulstatNomenclatures', N'Name'                   , N'Наименование на категория.'
exec spDescColumn N'BulstatNomenclatures', N'NomenclatureDate'       , N'Дата на създаване.'
GO
