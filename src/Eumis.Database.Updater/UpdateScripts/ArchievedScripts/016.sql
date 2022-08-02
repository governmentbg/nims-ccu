GO
CREATE TABLE [dbo].[Regions] (
    [RegionId]		INT   NOT NULL IDENTITY,
	[CountryId]		INT	  NOT NULL,
    [NutsCode]      NVARCHAR(200)   NOT NULL,
    [Name]          NVARCHAR(MAX)   NOT NULL,
	[FullPathName]  NVARCHAR(1000)  NOT NULL,
	[FullPath]		NVARCHAR(500)   NOT NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY ([RegionId]),
	CONSTRAINT [FK_Regions_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId]),
);

GO
exec spDescTable  N'Regions', N'Региони'
exec spDescColumn N'Regions', N'RegionId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Regions', N'CountryId'       , N'Идентификатор на държава.'
exec spDescColumn N'Regions', N'NutsCode'		 , N'Код по ZZ'
exec spDescColumn N'Regions', N'Name'            , N'Наименование.'
exec spDescColumn N'Regions', N'FullPathName'    , N'Пълно наименование.'
exec spDescColumn N'Regions', N'FullPath'        , N'Всички кодове.'

GO
ALTER TABLE Settlements ADD [Priority] INT NOT NULL DEFAULT(0)

