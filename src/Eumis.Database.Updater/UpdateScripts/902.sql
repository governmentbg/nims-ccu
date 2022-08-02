GO
ALTER TABLE [dbo].[ContractIndicators] DROP CONSTRAINT [CHK_ContractIndicators_FinanceSource];
ALTER TABLE [dbo].[ContractIndicators] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportAdvanceNVPaymentAmounts] DROP CONSTRAINT [CHK_ContractReportAdvanceNVPaymentAmounts_FinanceSource];
ALTER TABLE [dbo].[ContractReportAdvanceNVPaymentAmounts] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportAdvancePaymentAmounts] DROP CONSTRAINT [CHK_ContractReportAdvancePaymentAmounts_FinanceSource];
ALTER TABLE [dbo].[ContractReportAdvancePaymentAmounts] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportCertAuthorityCorrections] DROP CONSTRAINT [CHK_ContractReportCertAuthorityCorrections_FinanceSource];
ALTER TABLE [dbo].[ContractReportCertAuthorityCorrections] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportCertCorrections] DROP CONSTRAINT [CHK_ContractReportCertCorrections_FinanceSource];
ALTER TABLE [dbo].[ContractReportCertCorrections] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportCorrections] DROP CONSTRAINT [CHK_ContractReportCorrections_FinanceSource];
ALTER TABLE [dbo].[ContractReportCorrections] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportPaymentCheckAmounts] DROP CONSTRAINT [CHK_ContractReportPaymentCheckAmounts_FinanceSource];
ALTER TABLE [dbo].[ContractReportPaymentCheckAmounts] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportRevalidationCertAuthorityCorrections] DROP CONSTRAINT [CHK_ContractReportRevalidationCertAuthorityCorrections_FinanceSource];
ALTER TABLE [dbo].[ContractReportRevalidationCertAuthorityCorrections] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractReportRevalidations] DROP CONSTRAINT [CHK_ContractReportRevalidations_FinanceSource];
ALTER TABLE [dbo].[ContractReportRevalidations] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ContractDebts] DROP CONSTRAINT [CHK_ContractDebts_FinanceSource];
ALTER TABLE [dbo].[ContractDebts] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[EuReimbursedAmounts] DROP CONSTRAINT [CHK_EuReimbursedAmounts_FinanceSource];
ALTER TABLE [dbo].[EuReimbursedAmounts] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[Irregularities] DROP CONSTRAINT [CHK_Irregularities_FinanceSource];
ALTER TABLE [dbo].[Irregularities] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ActuallyPaidAmounts] DROP CONSTRAINT [CHK_ActuallyPaidAmounts_FinanceSource];
ALTER TABLE [dbo].[ActuallyPaidAmounts] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[CompensationDocuments] DROP CONSTRAINT [CHK_CompensationDocuments_FinanceSource];
ALTER TABLE [dbo].[CompensationDocuments] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[FIReimbursedAmounts] DROP CONSTRAINT [CHK_FIReimbursedAmounts_FinanceSource];
ALTER TABLE [dbo].[FIReimbursedAmounts] DROP COLUMN [FinanceSource];
GO
DROP TABLE [dbo].[Prognoses]
CREATE TABLE [dbo].[Prognoses] (
    [PrognosisId]                  INT           NOT NULL IDENTITY,
    [Level]                        INT           NOT NULL,
    [ProgrammeId]                  INT           NULL,
    [ProgrammePriorityId]          INT           NULL,
    [ProcedureId]                  INT           NULL,
    [Status]                       INT           NOT NULL,

    [Year]                         INT           NOT NULL,
    [Month]                        INT           NOT NULL,

    [ContractedEuAmount]           MONEY         NULL,
    [ContractedBgAmount]           MONEY         NULL,
    [ContractedBfpAmount]          MONEY         NULL,

    [PaymentEuAmount]              MONEY         NULL,
    [PaymentBgAmount]              MONEY         NULL,
    [PaymentBfpAmount]             MONEY         NULL,

    [AdvancePaymentEuAmount]       MONEY         NULL,
    [AdvancePaymentBgAmount]       MONEY         NULL,
    [AdvancePaymentBfpAmount]      MONEY         NULL,

    [AdvanceVerPaymentEuAmount]    MONEY         NULL,
    [AdvanceVerPaymentBgAmount]    MONEY         NULL,
    [AdvanceVerPaymentBfpAmount]   MONEY         NULL,

    [IntermediatePaymentEuAmount]  MONEY         NULL,
    [IntermediatePaymentBgAmount]  MONEY         NULL,
    [IntermediatePaymentBfpAmount] MONEY         NULL,

    [FinalPaymentEuAmount]         MONEY         NULL,
    [FinalPaymentBgAmount]         MONEY         NULL,
    [FinalPaymentBfpAmount]        MONEY         NULL,

    [ApprovedEuAmount]             MONEY         NULL,
    [ApprovedBgAmount]             MONEY         NULL,
    [ApprovedBfpAmount]            MONEY         NULL,

    [CertifiedEuAmount]            MONEY         NULL,
    [CertifiedBgAmount]            MONEY         NULL,
    [CertifiedBfpAmount]           MONEY         NULL,

    [IsActivated]                  BIT           NOT NULL,
    [DeleteNote]                   NVARCHAR(MAX) NULL,
    [CreateDate]                   DATETIME2     NOT NULL,
    [ModifyDate]                   DATETIME2     NOT NULL,
    [Version]                      ROWVERSION    NOT NULL,

    CONSTRAINT [PK_Prognoses]                         PRIMARY KEY ([PrognosisId]),
    CONSTRAINT [FK_Prognoses_Programmes]              FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_Prognoses_ProgrammePriorities]     FOREIGN KEY ([ProgrammePriorityId]) REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_Prognoses_Procedures]              FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_Prognoses_Level]                  CHECK       ([Level] IN (1, 2, 3)),
    CONSTRAINT [CHK_Prognoses_LevelProgramme]         CHECK       ([Level] != 1 OR [ProgrammeId] IS NOT NULL),
    CONSTRAINT [CHK_Prognoses_LevelProgrammePriority] CHECK       ([Level] != 2 OR [ProgrammePriorityId] IS NOT NULL),
    CONSTRAINT [CHK_Prognoses_LevelProcedure]         CHECK       ([Level] != 3 OR [ProcedureId] IS NOT NULL),
    CONSTRAINT [CHK_Prognoses_Status]                 CHECK       ([Status] IN (1, 2, 3)),
    CONSTRAINT [CHK_Prognoses_Year]                   CHECK       ([Year] IN (2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023)),
    CONSTRAINT [CHK_Prognoses_Month]                  CHECK       ([Month] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProgramme]
