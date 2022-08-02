PRINT 'Insert Gipsy Procedure'
GO

SET IDENTITY_INSERT [dbo].[Procedures] ON 

GO
INSERT [dbo].[Procedures]
      ([ProcedureId], [Gid], [ProcedureTypeId], [ProcedureStatus], [ProcedureContractReportDocumentsSectionStatus], [ApplicationFormType], [ListingDate], [Code], [AllowedRegistrationType], [Name], [NameAlt], [Description], [DescriptionAlt], [ProjectMinAmount], [ProjectMaxAmount], [ProjectDuration], [IsIntegrated], [CreateDate], [ModifyDate], [AllowConcurrancyContractReports])
  VALUES
      (3, N'2b88132d-f5da-488f-9f78-12cf433ae090', 1, 4, 1, 3, CAST(0x07000000000088390B AS DateTime2), N'BG05M9OP001_28_2015_01', 3, N'Разработване и внедряване на система за мониторинг, оценка и контрол за изпълнение на Националната стратегия на Република България за интегриране на ромите 2012-2020', NULL, N'Операцията има за основна цел да подпомогне интеграционните политики, като създаде ефективна система за регулярно и системно информационно осигуряване, наблюдение, оценка и контрол на постиженията/ неуспехите от провежданата Националната стратегия на Република България  за интегриране на ромите 2012-2020 г. (Стратегията) и други интервенции, насочени към подобряване на ситуацията на ромите в България. По този начин ще бъде създаден адекватен и ефективен механизъм за проследяване на изпълнението, неговото качество, постигнатите резултати, настъпилите промени, а също и на съществуващи проблеми, при постигане на целите на Стратегията и на други мерки и проекти за социално включване на уязвими групи с фокус върху ромите, планирани на национално и местно ниво.<br/>
<b>Допустими дейности:</b><br/>
1.Преглед, анализ и оценка на законодателство, структури, съществуващи аналогични  системи, информационна осигуреност на публични политики за борба с бедността и интеграция на уязвими етнически малцинства с фокус ромите вкл. оценка на нуждите (рискове);<br/>
2.Идентифициране на европейски добри практики за провеждане на мониторинг, оценка, контрол и информационна осигуреност на публични политики за борба с бедността и интеграция на уязвими етнически малцинства с фокус върху ромите; анализ с оглед възможността за прилагане в България;<br/>
3.Изготвяне на концепция, включително с методика на Системата за мониторинг, оценка и контрол и други интервенции за интегриране на уязвими етнически малцинства с фокус ромите, с което да се създадат условия за изграждане и внедряване на цялостна и ефективна Система;<br/>
4. Изграждане на Системата за мониторинг, оценка и контрол, която включва участие на всички заинтересовани страни и комбинира разнообразни форми на мониторинг и оценка;
5. Пилотно тестване на Системата за мониторинг, оценка и контрол;<br/>
6.Внедряване на Системата за мониторинг, оценка и контрол, вкл. изготвяне необходимата документация за улесняване на нейното функциониране, повишаване административния капацитет на всички институции, отговорни за прилагане на Системата на национално и местно ниво;<br/>
7. Изграждане на мрежа от заинтересовани страни, които да подават данни и информация към Системата за мониторинг, оценка и контрол.', NULL, 0.0000, 200000000.0000, 12, 0, CAST(0x078ED5377B6B89390B AS DateTime2), CAST(0x073F5A68928E89390B AS DateTime2), 0)
GO
SET IDENTITY_INSERT [dbo].[Procedures] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureLocations] ON 
INSERT [dbo].[ProcedureLocations]
    ([ProcedureLocationId], [ProcedureId], [NutsLevel], [CountryId], [Nuts1Id], [Nuts2Id], [DistrictId], [MunicipalityId], [SettlementId], [ProtectedZoneId])
VALUES
    (3, 3, 1, 23, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProcedureLocations] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcedureTimeLimits] ON 
GO
INSERT [dbo].[ProcedureTimeLimits]
    ([ProcedureTimeLimitId], [ProcedureId], [EndDate], [Notes])
VALUES
    (4, 3, CAST(0x07001CEDAE92A7390B AS DateTime2), NULL)
GO
SET IDENTITY_INSERT [dbo].[ProcedureTimeLimits] OFF

