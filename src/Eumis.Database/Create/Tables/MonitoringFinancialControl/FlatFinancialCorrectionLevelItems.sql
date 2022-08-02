PRINT 'FlatFinancialCorrectionLevelItems'
GO

CREATE TABLE [dbo].[FlatFinancialCorrectionLevelItems] (
    [FlatFinancialCorrectionLevelItemId]    INT                    NOT NULL IDENTITY,
    [FlatFinancialCorrectionId]             INT                    NOT NULL,
    [Type]                                  NVARCHAR(50)           NOT NULL,
    [ProgrammeId]                           INT                    NULL,
    [ProgrammePriorityId]                   INT                    NULL,
    [ProcedureId]                           INT                    NULL,
    [ContractId]                            INT                    NULL,
    [ContractContractId]                    INT                    NULL,
    [Percent]                               MONEY                  NULL,
    [EuAmount]                              MONEY                  NULL,
    [BgAmount]                              MONEY                  NULL,
    [TotalAmount]                           MONEY                  NULL,

    CONSTRAINT [PK_FlatFinancialCorrectionLevelItems]                     PRIMARY KEY ([FlatFinancialCorrectionLevelItemId]),
    CONSTRAINT [FK_FlatFinancialCorrectionLevelItems_Blobs]               FOREIGN KEY ([FlatFinancialCorrectionId])  REFERENCES [dbo].[FlatFinancialCorrections] ([FlatFinancialCorrectionId]),
    CONSTRAINT [FK_FlatFinancialCorrectionLevelItems_Programmes]          FOREIGN KEY ([ProgrammeId])                REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_FlatFinancialCorrectionLevelItems_ProgrammePriorities] FOREIGN KEY ([ProgrammePriorityId])        REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_FlatFinancialCorrectionLevelItems_Procedures]          FOREIGN KEY ([ProcedureId])                REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_FlatFinancialCorrectionLevelItems_Contracts]           FOREIGN KEY ([ContractId])                 REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_FlatFinancialCorrectionLevelItems_ContractContracts]   FOREIGN KEY ([ContractContractId])         REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [CHK_FlatFinancialCorrectionLevelItems_ClassProgramme]           CHECK       ([Type] != 'Programme'               OR [ProgrammeId] IS NOT NULL),
    CONSTRAINT [CHK_FlatFinancialCorrectionLevelItems_ClassProgrammePriority]   CHECK       ([Type] != 'ProgrammePriority'       OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_FlatFinancialCorrectionLevelItems_ClassProcedure]           CHECK       ([Type] != 'Procedure'               OR [ProcedureId] IS NOT NULL),
    CONSTRAINT [CHK_FlatFinancialCorrectionLevelItems_ClassContract]            CHECK       ([Type] != 'Contract'                OR [ContractId] IS NOT NULL),
    CONSTRAINT [CHK_FlatFinancialCorrectionLevelItems_ClassContractContract]    CHECK       ([Type] != 'ContractContract'        OR [ContractContractId] IS NOT NULL)
);
GO

exec spDescTable  N'FlatFinancialCorrectionLevelItems', N'Обхват на финансови корекции за системни пропуски.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'FlatFinancialCorrectionLevelItemId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'Type'                                , N'Тип на обхват.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'ProgrammeId'                         , N'Идентификатор на програма.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'ProgrammePriorityId'                 , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'ProcedureId'                         , N'Идентификатор на процедура.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'ContractId'                          , N'Идентификатор на договор за БФП.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'ContractContractId'                  , N'Идентификатор на Договор с изпълнител.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'Percent'                             , N'Процент на наложената финансова корекция.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'EuAmount'                            , N'Стойност финансиране ЕС.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'BgAmount'                            , N'Стойност национално финансиране.'
exec spDescColumn N'FlatFinancialCorrectionLevelItems', N'TotalAmount'                         , N'Обща сума.'

GO
