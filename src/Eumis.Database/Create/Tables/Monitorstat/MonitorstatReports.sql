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

exec spDescTable  N'MonitorstatReports', N'Справки към изследвания от Мониторстат.'
exec spDescColumn N'MonitorstatReports', N'MonitorstatReportId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MonitorstatReports', N'Code'                   , N'Код.'
exec spDescColumn N'MonitorstatReports', N'Name'                   , N'Наименование.'
exec spDescColumn N'MonitorstatReports', N'MonitorstatSurveyId'    , N'Идентификатор на изследване.'
GO
