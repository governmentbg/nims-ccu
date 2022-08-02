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

exec spDescTable  N'MonitorstatSurveys', N'Изследвания от Мониторстат.'
exec spDescColumn N'MonitorstatSurveys', N'MonitorstatSurveyId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MonitorstatSurveys', N'Code'                   , N'Код.'
exec spDescColumn N'MonitorstatSurveys', N'Name'                   , N'Наименование.'
exec spDescColumn N'MonitorstatSurveys', N'Year'                   , N'Година.'
exec spDescColumn N'MonitorstatSurveys', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'MonitorstatSurveys', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'MonitorstatSurveys', N'Version'                , N'Версия.'
GO
