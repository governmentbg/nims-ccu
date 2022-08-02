PRINT 'CertAuthorityCheckLevelItems'
GO

CREATE TABLE [dbo].[CertAuthorityCheckLevelItems] (
    [CertAuthorityCheckLevelItemId]    INT                    NOT NULL IDENTITY,
    [CertAuthorityCheckId]             INT                    NOT NULL,
    [Level]                            INT                    NOT NULL,
    [ProgrammeId]                      INT                    NULL,
    [ProgrammePriorityId]              INT                    NULL,
    [ProcedureId]                      INT                    NULL,
    [ContractId]                       INT                    NULL,

    CONSTRAINT [PK_CertAuthorityCheckLevelItems]                     PRIMARY KEY ([CertAuthorityCheckLevelItemId]),
    CONSTRAINT [FK_CertAuthorityCheckLevelItems_CertAuthorityChecks] FOREIGN KEY ([CertAuthorityCheckId])       REFERENCES [dbo].[CertAuthorityChecks] ([CertAuthorityCheckId]),
    CONSTRAINT [FK_CertAuthorityCheckLevelItems_Programmes]          FOREIGN KEY ([ProgrammeId])                REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_CertAuthorityCheckLevelItems_ProgrammePriorities] FOREIGN KEY ([ProgrammePriorityId])        REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_CertAuthorityCheckLevelItems_Procedures]          FOREIGN KEY ([ProcedureId])                REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_CertAuthorityCheckLevelItems_Contracts]           FOREIGN KEY ([ContractId])                 REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_CertAuthorityCheckLevelItems_Level]                  CHECK ([Level]       IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_CertAuthorityCheckLevelItems_LevelProgramme]         CHECK ([Level] != 1 OR [ProgrammeId]         IS NOT NULL),
    CONSTRAINT [CHK_CertAuthorityCheckLevelItems_LevelProgrammePriority] CHECK ([Level] != 2 OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_CertAuthorityCheckLevelItems_LevelProcedure]         CHECK ([Level] != 3 OR [ProcedureId]         IS NOT NULL),
    CONSTRAINT [CHK_CertAuthorityCheckLevelItems_LevelContract]          CHECK ([Level] != 4 OR [ContractId]          IS NOT NULL)
);
GO

exec spDescTable  N'CertAuthorityCheckLevelItems', N'Ниво на извършване на проверката.'
exec spDescColumn N'CertAuthorityCheckLevelItems', N'CertAuthorityCheckLevelItemId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CertAuthorityCheckLevelItems', N'CertAuthorityCheckId'         , N'Идентификатор на проверки на СО.'
exec spDescColumn N'CertAuthorityCheckLevelItems', N'Level'                        , N'Ниво: 1 – Оперативна програма, 2 – Приоритетна ос, 3 – Процедура, 4 - Договор за БФП.'
exec spDescColumn N'CertAuthorityCheckLevelItems', N'ProgrammeId'                  , N'Идентификатор на програма.'
exec spDescColumn N'CertAuthorityCheckLevelItems', N'ProgrammePriorityId'          , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'CertAuthorityCheckLevelItems', N'ProcedureId'                  , N'Идентификатор на процедура.'
exec spDescColumn N'CertAuthorityCheckLevelItems', N'ContractId'                   , N'Идентификатор на договор за БФП.'
GO