ON [Prognoses]([ProgrammeId], [Year], [Month])
WHERE [Level] = 1;
GO
CREATE UNIQUE INDEX [UQ_Prognoses_LevelProgrammePriority]
ON [Prognoses]([ProgrammePriorityId], [Year], [Month])
WHERE [Level] = 2;
GO
CREATE UNIQUE INDEX [UQ_Prognoses_LevelProcedure]
ON [Prognoses]([ProcedureId], [Year], [Month])
WHERE [Level] = 3;
GO
ALTER TABLE [dbo].[ReimbursedAmounts] DROP CONSTRAINT [CHK_ReimbursedAmounts_FinanceSource];
GO
ALTER TABLE [dbo].[ReimbursedAmounts] DROP COLUMN [FinanceSource]

DROP TABLE [dbo].[MapNodeBudgets]
CREATE TABLE [dbo].[MapNodeBudgets](
    [MapNodeId]                INT             NOT NULL,
    [BudgetPeriodId]           INT             NOT NULL,
    [ProgrammeId]              INT             NOT NULL,
    [EuAmount]                 MONEY           NOT NULL,
    [BgAmount]                 MONEY           NOT NULL,
    [EuReservedAmount]         MONEY           NOT NULL,
    [BgReservedAmount]         MONEY           NOT NULL,
    [NextThreeWithAdvances]    MONEY           NOT NULL,
    [NextThreeWithoutAdvances] MONEY           NOT NULL,

    CONSTRAINT [PK_MapNodeBudgets]                       PRIMARY KEY     ([MapNodeId], [BudgetPeriodId]),
    CONSTRAINT [FK_MapNodeBudgets_MapNodes]              FOREIGN KEY     ([MapNodeId])                     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeBudgets_BudgetPeriods]         FOREIGN KEY     ([BudgetPeriodId])                REFERENCES [dbo].[BudgetPeriods]         ([BudgetPeriodId]),
    CONSTRAINT [FK_MapNodeBudgets_Programmes]            FOREIGN KEY     ([ProgrammeId])                   REFERENCES [dbo].[MapNodes] ([MapNodeId]),
);
GO

