PRINT 'Insert CompanySizeTypes'
GO

SET IDENTITY_INSERT [CompanySizeTypes] ON

INSERT INTO [CompanySizeTypes]
    ([CompanySizeTypeId], [Gid]                                  , [Name]        , [Alias]        , [Order]     , [NameAlt]         )
VALUES
    (1                  , N'24307c2e-f1ed-43a1-8499-7bc9cfcb34d1', N'Микро'      , NULL           , 1           , N'Micro'          ),
    (2                  , N'556ee465-d2d0-40bb-a014-30a481705edd', N'Малко'      , NULL           , 2           , N'Small'          ),
    (3                  , N'ffc36285-b8b3-4b94-9fa7-dbf97d92a8c4', N'Средно'     , NULL           , 3           , N'Medium'         ),
    (4                  , N'00a7550c-2bcd-4543-973c-097a45aa956e', N'Голямо'     , NULL           , 4           , N'Large'          ),
    (5                  , N'dafda889-346b-4667-a4c1-be97c0c7f68a', N'Неприложимо', N'inapplicable', 5           , N'Not applicable' )

SET IDENTITY_INSERT [CompanySizeTypes] OFF
GO
