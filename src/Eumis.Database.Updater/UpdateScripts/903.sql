GO
DROP TABLE [dbo].[MapNodeInstitutions];
GO
DROP TABLE [dbo].[MapNodeInterventionCategories];
GO
CREATE TABLE [dbo].[MapNodeDirections] (
    [MapNodeDirectionId]    INT                NOT NULL      IDENTITY,
    [MapNodeId]             INT                NOT NULL,
    [DirectionId]           INT                NOT NULL,
    [SubDirectionId]        INT                NULL,
    CONSTRAINT [PK_MapNodeDirections]                      PRIMARY KEY ([MapNodeDirectionId]),
    CONSTRAINT [FK_MapNodeDirections_MapNodes]             FOREIGN KEY ([MapNodeId])            REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeDirections_Directions]           FOREIGN KEY ([DirectionId])          REFERENCES [dbo].[Directions] ([DirectionId]),
    CONSTRAINT [FK_MapNodeDirections_SubDirections]        FOREIGN KEY ([SubDirectionId])       REFERENCES [dbo].[SubDirections] ([SubDirectionId])
);
GO
INSERT INTO [dbo].[MapNodeDirections]
SELECT 
	[ProgrammeId] MapNodeId,
	[DirectionId],
	[SubDirectionId]
FROM [dbo].[ProgrammeDirections];
GO
ALTER TABLE [dbo].[MapNodes] DROP CONSTRAINT [FK_MapNodes_ProgrammeGroups];
GO
ALTER TABLE [dbo].[MapNodes] DROP COLUMN [ProgrammeGroupId];
GO
DROP TABLE [dbo].[ProgrammeGroups];
GO
ALTER TABLE [dbo].[CheckSheets] DROP CONSTRAINT [FK_CheckSheets_ProgrammeCheckLists];
GO
ALTER TABLE [dbo].[CheckSheets] DROP COLUMN [ProgrammeCheckListId]; 
GO
DROP TABLE [dbo].[ProgrammeCheckListVersionXmlFiles];
GO
DROP TABLE [dbo].[ProgrammeCheckListVersionXmls];
GO

DROP TABLE [dbo].[ProgrammeCheckLists];
GO
DROP  TABLE [dbo].[MonitorstatMapNodes]
GO
DROP  TABLE [dbo].[MonitorstatReports]
GO
DROP  TABLE [dbo].[MonitorstatSurveys]
GO

ALTER TABLE [dbo].[MapNodes] DROP CONSTRAINT [FK_MapNodes_InvestmentPriorities];
GO
ALTER TABLE [dbo].[MapNodes] DROP COLUMN [InvestmentPriorityId];
GO
DROP  TABLE [dbo].[InvestmentPriorities];
GO
CREATE TABLE [dbo].[ProcedureDirections] (
    [ProcedureDirectionId]    INT                NOT NULL      IDENTITY,
    [ProcedureId]             INT                NOT NULL,
	[ProgrammePriorityId]     INT                NOT NULL,
    [DirectionId]             INT                NOT NULL,
    [SubDirectionId]          INT                NULL,
	[Amount]                  MONEY              NULL,
    CONSTRAINT [PK_ProcedureDirections]                      PRIMARY KEY ([ProcedureDirectionId]),
    CONSTRAINT [FK_ProcedureDirections_Procedures]           FOREIGN KEY ([ProcedureId])          REFERENCES [dbo].[Procedures] ([ProcedureId]),
	CONSTRAINT [FK_ProcedureDirections_MapNodes]             FOREIGN KEY ([ProgrammePriorityId])  REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProcedureDirections_Directions]           FOREIGN KEY ([DirectionId])          REFERENCES [dbo].[Directions] ([DirectionId]),
    CONSTRAINT [FK_ProcedureDirections_SubDirections]        FOREIGN KEY ([SubDirectionId])       REFERENCES [dbo].[SubDirections] ([SubDirectionId])
);
GO