GO
SET IDENTITY_INSERT [dbo].[ProcedureShares] ON 
GO
INSERT
    [dbo].[ProcedureShares] ([ProcedureShareId], [ProcedureId], [ProgrammeId], [ProgrammePriorityId], [FinanceSource], [EuAmount], [BgAmount], [IsPrimary], [IsActivated])
VALUES
    (7, 3, 4, 403, 1, 1700000.0000, 300000.0000, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[ProcedureShares] OFF
GO

GO
INSERT [dbo].[ProcedureSpecificTargets] ([ProcedureId], [SpecificTargetId]) VALUES (3, 4030101)
GO

GO
INSERT [dbo].[ProcedureInvestmentPriorities] ([ProcedureId], [InvestmentPriorityId]) VALUES (3, 40301)
GO

GO
INSERT [dbo].[ProcedureProgrammes] ([ProcedureId], [ProgrammeId]) VALUES (3, 4)
GO

GO
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel1] ON 
GO

INSERT [dbo].[ProcedureBudgetLevel1] ([ProcedureBudgetLevel1Id], [ProcedureId], [ProgrammeId], [ExpenseTypeId], [Gid], [OrderNum], [IsActivated], [IsActive]) VALUES (12, 3, 4, 2, N'78772d5e-ff21-4c79-94a9-2d6878ac5ef1', 0, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel1] ([ProcedureBudgetLevel1Id], [ProcedureId], [ProgrammeId], [ExpenseTypeId], [Gid], [OrderNum], [IsActivated], [IsActive]) VALUES (13, 3, 4, 1, N'f111e591-d2cb-46ca-9e23-4bbbf2bf92af', 0, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel1] ([ProcedureBudgetLevel1Id], [ProcedureId], [ProgrammeId], [ExpenseTypeId], [Gid], [OrderNum], [IsActivated], [IsActive]) VALUES (14, 3, 4, 3, N'c6bf4bed-a113-450b-bf1c-b55c272d1c55', 0, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel1] ([ProcedureBudgetLevel1Id], [ProcedureId], [ProgrammeId], [ExpenseTypeId], [Gid], [OrderNum], [IsActivated], [IsActive]) VALUES (15, 3, 4, 8, N'c6206d53-badc-4f60-ab1f-21d7e1bb2f91', 0, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel1] ([ProcedureBudgetLevel1Id], [ProcedureId], [ProgrammeId], [ExpenseTypeId], [Gid], [OrderNum], [IsActivated], [IsActive]) VALUES (16, 3, 4, 6, N'51fcd1ef-fb6a-446b-ade1-e7fd912b5275', 0, 1, 1)

GO
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel1] OFF
GO

GO
INSERT [dbo].[ProcedureNumbers] ([ProcedureId], [ProgrammePriorityId], [Number]) VALUES (3, 403, 1)
GO

GO
INSERT [dbo].[ProcedureIndicators] ([ProcedureId], [IndicatorId], [BaseTotalValue], [BaseMenValue], [BaseWomenValue], [BaseYear], [TargetTotalValue], [TargetMenValue], [TargetWomenValue], [MilestoneTargetTotalValue], [MilestoneTargetMenValue], [MilestoneTargetWomenValue], [DataSource], [Description], [IsActivated], [IsActive], [SourceMapNodeId]) VALUES (3, 83, NULL, NULL, NULL, NULL, CAST(150000.000 AS Decimal(15, 3)), NULL, NULL, CAST(150000.000 AS Decimal(15, 3)), NULL, NULL, N'Източник', NULL, 0, 1, 40301)
GO

GO
INSERT [dbo].[ProcedureInterventionCategories] ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive]) VALUES (3, 119, 1, 1)
GO
INSERT [dbo].[ProcedureInterventionCategories] ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive]) VALUES (3, 124, 1, 1)
GO
INSERT [dbo].[ProcedureInterventionCategories] ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive]) VALUES (3, 137, 1, 1)
GO
INSERT [dbo].[ProcedureInterventionCategories] ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive]) VALUES (3, 144, 1, 1)
GO
INSERT [dbo].[ProcedureInterventionCategories] ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive]) VALUES (3, 156, 1, 1)
GO
INSERT [dbo].[ProcedureInterventionCategories] ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive]) VALUES (3, 162, 1, 1)
GO
INSERT [dbo].[ProcedureInterventionCategories] ([ProcedureId], [InterventionCategoryId], [IsActivated], [IsActive]) VALUES (3, 188, 1, 1)
GO

