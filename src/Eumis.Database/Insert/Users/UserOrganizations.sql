PRINT 'Insert UserOrganizations'
GO

SET IDENTITY_INSERT [UserOrganizations] ON

INSERT INTO [UserOrganizations]
    ([UserOrganizationId], [Name], [CreateDate], [ModifyDate])
VALUES
    (1                   , N'ЦКЗ'   , GETDATE()   , GETDATE()   ),
    (2                   , N'ОПДУ'  , GETDATE()   , GETDATE()   ),
    (3                   , N'ОПТТИ' , GETDATE()   , GETDATE()   ),
    (4                   , N'ОПРР'  , GETDATE()   , GETDATE()   ),
    (5                   , N'ОПРЧР' , GETDATE()   , GETDATE()   ),
    (6                   , N'ОПИК'  , GETDATE()   , GETDATE()   ),
    (7                   , N'ОПОС'  , GETDATE()   , GETDATE()   ),
    (8                   , N'ОПНОИР', GETDATE()   , GETDATE()   )

SET IDENTITY_INSERT [UserOrganizations] OFF
GO

