PRINT 'Insert Big Procedure'
GO

SET IDENTITY_INSERT [dbo].[Procedures] ON 
INSERT [dbo].[Procedures]
    ([ProcedureId], [Gid], [ProcedureTypeId], [ProcedureStatus], [ProcedureContractReportDocumentsSectionStatus], [ApplicationFormType], [AllowedRegistrationType], [ListingDate], [Code], [Name], [NameAlt], [Description], [DescriptionAlt], [ProjectMinAmount], [ProjectMaxAmount], [ProjectDuration], [IsIntegrated], [CreateDate], [ModifyDate], [AllowConcurrancyContractReports])
VALUES
    (2, N'cae66cb6-4075-4acf-b0a4-643858379e79', 1, 4, 1, 1, 3, CAST(0x07000000000082380B AS DateTime2), N'BG05SFOP001-1.2014.001', N'Голяма тестова процедура', N'Big Test Procedure', N'Управляващият орган на оперативна програма „Административен капацитет” (ОПАК), съфинансирана от Европейския социален фонд, обявява нова процедура за предоставяне на безвъзмездна финансова помощ, предназначена за централните администрации.
    
    Процедурата е по приоритетна ос ІІІ „Качествено административно обслужване и развитие на електронното управление”, подприоритет 3.2. „Стандартна информационно-комуникационна среда и оперативна съвместимост”, бюджетна линия BG051PO002/13/3.2-04.
    
    Ще бъдат финансирани проекти за разширяване функционалността на единния портал за предоставяне на електронни административни услуги като точка за достъп до Единното звено за контакт (ЕЗК), както и за осигуряване на оперативна съвместимост на информационните системи на администрациите на хоризонтално ниво. Общият размер на отпуснатата безвъзмездна финансова помощ е 3 млн. лв. Предвидената минимална стойност на проектите е 20 000 лв., а максималният праг е 400 000 лв.
    
    Примерни допустими дейности по процедурата са: изграждане на информационно-комуникационни системи или надграждане на съществуващи такива; разработване и внедряване на нови модули към съществуващи информационни системи; усъвършенстване на съществуващи и внедряване на нови електронни административни услуги; създаване на условия за достъп на хората с увреждания до административни услуги и други.
    
    Проектите трябва да бъдат изпълнени в рамките на 18 месеца. Крайният срок за подаване на проектните предложения е 31.05.2013 г. (петък), 17.30 часа.
    
    График за предстоящите информационни дни по процедурата предстои да бъде публикуван на интернет страницата на Управляващия орган.', NULL, 1200000.0000, 2000000.0000, 12, 1, CAST(0x07570B382C894F390B AS DateTime2), CAST(0x07D2B9A9EF8B4F390B AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Procedures] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureLocations] ON 
INSERT [dbo].[ProcedureLocations]
    ([ProcedureLocationId], [ProcedureId], [NutsLevel], [CountryId], [Nuts1Id], [Nuts2Id], [DistrictId], [MunicipalityId], [SettlementId], [ProtectedZoneId])
VALUES
    (2, 2, 1, 23, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProcedureLocations] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureTimeLimits] ON 
INSERT [dbo].[ProcedureTimeLimits]
    ([ProcedureTimeLimitId], [ProcedureId], [EndDate], [Notes])
VALUES
    (2, 2, CAST(0x0700B4284D8A96390B AS DateTime2), N'Бележки')
SET IDENTITY_INSERT [dbo].[ProcedureTimeLimits] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureShares] ON 
INSERT [dbo].[ProcedureShares]
    ([ProcedureShareId], [ProcedureId], [ProgrammeId], [ProgrammePriorityId], [FinanceSource], [EuAmount], [BgAmount], [IsPrimary], [IsActivated])
VALUES
    (3, 2, 1, 101, 1, 30231810.0000, 2668190.0000, 1, 1),
    (4, 2, 1, 102, 1, 1785000.0000, 315000.0000, 0, 1),
    (5, 2, 2, 201, 3, 900000.0000, 100000.0000, 0, 1),
    (6, 2, 2, 202, 3, 1000000.0000, 200000, 0, 1)
SET IDENTITY_INSERT [dbo].[ProcedureShares] OFF
GO

INSERT [dbo].[ProcedureInvestmentPriorities]
    ([ProcedureId], [InvestmentPriorityId])
VALUES
    (2, 10101),
    (2, 20101),
    (2, 20201)
GO

INSERT [dbo].[ProcedureSpecificTargets]
    ([ProcedureId], [SpecificTargetId])
VALUES
    (2, 1010101),
    (2, 2010101),
    (2, 2020101)
GO

INSERT [dbo].[ProcedureProgrammes]
    ([ProcedureId], [ProgrammeId])
VALUES
    (2, 1),
    (2, 2)
GO

INSERT [dbo].[ProcedureNumbers]
    ([ProcedureId], [ProgrammePriorityId], [Number])
VALUES
    (2, 101, 1)
GO

INSERT [dbo].[ProcedureInterventionCategories]
    ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive])
VALUES
    (2, 2, 0, 1),
    (2, 4, 0, 1),
    (2, 7, 0, 1),
    (2, 124, 0, 1),
    (2, 125, 0, 1),
    (2, 128, 0, 1),
    (2, 130, 0, 1),
    (2, 131, 0, 1),
    (2, 132, 0, 1),
    (2, 133, 0, 1),
    (2, 134, 0, 1),
    (2, 135, 0, 1),
    (2, 138, 0, 1),
    (2, 139, 0, 1),
    (2, 145, 0, 1),
    (2, 146, 0, 1),
    (2, 147, 0, 1),
    (2, 157, 0, 1),
    (2, 165, 0, 1),
    (2, 166, 0, 1)
GO


SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel1] ON 
INSERT [dbo].[ProcedureBudgetLevel1]
    ([ProcedureBudgetLevel1Id], [ProcedureId], [ProgrammeId], [ExpenseTypeId], [Gid], [OrderNum], [IsActivated], [IsActive])
