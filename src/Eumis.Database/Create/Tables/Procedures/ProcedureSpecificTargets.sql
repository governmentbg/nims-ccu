PRINT 'ProcedureSpecificTargets'
GO

CREATE TABLE [dbo].[ProcedureSpecificTargets] (
    [ProcedureId]                INT NOT NULL,
    [SpecificTargetId]           INT NOT NULL,

    CONSTRAINT [PK_ProcedureSpecificTargets]                 PRIMARY KEY ([ProcedureId], [SpecificTargetId]),
    CONSTRAINT [FK_ProcedureSpecificTargets_Procedures]      FOREIGN KEY ([ProcedureId])      REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureSpecificTargets_MapNodes]        FOREIGN KEY ([SpecificTargetId]) REFERENCES [dbo].[MapNodes] ([MapNodeId])
);
GO

exec spDescTable  N'ProcedureSpecificTargets', N'Специфични цели за процедура.'
exec spDescColumn N'ProcedureSpecificTargets', N'ProcedureId'           , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureSpecificTargets', N'SpecificTargetId'      , N'Идентификатор на специфична цел.'
GO

