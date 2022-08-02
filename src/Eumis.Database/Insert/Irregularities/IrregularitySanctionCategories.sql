PRINT 'Insert IrregularitySanctionCategories'
GO

SET IDENTITY_INSERT [IrregularitySanctionCategories] ON

INSERT INTO [IrregularitySanctionCategories]
    ([IrregularitySanctionCategoryId], [Code], [Name]            )
VALUES
    (1                               , N'S1' , N'Административни'),
    (2                               , N'S5' , N'Наказателни'    );
GO

SET IDENTITY_INSERT [IrregularitySanctionCategories] OFF
GO