VALUES
    (3, 2, 1, 1, N'3ae1da29-4619-4c70-869d-6c1b7f7c94fd',1, 0, 1),
    (4, 2, 1, 2, N'83bbdc77-e28a-472e-a281-bdca178d1179',2, 0, 1),
    (5, 2, 1, 3, N'90181067-23eb-4c66-adbf-64cb79ed4885',3, 0, 1),
    (6, 2, 1, 4, N'ad6cff9a-a901-4a5e-9da5-6bacbac67a2d',4, 0, 1),
    (7, 2, 1, 5, N'bc4c63d5-4bf9-4d09-b49a-61360cb8f86b',5, 0, 1),
    (8, 2, 1, 6, N'85aa4f89-7be9-4a1b-a825-ab3110ee547a',6, 0, 1),
    (9, 2, 1, 7, N'6455398a-f2a9-4953-b084-a6dffba39c9a',7, 0, 1),
    (10, 2, 2, 8, N'd87ce17f-1f47-4c08-8feb-ad2f66f12710',1, 0, 1),
    (11, 2, 2, 9, N'47d00a19-82e8-4258-b101-39e12582b27d',2, 0, 1)

SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel1] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel2] ON 
INSERT [dbo].[ProcedureBudgetLevel2]
    ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive])
VALUES
    (7,  4,  3, N'Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)', N'49270358-1f9d-477a-8e1f-5ba35e9dbaa1', 1, 3, 1, 0, 1),
    (8,  3,  3, N'Разходи за командировки', N'b452f44f-f8be-4cd3-95d1-997fafe6637f', 2, 2, 1, 0, 1),
    (9,  4,  4, N'Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)', N'35a3927b-d2eb-4ae7-88fe-65035bfe32e1', 1, 1, 1, 0, 1),
    (10, 4,  4, N'Разходи за командировки', N'002e179d-facc-41c0-9be1-1e1e8184cf36', 2, 1, 1, 0, 1),
    (11, 3,  5, N'Закупуване на оборудване, съоръжения и обзавеждане', N'06a17711-c21a-49f8-9c7a-4c9d35a0bd80', 1, 3, 0, 0, 1),
    (12, 3,  5, N'Закупуване на машини', N'd2c97c41-d370-4242-8270-a1f76b553779', 2, 2, 0, 0, 1),
    (13, 3,  5, N'Закупуване на земя', N'4536e955-6c64-43ff-b256-bfc6ff724a41', 3, 2, 0, 0, 1),
    (14, 3,  5, N'Други (моля уточнете)', N'99fe45e3-79a0-4822-a1bd-e99f879105c3', 4, 1, 0, 0, 1),
    (15, 3,  6, N'Закупуване на патенти, лицензи, права', N'84504a1e-9f00-4b49-bb5e-5658ad533739', 1, 3, 1, 0, 1),
    (16, 3,  6, N'Закупуване на софтуер', N'ffff6aa3-a1c5-4873-a742-3eaaa88f7bd1', 2, 2, 1, 0, 1),
    (17, 3,  6, N'Други (моля уточнете) ', N'307d115b-2144-4630-8d20-1f7bb674bdb9', 3, 2, 1, 0, 1),
    (18, 3,  7, N'Разходи за СМР сгради', N'ab1744bd-9bbe-46f6-9dd8-06c97a05911b', 1, 1, 1, 0, 1),
    (19, 3,  7, N'СМР инфраструктура', N'9a597b99-2e33-4473-9d8e-ba536dd64202', 2, 1, 1, 0, 1),
    (20, 3,  7, N'Други (моля уточнете)', N'2f2d5d4e-17b1-476d-8211-8bb23a543606', 3, 3, 1, 0, 1),
    (21, 3,  8, N'Разходи за проектиране, проучвания, анализи, стратегии, програми и т.н.', N'0d873223-e256-4842-87d1-059cfed88d04', 1, 1, 1, 0, 1),
    (22, 3,  8, N'Разходи за организиране на и/или участие в събития (международни изложения, панаири и т.н.)', N'e37e2942-46eb-47d0-8bb3-47bd7c37eedd', 2, 3, 1, 0, 1),
    (23, 3,  8, N'Разходи за обучения', N'e07b07c2-749c-4b9e-a5f2-213e439ac95c', 3, 3, 1, 0, 1),
    (24, 3,  8, N'Разходи за маркетинг и реклама', N'b170bdd4-22cf-4193-b07c-9e46ed857e69', 4, 2, 1, 0, 1),
    (25, 3,  8, N'Разходи за одит на проекта', N'bf1cc8b9-a382-490b-82e4-a3dc8047b24a', 5, 2, 1, 0, 1),
    (26, 3,  8, N'Други услуги - моля опишете', N'dffcc962-c2d5-4b9c-b034-9793a275ac96', 6, 2, 1, 0, 1),
    (27, 3,  9, N'Разходи за материали за публичност и визуализация', N'75e559ce-2391-4fcc-8da6-76043cf3758d', 1, 2, 1, 0, 1),
    (28, 3,  9, N'Разходи за дейности по информиране и публичност за проекта, съфинансиран по Оперативната програма ', N'9c1d4cfb-de41-4e0a-8018-d0330d064b64', 2, 3, 1, 0, 1),
    (29, 5, 10, N'Разходи за закупуване на материали ', N'7575091e-0f2a-4618-88ad-69309331b0b0', 1, 3, 1, 0, 1),
    (30, 5, 10, N'Разходи за закупуване на консумативи', N'edb1e801-60fa-4700-a096-9a1577bbe256', 2, 3, 1, 0, 1),
    (31, 5, 11, N'Разходи по стандартна таблица на разходите за единица продукт', N'bbaefda2-8753-45ea-a4bc-c9c87a4c59f6', 1, 2, 1, 0, 1),
    (32, 5, 11, N'Еднократни суми, които не надхвърлят левовата равностойност на 100 000 евро публичен принос', N'4714e131-6184-4a37-bb46-f56757b41dc5', 2, 3, 1, 0, 1),
    (33, 5, 11, N'Финансиране с единна ставка', N'40423568-9d24-452a-82a8-1c92ab5fa822', 3, 3, 1, 0, 1),
    (34, 5, 11, N'Разходи за амортизация ', N'7e8a3535-4431-43ca-885e-021bac19c642', 4, 3, 1, 0, 1)

SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel2] OFF
GO


SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel3] ON 
INSERT [dbo].[ProcedureBudgetLevel3]
    ([ProcedureBudgetLevel3Id], [ProcedureBudgetLevel2Id], [Gid], [Note], [OrderNum])