GO
SET IDENTITY_INSERT [Regions] ON
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (1, 23, N'BG0000100', N'Плаж Шкорпиловци', N'България, Плаж Шкорпиловци', N'BG, BG0000100')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (2, 23, N'BG0000102', N'Долината на река Батова', N'България, Долината на река Батова', N'BG, BG0000102')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (3, 23, N'BG0000103', N'Галата', N'България, Галата', N'BG, BG0000103')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (4, 23, N'BG0000104', N'Провадийско - Роякско плато', N'България, Провадийско - Роякско плато', N'BG, BG0000104')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (5, 23, N'BG0000106', N'Хърсовска река', N'България, Хърсовска река', N'BG, BG0000106')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (6, 23, N'BG0000107', N'Суха река', N'България, Суха река', N'BG, BG0000107')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (7, 23, N'BG0000113', N'Витоша', N'България, Витоша', N'BG, BG0000113')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (8, 23, N'BG0000116', N'Камчия', N'България, Камчия', N'BG, BG0000116')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (9, 23, N'BG0000117', N'Котленска планина', N'България, Котленска планина', N'BG, BG0000117')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (10, 23, N'BG0000118', N'Златни пясъци', N'България, Златни пясъци', N'BG, BG0000118')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (11, 23, N'BG0000119', N'Трите братя', N'България, Трите братя', N'BG, BG0000119')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (12, 23, N'BG0000130', N'Крайморска Добруджа', N'България, Крайморска Добруджа', N'BG, BG0000130')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (13, 23, N'BG0000132', N'Побитите камъни', N'България, Побитите камъни', N'BG, BG0000132')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (14, 23, N'BG0000133', N'Камчийска и Еменска планина', N'България, Камчийска и Еменска планина', N'BG, BG0000133')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (15, 23, N'BG0000134', N'Чокльово блато', N'България, Чокльово блато', N'BG, BG0000134')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (16, 23, N'BG0000136', N'Река Горна Луда Камчия', N'България, Река Горна Луда Камчия', N'BG, BG0000136')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (17, 23, N'BG0000137', N'Река Долна Луда Камчия', N'България, Река Долна Луда Камчия', N'BG, BG0000137')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (18, 23, N'BG0000138', N'Каменица', N'България, Каменица', N'BG, BG0000138')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (19, 23, N'BG0000139', N'Луда Камчия', N'България, Луда Камчия', N'BG, BG0000139')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (20, 23, N'BG0000141', N'Река Камчия', N'България, Река Камчия', N'BG, BG0000141')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (21, 23, N'BG0000143', N'Караагач', N'България, Караагач', N'BG, BG0000143')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (22, 23, N'BG0000146', N'Плаж Градина - Златна рибка', N'България, Плаж Градина - Златна рибка', N'BG, BG0000146')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (23, 23, N'BG0000149', N'Ришки проход', N'България, Ришки проход', N'BG, BG0000149')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (24, 23, N'BG0000151', N'Айтоска планина', N'България, Айтоска планина', N'BG, BG0000151')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (25, 23, N'BG0000152', N'Поморийско езеро', N'България, Поморийско езеро', N'BG, BG0000152')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (26, 23, N'BG0000154', N'Езеро Дуранкулак', N'България, Езеро Дуранкулак', N'BG, BG0000154')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (27, 23, N'BG0000156', N'Шабленски езерен комплекс', N'България, Шабленски езерен комплекс', N'BG, BG0000156')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (28, 23, N'BG0000164', N'Сините камъни', N'България, Сините камъни', N'BG, BG0000164')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (29, 23, N'BG0000165', N'Лозенска планина', N'България, Лозенска планина', N'BG, BG0000165')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (30, 23, N'BG0000166', N'Врачански Балкан', N'България, Врачански Балкан', N'BG, BG0000166')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (31, 23, N'BG0000167', N'Беласица', N'България, Беласица', N'BG, BG0000167')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (32, 23, N'BG0000168', N'Лудогорие', N'България, Лудогорие', N'BG, BG0000168')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (33, 23, N'BG0000169', N'Лудогорие - Сребърна', N'България, Лудогорие - Сребърна', N'BG, BG0000169')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (34, 23, N'BG0000171', N'Лудогорие - Боблата', N'България, Лудогорие - Боблата', N'BG, BG0000171')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (35, 23, N'BG0000173', N'Островче', N'България, Островче', N'BG, BG0000173')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (36, 23, N'BG0000178', N'Тича', N'България, Тича', N'BG, BG0000178')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (37, 23, N'BG0000180', N'Боблата', N'България, Боблата', N'BG, BG0000180')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (38, 23, N'BG0000181', N'Река Вит', N'България, Река Вит', N'BG, BG0000181')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (39, 23, N'BG0000182', N'Орсоя', N'България, Орсоя', N'BG, BG0000182')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (40, 23, N'BG0000190', N'Витата стена', N'България, Витата стена', N'BG, BG0000190')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (41, 23, N'BG0000191', N'Варненско-Белославско езеро', N'България, Варненско-Белославско езеро', N'BG, BG0000191')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (42, 23, N'BG0000192', N'Река Тунджа 1', N'България, Река Тунджа 1', N'BG, BG0000192')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (43, 23, N'BG0000194', N'Река Чая', N'България, Река Чая', N'BG, BG0000194')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (44, 23, N'BG0000195', N'Река Тунджа 2', N'България, Река Тунджа 2', N'BG, BG0000195')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (45, 23, N'BG0000196', N'Река Мочурица', N'България, Река Мочурица', N'BG, BG0000196')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (46, 23, N'BG0000198', N'Средецка река', N'България, Средецка река', N'BG, BG0000198')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (47, 23, N'BG0000199', N'Цибър', N'България, Цибър', N'BG, BG0000199')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (48, 23, N'BG0000203', N'Тулово', N'България, Тулово', N'BG, BG0000203')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (49, 23, N'BG0000205', N'Стралджа', N'България, Стралджа', N'BG, BG0000205')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (50, 23, N'BG0000206', N'Съдиево', N'България, Съдиево', N'BG, BG0000206')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (51, 23, N'BG0000208', N'Босна', N'България, Босна', N'BG, BG0000208')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (52, 23, N'BG0000209', N'Пирин', N'България, Пирин', N'BG, BG0000209')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (53, 23, N'BG0000211', N'Твърдишка планина', N'България, Твърдишка планина', N'BG, BG0000211')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (54, 23, N'BG0000212', N'Сакар', N'България, Сакар', N'BG, BG0000212')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (55, 23, N'BG0000213', N'Търновски височини', N'България, Търновски височини', N'BG, BG0000213')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (56, 23, N'BG0000214', N'Дряновски манастир', N'България, Дряновски манастир', N'BG, BG0000214')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (57, 23, N'BG0000216', N'Емен', N'България, Емен', N'BG, BG0000216')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (58, 23, N'BG0000217', N'Ждрелото на река Тунджа', N'България, Ждрелото на река Тунджа', N'BG, BG0000217')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (59, 23, N'BG0000218', N'Дервентски възвишения 1', N'България, Дервентски възвишения 1', N'BG, BG0000218')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (60, 23, N'BG0000219', N'Дервентски възвишения 2', N'България, Дервентски възвишения 2', N'BG, BG0000219')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (61, 23, N'BG0000220', N'Долна Места', N'България, Долна Места', N'BG, BG0000220')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (62, 23, N'BG0000224', N'Огражден - Малешево', N'България, Огражден - Малешево', N'BG, BG0000224')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (63, 23, N'BG0000230', N'Факийска река', N'България, Факийска река', N'BG, BG0000230')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (64, 23, N'BG0000231', N'Беленска гора', N'България, Беленска гора', N'BG, BG0000231')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (65, 23, N'BG0000232', N'Батин', N'България, Батин', N'BG, BG0000232')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (66, 23, N'BG0000233', N'Студена река', N'България, Студена река', N'BG, BG0000233')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (67, 23, N'BG0000237', N'Остров Пожарево', N'България, Остров Пожарево', N'BG, BG0000237')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (68, 23, N'BG0000239', N'Обнова - Караман дол', N'България, Обнова - Караман дол', N'BG, BG0000239')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (69, 23, N'BG0000240', N'Студенец', N'България, Студенец', N'BG, BG0000240')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (70, 23, N'BG0000241', N'Сребърна', N'България, Сребърна', N'BG, BG0000241')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (71, 23, N'BG0000242', N'Залив Ченгене скеле', N'България, Залив Ченгене скеле', N'BG, BG0000242')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (72, 23, N'BG0000247', N'Никополско плато', N'България, Никополско плато', N'BG, BG0000247')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (73, 23, N'BG0000254', N'Бесапарски възвишения', N'България, Бесапарски възвишения', N'BG, BG0000254')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (74, 23, N'BG0000255', N'Градинска гора', N'България, Градинска гора', N'BG, BG0000255')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (75, 23, N'BG0000261', N'Язовир Копринка', N'България, Язовир Копринка', N'BG, BG0000261')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (76, 23, N'BG0000263', N'Скалско', N'България, Скалско', N'BG, BG0000263')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (77, 23, N'BG0000266', N'Пещера Мандрата', N'България, Пещера Мандрата', N'BG, BG0000266')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (78, 23, N'BG0000269', N'Пещера Лястовицата', N'България, Пещера Лястовицата', N'BG, BG0000269')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (79, 23, N'BG0000270', N'Атанасовско езеро', N'България, Атанасовско езеро', N'BG, BG0000270')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (80, 23, N'BG0000271', N'Мандра-Пода', N'България, Мандра-Пода', N'BG, BG0000271')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (81, 23, N'BG0000273', N'Бургаско езеро', N'България, Бургаско езеро', N'BG, BG0000273')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (82, 23, N'BG0000275', N'Язовир Стамболийски', N'България, Язовир Стамболийски', N'BG, BG0000275')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (83, 23, N'BG0000279', N'Стара река', N'България, Стара река', N'BG, BG0000279')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (84, 23, N'BG0000280', N'Златаришка река', N'България, Златаришка река', N'BG, BG0000280')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (85, 23, N'BG0000281', N'Река Белица', N'България, Река Белица', N'BG, BG0000281')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (86, 23, N'BG0000282', N'Дряновска река', N'България, Дряновска река', N'BG, BG0000282')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (87, 23, N'BG0000287', N'Меричлерска река', N'България, Меричлерска река', N'BG, BG0000287')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (88, 23, N'BG0000289', N'Трилистник', N'България, Трилистник', N'BG, BG0000289')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (89, 23, N'BG0000291', N'Гора Шишманци', N'България, Гора Шишманци', N'BG, BG0000291')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (90, 23, N'BG0000294', N'Кършалево', N'България, Кършалево', N'BG, BG0000294')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (91, 23, N'BG0000295', N'Долни Коритен', N'България, Долни Коритен', N'BG, BG0000295')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (92, 23, N'BG0000298', N'Конявска планина', N'България, Конявска планина', N'BG, BG0000298')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (93, 23, N'BG0000301', N'Черни рид', N'България, Черни рид', N'BG, BG0000301')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (94, 23, N'BG0000304', N'Голак', N'България, Голак', N'BG, BG0000304')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (95, 23, N'BG0000308', N'Верила', N'България, Верила', N'BG, BG0000308')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (96, 23, N'BG0000313', N'Руй', N'България, Руй', N'BG, BG0000313')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (97, 23, N'BG0000314', N'Ребро', N'България, Ребро', N'BG, BG0000314')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (98, 23, N'BG0000322', N'Драгоман', N'България, Драгоман', N'BG, BG0000322')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (99, 23, N'BG0000332', N'Карлуковски карст', N'България, Карлуковски карст', N'BG, BG0000332')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (100, 23, N'BG0000334', N'Остров', N'България, Остров', N'BG, BG0000334')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (101, 23, N'BG0000335', N'Карабоаз', N'България, Карабоаз', N'BG, BG0000335')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (102, 23, N'BG0000336', N'Златия', N'България, Златия', N'BG, BG0000336')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (103, 23, N'BG0000339', N'Раброво', N'България, Раброво', N'BG, BG0000339')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (104, 23, N'BG0000340', N'Цар Петрово', N'България, Цар Петрово', N'BG, BG0000340')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (105, 23, N'BG0000365', N'Овчи хълмове', N'България, Овчи хълмове', N'BG, BG0000365')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (106, 23, N'BG0000366', N'Кресна - Илинденци', N'България, Кресна - Илинденци', N'BG, BG0000366')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (107, 23, N'BG0000372', N'Циганско градище', N'България, Циганско градище', N'BG, BG0000372')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (108, 23, N'BG0000374', N'Бебреш', N'България, Бебреш', N'BG, BG0000374')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (109, 23, N'BG0000377', N'Калимок - Бръшлен', N'България, Калимок - Бръшлен', N'BG, BG0000377')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (110, 23, N'BG0000382', N'Шуменско плато', N'България, Шуменско плато', N'BG, BG0000382')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (111, 23, N'BG0000393', N'Екокоридор Камчия - Емине', N'България, Екокоридор Камчия - Емине', N'BG, BG0000393')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (112, 23, N'BG0000396', N'Персина', N'България, Персина', N'BG, BG0000396')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (113, 23, N'BG0000399', N'Българка', N'България, Българка', N'BG, BG0000399')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (114, 23, N'BG0000401', N'Свети Илийски възвишения', N'България, Свети Илийски възвишения', N'BG, BG0000401')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (115, 23, N'BG0000402', N'Бакаджиците', N'България, Бакаджиците', N'BG, BG0000402')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (116, 23, N'BG0000418', N'Керменски възвишения', N'България, Керменски възвишения', N'BG, BG0000418')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (117, 23, N'BG0000420', N'Гребенец', N'България, Гребенец', N'BG, BG0000420')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (118, 23, N'BG0000421', N'Преславска планина', N'България, Преславска планина', N'BG, BG0000421')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (119, 23, N'BG0000424', N'Река Въча - Тракия', N'България, Река Въча - Тракия', N'BG, BG0000424')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (120, 23, N'BG0000425', N'Река Съзлийка', N'България, Река Съзлийка', N'BG, BG0000425')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (121, 23, N'BG0000426', N'Река Луда Яна', N'България, Река Луда Яна', N'BG, BG0000426')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (122, 23, N'BG0000427', N'Река Овчарица', N'България, Река Овчарица', N'BG, BG0000427')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (123, 23, N'BG0000429', N'Река Стряма', N'България, Река Стряма', N'BG, BG0000429')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (124, 23, N'BG0000432', N'Голяма река', N'България, Голяма река', N'BG, BG0000432')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (125, 23, N'BG0000434', N'Банска река', N'България, Банска река', N'BG, BG0000434')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (126, 23, N'BG0000435', N'Река Каялийка', N'България, Река Каялийка', N'BG, BG0000435')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (127, 23, N'BG0000436', N'Река Мечка', N'България, Река Мечка', N'BG, BG0000436')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (128, 23, N'BG0000437', N'Река Черкезица', N'България, Река Черкезица', N'BG, BG0000437')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (129, 23, N'BG0000438', N'Река Чинардере', N'България, Река Чинардере', N'BG, BG0000438')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (130, 23, N'BG0000440', N'Река Соколица', N'България, Река Соколица', N'BG, BG0000440')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (131, 23, N'BG0000441', N'Река Блатница', N'България, Река Блатница', N'BG, BG0000441')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (132, 23, N'BG0000442', N'Река Мартинка', N'България, Река Мартинка', N'BG, BG0000442')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (133, 23, N'BG0000443', N'Река Омуровска', N'България, Река Омуровска', N'BG, BG0000443')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (134, 23, N'BG0000444', N'Река Пясъчник', N'България, Река Пясъчник', N'BG, BG0000444')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (135, 23, N'BG0000487', N'Божите мостове', N'България, Божите мостове', N'BG, BG0000487')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (136, 23, N'BG0000494', N'Централен Балкан', N'България, Централен Балкан', N'BG, BG0000494')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (137, 23, N'BG0000495', N'Рила', N'България, Рила', N'BG, BG0000495')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (138, 23, N'BG0000496', N'Рилски манастир', N'България, Рилски манастир', N'BG, BG0000496')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (139, 23, N'BG0000497', N'Арчар', N'България, Арчар', N'BG, BG0000497')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (140, 23, N'BG0000498', N'Видбол', N'България, Видбол', N'BG, BG0000498')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (141, 23, N'BG0000500', N'Войница', N'България, Войница', N'BG, BG0000500')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (142, 23, N'BG0000501', N'Голяма Камчия', N'България, Голяма Камчия', N'BG, BG0000501')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (143, 23, N'BG0000503', N'Река Лом', N'България, Река Лом', N'BG, BG0000503')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (144, 23, N'BG0000507', N'Делейна', N'България, Делейна', N'BG, BG0000507')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (145, 23, N'BG0000508', N'Река Скът', N'България, Река Скът', N'BG, BG0000508')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (146, 23, N'BG0000509', N'Цибрица', N'България, Цибрица', N'BG, BG0000509')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (147, 23, N'BG0000513', N'Войнишки Бакаджик', N'България, Войнишки Бакаджик', N'BG, BG0000513')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (148, 23, N'BG0000516', N'Черната могила', N'България, Черната могила', N'BG, BG0000516')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (149, 23, N'BG0000517', N'Портитовци - Владимирово', N'България, Портитовци - Владимирово', N'BG, BG0000517')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (150, 23, N'BG0000518', N'Въртопски дол', N'България, Въртопски дол', N'BG, BG0000518')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (151, 23, N'BG0000519', N'Моминбродско блато', N'България, Моминбродско блато', N'BG, BG0000519')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (152, 23, N'BG0000521', N'Макреш', N'България, Макреш', N'BG, BG0000521')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (153, 23, N'BG0000522', N'Видински парк', N'България, Видински парк', N'BG, BG0000522')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (154, 23, N'BG0000523', N'Шишенци', N'България, Шишенци', N'BG, BG0000523')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (155, 23, N'BG0000524', N'Оризището', N'България, Оризището', N'BG, BG0000524')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (156, 23, N'BG0000525', N'Тимок', N'България, Тимок', N'BG, BG0000525')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (157, 23, N'BG0000526', N'Долно Линево', N'България, Долно Линево', N'BG, BG0000526')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (158, 23, N'BG0000527', N'Козлодуй', N'България, Козлодуй', N'BG, BG0000527')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (159, 23, N'BG0000528', N'Островска степ - Вадин', N'България, Островска степ - Вадин', N'BG, BG0000528')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (160, 23, N'BG0000529', N'Мартен - Ряхово', N'България, Мартен - Ряхово', N'BG, BG0000529')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (161, 23, N'BG0000530', N'Пожарево - Гарван', N'България, Пожарево - Гарван', N'BG, BG0000530')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (162, 23, N'BG0000532', N'Остров Близнаци', N'България, Остров Близнаци', N'BG, BG0000532')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (163, 23, N'BG0000533', N'Острови Козлодуй', N'България, Острови Козлодуй', N'BG, BG0000533')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (164, 23, N'BG0000534', N'Остров Чайка', N'България, Остров Чайка', N'BG, BG0000534')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (165, 23, N'BG0000539', N'Гора Тополяне', N'България, Гора Тополяне', N'BG, BG0000539')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (166, 23, N'BG0000552', N'Остров Кутово', N'България, Остров Кутово', N'BG, BG0000552')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (167, 23, N'BG0000553', N'Гора Тополчане', N'България, Гора Тополчане', N'BG, BG0000553')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (168, 23, N'BG0000554', N'Гора Желю Войвода', N'България, Гора Желю Войвода', N'BG, BG0000554')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (169, 23, N'BG0000567', N'Гора Блатец', N'България, Гора Блатец', N'BG, BG0000567')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (170, 23, N'BG0000569', N'Кардам', N'България, Кардам', N'BG, BG0000569')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (171, 23, N'BG0000570', N'Изворово - Краище', N'България, Изворово - Краище', N'BG, BG0000570')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (172, 23, N'BG0000572', N'Росица - Лозница', N'България, Росица - Лозница', N'BG, BG0000572')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (173, 23, N'BG0000573', N'Комплекс Калиакра', N'България, Комплекс Калиакра', N'BG, BG0000573')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (174, 23, N'BG0000574', N'Ахелой - Равда - Несебър', N'България, Ахелой - Равда - Несебър', N'BG, BG0000574')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (175, 23, N'BG0000576', N'Свищовска гора', N'България, Свищовска гора', N'BG, BG0000576')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (176, 23, N'BG0000578', N'Река Марица', N'България, Река Марица', N'BG, BG0000578')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (177, 23, N'BG0000587', N'Варкан', N'България, Варкан', N'BG, BG0000587')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (178, 23, N'BG0000589', N'Марина дупка', N'България, Марина дупка', N'BG, BG0000589')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (179, 23, N'BG0000591', N'Седларката', N'България, Седларката', N'BG, BG0000591')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (180, 23, N'BG0000593', N'Билерниците', N'България, Билерниците', N'BG, BG0000593')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (181, 23, N'BG0000594', N'Божия мост - Понора', N'България, Божия мост - Понора', N'BG, BG0000594')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (182, 23, N'BG0000601', N'Каленска пещера', N'България, Каленска пещера', N'BG, BG0000601')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (183, 23, N'BG0000602', N'Кабиюк', N'България, Кабиюк', N'BG, BG0000602')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (184, 23, N'BG0000605', N'Божкова дупка', N'България, Божкова дупка', N'BG, BG0000605')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (185, 23, N'BG0000607', N'Пещера Микре', N'България, Пещера Микре', N'BG, BG0000607')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (186, 23, N'BG0000608', N'Ломовете', N'България, Ломовете', N'BG, BG0000608')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (187, 23, N'BG0000609', N'Река Росица', N'България, Река Росица', N'BG, BG0000609')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (188, 23, N'BG0000610', N'Река Янтра', N'България, Река Янтра', N'BG, BG0000610')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (189, 23, N'BG0000611', N'Язовир Горни Дъбник', N'България, Язовир Горни Дъбник', N'BG, BG0000611')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (190, 23, N'BG0000612', N'Река Блягорница', N'България, Река Блягорница', N'BG, BG0000612')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (191, 23, N'BG0000613', N'Река Искър', N'България, Река Искър', N'BG, BG0000613')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (192, 23, N'BG0000614', N'Река Огоста', N'България, Река Огоста', N'BG, BG0000614')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (193, 23, N'BG0000615', N'Деветашко плато', N'България, Деветашко плато', N'BG, BG0000615')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (194, 23, N'BG0000616', N'Микре', N'България, Микре', N'BG, BG0000616')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (195, 23, N'BG0000617', N'Река Палакария', N'България, Река Палакария', N'BG, BG0000617')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (196, 23, N'BG0000618', N'Видима', N'България, Видима', N'BG, BG0000618')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (197, 23, N'BG0000620', N'Поморие', N'България, Поморие', N'BG, BG0000620')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (198, 23, N'BG0000621', N'Езеро Шабла - Езерец', N'България, Езеро Шабла - Езерец', N'BG, BG0000621')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (199, 23, N'BG0000622', N'Варненско-Белославски комплекс', N'България, Варненско-Белославски комплекс', N'BG, BG0000622')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (200, 23, N'BG0000623', N'Таушан тепе', N'България, Таушан тепе', N'BG, BG0000623')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (201, 23, N'BG0000624', N'Любаш', N'България, Любаш', N'BG, BG0000624')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (202, 23, N'BG0000625', N'Изворо', N'България, Изворо', N'BG, BG0000625')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (203, 23, N'BG0000626', N'Круше', N'България, Круше', N'BG, BG0000626')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (204, 23, N'BG0000627', N'Конунски дол', N'България, Конунски дол', N'BG, BG0000627')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (205, 23, N'BG0000628', N'Чирпански възвишения', N'България, Чирпански възвишения', N'BG, BG0000628')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (206, 23, N'BG0000631', N'Ново село', N'България, Ново село', N'BG, BG0000631')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (207, 23, N'BG0000635', N'Девненски хълмове', N'България, Девненски хълмове', N'BG, BG0000635')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (208, 23, N'BG0001001', N'Ропотамо', N'България, Ропотамо', N'BG, BG0001001')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (209, 23, N'BG0001004', N'Емине - Иракли', N'България, Емине - Иракли', N'BG, BG0001004')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (210, 23, N'BG0001007', N'Странджа', N'България, Странджа', N'BG, BG0001007')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (211, 23, N'BG0001011', N'Осоговска планина', N'България, Осоговска планина', N'BG, BG0001011')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (212, 23, N'BG0001012', N'Земен', N'България, Земен', N'BG, BG0001012')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (213, 23, N'BG0001013', N'Скрино', N'България, Скрино', N'BG, BG0001013')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (214, 23, N'BG0001014', N'Карлуково', N'България, Карлуково', N'BG, BG0001014')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (215, 23, N'BG0001017', N'Кървав камък', N'България, Кървав камък', N'BG, BG0001017')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (216, 23, N'BG0001021', N'Река Места', N'България, Река Места', N'BG, BG0001021')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (217, 23, N'BG0001022', N'Орановски пролом - Лешко', N'България, Орановски пролом - Лешко', N'BG, BG0001022')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (218, 23, N'BG0001023', N'Рупите - Струмешница', N'България, Рупите - Струмешница', N'BG, BG0001023')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (219, 23, N'BG0001028', N'Среден Пирин - Алиботуш', N'България, Среден Пирин - Алиботуш', N'BG, BG0001028')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (220, 23, N'BG0001030', N'Родопи - Западни', N'България, Родопи - Западни', N'BG, BG0001030')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (221, 23, N'BG0001031', N'Родопи - Средни', N'България, Родопи - Средни', N'BG, BG0001031')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (222, 23, N'BG0001032', N'Родопи - Източни', N'България, Родопи - Източни', N'BG, BG0001032')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (223, 23, N'BG0001033', N'Брестовица', N'България, Брестовица', N'BG, BG0001033')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (224, 23, N'BG0001034', N'Остър камък', N'България, Остър камък', N'BG, BG0001034')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (225, 23, N'BG0001036', N'Български извор', N'България, Български извор', N'BG, BG0001036')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (226, 23, N'BG0001037', N'Пъстрина', N'България, Пъстрина', N'BG, BG0001037')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (227, 23, N'BG0001039', N'Попинци', N'България, Попинци', N'BG, BG0001039')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (228, 23, N'BG0001040', N'Западна Стара планина и Предбалкан', N'България, Западна Стара планина и Предбалкан', N'BG, BG0001040')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (229, 23, N'BG0001042', N'Искърски пролом - Ржана', N'България, Искърски пролом - Ржана', N'BG, BG0001042')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (230, 23, N'BG0001043', N'Етрополе - Байлово', N'България, Етрополе - Байлово', N'BG, BG0001043')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (231, 23, N'BG0001307', N'Плана', N'България, Плана', N'BG, BG0001307')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (232, 23, N'BG0001375', N'Острица', N'България, Острица', N'BG, BG0001375')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (233, 23, N'BG0001386', N'Яденица', N'България, Яденица', N'BG, BG0001386')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (234, 23, N'BG0001389', N'Средна гора', N'България, Средна гора', N'BG, BG0001389')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (235, 23, N'BG0001493', N'Централен Балкан - буфер', N'България, Централен Балкан - буфер', N'BG, BG0001493')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (236, 23, N'BG0001500', N'Аладжа банка', N'България, Аладжа банка', N'BG, BG0001500')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (237, 23, N'BG0001501', N'Емона', N'България, Емона', N'BG, BG0001501')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (238, 23, N'BG0001502', N'Отманли', N'България, Отманли', N'BG, BG0001502')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (239, 23, N'BG0002001', N'Раяновци', N'България, Раяновци', N'BG, BG0002001')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (240, 23, N'BG0002002', N'Западен Балкан', N'България, Западен Балкан', N'BG, BG0002002')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (241, 23, N'BG0002003', N'Кресна', N'България, Кресна', N'BG, BG0002003')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (242, 23, N'BG0002004', N'Долни Богров - Казичене', N'България, Долни Богров - Казичене', N'BG, BG0002004')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (243, 23, N'BG0002005', N'Понор', N'България, Понор', N'BG, BG0002005')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (244, 23, N'BG0002006', N'Рибарници Орсоя', N'България, Рибарници Орсоя', N'BG, BG0002006')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (245, 23, N'BG0002007', N'Остров Ибиша', N'България, Остров Ибиша', N'BG, BG0002007')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (246, 23, N'BG0002008', N'Остров до Горни Цибър', N'България, Остров до Горни Цибър', N'BG, BG0002008')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (247, 23, N'BG0002009', N'Златията', N'България, Златията', N'BG, BG0002009')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (248, 23, N'BG0002010', N'Язовир Пясъчник', N'България, Язовир Пясъчник', N'BG, BG0002010')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (249, 23, N'BG0002012', N'Крумовица', N'България, Крумовица', N'BG, BG0002012')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (250, 23, N'BG0002013', N'Студен кладенец', N'България, Студен кладенец', N'BG, BG0002013')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (251, 23, N'BG0002014', N'Маджарово', N'България, Маджарово', N'BG, BG0002014')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (252, 23, N'BG0002015', N'Язовир Конуш', N'България, Язовир Конуш', N'BG, BG0002015')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (253, 23, N'BG0002016', N'Рибарници Пловдив', N'България, Рибарници Пловдив', N'BG, BG0002016')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (254, 23, N'BG0002017', N'Комплекс Беленски острови', N'България, Комплекс Беленски острови', N'BG, BG0002017')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (255, 23, N'BG0002018', N'Остров Вардим', N'България, Остров Вардим', N'BG, BG0002018')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (256, 23, N'BG0002019', N'Бяла река', N'България, Бяла река', N'BG, BG0002019')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (257, 23, N'BG0002020', N'Радинчево', N'България, Радинчево', N'BG, BG0002020')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (258, 23, N'BG0002021', N'Сакар', N'България, Сакар', N'BG, BG0002021')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (259, 23, N'BG0002022', N'Язовир Розов кладенец', N'България, Язовир Розов кладенец', N'BG, BG0002022')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (260, 23, N'BG0002023', N'Язовир Овчарица', N'България, Язовир Овчарица', N'BG, BG0002023')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (261, 23, N'BG0002024', N'Рибарници Мечка', N'България, Рибарници Мечка', N'BG, BG0002024')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (262, 23, N'BG0002025', N'Ломовете', N'България, Ломовете', N'BG, BG0002025')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (263, 23, N'BG0002026', N'Дервентски възвишения', N'България, Дервентски възвишения', N'BG, BG0002026')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (264, 23, N'BG0002027', N'Язовир Малко Шарково', N'България, Язовир Малко Шарково', N'BG, BG0002027')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (265, 23, N'BG0002028', N'Комплекс Стралджа', N'България, Комплекс Стралджа', N'BG, BG0002028')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (266, 23, N'BG0002029', N'Котленска планина', N'България, Котленска планина', N'BG, BG0002029')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (267, 23, N'BG0002030', N'Комплекс Калимок', N'България, Комплекс Калимок', N'BG, BG0002030')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (268, 23, N'BG0002031', N'Стената', N'България, Стената', N'BG, BG0002031')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (269, 23, N'BG0002038', N'Провадийско-Роякско плато', N'България, Провадийско-Роякско плато', N'BG, BG0002038')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (270, 23, N'BG0002039', N'Хърсовска река', N'България, Хърсовска река', N'BG, BG0002039')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (271, 23, N'BG0002040', N'Странджа', N'България, Странджа', N'BG, BG0002040')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (272, 23, N'BG0002041', N'Комплекс Ропотамо', N'България, Комплекс Ропотамо', N'BG, BG0002041')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (273, 23, N'BG0002043', N'Емине', N'България, Емине', N'BG, BG0002043')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (274, 23, N'BG0002044', N'Камчийска планина', N'България, Камчийска планина', N'BG, BG0002044')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (275, 23, N'BG0002045', N'Комплекс Камчия', N'България, Комплекс Камчия', N'BG, BG0002045')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (276, 23, N'BG0002046', N'Ятата', N'България, Ятата', N'BG, BG0002046')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (277, 23, N'BG0002048', N'Суха река', N'България, Суха река', N'BG, BG0002048')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (278, 23, N'BG0002050', N'Дуранкулашко езеро', N'България, Дуранкулашко езеро', N'BG, BG0002050')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (279, 23, N'BG0002051', N'Калиакра', N'България, Калиакра', N'BG, BG0002051')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (280, 23, N'BG0002052', N'Язовир Жребчево', N'България, Язовир Жребчево', N'BG, BG0002052')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (281, 23, N'BG0002053', N'Врачански Балкан', N'България, Врачански Балкан', N'BG, BG0002053')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (282, 23, N'BG0002054', N'Средна гора', N'България, Средна гора', N'BG, BG0002054')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (283, 23, N'BG0002057', N'Бесапарски ридове', N'България, Бесапарски ридове', N'BG, BG0002057')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (284, 23, N'BG0002058', N'Сините камъни - Гребенец', N'България, Сините камъни - Гребенец', N'BG, BG0002058')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (285, 23, N'BG0002059', N'Каменски баир', N'България, Каменски баир', N'BG, BG0002059')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (286, 23, N'BG0002060', N'Галата', N'България, Галата', N'BG, BG0002060')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (287, 23, N'BG0002061', N'Балчик', N'България, Балчик', N'BG, BG0002061')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (288, 23, N'BG0002062', N'Лудогорие', N'България, Лудогорие', N'BG, BG0002062')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (289, 23, N'BG0002063', N'Западни Родопи', N'България, Западни Родопи', N'BG, BG0002063')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (290, 23, N'BG0002064', N'Гарванско блато', N'България, Гарванско блато', N'BG, BG0002064')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (291, 23, N'BG0002065', N'Блато Малък Преславец', N'България, Блато Малък Преславец', N'BG, BG0002065')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (292, 23, N'BG0002066', N'Западна Странджа', N'България, Западна Странджа', N'BG, BG0002066')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (293, 23, N'BG0002067', N'Остров Голя', N'България, Остров Голя', N'BG, BG0002067')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (294, 23, N'BG0002069', N'Рибарници Звъничево', N'България, Рибарници Звъничево', N'BG, BG0002069')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (295, 23, N'BG0002070', N'Рибарници Хаджи Димитрово', N'България, Рибарници Хаджи Димитрово', N'BG, BG0002070')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (296, 23, N'BG0002071', N'Мост Арда', N'България, Мост Арда', N'BG, BG0002071')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (297, 23, N'BG0002072', N'Мелнишки пирамиди', N'България, Мелнишки пирамиди', N'BG, BG0002072')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (298, 23, N'BG0002073', N'Добростан', N'България, Добростан', N'BG, BG0002073')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (299, 23, N'BG0002074', N'Никополско плато', N'България, Никополско плато', N'BG, BG0002074')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (300, 23, N'BG0002076', N'Места', N'България, Места', N'BG, BG0002076')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (301, 23, N'BG0002077', N'Бакърлъка', N'България, Бакърлъка', N'BG, BG0002077')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (302, 23, N'BG0002078', N'Славянка', N'България, Славянка', N'BG, BG0002078')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (303, 23, N'BG0002079', N'Осогово', N'България, Осогово', N'BG, BG0002079')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (304, 23, N'BG0002081', N'Марица - Първомай', N'България, Марица - Първомай', N'BG, BG0002081')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (305, 23, N'BG0002082', N'Батова', N'България, Батова', N'BG, BG0002082')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (306, 23, N'BG0002083', N'Свищовско-Беленска низина', N'България, Свищовско-Беленска низина', N'BG, BG0002083')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (307, 23, N'BG0002084', N'Палакария', N'България, Палакария', N'BG, BG0002084')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (308, 23, N'BG0002085', N'Чаиря', N'България, Чаиря', N'BG, BG0002085')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (309, 23, N'BG0002086', N'Оризища Цалапица', N'България, Оризища Цалапица', N'BG, BG0002086')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (310, 23, N'BG0002087', N'Марица - Пловдив', N'България, Марица - Пловдив', N'BG, BG0002087')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (311, 23, N'BG0002088', N'Микре', N'България, Микре', N'BG, BG0002088')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (312, 23, N'BG0002089', N'Ноевци', N'България, Ноевци', N'BG, BG0002089')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (313, 23, N'BG0002090', N'Берковица', N'България, Берковица', N'BG, BG0002090')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (314, 23, N'BG0002091', N'Остров Лакът', N'България, Остров Лакът', N'BG, BG0002091')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (315, 23, N'BG0002092', N'Харманлийска река', N'България, Харманлийска река', N'BG, BG0002092')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (316, 23, N'BG0002093', N'Овчарово', N'България, Овчарово', N'BG, BG0002093')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (317, 23, N'BG0002094', N'Адата - Тунджа', N'България, Адата - Тунджа', N'BG, BG0002094')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (318, 23, N'BG0002095', N'Горни Дъбник - Телиш', N'България, Горни Дъбник - Телиш', N'BG, BG0002095')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (319, 23, N'BG0002096', N'Обнова', N'България, Обнова', N'BG, BG0002096')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (320, 23, N'BG0002097', N'Белите скали', N'България, Белите скали', N'BG, BG0002097')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (321, 23, N'BG0002098', N'Рупите', N'България, Рупите', N'BG, BG0002098')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (322, 23, N'BG0002099', N'Кочериново', N'България, Кочериново', N'BG, BG0002099')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (323, 23, N'BG0002100', N'Долна Козница', N'България, Долна Козница', N'BG, BG0002100')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (324, 23, N'BG0002101', N'Мещица', N'България, Мещица', N'BG, BG0002101')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (325, 23, N'BG0002102', N'Деветашко плато', N'България, Деветашко плато', N'BG, BG0002102')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (326, 23, N'BG0002103', N'Злато поле', N'България, Злато поле', N'BG, BG0002103')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (327, 23, N'BG0002104', N'Цибърско блато', N'България, Цибърско блато', N'BG, BG0002104')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (328, 23, N'BG0002105', N'Персенк', N'България, Персенк', N'BG, BG0002105')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (329, 23, N'BG0002106', N'Язовир Ивайловград', N'България, Язовир Ивайловград', N'BG, BG0002106')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (330, 23, N'BG0002107', N'Бобошево', N'България, Бобошево', N'BG, BG0002107')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (331, 23, N'BG0002108', N'Скрино', N'България, Скрино', N'BG, BG0002108')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (332, 23, N'BG0002109', N'Васильовска планина', N'България, Васильовска планина', N'BG, BG0002109')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (333, 23, N'BG0002110', N'Априлци', N'България, Априлци', N'BG, BG0002110')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (334, 23, N'BG0002111', N'Велчево', N'България, Велчево', N'BG, BG0002111')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (335, 23, N'BG0002112', N'Руй', N'България, Руй', N'BG, BG0002112')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (336, 23, N'BG0002113', N'Триград - Мурсалица', N'България, Триград - Мурсалица', N'BG, BG0002113')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (337, 23, N'BG0002114', N'Рибарници Челопечене', N'България, Рибарници Челопечене', N'BG, BG0002114')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (338, 23, N'BG0002115', N'Било', N'България, Било', N'BG, BG0002115')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (339, 23, N'BG0002126', N'Пирин буфер', N'България, Пирин буфер', N'BG, BG0002126')
GO
INSERT [dbo].[Regions] ([RegionId], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (340, 23, N'BG0002128', N'Централен Балкан буфер', N'България, Централен Балкан буфер', N'BG, BG0002128')
GO
SET IDENTITY_INSERT [Regions] OFF

GO

-- update Nuts1s
update Nuts1s set FullPathName = 
(
	select c.Name + ', ' + n1.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	where Nuts1s.Nuts1Id = n1.Nuts1Id
)

update Nuts1s set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	where Nuts1s.Nuts1Id = n1.Nuts1Id
)

