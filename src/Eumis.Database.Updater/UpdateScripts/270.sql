PRINT 'MonitorstatSurveys'
GO

CREATE TABLE [dbo].[MonitorstatSurveys] (
    [MonitorstatSurveyId]       INT                 NOT NULL IDENTITY,
    [Code]                      NVARCHAR(50)        NOT NULL UNIQUE,
    [Name]                      NVARCHAR(MAX)       NOT NULL,
    [Year]                      INT                 NOT NULL,

    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL,
    
    CONSTRAINT [PK_MonitorstatSurveys]              PRIMARY KEY ([MonitorstatSurveyId]),
    CONSTRAINT [CHK_MonitorstatSurveys_Year]        CHECK       ([Year]     IN (2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025)),
);
GO

PRINT 'MonitorstatReports'
GO

CREATE TABLE [dbo].[MonitorstatReports] (
    [MonitorstatReportId]               INT                 NOT NULL IDENTITY,
    [MonitorstatSurveyId]               INT                 NOT NULL,
    [Code]                              NVARCHAR(50)        NOT NULL,
    [Name]                              NVARCHAR(MAX)       NOT NULL,
    
    CONSTRAINT [PK_MonitorstatReports]                           PRIMARY KEY ([MonitorstatReportId]),
    CONSTRAINT [FK_MonitorstatReports_MonitorstatSurveys]        FOREIGN KEY ([MonitorstatSurveyId])             REFERENCES [dbo].[MonitorstatSurveys] ([MonitorstatSurveyId]),
);
GO

PRINT 'ProcedureMonitorstatDocuments'
GO

CREATE TABLE [dbo].[ProcedureMonitorstatDocuments] (
    [ProcedureMonitorstatDocumentId]                     INT                 NOT NULL IDENTITY,
    [ProcedureId]                                        INT                 NOT NULL,
    [Status]                                             INT                 NOT NULL,
    [Year]                                               INT                 NOT NULL,
    [MonitorstatSurveyId]                                INT                 NOT NULL,
    [MonitorstatReportId]                                INT                 NOT NULL,

    [CreateDate]                                         DATETIME2           NOT NULL,
    [ModifyDate]                                         DATETIME2           NOT NULL,
    [Version]                                            ROWVERSION          NOT NULL,
    
    CONSTRAINT [PK_ProcedureMonitorstatDocuments]                           PRIMARY KEY ([ProcedureMonitorstatDocumentId]),
    CONSTRAINT [FK_ProcedureMonitorstatDocuments_MonitorstatSurveys]        FOREIGN KEY ([MonitorstatSurveyId])             REFERENCES [dbo].[MonitorstatSurveys] ([MonitorstatSurveyId]),
    CONSTRAINT [FK_ProcedureMonitorstatDocuments_Procedures]                FOREIGN KEY ([ProcedureId])                     REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_ProcedureMonitorstatDocuments_MonitorstatReports]        FOREIGN KEY ([MonitorstatReportId])             REFERENCES [dbo].[MonitorstatReports] ([MonitorstatReportId]),
    CONSTRAINT [UQ_ProcedureMonitorstatDocuments]                           UNIQUE      ([ProcedureId], [MonitorstatReportId]),
    CONSTRAINT [CHK_ProcedureMonitorstatDocuments_Status]                   CHECK       ([Status]   IN (1, 2, 3)),
    CONSTRAINT [CHK_ProcedureMonitorstatDocuments_Year]                     CHECK       ([Year]     IN (2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025)),
);
GO

PRINT 'MonitorstatMapNodes'
GO

