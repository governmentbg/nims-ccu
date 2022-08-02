PRINT 'Insert EvalSessionProjectStandings'
GO

SET IDENTITY_INSERT [dbo].[EvalSessionProjectStandings] ON 
GO

INSERT [dbo].[EvalSessionProjectStandings]
    ([EvalSessionId], [EvalSessionProjectStandingId], [ProjectId], [IsPreliminary], [OrderNum], [Status], [ManualStatus], [GrandAmount], [IsDeleted], [IsDeletedNote], [Notes], [EvalSessionStandingId], [CreateDate]                           , [ProjectVersionXmlId])
VALUES
    (1              , 1                             , 2          , 0              , 1         , 1       , 1             , 0.0000       , 0          , NULL           , NULL   , NULL                   , CAST(0x07748B62D87D0F3A0B AS DateTime2), 2                    ),
    (1              , 2                             , 1          , 0              , 2         , 2       , 1             , 1500000.0000 , 0          , NULL           , NULL   , NULL                   , CAST(0x072D526AE47D0F3A0B AS DateTime2), 1                    )
GO

SET IDENTITY_INSERT [dbo].[EvalSessionProjectStandings] OFF
GO


INSERT [dbo].[EvalSessionProjectStandingEvaluations]
    ([EvalSessionId], [EvalSessionProjectStandingId], [EvalSessionEvaluationId])
VALUES
    (1              , 1                             , 3                        ),
    (1              , 1                             , 4                        ),
    (1              , 2                             , 1                        ),
    (1              , 2                             , 2                        )
GO
