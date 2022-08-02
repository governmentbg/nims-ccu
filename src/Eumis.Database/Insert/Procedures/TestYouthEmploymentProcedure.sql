PRINT 'Insert YouthEmployment Procedure'
GO

SET IDENTITY_INSERT [dbo].[Procedures] ON 
INSERT [dbo].[Procedures]
    ([ProcedureId], [Gid], [ProcedureTypeId], [ProcedureStatus], [ProcedureContractReportDocumentsSectionStatus], [ApplicationFormType], [ListingDate], [Code], [AllowedRegistrationType],[Name], [NameAlt], [Description], [DescriptionAlt], [ProjectMinAmount], [ProjectMaxAmount], [ProjectDuration], [IsIntegrated], [CreateDate], [ModifyDate], [AllowConcurrancyContractReports])
VALUES
    (1, N'8cf3e381-428c-435d-9d44-f9daffe25751', 1, 4, 1, 1, CAST(0x07000000000082380B AS DateTime2), N'BG05M9OP001-1.2014.001', 3, N'Младежка заетост', N'Youth Employment', N'Целта на процедурата е: Да се повиши конкурентоспособността на младежите чрез осигуряване на възможност за стажуване или обучение по време на работа, което ще улесни прехода от образование към заетост, а и едновременно с това ще доведе до натрупване на ценен професионален опит, необходим за заемане на свободни работни места, заявени от работодатели', NULL, 1200000.0000, 2000000.0000, 12, 0, CAST(0x07570B382C894F390B AS DateTime2), CAST(0x07D2B9A9EF8B4F390B AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Procedures] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureLocations] ON 
INSERT [dbo].[ProcedureLocations]
    ([ProcedureLocationId], [ProcedureId], [NutsLevel], [CountryId], [Nuts1Id], [Nuts2Id], [DistrictId], [MunicipalityId], [SettlementId], [ProtectedZoneId])
VALUES
    (1, 1, 1, 23, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProcedureLocations] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureTimeLimits] ON 
INSERT [dbo].[ProcedureTimeLimits]
    ([ProcedureTimeLimitId], [ProcedureId], [EndDate], [Notes])
VALUES
    (1, 1, CAST(0x0700B4284D8A96390B AS DateTime2), N'Бележки')
SET IDENTITY_INSERT [dbo].[ProcedureTimeLimits] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureShares] ON 
INSERT [dbo].[ProcedureShares]
    ([ProcedureShareId], [ProcedureId], [ProgrammeId], [ProgrammePriorityId], [FinanceSource], [EuAmount], [BgAmount], [IsPrimary],[IsActivated])
VALUES
    (1, 1, 4, 401, 4, 30231810.0000, 2668190.0000, 1, 1),
    (2, 1, 4, 401, 1, 1785000.0000, 315000.0000, 0, 1)
SET IDENTITY_INSERT [dbo].[ProcedureShares] OFF
GO

INSERT [dbo].[ProcedureInvestmentPriorities]
    ([ProcedureId], [InvestmentPriorityId])
VALUES
    (1, 40102)
GO

INSERT [dbo].[ProcedureSpecificTargets]
    ([ProcedureId], [SpecificTargetId])
VALUES
    (1, 4010203)
GO

INSERT [dbo].[ProcedureProgrammes]
    ([ProcedureId], [ProgrammeId])
VALUES
    (1, 4)
GO

INSERT [dbo].[ProcedureNumbers]
    ([ProcedureId], [ProgrammePriorityId], [Number])
VALUES
    (1, 401, 1)
GO

INSERT [dbo].[ProcedureInterventionCategories]
    ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive])
VALUES
    (1, 103, 1, 1),
    (1, 124, 1, 1),
    (1, 137, 1, 1),
    (1, 144, 1, 1),
    (1, 145, 1, 1),
    (1, 157, 1, 1),
    (1, 158, 1, 1),
    (1, 159, 1, 1),
    (1, 160, 1, 1),
    (1, 161, 1, 1),
    (1, 165, 1, 1)
GO

SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel1] ON 
INSERT [dbo].[ProcedureBudgetLevel1]
    ([ProcedureBudgetLevel1Id], [ProcedureId], [ProgrammeId], [ExpenseTypeId], [Gid], [OrderNum], [IsActivated], [IsActive])
VALUES
    (1, 1, 4, 2, N'1fbea294-3752-46b0-b060-c695b7f46072', 1, 1, 1),
    (2, 1, 4, 1, N'7a83a2ea-b9a6-4dfa-bfb6-9f9b9165df55', 2, 1, 1)
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel1] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel2] ON 
INSERT [dbo].[ProcedureBudgetLevel2]
    ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive])
