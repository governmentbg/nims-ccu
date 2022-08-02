USE [$(dbName)]
GO

---------------------------------------------------------------
--Tools
---------------------------------------------------------------

:r $(rootPath)\"Tools\Tool_ScriptDiagram2008.sql"
:r $(rootPath)\"Tools\spDesc.sql"
:r $(rootPath)\"Tools\sp_generate_inserts.sql"

---------------------------------------------------------------
--Tables
---------------------------------------------------------------
-- System
:r $(rootPath)"\Tables\System\GParams.sql"
:r $(rootPath)"\Tables\System\Logs.sql"
:r $(rootPath)"\Tables\System\Blobs.sql"
:r $(rootPath)"\Tables\System\BlobContents.sql"
:r $(rootPath)"\Tables\System\LoginCertificates.sql"

-- Users
:r $(rootPath)"\Tables\Users\Users.sql"
:r $(rootPath)"\Tables\Users\Roles.sql"
:r $(rootPath)"\Tables\Users\UserRoles.sql"

--Nuts
:r $(rootPath)"\Tables\Nuts\Countries.sql"
:r $(rootPath)"\Tables\Nuts\ProtectedZones.sql"
:r $(rootPath)"\Tables\Nuts\Nuts1s.sql"
:r $(rootPath)"\Tables\Nuts\Nuts2s.sql"
:r $(rootPath)"\Tables\Nuts\Districts.sql"
:r $(rootPath)"\Tables\Nuts\Municipalities.sql"
:r $(rootPath)"\Tables\Nuts\Settlements.sql"

--Companies
:r $(rootPath)"\Tables\Companies\KidCodes.sql"
:r $(rootPath)"\Tables\Companies\CompanyTypes.sql"
:r $(rootPath)"\Tables\Companies\CompanyLegalTypes.sql"
:r $(rootPath)"\Tables\Companies\CompanySizeTypes.sql"

---------------------------------------------------------------
--Diagrams
---------------------------------------------------------------
:r $(rootPath)"\Diagrams\Nuts.sql"


---------------------------------------------------------------
-- Insert
---------------------------------------------------------------

-- Companies
:r $(rootPath)\"..\Insert\Companies\KidCodes.sql"
:r $(rootPath)\"..\Insert\Companies\CompanyTypes.sql"
:r $(rootPath)\"..\Insert\Companies\CompanyLegalTypes.sql"
:r $(rootPath)\"..\Insert\Companies\CompanySizeTypes.sql"

-- Geography
:r $(rootPath)\"..\Insert\Geography\Countries.sql"
:r $(rootPath)\"..\Insert\Geography\ProtectedZones.sql"
:r $(rootPath)\"..\Insert\Geography\Nuts1s.sql"
:r $(rootPath)\"..\Insert\Geography\Nuts2s.sql"
:r $(rootPath)\"..\Insert\Geography\Districts.sql"
:r $(rootPath)\"..\Insert\Geography\Municipalities.sql"
:r $(rootPath)\"..\Insert\Geography\Settlements.sql"

