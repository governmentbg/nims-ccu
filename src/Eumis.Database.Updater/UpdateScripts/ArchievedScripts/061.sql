GO

-- Projects
ALTER TABLE Projects
ADD [CompanyName]                           NVARCHAR(200)       NOT NULL CONSTRAINT DEFAULT_CompanyName DEFAULT 'empty',
    [CompanyUin]                            NVARCHAR(200)       NOT NULL CONSTRAINT DEFAULT_CompanyUin DEFAULT 'empty',
    [CompanyUinType]                        INT                 NOT NULL CONSTRAINT DEFAULT_CompanyUinType DEFAULT 0,
    [CompanyKidCodeId]                      INT                 NULL,
    [CompanySizeTypeId]                     INT                 NOT NULL CONSTRAINT DEFAULT_CompanySizeTypeId DEFAULT 1,
    [KidCodeId]                             INT                 NULL
GO

ALTER TABLE Projects
DROP
    CONSTRAINT [CHK_Projects_RegistrationStatus]
ALTER TABLE Projects
ADD
    CONSTRAINT [CHK_Projects_RegistrationStatus]         CHECK       ([RegistrationStatus] IN (1, 2, 3)),
    CONSTRAINT [CHK_Projects_CompanyUinType]             CHECK       ([CompanyUinType] IN (0, 1, 2, 3)),
    CONSTRAINT [FK_Projects_CompanyKidCodes]             FOREIGN KEY ([CompanyKidCodeId])    REFERENCES [dbo].[KidCodes] ([KidCodeId]),
    CONSTRAINT [FK_Projects_CompanySizeType]             FOREIGN KEY ([CompanySizeTypeId])   REFERENCES [dbo].[CompanySizeTypes] ([CompanySizeTypeId]),
    CONSTRAINT [FK_Projects_ProjectKidCodes]             FOREIGN KEY ([KidCodeId])           REFERENCES [dbo].[KidCodes] ([KidCodeId]);
GO

UPDATE p
SET
    p.CompanyName = c.Name,
    p.CompanyUin = c.Uin,
    p.CompanyUinType = c.UinType,
    p.CompanyKidCodeId = c.KidCodeId,
    p.CompanySizeTypeId = c.CompanySizeTypeId
FROM Projects p
INNER JOIN Companies c ON p.CompanyId = c.CompanyId;
GO

ALTER TABLE Projects
DROP
  CONSTRAINT DEFAULT_CompanyName,
  CONSTRAINT DEFAULT_CompanyUin,
  CONSTRAINT DEFAULT_CompanyUinType,
  CONSTRAINT DEFAULT_CompanySizeTypeId
GO

-- ProcedureEvalTables
ALTER TABLE ProcedureEvalTables
DROP
    CONSTRAINT [CHK_ProcedureEvalTables_Type]
ALTER TABLE ProcedureEvalTables
ADD
    CONSTRAINT [CHK_ProcedureEvalTables_Type]            CHECK       ([Type] IN (1, 2, 3))

-- EvalSessions
ALTER TABLE EvalSessions
DROP
    CONSTRAINT [FK_EvalSessions_EvalSessionStatuses],
    CONSTRAINT [FK_EvalSessions_EvalSessionTypes],
    COLUMN [EvalSessionStatusId],
    COLUMN [EvalSessionTypeId]
ALTER TABLE EvalSessions
ADD
    [EvalSessionStatus] INT NOT NULL,
    [EvalSessionType]   INT NOT NULL,
    CONSTRAINT [CHK_EvalSessions_Status]                CHECK       ([EvalSessionStatus] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_EvalSessions_Type]                  CHECK       ([EvalSessionType] IN (1, 2, 3))
ALTER TABLE EvalSessions
ALTER COLUMN [SessionNum] NVARCHAR(50) NOT NULL
ALTER TABLE EvalSessions
ALTER COLUMN [SessionDate] DATETIME2 NOT NULL

-- EvalSessionProjects
ALTER TABLE EvalSessionProjects
DROP
    CONSTRAINT [CHK_EvalSessionProjects_Type],
    COLUMN [Type]
