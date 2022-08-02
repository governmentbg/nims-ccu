PRINT 'Insert EvalSessionEvaluations'
GO

SET IDENTITY_INSERT [dbo].[EvalSessionEvaluations] ON 
GO
INSERT [dbo].[EvalSessionEvaluations]
    ([EvalSessionId], [EvalSessionEvaluationId], [ProjectId], [CalculationType], [EvalType], [EvalTableType], [EvalIsPassed], [EvalPoints]                  , [EvalNote], [IsDeleted], [IsDeletedNote], [CreateDate]                           )
VALUES
    (1              , 1                        , 1          , 1                , 1         , 1              , 1             , NULL                          , NULL      , 0          , NULL           , CAST(0x07D31469C37D0F3A0B AS DateTime2)),
    (1              , 2                        , 1          , 1                , 2         , 2              , 1             , CAST(16.570 AS Decimal(15, 3)), NULL      , 0          , NULL           , CAST(0x07DE1AB2C77D0F3A0B AS DateTime2)),
    (1              , 3                        , 2          , 1                , 1         , 1              , 1             , NULL                          , NULL      , 0          , NULL           , CAST(0x0770F5CDCB7D0F3A0B AS DateTime2)),
    (1              , 4                        , 2          , 1                , 2         , 2              , 1             , CAST(17.060 AS Decimal(15, 3)), NULL      , 0          , NULL           , CAST(0x07DAF345CF7D0F3A0B AS DateTime2))
GO

SET IDENTITY_INSERT [dbo].[EvalSessionEvaluations] OFF
GO


INSERT [dbo].[EvalSessionEvaluationSheets]
    ([EvalSessionId], [EvalSessionEvaluationId], [EvalSessionSheetId])
VALUES
    (1              , 1                        , 1                   ),
    (1              , 2                        , 2                   ),
    (1              , 3                        , 3                   ),
    (1              , 4                        , 4                   )
GO
