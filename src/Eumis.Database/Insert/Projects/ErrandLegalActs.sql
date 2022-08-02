PRINT 'Insert ErrandLegalActs'
GO

SET IDENTITY_INSERT [ErrandLegalActs] ON

INSERT INTO [ErrandLegalActs]
    ([ErrandLegalActId], [Gid]                                  , [Name])
VALUES
    (1                 , N'92af17f1-9243-4f7e-be07-7a2629688d1b', N'ЗОП'),
    (2                 , N'7e9b44e8-742b-45e5-b967-7b7feec6e18a', N'ПМС')

SET IDENTITY_INSERT [ErrandLegalActs] OFF
GO
