PRINT 'Insert IrregularityCategories'
GO

SET IDENTITY_INSERT [IrregularityCategories] ON

INSERT INTO [IrregularityCategories]
    ([IrregularityCategoryId], [Code], [Name]                   )
VALUES
    (1                       , N'T11', N'Заявление'             ),
    (2                       , N'T12', N'Бенефициент'           ),
    (3                       , N'T13', N'Счетоводство'          ),
    (4                       , N'T14', N'Документи'             ),
    (5                       , N'T15', N'Продукти'              ),
    (6                       , N'T16', N'Действие(бездействие)' ),
    (7                       , N'T17', N'Движение'              ),
    (8                       , N'T18', N'Фалит'                 ),
    (9                       , N'T19', N'Стандарти и цялостност'),
    (10                      , N'Т40', N'Обществени поръчки'    ),
    (11                      , N'Т90', N'Други'                 );

GO

SET IDENTITY_INSERT [IrregularityCategories] OFF
GO