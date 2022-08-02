PRINT 'Insert ExpenseTypes'
GO

SET IDENTITY_INSERT [ExpenseTypes] ON

INSERT INTO [ExpenseTypes]
    ([ExpenseTypeId], [Gid]  , [Name]                                           ,[IsActive] , [CreateDate], [ModifyDate])
VALUES
    (1              , NEWID(), N'НЕПРЕКИ РАЗХОДИ'                               ,1          , GETDATE()   , GETDATE()   ),
    (2              , NEWID(), N'РАЗХОДИ ЗА ПЕРСОНАЛ'                           ,1          , GETDATE()   , GETDATE()   ),
    (3              , NEWID(), N'РАЗХОДИ ЗА МАТЕРИАЛНИ АКТИВИ'                  ,1          , GETDATE()   , GETDATE()   ),
    (4              , NEWID(), N'РАЗХОДИ ЗА НЕМАТЕРИАЛНИ АКТИВИ'                ,1          , GETDATE()   , GETDATE()   ),
    (5              , NEWID(), N'РАЗХОДИ ЗА СМР'                                ,1          , GETDATE()   , GETDATE()   ),
    (6              , NEWID(), N'РАЗХОДИ ЗА УСЛУГИ'                             ,1          , GETDATE()   , GETDATE()   ),
    (7              , NEWID(), N'РАЗХОДИ ЗА МАТЕРИАЛИ'                          ,1          , GETDATE()   , GETDATE()   ),
    (8              , NEWID(), N'НЕДОПУСТИМИ РАЗХОДИ'                           ,1          , GETDATE()   , GETDATE()   ),
    (9              , NEWID(), N'РАЗХОДИ ЗА ПРОВЕЖДАНЕ И УЧАСТИЕ В МЕРОПРИЯТИЯ' ,1          , GETDATE()   , GETDATE()   ),
    (10             , NEWID(), N'НЕПРЕДВИДЕНИ РАЗХОДИ'                          ,1          , GETDATE()   , GETDATE()   ),
    (11             , NEWID(), N'РАЗХОДИ ЗА ТАКСИ'                              ,1          , GETDATE()   , GETDATE()   ),
    (12             , NEWID(), N'Невъзстановим ДДС (ако е приложимо)'           ,1          , GETDATE()   , GETDATE()   )

SET IDENTITY_INSERT [ExpenseTypes] OFF
GO
