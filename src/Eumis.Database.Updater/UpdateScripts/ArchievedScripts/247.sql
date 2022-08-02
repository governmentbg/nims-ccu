GO

ALTER TABLE [Procedures] ADD
    [ProjectMinAmountInfo]          NVARCHAR(500)       NULL,
    [ProjectMinAmountInfoAlt]       NVARCHAR(500)       NULL,
    [ProjectMaxAmountInfo]          NVARCHAR(500)       NULL,
    [ProjectMaxAmountInfoAlt]       NVARCHAR(500)       NULL
GO

PRINT 'ProcedureIndicativeAnnualWorkingProgrammes'
GO

CREATE TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammes] (
    [ProcedureIndicativeAnnualWorkingProgrammeId]   INT             NOT NULL IDENTITY,
    [ProcedureId]                                   INT             NOT NULL,
    [Year]                                          INT             NOT NULL,
    [EligibleActivities]                            NVARCHAR(MAX)   NOT NULL,
    [EligibleActivitiesAlt]                         NVARCHAR(MAX)   NOT NULL,
    [EligibleCosts]                                 NVARCHAR(MAX)   NOT NULL,
    [EligibleCostsAlt]                              NVARCHAR(MAX)   NOT NULL,
    [MaxPercentCoFinancing]                         MONEY           NOT NULL,
    [MaxPercentCoFinancingInfo]                     NVARCHAR(MAX)   NOT NULL,
    [MaxPercentCoFinancingInfoAlt]                  NVARCHAR(MAX)   NOT NULL,
    [IsStateAssistance]                             INT             NOT NULL,
    [IsMinimalAssistance]                           INT             NOT NULL,
    [CreateDate]                                    DATETIME2       NOT NULL,
    [ModifyDate]                                    DATETIME2       NOT NULL,
    [Version]                                       ROWVERSION      NOT NULL,

    CONSTRAINT [PK_ProcedureIndicativeAnnualWorkingProgrammes]                          PRIMARY KEY ([ProcedureIndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammes_Procedures]               FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureIndicativeAnnualWorkingProgrammes_Year]                    CHECK ([Year] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)),
    CONSTRAINT [CHK_ProcedureIndicativeAnnualWorkingProgrammes_IsStateAssistance]       CHECK ([IsStateAssistance] IN (1, 2, 3)),
    CONSTRAINT [CHK_ProcedureIndicativeAnnualWorkingProgrammes_IsMinimalAssistance]     CHECK ([IsMinimalAssistance] IN (1, 2, 3)),
);
GO

PRINT 'ProcedureIndicativeAnnualWorkingProgrammeCandidates'
GO

CREATE TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammeCandidates] (
    [ProcedureIndicativeAnnualWorkingProgrammeCandidateId]  INT             NOT NULL IDENTITY,
    [ProcedureIndicativeAnnualWorkingProgrammeId]           INT             NOT NULL,
    [CompanyTypeId]                                         INT             NULL,
    [CompanyLegalTypeId]                                    INT             NULL,
    [Info]                                                  NVARCHAR(MAX)   NULL,
    [InfoAlt]                                               NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_ProcedureIndicativeAnnualWorkingProgrammeCandidates]                                              PRIMARY KEY ([ProcedureIndicativeAnnualWorkingProgrammeCandidateId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCandidates_ProcedureIndicativeAnnualWorkingProgrammes]   FOREIGN KEY ([ProcedureIndicativeAnnualWorkingProgrammeId]) REFERENCES [dbo].[ProcedureIndicativeAnnualWorkingProgrammes] ([ProcedureIndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCandidates_CompanyTypes]                                 FOREIGN KEY ([CompanyTypeId])                               REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCandidates_CompanyLegalTypes]                            FOREIGN KEY ([CompanyLegalTypeId])                          REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
);
GO

PRINT 'ProcedureIndicativeAnnualWorkingProgrammeCompanies'
GO