ALTER TABLE EvalSessionProjects
ADD
    [IsDeleted]         BIT             NOT NULL,
    [IsDeletedNote]     NVARCHAR(200)   NULL

-- EvalSessionUsers
ALTER TABLE EvalSessionUsers
ALTER COLUMN [Position] NVARCHAR(50) NULL

-- delete EvalSessionDocs and create EvalSessionReports
DROP TABLE [dbo].[EvalSessionDocs];
GO

CREATE TABLE [dbo].[EvalSessionReports] (
    [EvalSessionReportId]   INT             NOT NULL IDENTITY,
    [EvalSessionId]         INT             NOT NULL,
    [RegNumber]             NVARCHAR(200)   NOT NULL,
    [Type]                  INT             NOT NULL,
    [Description]           NVARCHAR(MAX)   NOT NULL,
    [IsDeleted]             BIT             NOT NULL,
    [IsDeletedNote]         NVARCHAR(200)   NULL,
    [CreateDate]            DATETIME2       NOT NULL,
    [ModifyDate]            DATETIME2       NOT NULL,
    [Version]               ROWVERSION      NOT NULL,

    CONSTRAINT [PK_EvalSessionReports]                  PRIMARY KEY ([EvalSessionReportId]),
    CONSTRAINT [FK_EvalSessionReports_EvalSessions]     FOREIGN KEY ([EvalSessionId])       REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [CHK_EvalSessionReports_Type]            CHECK       ([Type] IN (1, 2, 3))
);
GO

CREATE TABLE [dbo].[EvalSessionReportProjects] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionReportId]           INT                NOT NULL,
    [ProjectId]                     INT                NOT NULL,

    [RegNumber]                     NVARCHAR(200)      NOT NULL,
    [RegDate]                       DATETIME2          NOT NULL,
    [RecieveDate]                   DATETIME2          NOT NULL,
    [RecieveType]                   INT                NOT NULL,
    [Name]                          NVARCHAR(MAX)      NOT NULL,
    [Duration]                      INT                NULL,
    [ProjectKidCodeId]              INT                NULL,
    [GrandAmount]                   MONEY              NULL,
    [CoFinancingAmount]             MONEY              NULL,

    [StandingStatus]                INT                NOT NULL,
    [StandingNum]                   INT                NULL,
    [StandingGrandAmount]           MONEY              NULL,

    [CompanyUin]                    NVARCHAR(200)      NOT NULL,
    [CompanyName]                   NVARCHAR(200)      NOT NULL,
    [CompanySizeTypeId]             INT                NOT NULL,
    [CompanyKidCodeId]              INT                NULL,
    [RegEmail]                      NVARCHAR (200)     NULL,

    [HasAdminAdmiss]                BIT                NOT NULL,
    [AdminAdmissResult]             INT                NULL,
    [AdminAdmissPoints]             DECIMAL(15,3)      NULL,

    [HasTechFinance]                BIT                NOT NULL,
    [TechFinanceResult]             INT                NULL,
    [TechFinancePoints]             DECIMAL(15,3)      NULL,

    [HasComplex]                    BIT                NOT NULL,
    [ComplexResult]                 INT                NULL,
    [ComplexPoints]                 DECIMAL(15,3)      NULL,

    CONSTRAINT [PK_EvalSessionReportProjects]                              PRIMARY KEY ([EvalSessionId], [EvalSessionReportId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionReportProjects_EvalSessions]                 FOREIGN KEY ([EvalSessionId])                          REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionReportProjects_EvalSessionReports]           FOREIGN KEY ([EvalSessionReportId])                    REFERENCES [dbo].[EvalSessionReports]  ([EvalSessionReportId]),
    CONSTRAINT [FK_EvalSessionReportProjects_Projects]                     FOREIGN KEY ([ProjectId])                              REFERENCES [dbo].[Projects]            ([ProjectId]),
    CONSTRAINT [FK_EvalSessionReportProjects_ProjectKidCodes]              FOREIGN KEY ([ProjectKidCodeId])                       REFERENCES [dbo].[KidCodes]            ([KidCodeId]),
    CONSTRAINT [FK_EvalSessionReportProjects_CompanySizeTypes]             FOREIGN KEY ([CompanySizeTypeId])                      REFERENCES [dbo].[CompanySizeTypes]    ([CompanySizeTypeId]),
    CONSTRAINT [FK_EvalSessionReportProjects_CompanyKidCodes]              FOREIGN KEY ([CompanyKidCodeId])                       REFERENCES [dbo].[KidCodes]            ([KidCodeId]),
    CONSTRAINT [CHK_EvalSessionReportProjects_RecieveType]                 CHECK       ([RecieveType]       IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_EvalSessionReportProjects_StandingStatus]              CHECK       ([StandingStatus]    IN (1, 2, 3, 4, 5, 6, 7, 8)),
    CONSTRAINT [CHK_EvalSessionReportProjects_AdminAdmissResult]           CHECK       ([AdminAdmissResult] IN (1, 2)),
    CONSTRAINT [CHK_EvalSessionReportProjects_TechFinanceResult]           CHECK       ([TechFinanceResult] IN (1, 2)),
    CONSTRAINT [CHK_EvalSessionReportProjects_ComplexResult]               CHECK       ([ComplexResult]     IN (1, 2))
);
GO

