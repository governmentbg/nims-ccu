PRINT 'EvalSessionAdminAdmissResults'
GO

CREATE TABLE [dbo].[EvalSessionAdminAdmissResults] (
    [EvalSessionAdminAdmissResultId]                INT             NOT NULL IDENTITY,
    [EvalSessionId]                                 INT             NOT NULL,
    [Status]                                        INT             NOT NULL,
    [StatusNote]                                    NVARCHAR(MAX)   NULL,
    [OrderNum]                                      INT             NOT NULL,
    [ProcedureId]                                   INT             NOT NULL,

    [PublicationDate]                               DATETIME2       NULL,
    [PublicationUserId]                             INT             NULL,

    [CreateDate]                                    DATETIME2       NOT NULL,
    [ModifyDate]                                    DATETIME2       NOT NULL,

    CONSTRAINT [PK_EvalSessionAdminAdmissResults]                               PRIMARY KEY ([EvalSessionAdminAdmissResultId]),
    CONSTRAINT [FK_EvalSessionAdminAdmissResults_EvalSessions]                  FOREIGN KEY ([EvalSessionId])           REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionAdminAdmissResults_Users]                         FOREIGN KEY ([PublicationUserId])       REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_EvalSessionAdminAdmissResults_Procedures]                    FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_EvalSessionAdminAdmissResults_Status]                       CHECK       ([Status] IN (1, 2, 3, 4))
);
GO

PRINT 'EvalSessionAdminAdmissProjects'
GO

CREATE TABLE [dbo].[EvalSessionAdminAdmissProjects] (
    [EvalSessionAdminAdmissProjectId]               INT                 NOT NULL IDENTITY,
    [EvalSessionAdminAdmissResultId]                INT                 NOT NULL,
    [ProjectId]                                     INT                 NOT NULL,
    [ProjectRegNumber]                              NVARCHAR(50)        NOT NULL,
    [ProjectName]                                   NVARCHAR(MAX)       NOT NULL,
    [ProjectNameAlt]                                NVARCHAR(MAX)       NULL,
    [CompanyName]                                   NVARCHAR(200)       NOT NULL,
    [CompanyNameAlt]                                NVARCHAR(200)       NULL,
    [NonAdmissionReason]                            NVARCHAR(MAX)       NULL,
    [AdminAdmissResult]                             INT                 NOT NULL,

    CONSTRAINT [PK_EvalSessionAdminAdmissProjects]                                  PRIMARY KEY ([EvalSessionAdminAdmissProjectId]),
    CONSTRAINT [FK_EvalSessionAdminAdmissProjects_EvalSessionAdminAdmissResults]    FOREIGN KEY ([EvalSessionAdminAdmissResultId])      REFERENCES [dbo].[EvalSessionAdminAdmissResults] ([EvalSessionAdminAdmissResultId]),
    CONSTRAINT [FK_EvalSessionAdminAdmissProjects_Projects]                         FOREIGN KEY ([ProjectId])                           REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [CHK_EvalSessionAdminAdmissProjects_AdminAdmissResult]               CHECK       ([AdminAdmissResult] IN (1, 2)),
);
GO
