PRINT 'HistoricContractContractedAmounts'
GO

CREATE TABLE [dbo].[HistoricContractContractedAmounts] (
    [HistoricContractContractedAmountId]    INT         NOT NULL,
    [HistoricContractId]                    INT         NOT NULL,
    [ContractedDate]                        DATETIME2   NOT NULL,
    [ContractedEuAmount]                    MONEY       NULL,
    [ContractedBgAmount]                    MONEY       NULL,
    [ContractedSeftAmount]                  MONEY       NULL,
    [IsLast]                                BIT         NOT NULL

    CONSTRAINT [PK_HistoricContractContractedAmounts]                       PRIMARY KEY ([HistoricContractContractedAmountId]),
    CONSTRAINT [FK_HistoricContractContractedAmounts_HistoricContracts]     FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractContractedAmounts', N'Договорени суми.'
exec spDescColumn N'HistoricContractContractedAmounts', N'HistoricContractContractedAmountId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractContractedAmounts', N'HistoricContractId'                   , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractContractedAmounts', N'ContractedDate'                       , N'Дата на договориране/промяна.'
exec spDescColumn N'HistoricContractContractedAmounts', N'ContractedEuAmount'                   , N'Безвъзмездна финансова помощ - Европейски съюз.'
exec spDescColumn N'HistoricContractContractedAmounts', N'ContractedBgAmount'                   , N'Безвъзмездна финансова помощ - Национално финансиране.'
exec spDescColumn N'HistoricContractContractedAmounts', N'ContractedSeftAmount'                 , N'Собствено финансиране.'
exec spDescColumn N'HistoricContractContractedAmounts', N'IsLast'                               , N'Маркер за последен запис.'
GO
