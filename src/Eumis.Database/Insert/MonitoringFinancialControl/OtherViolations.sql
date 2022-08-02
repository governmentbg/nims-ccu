PRINT 'Insert OtherViolations'
GO

SET IDENTITY_INSERT [OtherViolations] ON

INSERT INTO [OtherViolations]
    ([OtherViolationId], [Gid]  , [Name]                                                          , [IsActive], [CreateDate], [ModifyDate])
VALUES
    (1                 , NEWID(), N'Неправомерни срокове'                                         , 1         ,GETDATE()    , GETDATE()),
    (2                 , NEWID(), N'Липса на публичност и възлагане без конкуренция'              , 1         ,GETDATE()    , GETDATE()),
    (3                 , NEWID(), N'Нарушения в документацията за обществени поръчки'             , 1         ,GETDATE()    , GETDATE()),
    (4                 , NEWID(), N'Нарушения в при оценка на проектните предложения'             , 1         ,GETDATE()    , GETDATE()),
    (5                 , NEWID(), N'Нарушения при изпълнението на сключения договор с изпълнител' , 1         ,GETDATE()    , GETDATE()),
    (6                 , NEWID(), N'Конфликт на интереси'                                         , 1         ,GETDATE()    , GETDATE()),
    (7                 , NEWID(), N'Нарушаване на правилата за държавната помощ'                  , 1         ,GETDATE()    , GETDATE()),
    (8                 , NEWID(), N'Липса на добро финансово управление'                          , 1         ,GETDATE()    , GETDATE()),
    (9                 , NEWID(), N'Липса на устойчивост'                                         , 1         ,GETDATE()    , GETDATE()),
    (10                , NEWID(), N'Липса на адекватна одитна следа'                              , 1         ,GETDATE()    , GETDATE()),
    (11                , NEWID(), N'Неизпълнение на мерките за информация и публичност'           , 1         ,GETDATE()    , GETDATE()),
    (12                , NEWID(), N'Неизпълнение на индикатори'                                   , 1         ,GETDATE()    , GETDATE())

SET IDENTITY_INSERT [OtherViolations] OFF
GO
