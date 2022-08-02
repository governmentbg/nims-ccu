PRINT 'EvalSessionReports'
GO

CREATE TABLE [dbo].[EvalSessionReports] (
    [EvalSessionReportId]   INT             NOT NULL IDENTITY,
    [EvalSessionId]         INT             NOT NULL,
    [RegNumber]             NVARCHAR(200)   NOT NULL,
    [Type]                  INT             NOT NULL,
    [Description]           NVARCHAR(MAX)   NOT NULL,
    [IsDeleted]             BIT             NOT NULL,
    [IsDeletedNote]         NVARCHAR(MAX)   NULL,
    [CreateDate]            DATETIME2       NOT NULL,
    [ModifyDate]            DATETIME2       NOT NULL,
    [Version]               ROWVERSION      NOT NULL,

    CONSTRAINT [PK_EvalSessionReports]                  PRIMARY KEY ([EvalSessionReportId]),
    CONSTRAINT [FK_EvalSessionReports_EvalSessions]     FOREIGN KEY ([EvalSessionId])       REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [CHK_EvalSessionReports_Type]            CHECK       ([Type] IN (1, 2, 3))
);
GO

exec spDescTable  N'EvalSessionReports', N'Доклади към оценителна сесия.'
exec spDescColumn N'EvalSessionReports', N'EvalSessionReportId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionReports', N'EvalSessionId'          , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionReports', N'RegNumber'              , N'Системно генериран регистрационен номер.'
exec spDescColumn N'EvalSessionReports', N'Type'                   , N'1 - Протокол; 2 - Доклад; 3 - Решение.'
exec spDescColumn N'EvalSessionReports', N'Description'            , N'Коментар/описание.'
exec spDescColumn N'EvalSessionReports', N'IsDeleted'              , N'Маркер, дали обобщената оценка е изтрита.'
exec spDescColumn N'EvalSessionReports', N'IsDeletedNote'          , N'Причина за изтриване.'
exec spDescColumn N'EvalSessionReports', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'EvalSessionReports', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'EvalSessionReports', N'Version'                , N'Версия.'
GO
