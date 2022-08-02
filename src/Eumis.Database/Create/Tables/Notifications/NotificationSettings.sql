GO
PRINT 'NotificationSettings'
CREATE TABLE [dbo].[NotificationSettings] (
    [NotificationSettingId]                     INT                 NOT NULL IDENTITY,
    [UserId]                                    INT                 NOT NULL,
    [NotificationEventId]                       INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [ProgrammeId]                               INT                 NULL,
    [Scope]                                     INT                 NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_NotificationSettings]                            PRIMARY KEY ([NotificationSettingId]),
    CONSTRAINT [FK_NotificationSettings_Users]                      FOREIGN KEY ([UserId])                      REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_NotificationSettings_NotificationEvents]         FOREIGN KEY ([NotificationEventId])         REFERENCES [dbo].[NotificationEvents] ([NotificationEventId]),
    CONSTRAINT [FK_NotificationSettings_MapNodes]                   FOREIGN KEY ([ProgrammeId])                 REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [CHK_NotificationSettings_Scope]                     CHECK       ([Scope] IN (1, 2, 3, 4)),
);

exec spDescTable  N'NotificationSettings', N'Настройки за нотификация на потребител.'
exec spDescColumn N'NotificationSettings', N'NotificationSettingId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationSettings', N'UserId'                        , N'Идентификатор на потребител.'
exec spDescColumn N'NotificationSettings', N'NotificationEventId'           , N'Идентификатор на събитие за нотификация.'
exec spDescColumn N'NotificationSettings', N'Status'                        , N'Статус: 1 - Чернова, 2 - Актуален.'
exec spDescColumn N'NotificationSettings', N'ProgrammeId'                   , N'Идентификатор на оперативна програма.'
exec spDescColumn N'NotificationSettings', N'Scope'                         , N'Обхват: 1 - Оперативна програма, 2 - Приоритетна ос, 3 - Процедура, 4 - Договор.'
exec spDescColumn N'NotificationSettings', N'CreateDate'                    , N'Дата на създаване.'
exec spDescColumn N'NotificationSettings', N'ModifyDate'                    , N'Дата на модифициране.'
exec spDescColumn N'NotificationSettings', N'Version'                       , N'Версия.'