VALUES
    (4,  7, N'ceea5ec3-afbd-4066-93bc-998b7398ad02', N'Разходи за ръководител на проекта', 1),
    (5,  7, N'0790cf61-d341-465e-ba73-e4151eff2e94', N'Технически и финансов персонал', 2),
    (6,  7, N'90cd9171-3c8b-4d03-9785-2692c138b156', N'Административен/помощен персонал', 3),
    (7,  7, N'0f006bf5-ee32-45e8-8251-25674d768082', N'Други (моля уточнете)', 4),
    (8,  8, N'de3bc642-f4df-489d-b40d-42426e9f2218', N'Дневни', 1),
    (9,  8, N'809a984f-f351-42bb-8d93-6de67d51ab61', N'Квартирни', 2),
    (10, 8, N'd1bfdfb6-541c-4653-abe7-4cc235ca8db8', N'Пътни', 3),
    (11, 9, N'13740b3a-bf0a-4350-8be1-8e5ffddfbab5', N'Възнаграждения и осигуровки от страна на работодателя за експерти, пряко свързани с основните дейности', 1),
    (12, 9, N'df6e085c-922b-4dd7-92f6-111120f43d7a', N'Други (моля уточнете)', 2),
    (13, 10, N'2c0e34a0-1b84-47f0-aca9-b79abaa642c0', N'Дневни', 1),
    (14, 10, N'c1a4d555-5b95-4946-807f-4575edc94937', N'Квартирни', 2),
    (15, 10, N'24ee3591-0872-4341-b85a-ae66c6f10f3a', N'Пътни', 3),
    (16, 11, N'a0485ebe-6bcb-4ede-bf0d-170cd1da4e9d', N'Вид/ тип оборудване', 1),
    (17, 12, N'9b06e654-7c4f-4245-979a-10c1a831329c', N'Вид / тип машини', 1),
    (18, 13, N'454c48ee-60a4-4935-980f-112c0b2d11c9', N'Покупка на земя', 1),
    (19, 13, N'7d110bf0-6c5a-41fc-abfc-a94b3be1b4de', N'Разходи, съпътстващи покупката на земя', 2),
    (20, 15, N'df7945c2-19d9-42c1-9912-43fb759064fe', N'Конкретен вид / тип', 1),
    (21, 16, N'd42a2925-0425-4735-aa1c-5fb4b67299dd', N'Конкретен тип / вид', 1),
    (22, 18, N'6190416d-ccf0-49c7-b5bb-4b4b08e10bd8', N'Изграждане на ПСОВ', 1),
    (23, 18, N'9a0c72d4-8611-4c4c-bf15-ca29520e100c', N'Реконструкция на училище', 2),
    (24, 19, N'a1ab3c94-e3b8-42f8-94bc-a495a59bf8fb', N'Изграждане на водопровод', 1),
    (25, 19, N'fb3f6955-e14d-4f94-a5d2-f800792d581d', N'Изграждане на канализация', 2),
    (26, 21, N'9fb9a945-b7df-4e6a-8d32-541c5fd626ed', N'Разходи за прединвестиционни прочувания', 1),
    (27, 21, N'63814bf8-1ffd-4820-8286-955d08da559c', N'разходи за АРП и финансов анализ', 2),
    (28, 21, N'6e6612c7-bbfc-45fd-88b2-5e4a1cdc131b', N'разходи за експертни анализи', 3),
    (29, 21, N'cd2e3de7-ff2b-4d54-92c5-ea234b696672', N'разходи за проектиране', 4),
    (30, 21, N'b2f83bae-fff6-4fb9-abe2-d74de8595a8e', N'Разходи за правни / нотариални услуги', 5),
    (31, 22, N'64747d77-e4a6-4577-aac6-469a9eb15b86', N'разходи за участие в събития', 1),
    (32, 22, N'57b09895-a61c-466d-9663-b7b4f89ff596', N'Разходи за организиране на събития', 2),
    (33, 23, N'afbb183a-da65-437f-9b25-9c5649ebe159', N'Разходи за организиране на обучения', 1),
    (34, 23, N'50bb81cd-8211-499a-90f4-9ebd22c63cf8', N'Разходи за материали за обучения', 2),
    (35, 24, N'0dfc828c-17d2-46d4-84a1-18551272a037', N'Разходи за реклама в медиите', 1),
    (36, 24, N'94b407ca-a14a-4927-8b1b-0dbffa88ae04', N'други', 2),
    (37, 25, N'29653ec2-5008-4b99-92c2-694dacdf0461', N'Разходи за одит', 1),
    (38, 26, N'cd39a72b-ce36-4581-93e1-22f57dd19bce', N'други', 1),
    (39, 27, N'006917ce-98f5-440b-b299-ecb0a334479b', N'Разходи за изработване и разпространение на информационни и рекламни материали', 1),
    (40, 28, N'ff5fe553-29a2-423b-8a15-c4713c00be62', N'Разходи за събития', 1),
    (41, 28, N'2b76a716-c339-443f-9614-4cca38cd8303', N'Разходи за конференции', 2),
    (42, 29, N'8d61fbe9-a682-4407-a953-d7e57b1957ec', N'Разходи за конкретен вид/тип материали', 1),
    (43, 30, N'6ce02cb4-395b-4b71-ba6f-3c1a7f4bf2bb', N'разходи за конкретен вид тип консумативи', 1),
    (44, 31, N'0fd3522c-d05a-43aa-b45a-9fc47aefd3b1', N'Разход от стандартната таблица за разходи', 1),
    (45, 32, N'28f9bf4e-e5b8-4263-ada3-ea49c0108a23', N'Конкретен разход до 100 000 евро', 1),
    (46, 33, N'ec37eeca-add4-4222-bc7d-84c42c027945', N'Конкретен разход с единна ставка', 1)
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel3] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureSpecFields] ON 
INSERT [dbo].[ProcedureSpecFields]
    ([ProcedureSpecFieldId], [Gid], [ProcedureId], [Title], [Description], [IsRequired], [MaxLength], [IsActivated], [IsActive])
VALUES
    (2, N'02147dd4-2f58-49e2-baa0-f3a63e64048f', 2, N'Допълнително поле 1', N'Някакво описание.', 1, 1000, 0, 1),
    (3, N'3D4A7E9D-4D81-471A-917E-8716ECFA7FEF', 2, N'Допълнително поле 2', N'Някакво описание.', 1, 3000, 0, 1),
    (4, N'39DC29C8-A6CC-4D1B-97AD-C178C4FAA037', 2, N'Допълнително поле 3', N'Някакво описание.', 0, 5000, 0, 1),
    (5, N'0888058D-E1EB-4A1A-BCEB-A8382175B61B', 2, N'Допълнително поле 4', N'Някакво описание.', 0, 10000, 0, 1)
SET IDENTITY_INSERT [dbo].[ProcedureSpecFields] OFF
GO

INSERT [dbo].[ProcedureIndicators]
    ([ProcedureId], [IndicatorId], [BaseTotalValue], [BaseMenValue], [BaseWomenValue], [BaseYear], [TargetTotalValue], [TargetMenValue], [TargetWomenValue], [MilestoneTargetTotalValue], [MilestoneTargetMenValue], [MilestoneTargetWomenValue], [DataSource], [Description], [IsActivated], [IsActive], [SourceMapNodeId])
