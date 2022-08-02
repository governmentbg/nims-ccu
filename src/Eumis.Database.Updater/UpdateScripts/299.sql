GO

UPDATE [NotificationEventPermissions] SET [PermissionType] = 'CertAuthorityCommunication', [ModifyDate] = GETDATE()  WHERE [NotificationEventPermissionId] =  42;
UPDATE [NotificationEventPermissions] SET [PermissionType] = 'CertAuthorityCommunication', [ModifyDate] = GETDATE()  WHERE [NotificationEventPermissionId] =  43;

UPDATE [NotificationEventPermissions] SET [PermissionType] = 'AuditAuthorityCommunication', [ModifyDate] = GETDATE()  WHERE [NotificationEventPermissionId] =  54;
UPDATE [NotificationEventPermissions] SET [PermissionType] = 'AuditAuthorityCommunication', [ModifyDate] = GETDATE()  WHERE [NotificationEventPermissionId] =  55;

GO
