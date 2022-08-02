PRINT 'HistoricContractProcurementPlans'
GO

CREATE TABLE [dbo].[HistoricContractProcurementPlans] (
    [HistoricContractProcurementPlanId]     INT             NOT NULL,
    [HistoricContractId]                    INT             NOT NULL,
    [ProcurementPlanName]                   NVARCHAR(MAX)   NOT NULL,
    [Amount]                                MONEY           NOT NULL

    CONSTRAINT [PK_HistoricContractProcurementPlans]                    PRIMARY KEY ([HistoricContractProcurementPlanId]),
    CONSTRAINT [FK_HistoricContractProcurementPlans_HistoricContracts]  FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractProcurementPlans',  N'Тръжни процедури.'
exec spDescColumn N'HistoricContractProcurementPlans',  N'HistoricContractProcurementPlanId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractProcurementPlans',  N'HistoricContractId'               , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractProcurementPlans',  N'ProcurementPlanName'              , N'Предмет на предвидената процедура.'
exec spDescColumn N'HistoricContractProcurementPlans',  N'Amount'                           , N'Прогнозна стойност.'
GO
