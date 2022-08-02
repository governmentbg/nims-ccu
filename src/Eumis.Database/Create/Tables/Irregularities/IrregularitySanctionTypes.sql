PRINT 'IrregularitySanctionTypes'
GO

CREATE TABLE [dbo].[IrregularitySanctionTypes] (
    [IrregularitySanctionTypeId]      INT                 NOT NULL IDENTITY,
    [IrregularitySanctionCategoryId]  INT                 NOT NULL,
    [Name]                    NVARCHAR(MAX)       NOT NULL,
    [Code]                    NVARCHAR(200)       NULL,

    CONSTRAINT [PK_IrregularitySanctionTypes]                                 PRIMARY KEY ([IrregularitySanctionTypeId]),
    CONSTRAINT [FK_IrregularitySanctionTypes_IrregularitySanctionCategories]  FOREIGN KEY ([IrregularitySanctionCategoryId]) REFERENCES [dbo].[IrregularitySanctionCategories] ([IrregularitySanctionCategoryId]),
);
GO

exec spDescTable  N'IrregularitySanctionTypes', N'Типове нередности.'
exec spDescColumn N'IrregularitySanctionTypes', N'IrregularitySanctionTypeId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularitySanctionTypes', N'IrregularitySanctionCategoryId', N'Идентификатор на категория нередност.'
exec spDescColumn N'IrregularitySanctionTypes', N'Name'                          , N'Наименование.'
exec spDescColumn N'IrregularitySanctionTypes', N'Code'                          , N'Код.'
GO
