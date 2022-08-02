PRINT 'HistoricContractActuallyPaidAmounts'
GO

CREATE TABLE [dbo].[HistoricContractActuallyPaidAmounts] (
    [HistoricContractActuallyPaidAmountId]  INT             NOT NULL,
    [HistoricContractId]                    INT             NOT NULL,
    [PaymentDate]                           DATETIME2       NOT NULL,
    [PaidEuAmount]                          MONEY           NULL,
    [PaidBgAmount]                          MONEY           NULL

    CONSTRAINT [PK_HistoricContractActuallyPaidAmounts]                     PRIMARY KEY ([HistoricContractActuallyPaidAmountId]),
    CONSTRAINT [FK_HistoricContractActuallyPaidAmounts_HistoricContracts]   FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractActuallyPaidAmounts'    , N'Реално изплатени суми.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'HistoricContractActuallyPaidAmountId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'HistoricContractId'                     , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'PaymentDate'                            , N'Дата.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'PaidEuAmount'                           , N'Безвъзмездна финансова помощ - Европейски съюз.'
exec spDescColumn N'HistoricContractActuallyPaidAmounts'    , N'PaidBgAmount'                           , N'Безвъзмездна финансова помощ - Национално финансиране.'
GO
