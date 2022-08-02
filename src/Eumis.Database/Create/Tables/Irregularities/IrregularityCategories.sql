PRINT 'IrregularityCategories'
GO

CREATE TABLE [dbo].[IrregularityCategories] (
    [IrregularityCategoryId]  INT                 NOT NULL IDENTITY,
    [Name]                    NVARCHAR(MAX)       NOT NULL,
    [Code]                    NVARCHAR(200)       NULL,

    CONSTRAINT [PK_IrregularityCategories]       PRIMARY KEY ([IrregularityCategoryId])
);
GO

exec spDescTable  N'IrregularityCategories', N'Категория на нередност.'
exec spDescColumn N'IrregularityCategories', N'IrregularityCategoryId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularityCategories', N'Name'                  , N'Наименование.'
exec spDescColumn N'IrregularityCategories', N'Code'                  , N'Код.'
GO
