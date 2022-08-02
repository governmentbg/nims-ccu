PRINT 'NotificationSettingSets'

CREATE TABLE [dbo].[NotificationSettingSets] (
    [NotificationSettingSetId]                  INT                 NOT NULL IDENTITY,
    [NotificationSettingId]                     INT                 NOT NULL,
    [Scope]                                     INT                 NOT NULL,
    [Identifier]                                INT                 NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL
    
    CONSTRAINT [PK_NotificationSettingSets]                                    PRIMARY KEY     ([NotificationSettingSetId]),
    CONSTRAINT [FK_NotificationSettingSets_UserNotificationSettings]           FOREIGN KEY     ([NotificationSettingId])       REFERENCES [dbo].[NotificationSettings] ([NotificationSettingId]),
    CONSTRAINT [CHK_NotificationSettingSets_Scope]                             CHECK           ([Scope] IN (1, 2, 3, 4)),
);

exec spDescTable  N'NotificationSettingSets', N'Набор от обекти за нотификация.'
exec spDescColumn N'NotificationSettingSets', N'NotificationSettingSetId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationSettingSets', N'NotificationSettingId'              , N'Идентификатор на настройка за нотификация на потребител.'
exec spDescColumn N'NotificationSettingSets', N'Scope'                              , N'Обхват: 1 - Оперативна програма, 2 - Приоритетна ос, 3 - Процедура, 4 - Договор.'
exec spDescColumn N'NotificationSettingSets', N'Identifier'                         , N'Идентификатор на обект: Оперативна програма, Приоритетна ос, Процедура, Договор.'
exec spDescColumn N'NotificationSettingSets', N'ModifyDate'                         , N'Дата на модифициране.'
