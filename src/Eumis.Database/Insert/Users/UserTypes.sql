PRINT 'Insert UserTypes'
GO

SET IDENTITY_INSERT [UserTypes] ON

INSERT INTO [UserTypes]
    ([UserTypeId], [Name]                  , [IsSuperUser], [PermissionTemplateId], [UserOrganizationId], [CreateDate], [ModifyDate])
VALUES
    (1           , N'Администратор ЦКЗ'      , 1 , 1 , 1 , GETDATE()   , GETDATE()   ),
    (2           , N'Администратор от ОПДУ'  , 0 , 2 , 2 , GETDATE()   , GETDATE()   ),
    (3           , N'Администратор от ОПТТИ' , 0 , 3 , 3 , GETDATE()   , GETDATE()   ),
    (4           , N'Администратор от ОПРР'  , 0 , 4 , 4 , GETDATE()   , GETDATE()   ),
    (5           , N'Администратор от ОПРЧР' , 0 , 5 , 5 , GETDATE()   , GETDATE()   ),
    (6           , N'Администратор от ОПИК'  , 0 , 6 , 6 , GETDATE()   , GETDATE()   ),
    (7           , N'Администратор от ОПОС'  , 0 , 7 , 7 , GETDATE()   , GETDATE()   ),
    (8           , N'Администратор от ОПНОИР', 0 , 8 , 8 , GETDATE()   , GETDATE()   ),
    (9           , N'Потребител от ОПДУ'     , 0 , 9 , 2 , GETDATE()   , GETDATE()   ),
    (10          , N'Потребител от ОПТТИ'    , 0 , 10, 3 , GETDATE()   , GETDATE()   ),
    (11          , N'Потребител от ОПРР'     , 0 , 11, 4 , GETDATE()   , GETDATE()   ),
    (12          , N'Потребител от ОПРЧР'    , 0 , 12, 5 , GETDATE()   , GETDATE()   ),
    (13          , N'Потребител от ОПИК'     , 0 , 13, 6 , GETDATE()   , GETDATE()   ),
    (14          , N'Потребител от ОПОС'     , 0 , 14, 7 , GETDATE()   , GETDATE()   ),
    (15          , N'Потребител от ОПНОИР'   , 0 , 15, 8 , GETDATE()   , GETDATE()   ),
    (16          , N'Админ сесии'            , 0 , 16, 1 , GETDATE()   , GETDATE()   ),
    (17          , N'Оценител към сесия'     , 0 , 17, 1 , GETDATE()   , GETDATE()   )

SET IDENTITY_INSERT [UserTypes] OFF
GO
