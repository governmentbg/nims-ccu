USE [$(dbName)]
GO

---------------------------------------------------------------
-- Insert
---------------------------------------------------------------

-- Users
:r $(rootPath)\"..\Insert\Users\PermissionTemplates.sql"
:r $(rootPath)\"..\Insert\Users\UserOrganizations.sql"
:r $(rootPath)\"..\Insert\Users\UserTypes.sql"
:r $(rootPath)\"..\Insert\Users\ProdUsers.sql"
:r $(rootPath)\"..\Insert\Users\ProdUserPermissions.sql"
