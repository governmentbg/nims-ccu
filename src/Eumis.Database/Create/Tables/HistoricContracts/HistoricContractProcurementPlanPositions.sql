PRINT 'HistoricContractProcurementPlanPositions'
GO

CREATE TABLE [dbo].[HistoricContractProcurementPlanPositions] (
    [HistoricContractProcurementPlanPositionId]     INT             NOT NULL,
    [HistoricContractProcurementPlanId]             INT             NOT NULL,
    [PositionName]                                  NVARCHAR(200)   NOT NULL

    CONSTRAINT [PK_HistoricContractProcurementPlanPositions]                                    PRIMARY KEY ([HistoricContractProcurementPlanPositionId]),
    CONSTRAINT [FK_HistoricContractProcurementPlanPositions_HistoricContractProcurementPlans]   FOREIGN KEY ([HistoricContractProcurementPlanId])           REFERENCES [dbo].[HistoricContractProcurementPlans] ([HistoricContractProcurementPlanId])
);
GO

exec spDescTable  N'HistoricContractProcurementPlanPositions',  N'Обособени позиции на тръжни процедури.'
exec spDescColumn N'HistoricContractProcurementPlanPositions',  N'HistoricContractProcurementPlanPositionId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractProcurementPlanPositions',  N'HistoricContractProcurementPlanId'            , N'Идентификатор на тръжна процедура.'
exec spDescColumn N'HistoricContractProcurementPlanPositions',  N'PositionName'                                 , N'Наименование.'
GO
