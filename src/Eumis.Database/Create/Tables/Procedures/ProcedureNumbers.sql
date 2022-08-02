PRINT 'ProcedureNumbers'
GO

CREATE TABLE [dbo].[ProcedureNumbers] (
    [ProcedureId]           INT NOT NULL,
    [ProgrammePriorityId]   INT NOT NULL,
    [Number]                INT NOT NULL,
    CONSTRAINT [PK_ProcedureNumbers]                            PRIMARY KEY ([ProcedureId], [ProgrammePriorityId]),
    CONSTRAINT [FK_ProcedureNumbers_Procedures]                 FOREIGN KEY ([ProcedureId]) REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureNumbers_MapNodes]        FOREIGN KEY ([ProgrammePriorityId]) REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [UQ_ProcedureNumbers_ProgrammePriorityId_Number] UNIQUE ([ProgrammePriorityId], [Number])
);
GO

exec spDescTable  N'ProcedureNumbers', N'Номер на процедура.'
exec spDescColumn N'ProcedureNumbers', N'ProcedureId', N'Идентификатор на процедура'
exec spDescColumn N'ProcedureNumbers', N'ProgrammePriorityId', N'Идентификатор на приоритетна ос.'
exec spDescColumn N'ProcedureNumbers', N'Number', N'Номер на процедура.'
GO