VALUES
    (2, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 2010101),
    (2, 56, CAST(3424234.000 AS Decimal(15, 3)), CAST(23453435.000 AS Decimal(15, 3)), CAST(345234234.000 AS Decimal(15, 3)), 2014, CAST(234234.000 AS Decimal(15, 3)), NULL, NULL, CAST(243234234.000 AS Decimal(15, 3)), NULL, NULL, NULL, NULL, 1, 1, 10101),
    (2, 185, CAST(2342342134.000 AS Decimal(15, 3)), CAST(234423.000 AS Decimal(15, 3)), CAST(234324423.000 AS Decimal(15, 3)), 2015, CAST(4564564.000 AS Decimal(15, 3)), NULL, NULL, CAST(234234.000 AS Decimal(15, 3)), NULL, NULL, NULL, NULL, 1, 1, 102),
    (2, 191, CAST(234777468.000 AS Decimal(15, 3)), CAST(543234.000 AS Decimal(15, 3)), CAST(234234234.000 AS Decimal(15, 3)), 2015, CAST(348800690.000 AS Decimal(15, 3)), CAST(3455345.000 AS Decimal(15, 3)), CAST(345345345.000 AS Decimal(15, 3)), CAST(57747468468.000 AS Decimal(15, 3)), CAST(34324234234.000 AS Decimal(15, 3)), CAST(23423234234.000 AS Decimal(15, 3)), NULL, NULL, 1, 1, 102);
GO

SET IDENTITY_INSERT [dbo].[ProcedureApplicationDocs] ON 
INSERT [dbo].[ProcedureApplicationDocs]
    ([ProcedureApplicationDocId], [Gid], [ProcedureId], [Name], [Extension], [IsRequired], [IsSignatureRequired], [IsActivated], [IsActive])
VALUES
    (2, N'55880c07-3267-4459-ac7c-48db334136b0', 2, N'Документ за подаване 1', NULL   , 1, 0, 0, 1),
    (3, N'5891cbc7-d7df-4344-a2d8-95bbef668202', 2, N'Документ за подаване 2', N'.pdf', 1, 0, 0, 1),
    (4, N'add9c23e-412f-4542-b57d-39e012d12b54', 2, N'Документ за подаване 3', NULL   , 0, 0, 0, 1),
    (5, N'11aacd49-5cef-48a4-92f5-2d662f865ec4', 2, N'Документ за подаване 4', N'.pdf', 0, 0, 0, 1)
SET IDENTITY_INSERT [dbo].[ProcedureApplicationDocs] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureApplicationGuidelines] ON 
INSERT [dbo].[ProcedureApplicationGuidelines]
    ([ProcedureApplicationGuidelineId], [Gid]                                  , [ProcedureId], [Name]     , [Decription] , [BlobKey]                              )
VALUES
    (4                                , N'8fcc92f4-1b82-4604-9d57-45150d088873', 2            , N'Насока 1', N'Описание 1', N'8e558ea6-6ce9-47fa-9b33-42b45acd7fa6'),
    (5                                , N'e02b8706-c64c-44f4-8367-2e6e93b1f242', 2            , N'Насока 2', N'Описание 2', N'a864fe69-cd2e-4ae8-805d-005186a07665'),
    (6                                , N'4f7acbe5-4434-482f-8440-8129d478e3bf', 2            , N'Насока 3', N''          , N'e2383d65-4b85-40d7-aeec-94f152a3ab6a')
SET IDENTITY_INSERT [dbo].[ProcedureApplicationGuidelines] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureBudgetValidationRules] ON 
GO
INSERT [dbo].[ProcedureBudgetValidationRules] ([ProcedureBudgetValidationRuleId], [ProcedureId], [ProgrammeId], [Message], [Condition], [Rule]) VALUES (1, 2, 1, N'Разходите за услугу трябва сумарно да надвишават 1000.00 лв.', NULL, N'(sum([85aa4f89-7be9-4a1b-a825-ab3110ee547a], [БФП], ([Допустим разход]) = ([Да]))) > 1000.00')
GO
INSERT [dbo].[ProcedureBudgetValidationRules] ([ProcedureBudgetValidationRuleId], [ProcedureId], [ProgrammeId], [Message], [Condition], [Rule]) VALUES (2, 2, 1, N'Трабва да има поне една група разходи от ниво 1, за която сумарната стойност на БФП на детайлите й да надвишава 1500 лв.', NULL, N'any([Ниво 1], (sum([Група], [БФП], [Всички])) > 1500.00)')
GO
INSERT [dbo].[ProcedureBudgetValidationRules] ([ProcedureBudgetValidationRuleId], [ProcedureId], [ProgrammeId], [Message], [Condition], [Rule]) VALUES (3, 2, 1, N'Трабва всички групи разходи от ниво 1, да имат сумарната стойност на БФП + СП на детайлите по-голяма от 400 лв.', NULL, N'all([Ниво 2], (sum([Група], [Общо], [Всички])) < 400)')
GO
INSERT [dbo].[ProcedureBudgetValidationRules] ([ProcedureBudgetValidationRuleId], [ProcedureId], [ProgrammeId], [Message], [Condition], [Rule]) VALUES (4, 2, 1, N'Трабва всички детайли на подразходи да имат стойнст за БФП по-голяма от 0 лв.', NULL, N'all([Бюджет], ([БФП]) > 0)')
GO
SET IDENTITY_INSERT [dbo].[ProcedureBudgetValidationRules] OFF
GO

