PRINT 'Insert InstitutionTypes'
GO

SET IDENTITY_INSERT [InstitutionTypes] ON

INSERT INTO [InstitutionTypes]
    ([InstitutionTypeId], [Code]   , [Name]                            , [Alias]       )
VALUES
    (1                  , N'Код 1' , N'Управляващ орган'               , N'Псевдоним 1'),
    (2                  , N'Код 2' , N'Одитен орган'                   , N'Псевдоним 2'),
    (3                  , N'Код 3' , N'Сертифициращ орган'             , N'Псевдоним 3'),
    (4                  , N'Код 4' , N'Централно координационно звено' , N'Псевдоним 3'),
    (5                  , N'Код 5' , N'Контрол'                        , N'Псевдоним 3'),
    (6                  , N'Код 6' , N'Външен оценител'                , N'Псевдоним 3')

SET IDENTITY_INSERT [InstitutionTypes] OFF
GO