CREATE TABLE [dbo].[EvalSessionDistributions] (
    [EvalSessionId]                 INT             NOT NULL,
    [EvalSessionDistributionId]     INT             NOT NULL IDENTITY,
    [EvalTableType]                 INT             NOT NULL,
    [Status]                        INT             NOT NULL,
    [Code]                          NVARCHAR(50)    NOT NULL,
    [CreateDate]                    DATETIME2       NOT NULL,
    [StatusNote]                    NVARCHAR(200)   NULL,
    [AssessorsPerProject]           INT             NOT NULL,

    CONSTRAINT [PK_EvalSessionDistributions]                  PRIMARY KEY ([EvalSessionId], [EvalSessionDistributionId]),
    CONSTRAINT [CHK_EvalSessionDistributions_EvalTableType]   CHECK       ([EvalTableType] IN (1, 2, 3)),
    CONSTRAINT [CHK_EvalSessionDistributions_Status]          CHECK       ([Status] IN (1, 2, 3)),
    CONSTRAINT [FK_EvalSessionDistributions_EvalSessions]     FOREIGN KEY ([EvalSessionId])       REFERENCES [dbo].[EvalSessions] ([EvalSessionId])
);

CREATE TABLE [dbo].[EvalSessionDistributionProjects] (
    [EvalSessionId]                 INT             NOT NULL,
    [EvalSessionDistributionId]     INT             NOT NULL,
    [ProjectId]                     INT             NOT NULL,
    [IsDeleted]                     BIT             NOT NULL,
    [IsDeletedNote]                 NVARCHAR(200)   NULL,

    CONSTRAINT [PK_EvalSessionDistributionProjects]                              PRIMARY KEY ([EvalSessionId], [EvalSessionDistributionId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionDistributionProjects_EvalSessionDistributions]     FOREIGN KEY ([EvalSessionId], [EvalSessionDistributionId])   REFERENCES [dbo].[EvalSessionDistributions] ([EvalSessionId], [EvalSessionDistributionId]),
    CONSTRAINT [FK_EvalSessionDistributionProjects_Projects]                     FOREIGN KEY ([EvalSessionId], [ProjectId])                   REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId])
);

CREATE TABLE [dbo].[EvalSessionDistributionUsers] (
    [EvalSessionId]                 INT             NOT NULL,
    [EvalSessionDistributionId]     INT             NOT NULL,
    [UserId]                        INT             NOT NULL,
    [IsDeleted]                     BIT             NOT NULL,
    [IsDeletedNote]                 NVARCHAR(200)   NULL,

    CONSTRAINT [PK_EvalSessionDistributionUsers]                              PRIMARY KEY ([EvalSessionId], [EvalSessionDistributionId], [UserId]),
    CONSTRAINT [FK_EvalSessionDistributionUsers_EvalSessionDistributions]     FOREIGN KEY ([EvalSessionId], [EvalSessionDistributionId]) REFERENCES [dbo].[EvalSessionDistributions] ([EvalSessionId], [EvalSessionDistributionId]),
    CONSTRAINT [FK_EvalSessionDistributionUsers_Users]                        FOREIGN KEY ([EvalSessionId], [UserId])                    REFERENCES [dbo].[EvalSessionUsers] ([EvalSessionId], [UserId])
);