-- update Nuts2s
update Nuts2s set FullPathName = 
(
	select c.Name + ', ' + n1.Name + ', ' + n2.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	where Nuts2s.Nuts2Id = n2.Nuts2Id
)

update Nuts2s set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode + ', ' + n2.NutsCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	where Nuts2s.Nuts2Id = n2.Nuts2Id
)

-- update Districts
UPDATE [dbo].[Districts] SET NutsCode = N'BG413' WHERE DistrictId = 1
UPDATE [dbo].[Districts] SET NutsCode = N'BG341' WHERE DistrictId = 2
UPDATE [dbo].[Districts] SET NutsCode = N'BG331' WHERE DistrictId = 3
UPDATE [dbo].[Districts] SET NutsCode = N'BG321' WHERE DistrictId = 4
UPDATE [dbo].[Districts] SET NutsCode = N'BG311' WHERE DistrictId = 5
UPDATE [dbo].[Districts] SET NutsCode = N'BG313' WHERE DistrictId = 6
UPDATE [dbo].[Districts] SET NutsCode = N'BG322' WHERE DistrictId = 7
UPDATE [dbo].[Districts] SET NutsCode = N'BG332' WHERE DistrictId = 8
UPDATE [dbo].[Districts] SET NutsCode = N'BG425' WHERE DistrictId = 9
UPDATE [dbo].[Districts] SET NutsCode = N'BG415' WHERE DistrictId = 10
UPDATE [dbo].[Districts] SET NutsCode = N'BG315' WHERE DistrictId = 11
UPDATE [dbo].[Districts] SET NutsCode = N'BG312' WHERE DistrictId = 12
UPDATE [dbo].[Districts] SET NutsCode = N'BG423' WHERE DistrictId = 13
UPDATE [dbo].[Districts] SET NutsCode = N'BG414' WHERE DistrictId = 14
UPDATE [dbo].[Districts] SET NutsCode = N'BG314' WHERE DistrictId = 15
UPDATE [dbo].[Districts] SET NutsCode = N'BG421' WHERE DistrictId = 16
UPDATE [dbo].[Districts] SET NutsCode = N'BG324' WHERE DistrictId = 17
UPDATE [dbo].[Districts] SET NutsCode = N'BG323' WHERE DistrictId = 18
UPDATE [dbo].[Districts] SET NutsCode = N'BG325' WHERE DistrictId = 19
UPDATE [dbo].[Districts] SET NutsCode = N'BG342' WHERE DistrictId = 20
UPDATE [dbo].[Districts] SET NutsCode = N'BG424' WHERE DistrictId = 21
UPDATE [dbo].[Districts] SET NutsCode = N'BG411' WHERE DistrictId = 22
UPDATE [dbo].[Districts] SET NutsCode = N'BG412' WHERE DistrictId = 23
UPDATE [dbo].[Districts] SET NutsCode = N'BG344' WHERE DistrictId = 24
UPDATE [dbo].[Districts] SET NutsCode = N'BG334' WHERE DistrictId = 25
UPDATE [dbo].[Districts] SET NutsCode = N'BG422' WHERE DistrictId = 26
UPDATE [dbo].[Districts] SET NutsCode = N'BG333' WHERE DistrictId = 27
UPDATE [dbo].[Districts] SET NutsCode = N'BG343' WHERE DistrictId = 28

