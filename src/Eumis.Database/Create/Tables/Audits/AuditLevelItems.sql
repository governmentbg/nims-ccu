PRINT 'AuditLevelItems'
GO

CREATE TABLE [dbo].[AuditLevelItems] (
    [AuditLevelItemId]    INT   NOT NULL IDENTITY,
    [AuditId]             INT   NOT NULL,
    [Level]               INT   NOT NULL,
    [ProgrammePriorityId] INT   NULL,
    [ProcedureId]         INT   NULL,
    [ContractId]          INT   NULL,
    [ContractContractId]  INT   NULL,

    CONSTRAINT [PK_AuditLevelItems]                     PRIMARY KEY ([AuditLevelItemId]),
    CONSTRAINT [FK_AuditLevelItems_Audits]              FOREIGN KEY ([AuditId])                    REFERENCES [dbo].[Audits] ([AuditId]),
    CONSTRAINT [FK_AuditLevelItems_ProgrammePriorities] FOREIGN KEY ([ProgrammePriorityId])        REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_AuditLevelItems_Procedures]          FOREIGN KEY ([ProcedureId])                REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_AuditLevelItems_Contracts]           FOREIGN KEY ([ContractId])                 REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_AuditLevelItemss_ContractContracts]  FOREIGN KEY ([ContractContractId])         REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [CHK_AuditLevelItems_Level]                  CHECK ([Level]       IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_AuditLevelItems_LevelProgrammePriority] CHECK ([Level] != 1 OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_AuditLevelItems_LevelProcedure]         CHECK ([Level] != 2 OR [ProcedureId]         IS NOT NULL),
    CONSTRAINT [CHK_AuditLevelItems_LevelContract]          CHECK ([Level] != 3 OR [ContractId]          IS NOT NULL),
    CONSTRAINT [CHK_AuditLevelItems_LevelContractContract]  CHECK ([Level] != 4 OR [ContractContractId]  IS NOT NULL)
);
GO

exec spDescTable  N'AuditLevelItems', N'Ниво на извършване на проверката.'
exec spDescColumn N'AuditLevelItems', N'AuditLevelItemId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AuditLevelItems', N'AuditId'              , N'Идентификатор на проверки на СО.'
exec spDescColumn N'AuditLevelItems', N'Level'                , N'Ниво: 1 – Приоритетна ос, 2 – Процедура, 3 - Договор за БФП; 4 - Договор с изпълнител.'
exec spDescColumn N'AuditLevelItems', N'ProgrammePriorityId'  , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'AuditLevelItems', N'ProcedureId'          , N'Идентификатор на процедура.'
exec spDescColumn N'AuditLevelItems', N'ContractId'           , N'Идентификатор на договор за БФП.'
exec spDescColumn N'AuditLevelItems', N'ContractContractId'   , N'Идентификатор на договор с изпълнител.'
GO
