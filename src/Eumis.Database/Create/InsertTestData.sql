USE [$(dbName)]
GO

---------------------------------------------------------------
-- Insert
---------------------------------------------------------------

-- System
:r $(rootPath)\"..\Insert\System\TestBlobContentLocations.sql"
:r $(rootPath)\"..\Insert\System\TestBlobs.sql"

-- Procedure
:r $(rootPath)\"..\Insert\Procedures\TestYouthEmploymentProcedure.sql"
:r $(rootPath)\"..\Insert\Procedures\TestBigProcedure.sql"
:r $(rootPath)\"..\Insert\Procedures\TestGipsyProcedure.sql"

-- Companies
:r $(rootPath)\"..\Insert\Companies\TestCompanies.sql"

-- Registrations
:r $(rootPath)\"..\Insert\Registrations\TestRegistrations.sql"

-- Users
:r $(rootPath)\"..\Insert\Users\PermissionTemplates.sql"
:r $(rootPath)\"..\Insert\Users\UserOrganizations.sql"
:r $(rootPath)\"..\Insert\Users\UserTypes.sql"
:r $(rootPath)\"..\Insert\Users\TestUsers.sql"
:r $(rootPath)\"..\Insert\Users\TestUserPermissions.sql"

-- Project
:r $(rootPath)\"..\Insert\Projects\TestProjects.sql"

-- EvalSession
:r $(rootPath)\"..\Insert\EvalSessions\EvalSessions.sql"
:r $(rootPath)\"..\Insert\EvalSessions\EvalSessionUsers.sql"
:r $(rootPath)\"..\Insert\EvalSessions\EvalSessionProjects.sql"
:r $(rootPath)\"..\Insert\EvalSessions\EvalSessionSheets.sql"
:r $(rootPath)\"..\Insert\EvalSessions\EvalSessionEvaluations.sql"
:r $(rootPath)\"..\Insert\EvalSessions\EvalSessionProjectStandings.sql"

-- EvalTables
:r $(rootPath)\"..\Insert\Procedures\EvalTables.sql"

--Contracts
:r $(rootPath)\"..\Insert\Contracts\Contracts.sql"
:r $(rootPath)\"..\Insert\Contracts\ContractReportMicrosDistricts.sql"
:r $(rootPath)\"..\Insert\Contracts\ContractReportMicrosMunicipalities.sql"
:r $(rootPath)\"..\Insert\Contracts\ContractReportMicrosSettlements.sql"

--SapImport
:r $(rootPath)\"..\Insert\SapInterfaces\SapSchemas.sql"