CREATE TABLE [dbo].[EvalSessionEvaluations] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionEvaluationId]       INT                NOT NULL IDENTITY,
    [ProjectId]                     INT                NOT NULL,
    [CalculationType]               INT                NOT NULL,
    [EvalType]                      INT                NOT NULL,
    [EvalTableType]                 INT                NOT NULL,
    [EvalIsPassed]                  BIT                NOT NULL,
    [EvalPoints]                    DECIMAL(15,3)      NULL,
    [EvalNote]                      NVARCHAR(200)      NULL,
    [IsDeleted]                     BIT                NOT NULL,
    [IsDeletedNote]                 NVARCHAR(200)      NULL,
    [CreateDate]                    DATETIME2          NOT NULL,

    CONSTRAINT [PK_EvalSessionEvaluations]                      PRIMARY KEY ([EvalSessionId], [EvalSessionEvaluationId]),
    CONSTRAINT [CHK_EvalSessionEvaluations_EvalTableType]       CHECK       ([EvalTableType] IN (1, 2, 3)),
    CONSTRAINT [CHK_EvalSessionEvaluations_CalculationType]     CHECK       ([CalculationType] IN (1, 2)),
    CONSTRAINT [FK_EvalSessionEvaluations_EvalSessions]         FOREIGN KEY ([EvalSessionId])                    REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionEvaluations_EvalSessionProjects]  FOREIGN KEY ([EvalSessionId], [ProjectId])       REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId])
);

CREATE UNIQUE INDEX [UQ_EvalSessionEvaluations_Project_IsDeleted]
    ON [dbo].[EvalSessionEvaluations]([EvalSessionId], [ProjectId], [EvalTableType], [IsDeleted]) WHERE [IsDeleted] = 0
GO

CREATE TABLE [dbo].[EvalSessionSheets] (
    [EvalSessionId]                 INT             NOT NULL,
    [EvalSessionSheetId]            INT             NOT NULL IDENTITY,
    [UserId]                        INT             NOT NULL,
    [ProjectId]                     INT             NOT NULL,
    [EvalTableType]                 INT             NOT NULL,
    [Status]                        INT             NOT NULL,
    [StatusNote]                    NVARCHAR(200)   NULL,
    [StatusDate]                    DATETIME2       NOT NULL,
    [EvalSessionDistributionId]     INT             NULL,
    [DistributionType]              INT             NOT NULL,
    [ContinuedEvalSessionSheetId]   INT             NULL,

    CONSTRAINT [PK_EvalSessionSheets]                            PRIMARY KEY ([EvalSessionId], [EvalSessionSheetId]),
    CONSTRAINT [CHK_EvalSessionSheets_EvalTableType]             CHECK       ([EvalTableType] IN (1, 2, 3)),
    CONSTRAINT [CHK_EvalSessionSheets_Status]                    CHECK       ([Status] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_EvalSessionSheets_DistributionType]          CHECK       ([DistributionType] IN (1, 2, 3)),
    CONSTRAINT [FK_EvalSessionSheets_EvalSessions]               FOREIGN KEY ([EvalSessionId])                                REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionSheets_Users]                      FOREIGN KEY ([EvalSessionId], [UserId])                      REFERENCES [dbo].[EvalSessionUsers] ([EvalSessionId], [UserId]),
    CONSTRAINT [FK_EvalSessionSheets_Projects]                   FOREIGN KEY ([EvalSessionId], [ProjectId])                   REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionSheets_EvalSessionDistributions]   FOREIGN KEY ([EvalSessionId], [EvalSessionDistributionId])   REFERENCES [dbo].[EvalSessionDistributions] ([EvalSessionId], [EvalSessionDistributionId]),
    CONSTRAINT [FK_EvalSessionSheets_ContinuedEvalSessionSheets] FOREIGN KEY ([EvalSessionId], [ContinuedEvalSessionSheetId]) REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId])
);

