-- 6c08d87 Remove registration certificates

GO
DROP TABLE RegCerts;

GO
ALTER TABLE Registrations DROP COLUMN CertThumb;

GO
DELETE UserPermissions WHERE PermissionType = 'Registration' AND Permission = 'CanAddRegCerts';

ALTER TABLE RequestPackages ADD
    [EndedByUserId] INT NULL,
    CONSTRAINT [FK_RequestPackages_EndedByUser] FOREIGN KEY ([EndedByUserId]) REFERENCES [dbo].[Users] ([UserId]);

-- 65c77c5 Add user password recovery
ALTER TABLE Users ADD PasswordRecoveryCode NVARCHAR (50) NULL;
