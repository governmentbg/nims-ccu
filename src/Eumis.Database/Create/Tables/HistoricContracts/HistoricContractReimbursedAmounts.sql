PRINT 'HistoricContractReimbursedAmounts'
GO

CREATE TABLE [dbo].[HistoricContractReimbursedAmounts] (
    [HistoricContractReimbursedAmountId]    INT         NOT NULL,
    [HistoricContractId]                    INT         NOT NULL,
    [ReimbursementDate]                     DATETIME2   NOT NULL,
    [ReimbursedPrincipalEuAmount]           MONEY       NULL,
    [ReimbursedPrincipalBgAmount]           MONEY       NULL

    CONSTRAINT [PK_HistoricContractReimbursedAmounts]                       PRIMARY KEY ([HistoricContractReimbursedAmountId]),
    CONSTRAINT [FK_HistoricContractReimbursedAmounts_HistoricContracts]     FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractReimbursedAmounts', N'Възстановени суми.'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'HistoricContractReimbursedAmountId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'HistoricContractId'                   , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'ReimbursementDate'                    , N'Дата на възстановяване.'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'ReimbursedPrincipalEuAmount'          , N'Възстановена БПФ - ЕС (Главница).'
exec spDescColumn N'HistoricContractReimbursedAmounts', N'ReimbursedPrincipalBgAmount'          , N'Възстановена БПФ - НФ (Главница).'
GO