CREATE TABLE [dbo].[EvalSessionSheetXmls] (
    [EvalSessionSheetXmlId]         INT                 NOT NULL IDENTITY,
    [Gid]                           UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [EvalSessionId]                 INT                 NOT NULL,
    [EvalSessionSheetId]            INT                 NOT NULL,
    [Xml]                           XML                 NOT NULL,
    [Hash]                          NVARCHAR(10)        NOT NULL UNIQUE,
    [EvalType]                      INT                 NOT NULL,
    [EvalTableType]                 INT                 NOT NULL,
    [EvalIsPassed]                  BIT                 NULL,
    [EvalPoints]                    DECIMAL(15,3)       NULL,
    [EvalNote]                      NVARCHAR(200)       NULL,
    [CreateDate]                    DATETIME2           NOT NULL,
    [ModifyDate]                    DATETIME2           NOT NULL,
    [Version]                       ROWVERSION          NOT NULL,

    CONSTRAINT [PK_EvalSessionSheetData]                        PRIMARY KEY ([EvalSessionSheetXmlId]),
    CONSTRAINT [FK_EvalSessionSheetData_EvalSessions]           FOREIGN KEY ([EvalSessionId])                         REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionSheetData_EvalSessionSheets]      FOREIGN KEY ([EvalSessionId], [EvalSessionSheetId])   REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId]),
);

CREATE TABLE [dbo].[EvalSessionStandings] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionStandingId]         INT                NOT NULL IDENTITY,
    [Code]                          NVARCHAR(50)       NOT NULL,
    [Status]                        INT                NOT NULL,
    [StatusNote]                    NVARCHAR(200)      NULL,
    [StatusDate]                    DATETIME2          NOT NULL,

    CONSTRAINT [PK_EvalSessionStandings]                      PRIMARY KEY ([EvalSessionId], [EvalSessionStandingId]),
    CONSTRAINT [CHK_EvalSessionStandings_Status]              CHECK       ([Status] IN (1, 2)),
    CONSTRAINT [FK_EvalSessionStandings_EvalSessions]         FOREIGN KEY ([EvalSessionId])                    REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
);
GO

CREATE TABLE [dbo].[EvalSessionStandingProjects] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionStandingId]         INT                NOT NULL,
    [ProjectId]                     INT                NOT NULL,

    CONSTRAINT [PK_EvalSessionStandingProjects]                           PRIMARY KEY ([EvalSessionId], [EvalSessionStandingId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionStandingProjects_EvalSessions]              FOREIGN KEY ([EvalSessionId])                             REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionStandingProjects_EvalSessionStandings]      FOREIGN KEY ([EvalSessionId], [EvalSessionStandingId])    REFERENCES [dbo].[EvalSessionStandings] ([EvalSessionId], [EvalSessionStandingId]),
    CONSTRAINT [FK_EvalSessionStandingProjects_EvalSessionProjects]       FOREIGN KEY ([EvalSessionId], [ProjectId])                REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId])
);
GO

