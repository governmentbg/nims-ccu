PRINT 'MapNodeInterventionCategories'
GO

CREATE TABLE [dbo].[MapNodeInterventionCategories](
    [MapNodeId]                 INT             NOT NULL,
    [InterventionCategoryId]    INT             NOT NULL,
    [Amount]                    MONEY           NOT NULL,

    CONSTRAINT [PK_MapNodeInterventionCategories]                           PRIMARY KEY   ([MapNodeId], [InterventionCategoryId]),
    CONSTRAINT [FK_MapNodeInterventionCategories_MapNodes]                  FOREIGN KEY   ([MapNodeId])                 REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeInterventionCategories_InterventionCategories]    FOREIGN KEY   ([InterventionCategoryId])    REFERENCES [dbo].[InterventionCategories] ([InterventionCategoryId])
);

GO

exec spDescTable  N'MapNodeInterventionCategories', N'Инвестиционни категории на елемент на оперативна карта.'
exec spDescColumn N'MapNodeInterventionCategories', N'MapNodeId'                 , N'Идентификатор на елемент на оперативна карта.'
exec spDescColumn N'MapNodeInterventionCategories', N'InterventionCategoryId'    , N'Идентификатор на категория на интервенция.'
exec spDescColumn N'MapNodeInterventionCategories', N'Amount'                    , N'Размер на финансиране.'

GO
