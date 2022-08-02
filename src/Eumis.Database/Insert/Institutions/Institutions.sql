PRINT 'Insert Institutions'
GO

SET IDENTITY_INSERT [Institutions] ON

INSERT INTO [Institutions]
    ([InstitutionId], [Name]                                                        , [Description])
VALUES
    (1              , N'МФ, Национален фонд'                                        , N'Описание 1'),
    (2              , N'МФ, Одит на средствата от ЕС'                               , N'Описание 2'),
    (3              , N'МТСП, Европейски фондове и международни програми и проекти' , N'Описание 3')

SET IDENTITY_INSERT [Institutions] OFF
GO