CREATE TABLE [dbo].[EvalSessionProjectStandings] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionProjectStandingId]  INT                NOT NULL IDENTITY,
    [ProjectId]                     INT                NOT NULL,
    [OrderNum]                      INT                NULL,
    [Status]                        INT                NOT NULL,
    [GrandAmount]                   MONEY              NULL,
    [IsDeleted]                     BIT                NOT NULL,
    [IsDeletedNote]                 NVARCHAR(200)      NULL,
    [Notes]                         NVARCHAR(200)      NULL,
    [EvalSessionStandingId]         INT                NULL,
    [CreateDate]                    DATETIME2          NOT NULL,

    CONSTRAINT [PK_EvalSessionProjectStanding]                        PRIMARY KEY ([EvalSessionId], [EvalSessionProjectStandingId]),
    CONSTRAINT [CHK_EvalSessionProjectStanding_Status]                CHECK       ([Status] IN (1, 2, 3, 4, 5)),
    CONSTRAINT [FK_EvalSessionProjectStanding_EvalSessions]           FOREIGN KEY ([EvalSessionId])                               REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionProjectStandings_EvalSessionProjects]   FOREIGN KEY ([EvalSessionId], [ProjectId])                  REFERENCES [dbo].[EvalSessionProjects] ([EvalSessionId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionProjectStandings_EvalSessionStandings]  FOREIGN KEY ([EvalSessionId], [EvalSessionStandingId])      REFERENCES [dbo].[EvalSessionStandings] ([EvalSessionId], [EvalSessionStandingId])
);
GO

CREATE TABLE [dbo].[EvalSessionProjectStandingEvaluations] (
    [EvalSessionId]                    INT                NOT NULL,
    [EvalSessionProjectStandingId]     INT                NOT NULL,
    [EvalSessionEvaluationId]          INT                NOT NULL,

    CONSTRAINT [PK_EvalSessionProjectStandingEvaluations]                                PRIMARY KEY ([EvalSessionId], [EvalSessionProjectStandingId], [EvalSessionEvaluationId]),
    CONSTRAINT [FK_EvalSessionProjectStandingEvaluations_EvalSessions]                   FOREIGN KEY ([EvalSessionId])                                  REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionProjectStandingEvaluations_EvalSessionProjectStandings]    FOREIGN KEY ([EvalSessionId], [EvalSessionProjectStandingId])  REFERENCES [dbo].[EvalSessionProjectStandings] ([EvalSessionId], [EvalSessionProjectStandingId]),
    CONSTRAINT [FK_EvalSessionProjectStandingEvaluations_EvalSessionEvaluations]         FOREIGN KEY ([EvalSessionId], [EvalSessionEvaluationId])       REFERENCES [dbo].[EvalSessionEvaluations] ([EvalSessionId], [EvalSessionEvaluationId])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_EvalSessionProjectStandings_EvalSessionId_OrderNum]
ON [EvalSessionProjectStandings]([EvalSessionId], [OrderNum])
WHERE [OrderNum] IS NOT NULL AND [IsDeleted] = 0;

GO
IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwUniqueEvalSessionProjectIndexed'))
DROP VIEW vwUniqueEvalSessionProjectIndexed
GO

CREATE VIEW vwUniqueEvalSessionProjectIndexed WITH SCHEMABINDING
AS

SELECT esp.ProjectId
FROM [dbo].[EvalSessionProjects] esp
JOIN [dbo].[EvalSessions] es ON esp.EvalSessionId = es.EvalSessionId
WHERE es.EvalSessionStatus IN (1, 2, 3) AND esp.IsDeleted = 0

GO

GRANT SELECT ON vwUniqueEvalSessionProjectIndexed TO PUBLIC
GO

CREATE UNIQUE CLUSTERED INDEX [vwUniqueEvalSessionProjectIndexed_PK]
 ON [dbo].[vwUniqueEvalSessionProjectIndexed] 
(
 ProjectId ASC
)

GO

CREATE TABLE [dbo].[EvalSessionEvaluationSheets] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionEvaluationId]       INT                NOT NULL,
    [EvalSessionSheetId]            INT                NOT NULL,

    CONSTRAINT [PK_EvalSessionEvaluationSheets]                           PRIMARY KEY ([EvalSessionId], [EvalSessionEvaluationId], [EvalSessionSheetId]),
    CONSTRAINT [FK_EvalSessionEvaluationSheets_EvalSessions]              FOREIGN KEY ([EvalSessionId])                               REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionEvaluationSheets_EvalSessionEvaluations]    FOREIGN KEY ([EvalSessionId], [EvalSessionEvaluationId])    REFERENCES [dbo].[EvalSessionEvaluations] ([EvalSessionId], [EvalSessionEvaluationId]),
    CONSTRAINT [FK_EvalSessionEvaluationSheets_EvalSessionSheets]         FOREIGN KEY ([EvalSessionId], [EvalSessionSheetId])         REFERENCES [dbo].[EvalSessionSheets] ([EvalSessionId], [EvalSessionSheetId])
);

