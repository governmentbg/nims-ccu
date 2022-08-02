PRINT 'ProgrammeDirections'
GO

CREATE TABLE [dbo].[ProgrammeDirections] (
    [ProgrammeDirectionId]  INT                NOT NULL      IDENTITY,
    [ProgrammeId]           INT                NOT NULL,
    [DirectionId]           INT                NOT NULL,
    [SubDirectionId]        INT                NULL,
    CONSTRAINT [PK_ProgrammeDirections]                      PRIMARY KEY ([ProgrammeDirectionId]),
    CONSTRAINT [FK_ProgrammeDirections_MapNodes]             FOREIGN KEY (ProgrammeId)            REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProgrammeDirections_Directions]           FOREIGN KEY ([DirectionId])          REFERENCES [dbo].[Directions] ([DirectionId]),
    CONSTRAINT [FK_ProgrammeDirections_SubDirections]        FOREIGN KEY ([SubDirectionId])       REFERENCES [dbo].[SubDirections] ([SubDirectionId])
)
GO

exec spDescTable  N'ProgrammeDirections', N'Направления на отговорни организации.'
exec spDescColumn N'ProgrammeDirections', N'ProgrammeDirectionId'                   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProgrammeDirections', N'ProgrammeId'                            , N'Идентификатор на отговорна организация.'
exec spDescColumn N'ProgrammeDirections', N'DirectionId'                            , N'Идентификатор на направление.'
exec spDescColumn N'ProgrammeDirections', N'SubDirectionId'                         , N'Идентификатор на поднаправление.'
GO
