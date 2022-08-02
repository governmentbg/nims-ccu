PRINT 'Insert ContractReportMicrosDistricts'
GO

SET IDENTITY_INSERT [ContractReportMicrosDistricts] ON

INSERT INTO [ContractReportMicrosDistricts]
    ([ContractReportMicrosDistrictId], [DistrictId], [Name]           )
VALUES
    (1                               ,1            , N'Благоевград'   ),
    (2                               ,2            , N'Бургас'        ),
    (3                               ,3            , N'Варна'         ),
    (4                               ,4            , N'Велико_Търново'),
    (5                               ,5            , N'Видин'         ),
    (6                               ,6            , N'Враца'         ),
    (7                               ,7            , N'Габрово'       ),
    (8                               ,8            , N'Добрич'        ),
    (9                               ,9            , N'Кърджали'      ),
    (10                              ,10           , N'Кюстендил'     ),
    (11                              ,11           , N'Ловеч'         ),
    (12                              ,12           , N'Монтана'       ),
    (13                              ,13           , N'Пазарджик'     ),
    (14                              ,14           , N'Перник'        ),
    (15                              ,15           , N'Плевен'        ),
    (16                              ,16           , N'Пловдив'       ),
    (17                              ,17           , N'Разград'       ),
    (18                              ,18           , N'Русе'          ),
    (19                              ,19           , N'Силистра'      ),
    (20                              ,20           , N'Сливен'        ),
    (21                              ,21           , N'Смолян'        ),
    (22                              ,22           , N'София'         ),
    (23                              ,23           , N'София_област'  ),
    (24                              ,24           , N'Стара_Загора'  ),
    (25                              ,25           , N'Търговище'     ),
    (26                              ,26           , N'Хасково'       ),
    (27                              ,27           , N'Шумен'         ),
    (28                              ,28           , N'Ямбол'         );
GO

SET IDENTITY_INSERT [ContractReportMicrosDistricts] OFF
GO