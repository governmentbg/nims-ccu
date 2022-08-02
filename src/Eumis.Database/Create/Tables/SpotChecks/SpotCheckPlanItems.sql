PRINT 'SpotCheckPlanItems'
GO

CREATE TABLE [dbo].[SpotCheckPlanItems] (
    [SpotCheckPlanItemId] INT   NOT NULL IDENTITY,
    [SpotCheckPlanId]     INT   NOT NULL,
    [Level]               INT   NOT NULL,
    [ProgrammePriorityId] INT   NULL,
    [ProcedureId]         INT   NULL,
    [ContractId]          INT   NULL,
    [ContractContractId]  INT   NULL,

    CONSTRAINT [PK_SpotCheckPlanItems]                     PRIMARY KEY ([SpotCheckPlanItemId]),
    CONSTRAINT [FK_SpotCheckPlanItems_SpotCheckPlans]      FOREIGN KEY ([SpotCheckPlanId])            REFERENCES [dbo].[SpotCheckPlans] ([SpotCheckPlanId]),
    CONSTRAINT [FK_SpotCheckPlanItems_ProgrammePriorities] FOREIGN KEY ([ProgrammePriorityId])        REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_SpotCheckPlanItems_Procedures]          FOREIGN KEY ([ProcedureId])                REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_SpotCheckPlanItems_Contracts]           FOREIGN KEY ([ContractId])                 REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_SpotCheckPlanItems_ContractContracts]   FOREIGN KEY ([ContractContractId])         REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [CHK_SpotCheckPlanItems_Level]                  CHECK ([Level]       IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_SpotCheckPlanItems_LevelProgrammePriority] CHECK ([Level] != 1 OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_SpotCheckPlanItems_LevelProcedure]         CHECK ([Level] != 2 OR [ProcedureId]         IS NOT NULL),
    CONSTRAINT [CHK_SpotCheckPlanItems_LevelContract]          CHECK ([Level] != 3 OR [ContractId]          IS NOT NULL),
    CONSTRAINT [CHK_SpotCheckPlanItems_LevelContractContract]  CHECK ([Level] != 4 OR [ContractContractId]  IS NOT NULL)
);
GO

exec spDescTable  N'SpotCheckPlanItems', N'Ниво на извършване на проверката.'
exec spDescColumn N'SpotCheckPlanItems', N'SpotCheckPlanItemId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckPlanItems', N'SpotCheckPlanId'      , N'Идентификатор на план за проверка на място.'
exec spDescColumn N'SpotCheckPlanItems', N'Level'                , N'Ниво: 1 – Приоритетна ос, 2 – Процедура, 3 - Договор за БФП; 4 - Договор с изпълнител.'
exec spDescColumn N'SpotCheckPlanItems', N'ProgrammePriorityId'  , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'SpotCheckPlanItems', N'ProcedureId'          , N'Идентификатор на процедура.'
exec spDescColumn N'SpotCheckPlanItems', N'ContractId'           , N'Идентификатор на договор за БФП.'
exec spDescColumn N'SpotCheckPlanItems', N'ContractContractId'   , N'Идентификатор на договор с изпълнител.'
GO