GO
SET IDENTITY_INSERT [dbo].[ProcedureSpecFields] ON 
GO
INSERT [dbo].[ProcedureSpecFields] ([ProcedureSpecFieldId], [Gid], [ProcedureId], [Title], [Description], [IsRequired], [MaxLength], [IsActivated], [IsActive]) VALUES (6, N'5a130322-f8aa-4c03-a1bd-0d0d46598858', 3, N'Опит на кандидата в управление/изпълнение на проекти и/или опит в изпълнение на дейности, подобни на тези включени в проектното предложение', N'Моля опишете опита по проекти, финансирани от структурните фондове, националния бюджет или други финансови инструменти на кандидата и/или  опита в изпълнението на дейности, подобни на тези, включени в проектното предложение. Посочете не повече от 3 проекта, изпълнени през последните 5 години.', 1, 10000, 1, 1)
GO
INSERT [dbo].[ProcedureSpecFields] ([ProcedureSpecFieldId], [Gid], [ProcedureId], [Title], [Description], [IsRequired], [MaxLength], [IsActivated], [IsActive]) VALUES (7, N'77be78ef-286b-4278-af30-bd6f65d7f32f', 3, N'Описание на целевата група', N'Следва да се опишат целевите групи и техни конкретни характеристики (възраст, степен на образование, степен и вид увреждане и т.н. съгласно изискванията в Насоките за кандидатстване). Да се посочи брой лица, включени в проекта. Следва да се опишат идентифицираните нужди и проблеми на целевите групи', 1, 5000, 1, 1)
GO
INSERT [dbo].[ProcedureSpecFields] ([ProcedureSpecFieldId], [Gid], [ProcedureId], [Title], [Description], [IsRequired], [MaxLength], [IsActivated], [IsActive]) VALUES (8, N'd0d4913e-c1ce-44e5-a17e-589f167a197d', 3, N'Готовност за стартиране на проекта', N'Опишете накратко дали са изпълнени всички предпоставки за стартиране на проекта. Зависи ли стартирането на изпълнението на проекта от други допълнителни условия – ако до какви?  Идентифицирайте ключовите рискове - с висок приоритет, които биха повлияли върху изпълнението на проекта и опишете мерките, които ще предприемете за намаляване или преодоляване на тяхното въздействие.', 1, 3000, 1, 1)
GO
INSERT [dbo].[ProcedureSpecFields] ([ProcedureSpecFieldId], [Gid], [ProcedureId], [Title], [Description], [IsRequired], [MaxLength], [IsActivated], [IsActive]) VALUES (9, N'9549ab6b-8d19-4cd1-b1f4-8210f4cecacd', 3, N'Устойчивост на резултатите', N'Моля опишете устойчивостта на резултатите и очаквания ефект върху целевите групи', 1, 1000, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[ProcedureSpecFields] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel2] ON 
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (35, 7, 12, N'Разходи за възнаграждения на физически лица, пряко ангажирани с изпълнението на финансираните дейности и необходими за тяхната подготовка и осъществяване, вкл. осигурителните вноски, начислени за сметка на осигурителя върху договореното възнаграждение съгласно националното законодателство', N'40a2a69d-b725-491d-a3f2-3ada007db45f', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (37, 7, 13, N'Разходи за командировки /дневни, пътни и квартирни/ на лицата, получаващи възнаграждения по раздел 1 и пътни разходи на целевата група', N'c11374fb-c935-438a-8315-bf91fcde0c4f', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (38, 7, 16, N'Разходи за наем', N'e6de8315-ac65-4672-ae9a-ac51a3865b5b', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (39, 7, 16, N'Разходи за експертизи, наблюдения, проучвания, анализи и изследвания', N'35e6fa9d-1d8b-4ee7-a356-d465b9a84402', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (40, 7, 16, N'Разходи за конференции, семинари', N'0e5e80f8-bf75-46db-9e30-32a7e94feefd', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (41, 7, 16, N'Разходи за дейности, свързани с осигуряване на публичност', N'03dd01a1-82bc-45cb-bc21-8ce2b779ffd8', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (42, 7, 16, N'Разходи, произтичащи от договори за изработка/ услуга или договори за поръчка по реда на ЗЗД, неквалифицирани другаде', N'd06e7a9b-ac14-492f-b8e3-0299e4120faf', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (43, 7, 16, N'Други преки разходи,пряко свързани с изпълнението на допустимите дейности по проекта и некласифицирани в раздели от 1 до 5 ', N'0384a1e5-3477-4c72-8cd8-66f78a1366c1', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (44, 7, 14, N'Разходи за закупуване на оборудване и обзавеждане', N'c59786a8-1103-48de-b1cb-e4d2c0475935', 0, 3, 1, 1, 1)
GO
INSERT [dbo].[ProcedureBudgetLevel2] ([ProcedureBudgetLevel2Id], [ProcedureShareId], [ProcedureBudgetLevel1Id], [Name], [Gid], [OrderNum], [AidMode], [IsEligibleCost], [IsActivated], [IsActive]) VALUES (45, 7, 15, N' Разходи за закупуване на материали,консумативи и други материални активи ', N'1e1c3ccf-1a06-4a2a-b611-9a3c79fec779', 0, 3, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel2] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel3] ON 
GO
INSERT [dbo].[ProcedureBudgetLevel3] ([ProcedureBudgetLevel3Id], [ProcedureBudgetLevel2Id], [Gid], [Note], [OrderNum]) VALUES (50, 35, N'4bfad7ba-b09b-4ca7-b356-c3a266af782e', N'Разходи за възнаграждения', 0)
GO
SET IDENTITY_INSERT [dbo].[ProcedureBudgetLevel3] OFF
GO

INSERT [dbo].[ProcedureVersions] ([ProcedureId], [ProcedureVersionId], [ProcedureGid], [ProcedureText], [IsActive], [CreateDate], [ModifyDate]) VALUES (3, 1, N'2b88132d-f5da-488f-9f78-12cf433ae090', N'{"name":"Разработване и внедряване на система за мониторинг, оценка и контрол за изпълнение на Националната стратегия на Република България за интегриране на ромите 2012-2020","code":"BG05M9OP001_28_2015_01","description":"Операцията има за основна цел да подпомогне интеграционните политики, като създаде ефективна система за регулярно и системно информационно осигуряване, наблюдение, оценка и контрол на постиженията/ неуспехите от провежданата Националната стратегия на Република България  за интегриране на ромите 2012-2020 г. (Стратегията) и други интервенции, насочени към подобряване на ситуацията на ромите в България. По този начин ще бъде създаден адекватен и ефективен механизъм за проследяване на изпълнението, неговото качество, постигнатите резултати, настъпилите промени, а също и на съществуващи проблеми, при постигане на целите на Стратегията и на други мерки и проекти за социално включване на уязвими групи с фокус върху ромите, планирани на национално и местно ниво.<br/>\r\n<b>Допустими дейности:</b><br/>\r\n1.Преглед, анализ и оценка на законодателство, структури, съществуващи аналогични  системи, информационна осигуреност на публични политики за борба с бедността и интеграция на уязвими етнически малцинства с фокус ромите вкл. оценка на нуждите (рискове);<br/>\r\n2.Идентифициране на европейски добри практики за провеждане на мониторинг, оценка, контрол и информационна осигуреност на публични политики за борба с бедността и интеграция на уязвими етнически малцинства с фокус върху ромите; анализ с оглед възможността за прилагане в България;<br/>\r\n3.Изготвяне на концепция, включително с методика на Системата за мониторинг, оценка и контрол и други интервенции за интегриране на уязвими етнически малцинства с фокус ромите, с което да се създадат условия за изграждане и внедряване на цялостна и ефективна Система;<br/>\r\n4. Изграждане на Системата за мониторинг, оценка и контрол, която включва участие на всички заинтересовани страни и комбинира разнообразни форми на мониторинг и оценка;\r\n5. Пилотно тестване на Системата за мониторинг, оценка и контрол;<br/>\r\n6.Внедряване на Системата за мониторинг, оценка и контрол, вкл. изготвяне необходимата документация за улесняване на нейното функциониране, повишаване административния капацитет на всички институции, отговорни за прилагане на Системата на национално и местно ниво;<br/>\r\n7. Изграждане на мрежа от заинтересовани страни, които да подават данни и информация към Системата за мониторинг, оценка и контрол.","applicationFormType":"standardWithPreliminarySelection","allowedRegistrationType":"digitalOrPaper","projectDuration":12,"nutsLevel":"country","locationFullPath":"BG","questionId":null,"qaBlobKey":null,"qaFileName":null,"qaModifyDate":null,"timeLimitId":4,"timeLimitsEndingDate":"2015-02-27T17:30:00","timeLimitsNotes":null,"categories":{"interventionField":[{"categoryId":119,"code":"119","name":"Инвестиции в институционален капацитет и в ефективността на публичните администрации и публичните служби на национално, регионално и местно равнище с цел осъществяването на реформи и постигането на по-добро регулиране и добро управление","isActive":true}],"formOfFinance":[{"categoryId":124,"code":"01","name":"Безвъзмездни средства","isActive":true}],"territorialDimension":[{"categoryId":137,"code":"07","name":"Не се прилага","isActive":true}],"territorialDeliveryMechanism":[{"categoryId":144,"code":"07","name":"Не се прилага","isActive":true}],"thematicObjective":[{"categoryId":156,"code":"12","name":"Не се прилага (само техническа помощ)","isActive":true}],"esfSecondaryTheme":[{"categoryId":162,"code":"06","name":"Недискриминация","isActive":true}],"economicDimension":[{"categoryId":188,"code":"24","name":"Други услуги, некласифицирани другаде","isActive":true}]},"appGuidelines":[],"appDocs":[],"specFields":[{"specFieldId":6,"gid":"5a130322-f8aa-4c03-a1bd-0d0d46598858","title":"Опит на кандидата в управление/изпълнение на проекти и/или опит в изпълнение на дейности, подобни на тези включени в проектното предложение","description":"Моля опишете опита по проекти, финансирани от структурните фондове, националния бюджет или други финансови инструменти на кандидата и/или  опита в изпълнението на дейности, подобни на тези, включени в проектното предложение. Посочете не повече от 3 проекта, изпълнени през последните 5 години.","isRequired":true,"isActive":true,"maxLength":10000},{"specFieldId":7,"gid":"77be78ef-286b-4278-af30-bd6f65d7f32f","title":"Описание на целевата група","description":"Следва да се опишат целевите групи и техни конкретни характеристики (възраст, степен на образование, степен и вид увреждане и т.н. съгласно изискванията в Насоките за кандидатстване). Да се посочи брой лица, включени в проекта. Следва да се опишат идентифицираните нужди и проблеми на целевите групи","isRequired":true,"isActive":true,"maxLength":5000},{"specFieldId":8,"gid":"d0d4913e-c1ce-44e5-a17e-589f167a197d","title":"Готовност за стартиране на проекта","description":"Опишете накратко дали са изпълнени всички предпоставки за стартиране на проекта. Зависи ли стартирането на изпълнението на проекта от други допълнителни условия – ако до какви?  Идентифицирайте ключовите рискове - с висок приоритет, които биха повлияли върху изпълнението на проекта и опишете мерките, които ще предприемете за намаляване или преодоляване на тяхното въздействие.","isRequired":true,"isActive":true,"maxLength":3000},{"specFieldId":9,"gid":"9549ab6b-8d19-4cd1-b1f4-8210f4cecacd","title":"Устойчивост на резултатите","description":"Моля опишете устойчивостта на резултатите и очаквания ефект върху целевите групи","isRequired":true,"isActive":true,"maxLength":1000}],"programmes":[{"programmeId":4,"programmeName":"Развитие на човешките ресурси","programmeCode":"2014BG05M9OP001","programmePriorities":[{"code":"BG05M9OP001-3","name":"Модернизация на институциите в сферата на социалното включване, здравеопазването, равните възможности и недискриминацията и условията на труд"}],"indicators":[{"indicatorId":83,"gid":"de8eb7e0-e884-4d1e-92ea-70b0266903b5","name":"Брой въведени нови и/или актуализирани процеси и модели за планиране и изпълнение на политики и услуги","type":"specific","kind":"result","trend":"inapplicable","measureName":"Брой","aggregatedReport":"notAggregated","aggregatedTarget":"notAggregated","hasGenderDivision":false,"isActive":true}],"budgetExpenseTypes":[{"budgetLevel1Id":12,"gid":"78772d5e-ff21-4c79-94a9-2d6878ac5ef1","name":"РАЗХОДИ ЗА ПЕРСОНАЛ","isActive":true,"expenses":[{"budgetLevel2Id":35,"gid":"40a2a69d-b725-491d-a3f2-3ada007db45f","name":"Разходи за възнаграждения на физически лица, пряко ангажирани с изпълнението на финансираните дейности и необходими за тяхната подготовка и осъществяване, вкл. осигурителните вноски, начислени за сметка на осигурителя върху договореното възнаграждение съгласно националното законодателство","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[{"budgetLevel3Id":50,"gid":"4bfad7ba-b09b-4ca7-b356-c3a266af782e","note":"Разходи за възнаграждения"}]}]},{"budgetLevel1Id":13,"gid":"f111e591-d2cb-46ca-9e23-4bbbf2bf92af","name":"НЕПРЕКИ РАЗХОДИ","isActive":true,"expenses":[{"budgetLevel2Id":37,"gid":"c11374fb-c935-438a-8315-bf91fcde0c4f","name":"Разходи за командировки /дневни, пътни и квартирни/ на лицата, получаващи възнаграждения по раздел 1 и пътни разходи на целевата група","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]}]},{"budgetLevel1Id":14,"gid":"c6bf4bed-a113-450b-bf1c-b55c272d1c55","name":"РАЗХОДИ ЗА МАТЕРИАЛНИ АКТИВИ","isActive":true,"expenses":[{"budgetLevel2Id":44,"gid":"c59786a8-1103-48de-b1cb-e4d2c0475935","name":"Разходи за закупуване на оборудване и обзавеждане","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]}]},{"budgetLevel1Id":15,"gid":"c6206d53-badc-4f60-ab1f-21d7e1bb2f91","name":"НЕДОПУСТИМИ РАЗХОДИ","isActive":true,"expenses":[{"budgetLevel2Id":45,"gid":"1e1c3ccf-1a06-4a2a-b611-9a3c79fec779","name":" Разходи за закупуване на материали,консумативи и други материални активи ","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]}]},{"budgetLevel1Id":16,"gid":"51fcd1ef-fb6a-446b-ade1-e7fd912b5275","name":"РАЗХОДИ ЗА УСЛУГИ","isActive":true,"expenses":[{"budgetLevel2Id":38,"gid":"e6de8315-ac65-4672-ae9a-ac51a3865b5b","name":"Разходи за наем","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]},{"budgetLevel2Id":39,"gid":"35e6fa9d-1d8b-4ee7-a356-d465b9a84402","name":"Разходи за експертизи, наблюдения, проучвания, анализи и изследвания","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]},{"budgetLevel2Id":40,"gid":"0e5e80f8-bf75-46db-9e30-32a7e94feefd","name":"Разходи за конференции, семинари","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]},{"budgetLevel2Id":41,"gid":"03dd01a1-82bc-45cb-bc21-8ce2b779ffd8","name":"Разходи за дейности, свързани с осигуряване на публичност","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]},{"budgetLevel2Id":42,"gid":"d06e7a9b-ac14-492f-b8e3-0299e4120faf","name":"Разходи, произтичащи от договори за изработка/ услуга или договори за поръчка по реда на ЗЗД, неквалифицирани другаде","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]},{"budgetLevel2Id":43,"gid":"0384a1e5-3477-4c72-8cd8-66f78a1366c1","name":"Други преки разходи,пряко свързани с изпълнението на допустимите дейности по проекта и некласифицирани в раздели от 1 до 5 ","isEligibleCost":true,"isActive":true,"programmePriorityCode":"BG05M9OP001-3","financeSource":"europeanSocialFund","aidMode":"notApplicable","details":[]}]}]}]}', 1, CAST(0x07C63D90DC6DDC390B AS DateTime2), CAST(0x07C63D90DC6DDC390B AS DateTime2))
GO
