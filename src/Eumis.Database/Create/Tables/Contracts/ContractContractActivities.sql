PRINT 'ContractContractActivities'
GO

CREATE TABLE [dbo].[ContractContractActivities] (
    [ContractContractActivityId]    INT                 NOT NULL IDENTITY,
    [ContractContractId]            INT                 NOT NULL,
    [ContractActivityId]            INT                 NULL,
    [ContractBudgetLevel3AmountId]  INT                 NOT NULL,

    CONSTRAINT [PK_ContractContractActivities]                              PRIMARY KEY ([ContractContractActivityId]),
    CONSTRAINT [FK_ContractContractActivities_ContractContracts]            FOREIGN KEY ([ContractContractId])              REFERENCES [dbo].[ContractContracts] ([ContractContractId]),
    CONSTRAINT [FK_ContractContractActivities_ContractActivities]           FOREIGN KEY ([ContractActivityId])              REFERENCES [dbo].[ContractActivities] ([ContractActivityId]),
    CONSTRAINT [FK_ContractContractActivities_ContractBudgetLevel3Amounts]  FOREIGN KEY ([ContractBudgetLevel3AmountId])    REFERENCES [dbo].[ContractBudgetLevel3Amounts] ([ContractBudgetLevel3AmountId])
);
GO

exec spDescTable  N'ContractContractActivities', N'Дейности по бюджетни редове към договори с изпълнител.'
exec spDescColumn N'ContractContractActivities', N'ContractContractActivityId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractContractActivities', N'ContractContractId'            , N'Идентификатор на договор с изпълнител.'
exec spDescColumn N'ContractContractActivities', N'ContractActivityId'            , N'Идентификатор на дейност.'
exec spDescColumn N'ContractContractActivities', N'ContractBudgetLevel3AmountId'  , N'Идентификатор на бюджетен ред към договор.'
GO
