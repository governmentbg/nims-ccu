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
    CONSTRAINT [CHK_ProcedureMonitorstatRequests_Status]                     CHECK       ([Status]   IN (1, 2)),
);
GO

exec spDescTable  N'ProcedureMonitorstatRequests', N'Заявки за Мониторстат изследвания към процедура.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'ProcedureMonitorstatRequestId'                  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'ProcedureId'                                    , N'Идентификатор на процедура.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'Status'                                         , N'Статус: 1 - Чернова, 2 - Активна.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'Name'                                           , N'Наименование.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'ExecutionStartDate'                             , N'Начална дата за изпълнението на заявка от Мониторстат.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'ExecutionEndDate'                               , N'Крайна дата за изпълнение на заявка от Мониторстат.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'MonitorstatInquiryGid'                          , N'Идентификатор на заявка към Мониторстат изследване.'

exec spDescColumn N'ProcedureMonitorstatRequests', N'CreateDate'                                     , N'Дата на създаване на записа.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'ModifyDate'                                     , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ProcedureMonitorstatRequests', N'Version'                                        , N'Версия.'
GO