CREATE TABLE [dbo].[ProcedureIndicativeAnnualWorkingProgrammeCompanies] (
    [ProcedureIndicativeAnnualWorkingProgrammeCompanyId]    INT             NOT NULL IDENTITY,
    [ProcedureIndicativeAnnualWorkingProgrammeId]           INT             NOT NULL,
    [CompanyId]                                             INT             NOT NULL,

    CONSTRAINT [PK_ProcedureIndicativeAnnualWorkingProgrammeCompanies]                                              PRIMARY KEY ([ProcedureIndicativeAnnualWorkingProgrammeCompanyId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCompanies_ProcedureIndicativeAnnualWorkingProgrammes]   FOREIGN KEY ([ProcedureIndicativeAnnualWorkingProgrammeId]) REFERENCES [dbo].[ProcedureIndicativeAnnualWorkingProgrammes] ([ProcedureIndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_ProcedureIndicativeAnnualWorkingProgrammeCompanies_Companies]                                    FOREIGN KEY ([CompanyId])                                   REFERENCES [dbo].[Companies] ([CompanyId])
);
GO

PRINT 'IndicativeAnnualWorkingProgrammes'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammes] (
    [IndicativeAnnualWorkingProgrammeId]    INT             NOT NULL IDENTITY,
    [ProgrammeId]                           INT             NOT NULL,
    [Year]                                  INT             NOT NULL,
    [Type]                                  INT             NOT NULL,
    [Status]                                INT             NOT NULL,
    [StatusNote]                            NVARCHAR(MAX)   NULL,
    [OrderVersionNum]                       INT             NOT NULL,
    [PublicatedByUserId]                    INT             NULL,
    [PublicationDate]                       DATETIME2       NULL,
    [CreatedByUserId]                       INT             NOT NULL,
    [CreateDate]                            DATETIME2       NOT NULL,
    [ModifyDate]                            DATETIME2       NOT NULL,
    [Version]                               ROWVERSION      NOT NULL,

    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammes]                   PRIMARY KEY ([IndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammes_Programmes]        FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammes_PublicatedByUser]  FOREIGN KEY ([PublicatedByUserId])  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammes_CreatedByUser]     FOREIGN KEY ([CreatedByUserId])     REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammes_Year]             CHECK   ([Year] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammes_Type]             CHECK   ([Type] IN (1, 2)),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammes_Status]           CHECK   ([Status] IN (1, 2, 3, 4)),
);
GO

PRINT 'IndicativeAnnualWorkingProgrammeTables'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammeTables] (
    [IndicativeAnnualWorkingProgrammeTableId]           INT             NOT NULL IDENTITY,
    [IndicativeAnnualWorkingProgrammeId]                INT             NOT NULL,
    [ProcedureStatus]                                   INT             NOT NULL,
    [ProgrammePriorityId]                               INT             NOT NULL,
    [OrderNum]                                          INT             NOT NULL,
    [ProcedureName]                                     NVARCHAR(MAX)   NOT NULL,
    [ProcedureNameAlt]                                  NVARCHAR(MAX)   NOT NULL,
    [ProcedureDescription]                              NVARCHAR(MAX)   NOT NULL,
    [ProcedureDescriptionAlt]                           NVARCHAR(MAX)   NOT NULL,
    [IndicativeAnnualWorkingProgrammeTypeConducting]    INT             NOT NULL,
    [WithPreSelection]                                  BIT             NOT NULL,
    [ProcedureTotalAmount]                              MONEY           NOT NULL,

    [EligibleActivities]                                NVARCHAR(MAX)   NOT NULL,
    [EligibleActivitiesAlt]                             NVARCHAR(MAX)   NOT NULL,

    [EligibleCosts]                                     NVARCHAR(MAX)   NOT NULL,
    [EligibleCostsAlt]                                  NVARCHAR(MAX)   NOT NULL,

    [MaxPercentCoFinancing]                             MONEY           NOT NULL,
    [MaxPercentCoFinancingInfo]                         NVARCHAR(MAX)   NOT NULL,
    [MaxPercentCoFinancingInfoAlt]                      NVARCHAR(MAX)   NOT NULL,

    [ListingDate]                                       DATETIME2       NOT NULL,

    [IsStateAssistance]                                 INT             NOT NULL,
    [IsMinimalAssistance]                               INT             NOT NULL,

    [ProjectMinAmount]                                  MONEY           NOT NULL,
    [ProjectMinAmountInfo]                              NVARCHAR(MAX)   NOT NULL,
    [ProjectMinAmountInfoAlt]                           NVARCHAR(MAX)   NOT NULL,

    [ProjectMaxAmount]                                  MONEY           NOT NULL,
    [ProjectMaxAmountInfo]                              NVARCHAR(MAX)   NOT NULL,
    [ProjectMaxAmountInfoAlt]                           NVARCHAR(MAX)   NOT NULL,

    [CreateDate]                                        DATETIME2       NOT NULL,
    [ModifyDate]                                        DATETIME2       NOT NULL,
    [Version]                                           ROWVERSION      NOT NULL,

    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammeTables]                                                  PRIMARY KEY ([IndicativeAnnualWorkingProgrammeTableId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTables_IndicativeAnnualWorkingProgrammes]                FOREIGN KEY ([IndicativeAnnualWorkingProgrammeId])      REFERENCES [dbo].[IndicativeAnnualWorkingProgrammes] ([IndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTables_ProgrammePriorities]                              FOREIGN KEY ([ProgrammePriorityId])                     REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammeTables_ProcedureStatus]                                 CHECK ([ProcedureStatus] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammeTables_IndicativeAnnualWorkingProgrammeTypeConducting]  CHECK ([IndicativeAnnualWorkingProgrammeTypeConducting] IN (1, 2)),
);
GO

