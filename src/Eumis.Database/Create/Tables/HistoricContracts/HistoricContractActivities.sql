PRINT 'HistoricContractActivities'
GO

CREATE TABLE [dbo].[HistoricContractActivities] (
    [HistoricContractActivityId]    INT             NOT NULL,
    [HistoricContractId]            INT             NOT NULL,
    [Activity]                      NVARCHAR(MAX)   NOT NULL

    CONSTRAINT [PK_HistoricContractActivities]                      PRIMARY KEY ([HistoricContractActivityId]),
    CONSTRAINT [FK_HistoricContractActivities_HistoricContracts]    FOREIGN KEY ([HistoricContractId])          REFERENCES [dbo].[HistoricContracts] ([HistoricContractId])
);
GO

exec spDescTable  N'HistoricContractActivities',    N'Дейности по основни данни за договорите.'
exec spDescColumn N'HistoricContractActivities',    N'HistoricContractActivityId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractActivities',    N'HistoricContractId'           , N'Идентификатор на основни данни за договори.'
exec spDescColumn N'HistoricContractActivities',    N'Activity'                     , N'Наименование.'
GO
