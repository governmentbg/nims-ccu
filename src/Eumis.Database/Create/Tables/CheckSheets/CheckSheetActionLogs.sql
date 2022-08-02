PRINT 'CheckSheetActionLogs'
GO

CREATE TABLE [dbo].[CheckSheetActionLogs] (
    [CheckSheetActionLogId]                     INT                 NOT NULL IDENTITY,
    [CheckSheetVersionXmlId]                    INT                 NULL,
    [CheckSheetId]                              INT                 NOT NULL,
    [VersionNum]                                INT                 NOT NULL,
    [UserId]                                    INT                 NOT NULL,
    [NotifiedUserId]                            INT                 NULL,
    [Role]                                      NVARCHAR(MAX)       NULL,
    [Reason]                                    NVARCHAR(MAX)       NULL,
    [Action]                                    INT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,

    CONSTRAINT [PK_CheckSheetActionLogs]                          PRIMARY KEY ([CheckSheetActionLogId]),
    CONSTRAINT [FK_CheckSheetActionLogs_CheckSheets]              FOREIGN KEY ([CheckSheetId])            REFERENCES [dbo].[CheckSheets] ([CheckSheetId]),
    CONSTRAINT [FK_CheckSheetActionLogs_CheckSheetVersionXmls]    FOREIGN KEY ([CheckSheetVersionXmlId])  REFERENCES [dbo].[CheckSheetVersionXmls] ([CheckSheetVersionXmlId]),
    CONSTRAINT [FK_CheckSheetActionLogs_Users]                    FOREIGN KEY ([UserId])                  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_CheckSheetActionLogs_NotifiedUsers]            FOREIGN KEY ([NotifiedUserId])          REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_CheckSheetActionLogs_Action]                  CHECK       ([Action] IN (1, 2, 3, 4, 5, 6, 7))
);
GO

exec spDescTable  N'CheckSheetActionLogs', N'Действие извършено върху контролен лист.'
exec spDescColumn N'CheckSheetActionLogs', N'CheckSheetActionLogId'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CheckSheetActionLogs', N'CheckSheetVersionXmlId'            , N'Идентификатор на Xml версия на контролен лист.'
exec spDescColumn N'CheckSheetActionLogs', N'CheckSheetId'                      , N'Идентификатор на контролен лист.'
exec spDescColumn N'CheckSheetActionLogs', N'VersionNum'                        , N'Пореден номер на версия.'
exec spDescColumn N'CheckSheetActionLogs', N'UserId'                            , N'Идентификатор на потребител извършил действието.'
exec spDescColumn N'CheckSheetActionLogs', N'NotifiedUserId'                    , N'Идентификатор на потребител уведомен при действие "Насочване".'
exec spDescColumn N'CheckSheetActionLogs', N'Role'                              , N'Роля на потребител извършил действието.'
exec spDescColumn N'CheckSheetActionLogs', N'Reason'                            , N'Причина за извършване на действието.'
exec spDescColumn N'CheckSheetActionLogs', N'Action'                            , N'Тип на действието: 1 - Създаване, 2 - Приключване, 3 - Връщане, 4 - Анулиране, 5 - Прекъсване, 6 - Активиране, 7 - Насочване.'
exec spDescColumn N'CheckSheetActionLogs', N'CreateDate'                        , N'Дата на създаване на записа.'

GO