PRINT 'IndicativeAnnualWorkingProgrammeTableCandidates'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableCandidates] (
    [IndicativeAnnualWorkingProgrammeTableCandidateId]      INT             NOT NULL IDENTITY,
    [IndicativeAnnualWorkingProgrammeTableId]               INT             NOT NULL,
    [CompanyTypeId]                                         INT             NULL,
    [CompanyLegalTypeId]                                    INT             NULL,
    [Info]                                                  NVARCHAR(MAX)   NULL,
    [InfoAlt]                                               NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammeTableCandidates]                                             PRIMARY KEY ([IndicativeAnnualWorkingProgrammeTableCandidateId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableCandidates_IndicativeAnnualWorkingProgrammeTables]      FOREIGN KEY ([IndicativeAnnualWorkingProgrammeTableId]) REFERENCES [dbo].[IndicativeAnnualWorkingProgrammeTables] ([IndicativeAnnualWorkingProgrammeTableId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableCandidates_CompanyTypes]                                FOREIGN KEY ([CompanyTypeId])                           REFERENCES [dbo].[CompanyTypes] ([CompanyTypeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableCandidates_CompanyLegalTypes]                           FOREIGN KEY ([CompanyLegalTypeId])                      REFERENCES [dbo].[CompanyLegalTypes] ([CompanyLegalTypeId]),
);
GO

PRINT 'IndicativeAnnualWorkingProgrammeTableProgrammes'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableProgrammes] (
    [IndicativeAnnualWorkingProgrammeTableProgrammeId]  INT             NOT NULL IDENTITY,
    [IndicativeAnnualWorkingProgrammeTableId]           INT             NOT NULL,
    [ProgrammeId]                                       INT             NOT NULL,

    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammeTableProgrammes]                                         PRIMARY KEY ([IndicativeAnnualWorkingProgrammeTableProgrammeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableProgrammes_IndicativeAnnualWorkingProgrammeTables]  FOREIGN KEY ([IndicativeAnnualWorkingProgrammeTableId]) REFERENCES [dbo].[IndicativeAnnualWorkingProgrammeTables] ([IndicativeAnnualWorkingProgrammeTableId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableProgrammes_Programmes]                              FOREIGN KEY ([ProgrammeId])                             REFERENCES [dbo].[MapNodes] ([MapNodeId]),
);
GO

PRINT 'IndicativeAnnualWorkingProgrammeTableTimeLimits'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammeTableTimeLimits] (
    [IndicativeAnnualWorkingProgrammeTableTimeLimitId]  INT         NOT NULL IDENTITY,
    [IndicativeAnnualWorkingProgrammeTableId]           INT         NOT NULL,
    [EndDate]                                           DATETIME2   NOT NULL,
    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammeTableTimeLimits] PRIMARY KEY ([IndicativeAnnualWorkingProgrammeTableTimeLimitId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammeTableTimeLimits_IndicativeAnnualWorkingProgrammeTables]  FOREIGN KEY ([IndicativeAnnualWorkingProgrammeTableId])  REFERENCES [dbo].[IndicativeAnnualWorkingProgrammeTables] ([IndicativeAnnualWorkingProgrammeTableId]),
);
GO
