GO

SET IDENTITY_INSERT [CompanyLegalTypes] ON
INSERT INTO [CompanyLegalTypes]
    ([CompanyLegalTypeId], [Gid]                                  , [CompanyTypeId], [Name]         )
VALUES
    (36                  , N'1f77c5ea-969f-4115-921c-1482df5dffb3', 6              , N'Чуждестранен')
SET IDENTITY_INSERT [CompanyLegalTypes] OFF

GO
UPDATE [CompanyLegalTypes] SET [Name] = N'Болница/медицинско заведение' WHERE [CompanyLegalTypeId] = 34 AND [Name] = N'Медицинско заведение'