VALUES
    (1, 2, 1, N'Разходи за трудови и други възнаграждения, стипендии и други доходи на физически лица, пряко ангажирани с изпълнението на финансираните дейности и необходими за тяхната подготовка и осъществяване, вкл.осигурителните вноски, начислени за сметка на осигурителя върху договореното възнаграждение съгласно националното законодателство.', N'a0fba875-ab7e-44c6-b9e6-6a64a2c7dcfa', 1, 3, 1, 1, 1),
    (2, 2, 1, N'Пътни разходи за целевата група ', N'04790722-ec11-4f54-86de-f8d0c88b26aa', 2, 3, 1, 1, 1),
    (3, 2, 1, N'Разходи за материали и консумативи', N'eb9d6af2-6bdd-4fd4-b208-2363e9e44dd3', 3, 3, 1, 1, 1),
    (4, 2, 1, N'Разходи за външни услуги ', N'fb7c397e-158f-4e8e-b7f4-60bd329d4c51', 4, 3, 1, 1, 1),
    (5, 1, 2, N'Разходи за възнаграждения на екипа по проекта', N'c27f6c74-eef4-48ff-b18a-ec942510f57b', 1, 3, 1, 1, 1),
    (6, 1, 2, N'Разходи за командировки на персонала и административни разходи за издръжка на офис на проекта', N'4fd6f03b-d32d-4da9-be57-b3b0a060ab83', 2, 3, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel2] OFF
GO


SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel3] ON 
INSERT [dbo].[ProcedureBudgetLevel3]
    ([ProcedureBudgetLevel3Id], [ProcedureBudgetLevel2Id], [Gid], [Note], [OrderNum])
VALUES
    (1, 1, N'c47770bd-e5e7-4b7d-9d3f-b6209a6d2326', N'Възнаграждения и осигуровки от страна на работодателя', 1),
    (2, 4, N'c0cd0785-53e6-4fb4-a30b-5f33565b61b2', N'Разходи за дейности, свързани с осигуряване на публичност - до 1% от разходите по операцията', 1),
    (3, 4, N'63e274ca-a8c0-43a0-9e4e-51f6b2fde398', N'Разходи, произтичащи от договори за изработка/ услуга или договори за поръчка по реда на ЗЗД  - АКО Е ПРИЛОЖИМО', 2)
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel3] OFF
GO


SET IDENTITY_INSERT [dbo].[ProcedureSpecFields] ON 
INSERT [dbo].[ProcedureSpecFields]
    ([ProcedureSpecFieldId], [Gid], [ProcedureId], [Title], [Description], [IsRequired], [MaxLength], [IsActivated], [IsActive])
VALUES
    (1, N'51f0a522-e8e5-45e1-b957-6e91b1f47486', 1, N'Устойчивост на резултатите (максимум 1 страници)', N'Моля опишете устойчивостта на резултатите и очаквания ефект върху целевите групи.', 1, 1000, 1, 1)
SET IDENTITY_INSERT [dbo].[ProcedureSpecFields] OFF
GO

INSERT [dbo].[ProcedureIndicators] ([ProcedureId], [IndicatorId], [BaseTotalValue], [BaseMenValue], [BaseWomenValue], [BaseYear], [TargetTotalValue], [TargetMenValue], [TargetWomenValue], [MilestoneTargetTotalValue], [MilestoneTargetMenValue], [MilestoneTargetWomenValue], [DataSource], [Description], [IsActivated], [IsActive], [SourceMapNodeId]) VALUES (1, 111, NULL, NULL, NULL, NULL, CAST(150000.000 AS Decimal(15, 3)), NULL, NULL, CAST(150000.000 AS Decimal(15, 3)), NULL, NULL, N'Източник', NULL, 0, 1, 40102)
GO
INSERT [dbo].[ProcedureIndicators] ([ProcedureId], [IndicatorId], [BaseTotalValue], [BaseMenValue], [BaseWomenValue], [BaseYear], [TargetTotalValue], [TargetMenValue], [TargetWomenValue], [MilestoneTargetTotalValue], [MilestoneTargetMenValue], [MilestoneTargetWomenValue], [DataSource], [Description], [IsActivated], [IsActive], [SourceMapNodeId]) VALUES (1, 129, NULL, NULL, NULL, NULL, CAST(200000.000 AS Decimal(15, 3)), NULL, NULL, CAST(200000.000 AS Decimal(15, 3)), NULL, NULL, N'Източник', NULL, 0, 1, 40102)
GO
INSERT [dbo].[ProcedureIndicators] ([ProcedureId], [IndicatorId], [BaseTotalValue], [BaseMenValue], [BaseWomenValue], [BaseYear], [TargetTotalValue], [TargetMenValue], [TargetWomenValue], [MilestoneTargetTotalValue], [MilestoneTargetMenValue], [MilestoneTargetWomenValue], [DataSource], [Description], [IsActivated], [IsActive], [SourceMapNodeId]) VALUES (1, 136, NULL, NULL, NULL, NULL, CAST(100000.000 AS Decimal(15, 3)), NULL, NULL, CAST(100000.000 AS Decimal(15, 3)), NULL, NULL, N'Източник', NULL, 0, 1, 40102)
GO

