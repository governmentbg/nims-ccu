PRINT 'Insert EvalSessions'
GO

INSERT INTO [dbo].[Counters]
    ([Name]                         , [CurrentNumber])
VALUES
    (N'eval-session-for-procedure#2', 1              );
GO

SET IDENTITY_INSERT [EvalSessions] ON

INSERT INTO [EvalSessions]
    ([EvalSessionId], [ProcedureId], [EvalSessionStatus], [EvalSessionType], [SessionNum]                        , [SessionDate], [OrderNum], [OrderDate], [CreateDate], [ModifyDate])
VALUES
    (1              , 2            , 3                  , 2                , N'BG05SFOP001-1.2014.001-S1'        , GETDATE()    , NULL      , NULL,        GETDATE()   , GETDATE()   )

SET IDENTITY_INSERT [EvalSessions] OFF
GO
