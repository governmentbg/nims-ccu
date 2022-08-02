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
    CONSTRAINT [CHK_ProcedureMonitorstatDocuments_Status]                   CHECK       ([Status]   IN (1, 2)),
    CONSTRAINT [CHK_ProcedureMonitorstatDocuments_Year]                     CHECK       ([Year]     IN (2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025)),
);
GO

exec spDescTable  N'ProcedureMonitorstatDocuments', N'Мониторстат изследвания към процедура.'
exec spDescColumn N'ProcedureMonitorstatDocuments', N'ProcedureMonitorstatDocumentId'                 , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureMonitorstatDocuments', N'ProcedureId'                                    , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureMonitorstatDocuments', N'Status'                                         , N'Статус: 1 - Чернова, 2 - Активна.'
exec spDescColumn N'ProcedureMonitorstatDocuments', N'Year'                                           , N'Година.'
exec spDescColumn N'ProcedureMonitorstatDocuments', N'MonitorstatSurveyId'                            , N'Идентификатор на Мониторстат изследване.'
exec spDescColumn N'ProcedureMonitorstatDocuments', N'MonitorstatReportId'                            , N'Идентификатор на справка към Мониторстат изследване.'

exec spDescColumn N'ProcedureMonitorstatDocuments', N'CreateDate'                                     , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureMonitorstatDocuments', N'ModifyDate'                                     , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureMonitorstatDocuments', N'Version'                                        , N'Версия.'
GO
