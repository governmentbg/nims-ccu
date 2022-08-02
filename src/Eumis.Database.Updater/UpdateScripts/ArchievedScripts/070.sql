GO

update Institutions
set Name=N'МФ, Дирекция "Национален фонд"'
where InstitutionId=2

update MapNodeInstitutions
set InstitutionId=2
where InstitutionId=10

update Institutions
set Name=N'МТСП, Агенция за социално подпомагане'
where InstitutionId=10

SET IDENTITY_INSERT [MapNodes] ON

INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (8, N'Programme', N'2014BG05FMOP001', N'ОПХ', N'Oперативна програма за храни и/или основно материално подпомагане', N'Foot and/or basic material assistance', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (801, N'ProgrammePriority', N'BG05FMOP001-1', N'ПО1', N'Без приоритетна ос', N'No programme priority', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (80101, N'InvestmentPriority', N'00', N'ИП00', N'Без инвестиционен приоритет', N'No investment priority', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, 1)
INSERT INTO [MapNodes] ([MapNodeId], [Type], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden]) VALUES (8010101, N'SpecificTarget', N'1', N'СЦ1', N'Без специфична цел', N'No specific target', GETDATE(), GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL)

SET IDENTITY_INSERT [MapNodes] OFF

GO

INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (8, NULL, 8, NULL)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (801, 8, 8, 801)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (80101, 801, 8, 801)
INSERT INTO [MapNodeRelations] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES (8010101, 80101, 8, 801)

GO

INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (8, 10, 1, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (8, 2, 3, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')
INSERT INTO [MapNodeInstitutions] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES (8, 3, 2, N'Име на контакт', N'Длъжност', N'Телефонен номер', N'Факс', N'e-mail')

GO


INSERT INTO [UserPermissions]
    ([UserId], [PermissionType] , [Permission]                     , [ProgrammeId])
VALUES
--admin
    (2       , N'OperationalMap', 'CanRead'                        , 8            ),
    (2       , N'OperationalMap', 'CanWrite'                       , 8            ),

    (2       , N'Procedure'     , 'CanRead'                        , 8            ),
    (2       , N'Procedure'     , 'CanWrite'                       , 8            ),
    (2       , N'Procedure'     , 'CanCheck'                       , 8            ),
    (2       , N'Procedure'     , 'CanDelete'                      , 8            ),

    (2       , N'Project'       , 'CanRead'                        , 8            ),
    (2       , N'Project'       , 'CanRegister'                    , 8            )

GO
