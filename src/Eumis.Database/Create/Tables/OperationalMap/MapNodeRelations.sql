PRINT 'MapNodeRelations'
GO

CREATE TABLE [dbo].[MapNodeRelations](
    [MapNodeId]                 INT      NOT NULL,
    [ParentMapNodeId]           INT      NULL,
    [ProgrammeId]               INT      NULL,
    [ProgrammePriorityId]       INT      NULL,

 CONSTRAINT [PK_MapNodeRelations]               PRIMARY KEY     ([MapNodeId]),
 CONSTRAINT [FK_MapNodeRelations_MapNodes1]     FOREIGN KEY     ([MapNodeId])               REFERENCES [dbo].[MapNodes] ([MapNodeId]),
 CONSTRAINT [FK_MapNodeRelations_MapNodes2]     FOREIGN KEY     ([ParentMapNodeId])         REFERENCES [dbo].[MapNodes] ([MapNodeId]),
 CONSTRAINT [FK_MapNodeRelations_MapNodes3]     FOREIGN KEY     ([ProgrammeId])             REFERENCES [dbo].[MapNodes] ([MapNodeId]),
 CONSTRAINT [FK_MapNodeRelations_MapNodes4]     FOREIGN KEY     ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
);

GO

exec spDescTable  N'MapNodeRelations', N'Връзки между елементите на оперативна карта.'
exec spDescColumn N'MapNodeRelations', N'ParentMapNodeId'           , N'Удентификатор на елемента родител.'
exec spDescColumn N'MapNodeRelations', N'ProgrammeId'               , N'Идентификатор на елемента на ниво оперативна програма.'
exec spDescColumn N'MapNodeRelations', N'ProgrammePriorityId'       , N'Идентификатор на елемента на ниво приоритетна ос.'

GO

