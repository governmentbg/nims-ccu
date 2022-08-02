PRINT 'Insert EvalSessionProjects'
GO

INSERT [dbo].[EvalSessionProjects]
    ([EvalSessionId], [ProjectId], [IsDeleted], [IsDeletedNote])
VALUES
    (1              , 1          , 0          , NULL           ),
    (1              , 2          , 0          , NULL           )
GO