INSERT [dbo].[ProcedureVersions] ([ProcedureId], [ProcedureVersionId], [ProcedureGid], [ProcedureText], [IsActive], [CreateDate], [ModifyDate]) VALUES (2, 1, N'cae66cb6-4075-4acf-b0a4-643858379e79', N'{"name":"Голяма тестова процедура","code":"BG05SFOP001-1.2014.001","description":"Управляващият орган на оперативна програма „Административен капацитет” (ОПАК), съфинансирана от Европейския социален фонд, обявява нова процедура за предоставяне на безвъзмездна финансова помощ, предназначена за централните администрации.\r\n    \r\n    Процедурата е по приоритетна ос ІІІ „Качествено административно обслужване и развитие на електронното управление”, подприоритет 3.2. „Стандартна информационно-комуникационна среда и оперативна съвместимост”, бюджетна линия BG051PO002/13/3.2-04.\r\n    \r\n    Ще бъдат финансирани проекти за разширяване функционалността на единния портал за предоставяне на електронни административни услуги като точка за достъп до Единното звено за контакт (ЕЗК), както и за осигуряване на оперативна съвместимост на информационните системи на администрациите на хоризонтално ниво. Общият размер на отпуснатата безвъзмездна финансова помощ е 3 млн. лв. Предвидената минимална стойност на проектите е 20 000 лв., а максималният праг е 400 000 лв.\r\n    \r\n    Примерни допустими дейности по процедурата са: изграждане на информационно-комуникационни системи или надграждане на съществуващи такива; разработване и внедряване на нови модули към съществуващи информационни системи; усъвършенстване на съществуващи и внедряване на нови електронни административни услуги; създаване на условия за достъп на хората с увреждания до административни услуги и други.\r\n    \r\n    Проектите трябва да бъдат изпълнени в рамките на 18 месеца. Крайният срок за подаване на проектните предложения е 31.05.2013 г. (петък), 17.30 часа.\r\n    \r\n    График за предстоящите информационни дни по процедурата предстои да бъде публикуван на интернет страницата на Управляващия орган.","applicationFormType":"standard","allowedRegistrationType":"digitalOrPaper","projectDuration":12,"nutsLevel":"country","internetAddress":null,"locationFullPath":"BG","questionId":null,"qaBlobKey":null,"qaFileName":null,"qaModifyDate":null,"timeLimitId":2,"timeLimitsEndingDate":"2015-02-10T16:30:00","timeLimitsNotes":"Бележки","categories":{"interventionField":[{"categoryId":2,"code":"002","name":"Научноизследователски и иновационни процеси в големи предприятия","isActive":true},{"categoryId":4,"code":"004","name":"Производствени инвестиции, свързани със сътрудничеството между големи предприятия и МСП за разработване на продукти и услуги на информационните и комуникационните технологии (ИКТ), електронната търговия и повишаването на търсенето на ИКТ","isActive":true},{"categoryId":7,"code":"007","name":"Природен газ","isActive":true}],"formOfFinance":[{"categoryId":124,"code":"01","name":"Безвъзмездни средства","isActive":true},{"categoryId":125,"code":"02","name":"Средства, подлежащи на възстановяване","isActive":true},{"categoryId":128,"code":"05","name":"Подкрепа чрез финансови инструменти: гаранция или еквивалентен инструмент","isActive":true},{"categoryId":130,"code":"07","name":"Награда","isActive":true}],"territorialDimension":[{"categoryId":131,"code":"01","name":"Големи градски райони (гъстонаселени, с население >50 000 души)","isActive":true},{"categoryId":132,"code":"02","name":"Малки градски райони (среднонаселени, с население >5 000 души)","isActive":true},{"categoryId":133,"code":"03","name":"Селски райони (слабонаселени)","isActive":true},{"categoryId":134,"code":"04","name":"Макрорегионален район за сътрудничество","isActive":true},{"categoryId":135,"code":"05","name":"Сътрудничество в райони по национални или регионални програми в национален контекст","isActive":true}],"territorialDeliveryMechanism":[{"categoryId":138,"code":"01","name":"Интегрирани териториални инвестиции — градски райони","isActive":true},{"categoryId":139,"code":"02","name":"Други интегрирани подходи за устойчиво развитие на градските райони","isActive":true}],"thematicObjective":[{"categoryId":145,"code":"01","name":"Засилване на научноизследователската дейност, развойната дейност в областта на технологиите и иновациите","isActive":true},{"categoryId":146,"code":"02","name":"Подобряване на достъпа до информационни и комуникационни технологии и на тяхното използване и качество","isActive":true},{"categoryId":147,"code":"03","name":"Повишаване на конкурентоспособността на малките и средните предприятия","isActive":true}],"esfSecondaryTheme":[{"categoryId":157,"code":"01","name":"Подкрепа за преминаването към нисковъглеродна икономика и икономика с ефективно използване на ресурсите","isActive":true}],"economicDimension":[{"categoryId":165,"code":"01","name":"Селско и горско стопанство","isActive":true},{"categoryId":166,"code":"02","name":"Рибарство и аквакултури","isActive":true}]},"appGuidelines":[{"appGuidelineId":4,"gid":"8fcc92f4-1b82-4604-9d57-45150d088873","name":"Насока 1","description":"Описание 1","blobKey":"8e558ea6-6ce9-47fa-9b33-42b45acd7fa6","filename":"blank.pdf"},{"appGuidelineId":5,"gid":"e02b8706-c64c-44f4-8367-2e6e93b1f242","name":"Насока 2","description":"Описание 2","blobKey":"a864fe69-cd2e-4ae8-805d-005186a07665","filename":"blank.docx"},{"appGuidelineId":6,"gid":"4f7acbe5-4434-482f-8440-8129d478e3bf","name":"Насока 3","description":"","blobKey":"e2383d65-4b85-40d7-aeec-94f152a3ab6a","filename":"blank.zip"}],"appDocs":[{"appDocId":2,"gid":"55880c07-3267-4459-ac7c-48db334136b0","name":"Документ за подаване 1","extension":null,"isRequired":true,"isSignatureRequired":false,"isActive":true},{"appDocId":3,"gid":"5891cbc7-d7df-4344-a2d8-95bbef668202","name":"Документ за подаване 2","extension":".pdf","isRequired":true,"isSignatureRequired":false,"isActive":true},{"appDocId":4,"gid":"add9c23e-412f-4542-b57d-39e012d12b54","name":"Документ за подаване 3","extension":null,"isRequired":false,"isSignatureRequired":false,"isActive":true},{"appDocId":5,"gid":"11aacd49-5cef-48a4-92f5-2d662f865ec4","name":"Документ за подаване 4","extension":".pdf","isRequired":false,"isSignatureRequired":false,"isActive":true}],"specFields":[{"specFieldId":2,"gid":"02147dd4-2f58-49e2-baa0-f3a63e64048f","title":"Допълнително поле 1","description":"Някакво описание.","isRequired":true,"isActive":true,"maxLength":1000},{"specFieldId":3,"gid":"3d4a7e9d-4d81-471a-917e-8716ecfa7fef","title":"Допълнително поле 2","description":"Някакво описание.","isRequired":true,"isActive":true,"maxLength":3000},{"specFieldId":4,"gid":"39dc29c8-a6cc-4d1b-97ad-c178c4faa037","title":"Допълнително поле 3","description":"Някакво описание.","isRequired":false,"isActive":true,"maxLength":5000},{"specFieldId":5,"gid":"0888058d-e1eb-4a1a-bceb-a8382175b61b","title":"Допълнително поле 4","description":"Някакво описание.","isRequired":false,"isActive":true,"maxLength":10000}],"programmes":[{"programmeId":1,"programmeName":"Добро управление","programmeCode":"2014BG05SFOP001","programmePriorities":[{"programmePriorityId":101,"gid":"f476af0a-c625-f4e0-3a36-197de768e67d","code":"BG05SFOP001-1","name":"Административно обслужване и е-управление","investmentPriorities":[{"investmentPriorityId":10101,"gid":"3c6c148e-334e-9f44-95a4-9679795f74f7","code":"11i","name":"Инвестиции в институционален капацитет и в ефикасността на публичните администрации и публичните услуги на национално, регионално и местно равнище с цел осъществяването на реформи и постигането на по-добро регулиране и добро управление","specificTargets":[{"specificTargetId":1010101,"gid":"b29a45d6-7b9c-9f9a-bf0c-7c15fa35faa3","code":"1","name":"Намаляване на административната и регулаторна тежест за гражданите и бизнеса и въвеждане на принципите на „епизоди от живота“ и „бизнес събития“"}]}],"financeSources":["europeanSocialFund"]},{"programmePriorityId":102,"gid":"dfe79880-9ab0-a3db-bf78-3794ba0db819","code":"BG05SFOP001-2","name":"Ефективно и професионално управление в партньорство с гражданското общество и бизнеса","investmentPriorities":null,"financeSources":["europeanSocialFund"]}],"indicators":[{"indicatorId":56,"gid":"8a5bfc77-da80-7bc2-46ce-1b582135759b","name":"Брой на подкрепени приоритетни електронизирани услуги, включително вътрешноадминистративни, на ниво транзакция и/или разплащане, базирани на държавния ХЧО, използвани над 5 000 пъти годишно","type":"specific","kind":"result","trend":"increase","measureName":"Брой","aggregatedReport":"notAggregated","aggregatedTarget":"notAggregated","hasGenderDivision":false,"isActive":true},{"indicatorId":185,"gid":"d76d9f1a-3ef3-8220-8bde-9423ffe28eca","name":"Администрации, подкрепени за въвеждане на системи за управление на качеството","type":"specific","kind":"performance","trend":"inapplicable","measureName":"Брой","aggregatedReport":"notAggregated","aggregatedTarget":"notAggregated","hasGenderDivision":false,"isActive":true},{"indicatorId":191,"gid":"eea62053-1171-fade-5a69-3befcc40229b","name":"брой проекти, изцяло или частично изпълнени от социални партньори или неправителствени организации","type":"common","kind":"performance","trend":"inapplicable","measureName":"Брой","aggregatedReport":"notAggregated","aggregatedTarget":"notAggregated","hasGenderDivision":true,"isActive":true}],"budgetExpenseTypes":[{"budgetLevel1Id":3,"gid":"3ae1da29-4619-4c70-869d-6c1b7f7c94fd","name":"НЕПРЕКИ РАЗХОДИ","isActive":true,"expenses":[{"budgetLevel2Id":7,"gid":"49270358-1f9d-477a-8e1f-5ba35e9dbaa1","name":"Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-2","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":4,"gid":"ceea5ec3-afbd-4066-93bc-998b7398ad02","note":"Разходи за ръководител на проекта"},{"budgetLevel3Id":5,"gid":"0790cf61-d341-465e-ba73-e4151eff2e94","note":"Технически и финансов персонал"},{"budgetLevel3Id":6,"gid":"90cd9171-3c8b-4d03-9785-2692c138b156","note":"Административен/помощен персонал"},{"budgetLevel3Id":7,"gid":"0f006bf5-ee32-45e8-8251-25674d768082","note":"Други (моля уточнете)"}]},{"budgetLevel2Id":8,"gid":"b452f44f-f8be-4cd3-95d1-997fafe6637f","name":"Разходи за командировки","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[{"budgetLevel3Id":8,"gid":"de3bc642-f4df-489d-b40d-42426e9f2218","note":"Дневни"},{"budgetLevel3Id":9,"gid":"809a984f-f351-42bb-8d93-6de67d51ab61","note":"Квартирни"},{"budgetLevel3Id":10,"gid":"d1bfdfb6-541c-4653-abe7-4cc235ca8db8","note":"Пътни"}]}]},{"budgetLevel1Id":4,"gid":"83bbdc77-e28a-472e-a281-bdca178d1179","name":"РАЗХОДИ ЗА ПЕРСОНАЛ","isActive":true,"expenses":[{"budgetLevel2Id":9,"gid":"35a3927b-d2eb-4ae7-88fe-65035bfe32e1","name":"Възнаграждения (брутни суми, вкл.и дължимите от работодателя осигурителни вноски)","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-2","financeSource":"europeanSocialFund","aidMode":"deminimis","details":[{"budgetLevel3Id":11,"gid":"13740b3a-bf0a-4350-8be1-8e5ffddfbab5","note":"Възнаграждения и осигуровки от страна на работодателя за експерти, пряко свързани с основните дейности"},{"budgetLevel3Id":12,"gid":"df6e085c-922b-4dd7-92f6-111120f43d7a","note":"Други (моля уточнете)"}]},{"budgetLevel2Id":10,"gid":"002e179d-facc-41c0-9be1-1e1e8184cf36","name":"Разходи за командировки","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-2","financeSource":"europeanSocialFund","aidMode":"deminimis","details":[{"budgetLevel3Id":13,"gid":"2c0e34a0-1b84-47f0-aca9-b79abaa642c0","note":"Дневни"},{"budgetLevel3Id":14,"gid":"c1a4d555-5b95-4946-807f-4575edc94937","note":"Квартирни"},{"budgetLevel3Id":15,"gid":"24ee3591-0872-4341-b85a-ae66c6f10f3a","note":"Пътни"}]}]},{"budgetLevel1Id":5,"gid":"90181067-23eb-4c66-adbf-64cb79ed4885","name":"РАЗХОДИ ЗА МАТЕРИАЛНИ АКТИВИ","isActive":true,"expenses":[{"budgetLevel2Id":11,"gid":"06a17711-c21a-49f8-9c7a-4c9d35a0bd80","name":"Закупуване на оборудване, съоръжения и обзавеждане","isEligibleCost":false,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":16,"gid":"a0485ebe-6bcb-4ede-bf0d-170cd1da4e9d","note":"Вид/ тип оборудване"}]},{"budgetLevel2Id":12,"gid":"d2c97c41-d370-4242-8270-a1f76b553779","name":"Закупуване на машини","isEligibleCost":false,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[{"budgetLevel3Id":17,"gid":"9b06e654-7c4f-4245-979a-10c1a831329c","note":"Вид / тип машини"}]},{"budgetLevel2Id":13,"gid":"4536e955-6c64-43ff-b256-bfc6ff724a41","name":"Закупуване на земя","isEligibleCost":false,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[{"budgetLevel3Id":18,"gid":"454c48ee-60a4-4935-980f-112c0b2d11c9","note":"Покупка на земя"},{"budgetLevel3Id":19,"gid":"7d110bf0-6c5a-41fc-abfc-a94b3be1b4de","note":"Разходи, съпътстващи покупката на земя"}]},{"budgetLevel2Id":14,"gid":"99fe45e3-79a0-4822-a1bd-e99f879105c3","name":"Други (моля уточнете)","isEligibleCost":false,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"deminimis","details":[]}]},{"budgetLevel1Id":6,"gid":"ad6cff9a-a901-4a5e-9da5-6bacbac67a2d","name":"РАЗХОДИ ЗА НЕМАТЕРИАЛНИ АКТИВИ","isActive":true,"expenses":[{"budgetLevel2Id":15,"gid":"84504a1e-9f00-4b49-bb5e-5658ad533739","name":"Закупуване на патенти, лицензи, права","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":20,"gid":"df7945c2-19d9-42c1-9912-43fb759064fe","note":"Конкретен вид / тип"}]},{"budgetLevel2Id":16,"gid":"ffff6aa3-a1c5-4873-a742-3eaaa88f7bd1","name":"Закупуване на софтуер","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[{"budgetLevel3Id":21,"gid":"d42a2925-0425-4735-aa1c-5fb4b67299dd","note":"Конкретен тип / вид"}]},{"budgetLevel2Id":17,"gid":"307d115b-2144-4630-8d20-1f7bb674bdb9","name":"Други (моля уточнете) ","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[]}]},{"budgetLevel1Id":7,"gid":"bc4c63d5-4bf9-4d09-b49a-61360cb8f86b","name":"РАЗХОДИ ЗА СМР","isActive":true,"expenses":[{"budgetLevel2Id":18,"gid":"ab1744bd-9bbe-46f6-9dd8-06c97a05911b","name":"Разходи за СМР сгради","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"deminimis","details":[{"budgetLevel3Id":22,"gid":"6190416d-ccf0-49c7-b5bb-4b4b08e10bd8","note":"Изграждане на ПСОВ"},{"budgetLevel3Id":23,"gid":"9a0c72d4-8611-4c4c-bf15-ca29520e100c","note":"Реконструкция на училище"}]},{"budgetLevel2Id":19,"gid":"9a597b99-2e33-4473-9d8e-ba536dd64202","name":"СМР инфраструктура","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"deminimis","details":[{"budgetLevel3Id":24,"gid":"a1ab3c94-e3b8-42f8-94bc-a495a59bf8fb","note":"Изграждане на водопровод"},{"budgetLevel3Id":25,"gid":"fb3f6955-e14d-4f94-a5d2-f800792d581d","note":"Изграждане на канализация"}]},{"budgetLevel2Id":20,"gid":"2f2d5d4e-17b1-476d-8211-8bb23a543606","name":"Други (моля уточнете)","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]}]},{"budgetLevel1Id":8,"gid":"85aa4f89-7be9-4a1b-a825-ab3110ee547a","name":"РАЗХОДИ ЗА УСЛУГИ","isActive":true,"expenses":[{"budgetLevel2Id":21,"gid":"0d873223-e256-4842-87d1-059cfed88d04","name":"Разходи за проектиране, проучвания, анализи, стратегии, програми и т.н.","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"deminimis","details":[{"budgetLevel3Id":26,"gid":"9fb9a945-b7df-4e6a-8d32-541c5fd626ed","note":"Разходи за прединвестиционни прочувания"},{"budgetLevel3Id":27,"gid":"63814bf8-1ffd-4820-8286-955d08da559c","note":"разходи за АРП и финансов анализ"},{"budgetLevel3Id":28,"gid":"6e6612c7-bbfc-45fd-88b2-5e4a1cdc131b","note":"разходи за експертни анализи"},{"budgetLevel3Id":29,"gid":"cd2e3de7-ff2b-4d54-92c5-ea234b696672","note":"разходи за проектиране"},{"budgetLevel3Id":30,"gid":"b2f83bae-fff6-4fb9-abe2-d74de8595a8e","note":"Разходи за правни / нотариални услуги"}]},{"budgetLevel2Id":22,"gid":"e37e2942-46eb-47d0-8bb3-47bd7c37eedd","name":"Разходи за организиране на и/или участие в събития (международни изложения, панаири и т.н.)","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":31,"gid":"64747d77-e4a6-4577-aac6-469a9eb15b86","note":"разходи за участие в събития"},{"budgetLevel3Id":32,"gid":"57b09895-a61c-466d-9663-b7b4f89ff596","note":"Разходи за организиране на събития"}]},{"budgetLevel2Id":23,"gid":"e07b07c2-749c-4b9e-a5f2-213e439ac95c","name":"Разходи за обучения","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":33,"gid":"afbb183a-da65-437f-9b25-9c5649ebe159","note":"Разходи за организиране на обучения"},{"budgetLevel3Id":34,"gid":"50bb81cd-8211-499a-90f4-9ebd22c63cf8","note":"Разходи за материали за обучения"}]},{"budgetLevel2Id":24,"gid":"b170bdd4-22cf-4193-b07c-9e46ed857e69","name":"Разходи за маркетинг и реклама","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[{"budgetLevel3Id":35,"gid":"0dfc828c-17d2-46d4-84a1-18551272a037","note":"Разходи за реклама в медиите"},{"budgetLevel3Id":36,"gid":"94b407ca-a14a-4927-8b1b-0dbffa88ae04","note":"други"}]},{"budgetLevel2Id":25,"gid":"bf1cc8b9-a382-490b-82e4-a3dc8047b24a","name":"Разходи за одит на проекта","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[{"budgetLevel3Id":37,"gid":"29653ec2-5008-4b99-92c2-694dacdf0461","note":"Разходи за одит"}]},{"budgetLevel2Id":26,"gid":"dffcc962-c2d5-4b9c-b034-9793a275ac96","name":"Други услуги - моля опишете","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[{"budgetLevel3Id":38,"gid":"cd39a72b-ce36-4581-93e1-22f57dd19bce","note":"други"}]}]},{"budgetLevel1Id":9,"gid":"6455398a-f2a9-4953-b084-a6dffba39c9a","name":"РАЗХОДИ ЗА МАТЕРИАЛИ","isActive":true,"expenses":[{"budgetLevel2Id":27,"gid":"75e559ce-2391-4fcc-8da6-76043cf3758d","name":"Разходи за материали за публичност и визуализация","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"stateAid","details":[{"budgetLevel3Id":39,"gid":"006917ce-98f5-440b-b299-ecb0a334479b","note":"Разходи за изработване и разпространение на информационни и рекламни материали"}]},{"budgetLevel2Id":28,"gid":"9c1d4cfb-de41-4e0a-8018-d0330d064b64","name":"Разходи за дейности по информиране и публичност за проекта, съфинансиран по Оперативната програма ","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG05SFOP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":40,"gid":"ff5fe553-29a2-423b-8a15-c4713c00be62","note":"Разходи за събития"},{"budgetLevel3Id":41,"gid":"2b76a716-c339-443f-9614-4cca38cd8303","note":"Разходи за конференции"}]}]}]},{"programmeId":2,"programmeName":"Транспорт и транспортна инфраструктура","programmeCode":"2014BG16M1OP001","programmePriorities":[{"programmePriorityId":201,"gid":"c381993a-1776-7921-2b77-02231583758a","code":"BG16M1OP001-1","name":"Развитие на железопътната инфраструктура по „основната” Трансевропейска транспортна мрежа","investmentPriorities":[{"investmentPriorityId":20101,"gid":"d97908f9-4638-7ba5-0f37-237944a5eac8","code":"7i","name":"Предоставяне на подкрепа за мултимодално Единно европейско транспортно пространство с помощта на инвестиции в трансевропейската транспортна мрежа (TEN-T)","specificTargets":[{"specificTargetId":2010101,"gid":"25099fa6-9c7d-f5d8-edd9-a87b728ab3d7","code":"1","name":"Привличане на пътнически и товарен трафик чрез подобряване на качеството на железопътната инфраструктура по Трансевропейската транспортна мрежа"}]}],"financeSources":["cohesionFund"]},{"programmePriorityId":202,"gid":"a715ce8d-7d06-8b39-07f9-4bf5fdd497f5","code":"BG16M1OP001-2","name":"Развитие на пътната инфраструктура по „основната” и \"разширената\" Трансевропейска транспортна мрежа","investmentPriorities":[{"investmentPriorityId":20201,"gid":"bcf4030a-f1fd-18df-8810-bedc667a8a88","code":"7i","name":"Предоставяне на подкрепа за мултимодално Единно европейско транспортно пространство с помощта на инвестиции в трансевропейската транспортна мрежа (TEN-T)","specificTargets":[{"specificTargetId":2020101,"gid":"29fd823c-b9e8-dc67-abb4-c47a1537b36f","code":"1","name":"\"Отстраняване на тесните места\" по пътната Трансевропейска транспортна мрежа"}]}],"financeSources":["cohesionFund"]}],"indicators":[{"indicatorId":1,"gid":"0456583c-7fe8-5985-7373-1fea83e21fab","name":"Допустими максимални скорости по железен път","type":"specific","kind":"result","trend":"increase","measureName":"км/ч","aggregatedReport":"notAggregated","aggregatedTarget":"notAggregated","hasGenderDivision":false,"isActive":true}],"budgetExpenseTypes":[{"budgetLevel1Id":10,"gid":"d87ce17f-1f47-4c08-8feb-ad2f66f12710","name":"НЕДОПУСТИМИ РАЗХОДИ","isActive":true,"expenses":[{"budgetLevel2Id":29,"gid":"7575091e-0f2a-4618-88ad-69309331b0b0","name":"Разходи за закупуване на материали ","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG16M1OP001-1","financeSource":"cohesionFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":42,"gid":"8d61fbe9-a682-4407-a953-d7e57b1957ec","note":"Разходи за конкретен вид/тип материали"}]},{"budgetLevel2Id":30,"gid":"edb1e801-60fa-4700-a096-9a1577bbe256","name":"Разходи за закупуване на консумативи","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG16M1OP001-1","financeSource":"cohesionFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":43,"gid":"6ce02cb4-395b-4b71-ba6f-3c1a7f4bf2bb","note":"разходи за конкретен вид тип консумативи"}]}]},{"budgetLevel1Id":11,"gid":"47d00a19-82e8-4258-b101-39e12582b27d","name":"РАЗХОДИ ЗА ПРОВЕЖДАНЕ И УЧАСТИЕ В МЕРОПРИЯТИЯ","isActive":true,"expenses":[{"budgetLevel2Id":31,"gid":"bbaefda2-8753-45ea-a4bc-c9c87a4c59f6","name":"Разходи по стандартна таблица на разходите за единица продукт","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG16M1OP001-1","financeSource":"cohesionFund","aidMode":"stateAid","details":[{"budgetLevel3Id":44,"gid":"0fd3522c-d05a-43aa-b45a-9fc47aefd3b1","note":"Разход от стандартната таблица за разходи"}]},{"budgetLevel2Id":32,"gid":"4714e131-6184-4a37-bb46-f56757b41dc5","name":"Еднократни суми, които не надхвърлят левовата равностойност на 100 000 евро публичен принос","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG16M1OP001-1","financeSource":"cohesionFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":45,"gid":"28f9bf4e-e5b8-4263-ada3-ea49c0108a23","note":"Конкретен разход до 100 000 евро"}]},{"budgetLevel2Id":33,"gid":"40423568-9d24-452a-82a8-1c92ab5fa822","name":"Финансиране с единна ставка","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG16M1OP001-1","financeSource":"cohesionFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":46,"gid":"ec37eeca-add4-4222-bc7d-84c42c027945","note":"Конкретен разход с единна ставка"}]},{"budgetLevel2Id":34,"gid":"7e8a3535-4431-43ca-885e-021bac19c642","name":"Разходи за амортизация ","isEligibleCost":true,"isStandardTablesExpense":false,"isOneTimeExpense":false,"isFlatRateExpense":false,"isLandExpense":false,"isEuApprovedStandardTablesExpense":false,"isEuApprovedOneTimeExpense":false,"isActive":true,"programmePriorityCode":"BG16M1OP001-1","financeSource":"cohesionFund","aidMode":"notApplicable","details":[]}]}]}],"version":1}', 1, CAST(0x07C357CCD975EF3A0B AS DateTime2), CAST(0x07C357CCD975EF3A0B AS DateTime2))
GO
