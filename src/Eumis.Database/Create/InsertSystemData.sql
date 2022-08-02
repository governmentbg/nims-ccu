USE [$(dbName)]
GO

---------------------------------------------------------------
-- Insert
---------------------------------------------------------------

-- System
:r $(rootPath)\"..\Insert\System\UpdateScripts.sql"

-- Institutions
:r $(rootPath)\"..\Insert\Institutions\InstitutionTypes.sql"

-- OperationalMap
:r $(rootPath)\"..\Insert\OperationalMap\Measures.sql"
:r $(rootPath)\"..\Insert\OperationalMap\BudgetPeriods.sql"
:r $(rootPath)\"..\Insert\OperationalMap\InterventionCategories.sql"

:r $(rootPath)\"..\Insert\ExcelConfig\InvestmentPriorities.sql"
:r $(rootPath)\"..\Insert\ExcelConfig\MapNodes.sql"
:r $(rootPath)\"..\Insert\ExcelConfig\MapNodeRelations.sql"
:r $(rootPath)\"..\Insert\ExcelConfig\Indicators.sql"
:r $(rootPath)\"..\Insert\ExcelConfig\MapNodeIndicators.sql"
:r $(rootPath)\"..\Insert\ExcelConfig\Institutions.sql"
:r $(rootPath)\"..\Insert\ExcelConfig\MapNodeInstitutions.sql"
:r $(rootPath)\"..\Insert\OperationalMap\MapNodeFinanceSources.sql"
:r $(rootPath)\"..\Insert\ExcelConfig\MapNodeBudgets.sql"

-- Geography
:r $(rootPath)\"..\Insert\Geography\Countries.sql"
:r $(rootPath)\"..\Insert\Geography\ProtectedZones.sql"
:r $(rootPath)\"..\Insert\Geography\Nuts1s.sql"
:r $(rootPath)\"..\Insert\Geography\Nuts2s.sql"
:r $(rootPath)\"..\Insert\Geography\Districts.sql"
:r $(rootPath)\"..\Insert\Geography\Municipalities.sql"
:r $(rootPath)\"..\Insert\Geography\Settlements.sql"

-- Procedure
:r $(rootPath)\"..\Insert\Procedures\ExpenseTypes.sql"
:r $(rootPath)\"..\Insert\Procedures\ExpenseSubTypes.sql"
:r $(rootPath)\"..\Insert\Procedures\ProcedureTypes.sql"

-- Companies
:r $(rootPath)\"..\Insert\Companies\KidCodes.sql"
:r $(rootPath)\"..\Insert\Companies\CompanySizeTypes.sql"
:r $(rootPath)\"..\Insert\Companies\CompanyTypes.sql"
:r $(rootPath)\"..\Insert\Companies\CompanyLegalTypes.sql"

-- Project
:r $(rootPath)\"..\Insert\Projects\ErrandLegalActs.sql"
:r $(rootPath)\"..\Insert\Projects\ErrandTypes.sql"
:r $(rootPath)\"..\Insert\Projects\ErrandAreas.sql"
:r $(rootPath)\"..\Insert\Projects\ProjectTypes.sql"

-- MonitoringFinancialControl
:r $(rootPath)\"..\Insert\MonitoringFinancialControl\OtherViolations.sql"
:r $(rootPath)\"..\Insert\MonitoringFinancialControl\FinancialCorrectionImposingReasons.sql"

-- Notifications
:r $(rootPath)\"..\Insert\Notifications\NotificationEvents.sql"
:r $(rootPath)\"..\Insert\Notifications\NotificationEventPermissions.sql"

-- Nomenclatures
:r $(rootPath)\"..\Insert\Nomenclatures\BulstatNomenclatures.sql"

-- Irregularities
:r $(rootPath)\"..\Insert\Irregularities\IrregularityCategories.sql"
:r $(rootPath)\"..\Insert\Irregularities\IrregularityTypes.sql"
:r $(rootPath)\"..\Insert\Irregularities\IrregularitySanctionCategories.sql"
:r $(rootPath)\"..\Insert\Irregularities\IrregularitySanctionTypes.sql"
:r $(rootPath)\"..\Insert\Irregularities\IrregularityFinancialStatuses.sql"

-- EvalSessions
:r $(rootPath)\"..\Insert\EvalSessions\EvalSessionProjectStandingRejectionReasons.sql"
