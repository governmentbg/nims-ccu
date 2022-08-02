GO
ALTER TABLE [dbo].[RegDataRequests] ADD
    [UserOrganizationId] INT NULL,
    [UserTypeId] INT NULL
GO

UPDATE [dbo].[RegDataRequests]
SET [UserOrganizationId] = u.UserOrganizationId,
    [UserTypeId] = u.UserTypeId
FROM [dbo].[RegDataRequests] rdr
INNER JOIN [dbo].[Users] u ON rdr.UserId = u.UserId;
GO

ALTER TABLE [dbo].[RegDataRequests] ALTER COLUMN [UserOrganizationId] INT NOT NULL;
ALTER TABLE [dbo].[RegDataRequests] ALTER COLUMN [UserTypeId] INT NOT NULL;
GO
