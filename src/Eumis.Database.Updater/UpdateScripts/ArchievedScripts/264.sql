PRINT 'EvalSessionResults'
GO

CREATE TABLE [dbo].[EvalSessionResults] (
    [EvalSessionResultId]                           INT             NOT NULL IDENTITY,
    [EvalSessionId]                                 INT             NOT NULL,
    [Status]                                        INT             NOT NULL,
    [StatusNote]                                    NVARCHAR(MAX)   NULL,
    [Type]                                          INT             NOT NULL,
    [OrderNum]                                      INT             NOT NULL,
    [ProcedureId]                                   INT             NOT NULL,

    [PublicationDate]                               DATETIME2       NULL,
    [PublicationUserId]                             INT             NULL,

    [CreateDate]                                    DATETIME2       NOT NULL,
    [ModifyDate]                                    DATETIME2       NOT NULL,

    CONSTRAINT [PK_EvalSessionResults]                               PRIMARY KEY ([EvalSessionResultId]),
    CONSTRAINT [FK_EvalSessionResults_EvalSessions]                  FOREIGN KEY ([EvalSessionId])           REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionResults_Users]                         FOREIGN KEY ([PublicationUserId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_EvalSessionResults_Procedures]                    FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_EvalSessionResults_Status]                       CHECK       ([Status] IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_EvalSessionResults_Type]                         CHECK       ([Type]   IN (1, 2, 3))
);
GO

PRINT 'EvalSessionResultProjects'
GO

CREATE TABLE [dbo].[EvalSessionResultProjects] (
    [EvalSessionResultProjectId]                    INT                 NOT NULL IDENTITY,
    [EvalSessionResultId]                           INT                 NOT NULL,
    [ProjectId]                                     INT                 NOT NULL,
    [ProjectRegNumber]                              NVARCHAR(50)        NOT NULL,
    [ProjectName]                                   NVARCHAR(MAX)       NOT NULL,
    [ProjectNameAlt]                                NVARCHAR(MAX)       NULL,
    [ProjectRegDate]                                DATETIME2           NOT NULL,
    [CompanyName]                                   NVARCHAR(200)       NOT NULL,
    [CompanyNameAlt]                                NVARCHAR(200)       NULL,
    [CompanyUin]                                    NVARCHAR(200)       NOT NULL,
    [CompanyUinType]                                INT                 NOT NULL,
    [GrantAmount]                                   MONEY               NULL,
    [SelfAmount]                                    MONEY               NULL,
    [StandingPreliminaryResult]                     BIT                 NULL,
    [StandingPreliminaryPoints]                     DECIMAL(15,3)       NULL,
    [EvaluationAdminAdmissResult]                   BIT                 NULL,
    [StandingTechFinanceResult]                     BIT                 NULL,
    [StandingTechFinancePoints]                     DECIMAL(15,3)       NULL,
    [StandingComplexResult]                         BIT                 NULL,
    [StandingComplexPoints]                         DECIMAL(15,3)       NULL,
    [NonAdmissionReason]                            NVARCHAR(MAX)       NULL,
    [Note]                                          NVARCHAR(MAX)       NULL,
    [ProjectStandingNumber]                         INT                 NULL,
    [ProjectStandingStatus]                         INT                 NULL,
    [GrantAmountCorrected]                          MONEY               NULL,
    [SelfAmountCorrected]                           MONEY               NULL,
    

    CONSTRAINT [PK_EvalSessionResultProjects]                                  PRIMARY KEY ([EvalSessionResultProjectId]),
    CONSTRAINT [FK_EvalSessionResultProjects_EvalSessionResults]               FOREIGN KEY ([EvalSessionResultId])                 REFERENCES [dbo].[EvalSessionResults] ([EvalSessionResultId]),
    CONSTRAINT [FK_EvalSessionResultProjects_Projects]                         FOREIGN KEY ([ProjectId])                           REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [CHK_EvalSessionResultProjects_CompanyUinType]                  CHECK       ([CompanyUinType]   IN (0, 1, 2, 3))
);
GO

GO
SET IDENTITY_INSERT [dbo].[EvalSessionResults] ON
INSERT INTO [EvalSessionResults] (
    [EvalSessionResultId],
    [EvalSessionId],
    [Status],
    [StatusNote],
    [Type],
    [OrderNum],
    [ProcedureId],
    [PublicationDate],
    [PublicationUserId],
    [CreateDate],
    [ModifyDate])
SELECT
    [EvalSessionAdminAdmissResultId],
    [EvalSessionId],
    [Status], 
    [StatusNote],
    2,
    [OrderNum],
    [ProcedureId],
    [PublicationDate],
    [PublicationUserId],
    [CreateDate],
    [ModifyDate]
FROM [EvalSessionAdminAdmissResults]
SET IDENTITY_INSERT [dbo].[EvalSessionResults] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[EvalSessionResultProjects] ON
INSERT INTO [EvalSessionResultProjects] (
    [EvalSessionResultProjectId],
    [EvalSessionResultId],
    [ProjectId],
    [ProjectRegNumber],
    [ProjectName],
    [ProjectNameAlt],
    [CompanyName],
    [CompanyNameAlt],
    [EvaluationAdminAdmissResult],
    [NonAdmissionReason],
    [CompanyUin],
    [CompanyUinType],
    [ProjectRegDate],
    [GrantAmount],
    [SelfAmount])
SELECT
    [EvalSessionAdminAdmissProjects].[EvalSessionAdminAdmissProjectId],
    [EvalSessionAdminAdmissProjects].[EvalSessionAdminAdmissResultId],
    [EvalSessionAdminAdmissProjects].[ProjectId],
    [EvalSessionAdminAdmissProjects].[ProjectRegNumber],
    [EvalSessionAdminAdmissProjects].[ProjectName],
    [EvalSessionAdminAdmissProjects].[ProjectNameAlt],
    [EvalSessionAdminAdmissProjects].[CompanyName],
    [EvalSessionAdminAdmissProjects].[CompanyNameAlt],
    CASE 
        WHEN [EvalSessionAdminAdmissProjects].[AdminAdmissResult] = 1 THEN 1 
        ELSE 0 
    END,
    [EvalSessionAdminAdmissProjects].[NonAdmissionReason],
    [Projects].[CompanyUin],
    [Projects].[CompanyUinType],
    [Projects].[RegDate],
    [Projects].[TotalBfpAmount],
    [Projects].[CoFinancingAmount]
FROM [EvalSessionAdminAdmissProjects]
LEFT JOIN [Projects] ON [EvalSessionAdminAdmissProjects].[ProjectId] = [Projects].[ProjectId]
SET IDENTITY_INSERT [dbo].[EvalSessionResultProjects] OFF
GO

-- UNCOMMENT BEFORE MERGE
-- DROP TABLE EvalSessionResultProjects
-- DROP TABLE EvalSessionResults