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
    CONSTRAINT [CHK_ProjectMonitorstatRequests_Status]                       CHECK       ([Status]   IN (1, 2, 3, 4)),
);
GO

exec spDescTable  N'ProjectMonitorstatRequests', N'Заявки за изследване на бенефициент от Мониторстат.'
exec spDescColumn N'ProjectMonitorstatRequests', N'ProjectMonitorstatRequestId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectMonitorstatRequests', N'ProjectId'                           , N'Идентификатор на проектно предложение.'
exec spDescColumn N'ProjectMonitorstatRequests', N'ProcedureMonitorstatRequestId'       , N'Идентификатор на заявка за изследване към процедура.'
exec spDescColumn N'ProjectMonitorstatRequests', N'ProjectVersionXmlId'                 , N'Идентификатор на версия на проектно предложение.'
exec spDescColumn N'ProjectMonitorstatRequests', N'Status'                              , N'Статус: 1 - Чернова, 2 - Изпратено, 3 - Отказано, 4 - Изготвено.'
exec spDescColumn N'ProjectMonitorstatRequests', N'ModifyDate'                          , N'Дата на модифициране.'
exec spDescColumn N'ProjectMonitorstatRequests', N'ProjectVersionXmlFileId'             , N'Идентификатор на декларация.'
exec spDescColumn N'ProjectMonitorstatRequests', N'UserId'                              , N'Идентификатор на потребител изпратил заявка.'
GO
