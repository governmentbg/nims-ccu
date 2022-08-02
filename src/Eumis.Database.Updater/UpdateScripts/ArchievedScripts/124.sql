GO

ALTER TABLE [dbo].[MapNodes]
ADD [Status] INT  NOT NULL CONSTRAINT DEFAULT_Status DEFAULT 2;
GO

ALTER TABLE [dbo].[MapNodes]
ADD
  CONSTRAINT [CHK_MapNodes_Status] CHECK ([Status] IN (1, 2, 3));
GO

ALTER TABLE [dbo].[MapNodes]
DROP
  CONSTRAINT DEFAULT_Status;
GO

CREATE TABLE [dbo].[MapNodeFinanceSources](
    [MapNodeId]        INT             NOT NULL,
    [FinanceSource]    INT             NOT NULL,

    CONSTRAINT [PK_MapNodeFinanceSources]                PRIMARY KEY     ([MapNodeId], [FinanceSource]),
    CONSTRAINT [FK_MapNodeFinanceSources_MapNodes]       FOREIGN KEY     ([MapNodeId])       REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_MapNodeFinanceSources_FinanceSource] CHECK           ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

INSERT INTO [MapNodeFinanceSources]
    ([MapNodeId], [FinanceSource])
VALUES
    (1          ,               1),
    (1          ,               4),
    (2          ,               2),
    (2          ,               3),
    (3          ,               2),
    (4          ,               1),
    (4          ,               4),
    (5          ,               2),
    (6          ,               2),
    (6          ,               3),
    (7          ,               1),
    (7          ,               2),
    (7          ,               4),
    (8          ,               5);
GO

UPDATE [dbo].[MapNodes]
SET [InvestmentPriorityId] =
    (SELECT [InvestmentPriorityId]
     FROM [dbo].[InvestmentPriorities]
     WHERE [Code] = '00' AND [Name] = 'Без инвестиционен приоритет')
WHERE [MapNodeId] IN (80101, 80102, 80103, 80104) AND [Type] = 'InvestmentPriority' AND [Code] = '00'


ALTER TABLE [dbo].[ContractIndicators] ADD
    CONSTRAINT [CHK_ContractIndicators_FinanceSource]                       CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[ContractReportAdvancePaymentAmounts] DROP
    CONSTRAINT [CHK_ContractReportAdvancePaymentAmounts_FinanceSource]
GO
ALTER TABLE [dbo].[ContractReportAdvancePaymentAmounts] ADD
    CONSTRAINT [CHK_ContractReportAdvancePaymentAmounts_FinanceSource]      CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[ContractReportCertCorrections] ADD
    CONSTRAINT [CHK_ContractReportCertCorrections_FinanceSource]            CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[ContractReportCorrections] ADD
    CONSTRAINT [CHK_ContractReportCorrections_FinanceSource]                CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[ContractReportRevalidations] ADD
    CONSTRAINT [CHK_ContractReportRevalidations_FinanceSource]              CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[EuReimbursedAmounts] DROP
    CONSTRAINT [CHK_EuReimbursedAmounts_FinanceSource]
GO
ALTER TABLE [dbo].[EuReimbursedAmounts] ADD
    CONSTRAINT [CHK_EuReimbursedAmounts_FinanceSource]                      CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[Irregularities] DROP
    CONSTRAINT [CHK_Irregularities_FinanceSource]
GO
ALTER TABLE [dbo].[Irregularities] ADD
    CONSTRAINT [CHK_Irregularities_FinanceSource]                           CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[CompensationDocuments] ADD
    CONSTRAINT [CHK_CompensationDocuments_FinanceSource]                    CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[MapNodeBudgets] DROP
    CONSTRAINT [CHK_MapNodeBudgets_FinanceSource]
GO
ALTER TABLE [dbo].[MapNodeBudgets] ADD
    CONSTRAINT [CHK_MapNodeBudgets_FinanceSource]                           CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[MapNodeIndicators] DROP
    CONSTRAINT [CHK_MapNodeIndicators_FinanceSource]
GO
ALTER TABLE [dbo].[MapNodeIndicators] ADD
    CONSTRAINT [CHK_MapNodeIndicators_FinanceSource]                        CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO


ALTER TABLE [dbo].[ProcedureShares] DROP
    CONSTRAINT [CHK_ProcedureShares_FinanceSource]
GO
ALTER TABLE [dbo].[ProcedureShares] ADD
    CONSTRAINT [CHK_ProcedureShares_FinanceSource]                          CHECK        ([FinanceSource]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
GO

-- adding foreign key to MapNodeFinanceSources in MapNodeBudgets
DELETE mnb
FROM MapNodeBudgets mnb
JOIN MapNodeRelations mnr ON mnb.MapNodeId = mnr.MapNodeId
WHERE EuAmount = 0 AND BgAmount = 0 AND EuReservedAmount = 0 AND EuReservedAmount = 0 AND
      mnb.FinanceSource NOT IN (SELECT mnfs.FinanceSource FROM MapNodeFinanceSources mnfs WHERE mnfs.MapNodeId = mnr.ProgrammeId);

ALTER TABLE [dbo].[MapNodeBudgets]
ADD [ProgrammeId] INT NOT NULL
    CONSTRAINT DEFAULT_ProgrammeId DEFAULT 1,
    CONSTRAINT [FK_MapNodeBudgets_Programmes] FOREIGN KEY ([ProgrammeId]) REFERENCES [dbo].[MapNodes] ([MapNodeId]);
GO

UPDATE mnb
SET [ProgrammeId] = mnr.[ProgrammeId]
FROM [dbo].[MapNodeBudgets] mnb
JOIN [dbo].[MapNodeRelations] mnr ON mnb.MapNodeId = mnr.MapNodeId;
GO

ALTER TABLE [dbo].[MapNodeBudgets]
DROP CONSTRAINT DEFAULT_ProgrammeId;
GO

ALTER TABLE [dbo].[MapNodeBudgets]
ADD CONSTRAINT [FK_MapNodeBudgets_MapNodeFinanceSources] FOREIGN KEY ([ProgrammeId], [FinanceSource]) REFERENCES [dbo].[MapNodeFinanceSources] ([MapNodeId], [FinanceSource]);
GO