update Districts set FullPathName = 
(
	select c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	where Districts.DistrictId = d.DistrictId
)

update Districts set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode + ', ' + n2.NutsCode + ', ' + d.NutsCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	where Districts.DistrictId = d.DistrictId
)

-- update Municipalities
update Municipalities set FullPathName = 
(
	select c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name + ', ' + m.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	inner join Municipalities m on m.DistrictId = d.DistrictId
	where Municipalities.MunicipalityId = m.MunicipalityId
)

update Municipalities set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode + ', ' + n2.NutsCode + ', ' + d.NutsCode + ', ' + m.LauCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	inner join Municipalities m on m.DistrictId = d.DistrictId
	where Municipalities.MunicipalityId = m.MunicipalityId
)

-- update Settlements
update Settlements set FullPathName = 
(
	select c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name + ', ' + m.Name + ', ' + s.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	inner join Municipalities m on m.DistrictId = d.DistrictId
	inner join Settlements s on s.MunicipalityId = m.MunicipalityId
	where Settlements.SettlementId = s.SettlementId
)

update Settlements set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode + ', ' + n2.NutsCode + ', ' + d.NutsCode + ', ' + m.LauCode + ', ' + s.LauCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	inner join Municipalities m on m.DistrictId = d.DistrictId
	inner join Settlements s on s.MunicipalityId = m.MunicipalityId
	where Settlements.SettlementId = s.SettlementId
)

UPDATE s 
    SET [Priority] = rns.rowNumber + 1
FROM Settlements s
INNER JOIN 
    (SELECT
        SettlementId,
        row_number() OVER (ORDER BY Name) as rowNumber
    FROM Settlements) rns ON s.SettlementId = rns.SettlementId

UPDATE Settlements SET [Priority] = 1 WHERE Name = N'гр.София'