CREATE TABLE [dbo].[EvalSessionDocuments] (
    [EvalSessionId]                           INT                 NOT NULL,
    [EvalSessionDocumentId]                   INT                 NOT NULL IDENTITY,
    [Name]                                    NVARCHAR(200)       NOT NULL,
    [Description]                             NVARCHAR(MAX)       NULL,
    [BlobKey]                                 UNIQUEIDENTIFIER    NOT NULL

    CONSTRAINT [PK_EvalSessionDocuments]              PRIMARY KEY       ([EvalSessionDocumentId]),
    CONSTRAINT [FK_EvalSessionDocuments_EvalSessions] FOREIGN KEY       ([EvalSessionId])       REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionDocuments_Blobs]        FOREIGN KEY       ([BlobKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

-- rename ProjectXmls table to ProjectVersionXmls
EXEC sp_rename 'dbo.ProjectXmls', 'ProjectVersionXmls';
GO
EXEC sp_rename 'dbo.ProjectVersionXmls.ProjectXmlId', 'ProjectVersionXmlId', 'COLUMN';
GO

-- create ProjectCommunications table
CREATE TABLE [dbo].[ProjectCommunications] (
    [ProjectCommunicationId]         INT                NOT NULL IDENTITY,
    [Gid]                            UNIQUEIDENTIFIER   NOT NULL,
    [ProjectId]                      INT                NOT NULL,
    [EvalSessionId]                  INT                NOT NULL,
    [Status]                         INT                NOT NULL,
    [StatusNote]                     NVARCHAR(MAX)      NULL,
    [RegNumber]                      NVARCHAR(200)      NULL,
    [QuestionEndingDate]             DATETIME2          NULL,
    [QuestionReadDate]               DATETIME2          NULL,
    [OrderNum]                       INT                NOT NULL,

    [QuestionProjectVersionXmlId]    INT                NOT NULL,
    [QuestionDate]                   DATETIME2          NULL,
    [QuestionContent]                NVARCHAR(MAX)      NULL,
    [QuestionXml]                    XML                NOT NULL,
    [QuestionHash]                   NVARCHAR(10)       NOT NULL UNIQUE,

    [AnswerProjectVersionXmlId]      INT                NULL,
    [AnswerDate]                     DATETIME2          NULL,
    [AnswerContent]                  NVARCHAR(MAX)      NULL,
    [AnswerXml]                      XML                NULL,
    [AnswerHash]                     NVARCHAR(10)       NULL,

    [CreateDate]                     DATETIME2          NOT NULL,
    [ModifyDate]                     DATETIME2          NOT NULL,
    [Version]                        ROWVERSION         NOT NULL

    CONSTRAINT [PK_ProjectCommunications]                              PRIMARY KEY ([ProjectCommunicationId]),
    CONSTRAINT [FK_ProjectCommunications_Projects]                     FOREIGN KEY ([ProjectId])                   REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectCommunications_EvalSessions]                 FOREIGN KEY ([EvalSessionId])               REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_ProjectCommunications_QuestionProjectVersionXmls]   FOREIGN KEY ([QuestionProjectVersionXmlId]) REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [FK_ProjectCommunications_AnswerProjectVersionXmls]     FOREIGN KEY ([AnswerProjectVersionXmlId])   REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [CHK_ProjectCommunications_Status]                      CHECK ([Status] IN (1, 2, 3, 4, 5, 6, 7, 8, 9))
);
GO

CREATE UNIQUE INDEX [UQ_ProjectCommunications_AnswerHash]
    ON [dbo].[ProjectCommunications]([AnswerHash]) WHERE [AnswerHash] IS NOT NULL
GO

ALTER TABLE [Companies]
    ALTER COLUMN [NameAlt] NVARCHAR(200) NULL;
GO
