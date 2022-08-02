PRINT 'InterventionCategories'
GO

CREATE TABLE [dbo].[InterventionCategories] (
    [InterventionCategoryId]    INT             NOT NULL IDENTITY,
    [Dimension]                 INT             NOT NULL,
    [Code]                      NVARCHAR(50)    NOT NULL,
    [Name]                      NVARCHAR(MAX)   NOT NULL,
    [NameAlt]                   NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_InterventionCategories]            PRIMARY KEY   ([InterventionCategoryId]),
    CONSTRAINT [UQ_InterventionCategories]            UNIQUE        ([Dimension], [Code]),
    CONSTRAINT [CHK_InterventionCategories_Dimension] CHECK         ([Dimension] IN (1, 2, 3, 4, 5, 6, 7)),
);
GO

exec spDescTable  N'InterventionCategories', N'Категория на интервенция.'
exec spDescColumn N'InterventionCategories', N'InterventionCategoryId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'InterventionCategories', N'Dimension'               , N'Измерение: 1 - Област на интервенция, 2 - Форма на финансиране, 3 - Тип на територията, 4 - Механизми за териториално изпълнение, 5 - Тематична цел, 6 - Вторична тема на ЕСФ, 7 - Стопанска дейност'
exec spDescColumn N'InterventionCategories', N'Code'                    , N'Код.'
exec spDescColumn N'InterventionCategories', N'Name'                    , N'Наименование.'
