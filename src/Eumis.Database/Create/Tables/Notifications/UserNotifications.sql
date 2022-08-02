PRINT 'UserNotifications'
CREATE TABLE [dbo].[UserNotifications] (
    [UserNotificationId]                        INT                 NOT NULL IDENTITY,
    [UserId]                                    INT                 NOT NULL,
    [NotificationEntryId]                       INT                 NOT NULL,
    [IsRead]                                    BIT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_UserNotifications]                               PRIMARY KEY ([UserNotificationId]),
    CONSTRAINT [FK_UserNotifications_Users]                         FOREIGN KEY ([UserId])                  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_UserNotifications_NotificationEnties]            FOREIGN KEY ([NotificationEntryId])     REFERENCES [dbo].[NotificationEntries]    ([NotificationEntryId]),
);

exec spDescTable  N'UserNotifications', N'Нотификации за потребители.'
exec spDescColumn N'UserNotifications', N'UserNotificationId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'UserNotifications', N'UserId'                        , N'Идентификатор на потребител.'
exec spDescColumn N'UserNotifications', N'NotificationEntryId'           , N'Индикатор на нотификация.'
exec spDescColumn N'UserNotifications', N'IsRead'                        , N'Прочетена нотификация: 0 - Не, 1 - Да.'
exec spDescColumn N'UserNotifications', N'CreateDate'                    , N'Дата на създаване.'
exec spDescColumn N'UserNotifications', N'ModifyDate'                    , N'Дата на модифициране.'
exec spDescColumn N'UserNotifications', N'Version'                       , N'Версия.'
