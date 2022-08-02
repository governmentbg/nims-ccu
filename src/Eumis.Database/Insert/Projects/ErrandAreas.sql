PRINT 'Insert ErrandAreas'
GO

SET IDENTITY_INSERT [ErrandAreas] ON

INSERT INTO [ErrandAreas]
    ([ErrandAreaId], [Code], [Name]         )
VALUES
    (1             , N'0'  , N'Доставка'    ),
    (2             , N'1'  , N'Услуга'      ),
    (3             , N'2'  , N'Строителство')

SET IDENTITY_INSERT [ErrandAreas] OFF
GO
