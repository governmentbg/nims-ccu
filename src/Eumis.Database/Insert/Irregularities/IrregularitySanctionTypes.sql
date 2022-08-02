﻿PRINT 'Insert IrregularitySanctionTypes'
GO

SET IDENTITY_INSERT [IrregularitySanctionTypes] ON

INSERT INTO [IrregularitySanctionTypes]
    ([IrregularitySanctionTypeId], [IrregularitySanctionCategoryId], [Code]  , [Name]                                                      )
VALUES
-- S1
    (1                           , 1                               , N'S1/00', N'Национална пропорционална глоба'                          ),
    (2                           , 1                               , N'S1/01', N'Национална непропорционална глоба'                        ),
    (3                           , 1                               , N'S1/02', N'Национална глоба по единна ставка'                        ),
    (4                           , 1                               , N'S1/03', N'Загуба на национални субсидии'                            ),
    (5                           , 1                               , N'S1/04', N'Изключване от бъдещи национални субсидии'                 ),
    (6                           , 1                               , N'S1/05', N'Ограничаване достъпа до участие в обществени поръчки'     ),
    (7                           , 1                               , N'S1/06', N'Пропорционална глоба от европейското финансиране'         ),
    (8                           , 1                               , N'S1/07', N'Непропорционална глоба от европейското финансиране'       ),
    (9                           , 1                               , N'S1/08', N'Глоба от европейското финансиране по единна ставка'       ),
    (10                          , 1                               , N'S1/09', N'Загуба на субсидии от европейското финансиране'           ),
    (11                          , 1                               , N'S1/10', N'Изключване от бъдещи субсидии от европейското финансиране'),
    (12                          , 1                               , N'S1/11', N'Други'                                                    ),

-- S5
    (13                          , 2                               , N'S5/00', N'Национална пропорционална глоба'                          ),
    (14                          , 2                               , N'S5/01', N'Национална непропорционална глоба'                        ),
    (15                          , 2                               , N'S5/02', N'Национална глоба по единна ставка'                        ),
    (16                          , 2                               , N'S5/03', N'Загуба на национални субсидии'                            ),
    (17                          , 2                               , N'S5/04', N'Изключване от бъдещи национални субсидии'                 ),
    (18                          , 2                               , N'S5/05', N'Ограничаване достъпа до участие в обществени поръчки'     ),
    (19                          , 2                               , N'S5/06', N'Пропорционална глоба от европейското финансиране'         ),
    (20                          , 2                               , N'S5/07', N'Непропорционална глоба от европейското финансиране'       ),
    (21                          , 2                               , N'S5/08', N'Глоба от европейското финансиране по единна ставка'       ),
    (22                          , 2                               , N'S5/09', N'Загуба на субсидии от европейското финансиране'           ),
    (23                          , 2                               , N'S5/10', N'Изключване от бъдещи субсидии от европейското финансиране'),
    (24                          , 2                               , N'S5/11', N'Лишаване от свобода'                                      ),
    (25                          , 2                               , N'S5/12', N'Лишаване от свобода под 1 година'                         ),
    (26                          , 2                               , N'S5/13', N'Лишаване от свобода от 1 до 4 години'                     ),
    (27                          , 2                               , N'S5/14', N'Лишаване от свобода от над 4 години'                      ),
    (28                          , 2                               , N'S5/15', N'Други'                                                    );
GO

SET IDENTITY_INSERT [IrregularitySanctionTypes] OFF
GO