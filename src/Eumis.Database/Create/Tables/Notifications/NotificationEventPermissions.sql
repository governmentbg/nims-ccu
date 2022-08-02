PRINT 'NotificationEventPermissions'
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

exec spDescTable  N'NotificationEventPermissions', N'Права за събития за нотификация.'
exec spDescColumn N'NotificationEventPermissions', N'NotificationEventPermissionId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NotificationEventPermissions', N'NotificationEventId'               , N'Идентификатор на събитие за нотификация.'
exec spDescColumn N'NotificationEventPermissions', N'Permission'                        , N'Право.'
exec spDescColumn N'NotificationEventPermissions', N'PermissionType'                    , N'Вид право.'
exec spDescColumn N'NotificationEventPermissions', N'ModifyDate'                        , N'Дата на модифициране.'