CREATE TABLE [dbo].[MonitorstatMapNodes] (
    [MonitorstatMapNodeId]                               INT                 NOT NULL IDENTITY,
    [MapNodeId]                                          INT                 NOT NULL,
    [Type]                                               INT                 NOT NULL,
    [MonitorstatGid]                                     UNIQUEIDENTIFIER    NOT NULL,
    
    [CreateDate]                                         DATETIME2           NOT NULL,
    [ModifyDate]                                         DATETIME2           NOT NULL,
    [Version]                                            ROWVERSION          NOT NULL,
    
    CONSTRAINT [PK_MonitorstatMapNodes]                  PRIMARY KEY ([MonitorstatMapNodeId]),
    CONSTRAINT [CHK_MonitorstatMapNodes_Type]            CHECK       ([Type]   IN (1, 2, 3)), -- 1 Programme, 2 Programme priority, 3 Procedure 
);
GO

PRINT 'ProcedureMonitorstatRequests'
GO

CREATE TABLE [dbo].[ProcedureMonitorstatRequests] (
    [ProcedureMonitorstatRequestId]                      INT                 NOT NULL IDENTITY,
    [ProcedureId]                                        INT                 NOT NULL,
    [Status]                                             INT                 NOT NULL,
    [Name]                                               NVARCHAR(MAX)       NULL,
    [ExecutionStartDate]                                 DATETIME2           NULL,
    [ExecutionEndDate]                                   DATETIME2           NULL,
    [MonitorstatInquiryGid]                              UNIQUEIDENTIFIER    NULL,
    
    [CreateDate]                                         DATETIME2           NOT NULL,
    [ModifyDate]                                         DATETIME2           NOT NULL,
    [Version]                                            ROWVERSION          NOT NULL,
    
    CONSTRAINT [PK_ProcedureMonitorstatRequests]                             PRIMARY KEY ([ProcedureMonitorstatRequestId]),
    CONSTRAINT [FK_ProcedureMonitorstatRequests_Procedures]                  FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureMonitorstatRequests_Status]                     CHECK       ([Status]   IN (1, 2)), -- 1 Draft, 2 Sent 
);
GO

PRINT 'ProjectMonitorstatRequests'
GO

CREATE TABLE [dbo].[ProjectMonitorstatRequests] (
    [ProjectMonitorstatRequestId]                        INT                 NOT NULL IDENTITY,
    [ProjectId]                                          INT                 NOT NULL,
    [ProcedureMonitorstatRequestId]                      INT                 NOT NULL,
    [ProjectVersionXmlId]                                INT                 NOT NULL,
    [Status]                                             INT                 NOT NULL,
    [ModifyDate]                                         DATETIME2           NOT NULL,
    [ProjectVersionXmlFileId]                            INT                 NOT NULL,
    [UserId]                                             INT                 NULL,

    CONSTRAINT [PK_ProjectMonitorstatRequests]                               PRIMARY KEY ([ProjectMonitorstatRequestId]),
    CONSTRAINT [FK_ProjectMonitorstatRequests_Projects]                      FOREIGN KEY ([ProjectId])                     REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectMonitorstatRequests_ProcedureMonitorstatRequests]  FOREIGN KEY ([ProcedureMonitorstatRequestId]) REFERENCES [dbo].[ProcedureMonitorstatRequests] ([ProcedureMonitorstatRequestId]),
    CONSTRAINT [FK_ProjectMonitorstatRequests_ProjectVersionXmls]            FOREIGN KEY ([ProjectVersionXmlId])           REFERENCES [dbo].[ProjectVersionXmls] ([ProjectVersionXmlId]),
    CONSTRAINT [FK_ProjectMonitorstatRequests_ProjectVersionXmlFiles]        FOREIGN KEY ([ProjectVersionXmlFileId])       REFERENCES [dbo].[ProjectVersionXmlFiles] ([ProjectVersionXmlFileId]),
    CONSTRAINT [FK_ProjectMonitorstatRequests_Users]                         FOREIGN KEY ([UserId])                        REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ProjectMonitorstatRequests_Status]                       CHECK       ([Status]   IN (1, 2, 3, 4)), -- 1 Draft, 2 Sent, 3 Canceled, 4 Prepared
);
GO
