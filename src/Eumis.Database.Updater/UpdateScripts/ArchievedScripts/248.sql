PRINT 'NotificationEvents'
GO

CREATE TABLE [dbo].[NotificationEvents] (
    [NotificationEventId]       INT             NOT NULL IDENTITY,
    [Name]                      NVARCHAR(200)   NOT NULL,
    [NameAlt]                   NVARCHAR(200)   NULL,
    [IsProgrammeDependent]      BIT             NOT NULL,
    [ModifyDate]                DATETIME2       NOT NULL
    CONSTRAINT [PK_NotificationEvents]          PRIMARY KEY ([NotificationEventId])
);
GO
exec spDescTable  N'NotificationEvents', N'Събития за нотификация.'
exec spDescColumn N'NotificationEvents', N'NotificationEventId'             , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationEvents', N'Name'                            , N'Наименование на събитие.'
exec spDescColumn N'NotificationEvents', N'IsProgrammeDependent'            , N'Зависи от оперативната програма: 0 - Не, 1 - Да.'
exec spDescColumn N'NotificationEvents', N'ModifyDate'                      , N'Дата на модифициране.'


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
GO

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

GO

PRINT 'NotificationEventPermissions'
GO

CREATE TABLE [dbo].[NotificationEventPermissions] (
    [NotificationEventPermissionId]     INT                 NOT NULL IDENTITY,
    [NotificationEventId]               INT                 NOT NULL,
    [Permission]                        NVARCHAR (100)      NOT NULL,
    [PermissionType]                    NVARCHAR (100)      NOT NULL,
    [ModifyDate]                        DATETIME2           NOT NULL
    CONSTRAINT [PK_NotificationEventPermissions]                            PRIMARY KEY     ([NotificationEventPermissionId]),
    CONSTRAINT [UQ_NotificationEventPermissions]                            UNIQUE          ([NotificationEventId], [Permission], [PermissionType]),
    CONSTRAINT [FK_NotificationEventPermissions_NotificationEvents]         FOREIGN KEY     ([NotificationEventId])       REFERENCES [dbo].[NotificationEvents] ([NotificationEventId]),
);
GO
exec spDescTable  N'NotificationEventPermissions', N'Права за събития за нотификация.'
exec spDescColumn N'NotificationEventPermissions', N'NotificationEventPermissionId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationEventPermissions', N'NotificationEventId'               , N'Идентификатор на събитие за нотификация.'
exec spDescColumn N'NotificationEventPermissions', N'Permission'                        , N'Право.'
exec spDescColumn N'NotificationEventPermissions', N'PermissionType'                    , N'Вид право.'
exec spDescColumn N'NotificationEventPermissions', N'ModifyDate'                        , N'Дата на модифициране.'

PRINT 'NotificationSettingSets'
GO

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
GO
exec spDescTable  N'NotificationSettingSets', N'Набор от обекти за нотификация.'
exec spDescColumn N'NotificationSettingSets', N'NotificationSettingSetId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationSettingSets', N'NotificationSettingId'              , N'Идентификатор на настройка за нотификация на потребител.'
exec spDescColumn N'NotificationSettingSets', N'Scope'                              , N'Обхват: 1 - Оперативна програма, 2 - Приоритетна ос, 3 - Процедура, 4 - Договор.'
exec spDescColumn N'NotificationSettingSets', N'Identifier'                         , N'Идентификатор на обект: Оперативна програма, Приоритетна ос, Процедура, Договор.'
exec spDescColumn N'NotificationSettingSets', N'ModifyDate'                         , N'Дата на модифициране.'

