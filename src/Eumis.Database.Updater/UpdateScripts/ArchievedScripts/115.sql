GO

ALTER TABLE [dbo].[ContractIndicators] ADD
    [ProgrammePriorityId]       INT                NULL,
    [InvestmentPriorityId]      INT                NULL,
    [SpecificTargetId]          INT                NULL,
    [FinanceSource]             INT                NULL,
    
    CONSTRAINT [FK_ContractIndicators_Countries_ProgrammePriorities]   FOREIGN KEY ([ProgrammePriorityId])        REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ContractIndicators_Countries_InvestmentPriorities]  FOREIGN KEY ([InvestmentPriorityId])       REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ContractIndicators_Countries_SpecificTargets]       FOREIGN KEY ([SpecificTargetId])           REFERENCES [dbo].[MapNodes] ([MapNodeId])

GO
