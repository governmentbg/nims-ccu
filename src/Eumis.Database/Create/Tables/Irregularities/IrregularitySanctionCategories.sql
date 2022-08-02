PRINT 'IrregularitySanctionCategories'
GO

CREATE TABLE [dbo].[IrregularitySanctionCategories] (
    [IrregularitySanctionCategoryId]  INT                 NOT NULL IDENTITY,
    [Name]                            NVARCHAR(MAX)       NOT NULL,
    [Code]                            NVARCHAR(200)       NULL,

    CONSTRAINT [PK_IrregularitySanctionCategories]   PRIMARY KEY ([IrregularitySanctionCategoryId])
);
GO

exec spDescTable  N'IrregularitySanctionCategories', N'Категории на санкции към нередности.'
exec spDescColumn N'IrregularitySanctionCategories', N'IrregularitySanctionCategoryId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularitySanctionCategories', N'Name'                          , N'Наименование.'
exec spDescColumn N'IrregularitySanctionCategories', N'Code'                          , N'Код.'
GO
