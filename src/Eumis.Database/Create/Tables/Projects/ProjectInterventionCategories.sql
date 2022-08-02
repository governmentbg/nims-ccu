PRINT 'ProjectInterventionCategories'
GO

CREATE TABLE [dbo].[ProjectInterventionCategories] (
    [ProjectId]                 INT     NOT NULL IDENTITY,
    [InterventionCategoryId]    INT     NOT NULL,

    CONSTRAINT [PK_ProjectInterventionCategories]                           PRIMARY KEY ([ProjectId], [InterventionCategoryId]),
    CONSTRAINT [FK_ProjectInterventionCategories_Projects]                  FOREIGN KEY ([ProjectId])               REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectInterventionCategories_InterventionCategories]    FOREIGN KEY ([InterventionCategoryId])  REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId])
);
GO

exec spDescTable  N'ProjectInterventionCategories', N'Категории на интервенция на Проектно предложение.'
exec spDescColumn N'ProjectInterventionCategories', N'ProjectId'               , N'Идентификатор на Проектно предложение.'
exec spDescColumn N'ProjectInterventionCategories', N'InterventionCategoryId'    , N'Идентификатор на категория на интервенция.'
GO

