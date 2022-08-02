PRINT 'ProcedureProgrammes'
GO

CREATE TABLE [dbo].[ProcedureProgrammes] (
    [ProcedureId]                INT NOT NULL,
    [ProgrammeId]                INT NOT NULL,

    CONSTRAINT [PK_ProcedureProgrammes]                 PRIMARY KEY ([ProcedureId], [ProgrammeId]),
    CONSTRAINT [FK_ProcedureProgrammes_Procedures]      FOREIGN KEY ([ProcedureId])      REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureProgrammes_MapNodes]        FOREIGN KEY ([ProgrammeId]) REFERENCES [dbo].[MapNodes] ([MapNodeId])
);
GO

exec spDescTable  N'ProcedureProgrammes', N'Програми, към които има дялове на процедура'
exec spDescColumn N'ProcedureProgrammes', N'ProcedureId'      , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureProgrammes', N'ProgrammeId'      , N'Идентификатор на програма.'
GO

