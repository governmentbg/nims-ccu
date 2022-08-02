PRINT 'ContractDifferentiatedPositions'
GO

CREATE TABLE [dbo].[ContractDifferentiatedPositions] (
    [ContractDifferentiatedPositionId]  INT                 NOT NULL IDENTITY,
    [ContractProcurementPlanId]         INT                 NOT NULL,
    [ContractContractId]                INT                 NULL,
    [Gid]                               UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [Name]                              NVARCHAR(MAX)       NOT NULL,
    [SubmittedOffersCount]              INT                 NULL,
    [RankedOffersCount]                 INT                 NULL,
    [Comment]                           NVARCHAR(MAX)       NULL,

    CONSTRAINT [PK_ContractDifferentiatedPositions]                             PRIMARY KEY ([ContractDifferentiatedPositionId]),
    CONSTRAINT [FK_ContractDifferentiatedPositions_ContractProcurementPlans]    FOREIGN KEY ([ContractProcurementPlanId])   REFERENCES [dbo].[ContractProcurementPlans] ([ContractProcurementPlanId]),
    CONSTRAINT [FK_ContractDifferentiatedPositions_ContractContracts]           FOREIGN KEY ([ContractContractId])          REFERENCES [dbo].[ContractContracts] ([ContractContractId])
);
GO

exec spDescTable  N'ContractDifferentiatedPositions', N'Обособени позиции към договор.'
exec spDescColumn N'ContractDifferentiatedPositions', N'ContractDifferentiatedPositionId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractDifferentiatedPositions', N'ContractProcurementPlanId'          , N'Идентификатор на процедура за избор на изпълнител.'
exec spDescColumn N'ContractDifferentiatedPositions', N'ContractContractId'                 , N'Идентификатор на договор с изпълнител.'
exec spDescColumn N'ContractDifferentiatedPositions', N'Gid'                                , N'Публичен идентификатор.'

exec spDescColumn N'ContractDifferentiatedPositions', N'Name'                               , N'Наименование.'
exec spDescColumn N'ContractDifferentiatedPositions', N'SubmittedOffersCount'               , N'Брой подадени оферти.'
exec spDescColumn N'ContractDifferentiatedPositions', N'RankedOffersCount'                  , N'Брой класирани оферти.'
exec spDescColumn N'ContractDifferentiatedPositions', N'Comment'                            , N'Коментар.'
GO
