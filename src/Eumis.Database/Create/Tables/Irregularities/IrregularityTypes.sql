PRINT 'IrregularityTypes'
GO

CREATE TABLE [dbo].[IrregularityTypes] (
    [IrregularityTypeId]      INT                 NOT NULL IDENTITY,
    [IrregularityCategoryId]  INT                 NOT NULL,
    [Name]                    NVARCHAR(MAX)       NOT NULL,
    [Code]                    NVARCHAR(200)       NULL,

    CONSTRAINT [PK_IrregularityTypes]                         PRIMARY KEY ([IrregularityTypeId]),
    CONSTRAINT [FK_IrregularityTypes_IrregularityCategories]  FOREIGN KEY ([IrregularityCategoryId]) REFERENCES [dbo].[IrregularityCategories] ([IrregularityCategoryId]),
);
GO

exec spDescTable  N'IrregularityTypes', N'Типове нередности.'
exec spDescColumn N'IrregularityTypes', N'IrregularityTypeId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularityTypes', N'IrregularityCategoryId', N'Идентификатор на категория нередност.'
exec spDescColumn N'IrregularityTypes', N'Name'                  , N'Наименование.'
exec spDescColumn N'IrregularityTypes', N'Code'                  , N'Код.'
GO
