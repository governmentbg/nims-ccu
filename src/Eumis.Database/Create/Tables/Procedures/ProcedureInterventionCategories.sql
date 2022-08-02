PRINT 'ProcedureInterventionCategories'
GO

CREATE TABLE [dbo].[ProcedureInterventionCategories] (
    [ProcedureId]               INT     NOT NULL,
    [InterventionCategoryId]    INT     NOT NULL,
    [IsActivated]               BIT     NOT NULL,
    [IsActive]                  BIT     NOT NULL,

    CONSTRAINT [PK_ProcedureInterventionCategories]                             PRIMARY KEY ([ProcedureId], [InterventionCategoryId]),
    CONSTRAINT [FK_ProcedureInterventionCategories_Procedures]                  FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureInterventionCategories_InterventionCategories]      FOREIGN KEY ([InterventionCategoryId])  REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId])
);
GO

exec spDescTable  N'ProcedureInterventionCategories', N'Категории на интервенция на процедура.'
exec spDescColumn N'ProcedureInterventionCategories', N'ProcedureId'               , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureInterventionCategories', N'InterventionCategoryId'    , N'Идентификатор на категория на интервенция.'
GO