DROP TABLE [MapNodeFinanceSources];
GO
ALTER TABLE [dbo].[MapNodeIndicators] DROP CONSTRAINT [CHK_MapNodeIndicators_FinanceSource];
ALTER TABLE [dbo].[MapNodeIndicators] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ProcedureShares] DROP CONSTRAINT [CHK_ProcedureShares_FinanceSource];
ALTER TABLE [dbo].[ProcedureShares] DROP COLUMN [FinanceSource];
GO
ALTER TABLE [dbo].[ProcedureShares] DROP COLUMN [EuAmount];
GO
ALTER TABLE [dbo].[Procedures] ADD [ProcedureKind] INT DEFAULT 1 NOT NULL;
GO
ALTER TABLE [dbo].[Procedures] WITH CHECK ADD CONSTRAINT [CHK_Procedures_ProcedureKind] CHECK ([ProcedureKind] IN (1, 2));
ALTER TABLE [dbo].[Procedures] ADD [YEAR] INT CONSTRAINT [Default_Procedures_Year] DEFAULT 2021 NOT NULL;
ALTER TABLE [dbo].[Procedures] DROP CONSTRAINT 
    [Default_Procedures_Year],
    [FK_Procedures_ProcedureTypes],
    [FK_Procedures_AttachedProcedures]
GO
ALTER TABLE [dbo].[Procedures] WITH CHECK ADD CONSTRAINT [CHK_Procedures_Year] CHECK ([Year] IN (2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030))
ALTER TABLE [dbo].[Procedures] DROP COLUMN 
    [ListingDate],
    [AllowConcurrancyContractReports],
    [IsIntegrated],
    [ProcedureTypeId],
    [AttachedProcedureId];

DROP TABLE [dbo].[procedureTypes]; 

ALTER TABLE [dbo].[ProcedureNumbers] ADD [Year] INT CONSTRAINT [Default_ProcedureNumbers_Year] DEFAULT 2021 NOT NULL;
GO
ALTER TABLE [dbo].[ProcedureNumbers] DROP CONSTRAINT [Default_ProcedureNumbers_Year];
GO
ALTER TABLE [dbo].[ProcedureNumbers] WITH CHECK ADD CONSTRAINT [CHK_ProcedureNumbers_Year] CHECK ([Year] IN (2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030));
GO
ALTER TABLE [dbo].[ProcedureNumbers] DROP CONSTRAINT [UQ_ProcedureNumbers_ProgrammePriorityId_Number];
GO
ALTER TABLE [dbo].[ProcedureNumbers] ADD CONSTRAINT [UQ_ProcedureNumbers_ProgrammePriorityId_Number] UNIQUE ([ProgrammePriorityId], [Year], [Number]);
GO
ALTER TABLE [dbo].[IndicativeAnnualWorkingProgrammeTables] DROP COLUMN [ListingDate];

CREATE TABLE [dbo].[ProcedureApplicationSections](
    [ProcedureId]                                  INT             NOT NULL,
    [Section]                                      INT             NOT NULL,
    [OrderNum]                                     INT             NOT NULL,
	[IsSelected]                                   BIT             NOT NULL                      
    CONSTRAINT [PK_ProcedureApplicationSections]                       PRIMARY KEY     ([ProcedureId], [Section]),
    CONSTRAINT [FK_ProcedureApplicationSections_Procedures]            FOREIGN KEY     ([ProcedureId])                     REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureApplicationSections_Section]              CHECK           ([Section] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14))
);

