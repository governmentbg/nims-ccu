GO

DELETE FROM [MapNodeInstitutions]

DELETE FROM [Institutions]

DBCC CHECKIDENT ([Institutions], RESEED, 1)

--Insert Institutions
SET IDENTITY_INSERT [Institutions] ON

INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (1, N'МС', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (2, N'МФ, Д „Национален фонд“', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (3, N'ИА Одит на средствата от ЕС', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (4, N'МТИТС, Д „Координация на програми и проекти”', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (5, N'МРР, ГД „Програмиране на регионалното развитие”', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (6, N'МТСП, ГД „Европейски фондове и международни програми и проекти”', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (7, N'МИЕ, ГД „Европейски фондове за конкурентоспособност”', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (8, N'МОСВ, ГД „Оперативна програма Околна среда”', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (9, N'МОН, ГД „Структурни фондове и международни образователни програми“', N'Описание')
INSERT INTO [Institutions] ([InstitutionId], [Name], [Description]) VALUES (10, N'МФ, Национален фонд', N'Описание')

SET IDENTITY_INSERT [Institutions] OFF

GO

--Insert MapNodeInstitutions
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (1, 1, 1, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (1, 2, 3, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (1, 3, 2, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (2, 4, 1, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (2, 2, 3, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (2, 3, 2, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (3, 5, 1, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (3, 2, 3, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (3, 3, 2, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (4, 6, 1, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (4, 2, 3, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (4, 3, 2, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (5, 7, 1, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (5, 2, 3, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (5, 3, 2, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (6, 8, 1, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (6, 2, 3, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (6, 3, 2, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (7, 9, 1, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (7, 10, 3, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (7, 3, 2, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')

GO