GO
PRINT 'NotificationEntries'
CREATE TABLE [dbo].[NotificationEntries] (
    [NotificationEntryId]                       INT                 NOT NULL IDENTITY,
    [NotificationEventId]                       INT                 NOT NULL,
    [Status]                                    INT                 NOT NULL,
    [DispatcherId]                              INT                 NOT NULL,
    [ProgrammeId]                               INT                 NULL,
    [ProgrammePriorityId]                       INT                 NULL,
    [ProcedureId]                               INT                 NULL,
    [ContractId]                                INT                 NULL,
    [DispatcherPath]                            NVARCHAR(100)       NULL,
    [FailedAttempts]                            INT                 NOT NULL,
    [CreateDate]                                DATETIME2           NOT NULL,
    [ModifyDate]                                DATETIME2           NOT NULL,
    [Version]                                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_NotificationEntries]                             PRIMARY KEY ([NotificationEntryId]),
    CONSTRAINT [FK_NotificationEntries_NotificationEvents]          FOREIGN KEY ([NotificationEventId])     REFERENCES [dbo].[NotificationEvents]    ([NotificationEventId]),
    CONSTRAINT [CHK_NotificationEntries_Status]                     CHECK       ([Status]                   IN (1, 2, 3)),
);
GO

exec spDescTable  N'NotificationEntries', N'Нотификации.'
exec spDescColumn N'NotificationEntries', N'NotificationEntryId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationEntries', N'NotificationEventId'        , N'Идентификатор на събитие за нотификация.'
exec spDescColumn N'NotificationEntries', N'Status'                     , N'Статус: 1 - За обработка, 2 - Грешка при обработка, 3 - Обработено.'
exec spDescColumn N'NotificationEntries', N'DispatcherId'               , N'Идентификатор на обекта генериращ нотификацията.'
exec spDescColumn N'NotificationEntries', N'ProgrammeId'                , N'Идентификатор на оперативна програма.'
exec spDescColumn N'NotificationEntries', N'ProgrammePriorityId'        , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'NotificationEntries', N'ProcedureId'                , N'Идентификатор на процедура.'
exec spDescColumn N'NotificationEntries', N'ContractId'                 , N'Идентификатор на договор.'
exec spDescColumn N'NotificationEntries', N'DispatcherPath'             , N'Адрес на който е достъпен ресурса.'
exec spDescColumn N'NotificationEntries', N'FailedAttempts'             , N'Брой неуспешни опити за обработка.'
exec spDescColumn N'NotificationEntries', N'CreateDate'                 , N'Дата на създаване.'
exec spDescColumn N'NotificationEntries', N'ModifyDate'                 , N'Дата на модифициране.'
exec spDescColumn N'NotificationEntries', N'Version'                    , N'Версия.'

GO

GO
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
GO

exec spDescTable  N'UserNotifications', N'Нотификации за потребители.'
exec spDescColumn N'UserNotifications', N'UserNotificationId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'UserNotifications', N'UserId'                        , N'Идентификатор на потребител.'
exec spDescColumn N'UserNotifications', N'NotificationEntryId'           , N'Индикатор на нотификация.'
exec spDescColumn N'UserNotifications', N'IsRead'                        , N'Прочетена нотификация: 0 - Не, 1 - Да.'
exec spDescColumn N'UserNotifications', N'CreateDate'                    , N'Дата на създаване.'
exec spDescColumn N'UserNotifications', N'ModifyDate'                    , N'Дата на модифициране.'
exec spDescColumn N'UserNotifications', N'Version'                       , N'Версия.'

-- INSERT SCRIPTS --

