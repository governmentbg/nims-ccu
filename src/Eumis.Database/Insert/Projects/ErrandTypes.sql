﻿PRINT 'Insert ErrandTypes'
GO

SET IDENTITY_INSERT [ErrandTypes] ON

INSERT INTO [ErrandTypes]
    ([ErrandTypeId], [ErrandLegalActId], [Code], [Name])
VALUES
    -- ЗОП
    (1             , 1                 , N'01' , N'Открита процедура'),
    (2             , 1                 , N'02' , N'Ограничена процедура'),
    (3             , 1                 , N'03' , N'Конкурс за проект'),
    (4             , 1                 , N'04' , N'Процедура на Договаряне с обявление'),
    (5             , 1                 , N'05' , N'Процедура на Договаряне без обявление'),
    (6             , 1                 , N'06' , N'Състезателен Диалог'),
    (7             , 1                 , N'07' , N'Друго (По указание на УО)'),

    -- ПМС
    (8             , 2                 , N'11' , N'Избор с публична покана'),
    (9             , 2                 , N'12' , N'Избор без провеждане на процедура (по указание на УО)'),

    -- ЗОП - допълнение
    (10            , 1                 , N'20' , N'Състезателна процедура с договаряне'),
    (11            , 1                 , N'21' , N'Договаряне с предварителна покана за участие'),
    (12            , 1                 , N'22' , N'Партньорство за иновации'),
    (13            , 1                 , N'23' , N'Договаряне без предварителна покана за участие'),
    (14            , 1                 , N'24' , N'Договаряне без публикуване на обявление за поръчка'),
    (15            , 1                 , N'25' , N'Публично състезание'),
    (16            , 1                 , N'26' , N'Пряко договаряне'),
    (17            , 1                 , N'27' , N'Събиране на оферти с обява')

SET IDENTITY_INSERT [ErrandTypes] OFF
GO