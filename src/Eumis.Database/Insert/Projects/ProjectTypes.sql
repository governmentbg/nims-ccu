PRINT 'Insert ProjectTypes'
GO

SET IDENTITY_INSERT [ProjectTypes] ON

INSERT INTO [ProjectTypes]
    ([ProjectTypeId], [Alias]           , [Name]                                , [NameAlt]                             )
VALUES
    (1              , N'projectProposal', N'Проектно предложение'               , 'Project proposal'                    ),
    (2              , N''               , N'Предложение за предварителен подбор', 'Proposal for preliminary selection'  ),
    (3              , N''               , N'Проектен фиш'                       , 'Project fiche'                       )

SET IDENTITY_INSERT [ProjectTypes] OFF
GO
