PRINT 'ProcedureMonitorstatEconomicActivities'
GO

CREATE TABLE [dbo].[ProcedureMonitorstatEconomicActivities] (
    [ProcedureMonitorstatEconomicActivityId]             INT                 NOT NULL IDENTITY,
    [ProcedureId]                                        INT                 NOT NULL,
    [Status]                                             INT                 NOT NULL,
    [Year]                                               INT                 NOT NULL,
    [Type]                                               INT                 NOT NULL,

    [CreateDate]                                         DATETIME2           NOT NULL,
    [ModifyDate]                                         DATETIME2           NOT NULL,
    [Version]                                            ROWVERSION          NOT NULL,

    CONSTRAINT [PK_ProcedureMonitorstatEconomicActivities]                           PRIMARY KEY ([ProcedureMonitorstatEconomicActivityId]),
    CONSTRAINT [FK_ProcedureMonitorstatEconomicActivities_Procedures]                FOREIGN KEY ([ProcedureId])                     REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [CHK_ProcedureMonitorstatEconomicActivities_Type]                     CHECK       ([Type]     IN (1, 2)),
    CONSTRAINT [CHK_ProcedureMonitorstatEconomicActivities_Status]                   CHECK       ([Status]   IN (1, 2)),
    CONSTRAINT [CHK_ProcedureMonitorstatEconomicActivities_Year]                     CHECK       ([Year]     IN (2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025)),
    CONSTRAINT [UQ_ProcedureMonitorstatEconomicActivities_Procedure_Year]            UNIQUE      ([ProcedureId], [Year])
);
GO

exec spDescTable  N'ProcedureMonitorstatEconomicActivities', N'Мониторстат икономическа дейност към процедура.'
exec spDescColumn N'ProcedureMonitorstatEconomicActivities', N'ProcedureMonitorstatEconomicActivityId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureMonitorstatEconomicActivities', N'ProcedureId'                                    , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureMonitorstatEconomicActivities', N'Year'                                           , N'Година.'
exec spDescColumn N'ProcedureMonitorstatEconomicActivities', N'Type'                                           , N'Тип: 1 - Основна, 2 - Основна и допълнителна.'
exec spDescColumn N'ProcedureMonitorstatEconomicActivities', N'Status'                                         , N'Статус: 1 - Чернова, 2 - Активен.'

exec spDescColumn N'ProcedureMonitorstatEconomicActivities', N'CreateDate'                                     , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureMonitorstatEconomicActivities', N'ModifyDate'                                     , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureMonitorstatEconomicActivities', N'Version'                                        , N'Версия.'
GO

GO

ALTER TABLE [dbo].[MonitorstatSurveys] DROP CONSTRAINT [CHK_MonitorstatSurveys_Year]
GO
ALTER TABLE [dbo].[MonitorstatSurveys]
WITH CHECK ADD CONSTRAINT [CHK_MonitorstatSurveys_Year]
CHECK ([Year]     IN (2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025))
GO

ALTER TABLE [dbo].[ProcedureMonitorstatDocuments] DROP CONSTRAINT [CHK_ProcedureMonitorstatDocuments_Year]
GO
ALTER TABLE [dbo].[ProcedureMonitorstatDocuments]
WITH CHECK ADD CONSTRAINT [CHK_ProcedureMonitorstatDocuments_Year]
CHECK ([Year]     IN (2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025))
GO

ALTER TABLE [dbo].[ProcedureMonitorstatEconomicActivities] DROP CONSTRAINT [CHK_ProcedureMonitorstatEconomicActivities_Year]
GO
ALTER TABLE [dbo].[ProcedureMonitorstatEconomicActivities]
WITH CHECK ADD CONSTRAINT [CHK_ProcedureMonitorstatEconomicActivities_Year]
CHECK ([Year]     IN (2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025))
GO

PRINT 'ProjectMonitorstatResponses'
GO

CREATE TABLE [dbo].[ProjectMonitorstatResponses] (
    [ProjectMonitorstatResponseId]                       INT                 NOT NULL IDENTITY,
    [ProjectMonitorstatRequestId]                        INT                 NOT NULL,
    [FileName]                                           NVARCHAR(MAX)       NOT NULL,
    [FileKey]                                            UNIQUEIDENTIFIER    NOT NULL,
    [ModifyDate]                                         DATETIME2           NOT NULL,
    
    CONSTRAINT [PK_ProjectMonitorstatResponses]                               PRIMARY KEY ([ProjectMonitorstatResponseId]),
    CONSTRAINT [FK_ProjectMonitorstatResponses_ProjectMonitorstatRequests]    FOREIGN KEY ([ProjectMonitorstatRequestId])   REFERENCES [dbo].[ProjectMonitorstatRequests] ([ProjectMonitorstatRequestId]),
    CONSTRAINT [FK_ProjectMonitorstatResponses_Blobs]                         FOREIGN KEY ([FileKey])                       REFERENCES [dbo].[Blobs] ([Key]),
);
GO

GO
ALTER TABLE [dbo].[ProjectMonitorstatRequests] DROP CONSTRAINT [CHK_ProjectMonitorstatRequests_Status]
GO
ALTER TABLE [dbo].[ProjectMonitorstatRequests]
WITH CHECK ADD CONSTRAINT [CHK_ProjectMonitorstatRequests_Status]
CHECK ([Status]     IN (1, 2, 3, 4, 5))
GO
ALTER TABLE [dbo].[ProjectMonitorstatRequests] ADD [ForeignGid] UNIQUEIDENTIFIER NULL
GO