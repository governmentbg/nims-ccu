GO

-- BudgetPeriods
ALTER TABLE [dbo].[BudgetPeriods]
ADD [Year] INT NOT NULL CONSTRAINT DEFAULT_Year DEFAULT 0;
GO

UPDATE [dbo].[BudgetPeriods]
SET [Year] = PARSE([Name] AS INT);
GO

ALTER TABLE [dbo].[BudgetPeriods]
DROP CONSTRAINT DEFAULT_Year;
GO

-- Prognoses
CREATE TABLE [dbo].[Prognoses] (
    [PrognosisId]                  INT           NOT NULL IDENTITY,
    [Level]                        INT           NOT NULL,
    [ProgrammeId]                  INT           NULL,
    [ProgrammePriorityId]          INT           NULL,
    [ProcedureId]                  INT           NULL,
    [Status]                       INT           NOT NULL,

    [Year]                         INT           NOT NULL,
    [Quarter]                      INT           NOT NULL,
    [FinanceSource]                INT           NOT NULL,

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
    CONSTRAINT [CHK_Prognoses_Quarter]                CHECK       ([Quarter] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_Prognoses_FinanceSource]          CHECK       ([FinanceSource] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProgramme]
ON [Prognoses]([ProgrammeId], [Year], [Quarter], [FinanceSource])
WHERE [Level] = 1;

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProgrammePriority]
ON [Prognoses]([ProgrammePriorityId], [Year], [Quarter], [FinanceSource])
WHERE [Level] = 2;

CREATE UNIQUE INDEX [UQ_Prognoses_LevelProcedure]
ON [Prognoses]([ProcedureId], [Year], [Quarter], [FinanceSource])
WHERE [Level] = 3;