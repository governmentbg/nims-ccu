--update finance source check constraints

ALTER TABLE ProcedureShares DROP CONSTRAINT [CHK_ProcedureShares_FinanceSource]
GO
ALTER TABLE ProcedureShares ADD CONSTRAINT [CHK_ProcedureShares_FinanceSource]          CHECK ([FinanceSource] IN (1, 2, 3, 4, 5))
GO

ALTER TABLE MapNodeIndicators DROP CONSTRAINT [CHK_MapNodeIndicators_FinanceSource]
GO
ALTER TABLE MapNodeIndicators ADD CONSTRAINT [CHK_MapNodeIndicators_FinanceSource]    CHECK         ([FinanceSource] IN (1, 2, 3, 4, 5))
GO

ALTER TABLE MapNodeBudgets DROP CONSTRAINT [CHK_MapNodeBudgets_FinanceSource]
GO
ALTER TABLE MapNodeBudgets ADD CONSTRAINT [CHK_MapNodeBudgets_FinanceSource]   CHECK           ([FinanceSource] IN (1, 2, 3, 4, 5))
GO


--update InstitutionType name
update InstitutionTypes
set Name=N'Одитен орган'
where InstitutionTypeId=2

--Update MapNodes
update MapNodes
set
Name=N'Закупуване на хранителни продукти',
NameAlt=N'Purchasing of food products'
where MapNodeId=801


SET IDENTITY_INSERT [MapNodes] ON
GO

INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (802, N'ProgrammePriority', N'BG05FMOP001-2', N'ПО2', N'Предоставяне на индивидуални пакети хранителни продукти', N'Distribution of individual packages with food products', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (80102, N'InvestmentPriority', N'00', N'ИП00', N'Без инвестиционен приоритет', N'No investment priority', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (8010201, N'SpecificTarget', N'1', N'СЦ1', N'Без специфична цел', N'No specific target', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (803, N'ProgrammePriority', N'BG05FMOP001-3', N'ПО3', N'Осигуряване на топъл обяд', N'Providing of warm meal', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (80103, N'InvestmentPriority', N'00', N'ИП00', N'Без инвестиционен приоритет', N'No investment priority', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (8010301, N'SpecificTarget', N'1', N'СЦ1', N'Без специфична цел', N'No specific target', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (804, N'ProgrammePriority', N'BG05FMOP001-4', N'ПО4', N'Техническа помощ', N'Technical assistance', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (80104, N'InvestmentPriority', N'00', N'ИП00', N'Без инвестиционен приоритет', N'No investment priority', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (8010401, N'SpecificTarget', N'1', N'СЦ1', N'Без специфична цел', N'No specific target', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)

SET IDENTITY_INSERT [MapNodes] OFF
GO

INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (802, 8, 8, 802)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (80102, 802, 8, 802)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (8010201, 80102, 8, 802)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (803, 8, 8, 803)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (80103, 803, 8, 803)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (8010301, 80103, 8, 803)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (804, 8, 8, 804)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (80104, 804, 8, 804)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (8010401, 80104, 8, 804)
GO

--Add MapNodeBudgets
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (501, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (501, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (501, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (501, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (501, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (501, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (501, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (502, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (502, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (502, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (502, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (502, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (502, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (502, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (503, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (503, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (503, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (503, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (503, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (503, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (503, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (504, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (504, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (504, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (504, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (504, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (504, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (504, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (505, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (505, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (505, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (505, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (505, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (505, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (505, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (601, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (601, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (601, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (601, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (601, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (601, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (601, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (602, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (602, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (602, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (602, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (602, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (602, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (602, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (603, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (603, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (603, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (603, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (603, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (603, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (603, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (604, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (604, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (604, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (604, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (604, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (604, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (604, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (605, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (605, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (605, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (605, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (605, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (605, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (605, 7, 5, 0, 0, 0, 0)

INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (606, 1, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (606, 2, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (606, 3, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (606, 4, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (606, 5, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (606, 6, 5, 0, 0, 0, 0)
INSERT INTO [MapNodeBudgets] ([MapNodeId], [BudgetPeriodId], [FinanceSource], [EuAmount], [BgAmount], [EuReservedAmount], [BgReservedAmount]) VALUES (606, 7, 5, 0, 0, 0, 0)