GO
ALTER TABLE [dbo].[Companies] ADD [ProgrammePriorityType] INT CONSTRAINT [Default_Companies_ProgrammePriorityType] DEFAULT 1 NOT NULL;
GO
ALTER TABLE [dbo].[Companies] DROP CONSTRAINT 
    [DF__Companies__IsLoc__1E6F845E],
    [Default_Companies_ProgrammePriorityType];
GO
ALTER TABLE [dbo].[Companies] DROP Column [IsLocalActionGroup];
GO
ALTER TABLE [dbo].[Companies] WITH CHECK ADD CONSTRAINT [CHK_Companies_ProgrammePriorityType] CHECK ([ProgrammePriorityType] IN (1, 2, 3));
GO
CREATE TABLE [ProgrammePriorityCompanies](
    [ProgrammePriorityId]                INT            NOT NULL,
    [ProgrammePriorityType]              INT            NOT NULL,
    [CompanyId]                          INT            NOT NULL,
    [HigherOrderCompanyId]               INT            NULL,

    CONSTRAINT [PK_ProgrammePriorityCompanies]                        PRIMARY KEY     ([ProgrammePriorityId]),
    CONSTRAINT [FK_ProgrammePriorityCompanies_MapNodes]               FOREIGN KEY     ([ProgrammePriorityId])           REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_ProgrammePriorityCompanies_Companies]              FOREIGN KEY     ([CompanyId])                     REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [FK_ProgrammePriorityCompanies_Companies2]             FOREIGN KEY     ([HigherOrderCompanyId])          REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [CHK_ProgrammePriorityCompanies_ProgrammePriorityType] CHECK           ([ProgrammePriorityType] IN (1, 2, 3))
);

GO
DROP TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableTimeLimits];
GO
DROP TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableProgrammes];
GO
DROP TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableCandidates];
GO
DROP TABLE [dbo].[IndicativeAnnualWorkingProgrammeTables];
GO
DROP TABLE [dbo].[IndicativeAnnualWorkingProgrammes];

GO
DROP INDEX [UQ_EvalSessionProjectStandings_EvalSessionId_OrderNum] ON [dbo].[EvalSessionProjectStandings];
GO
ALTER TABLE [dbo].[EvalSessionProjectStandings] DROP Constraint [FK_EvalSessionProjectStandings_ProcedureBudgetComponents];
GO
ALTER TABLE [dbo].[EvalSessionProjectStandings] DROP COLUMN [ProcedureBudgetComponentId]
GO
DROP VIEW [dbo].[vwUniqueProcedureBudgetSizeTypeKidCodeIndexed];
GO
DROP TABLE [dbo].[ProcedureBudgetKidCodes];
GO
DROP TABLE [dbo].[ProcedureBudgetSizeTypes];
GO
DROP TABLE [dbo].[ProcedureBudgetComponents];

CREATE UNIQUE NONCLUSTERED INDEX [UQ_EvalSessionProjectStandings_EvalSessionId_OrderNum] ON [dbo].[EvalSessionProjectStandings]
(
    [EvalSessionId] ASC,
    [IsPreliminary] ASC,
    [OrderNum] ASC
)
WHERE ([OrderNum] IS NOT NULL AND [IsDeleted]=(0));

GO
DROP TABLE [dbo].[ProcedureDiscussions];
GO
DROP TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammeCandidates];
GO
DROP TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammeCompanies];
GO
DROP TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammes];
GO
DROP TABLE [dbo].[ProjectMonitorstatResponses];
GO
DROP TABLE [dbo].[ProjectMonitorstatRequests];
GO
DROP TABLE [dbo].[ProcedureMonitorstatRequests];
GO
DROP TABLE [dbo].[ProcedureMonitorstatDocuments];
GO
DROP TABLE [dbo].[PublicDiscussionComments];
GO
DROP TABLE [dbo].[PublicDiscussionGuidelines];
GO
DROP TABLE [dbo].[PublicDiscussions];
GO
