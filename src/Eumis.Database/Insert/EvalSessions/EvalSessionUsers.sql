PRINT 'Insert EvalSessionUsers'
GO
SET IDENTITY_INSERT [EvalSessionUsers] ON

INSERT INTO [EvalSessionUsers]
    ([EvalSessionUserId], [EvalSessionId], [UserId]      , [Type]  , [Position]     , [Status])
VALUES
    (1                  , 1              , 6             , 1       , N'Админ Сесия' , 2       ),
    (2                  , 1              , 7             , 2       , N'Оценител'    , 2       ),
    (3                  , 1              , 8             , 3       , N'Помощник'    , 2       )

SET IDENTITY_INSERT [EvalSessionUsers] OFF
GO
