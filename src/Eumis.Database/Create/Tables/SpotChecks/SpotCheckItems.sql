PRINT 'SpotCheckItems'
GO

CREATE TABLE [dbo].[SpotCheckItems] (
    [SpotCheckItemId]     INT   NOT NULL IDENTITY,
    [SpotCheckId]         INT   NOT NULL,
    [Level]               INT   NOT NULL,
    [ProgrammePriorityId] INT   NULL,
    [ProcedureId]         INT   NULL,
    [ContractId]          INT   NULL,
    [ContractContractId]  INT   NULL,

    CONSTRAINT [PK_SpotCheckItems]                     PRIMARY KEY ([SpotCheckItemId]),
    CONSTRAINT [FK_SpotCheckItems_SpotChecks]          FOREIGN KEY ([SpotCheckId])            REFERENCES [dbo].[SpotChecks] ([SpotCheckId]),
    CONSTRAINT [FK_SpotCheckItems_ProgrammePriorities] FOREIGN KEY ([ProgrammePriorityId])    REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_SpotCheckItems_Procedures]          FOREIGN KEY ([ProcedureId])            REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_SpotCheckItems_Contracts]           FOREIGN KEY ([ContractId])             REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_SpotCheckItems_ContractContracts]   FOREIGN KEY ([ContractContractId])     REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [CHK_SpotCheckItems_Level]                  CHECK ([Level]       IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_SpotCheckItems_LevelProgrammePriority] CHECK ([Level] != 1 OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_SpotCheckItems_LevelProcedure]         CHECK ([Level] != 2 OR [ProcedureId]         IS NOT NULL),
    CONSTRAINT [CHK_SpotCheckItems_LevelContract]          CHECK ([Level] != 3 OR [ContractId]          IS NOT NULL),
    CONSTRAINT [CHK_SpotCheckItems_LevelContractContract]  CHECK ([Level] != 4 OR [ContractContractId]  IS NOT NULL)
);
GO

exec spDescTable  N'SpotCheckItems', N'Ниво на извършване на проверката.'
exec spDescColumn N'SpotCheckItems', N'SpotCheckItemId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckItems', N'SpotCheckId'          , N'Идентификатор на проверка на място.'
exec spDescColumn N'SpotCheckItems', N'Level'                , N'Ниво: 1 – Приоритетна ос, 2 – Процедура, 3 - Договор за БФП; 4 - Договор с изпълнител.'
exec spDescColumn N'SpotCheckItems', N'ProgrammePriorityId'  , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'SpotCheckItems', N'ProcedureId'          , N'Идентификатор на процедура.'
exec spDescColumn N'SpotCheckItems', N'ContractId'           , N'Идентификатор на договор за БФП.'
exec spDescColumn N'SpotCheckItems', N'ContractContractId'   , N'Идентификатор на договор с изпълнител.'
GO