GO
SET IDENTITY_INSERT [dbo].[NotificationEvents] ON
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (1, N'Корекция на индикатори', N'On indicator correction', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (2, N'Корекция на данни от ОП', N'On data correction for OP', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (3, N'Промяна статуса на процедура от Активна в Чернова', N'On procedure status change from Active to Draft', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (4, N'Подадено проектно предложение', N'On project submission', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (5, N'Нова версия на процедура за избора на изпълнител', N'On new procurement activation', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (6, N'Нова версия на план за разходване на средства', N'On new spending plan activation', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (7, N'Получаване на нова кореспонденция', N'On new communication received', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (8, N'Получаване подаване на ПОД', N'On contract report change status to sent unchecked', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (9, N'Повторно изпращане на искане за плащане', N'On contract report payment re-sent', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (10, N'Връщане на искане за плащане към ПОД', N'On contract report payment status to returned', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (11, N'Връщане на финансов отчет към ПОД', N'On contract report financial status to returned', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (12, N'Повторно изпращане на финансов отчет към ПОД', N'On contract report financial re-sent', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (13, N'Връщане на технически отчет към ПОД', N'On contract report technical status to returned', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (14, N'Повторно изпращане на технически отчет към ПОД', N'On contract report technical re-sent', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (15, N'Връщане на микроданни към ПОД', N'On contract report microdata status to returned', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (16, N'Повторно изпращане на микроданни към ПОД', N'On contract report microdata re-sent', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (17, N'Подаден доклад по сертификaция', N'On submit cert report', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (18, N'Връщане на доклад по сертификация', N'On return cert report ', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (19, N'Получена комуникация със сертифициращ орган', N'On cert authority communication received', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (20, N'Промяна на статуса на пакет заявки от чернова на въведен', N'On request package status to entered', 0, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (21, N'Промяна на статуса на пакет заявки от въведен на проверен', N'On request package status to checked', 0, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (22, N'Промяна на статуса на пакет заявки от проверен на чернова', N'On request package status to draft', 0, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (23, N'Върнат отговор по проектно предложение от кандидат', N'On project candidate submit answer', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (24, N'Промяна на статуса на оценителен лист от Прекъснат на Продължена оценка', N'On eval sheet status change', 1, GETDATE())
GO
INSERT [dbo].[NotificationEvents] ([NotificationEventId], [Name], [NameAlt], [IsProgrammeDependent], [ModifyDate]) VALUES (25, N'Получена комуникация с одитиращ орган', N'On audit authority communication received', 1, GETDATE())
GO
SET IDENTITY_INSERT [dbo].[NotificationEvents] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[NotificationEventPermissions] ON
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (1, 1, N'CanWrite', N'Indicator', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (2, 2, N'CanWrite', N'Indicator', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (3, 3, N'CanCheck', N'Procedure', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (4, 4, N'CanRegister', N'Project', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (5, 5, N'CanWrite', N'Contract', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (6, 5, N'CanRead', N'Contract', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (7, 6, N'CanWrite', N'Contract', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (8, 6, N'CanRead', N'Contract', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (9, 7, N'CanWrite', N'ContractCommunication', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (10, 7, N'CanRead', N'ContractCommunication', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (11, 8, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (12, 8, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (13, 8, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (14, 9, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (15, 9, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (16, 9, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (17, 10, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (18, 10, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (19, 10, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (20, 11, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (21, 11, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (22, 11, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (23, 12, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (24, 12, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (25, 12, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (26, 13, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (27, 13, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (28, 13, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (29, 14, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (30, 14, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (31, 14, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (32, 15, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (33, 15, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (34, 15, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (35, 16, N'CanRead', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (36, 16, N'CanWriteFinancial', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (37, 16, N'CanWriteTechnical', N'MonitoringFinancialControl', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (38, 17, N'CanRead', N'Certification', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (39, 17, N'CanWrite', N'Certification', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (40, 18, N'CanRead', N'Certification', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (41, 18, N'CanWrite', N'Certification', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (42, 19, N'CanRead', N'Certification', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (43, 19, N'CanWrite', N'Certification', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (44, 20, N'CanAdministrate', N'UserAdmin', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (45, 20, N'CanControl', N'UserAdmin', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (46, 21, N'CanAdministrate', N'UserAdmin', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (47, 21, N'CanControl', N'UserAdmin', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (48, 22, N'CanAdministrate', N'UserAdmin', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (49, 22, N'CanControl', N'UserAdmin', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (50, 23, N'CanAdministrate', N'EvalSession', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (51, 23, N'CanEvaluate', N'EvalSession', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (52, 24, N'CanAdministrate', N'EvalSession', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (53, 24, N'CanEvaluate', N'EvalSession', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (54, 25, N'CanRead', N'Audit', GETDATE())
GO
INSERT [dbo].[NotificationEventPermissions] ([NotificationEventPermissionId], [NotificationEventId], [Permission], [PermissionType], [ModifyDate]) VALUES (55, 25, N'CanWrite', N'Audit', GETDATE())
GO
SET IDENTITY_INSERT [dbo].[NotificationEventPermissions] OFF
GO