SET IDENTITY_INSERT [dbo].[ProcedureApplicationGuidelines] ON 
INSERT [dbo].[ProcedureApplicationGuidelines]
    ([ProcedureApplicationGuidelineId], [Gid]                                  , [ProcedureId], [Name]                     , [Decription] , [BlobKey]                              )
VALUES
    (1                                , N'12fbbc05-fff7-4107-a5e8-e6e44d405d12', 1            , N'Покана'                  , N''          , N'cba9ac93-8d88-4d5b-b92e-0aefe1446ea6'),
    (2                                , N'530ab535-6927-483f-8ffc-78c9e08d346c', 1            , N'Образец на Договор'      , N''          , N'2baa4378-ce0a-48c2-9c8b-baf57ef879b5'),
    (3                                , N'44c3113c-3bf9-49ee-897e-3fd2146bb8bb', 1            , N'Насоки за кандидатстване', N''          , N'193daaa2-abad-40d9-bd39-4b16537964dd')
SET IDENTITY_INSERT [dbo].[ProcedureApplicationGuidelines] OFF
GO

INSERT [dbo].[ProcedureVersions] ([ProcedureId], [ProcedureVersionId], [ProcedureGid], [ProcedureText], [IsActive], [CreateDate], [ModifyDate]) VALUES (1, 1, N'8cf3e381-428c-435d-9d44-f9daffe25751', N'{"name":"Младежка заетост","code":"BG05M9OP001-1.2014.001","description":"Целта на процедурата е: Да се повиши конкурентоспособността на младежите чрез осигуряване на възможност за стажуване или обучение по време на работа, което ще улесни прехода от образование към заетост, а и едновременно с това ще доведе до натрупване на ценен професионален опит, необходим за заемане на свободни работни места, заявени от работодатели","applicationFormType":"standard","allowedRegistrationType":"digitalOrPaper","projectDuration":12,"nutsLevel":"country","locationFullPath":"BG","questionId":null,"qaBlobKey":null,"qaFileName":null,"qaModifyDate":null,"timeLimitId":1,"timeLimitsEndingDate":"2015-02-10T16:30:00","timeLimitsNotes":"Бележки","categories":{"interventionField":[{"categoryId":103,"code":"103","name":"Устойчиво интегриране на пазара на труда на младите хора, в частност тези, които не са ангажирани с трудова дейност, образование или обучение, включително младите хора, изложени на риск от социално изключване, и младите хора от маргинализирани общности, включително чрез прилагане на схемата за гаранция за младежта","isActive":true}],"formOfFinance":[{"categoryId":124,"code":"01","name":"Безвъзмездни средства","isActive":true}],"territorialDimension":[{"categoryId":137,"code":"07","name":"Не се прилага","isActive":true}],"territorialDeliveryMechanism":[{"categoryId":144,"code":"07","name":"Не се прилага","isActive":true}],"thematicObjective":[{"categoryId":145,"code":"01","name":"Засилване на научноизследователската дейност, развойната дейност в областта на технологиите и иновациите","isActive":true}],"esfSecondaryTheme":[{"categoryId":157,"code":"01","name":"Подкрепа за преминаването към нисковъглеродна икономика и икономика с ефективно използване на ресурсите","isActive":true},{"categoryId":158,"code":"02","name":"Социални иновации","isActive":true},{"categoryId":159,"code":"03","name":"Повишаване на конкурентоспособността на МСП","isActive":true},{"categoryId":160,"code":"04","name":"Засилване на научноизследователската дейност, развойната дейност в областта на технологиите и иновациите","isActive":true},{"categoryId":161,"code":"05","name":"Подобряване на достъпа до информационни и комуникационни технологии и на тяхното използване и качество","isActive":true}],"economicDimension":[{"categoryId":165,"code":"01","name":"Селско и горско стопанство","isActive":true}]},"appGuidelines":[{"appGuidelineId":1,"gid":"12fbbc05-fff7-4107-a5e8-e6e44d405d12","name":"Покана","description":"","blobKey":"cba9ac93-8d88-4d5b-b92e-0aefe1446ea6","filename":"blank.pdf"},{"appGuidelineId":2,"gid":"530ab535-6927-483f-8ffc-78c9e08d346c","name":"Образец на Договор","description":"","blobKey":"2baa4378-ce0a-48c2-9c8b-baf57ef879b5","filename":"blank.docx"},{"appGuidelineId":3,"gid":"44c3113c-3bf9-49ee-897e-3fd2146bb8bb","name":"Насоки за кандидатстване","description":"","blobKey":"193daaa2-abad-40d9-bd39-4b16537964dd","filename":"blank.zip"}],"appDocs":[],"specFields":[{"specFieldId":1,"gid":"51f0a522-e8e5-45e1-b957-6e91b1f47486","title":"Устойчивост на резултатите (максимум 1 страници)","description":"Моля опишете устойчивостта на резултатите и очаквания ефект върху целевите групи.","isRequired":true,"isActive":true,"maxLength":1000}],"programmes":[{"programmeId":4,"programmeName":"Развитие на човешките ресурси","programmeCode":"2014BG05M9OP001","programmePriorities":[{"code":"BG05M9OP001-1","name":"Подобряване достъпа до заетост и качеството на работните места"}],"indicators":[{"indicatorId":129,"gid":"ac3af059-f25c-4ff2-8c34-63e181b44a50","name":"Неактивни участници от ромски произход на възраст 15- 24 г.вкл., неангажирани с образование или обучение, които при напускане на операцията са ангажирани с образование/обучение, получават квалификация или имат работа, включително като самостоятелно заети","type":"specific","kind":"result","trend":"increase","measureName":"Брой","aggregatedReport":"notAggregated","aggregatedTarget":"notAggregated","hasGenderDivision":false,"isActive":true},{"indicatorId":136,"gid":"8c0c1e92-69ae-4a6c-a6cd-444d1f9c7751","name":"Безработни роми на възраст 25 - 29 г. вкл., с основна или по-ниска образователна степен, които при напускане на операцията са ангажирани с образование/ обучение, или които получават квалификация или имат работа, включително като самостоятелно заети лица","type":"specific","kind":"result","trend":"increase","measureName":"Брой","aggregatedReport":"notAggregated","aggregatedTarget":"notAggregated","hasGenderDivision":false,"isActive":true}],"budgetExpenseTypes":[{"budgetLevel1Id":1,"gid":"1fbea294-3752-46b0-b060-c695b7f46072","name":"РАЗХОДИ ЗА ПЕРСОНАЛ","isActive":true,"expenses":[{"budgetLevel2Id":1,"gid":"a0fba875-ab7e-44c6-b9e6-6a64a2c7dcfa","name":"Разходи за трудови и други възнаграждения, стипендии и други доходи на физически лица, пряко ангажирани с изпълнението на финансираните дейности и необходими за тяхната подготовка и осъществяване, вкл.осигурителните вноски, начислени за сметка на осигурителя върху договореното възнаграждение съгласно националното законодателство.","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":1,"gid":"c47770bd-e5e7-4b7d-9d3f-b6209a6d2326","note":"Възнаграждения и осигуровки от страна на работодателя"}]},{"budgetLevel2Id":2,"gid":"04790722-ec11-4f54-86de-f8d0c88b26aa","name":"Пътни разходи за целевата група ","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]},{"budgetLevel2Id":3,"gid":"eb9d6af2-6bdd-4fd4-b208-2363e9e44dd3","name":"Разходи за материали и консумативи","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]},{"budgetLevel2Id":4,"gid":"fb7c397e-158f-4e8e-b7f4-60bd329d4c51","name":"Разходи за външни услуги ","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-1","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":2,"gid":"c0cd0785-53e6-4fb4-a30b-5f33565b61b2","note":"Разходи за дейности, свързани с осигуряване на публичност - до 1% от разходите по операцията"},{"budgetLevel3Id":3,"gid":"63e274ca-a8c0-43a0-9e4e-51f6b2fde398","note":"Разходи, произтичащи от договори за изработка/ услуга или договори за поръчка по реда на ЗЗД  - АКО Е ПРИЛОЖИМО"}]}]},{"budgetLevel1Id":2,"gid":"7a83a2ea-b9a6-4dfa-bfb6-9f9b9165df55","name":"НЕПРЕКИ РАЗХОДИ","isActive":true,"expenses":[{"budgetLevel2Id":5,"gid":"c27f6c74-eef4-48ff-b18a-ec942510f57b","name":"Разходи за възнаграждения на екипа по проекта","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-1","financeSource":"youthEmploymentInitiative","aidMode":"notApplicable","details":[]},{"budgetLevel2Id":6,"gid":"4fd6f03b-d32d-4da9-be57-b3b0a060ab83","name":"Разходи за командировки на персонала и административни разходи за издръжка на офис на проекта","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-1","financeSource":"youthEmploymentInitiative","aidMode":"notApplicable","details":[]}]}]}]}', 1, CAST(0x07F5B247CF6DDC390B AS DateTime2), CAST(0x07F5B247CF6DDC390B AS DateTime2))
GO
